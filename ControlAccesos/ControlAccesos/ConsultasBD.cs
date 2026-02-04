using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ControlAccesos
{
    internal class ConsultasBD
    {
        private SqlConnection conexionBD;

        public ConsultasBD()
        {
            conexionBD = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"]);
        }


        public bool TienePermisos(string empresa, string aplicacion, string usuario)
        {
            try
            {
                conexionBD.Open();

                var strsql = @"SELECT  COUNT(*) 
                            FROM    TC_IMEAPPS_PERMISOS 
                            WHERE   CODEMP = @codemp AND 
                                    (USUARIO = @usuario ";

                if (!usuario.Contains("tablet") && !usuario.Contains("operario")){
                    strsql += "OR USUARIO= '*'";
                }

                strsql += ") AND APLICACION = @aplicacion";

                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", empresa);
                comando.Parameters.AddWithValue("@aplicacion", aplicacion);
                comando.Parameters.AddWithValue("@usuario", usuario);

                var cantidad = Convert.ToInt32(comando.ExecuteScalar());
                conexionBD.Close();

                if (cantidad == 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexionBD.State == ConnectionState.Open)
                    conexionBD.Close();
            }
        }
    }
}