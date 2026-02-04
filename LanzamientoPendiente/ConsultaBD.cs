using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace LanzamientoPendiente
{
    class ConsultaBD
    {
        public String obtenerPedido(string pedido)
        {
            string empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString(); 
            string infoPedido = "";

            string sql = "SELECT 'PEDIDO -> ' + CAST(NUMPED as varchar) + '  -  ' + 'CLIENTE: ' + TERCERO  +' ' + RTERCERO FROM  T_ORDTER WHERE (CODEMP = '"+empresa+"') AND (NUMPED = '" + pedido + "') AND (TIPOREG = 'CF')";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        infoPedido = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                    }
                }

            }

            return infoPedido;

        }

        public String obtenerDenominacionCodigo(string codigo)
        {
            string denominacion = "";
            string empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString(); 

            string sql = "SELECT DENOMINACION FROM T_ARTICULOS WHERE (CODIGO = '" + codigo + "') AND (CodEMP = '" + empresa + "')";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        denominacion = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                    }
                }

            }

            return denominacion;

        }


        public String obtenerDenominacionActual(string pedido , string linea)
        {
            string denominacion = "";
            string empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString(); 


            string sql = "SELECT     DENOMINACION  FROM "; //+ DENOMINACION 
            sql += " T_ORDTERL WHERE   (TIPOREG = 'CF') AND (CODEMP = '" + empresa + "') ";
            sql += " AND (NUMPED = '" + pedido + "') AND (LINEA = '" + linea + "') AND (CODEMP = '" + empresa + "')";


            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        denominacion = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                    }
                }

            }

            return denominacion;

        }



        public List<string> obtenerLineasPedido(string pedido)
        {
            string strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());
            List<string> lineas = new List<String>();
            string empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString(); 
            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "SELECT        CAST (LINEA AS VARCHAR) + ' / ' + CODIGO + ' / ' + CAST(CTDUP AS VARCHAR) + ' un. / ' + DENOMINACION  FROM "; //+ DENOMINACION 
                strSql += " T_ORDTERL WHERE        (CODIGO LIKE '%Desarrollo%') AND (TIPOREG = 'CF') AND (CODEMP = '" + empresa + "') ";
                strSql += " AND (NUMPED = '" + pedido + "')";

                sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataReader reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    lineas.Add(reader[0].ToString());
                }

            }

            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return lineas;
        }


        public int actualizarCodigoDenominacion(string linea, string pedido, string codigo, string denominacion)
        {
            int afectadas = 0;
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());
            string empresa = empresaGlobal.empresaID; //ConfigurationManager.AppSettings["EMPRESA"].ToString(); 
            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                /*if (string.IsNullOrEmpty(denominacion))
                {
                    //MRP
                    strSql = "update T_ORDTERL ";
                    strSql += "		set CODIGO = '"+codigo+"' ";
                    strSql += "     WHERE  (CODIGO LIKE '%Desarrollo%') AND (TIPOREG = 'CF') AND (CODEMP = '3') AND (NUMPED = " + pedido + " ) AND (LINEA = " + linea + " )";
                }
                else {
                    //MRP
                    strSql = "update T_ORDTERL ";
                    strSql += "		set CODIGO = '" + codigo + "' , DENOMINACION = '" + denominacion + "' ";
                    strSql += "     WHERE  (CODIGO LIKE '%Desarrollo%') AND (TIPOREG = 'CF') AND (CODEMP = '3') AND (NUMPED = " + pedido + " ) AND (LINEA = " + linea + " )";
                }*/

                strSql = "";
                strSql = strSql + " update    t_ordterl";
                strSql = strSql + " set       codigo = '" + codigo + "',";
                if (!string.IsNullOrEmpty(denominacion))
                    strSql = strSql + "       denominacion = '" + denominacion + "',";
                strSql = strSql + "           fecham = getdate(),";
                strSql = strSql + "           usumod = '" + Environment.UserName + "',";
                strSql = strSql + "           proyecto = NULL,";
                strSql = strSql + "           clase = isnull((SELECT CLASE FROM T_TARIFAS_CLI WHERE CodEmp = '" + empresa + "' AND CODIGO = '" + codigo + "' AND TERCERO = '0' AND CORREL = 0 AND CodDIV = 'EUR'),'0')";
                strSql = strSql + " where     codemp = '" + empresa + "' and tiporeg = 'cf' and numped = " + pedido + " and linea = " + linea;
                //comando.ExecuteScalar();


                sqlCmd = new SqlCommand(strSql, conexion);
                //sqlCmd.ExecuteScalar();
                afectadas += sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la fecha:   " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return afectadas;
        }




    }
}
