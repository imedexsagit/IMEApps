using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using empresaGlobalProj;

namespace EstadoTaller.BD
{
    public class ConsultasBD : IDisposable
    {
        private SqlConnection conexionBD;
        //JRM - Cambiar Empresa se cambia 3 por empresaGlobal.empresaID
        private string Empresa = empresaGlobal.empresaID; // se borra el const, antes era private const string Empresa ---> si falla es por ésto
        //MMM Ya no hace falta, porque ahora consultamos todas las operaciones de todas las maquinas. 
        //private const string OperacionEstructural = "100001";
        

        public ConsultasBD()
        {

            conexionBD = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"]);        
        }

        public IEnumerable<string> MaquinasPorOperacion(int seccion)
        {
            var listaMaquinas = new List<string>();
            try
            {
                if (conexionBD.State != ConnectionState.Closed)
                {
                    conexionBD.Close();
                }

                    conexionBD.Open();
                SqlCommand comando;
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;

                    comando.CommandText = "";
                    //comando.CommandText = @"select puesto from t_operac where operac = @operacion and CodEMP = @empresa and  puesto <> '100'";
                    comando.CommandText = @"select puesto from t_puestos where secc = @secc and CodEMP = @empresa order by PSATUR";
                    comando.Parameters.AddWithValue("@secc", "0"+ seccion.ToString()); //VICTOR - PASO2 deja de funcionar de un dia para otro añado el 0 delante para que obtenga la sección
                    comando.Parameters.AddWithValue("@empresa", Empresa);
                    //comando.Parameters.AddWithValue("@operacion", OperacionEstructural);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {                            
                            listaMaquinas.Add(reader["puesto"].ToString());
                        }
                    }
                }
                for (int i = 0; i < listaMaquinas.Count; i++)
                {
                    System.Diagnostics.Debug.WriteLine("Máquinas listadas: " + listaMaquinas[i].ToString());
                }
                return listaMaquinas;
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

        public string GetDenominacionMaquina(string maquina)
        {
            string resultado = string.Empty;
            try
            {
                if (conexionBD.State != ConnectionState.Closed)
                    conexionBD.Close();

                conexionBD.Open();
                SqlCommand comando;
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;

                    comando.CommandText = "";                    
                    comando.CommandText = @"select denomi from t_puestos where puesto = @puesto and CodEMP = @empresa";
                    comando.Parameters.AddWithValue("@puesto", maquina);
                    comando.Parameters.AddWithValue("@empresa", Empresa);                    
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {                        
                        while (reader.Read())
                        {                            
                            resultado = reader["denomi"].ToString();
                        }
                    }
                }
                return resultado;
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

        public void TiempoPendientePorMaquina(string sesion, string puesto, Int16 realizable, int material, out double horasTotales, out double kgTotales)
        {
            try
            {
                if (conexionBD.State != ConnectionState.Closed)
                    conexionBD.Close();
                
                conexionBD.Open();
                SqlCommand comando;
                horasTotales = 0;
                kgTotales = 0;
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;

                    comando.CommandText = "";
                    comando.CommandText = @"select ISNULL(sum(HORAS),0) as HorasTotales,ISNULL(sum(CANTIDAD_MP),0) as kgTotales
                                            from TC_ESTADO_TALLER_Cambios_MP
                                            where CODEMP = @empresa and
	                                              PUESTO = @puesto and
                                                  REALIZABLE = " + realizable + @" and
	                                              sesion = @sesion";

                    if (material == 0)      //ang
                        comando.CommandText += " and (isnull(substring(MP_Cambio, 1, 1), '') <> 'C' and isnull(substring(MP_Cambio, 1, 1), '') <> 'P') ";
                    if (material == 1)      //ch-pl
                        comando.CommandText += " and (isnull(substring(MP_Cambio, 1, 1), '') = 'C' or isnull(substring(MP_Cambio, 1, 1), '') = 'P') ";


                    comando.Parameters.AddWithValue("@empresa", Empresa);
                    comando.Parameters.AddWithValue("@puesto", puesto);
                    comando.Parameters.AddWithValue("@sesion", sesion);
                    //comando.Parameters.AddWithValue("@realizable", realizable);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            horasTotales = (double)reader["HorasTotales"];
                            kgTotales = (double)reader["kgTotales"];

                            //if (material == 0)
                            //{
                            //    horasTotales = horasTotales * 0.8;
                            //    kgTotales = kgTotales * 0.8;
                            //}
                            //if (material == 1)
                            //{
                            //    horasTotales = horasTotales * 0.2;
                            //    kgTotales = kgTotales * 0.2;
                            //}
                        }
                    }
                }
                
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

        public DataTable ObtenerInformacionDetallada(string sesion, string puesto, bool realizable, int material)
        {
            try
            {
                var dt = new DataTable();
                if (conexionBD.State != ConnectionState.Closed)
                    conexionBD.Close();

                conexionBD.Open();
                SqlCommand comando;
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;

                    comando.CommandText = "";

                    comando.CommandText = @"SELECT [PROYECTO]
                                                  ,[BONO]
                                                  ,[MARCA]
                                                  ,[LONGITUD]
                                                  ,[ORDFAB]
                                                  ,[CANTIDAD]
                                                  ,[LANZADAS]
                                                  ,[FICHADAS]
                                                  ,[HORAS]
                                                  ,[MP]
                                                  ,[CANTIDAD_MP] as KG
                                                  ,[REALIZABLE]
                                              FROM [dbo].[TC_ESTADO_TALLER_Cambios_MP]
                                              where sesion = @sesion and CODEMP=@empresa and PUESTO = @puesto";
                    if (realizable)
                        comando.CommandText += " and (REALIZABLE = 1 or REALIZABLE =2)";
                    else
                        comando.CommandText += " and (REALIZABLE = 0)";

                    if (material == 0)      //ang
                        comando.CommandText += " and (isnull(substring(MP_Cambio, 1, 1), '') <> 'C' and isnull(substring(MP_Cambio, 1, 1), '') <> 'P') ";
                    if (material == 1)      //ch-pl
                        comando.CommandText += " and (isnull(substring(MP_Cambio, 1, 1), '') = 'C' or isnull(substring(MP_Cambio, 1, 1), '') = 'P') ";


                    //if (material == 0) //ang
                    //if material == 1 //ch-pl
                    //if material == 2 //resto
                    

                    comando.Parameters.AddWithValue("@empresa", Empresa);
                    comando.Parameters.AddWithValue("@puesto", puesto);
                    comando.Parameters.AddWithValue("@sesion", sesion);
                    var da = new SqlDataAdapter(comando);                    
                    da.Fill(dt);
                }                

                return dt;
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

        public DataTable ObtenerInformacionDetalladaExportacion(string sesion, int tipoMaquina,string proveedor , bool realizable, int material)
        {
            try
            {
                var dt = new DataTable();
                if (conexionBD.State != ConnectionState.Closed)
                    conexionBD.Close();

                conexionBD.Open();
                SqlCommand comando;
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;

                    comando.CommandText = "";

                    comando.CommandText = @"SELECT [PUESTO],[PROYECTO]
                                                  ,[BONO]
                                                  ,[MARCA]
                                                  ,[LONGITUD]
                                                  ,[ORDFAB]
                                                  ,[CANTIDAD]
                                                  ,[LANZADAS]
                                                  ,[FICHADAS]
                                                  ,[HORAS]
                                                  ,[MP]
                                                  ,[CANTIDAD_MP] as KG
                                                  ,[REALIZABLE]
                                              FROM [dbo].[TC_ESTADO_TALLER_Cambios_MP]
                                              where sesion = @sesion and CODEMP=@empresa ";
                
                    if(proveedor.Equals("Todas")){
                        comando.CommandText += "and PUESTO in (select T_PUESTOS.puesto from t_puestos  join T_MAQUINAS on T_PUESTOS.PUESTO =T_MAQUINAS.PUESTO and T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP where secc = @tipoMaquina and T_PUESTOS.CodEMP = '3' and (PROVEEDOR = 'Made' or PROVEEDOR = 'IMEDEXSA')) ";
                    }else if (proveedor.Equals("Imedexsa")){
                        comando.CommandText += "and PUESTO in (select T_PUESTOS.puesto from t_puestos  join T_MAQUINAS on T_PUESTOS.PUESTO =T_MAQUINAS.PUESTO and T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP where secc = @tipoMaquina and T_PUESTOS.CodEMP = '3' and PROVEEDOR = 'Imedexsa') ";
                    }
                    else if (proveedor.Equals("Made"))
                    {
                        comando.CommandText += "and PUESTO in (select T_PUESTOS.puesto from t_puestos  join T_MAQUINAS on T_PUESTOS.PUESTO =T_MAQUINAS.PUESTO and T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP where secc = @tipoMaquina and T_PUESTOS.CodEMP = '3' and PROVEEDOR = 'Made') ";
                    }
           
                    if (realizable)
                        comando.CommandText += " and (REALIZABLE = 1 or REALIZABLE =2)";
                        //comando.CommandText += " and (REALIZABLE =2)";
                    else
                        comando.CommandText += " and (REALIZABLE = 0)";

                    if (material == 0)      //ang
                        comando.CommandText += " and (isnull(substring(MP_Cambio, 1, 1), '') <> 'C' and isnull(substring(MP_Cambio, 1, 1), '') <> 'P') ";
                    if (material == 1)      //ch-pl
                        comando.CommandText += " and (isnull(substring(MP_Cambio, 1, 1), '') = 'C' or isnull(substring(MP_Cambio, 1, 1), '') = 'P') ";

                        comando.CommandText += " order by PUESTO,PROYECTO ";


                    //if (material == 0) //ang
                    //if material == 1 //ch-pl
                    //if material == 2 //resto

                    // CARLOS 05-06-2025 // Cambio de entero a string

                    string seccion;
                    switch (tipoMaquina){
                        case 1:
                            seccion = "01";
                            break;
                        case 2:
                            seccion = "02";
                            break;
                        case 3:
                            seccion = "03";
                            break;
                        case 4:
                            seccion = "04";
                            break;
                        case 5:
                            seccion = "05";
                            break;
                        default:
                            seccion = "00";
                            break; 
                    }


                    comando.Parameters.AddWithValue("@empresa", Empresa);
                   // comando.Parameters.AddWithValue("@puesto", puesto);
                    comando.Parameters.AddWithValue("@tipoMaquina", seccion);
                    comando.Parameters.AddWithValue("@sesion", sesion);
                    var da = new SqlDataAdapter(comando);
                    da.Fill(dt);
                }

                return dt;
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


        public string ActualizarEstadoTaller()
        {
            return "";
         /* MMM El prodedimiento de la base de datos se ejecuta en un job.
          * 
          * try
            {
                if (conexionBD.State != ConnectionState.Closed)
                    conexionBD.Close();
                conexionBD.Open();
                SqlCommand comando;
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;

                    comando.CommandText = "";
                    //comando.CommandText = @"exec [dbo].[IME_Cargar_TC_Estado_Taller_Cambios_MP] @empresa, @operacion";
                    comando.CommandText = @"exec [dbo].[IME_Cargar_TC_Estado_Taller_Cambios_MP] @empresa";
                    comando.Parameters.AddWithValue("@empresa", Empresa);                    
                    //comando.Parameters.AddWithValue("@operacion", OperacionEstructural);
                    //comando.CommandTimeout = 110;
                    comando.CommandTimeout = 300;
                    comando.ExecuteNonQuery();

                    comando.CommandText = "";
                    comando.CommandText = @"select Max (SESION)
                                             from TC_ESTADO_TALLER_Cambios_MP
                                             where CARGA_FINALIZADA = 1";

                    string sesion = comando.ExecuteScalar().ToString();
                    return sesion;
                }
            }
            catch (Exception e)
            {
                throw e;
                //return "";
            }
            finally
            {
                conexionBD.Close();
            }*/
            
            
        }

        public string UltimoSesionEstadoTaller()
        {
          
            if (conexionBD.State != ConnectionState.Closed)
            {
                conexionBD.Close();
                
            }

             conexionBD.Open();

            var sesion = "";
        
            SqlCommand comando;
            try
            {
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;
                    comando.CommandText = "";
                    comando.CommandText = @"select MAX (sesion) as sesion
                                            from TC_ESTADO_TALLER_Cambios_MP
                                            where CARGA_FINALIZADA=1";

                    sesion = comando.ExecuteScalar().ToString();
                }
                
            }
                
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }
            return sesion;
        }

        public void Dispose()
        {
            if (conexionBD.State == ConnectionState.Open)
            {
                conexionBD.Close();
            }
        }

        public string TiempoSesionMs(string sesionActual)
        {
            return "";
         /*   if (conexionBD.State != ConnectionState.Closed)
                conexionBD.Close();


            conexionBD.Open();
            var tiempo = "";

            SqlCommand comando;
            try
            {
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;
                    comando.CommandText = "";
                    comando.CommandText = @"select DATEDIFF(millisecond, FECHAC, GETDATE() ) as dif
                                            from TC_ESTADO_TALLER_Cambios_MP
                                            where CARGA_FINALIZADA=1 and sesion = @sesion";
                    comando.Parameters.AddWithValue("@sesion", sesionActual);
                    tiempo = comando.ExecuteScalar().ToString();

                }
                
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexionBD.Close();
            }

            return tiempo;*/
        }

        public bool ExisteCargaPendiente(int tiempo)
        {
            return false;
           /* if (conexionBD.State != ConnectionState.Closed)
                conexionBD.Close();

            conexionBD.Open();

            SqlCommand comando;
            try
            {
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;
                    comando.CommandText = "";
                    comando.CommandText = @"select count(*)
                                            from TC_ESTADO_TALLER_Cambios_MP
                                            where (CARGA_FINALIZADA=2 or CARGA_FINALIZADA=0)  and DATEDIFF(millisecond, FECHAC, GETDATE() ) < @tiempo";
                    comando.Parameters.AddWithValue("@tiempo", tiempo);
                   
                    var res =comando.ExecuteScalar().ToString();
                    if (String.IsNullOrEmpty(res))
                    {
                        return false;
                    }
                    else
                    {
                        if (res == "0")
                            return false;
                        return true;
                    }

                }
            }

            catch (Exception e)
            {
                throw e;    
                //return false;
            
            }
            finally
            {
                conexionBD.Close();
            }*/
        }

        public void BorrarDatosObsoletos(int tiempoBorrado)
        {
         /*   if (conexionBD.State != ConnectionState.Closed)
                conexionBD.Close();

            conexionBD.Open();

            SqlCommand comando;
            try
            {
                using (comando = new SqlCommand())
                {
                    comando.Connection = conexionBD;
                    comando.CommandText = "";
                    comando.CommandText = @"delete
                                            from TC_ESTADO_TALLER_Cambios_MP
                                            where DATEDIFF(millisecond, FECHAC, GETDATE() ) >= @tiempo";
                    comando.Parameters.AddWithValue("@tiempo", tiempoBorrado);

                    comando.ExecuteNonQuery();

                }
            }

            catch (Exception e)
            {
                throw e;                
            }
            finally
            {
                conexionBD.Close();
            }*/
        }

        
    }
}