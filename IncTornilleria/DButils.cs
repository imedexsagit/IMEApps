using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using empresaGlobalProj;

namespace IncTornilleria
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

        public int existePadre(string padre)
        {
            int existe = 0;

            
            conexion.Open();
            
            string strsql = "";
            
            strsql += "select CATEGORIA, DENOMINACION from gg.dbo.T_ARTICULOS where CODIGO = '" + padre + "' and CodEMP = " + empresa;
            
            SqlCommand comando = new SqlCommand(strsql, conexion);
            SqlDataReader reader = comando.ExecuteReader();
            
            if (reader.HasRows)
            {
                reader.Read();
                if (reader.GetString(0) == "TO")
                {
                    if( reader.GetString(1).ToUpper().Contains("INCR")) existe = 1;
                    else{
                        existe = 4;
                        conexion.Close();
                        return existe;
                    }
                }
                else existe = 3;
                conexion.Close();
                return existe;
            }
            
            strsql = "";
            
            strsql += "select * from gg.dbo.T_ESTRUC where CONJUN = '" + padre + "' and CodEMP = " + empresa;
            
            comando = new SqlCommand(strsql, conexion);
            reader = comando.ExecuteReader();
            
            if (reader.HasRows)
            {
                existe = 2;
            }
            
            conexion.Close();

            return existe;

        }

        public string estadoTornillo(string codigo)
        {
            string estado = "";

            conexion.Open();

            string strsql = "";

            strsql += "select CATEGORIA, CLAOBS from gg.dbo.T_ARTICULOS where CODIGO = '" + codigo + "' and CodEMP = " + empresa;

            SqlCommand comando = new SqlCommand(strsql, conexion);
            SqlDataReader reader = comando.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                if (reader.GetString(0) == "TO")
                {
                    if (!reader.GetBoolean(1))
                    {
                        estado = "CORRECTO";
                    }
                    else
                    {
                        estado = "OBSOLETO";
                    }
                }
                else
                {
                    estado = "NO TORNILLO";
                }
                
            }
            else
            {
                estado = "INEXISTENTE";
            }

            conexion.Close();

            return estado;

        }

        public void guardarIncrementos(string compon, string conjun, int ctdenl, int correl)
        {
            conexion.Open();

            string strsql = "";

            strsql += "INSERT INTO [dbo].[T_ESTRUC] ([CodEMP], [ALTERN], [CONJUN],[CORREL],[COMPON],[CTDENL],[CLAVE],[OPERAC],[PLAZO],[PERDIDA],[FECHAC],[USUCRE],[SALAUTO],[STWF]) ";
            strsql += "VALUES (" + empresa + ", 0,'" + conjun + "'," + correl + ",'" + compon + "'," + ctdenl + ",0,0,0,0,GETDATE(),'" + Environment.UserName + "',0,0)";

            SqlCommand comando = new SqlCommand(strsql, conexion);
            comando.ExecuteScalar();

            conexion.Close();
        }


    }
}
