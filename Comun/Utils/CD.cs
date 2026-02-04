using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace Utils
{
    public static class CD
    {

        

        public static String getConexion()
        {
            return ConfigurationManager.AppSettings["ggConnectionString"].ToString();
        }

        public static String getConexionDesarrollo()
        {
            return ConfigurationManager.AppSettings["ggConnectionString"].ToString();
        }


        public static void crearArticulo(String codEmp, String articulo, String denominacion, String familia, String categoria, String path, String user)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                //Se crea el Articulo
                strSql = " Insert Into T_Articulos (CodEMP, CODIGO, DENOMINACION, ABC, CATEGORIA, FAMILIA, CLAOBS, CODEST, UNIDAD, STKFIS, STKCAL, STKREC, NUCODI, CODESP, DocuPath, USUCRE, FECHAC)";
                strSql += "	Values ('" + codEmp + "', '" + articulo + "', '" + denominacion + "', 'A','" + categoria + "', '" + familia + "', 0, '3" + familia + "', 'UN', 0, 0, 0, '" + articulo + "', '" + articulo + "', right('" + path + "', 255), '" + user + "', getdate())";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el articulo: " + articulo + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public static void crearCaracteristicasArticulo(String codEmp, String articulo, String categoria, float peso, int longitud, String area, bool fabricable, String user)
        {
            String fantasma;

            if (fabricable)
                fantasma = "N";
            else
                fantasma = "S";

            bool tienePesoArticulo = tienePeso(codEmp, categoria);
            bool tieneLongitudArticulo = tieneLongitud(codEmp, categoria);
            bool tieneAreaArticulo = tieneArea(codEmp, categoria);
            String nivelMRPArticulo = getNivelMRP(codEmp, categoria);


            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();


                //Peso
                if (tienePesoArticulo)
                {
                    strSql = "INSERT INTO T_ArtCar (CodEmp, Codigo, Catego, Caract, Valor, FechaC, UsuCre ) ";
                    strSql += "	Values ('" + codEmp + "', '" + articulo + "', '" + categoria + "', '02',  '" + peso.ToString().Replace(",", ".") + "', getdate(), '" + user + "')";
                    sqlCmd = new SqlCommand(strSql, conexion);
                    sqlCmd.ExecuteScalar();
                }

                //MRP
                strSql = "INSERT INTO T_ArtCar (CodEmp, Codigo, Catego, Caract, Valor, FechaC, UsuCre ) ";
                strSql += "	Values ('" + codEmp + "', '" + articulo + "', '" + categoria + "', '03', '" + nivelMRPArticulo + "', getdate(), '" + user + "')";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();

                //EnCarpeta
                strSql = "INSERT INTO T_ArtCar (CodEmp, Codigo, Catego, Caract, Valor, FechaC, UsuCre ) ";
                strSql += "	Values ('" + codEmp + "', '" + articulo + "', '" + categoria + "', '08', 'N', getdate(), '" + user + "')";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();

                //Longitud
                if (tieneLongitudArticulo)
                {
                    strSql = "INSERT INTO T_ArtCar (CodEmp, Codigo, Catego, Caract, Valor, FechaC, UsuCre ) ";
                    strSql += "	Values ('" + codEmp + "', '" + articulo + "', '" + categoria + "', '04', '" + longitud.ToString() + "', getdate(), '" + user + "')";
                    sqlCmd = new SqlCommand(strSql, conexion);
                    sqlCmd.ExecuteScalar();
                }

                //Area
                if (tieneAreaArticulo)
                {
                    strSql = "INSERT INTO T_ArtCar (CodEmp, Codigo, Catego, Caract, Valor, FechaC, UsuCre ) ";
                    strSql += "	Values ('" + codEmp + "', '" + articulo + "', '" + categoria + "', '05', '" + area + "', getdate(), '" + user + "')";
                    sqlCmd = new SqlCommand(strSql, conexion);
                    sqlCmd.ExecuteScalar();
                }

                //Fantasma
                strSql = "INSERT INTO T_ArtCar (CodEmp, Codigo, Catego, Caract, Valor, FechaC, UsuCre ) ";
                strSql += "	Values ('" + codEmp + "', '" + articulo + "', '" + categoria + "', '100', '" + fantasma + "', getdate(), '" + user + "')";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear las características del articulo: " + articulo + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }


        #region Comprobaciones de características necesarias por Categoría
        private static bool tienePeso(String CodEmp, String categoria)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "SELECT COUNT(*) ";
                strSql += " FROM T_CarCat ";
                strSql += " WHERE CodEmp = '" + CodEmp + "' ";
                strSql += "     and Catego = '" + categoria + "' ";
                strSql += "     and Caract = '02' ";

                sqlCmd = new SqlCommand(strSql, conexion);

                if (Convert.ToInt32(sqlCmd.ExecuteScalar()) == 0)
                    return false;
                else
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

        }

        private static bool tieneLongitud(String CodEmp, String categoria)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "SELECT COUNT(*) ";
                strSql += " FROM T_CarCat ";
                strSql += " WHERE CodEmp = '" + CodEmp + "' ";
                strSql += "     and Catego = '" + categoria + "' ";
                strSql += "     and Caract = '04' ";

                sqlCmd = new SqlCommand(strSql, conexion);

                if (Convert.ToInt32(sqlCmd.ExecuteScalar()) == 0)
                    return false;
                else
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public static bool tieneArea(String CodEmp, String categoria)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "SELECT COUNT(*) ";
                strSql += " FROM T_CarCat ";
                strSql += " WHERE CodEmp = '" + CodEmp + "' ";
                strSql += "     and Catego = '" + categoria + "' ";
                strSql += "     and Caract = '05' ";

                sqlCmd = new SqlCommand(strSql, conexion);

                if (Convert.ToInt32(sqlCmd.ExecuteScalar()) == 0)
                    return false;
                else
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private static String getNivelMRP(String CodEmp, String categoria)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "SELECT isnull(valordefecto, 10) as Nivel ";
                strSql += " FROM IME_CATEGO_CARAC_DEFECTO ";
                strSql += " WHERE CodEmp = '" + CodEmp + "' ";
                strSql += "     and Catego = '" + categoria + "' ";
                strSql += "     and Caract = '03' ";

                sqlCmd = new SqlCommand(strSql, conexion);

                return sqlCmd.ExecuteScalar().ToString();
            }
            catch (Exception) { }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return "10";
        }
        #endregion






        public static void borrarEstructuraArticulo(String codEmp, String articulo)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "DELETE ";
                strSql += "	    FROM T_ESTRUC ";
                strSql += "     WHERE Codemp = '" + codEmp + "' ";
                strSql += "         and CONJUN = '" + articulo + "' ";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar la estructura del articulo: " + articulo + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public static void borrarRutaArticulo(String codEmp, String articulo)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "DELETE ";
                strSql += "	    FROM T_RUTAS_CABECERA ";
                strSql += "     WHERE Codemp = '" + codEmp + "' ";
                strSql += "         and codigo = '" + articulo + "' ";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();


                strSql = "DELETE ";
                strSql += "	    FROM T_RUTAS ";
                strSql += "     WHERE Codemp = '" + codEmp + "' ";
                strSql += "         and codigo = '" + articulo + "' ";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar la ruta del articulo: " + articulo + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public static void borrarCaracteristicasArticulo(String codEmp, String articulo)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "DELETE ";
                strSql += "	    FROM T_ARTCAR ";
                strSql += "     WHERE Codemp = '" + codEmp + "' ";
                strSql += "         and codigo = '" + articulo + "' ";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar las caracteristicas del articulo: " + articulo + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public static void borrarArticulo(String codEmp, String articulo)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "DELETE ";
                strSql += "	    FROM T_ARTICULOS ";
                strSql += "     WHERE Codemp = '" + codEmp + "' ";
                strSql += "         and codigo = '" + articulo + "' ";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar el articulo: " + articulo + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }





        //Métodos usados desde la intranet
        public static DataTable GetListaAlmacen(String Codemp, String codigo, String idioma, int peso_tornillos)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Cantidad_Padre");
            dataTable.Columns.Add("Codigo_Padre");
            dataTable.Columns.Add("Correlacion");
            dataTable.Columns.Add("ID_PADRE");
            dataTable.Columns.Add("Cantidad_Hijo");
            dataTable.Columns.Add("Codigo_Hijo");
            dataTable.Columns.Add("Codigo_Hijo_Denominacion");
            dataTable.Columns.Add("Categoria_Hijo");
            dataTable.Columns.Add("Familia_Hijo");
            dataTable.Columns.Add("PesoUnitario");
            dataTable.Columns.Add("MP");
            dataTable.Columns.Add("Long");
            dataTable.Columns.Add("Area");
            dataTable.Columns.Add("Fabricable");
            dataTable.Columns.Add("Nivel");
            dataTable.Columns.Add("Categoria_MP");
            dataTable.Columns.Add("Taladrado");


            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();


                SqlCommand sqlCmd = new SqlCommand("IME_TeklaListaAlmacen", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.Add("@CodEmp", SqlDbType.VarChar);
                sqlCmd.Parameters["@CodEmp"].Value = Codemp;

                sqlCmd.Parameters.Add("@Altern", SqlDbType.VarChar);
                sqlCmd.Parameters["@Altern"].Value = "0";

                sqlCmd.Parameters.Add("@codigo", SqlDbType.VarChar);
                sqlCmd.Parameters["@codigo"].Value = codigo;

                //JRegino 11/07/2016
                sqlCmd.Parameters.Add("@idioma", SqlDbType.VarChar);
                sqlCmd.Parameters["@idioma"].Value = idioma;

                //Tomás 30/11/2017
                sqlCmd.Parameters.Add("@peso_tornillos", SqlDbType.Int);
                sqlCmd.Parameters["@peso_tornillos"].Value = peso_tornillos;


                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow fila = dataTable.NewRow();
                    fila[0] = reader[0];        //ID
                    fila[1] = reader[1];
                    fila[2] = reader[2];
                    fila[3] = reader[3];
                    fila[4] = reader[4];
                    fila[5] = reader[5];        //Cantidad_Hijo
                    fila[6] = reader[6];
                    fila[7] = reader[7];
                    fila[8] = reader[8];
                    fila[9] = reader[9];        //Familia_Hijo
                    fila[10] = reader[10];
                    fila[11] = reader[11];
                    fila[12] = reader[12];
                    fila[13] = reader[13];
                    fila[14] = reader[14];
                    fila[15] = reader[15];      //Fabricable
                    fila[16] = reader[16];
                    fila[17] = reader[17]; //22_05_19 MMM Taladrado


                    dataTable.Rows.Add(fila);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return dataTable;
        }

        /// <summary>
        /// GMM
        /// </summary>
        /// <param name="Codemp"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static DataTable GetListaAlmacenAscendentes(String Codemp, String codigo, int peso_tornillos)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Cantidad_Padre");
            dataTable.Columns.Add("Codigo_Padre");
            dataTable.Columns.Add("Correlacion");
            dataTable.Columns.Add("ID_PADRE");
            dataTable.Columns.Add("Cantidad_Hijo");
            dataTable.Columns.Add("Codigo_Hijo");
            dataTable.Columns.Add("Codigo_Hijo_Denominacion");
            dataTable.Columns.Add("Categoria_Hijo");
            dataTable.Columns.Add("Familia_Hijo");
            dataTable.Columns.Add("PesoUnitario");
            dataTable.Columns.Add("MP");
            dataTable.Columns.Add("Long");
            dataTable.Columns.Add("Area");
            dataTable.Columns.Add("Fabricable");
            dataTable.Columns.Add("Nivel");
            dataTable.Columns.Add("Categoria_MP");
            dataTable.Columns.Add("Taladrado");


            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();


                SqlCommand sqlCmd = new SqlCommand("IME_TeklaListaAlmacen_Ascendientes", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.Add("@CodEmp", SqlDbType.VarChar);
                sqlCmd.Parameters["@CodEmp"].Value = Codemp;

                sqlCmd.Parameters.Add("@Altern", SqlDbType.VarChar);
                sqlCmd.Parameters["@Altern"].Value = "0";

                sqlCmd.Parameters.Add("@codigo", SqlDbType.VarChar);
                sqlCmd.Parameters["@codigo"].Value = codigo;

                sqlCmd.Parameters.Add("@peso_tornillos", SqlDbType.VarChar);
                sqlCmd.Parameters["@peso_tornillos"].Value = peso_tornillos;



                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow fila = dataTable.NewRow();
                    fila[0] = reader[0];        //ID
                    fila[1] = reader[1];
                    fila[2] = reader[2];
                    fila[3] = reader[3];
                    fila[4] = reader[4];
                    fila[5] = reader[5];        //Cantidad_Hijo
                    fila[6] = reader[6];
                    fila[7] = reader[7];
                    fila[8] = reader[8];
                    fila[9] = reader[9];        //Familia_Hijo
                    fila[10] = reader[10];
                    fila[11] = reader[11];
                    fila[12] = reader[12];
                    fila[13] = reader[13];
                    fila[14] = reader[14];
                    fila[15] = reader[15];      //Fabricable
                    fila[16] = reader[16];
                    fila[17] = reader[17];//Taladrado


                    dataTable.Rows.Add(fila);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return dataTable;
        }

        public static void añadirAEstructura(String codEmp, String Estructura, String articuloAniadir, String user)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "Insert Into T_ESTRUC (CodEMP, ALTERN, CONJUN, CORREL, COMPON, DESENL, CTDENL, CLAVE, CARACT, FDESDE, FHASTA, OPERAC, PLAZO, PERDIDA, FECHAC, FECHAM, USUCRE, USUMOD, DIVISION) ";
                strSql += "	    Select CodEMP, '0', '" + Estructura + "', isnull((Select Max(Correl)+10 From T_ESTRUC Where CodEMP = '" + codEmp + "' and CONJUN = 'Estructura'), 10) as Correl, ";
                strSql += "				CODIGO, null, 1, 0, null, null, null, 0, 0, 0, GETDATE(), null, '" + user + "', null, null ";
                strSql += "		    From T_ARTICULOS";
                strSql += "		    Where (CodEMP = '" + codEmp + "') ";
                strSql += " 				and (CLAOBS = 0) ";
                strSql += " 				and (CODIGO = '" + articuloAniadir + "')";


                sqlCmd = new SqlCommand(strSql, conexion);

                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al añadir a la estrucutura: " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public static void quitarDeEstructura(String codEmp, String Estructura, String articuloAQuitar)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "Delete From T_ESTRUC ";
                strSql += "     Where (CodEMP = '" + codEmp + "') ";
                strSql += " 				and (ALTERN = '0') ";
                strSql += " 				and (CONJUN = '" + Estructura + "')";
                strSql += " 				and (COMPON = '" + articuloAQuitar + "')";


                sqlCmd = new SqlCommand(strSql, conexion);

                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al quitar de la estrucutura: " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public static void copiarEstructura(String codEmp, String EstructuraOrigen, String EstructuraDestino, String user)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "Insert Into T_ESTRUC (CodEMP, ALTERN, CONJUN, CORREL, COMPON, DESENL, CTDENL, CLAVE, CARACT, FDESDE, FHASTA, OPERAC, PLAZO, PERDIDA, FECHAC, FECHAM, USUCRE, USUMOD, DIVISION) ";
                strSql += "	    Select CodEMP, ALTERN, '" + EstructuraDestino + "', CORREL, COMPON, DESENL, CTDENL, CLAVE, CARACT, FDESDE, FHASTA, OPERAC, PLAZO, PERDIDA, GETDATE(), null, '" + user + "', null, DIVISION ";
                strSql += "         From T_ESTRUC ";
                strSql += "         Where (Codemp = '" + codEmp + "') ";
                strSql += "                 and (Altern = 0) ";
                strSql += "                 and (Conjun = '" + EstructuraOrigen + "') ";

                sqlCmd = new SqlCommand(strSql, conexion);

                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al copiar la estrucutura: " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        /* CREA UNA NUEVA CATEGORÍA */
        public static bool Crear_Categoria(string codemp,string codigo,string denominacion, string abreviatura, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;
                                    
            try
            {
                conexion = new SqlConnection(getConexion());                
                conexion.Open();

                strsql = "";
                strsql = strsql + " insert into t_categorias (codemp,catego,denomi,abrev,escan,clave1,clave2,clave3,clave4,clave5,clave6,carpeso,fechac,usucre)";
                strsql = strsql + " values ('" + codemp + "',";
                strsql = strsql + "         '" + codigo + "',";
                strsql = strsql + "         '" + denominacion + "',";
                strsql = strsql + "         '" + abreviatura + "',";
                strsql = strsql + "         10,'1','F','1','0',NULL,'2','02',";
                strsql = strsql + "         getdate(),";
                strsql = strsql + "         '" + usuario + "')";
                
                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                conexion.Close();

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

        /* COMPROBAR SI EXISTEN ARTÍCULOS DE UNA DETERMINADA CATEGORÍA */
        public static bool Existe_Articulo_Categoria(string codemp, string categoria)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string cantidad;

            try
            {
                conexion = new SqlConnection(getConexion());                
                conexion.Open();

                strsql = "";
                strsql = strsql + " SELECT  COUNT(*)";
                strsql = strsql + " FROM    T_ARTICULOS";
                strsql = strsql + " WHERE   CODEMP = '" + codemp + "' AND";
                strsql = strsql + "         CATEGORIA = '" + categoria +"'";                

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                cantidad = table.Rows[0][0].ToString();
 
                conexion.Close();

                if(cantidad == "0")
                    return false;
                else
                    return true;                
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }

        /* ELIMINAR UNA CATEGORÍA. OJO!!! ANTES COMPROBAR QUE LA CATEGORÍA NO TIENE ARTÍCULOS */
        public static void Eliminar_Categoria(string codemp, string categoria)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;            

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();                

                strsql = "";
                strsql = strsql + " delete  from t_categorias";
                strsql = strsql + " where   codemp = '" + codemp + "' and ";
                strsql = strsql + "         catego = '" + categoria + "'";
                strsql = strsql + Environment.NewLine;
                strsql = strsql + " delete  from t_carcat";
                strsql = strsql + " where   codemp = '" + codemp + "' and ";
                strsql = strsql + "         catego = '" + categoria + "'";
                strsql = strsql + Environment.NewLine;
                strsql = strsql + " delete  from t_artcar";
                strsql = strsql + " where   codemp = '" + codemp + "' and ";
                strsql = strsql + "         catego = '" + categoria + "'";                
                                
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

        /* CREAR UNA JERARQUÍA ENTRE DOS CATEGORÍAS */
        public static void Crear_Jerarquia_Categorias(string codemp, string catego_padre,string catego_hija, string denomi_padre, string denomi_hija, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " insert into tc_categorias_jerarquia (codemp,catego_padre,catego_hija,denomi_padre,denomi_hija,usucre,fechac)";
                strsql = strsql + " values ('" + codemp + "',";
                strsql = strsql + "         '" + catego_padre + "',";
                strsql = strsql + "         '" + catego_hija + "',";
                strsql = strsql + "         '" + denomi_padre + "',";
                strsql = strsql + "         '" + denomi_hija + "',";
                strsql = strsql + "         '" + usuario + "',";
                strsql = strsql + "         getdate())";
                
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

        /* ELIMINAR UNA JERARQUÍA ENTRE DOS CATEGORÍAS */
        public static void Eliminar_Jerarquia_Categorias(string codemp, string catego_padre,string catego_hija)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " delete  from tc_categorias_jerarquia";
                strsql = strsql + " where   codemp = '" + codemp + "'";
                if(catego_padre != "*")
                    strsql = strsql + "     and catego_padre = '" + catego_padre + "'";
                if(catego_hija != "*")
                    strsql = strsql + "     and catego_hija = '" + catego_hija +  "'";                

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

        /* ACTUALIZAR UNA JERARQUÍA ENTRE DOS CATEGORÍAS */
        public static void Actualizar_Jerarquia_Categorias(string codemp, string catego_padre, string catego_hija, string valor, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " update  tc_categorias_jerarquia";
                strsql = strsql + " set     catego_hija = '"+ valor +"',";
                strsql = strsql + "         denomi_hija = '" + valor + "',";
                strsql = strsql + "         usumod = '" + usuario + "',";
                strsql = strsql + "         fecham = getdate()";
                strsql = strsql + " where   codemp = '" + codemp + "' and ";
                strsql = strsql + "         catego_padre = '" + catego_padre + "' and";                
                strsql = strsql + "         catego_hija = '" + catego_hija + "'";

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

        /* ASOCIAR UNA CARACTERÍSTICA A UNA CATEGORÍA */
        public static void Asociar_Caracteristica_Categoria(string codemp, string categoria, string caracteristica,string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " insert into t_carcat (codemp,catego,caract,usucre,fechac)";
                strsql = strsql + " values ('" + codemp + "',";
                strsql = strsql + "         '" + categoria + "',";
                strsql = strsql + "         '" + caracteristica + "',";
                strsql = strsql + "         '" + usuario + "',";
                strsql = strsql + "         getdate())";

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

        /* QUITAR UNA CARACTERÍSTICA A UNA CATEGORÍA. ADEMÁS, ELIMINA TAMBIÉN LOS VALORES DE ESA CARACTERÍSTICA PARA TODOS LOS CÓDIGO DE ESA CATEGORÍA */
        public static void Quitar_Caracteristica_Categoria(string codemp, string categoria, string caracteristica)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " delete  from t_carcat";
                strsql = strsql + " where   codemp = '" + codemp + "' and";
                strsql = strsql + "         catego = '" + categoria + "' and";
                strsql = strsql + "         caract = '" + caracteristica + "'";
                strsql = strsql + Environment.NewLine;
                strsql = strsql + " delete  from t_artcar";
                strsql = strsql + " where   codemp = '" + codemp + "' and";
                strsql = strsql + "         catego = '" + categoria + "' and";
                strsql = strsql + "         caract = '" + caracteristica + "'";                
                
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

        /* ACTUALIZAR EL NOMBRE DE UNA CARACTERÍSTICA */
        public static void Actualizar_Nombre_Caracteristica(string codemp, string caracteristica, string nombre, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " update  t_caract";
                strsql = strsql + " set     usumod = '" + usuario + "',";
                strsql = strsql + "         fecham = getdate(),";
                strsql = strsql + "         denomi = '" + nombre + "'";    
                strsql = strsql + " where   codemp = '" + codemp + "' and ";
                strsql = strsql + "         caract = '" + caracteristica + "'";                

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

        /* OBTIENE EL SIGUIENTE AL ID MÁXIMO DE CARACTERÍSTICA (>=500 INGENIERÍA) PARA CREAR UNA NUEVA */
        public static string Obtener_Max_Caracteristica(string codemp)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string max;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " select	isnull(max(caract) + 1,'500')";
                strsql = strsql + " from	t_caract";
                strsql = strsql + " WHERE   CODEMP = '" + codemp + "' AND";
                strsql = strsql + "         CARACT >= 500";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                max = table.Rows[0][0].ToString();

                conexion.Close();

                return max;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /* ASOCIAR UNA CARACTERÍSTICA A UNA CATEGORÍA */
        public static void Crear_Caracteristica(string codemp, string caracteristica, string denomi, string numerica, string tipo_valores, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " insert into t_caract (codemp, caract, denomi, clave1, clave3, usucre, fechac)";
                strsql = strsql + " values ('" + codemp + "',";
                strsql = strsql + "         '" + caracteristica + "',";
                strsql = strsql + "         '" + denomi + "',";
                strsql = strsql + "         '" + numerica + "',";
                strsql = strsql + "         '" + tipo_valores + "',";
                strsql = strsql + "         '" + usuario + "',";
                strsql = strsql + "         getdate())";

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

        /* ELIMINAR UNA CARACTERÍSTICA. ELIMINA TODAS LAS ASOCIACIONES A LAS CATEGORÍAS Y TODOS SUS VALORES EN LOS CÓDIGOS QUE LOS TUVIERA Y SUS POSIBLES VALORES */
        public static void Eliminar_Caracteristica(string codemp, string caracteristica)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " delete  from t_caract";
                strsql = strsql + " where   codemp = '" + codemp + "' and";                
                strsql = strsql + "         caract = '" + caracteristica + "'";
                strsql = strsql + Environment.NewLine;                
                strsql = strsql + " delete  from t_carcat";
                strsql = strsql + " where   codemp = '" + codemp + "' and";                
                strsql = strsql + "         caract = '" + caracteristica + "'";
                strsql = strsql + Environment.NewLine;
                strsql = strsql + " delete  from t_valcar";
                strsql = strsql + " where   codemp = '" + codemp + "' and";
                strsql = strsql + "         caract = '" + caracteristica + "'";
                strsql = strsql + Environment.NewLine;
                strsql = strsql + " delete  from t_artcar";
                strsql = strsql + " where   codemp = '" + codemp + "' and";                
                strsql = strsql + "         caract = '" + caracteristica + "'";

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

        /* ACTUALIZAR VALOR DE UNA CARACTERÍSTICA */
        public static void Actualizar_Valor_Caracteristica(string codemp, string caracteristica, string valor, string nuevo_valor, string nueva_denominacion, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " update  t_valcar";
                strsql = strsql + " set     usumod = '" + usuario + "',";
                strsql = strsql + "         fecham = getdate(),";
                strsql = strsql + "         valor = '" + nuevo_valor + "',";
                strsql = strsql + "         abrev = '" + nueva_denominacion + "'";
                strsql = strsql + " where   codemp = '" + codemp + "' and ";
                strsql = strsql + "         caract = '" + caracteristica + "' and";
                strsql = strsql + "         valor = '" + valor + "'";

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

        /* CREAR VALOR A UNA CARACTERÍSTICA */
        public static void Crear_Valor_Caracteristica(string codemp, string caracteristica, string valor, string denominacion, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " insert into t_valcar (codemp, caract, valor, abrev, usucre, fechac)";
                strsql = strsql + " values ('" + codemp + "',";
                strsql = strsql + "         '" + caracteristica + "',";
                strsql = strsql + "         '" + valor + "',";
                strsql = strsql + "         '" + denominacion + "',";                
                strsql = strsql + "         '" + usuario + "',";
                strsql = strsql + "         getdate())";

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

        /* ELIMINAR UN VALOR DE UNA CARACTERÍSTICA*/
        public static void Eliminar_Valor_Caracteristica(string codemp, string caracteristica, string valor)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " delete  from t_valcar";
                strsql = strsql + " where   codemp = '" + codemp + "' and";
                strsql = strsql + "         caract = '" + caracteristica + "' and";
                strsql = strsql + "         valor = '" + valor + "'";
                
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

        /* COMPROBAR SI UN CÓDIGO TIENE VALOR PARA UNA CARACTERÍSTICA */
        public static bool Tiene_Valor_Caracteristica_Codigo(string codemp,string codigo,string categoria,string caracteristica)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string numero;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " select	count(*)";
                strsql = strsql + " from	t_artcar";
                strsql = strsql + " WHERE   CODEMP = '" + codemp + "' AND";
                strsql = strsql + "         codigo = '" + codigo + "' AND";
                strsql = strsql + "         catego = '" + categoria + "' AND";
                strsql = strsql + "         caract = '" + caracteristica + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                numero = table.Rows[0][0].ToString();

                conexion.Close();

                if (numero == "0")
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

        /* INSERTAR EL VALOR DE UNA CARACTERÍSTICA PARA UN CÓDIGO */
        public static void Insertar_Valor_Caracteristica_Codigo(string codemp, string codigo, string categoria, string caracteristica,string valor,string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                if (caracteristica == "999")
                    valor = valor.Substring(0, valor.IndexOf(" --> ", 0));
                
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " insert into t_artcar (codemp, codigo, catego, caract, valor, usucre, fechac)";
                strsql = strsql + " values ('" + codemp + "',";
                strsql = strsql + "         '" + codigo + "',";
                strsql = strsql + "         '" + categoria + "',";
                strsql = strsql + "         '" + caracteristica + "',";
                strsql = strsql + "         '" + valor + "',";
                strsql = strsql + "         '" + usuario + "',";
                strsql = strsql + "         getdate())";

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

        /* ACTUALIZAR EL VALOR DE UNA CARACTERÍSTICA PARA UN CÓDIGO */
        public static void Actualizar_Valor_Caracteristica_Codigo(string codemp, string codigo, string categoria, string caracteristica, string valor, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;            

            try
            {
                if (caracteristica == "999")
                    valor = valor.Substring(0, valor.IndexOf(" --> ", 0));
                
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " update  t_artcar";
                strsql = strsql + " set     usumod = '" + usuario + "',";
                strsql = strsql + "         fecham = getdate(),";
                strsql = strsql + "         valor = '" + valor + "'";                
                strsql = strsql + " where   codemp = '" + codemp + "' and ";
                strsql = strsql + "         codigo = '" + codigo + "' and";
                strsql = strsql + "         catego = '" + categoria + "' and";
                strsql = strsql + "         caract = '" + caracteristica + "'";

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

        /* COMPROBAR TIPO DE VALORES DE UNA CARACTERÍSTICA  */
        public static string Tipo_Valores_Caracteristica(string codemp, string caracteristica)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            string tipo;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " select	clave3";
                strsql = strsql + " from	t_caract";
                strsql = strsql + " WHERE   CODEMP = '" + codemp + "' AND";
                strsql = strsql + "         caract = '" + caracteristica + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                tipo = table.Rows[0][0].ToString();

                conexion.Close();

                return tipo;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /* COMPROBAR SI LOS VALORES DE UNA CARACTERÍSTICA DEBEN SER NÚMERICOS  */
        public static bool Es_Numerica_Caracteristica(string codemp, string caracteristica)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;
            bool numerica;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "";
                strsql = strsql + " select	clave1";
                strsql = strsql + " from	t_caract";
                strsql = strsql + " WHERE   CODEMP = '" + codemp + "' AND";
                strsql = strsql + "         caract = '" + caracteristica + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);
                if (table.Rows[0][0].ToString() == "S")
                    numerica = true;
                else
                    numerica = false;

                conexion.Close();

                return numerica;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public static string TieneSuperFamiliaCodigo(string codemp,string codigo,string categoria)
        {
            SqlConnection conexion_bd;
            SqlCommand consulta;
            SqlDataReader datosconsulta;
            string strsql;
            string valor = "-111";

            conexion_bd = new SqlConnection(getConexion());
            conexion_bd.Open();

            strsql = "";
            strsql = strsql + " select   valor";
            strsql = strsql + " from     t_artcar";
            strsql = strsql + " where    codemp = '" + codemp + "' and codigo = '" + codigo + "' and catego = '" + categoria + "' and caract = '999' and valor is not null and valor <> '' and valor <> '???'";
            consulta = new SqlCommand(strsql, conexion_bd);
            datosconsulta = consulta.ExecuteReader();
            if(datosconsulta.Read())
                valor = datosconsulta[0].ToString();

            datosconsulta.Close();
            conexion_bd.Close();

            return valor;
        }



        public static string TieneDiseñadoPorCodigo(string codemp, string codigo, string categoria)
        {
            SqlConnection conexion_bd;
            SqlCommand consulta;
            SqlDataReader datosconsulta;
            string strsql;
            string valor = "zzz";

            conexion_bd = new SqlConnection(getConexion());
            conexion_bd.Open();

            strsql = "";
            strsql = strsql + " select   valor";
            strsql = strsql + " from     t_artcar";
            strsql = strsql + " where    codemp = '" + codemp + "' and codigo = '" + codigo + "' and catego = '" + categoria + "' and caract = '501' and valor is not null and valor <> '' and valor <> '???'";
            consulta = new SqlCommand(strsql, conexion_bd);
            datosconsulta = consulta.ExecuteReader();
            if (datosconsulta.Read())
                valor = datosconsulta[0].ToString();

            datosconsulta.Close();
            conexion_bd.Close();

            return valor;
        }



        public static bool CopiarCaracteristicaHijos(string codemp, string codigo, string caracteristica, string valor, string usuario)
        {
            string strsql;
            SqlCommand comando;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(getConexion());
                conexion.Open();

                strsql = "exec [dbo].[IME_CopiarCaracterísticaHijos] '" + codemp + "','" + codigo + "','" + caracteristica + "','" + valor + "','" + usuario + "'";
                
                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteScalar();

                conexion.Close();

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

      
    }
}
