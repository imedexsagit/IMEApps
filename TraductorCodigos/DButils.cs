using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace TraductorCodigos
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


        public DataTable traducir(string codigo)
        {
            DataTable codCliente = null;

            string strsql = "";

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();
                strsql = "select COD_CLIENTE, CLIENTE as CODIGO FROM TC_CODIF_CLIENTES WHERE CODEMP = '" + empresa + "' AND COD_INTERNO = '" + codigo + "'";


                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                codCliente = new DataTable();
                adapter.Fill(codCliente);

                if (codCliente.Rows.Count == 0)
                {
                    strsql = "select COD_INTERNO, CLIENTE as CODIGO FROM TC_CODIF_CLIENTES WHERE CODEMP = '" + empresa + "' AND COD_CLIENTE = '" + codigo + "'";
                    comando = new SqlCommand(strsql, conexion);
                    adapter = new SqlDataAdapter(comando);
                    codCliente = new DataTable();
                    adapter.Fill(codCliente);
                }


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return codCliente;
        }

        public void nuevoCodigo(string cod_cli, string cod_empresa, string cliente)
        {

            string strsql = "";

            SqlCommand comando;

            try
            {

                conexion.Open();
                strsql = "INSERT INTO TC_CODIF_CLIENTES (CODEMP, COD_CLIENTE, COD_INTERNO, CLIENTE) VALUES ('" + empresa + "', '" + cod_cli + "', '" + cod_empresa + "', '" + cliente + "')";


                comando = new SqlCommand(strsql, conexion);

                comando.ExecuteNonQuery();

                conexion.Close();
                MessageBox.Show("Relación añadida a la base de datos.", "FINALIZADO",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable obtenerClientes()
        {
            string strsql = "";
            DataTable clientes = null;

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();
                strsql = "select distinct CLIENTE FROM TC_CODIF_CLIENTES WHERE CODEMP = '" + empresa + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                clientes = new DataTable();
                adapter.Fill(clientes);

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return clientes;
        }

        public DataTable obtenerClientesCodigo(string codigo)
        {
            string strsql = "";
            DataTable clientes = null;

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();
                strsql = "select distinct CLIENTE FROM TC_CODIF_CLIENTES WHERE CODEMP = '" + empresa + "' and COD_INTERNO = '" + codigo + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                clientes = new DataTable();
                adapter.Fill(clientes);

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return clientes;
        }

        public string traducirConCliente(string codigo, string cliente)
        {
            DataTable codCliente = null;

            string strsql = "";

            SqlCommand comando;
            SqlDataAdapter adapter;

            string codCli = "-";

            try
            {

                conexion.Open();
                strsql = "select COD_CLIENTE as CODIGO FROM TC_CODIF_CLIENTES WHERE CODEMP = '" + empresa + "' AND COD_INTERNO = '" + codigo + "' and CLIENTE like '" + cliente + "'";


                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                codCliente = new DataTable();
                adapter.Fill(codCliente);
                conexion.Close();

                codCli = codCliente.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return codCli;
        }

    }

        
}
