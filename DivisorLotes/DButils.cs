using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace DivisorLotes
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
                strsql = strsql + " select razons, codalm from T_ALMACEN";
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

        public int getIdAlmacen(string almacen)
        {
            int id = 0;

            string strsql;
            string usuario = Environment.UserName.ToUpper();
            empresa = empresa = empresaGlobal.empresaID;

            try
            {
                int charpos = almacen.IndexOf("#");
                almacen = almacen.Remove(charpos - 1);

                conexion.Open();

                strsql = "";
                strsql = "SELECT CODALM FROM T_ALMACEN WHERE RAZONS = '" + almacen + "'";
                SqlCommand SelectCommand = new SqlCommand(strsql, conexion);
                id = Int32.Parse(SelectCommand.ExecuteScalar().ToString());

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return id;
        }


        public void Dividir(string lote, string peso, int idog, int iddst)
        {
            string strsql;
            SqlCommand comando;
            string usuario = Environment.UserName.ToUpper();
            empresa = empresaGlobal.empresaID;

            try
            {
                conexion.Open();
                strsql = "";
                strsql = strsql + "exec gg.dbo.IME_Dividir_Lote '" + empresa + "', '" + lote + "', " + peso + ", '" + idog + "', '" + iddst + "', '" + usuario + "'";

                comando = new SqlCommand(strsql, conexion);
                string result = comando.ExecuteScalar().ToString();
                
                System.Windows.Forms.MessageBox.Show(result);
                

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool esMayorQueConsumo(string lote, int peso)
        {
            int consumo = 0;
            bool correcto = false;

            string strsql = "";
            empresa = empresaGlobal.empresaID;

            try
            {
                conexion.Open();
                strsql = "SELECT * FROM TC_SALIDAS_FAB WHERE Lote = '" + lote + "' and CodEmp = " + empresa;

                SqlCommand cmd = new SqlCommand(strsql, conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string flagp = reader["FLAGP"].ToString();
                        int cantidade = Convert.ToInt32(reader["CANTIDADE"].ToString());
                        int cantidadd = Convert.ToInt32(reader["CATIDADD"].ToString());
                        if (flagp == "0")
                        {
                            consumo += cantidade -  cantidadd;
                        }
                    }
                }
                else
                {
                    consumo = 0;
                }

                strsql = "SELECT STOCK FROM T_STOCKS WHERE Lote = '" + lote + "' and CodEmp = " + empresa;
                cmd = new SqlCommand(strsql, conexion);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        correcto = (Convert.ToInt32(reader["STOCK"].ToString()) - peso) > consumo;
                    }
                }
                else
                {
                    correcto = true;
                }


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return correcto;
        }
    }
}
