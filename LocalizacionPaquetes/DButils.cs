using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace LocalizacionPaquetes
{
    class DButils
    {
        public string empresa;                
        SqlConnection conexion;

        public DButils()
        {

            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();                        
            conexion = new SqlConnection(Utils.CD.getConexion());
            //conexion = new SqlConnection("Data Source=srvdesarrollo;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");
        }

        public DataTable obtenerAlmacenes()
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select DISTINCT razons, codalm from T_ALMACEN_PARCELAS";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' and codalm <> 99 and codalm <> 10";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable obtenerDatosParcelas(string codalm)
        {
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;
            string strsql = "";

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select naves, filas, columnas, fuera from T_ALMACEN_PARCELAS";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND CODALM = '" + codalm + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return table;
        }

        public DataTable obtenerDatosFuera(string codalm)
        {
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;
            string strsql = "";

            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select p1, p2 from T_ALMACEN_PARCELAS_F";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND CODALM = '" + codalm + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public bool existePaq(string cb_num)
        {
            bool existe = false;

            SqlCommand comando;
            SqlDataReader reader;
            string strsql = "";

            try
            {

                conexion.Open();
                strsql = "";

                if (!cb_num.Contains("-") && !cb_num.Contains("'"))
                {
                    cb_num = cb_num.Remove(0, 2); //se quita el 73 del principio
                    strsql = strsql + " select * from imedexsa_intranet.dbo.GL_PAQUE";
                    strsql = strsql + " WHERE   empresa = '" + empresa + "' AND cb_num = '" + cb_num + "' ";
                }
                else
                {
                    string codPacking = "";
                    string paquete = "";
                    string modpaq = "";
                    int contadorpaq = 1;

                    foreach (char c in cb_num)
                    {
                        if ((!c.Equals('-') && !c.Equals('\'')) && contadorpaq == 1) codPacking += c;
                        if ((!c.Equals('-') && !c.Equals('\'')) && contadorpaq == 2) paquete += c;
                        if ((!c.Equals('-') && !c.Equals('\'')) && contadorpaq == 3) modpaq += c;


                        if (c.Equals('-') || c.Equals('\''))
                        {
                            contadorpaq++;
                        }
                    }

                    if ((Convert.ToInt32(paquete) % 23 != Convert.ToInt32(modpaq)))
                    {
                        conexion.Close(); return false;
                    }


                    strsql = strsql + " select * from TC_PACKING_LIST";
                    strsql = strsql + " WHERE   codemp = '" + empresa + "' AND packing = '" + codPacking + "' ";
                }

                comando = new SqlCommand(strsql, conexion);
                reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    existe = true;
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return existe;

        }

        public bool checkMismaParcela(string paq, string parcela, string codalm)
        {
            bool mismaParcela = false;

            SqlCommand comando;
            SqlDataReader reader;
            string strsql = "";

            try
            {

                conexion.Open();


                strsql = "";
                strsql = strsql + " select * FROM T_PAQ_PARCELAS";

                

                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND CODALM = '" + codalm + "' AND CB_NUM = '" + formatearCodPaquete(paq) + "' ";
                strsql = strsql + " ORDER BY FECHAC DESC";
                

                comando = new SqlCommand(strsql, conexion);
                reader = comando.ExecuteReader();

                string parcelaCons;

                if (reader.Read())
                {
                    parcelaCons = reader["parcela"].ToString();

                    if (parcelaCons == parcela)
                    {
                        mismaParcela = true;
                    }
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return mismaParcela;
        }

        public void guardarParcela(string paq, string codalm, string[] parcela)
        {
            SqlCommand comando;
            string strsql = "";

            try
            {
                conexion.Open();

                /*if (parcela[0] == "P")
                {
                    strsql = "";

                    strsql = strsql + " SELECT * FROM T_ALMACEN_PARCELAS_F WHERE CODALM = '" + codalm + "' AND p1 = '" + parcela[1] + "' AND p2 = '" + parcela[2] + "'";


                    comando = new SqlCommand(strsql, conexion);
                    reader = comando.ExecuteReader();

                    if (!reader.Read())
                    {
                        System.Windows.Forms.MessageBox.Show("LA PARCELA SELECCIONADA NO EXISTE EN EL ALMACÉN", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conexion.Close();
                        return;
                    }
                }*/



                strsql = "";
                strsql = strsql + " INSERT INTO T_PAQ_PARCELAS (CODEMP, CB_NUM, CODALM, PARCELA, USUCRE, FECHAC) ";
                strsql = strsql + " VALUES ('" + empresa + "', '" + formatearCodPaquete(paq) + "', '" + codalm + "', '" + string.Join("",parcela) + "', '" + Environment.UserName + "', getdate())";

                if ((string.Join("", parcela) != "1A1" && string.Join("", parcela) != "1B1" && codalm == "5") || codalm != "5") //Comprobación para SanDeCa
                {
                    comando = new SqlCommand(strsql, conexion);
                    comando.ExecuteNonQuery();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("LA PARCELA SELECCIONADA NO EXISTE EN EL ALMACÉN", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable historialPaquete(string codalm, string paq)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                

                strsql = "";
                strsql = strsql + " select PARCELA, (SELECT TOP (1) RAZONS FROM T_ALMACEN_PARCELAS tap WHERE tap.CODALM = tpp.CODALM) as ALMACEN, FECHAC from T_PAQ_PARCELAS tpp";
                strsql = strsql + " WHERE   /*CODEMP = '" + empresa + "' AND*/ CB_NUM = '"+ formatearCodPaquete(paq) +"' ORDER BY FECHAC DESC";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }


        public string formatearCodPaquete(string paq)
        {
            string nuevopaq = "";

            if (!paq.Contains("-") && !paq.Contains("'"))
            {
                nuevopaq = paq.Remove(0, 2); //se quita el 73 del principio

            }
            else
            {
                string codPacking = "";
                string paquete = "";
                string modpaq = "";
                int contadorpaq = 1;

                foreach (char c in paq)
                {
                    if ((!c.Equals('-') && !c.Equals('\'')) && contadorpaq == 1) codPacking += c;
                    if ((!c.Equals('-') && !c.Equals('\'')) && contadorpaq == 2) paquete += c;
                    if ((!c.Equals('-') && !c.Equals('\'')) && contadorpaq == 3) modpaq += c;


                    if (c.Equals('-') || c.Equals('\''))
                    {
                        contadorpaq++;
                    }
                }


                nuevopaq = codPacking + "-" + paquete + "-" + modpaq;

            }

            return nuevopaq;
        }
    }
}
