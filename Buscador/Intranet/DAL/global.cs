using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using empresaGlobalProj;

namespace Intranet.DAL
{
    class global
    {
        public static DataTable ValoresCaracteristicaDeComponente(string CodEMP, string componente)
        {
            //string sql = "SELECT v.CARACT, v.VALOR, c.DENOMI ";
            //sql = sql + "FROM T_ARTCAR v ";
            //sql = sql + "INNER JOIN T_CARACT c ";
            //sql = sql + "	ON v.CodEMP = c.CODEMP AND v.CARACT = c.CARACT ";
            //sql = sql + "WHERE v.CodEMP = @CodEMP ";
            //sql = sql + "	AND v.caract >= '500' ";
            //sql = sql + "	AND v.CODIGO = @componente ";
            //sql = sql + "ORDER BY c.DENOMI ";

            string sql = "SELECT v.CODIGO, v.CATEGO, v.CARACT, v.VALOR ";
            sql = sql + "FROM T_ARTCAR v ";            
            sql = sql + "WHERE v.CodEMP = @CodEMP ";
            sql = sql + "	AND v.caract >= '500' ";
            sql = sql + "	AND v.CODIGO = @componente ";            

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@CodEMP", CodEMP);
                cmd.Parameters.AddWithValue("@componente", componente);

                con.Open();
                SqlDataReader dr = null;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
            }

            return dt;
        }

        public static DataTable Subcomponentes(string CodEMP, string componentePadre)
        {
            //string sql = "SELECT [COMPON] ";
            //sql = sql + "  FROM [dbo].[T_ESTRUC] e ";
            //sql = sql + "  WHERE e.CodEMP = @CodEMP ";
            //sql = sql + "	AND e.CONJUN = @componentePadre ";

            string sql = "SELECT [COMPON] ";
            sql = sql + "FROM [dbo].[T_ESTRUC] e ";
            sql = sql + "INNER JOIN T_ARTICULOS a ";
            sql = sql + "	ON e.CodEMP = a.CodEMP AND e.COMPON = a.CODIGO ";
            sql = sql + "WHERE e.CodEMP = @CodEMP ";
            sql = sql + "	AND (a.FAMILIA <> 'MP') ";
            sql = sql + "	AND e.CONJUN = @componentePadre ";

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@CodEMP", CodEMP);              
                cmd.Parameters.AddWithValue("@componentePadre", componentePadre);              

                con.Open();
                SqlDataReader dr = null;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
            }

            return dt;
        }

        public static void ActualizaValorCaracteristica(string CodEMP, string codigo, string catego, string caract, string valor)
        {
            string sql = "UPDATE [dbo].[T_ARTCAR] ";
            sql = sql + "   SET [VALOR] = @valor ";
            sql = sql + " WHERE [CodEMP] = @CodEMP ";
            sql = sql + "	AND [CODIGO] = @codigo ";
            sql = sql + "	AND [CATEGO] = @catego ";
            sql = sql + "	AND [CARACT] = @caract ";

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@CodEMP", CodEMP);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@catego", catego);
                cmd.Parameters.AddWithValue("@caract", caract);
                cmd.Parameters.AddWithValue("@valor", valor);
                
                con.Open();               
                cmd.ExecuteNonQuery();
            }            
        }

        // // VSS: Esta función se encarga de actualizar valores de las características existentes en T_ARTCAR 
        // tanto para el origen como para el artículo destino
        public static void ActualizaValoresCaracteristicasCompartidasUPDATE(string CodEMP, string articuloOrigen, string articuloDestino, string usuarioModificacion, DateTime horaActual)
        {
            string sql = "UPDATE T_ARTCAR ";
            sql = sql + "SET T_ARTCAR.VALOR = origen.VALOR ";
            sql = sql + " , T_ARTCAR.FECHAM = @horaActual ";
            sql = sql + " , T_ARTCAR.USUMOD = @usuarioModificacion ";
            sql = sql + "FROM [dbo].[T_ARTICULOS] a ";
            sql = sql + "INNER JOIN T_CARCAT c ";
            sql = sql + "ON a.CodEMP = c.CodEMP ";
            sql = sql + "	AND a.CATEGORIA = c.CATEGO ";
            //sql = sql + "LEFT JOIN T_ARTCAR ac ";
            sql = sql + "INNER JOIN T_ARTCAR ac ";
            sql = sql + "ON c.CodEMP = ac.CodEMP ";
            sql = sql + "	AND c.CATEGO = ac.CATEGO ";
            sql = sql + "	AND c.CARACT = ac.CARACT ";
            sql = sql + "	AND ac.CODIGO = a.CODIGO ";
            sql = sql + "INNER JOIN ";
            sql = sql + "( ";
            sql = sql + "	/* Características del producto origen */ ";
            sql = sql + "	SELECT ac.CodEMP, ac.CODIGO, ac.CATEGO, ac.CARACT, ac.VALOR ";
            sql = sql + "	FROM [dbo].[T_ARTICULOS] a ";
            sql = sql + "	INNER JOIN T_ARTCAR ac ";
            sql = sql + "	ON a.CodEMP = ac.CodEMP ";
            sql = sql + "		AND a.CODIGO = ac.CODIGO ";
            sql = sql + "		AND ac.CARACT >= '500' ";
            sql = sql + "	WHERE a.CodEMP = @CodEmp ";
            sql = sql + "		AND a.CODIGO = @articuloOrigen ";
            sql = sql + ") origen ";
            sql = sql + "ON ac.CodEMP = origen.CodEMP AND ac.CARACT = origen.CARACT ";
            sql = sql + "WHERE a.CodEMP = @CodEmp ";
            sql = sql + "	AND a.CODIGO = @articuloDestino ";
            sql = sql + "	AND c.CARACT >= '500' ";
            //sql = sql + "	AND ac.CODEMP IS NOT NULL ";
            sql = sql + "	AND ac.VALOR <> origen.VALOR ";

            sql = sql + "  AND (ac.VALOR IS NULL OR ac.VALOR = '' OR ac.VALOR = '???') ";

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@CodEMP", CodEMP);
                cmd.Parameters.AddWithValue("@articuloOrigen", articuloOrigen);
                cmd.Parameters.AddWithValue("@articuloDestino", articuloDestino);
                cmd.Parameters.AddWithValue("@usuarioModificacion", usuarioModificacion);
                cmd.Parameters.AddWithValue("@horaActual", horaActual);    

                con.Open();
                int n = cmd.ExecuteNonQuery();

                //if (n > 0)
                //{

                //}
            }
        }       

        // VSS: Esta función se encarga de insertar en T_ARTCAR características que correspondan al artículo destino según su categoría (T_CARCAT)
        // que no existan todavía en T_ARTCAR con valores de carácteristicas del artículo origen
        public static void ActualizaValoresCaracteristicasCompartidasINSERT(string CodEMP, string articuloOrigen, string articuloDestino, string usuarioIns, DateTime horaActual)
        {
            string sql = "INSERT INTO [dbo].[T_ARTCAR] ";
            sql = sql + "           ([CodEMP] ";
            sql = sql + "           ,[CODIGO] ";
            sql = sql + "           ,[CATEGO] ";
            sql = sql + "           ,[CARACT] ";
            sql = sql + "           ,[VALOR] ";
            sql = sql + "           ,[FECHAC] ";
            sql = sql + "           ,[USUCRE] ";
            sql = sql + "           ,[DIVISION] ";
            sql = sql + "           ,[STWF]) ";
            sql = sql + "SELECT a.CodEMP, a.CODIGO, a.CATEGORIA, c.CARACT, origen.VALOR, @horaActual, @usuarioIns, c.DIVISION, c.STWF ";
            sql = sql + "FROM [dbo].[T_ARTICULOS] a ";
            sql = sql + "INNER JOIN T_CARCAT c ";
            sql = sql + "ON a.CodEMP = c.CodEMP ";
            sql = sql + "	AND a.CATEGORIA = c.CATEGO ";
            sql = sql + "LEFT JOIN T_ARTCAR ac ";
            sql = sql + "ON c.CodEMP = ac.CodEMP ";
            sql = sql + "	AND c.CATEGO = ac.CATEGO ";
            sql = sql + "	AND c.CARACT = ac.CARACT ";
            sql = sql + "	AND ac.CODIGO = a.CODIGO ";
            sql = sql + "INNER JOIN ";
            sql = sql + "( ";
            sql = sql + "	/* Características del producto origen */ ";
            sql = sql + "	SELECT ac.CodEMP, ac.CODIGO, ac.CATEGO, ac.CARACT, ac.VALOR ";
            sql = sql + "	FROM [dbo].[T_ARTICULOS] a ";
            sql = sql + "	INNER JOIN T_ARTCAR ac ";
            sql = sql + "	ON a.CodEMP = ac.CodEMP ";
            sql = sql + "		AND a.CODIGO = ac.CODIGO ";
            sql = sql + "		AND ac.CARACT >= '500' ";
            sql = sql + "	WHERE a.CodEMP = @CodEmp ";
            sql = sql + "		AND a.CODIGO = @articuloOrigen ";
            sql = sql + ") origen ";
            sql = sql + "ON c.CodEMP = origen.CodEMP AND c.CARACT = origen.CARACT ";
            sql = sql + "WHERE a.CodEMP = @CodEmp ";
            sql = sql + "	AND a.CODIGO = @articuloDestino ";
            sql = sql + "	AND c.CARACT >= '500' ";
            sql = sql + "	AND ac.CODEMP IS NULL ";

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@CodEMP", CodEMP);
                cmd.Parameters.AddWithValue("@articuloOrigen", articuloOrigen);
                cmd.Parameters.AddWithValue("@articuloDestino", articuloDestino);
                cmd.Parameters.AddWithValue("@usuarioIns", usuarioIns);
                cmd.Parameters.AddWithValue("@horaActual", horaActual);

                con.Open();
                int n = cmd.ExecuteNonQuery();

                //if (n > 0)
                //{

                //}
            }
        }        

        public static DataTable Familias(string filtroLista)
        {
            //JRM - Cambiar Empresa se cambia "3" por empresaGlobal.empresaID
            string sql = "SELECT     FAMILIA, REPLACE(DENOMI, 'serie ', '') AS DENOMI ";
            sql = sql + "FROM         T_FAMILIAS ";
            sql = sql + "WHERE     (CodEMP = '" + empresaGlobal.empresaID + "') ";
            if (filtroLista != "")
                sql = sql + "	AND DENOMI LIKE '%' + @filtroLista + '%' ";
            sql = sql + "ORDER BY FAMILIA ";

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                if (filtroLista != "")
                    cmd.Parameters.AddWithValue("@filtroLista", filtroLista);                

                con.Open();
                SqlDataReader dr = null;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
            }

            return dt;
        }

        public static DataTable Categorias(string filtroLista)
        {
            //JRM - Cambiar Empresa se cambia "3" por empresaGlobal.empresaID
            string sql = "SELECT     CATEGO, DENOMI ";
            sql = sql + "FROM         T_CATEGORIAS ";
            sql = sql + "WHERE     (CODEMP = '" + empresaGlobal.empresaID + "') ";
            if (filtroLista != "")
                sql = sql + "	AND DENOMI LIKE '%' + @filtroLista + '%' ";
            sql = sql + "ORDER BY DENOMI ";

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                if (filtroLista != "")
                    cmd.Parameters.AddWithValue("@filtroLista", filtroLista);

                con.Open();
                SqlDataReader dr = null;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
            }

            return dt;
        }

        public static DataTable Comerciales(string filtroLista)
        {
            string sql = "SELECT     REPRES, NOMBRE ";
            sql = sql + "FROM         T_REPRES ";
            sql = sql + "WHERE     (CodEMP = @codemp) ";
            if (filtroLista != "")
                sql = sql + "	AND (REPRES LIKE '%' + @filtroLista + '%' OR NOMBRE LIKE '%' + @filtroLista + '%') ";
            sql = sql + "ORDER BY NOMBRE ";

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                if (filtroLista != "")
                    cmd.Parameters.AddWithValue("@filtroLista", filtroLista);

                con.Open();
                SqlDataReader dr = null;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
            }

            return dt;
        }

        public static DataTable Lugares(string filtroLista)
        {
            //JRM - Cambiar Empresa se cambia "3" por empresaGlobal.empresaID
            string sql = "SELECT     CODEXP, DENOMI ";
            sql = sql + "FROM         T_EXPEDICIONES ";
            sql = sql + "WHERE     (CodEMP = '" + empresaGlobal.empresaID + "') ";
            if (filtroLista != "")
                sql = sql + "	AND DENOMI LIKE '%' + @filtroLista + '%' ";
            sql = sql + "ORDER BY DENOMI ";

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                if (filtroLista != "")
                    cmd.Parameters.AddWithValue("@filtroLista", filtroLista);

                con.Open();
                SqlDataReader dr = null;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
            }

            return dt;
        }

        public static DataTable Clientes(string filtroLista)
        {
            //JRM - Cambiar Empresa se cambia "3" por empresaGlobal.empresaID
            string sql = "SELECT     CODCLI, NOMBRE ";
            sql = sql + "FROM         T_CLIENTES ";
            sql = sql + "WHERE     (CodEMP = '" + empresaGlobal.empresaID + "') ";
            if (filtroLista != "")
                sql = sql + "	AND (CODCLI LIKE '%' + @filtroLista + '%' OR NOMBRE LIKE '%' + @filtroLista + '%') ";
            sql = sql + "ORDER BY NOMBRE ";            

            string conexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                if (filtroLista != "")
                    cmd.Parameters.AddWithValue("@filtroLista", filtroLista);

                con.Open();
                SqlDataReader dr = null;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
            }

            return dt;
        }      
    }
}
