using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using empresaGlobalProj;

namespace LanzamientoProyectos.BD
{
    public class ConsultasBD : IDisposable
    {
        private SqlConnection conexionBD;
        //JRM - Cambiar Empresa cambio "3" por empresaGlobal.empresaID; Indicaba un ERROR YA QUE DEBE DE SER CONST, SE HA QUITADO EL CONST de private const string Empresa
        private string Empresa = empresaGlobal.empresaID;

        public ConsultasBD()
        {
            conexionBD = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"]);
        }

        public DataTable CargarFamiliasCodigos()
        {
            try
            {
                conexionBD.Open();
                var strsql = @"SELECT DISTINCT FAMILIA,Convert(varchar(50),FAMILIA) as FAMILIAS
                            FROM            T_ARTICULOS
                            WHERE         (CodEMP = @codemp) and FAMILIA is not null
                            ORDER BY FAMILIA";

                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", Empresa);
                var adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }


        public DataTable CargarLineasProyecto(int idProyecto)
        {
            try
            {
                conexionBD.Open();
                var strsql = @"SELECT [LINEAPRO],[CODIGO], [CANTIDAD]  FROM [dbo].[TC_LANZA_AGRUPA] WHERE CODEMP = @codemp AND CODLANZA = @codLanza order by [LINEAPRO] ";

                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@codLanza", idProyecto);
                var adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public DataTable CargarLineasNecesidad(int idProyecto)
        {
            try
            {
                conexionBD.Open();
                var strsql =
                    //"SELECT [CODIGO],[STSEGU],[STKFIS],[CANTOF],[NECESINH],[NECECONH],[LOTECO],[CANTSUG],[CANTLANZA],[SINRUTA] FROM [dbo].[TC_LANZA_NECE] WHERE CODEMP = @codemp AND CODLANZA = @codLanza";
                    "SELECT [CODIGO],[STSEGU],[STKFIS],[CANTSUG],[CANTLANZA],case dbo.IME_Comprobar_Operaciones_Tiempos_Codigo (CodEMP,CODIGO) when 0 then 'No' when 1 then 'No-Tk' when -1 then 'Si' end as REVISAR FROM [dbo].[TC_LANZA_NECE] WHERE CODEMP = @codemp AND CODLANZA = @codLanza";

                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@codLanza", idProyecto);
                var adapter = new SqlDataAdapter(comando);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                conexionBD.Close();
            }
        }

        public void Dispose()
        {
            conexionBD.Close();
        }

        public DataTable CargarProyectos()
        {
            try
            {
                conexionBD.Open();
                var strsql = @"SELECT DISTINCT CODLANZA, Convert(varchar(50),CODLANZA) as CODLANZAS
                            FROM            TC_LANZA
                            WHERE        (CODEMP = @codemp)
                            ORDER BY CODLANZA DESC";

                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", Empresa);
                var adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public DataTable CargarPedidos()
        {
            try
            {
                conexionBD.Open();
                var strsql = @"SELECT DISTINCT NUMPED, Convert(varchar(50),NUMPED) as NUMPEDS
                            FROM            T_ORDTER
                            WHERE        (CODEMP = @codemp)
                            ORDER BY NUMPED DESC";

                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", Empresa);
                var adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public DataTable GetLineasPedido(int numPedido, string familia, string tipoReg, DateTime horizonte)
        {
            try
            {
                conexionBD.Open();

                var strsql = @"select TORL.[TIPOREG]
                                      ,TORL.[NUMPED]
                                      ,TORL.[LINEA]
                                      ,TORL.[CODIGO]
                                      ,TORL.[CTDUP]
                                      ,TART.[DENOMINACION] 
                            from T_ORDTERL as TORL inner join T_ARTICULOS  as TART on TORL.CODIGO = TART.CODIGO
                            where TORL.CODEMP = @codemp
	                              and TART.CodEMP=@codemp
	                              and TORL.FLAG =0
	                              and TART.CATEGORIA <> 'MP' AND TART.CATEGORIA <> 'TO'";

                //TODO: GMM Cuando se pueda partir una linea de pedido en sus componenetes esta será la parte a modificar
                strsql += " and DATEDIFF(mi, convert (datetime, TORL.FFINAL ,103), convert(datetime, @horizonte, 103))>=0";

                #region filtros en query

                if (!string.IsNullOrEmpty(tipoReg))
                {
                    strsql += " and TORL.TIPOREG = @tipoReg";
                }

                if (!string.IsNullOrEmpty(familia))
                {
                    strsql += " and TART.FAMILIA =@familia";
                }

                if (numPedido > 0)
                {
                    strsql += " and TORL.NUMPED = @numped";
                }

                #endregion

                strsql += " order by TORL.NUMPED, TORL.LINEA asc";
                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@horizonte", horizonte);

                #region parametros filtro

                if (!string.IsNullOrEmpty(tipoReg))
                {
                    comando.Parameters.AddWithValue("@tipoReg", tipoReg);
                }

                if (!string.IsNullOrEmpty(familia))
                {
                    comando.Parameters.AddWithValue("@familia", familia);
                }

                if (numPedido > 0)
                {
                    comando.Parameters.AddWithValue("@numped", numPedido);
                }

                #endregion

                var adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public ggDataSet.TC_LANZADataTable GetProyectos(int numProyecto, int numPedido, string familia, string tipoReg, string codigoProyecto)
        {
            try
            {
                conexionBD.Open();
                //Codigo Rodrigo
                //                strsql = @"SELECT	TORL.CODIGO, CAST(TORL.LINEA AS INT) AS LINEA, CAST(TORL.CTDUP AS INT) AS CTDUP 
                //                            FROM	T_ORDTER AS TOR INNER JOIN T_ORDTERL AS TORL ON TOR.CODEMP = TORL.CODEMP AND TOR.TIPOREG = TORL.TIPOREG AND TOR.NUMPED = TORL.NUMPED
                //                            WHERE	TOR.CODEMP = @codemp AND TOR.TIPOREG = 'CF' AND TOR.NUMPED = @numped";

                var strsql = @"select   TC_LANZA.CODEMP, TC_LANZA.CODLANZA, TC_LANZA.PROYECTO, TC_LANZA.VALIDADO, TC_LANZA.HORIZONTE, TC_LANZA.FAMILIA, TC_LANZA.TIPOREG, TC_LANZA.NUMPED, TC_LANZA.CODIGO, 
                                        TC_LANZA.FECHAC, TC_LANZA.FECHAM, TC_LANZA.USUCRE, TC_LANZA.USUMOD, TC_LANZA.AVISO, TC_LANZA.Visible, TC_LANZA.CALCULAR, TC_LANZA.Parado
                                from    TC_LANZA left join TC_LANZA_PEDLIN ON 
                                            TC_LANZA.CODEMP = TC_LANZA_PEDLIN.CODEMP AND
                                            TC_LANZA.CODLANZA = TC_LANZA_PEDLIN.CODLANZA
                                where  TC_LANZA.CODEMP = @codemp";

                //TODO: GMM Cuando se pueda partir una linea de pedido en sus componenetes esta será la parte a modificar
                //strsql += " and   DATEDIFF(mi,convert(datetime,HORIZONTE, 103),convert(datetime,@horizonte, 103))>=0";

                #region filtros en query

                if (!string.IsNullOrEmpty(tipoReg))
                {
                    strsql += " and  TC_LANZA.TIPOREG = @tipoReg";
                }

                if (!string.IsNullOrEmpty(familia))
                {
                    strsql += " and  TC_LANZA.FAMILIA =@familia";
                }

                if (numPedido > 0)
                {
                    strsql += " and TC_LANZA_PEDLIN.NUMPED = @numped";
                }

                if (!string.IsNullOrEmpty(codigoProyecto))
                {
                    strsql += " and  TC_LANZA.PROYECTO like '%'+@codigo +'%'";
                }

                if (numProyecto > 0)
                {
                    strsql += "   and  TC_LANZA.CODLANZA = @numproyecto";
                }

                #endregion

                strsql += " group by  TC_LANZA.CODEMP, TC_LANZA.CODLANZA, TC_LANZA.PROYECTO, TC_LANZA.VALIDADO, TC_LANZA.HORIZONTE, TC_LANZA.FAMILIA, TC_LANZA.TIPOREG, TC_LANZA.NUMPED, TC_LANZA.CODIGO, TC_LANZA.FECHAC, TC_LANZA.FECHAM, TC_LANZA.USUCRE, TC_LANZA.USUMOD, TC_LANZA.AVISO, TC_LANZA.Visible, TC_LANZA.CALCULAR, TC_LANZA.Parado";
                strsql += " order by  TC_LANZA.CODLANZA asc";
                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", Empresa);
                //comando.Parameters.AddWithValue("@horizonte", horizonte);

                #region parametros filtro

                if (!string.IsNullOrEmpty(tipoReg))
                {
                    comando.Parameters.AddWithValue("@tipoReg", tipoReg);
                }

                if (!string.IsNullOrEmpty(familia))
                {
                    comando.Parameters.AddWithValue("@familia", familia);
                }

                if (numPedido > 0)
                {
                    comando.Parameters.AddWithValue("@numped", numPedido);
                }
                if (!string.IsNullOrEmpty(codigoProyecto))
                {
                    comando.Parameters.AddWithValue("@codigo", codigoProyecto);
                }

                if (numProyecto > 0)
                {
                    comando.Parameters.AddWithValue("@numproyecto", numProyecto);
                }

                #endregion

                var adapter = new SqlDataAdapter(comando);
                ggDataSet.TC_LANZADataTable table = new ggDataSet.TC_LANZADataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }


        //Eliminar las líneas

        public void EliminarLineasLanza(int numProyecto)
        {
            try
            {
                conexionBD.Open();

                var strSql = "DELETE ";
                strSql += "	    FROM TC_LANZA_AGRUPA ";
                strSql += "     WHERE Codemp = @codemp ";
                strSql += "         and CODLANZA  = @numProyecto ";

                var sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numproyecto", numProyecto);
                sqlCmd.ExecuteScalar();

                strSql = "DELETE ";
                strSql += "	    FROM TC_LANZA_PEDLIN ";
                strSql += "     WHERE Codemp = @codemp ";
                strSql += "         and CODLANZA  = @numProyecto ";

                sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numproyecto", numProyecto);
                sqlCmd.ExecuteScalar();

                strSql = "DELETE ";
                strSql += "	    FROM TC_LANZA_NECE ";
                strSql += "     WHERE Codemp = @codemp ";
                strSql += "         and CODLANZA  = @numProyecto ";

                sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numproyecto", numProyecto);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public void EliminarLineasLanza(int numProyecto, int lineaPro)
        {
            try
            {
                conexionBD.Open();


                var strSql = @"DELETE FROM [dbo].[TC_LANZA_PEDLIN]
                           Where [CODEMP]=@codemp and 
                                 [CODLANZA]=@numProyecto and
                                 [LINEAPRO]= @lineaPro";

                var sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@lineaPro", lineaPro);
                sqlCmd.ExecuteScalar();

                strSql = @"DELETE FROM [dbo].[TC_LANZA_AGRUPA]
                           Where [CODEMP]=@codemp and 
                                 [CODLANZA]=@numProyecto and
                                 [LINEAPRO]= @lineaPro";

                sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@lineaPro", lineaPro);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw new Exception("Error al Eliminar LINEAS");
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public void InsertarLineasLanza(int numProyecto, string codigoProyecto, string tipoReg, int numPedido, int lineaPedido, string usuario, int cantidad, string codigo,string codigoPadre)
        {
            try
            {
                conexionBD.Open();


                //Averiguo el valor mas alto de la linea de proyecto
                var strSql = @"select max (LINEAPRO)
                                from TC_LANZA_AGRUPA 
                                where CODEMP = @codemp and
	                                  CODLANZA= @numProyecto and
	                                  PROYECTO = @codigoProyecto";
                var sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@codigoProyecto", codigoProyecto);
                var numLineas = sqlCmd.ExecuteScalar().ToString();
                var lineaPro = string.IsNullOrEmpty(numLineas) ? 1 : Convert.ToInt32(numLineas) + 1;

                //Comprobamos si ya existe, si es asi aumentamos la cantidad.

                strSql = @"SELECT [CANTIDAD] FROM [dbo].[TC_LANZA_AGRUPA] WHERE [CODEMP] =@codemp AND [CODLANZA] = @numProyecto AND [CODIGO]= @codigo";
                sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@codigo", codigo);

                var result = sqlCmd.ExecuteScalar();
                bool existe = result != null;

                if (existe)
                {

                    int cantidadInical = Convert.ToInt32(result);
                    strSql = @"UPDATE [dbo].[TC_LANZA_AGRUPA]
                               SET [CANTIDAD] = @cantidad
                                  ,[FECHAM] = GETDATE()
                                  ,[USUMOD] = @usuario
                             WHERE [CODEMP] = @codemp
                                    and [CODLANZA] = @numProyecto
                                    and [CODIGO]= @codigo
                                    and PROYECTO = @codigoProyecto";
                    sqlCmd = new SqlCommand(strSql, conexionBD);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                    sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                    sqlCmd.Parameters.AddWithValue("@codigo", codigo);
                    sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                    sqlCmd.Parameters.AddWithValue("@cantidad", cantidad + cantidadInical);
                    sqlCmd.Parameters.AddWithValue("@codigoProyecto", codigoProyecto);
                    sqlCmd.ExecuteScalar();

                    //La Linea del proyecto es la que ya existe

                    strSql = @"select LINEAPRO
                                from TC_LANZA_AGRUPA 
                                where CODEMP = @codemp and
	                                  CODLANZA = @numProyecto and
	                                  CODIGO = @codigo and
                                      PROYECTO = @codigoProyecto";
                    sqlCmd = new SqlCommand(strSql, conexionBD);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                    sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                    sqlCmd.Parameters.AddWithValue("@codigo", codigo);
                    sqlCmd.Parameters.AddWithValue("@codigoProyecto", codigoProyecto);
                    lineaPro = Convert.ToInt32(sqlCmd.ExecuteScalar().ToString());


                }
                else
                {

                    strSql = @"INSERT INTO [dbo].[TC_LANZA_AGRUPA]([CODEMP],[CODLANZA],[PROYECTO],[LINEAPRO],[CANTIDAD],[FECHAC],[FECHAM],[USUCRE],[USUMOD],[CODIGO]) 
                            VALUES (@codemp,@numProyecto,@codigoProyecto,@lineaPro,  @cantidad  ,GETDATE(),NULL,@usuario,NULL,@codigo)";

                    sqlCmd = new SqlCommand(strSql, conexionBD);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                    sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                    sqlCmd.Parameters.AddWithValue("@codigoProyecto", codigoProyecto);
                    sqlCmd.Parameters.AddWithValue("@lineaPro", lineaPro);
                    sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                    sqlCmd.Parameters.AddWithValue("@cantidad", cantidad);
                    sqlCmd.Parameters.AddWithValue("@codigo", codigo);
                    sqlCmd.ExecuteScalar();
                }

                //Ahora comprobamos en PEDLIN

                strSql = @"SELECT [CANTIDAD] FROM [dbo].[TC_LANZA_PEDLIN] WHERE [CODEMP] =@codemp AND [CODLANZA] = @numProyecto AND [CODIGO]= @codigo AND [NUMPED] = @numPedido AND [LINEAP] = @lineaPedido";
                sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@codigo", codigo);
                sqlCmd.Parameters.AddWithValue("@numPedido", numPedido);
                sqlCmd.Parameters.AddWithValue("@lineaPedido", lineaPedido);

                result = sqlCmd.ExecuteScalar();
                existe = result != null;

                if (existe)
                {
                    int cantidadInical = Convert.ToInt32(result);
                    strSql = @"UPDATE [dbo].[TC_LANZA_PEDLIN]
                               SET [CANTIDAD] = @cantidad
                                  ,[FECHAM] = GETDATE()
                                  ,[USUMOD] = @usuario
                             WHERE [CODEMP] = @codemp
                                    and [CODLANZA] = @numProyecto
                                    and [CODIGO]= @codigo
                                    and [NUMPED]= @numPedido
                                    and [LINEAP]= @lineaPedido";
                    sqlCmd = new SqlCommand(strSql, conexionBD);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                    sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                    sqlCmd.Parameters.AddWithValue("@codigo", codigo);
                    sqlCmd.Parameters.AddWithValue("@numPedido", numPedido);
                    sqlCmd.Parameters.AddWithValue("@lineaPedido", lineaPedido);
                    sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                    sqlCmd.Parameters.AddWithValue("@cantidad", cantidad + cantidadInical);
                    sqlCmd.ExecuteScalar();
                }
                else
                {
                    strSql = @"INSERT INTO [dbo].[TC_LANZA_PEDLIN]([CODEMP],[CODLANZA],[PROYECTO],[LINEAPRO],[TIPOREG],[NUMPED],[LINEAP],[FECHAC],[FECHAM],[USUCRE],[USUMOD],[CODIGO],[CANTIDAD],[CODIGO_PADRE])
                          VALUES (@codemp,@numProyecto,@codigoProyecto,@lineaPro,@tipoReg,@numPedido,@lineaPedido,GETDATE(),NULL,@usuario,NULL,@codigo,@cantidad, @codigoPadre)";

                    sqlCmd = new SqlCommand(strSql, conexionBD);
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                    sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                    sqlCmd.Parameters.AddWithValue("@codigoProyecto", codigoProyecto);
                    sqlCmd.Parameters.AddWithValue("@lineaPro", lineaPro);
                    sqlCmd.Parameters.AddWithValue("@tipoReg", tipoReg);
                    sqlCmd.Parameters.AddWithValue("@numPedido", numPedido);
                    sqlCmd.Parameters.AddWithValue("@lineaPedido", lineaPedido);
                    sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                    sqlCmd.Parameters.AddWithValue("@codigo", codigo);
                    sqlCmd.Parameters.AddWithValue("@cantidad", cantidad);
                    sqlCmd.Parameters.AddWithValue("@codigoPadre", codigoPadre);


                    sqlCmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                throw new Exception("Error al INSERTAR LINEAS");
            }
            finally
            {
                conexionBD.Close();
            }
        }


        public void CalcularLineasNecesidad(int numProyecto, string usuario)
        {
            try
            {
                conexionBD.Open();

                var strSql = "EXEC [dbo].[IME_Calcular_TC_Lanza_Nece] @codemp, @usuario,@numProyecto";

                var sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("Error ejecutar IME_Calcular_TC_Lanza_Nece", e);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public bool ValidarProyecto(int numProyecto, string usuario)
        {
            try
            {
                conexionBD.Open();
                //Comprobamos si el el proyecto tiene necesidades
                var strSql = @"select count (*) from TC_LANZA_NECE where CODEMP = @CodEmp AND CODLANZA = @numProyecto";
                var sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);

                int numNece = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (numNece <= 0)
                {
                    return false;
                }


                strSql = "EXEC [dbo].[IME_LanzarProyecto] @codemp,@usuario, @numProyecto";
               
                sqlCmd = new SqlCommand(strSql, conexionBD);
                //MMM Establecemos el timeout
                sqlCmd.CommandTimeout = 120;
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                sqlCmd.ExecuteScalar();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Error ejecutar IME_LanzarProyecto");
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public int CrearNuevoProyecto(string codigoProyecto, string tipoReg, string familia, int numPedido, DateTime horizonte, string usuario)
        {
            try
            {
                conexionBD.Open();
                var strsql = @"select isnull(max(codlanza),0) from TC_LANZA where CODEMP = @codemp";
                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);

                int numProyecto = (int) comando.ExecuteScalar() + 1;


                strsql = @"INSERT INTO [dbo].[TC_LANZA]
                                ([CODEMP],[CODLANZA],[PROYECTO],[VALIDADO],[HORIZONTE],[FAMILIA],[TIPOREG],[NUMPED],[FECHAC],[USUCRE],[AVISO],[Visible],[CALCULAR],[Parado])
                         VALUES (@codemp,@codlanza,@codigo,'N',@horizonte,@familia,@tipoReg,@numPed,GETDATE(),@usuario,'S',1,'S',0)";


                comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@codlanza", numProyecto);
                comando.Parameters.AddWithValue("@codigo", codigoProyecto);
                comando.Parameters.AddWithValue("@horizonte", horizonte);
                comando.Parameters.AddWithValue("@usuario", usuario);

                #region parametros filtro

                if (!string.IsNullOrEmpty(familia))
                {
                    comando.Parameters.AddWithValue("@familia", familia);
                }
                else
                {
                    comando.Parameters.AddWithValue("@familia", DBNull.Value);
                }
                if (!string.IsNullOrEmpty(tipoReg))
                {
                    comando.Parameters.AddWithValue("@tipoReg", tipoReg);
                }
                else
                {
                    comando.Parameters.AddWithValue("@tipoReg", DBNull.Value);
                }

                if (numPedido > 0)
                {
                    comando.Parameters.AddWithValue("@numped", numPedido);
                }
                else
                {
                    comando.Parameters.AddWithValue("@numped", DBNull.Value);
                }

                #endregion

                comando.ExecuteNonQuery();
                return numProyecto;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public int CrearNuevoProyectoExpedicion(string codigoProyecto, string tipoReg, string familia, int numPedido, DateTime horizonte, string usuario)
        {
            try
            {
                conexionBD.Open();
                var strsql = @"select min(codlanza) from TC_LANZA where CODEMP = @codemp";
                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);

                int numProyecto = (int)comando.ExecuteScalar() - 1;


                strsql = @"INSERT INTO [dbo].[TC_LANZA]
                                ([CODEMP],[CODLANZA],[PROYECTO],[VALIDADO],[HORIZONTE],[FAMILIA],[TIPOREG],[NUMPED],[FECHAC],[USUCRE],[AVISO],[Visible],[CALCULAR],[Parado])
                         VALUES (@codemp,@codlanza,@codigo,'N',@horizonte,@familia,@tipoReg,@numPed,GETDATE(),@usuario,'S',1,'S',0)";


                comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@codlanza", numProyecto);
                comando.Parameters.AddWithValue("@codigo", codigoProyecto);
                comando.Parameters.AddWithValue("@horizonte", horizonte);
                comando.Parameters.AddWithValue("@usuario", usuario);

                #region parametros filtro

                if (!string.IsNullOrEmpty(familia))
                {
                    comando.Parameters.AddWithValue("@familia", familia);
                }
                else
                {
                    comando.Parameters.AddWithValue("@familia", DBNull.Value);
                }
                if (!string.IsNullOrEmpty(tipoReg))
                {
                    comando.Parameters.AddWithValue("@tipoReg", tipoReg);
                }
                else
                {
                    comando.Parameters.AddWithValue("@tipoReg", DBNull.Value);
                }

                if (numPedido > 0)
                {
                    comando.Parameters.AddWithValue("@numped", numPedido);
                }
                else
                {
                    comando.Parameters.AddWithValue("@numped", DBNull.Value);
                }

                #endregion

                comando.ExecuteNonQuery();

                return numProyecto;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }


        public void GuardarDatosProyecto(int numProyecto, string codigoProyecto, int numPedido, string familia, string tipoReg, DateTime horizonte, string usuario)
        {
            try
            {
                conexionBD.Open();


                var strsql = @"UPDATE [dbo].[TC_LANZA]
                           SET [PROYECTO] = @codigo
                              ,[HORIZONTE] = @horizonte
                              ,[FAMILIA] = @familia
                              ,[TIPOREG] = @tipoReg
                              ,[NUMPED] = @numped
                              ,[FECHAM] = GETDATE()
                              ,[USUMOD] = @usuario
                              
                         WHERE [CODEMP] = @codemp and
                               [CODLANZA] = @codlanza";


                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@codlanza", numProyecto);
                comando.Parameters.AddWithValue("@codigo", codigoProyecto);
                comando.Parameters.AddWithValue("@horizonte", horizonte);
                comando.Parameters.AddWithValue("@usuario", usuario);

                #region parametros filtro

                if (!string.IsNullOrEmpty(familia))
                {
                    comando.Parameters.AddWithValue("@familia", familia);
                }
                else
                {
                    comando.Parameters.AddWithValue("@familia", DBNull.Value);
                }
                if (!string.IsNullOrEmpty(tipoReg))
                {
                    comando.Parameters.AddWithValue("@tipoReg", tipoReg);
                }
                else
                {
                    comando.Parameters.AddWithValue("@tipoReg", DBNull.Value);
                }

                if (numPedido > 0)
                {
                    comando.Parameters.AddWithValue("@numped", numPedido);
                }
                else
                {
                    comando.Parameters.AddWithValue("@numped", DBNull.Value);
                }

                #endregion

                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        /*public void InvalidarProyecto(int numProyecto, string usuario)
        {
            try
            {
                conexionBD.Open();
                //Comprobamos si el el proyecto tiene necesidades

                var strSql = "EXEC [dbo].[IME_BorrarProyectoLanzado] @codemp,@usuario, @numProyecto";

                var sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw new Exception("Error ejecutar IME_BorrarProyectoLanzado");
            }
            finally
            {
                conexionBD.Close();
            }
        }*/



        public bool InvalidarProyecto(int numProyecto, string usuario)
        {
            try
            {
                conexionBD.Open();                

                var strSql = "EXEC [dbo].[IME_BorrarProyectoLanzado] @codemp, @usuario, @numProyecto";

                var sqlCmd = new SqlCommand(strSql, conexionBD);
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@codemp", Empresa);
                sqlCmd.Parameters.AddWithValue("@numProyecto", numProyecto);
                sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                if(sqlCmd.ExecuteScalar().ToString() == "ok")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw new Exception("Error ejecutar IME_BorrarProyectoLanzado");
            }
            finally
            {
                conexionBD.Close();
            }                            
        }



        public void ActualizarCantidadCodigoNecesidades(int numProyecto, string codigoMarca, int cantidad, string usuario)
        {
            try
            {
                conexionBD.Open();


                var strsql = @"UPDATE [dbo].[TC_LANZA_NECE]
                           SET [CANTLANZA] = @cantLanza
                              ,[FECHAM] = GETDATE()
                              ,[USUMOD] = @usuario
                         WHERE [CODEMP] = @codemp and
                               [CODLANZA] = @codlanza and
                               [CODIGO] = @codigo";


                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@codlanza", numProyecto);
                comando.Parameters.AddWithValue("@codigo", codigoMarca);
                comando.Parameters.AddWithValue("@cantLanza", cantidad);
                comando.Parameters.AddWithValue("@usuario", usuario);


                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public bool ExisteCodigoManual(string codigo)
        {
            try
            {
                conexionBD.Open();
                var strsql = @"SELECT count(*) 
                            FROM    T_ARTICULOS
                            WHERE   (CATEGORIA <> 'MP') AND 
                                    (CATEGORIA <> 'TO') AND 
                                    (CodEMP = @codemp)  AND 
                                    (CODIGO=@codigo)";
                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@codigo", codigo);

                if ((int) comando.ExecuteScalar() > 0)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public void GetCantidadLanzada(string codigoPadre, string codigo, int numPedido, int linea, out int lanzada, out string proyecto)
        {
            try
            {
                lanzada = 0;
                proyecto = "";
                conexionBD.Open();

//                var strsql = @"select CODLANZA, CANTIDAD
//                                from TC_LANZA_PEDLIN as t1
//                                where t1.CODEMP=@codemp and t1.CODIGO_PADRE= @codigoPadre and t1.CODIGO = @codigo and t1.NUMPED=@numPedido and t1.LINEAP =@linea";

                var strsql = @"select   dbo.Get_Proyectos_Codigo_Linea_Pedido (CODEMP,CODIGO,CODIGO_PADRE,NUMPED,LINEAP),SUM(CANTIDAD)
                                from	TC_LANZA_PEDLIN
                                where   CODEMP = @codemp and CODIGO_PADRE = @codigoPadre and CODIGO = @codigo and NUMPED = @numPedido and LINEAP = @linea
                                group by CODEMP,CODIGO,CODIGO_PADRE,NUMPED,LINEAP";

                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@codigoPadre", codigoPadre);
                comando.Parameters.AddWithValue("@codigo", codigo);
                comando.Parameters.AddWithValue("@numPedido", numPedido);
                comando.Parameters.AddWithValue("@linea", linea);

               
                SqlDataReader sqlDR = comando.ExecuteReader();

                if (sqlDR.Read())
                {
                    proyecto= sqlDR[0].ToString();
                    if(!string.IsNullOrEmpty(sqlDR[1].ToString()))
                        lanzada = Convert.ToInt32(sqlDR[1]);
                }
                sqlDR.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }



        public bool CodigoDelineadoPorCompleto(string codigo)
        {
            try
            {
                bool delineado = false;
                
                conexionBD.Open();
                var strsql = @"Select dbo.IME_CodigoDelineadoPorCompleto(@codemp,@codigo)";                                
                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);                
                comando.Parameters.AddWithValue("@codigo", codigo);                                
                SqlDataReader sqlDR = comando.ExecuteReader();
                if (sqlDR.Read())
                {
                    if (sqlDR[0].ToString().ToUpper() == "true".ToUpper())
                        delineado = true;
                    else
                        delineado = false;                    
                                        
                }
                sqlDR.Close();
                return delineado;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }



        public DataTable ObtenerPedidosProyecto(string proyecto)
        {
            try
            {
                conexionBD.Open();
                var strsql = @"SELECT   NUMPED,Convert(varchar(50),NUMPED) as NUMPEDS
                                FROM    TC_LANZA_PEDLIN
                                WHERE   CodEMP = @codemp and CODLANZA = @proyecto and NUMPED > 0
                                GROUP BY NUMPED
                                ORDER BY NUMPED";

                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@proyecto", proyecto);
                var adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }



        public bool LineaLanzadaParcialmente(int pedido,int linea)
        {
            try
            {                
                conexionBD.Open();
                var strsql = @"SELECT   count(*)
                                FROM    TC_LANZA_PEDLIN
                                WHERE   CodEMP = @codemp and numped = @pedido and lineap = @linea and codigo_padre <> ''";                                
                var comando = new SqlCommand(strsql, conexionBD);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@codemp", Empresa);
                comando.Parameters.AddWithValue("@pedido", pedido);
                comando.Parameters.AddWithValue("@linea", linea);
                
                if ((int)comando.ExecuteScalar() > 0)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }



        public void crearEtiquetaProyectoExpedicion(int proyecto, string usuario)
        {
            try
            {
                conexionBD.Open();

                String strsql;
                Int32 ordfab;
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexionBD;
                //JRM - Cambiar Empresa cambio '3' por empresaGlobal.empresaID; y he puesto en una línea el código
                strsql = @"SELECT ORDFAB
	                            FROM T_ORDFAB
	                            WHERE (CODEMP = '" + empresaGlobal.empresaID + "') AND (TIPOREG = 'f') AND (Proyecto = @proyecto)";
                comando.CommandText = strsql;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@proyecto", proyecto);
                ordfab = (int)comando.ExecuteScalar();



                //Crear nueva etiqueta
                //JRM - Cambiar Empresa cambio '3' por empresaGlobal.empresaID;
                strsql = @"INSERT INTO TC_LANZA_ETIQUETA (CodEmp, OrdFab, Cantidad, Color, fechac, USUCRE, almacen)
		                            VAlues ('" + empresaGlobal.empresaID + "', @ordfab, 1, 'Blanca', getdate(), @usuario, 1)";
                comando.CommandText = strsql;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@ordfab", ordfab);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        public void borrarEtiquetaProyectoExpedicion(int proyecto, string usuario)
        {
            try
            {
                conexionBD.Open();

                String strsql;
                Int32 ordfab;
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexionBD;
                //JRM - Cambiar Empresa cambio '3' por empresaGlobal.empresaID; y he puesto en una línea el código
                strsql = @"SELECT ORDFAB
	                            FROM T_ORDFAB
	                            WHERE (CODEMP = '" + empresaGlobal.empresaID + "') AND (TIPOREG = 'f') AND (Proyecto = @proyecto)";
                comando.CommandText = strsql;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@proyecto", proyecto);
                ordfab = (int)comando.ExecuteScalar();


                //Borrar posible etiqueta asociada
                //JRM - Cambiar Empresa cambio '3' por empresaGlobal.empresaID; y he puesto en una línea el código
                strsql = @"DELETE
                                FROM TC_LANZA_ETIQUETA 
	                            WHERE (CODEMP = '" + empresaGlobal.empresaID + "') AND (TIPOREG = 'f') AND (ORDFAB = @ordfab)";
                comando.CommandText = strsql;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@ordfab", ordfab);
                comando.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
        }


    }
}