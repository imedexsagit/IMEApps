using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using empresaGlobalProj;
using System.Data.SqlClient;

namespace AutoLanzamientoProyectos
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

        public System.Data.DataTable getProyectos()
        {
            this.conexion.Open();
            string strsql = @"SELECT TOP(50) TC_LANZA.CODLANZA, TC_LANZA.PROYECTO
                            FROM TC_LANZA
                            WHERE (((TC_LANZA.CODEMP)='3') AND ((TC_LANZA.VALIDADO)='S'))
                            ORDER BY TC_LANZA.CODLANZA DESC;
                            ";

            SqlCommand comando = new SqlCommand(strsql, conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            this.conexion.Close();

            return table;
        }

        public System.Data.DataTable getBufferLanza()
        {
            this.conexion.Open();
            string strsql = @"SELECT 
                                codlanza as PROYECTO, 
	                            (SELECT RAZONS
	                            FROM T_ALMACEN
	                            WHERE (CodEMP = '3') and codalm = al.ALMACEN) AS ALMACEN,
	                            case WHEN  COMPLETADO <> '0' then 'COMPLETADO' ELSE 'NO COMPLETADO' END AS COMPLETADO,
	                            FECHA_COMPELTADO AS [FECHA COMPLETADO],
                                ERROR AS OBSERVACIONES
                            FROM 
	                            BUFFER_AUTOLANZA al
                            ORDER BY
	                            FECHAC DESC;
                            ";

            SqlCommand comando = new SqlCommand(strsql, conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            this.conexion.Close();

            return table;
        }

        public System.Data.DataTable getAlmacenes()
        {
            this.conexion.Open();
            string strsql = @"SELECT        CODALM, RAZONS
                                FROM            T_ALMACEN
                                WHERE        (CodEMP = '3')
                                                            ";

            SqlCommand comando = new SqlCommand(strsql, conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            this.conexion.Close();

            return table;
        }
        public void eliminarLanza(string codlanza)
        {
            this.conexion.Open();
            string strsql = "DELETE FROM BUFFER_AUTOLANZA WHERE CODLANZA = " + codlanza;

            SqlCommand comando = new SqlCommand(strsql, conexion);
            comando.ExecuteNonQuery();
            this.conexion.Close();
        }

    }
}
