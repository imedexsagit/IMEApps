using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace GestorPesoClientes
{
    class DButils
    {
        public string empresa;                
        SqlConnection conexion;

        List<string> marcadosOriginal = new List<string>();


        public DButils()
        {
            
            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();                        
            conexion = new SqlConnection(Utils.CD.getConexion());
            //conexion = new SqlConnection("Data Source=srvdesarrollo;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");
        }

        public DataTable obtenerClientes()
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select CODCLI, ABREV, NOMBRE from T_CLIENTES";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' ORDER BY NOMBRE";

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

        public List<string> obtenerMarcados()
        {
            string strsql;
            SqlCommand comando;
            SqlDataReader reader;
            List<string> marcados = new List<string>();


            try
            {

                conexion.Open();

                strsql = "";
                strsql = strsql + " select CODCLI FROM T_CLIENTES_PESO_OCULTO";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' ORDER BY NOMBRE";

                comando = new SqlCommand(strsql, conexion);
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    marcados.Add(reader.GetString(0));
                    marcadosOriginal.Add(reader.GetString(0));

                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            return marcados;

        }

        public void guardarCambios(DataTable table)
        {

            SqlCommand comando;
            //SqlDataAdapter adapter;
            Cursor.Current = Cursors.WaitCursor;
            conexion.Open();

            string usuario = Environment.UserName.ToUpper();
            string strsql = "";
            bool elementos = false; //flag para comprobar elementos en cada loop

            try
            {
                strsql = "delete from T_CLIENTES_PESO_OCULTO WHERE CODEMP = '" + empresa + "' AND (";
                foreach (DataRow row in table.Rows) //loop de borrado
                {
                    if ((bool)row["CON_PESO"] == true && (marcadosOriginal.Contains(row["CODCLI"].ToString())))
                    {
                        elementos = true;
                        strsql += "CODCLI = '" + row["CODCLI"] + "' OR ";
                    }
                }

                strsql = strsql.Remove(strsql.Length - 3);
                strsql += ")";

                if (elementos)
                {
                    comando = new SqlCommand(strsql, conexion);
                    comando.ExecuteScalar();
                    //adapter = new SqlDataAdapter(comando);
                }

                elementos = false; //reset de flag
                strsql = "insert into T_CLIENTES_PESO_OCULTO (CODEMP, CODCLI, ABREV, NOMBRE, FECHAC, USUCRE) VALUES";
                foreach (DataRow row in table.Rows) //loop de inserción
                {
                    if ((bool)row["CON_PESO"] == false && (!marcadosOriginal.Contains(row["CODCLI"].ToString())))
                    {
                        elementos = true;
                        strsql += " ('" + empresa + "', '" + row["CODCLI"] + "', '" + row["ABREV"] + "', '" + row["NOMBRE"] + "', GETDATE(), '" + usuario + "'),";
                    }
                }

                strsql = strsql.Remove(strsql.Length - 1);

                if (elementos)
                {
                    comando = new SqlCommand(strsql, conexion);
                    comando.ExecuteScalar();
                    //adapter = new SqlDataAdapter(comando);
                }

                conexion.Close();
                System.Windows.Forms.MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE", "ATENCIÓN");
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            Cursor.Current = Cursors.Default;
            
        }
        
    }
}
