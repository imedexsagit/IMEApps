using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;


namespace PlanosPedidos
{
    class PlanosPedidosBD
    {
        public string empresa;                
        SqlConnection conexion;



        public PlanosPedidosBD()
        {
            //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID; 
            empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString();                        
            conexion = new SqlConnection(Utils.CD.getConexion());
        }



        public void ObtenerClienteCabecera(string pedido,out string cliente,out string cabecera)
        {
            string strsql;            
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;
            cliente = "";
            cabecera = "";

            try
            {
                if (pedido == "")
                    pedido = "-999";
                
                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT  RTERCERO,TEXTOC";
                strsql = strsql + " FROM    T_ORDTER";
                strsql = strsql + " WHERE   CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         TIPOREG = 'CF' AND";
                strsql = strsql + "         NUMPED = " + pedido;                

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    cliente = table.Rows[0][0].ToString();
                    cabecera = table.Rows[0][1].ToString();
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



        public DataTable ObtenerLineas(string pedido,bool desarrollo)
        {
            string strsql;            
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;            

            try
            {

                if (pedido == "")
                    pedido = "-999";

                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT DISTINCT LINEA AS LÍNEA,T_ORDTERL.CODIGO AS CÓDIGO, T_CATEGORIAS.DENOMI AS CATEGORIA , T_ORDTERL.DENOMINACION AS DENOMINACIÓN";
                strsql = strsql + " FROM    T_ORDTERL";
                strsql = strsql + " LEFT JOIN T_ARTICULOS ON T_ORDTERL.CODIGO = T_ARTICULOS.CODIGO"; //Carlos Sanchez  - modifico la consulta para sacar tambien la categoria
                strsql = strsql + " LEFT JOIN T_CATEGORIAS ON T_ARTICULOS.CATEGORIA = T_CATEGORIAS.CATEGO";//carlos sanchez
                strsql = strsql + " WHERE   T_ORDTERL.CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         TIPOREG = 'CF' AND";
                strsql = strsql + "         NUMPED = " + pedido + " AND";
                if(desarrollo)
                    strsql = strsql + "         (T_ORDTERL.CODIGO IN (SELECT CODIGO FROM T_ARTICULOS WHERE CODEMP = '" + empresa + "') OR T_ORDTERL.CODIGO LIKE 'DESARROLLO-%')";                
                else
                    strsql = strsql + "         T_ORDTERL.CODIGO IN (SELECT CODIGO FROM T_ARTICULOS WHERE CODEMP = '" + empresa + "')";                
                strsql = strsql + " ORDER BY LINEA";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);               

                conexion.Close();

                return table;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return table;
            }
        }

      
        public DataTable ObtenerLineasFiltro(string pedido, bool desarrollo, string criterio, string campo)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;
           

            if (campo == "CODIGO") campo = "T_ORDTERL.CODIGO";
            if (campo == "DENOMINACION") campo = "T_ORDTERL.DENOMINACION";

            try
            {

                if (pedido == "")
                    pedido = "-999";

                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT DISTINCT LINEA AS LÍNEA,T_ORDTERL.CODIGO AS CÓDIGO, T_CATEGORIAS.DENOMI AS CATEGORIA , T_ORDTERL.DENOMINACION AS DENOMINACIÓN";
                strsql = strsql + " FROM    T_ORDTERL";
                strsql = strsql + " LEFT JOIN T_ARTICULOS ON T_ORDTERL.CODIGO = T_ARTICULOS.CODIGO";//Carlos Sanchez  - modifico la consulta para sacar tambien la categoria
                strsql = strsql + " LEFT JOIN T_CATEGORIAS ON T_ARTICULOS.CATEGORIA = T_CATEGORIAS.CATEGO";//Carlos Sanchez
                strsql = strsql + " WHERE   T_ORDTERL.CODEMP = '" + empresa + "' AND";
                strsql = strsql + "         TIPOREG = 'CF' AND";
                strsql = strsql + "         NUMPED = " + pedido + " AND";
                if (desarrollo)
                    strsql = strsql + "         (T_ORDTERL.CODIGO IN (SELECT CODIGO FROM T_ARTICULOS WHERE CODEMP = '" + empresa + "') OR T_ORDTERL.CODIGO LIKE 'DESARROLLO-%')";
                else
                    strsql = strsql + "         T_ORDTERL.CODIGO IN (SELECT CODIGO FROM T_ARTICULOS WHERE CODEMP = '" + empresa + "')";
                strsql = strsql + " AND "+ campo + " like '%"+criterio+"%' ORDER BY LINEA";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

                return table;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return table;
            }
        }

        //Carlos Sánchez  26/01/2022 - ObtenerFamilia - dado un codigo, os devuelve su familia, para comprobar si es Tornilleria o no
        public string ObtenerFamilia(string codigo)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            string familia = string.Empty;

            try
            {
               

                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT FAMILIA FROM T_ARTICULOS ";

                strsql = strsql + " WHERE   CODIGO  = '" + codigo + "' ";
            
                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    familia = table.Rows[0][0].ToString();
                    
                }

                conexion.Close();
                return familia;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return familia;
        }

    }
}
