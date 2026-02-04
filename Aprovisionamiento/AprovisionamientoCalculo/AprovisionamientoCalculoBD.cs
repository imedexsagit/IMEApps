using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace AprovisionamientoCalculo
{
    class AprovisionamientoCalculoBD
    {


        public bool Tiene_Permisos(string empresa,string usuario)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string cantidad;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT  COUNT(*)";
                strsql = strsql + " FROM    TC_APROVISIONAMIENTO_CALCULOS_Permisos";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         USUARIO = '" + usuario + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                cantidad = table.Rows[0][0].ToString();

                conexion.Close();

                if (cantidad == "0")
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public string Calculo_En_Ejecucion(string empresa)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string usuario;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT  TOP (1) USUARIO";
                strsql = strsql + " FROM    TC_APROVISIONAMIENTO_CALCULOS";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         FECHA_FIN IS NULL";
                strsql = strsql + " ORDER BY FECHA_INICIO DESC";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count == 0)
                    usuario = "";
                else
                    usuario = table.Rows[0][0].ToString();

                conexion.Close();

                return usuario;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "error";
            }
        }


        public bool Calcular_Aprovisionamiento(string empresa, string usuario)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string salida;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "exec dbo.IME_Aprov_Calcular_Aprovisionamiento '" + empresa + "','0','" + usuario + "'";
                
                comando = new SqlCommand(strsql, conexion);
                comando.CommandTimeout = 3600;
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                salida = table.Rows[0][0].ToString();

                conexion.Close();

                if (salida == "-1")
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                //MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public void Finalizar_Calculo_Erroneo(string empresa)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " update  TC_APROVISIONAMIENTO_CALCULOS";
                strsql = strsql + " set     FECHA_FIN = GETDATE()";
                strsql = strsql + " where   codemp = '" + empresa + "' and ";
                strsql = strsql + "         FECHA_FIN IS NULL";

                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
