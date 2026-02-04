using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;



namespace GestionGalvanizado
{
    class ConsultasBD
    {

        //Métodos
        public DataTable ObtenerInformacionCuelgueDos(string cuelgue)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;

         

            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "select * from TC_GALV_Cuelgue where idCuelgue = '17610'";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //Métodos
        public DataTable ObtenerTiposPerchas(string year, string mes)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "select isNull(count(*),0), percha.Tipo from TC_GALV_Cuelgue cg join TC_GALV_Percha percha on cg.IdPercha = percha.IdPercha where MONTH(case when CAST(cg.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end) = '" + mes + "' and YEAR(case when CAST(cg.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end) = '" + year + "' and cg.estado = '4' and cg.tipo = 'Produccion' and cg.PesoInicial > 0 group by percha.tipo";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }




        public DataTable ObtenerInformacionCuelgue(String fecha)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("IdCuelgue");
            dataTable.Columns.Add("FechaC");
            dataTable.Columns.Add("IdPercha");
            dataTable.Columns.Add("Posicion");
            dataTable.Columns.Add("PesoPercha");
            dataTable.Columns.Add("PesadaUno");
            dataTable.Columns.Add("KgNegro");
            dataTable.Columns.Add("FechaSalida");
            dataTable.Columns.Add("PesadaDos");
            dataTable.Columns.Add("KgBlanco");
            dataTable.Columns.Add("ConsumoZn");
            dataTable.Columns.Add("PorcZn");
            dataTable.Columns.Add("FechaHorno");
            dataTable.Columns.Add("Micras_min");
            dataTable.Columns.Add("Micras_med");
            dataTable.Columns.Add("Micras_max");
            dataTable.Columns.Add("MP");
            dataTable.Columns.Add("Espesor");
            dataTable.Columns.Add("Inmersion");



            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();


                SqlCommand sqlCmd = new SqlCommand("Info_Galv_Cuelgue_detalle", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                sqlCmd.Parameters.Add("@CodEmp", SqlDbType.VarChar);
                sqlCmd.Parameters["@CodEmp"].Value = '3';

                sqlCmd.Parameters.Add("@fecha", SqlDbType.VarChar);
                sqlCmd.Parameters["@fecha"].Value = fecha;

                sqlCmd.Parameters.Add("@IdCuelgue", SqlDbType.Int);
                sqlCmd.Parameters["@IdCuelgue"].Value = 0;


                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow fila = dataTable.NewRow();
                    fila[0] = reader[0];
                    fila[1] = reader[1];
                    fila[2] = reader[2];
                    fila[3] = reader[3];
                    fila[4] = reader[4];
                    fila[5] = reader[5];
                    fila[6] = reader[6];
                    fila[7] = reader[7];
                    fila[8] = reader[8];
                    fila[9] = reader[9];
                    fila[10] = reader[10];
                    fila[11] = reader[11];
                    fila[12] = reader[12];
                    fila[13] = reader[13];
                    fila[14] = reader[14];
                    fila[15] = reader[15];
                    fila[16] = reader[16];
                    fila[17] = reader[17];
                    fila[18] = reader[18];


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









        public DataTable ObtenerInformacionCuelgueIndividual(int cuelgue)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("IdCuelgue");
            dataTable.Columns.Add("FechaC");
            dataTable.Columns.Add("IdPercha");
            dataTable.Columns.Add("Posicion");
            dataTable.Columns.Add("PesoPercha");
            dataTable.Columns.Add("PesadaUno");
            dataTable.Columns.Add("KgNegro");
            dataTable.Columns.Add("FechaSalida");
            dataTable.Columns.Add("PesadaDos");
            dataTable.Columns.Add("KgBlanco");
            dataTable.Columns.Add("ConsumoZn");
            dataTable.Columns.Add("PorcZn");
            dataTable.Columns.Add("FechaHorno");
            dataTable.Columns.Add("Micras_min");
            dataTable.Columns.Add("Micras_med");
            dataTable.Columns.Add("Micras_max");
            dataTable.Columns.Add("MP");
            dataTable.Columns.Add("Espesor");
            dataTable.Columns.Add("Inmersion");



            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();


                SqlCommand sqlCmd = new SqlCommand("Info_Galv_Cuelgue_Individual", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;



                sqlCmd.Parameters.Add("@cuelgue", SqlDbType.Int);
                sqlCmd.Parameters["@cuelgue"].Value = cuelgue;


                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow fila = dataTable.NewRow();
                    fila[0] = reader[0];
                    fila[1] = reader[1];
                    fila[2] = reader[2];
                    fila[3] = reader[3];
                    fila[4] = reader[4];
                    fila[5] = reader[5];
                    fila[6] = reader[6];
                    fila[7] = reader[7];
                    fila[8] = reader[8];
                    fila[9] = reader[9];
                    fila[10] = reader[10];
                    fila[11] = reader[11];
                    fila[12] = reader[12];
                    fila[13] = reader[13];
                    fila[14] = reader[14];
                    fila[15] = reader[15];
                    fila[16] = reader[16];
                    fila[17] = reader[17];
                    fila[18] = reader[18];


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





        public DataTable ObtenerInformacionAModificar(String cuelgue)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "select PesoInicial, PesoNegro, nCubasDecapado, micras_min, micras_med, micras_max, PesoBlanco, utiles, Tipo,DATEDIFF(second,FechaIniGalvanizado,FechaFinGalvanizado) as Inmersion  from TC_GALV_Cuelgue where idCuelgue = '" + cuelgue + "'";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public void modificarDatosCuelguePrueba(string idCuelgue, int pesoInicial, int pesoNegro, int numCuba, int micrasMin, int micrasMed, int micrasMax, int pesoBlanco, int utiles)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;

            strsql = "update [dbo].[TC_GALV_Cuelgue] set " +
                         "PesoInicial ='" + pesoInicial + "' " +
                         ",PesoNegro ='" + pesoNegro + "' " +
                         ",nCubasDecapado ='" + numCuba + "' " +
                         ",micras_min ='" + micrasMin + "' " +
                         ",micras_med ='" + micrasMed + "' " +
                         ",micras_max ='" + micrasMax + "' " +
                         ",PesoBlanco ='" + pesoBlanco + "' " +
                         ",utiles ='" + utiles + "' " +
                         " where IdCuelgue = '" + idCuelgue + "' ";


            using ( conexion = new SqlConnection(Utils.CD.getConexion()))
            {
                comando = new SqlCommand(strsql, conexion);
                conexion.Open();

                int filasEfectadas = (int)comando.ExecuteNonQuery();
                if (filasEfectadas < 1)
                {
                    Console.WriteLine("No se ha modificado la inspección");
                    // todoCorrecto = false;
                }
                Console.WriteLine("inspección modificada");

            }

            //return todoCorrecto;

        }


        public void modificarDatosCuelgue(string idCuelgue, string pesoInicial, string pesoNegro, string numCuba, string micrasMin, string micrasMed, string micrasMax, string pesoBlanco, string utiles, string tipo, string segundosInmersion)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;

            strsql = "update [dbo].[TC_GALV_Cuelgue] set " +
                         "PesoInicial ='" + pesoInicial + "' " +
                         ",PesoNegro ='" + pesoNegro + "' " +
                         ",nCubasDecapado ='" + numCuba + "' " +
                         ",micras_min ='" + micrasMin + "' " +
                         ",micras_med ='" + micrasMed + "' " +
                         ",micras_max ='" + micrasMax + "' " +
                         ",PesoBlanco ='" + pesoBlanco + "' " +
                         ",utiles ='" + utiles + "' " +
                         ",tipo ='" + tipo + "' " +
                         ",FechaFinGalvanizado = dateadd(second,"+segundosInmersion+",FechaIniGalvanizado)"+ 
                         " where IdCuelgue = '" + idCuelgue + "' ";


            using (conexion = new SqlConnection(Utils.CD.getConexion()))
            {
                comando = new SqlCommand(strsql, conexion);
                conexion.Open();

                int filasEfectadas = (int)comando.ExecuteNonQuery();
                if (filasEfectadas < 1)
                {
                    Console.WriteLine("No se ha modificado la inspección");
                    // todoCorrecto = false;
                }
                Console.WriteLine("inspección modificada");

            }

            //return todoCorrecto;

        }



        public DataTable ObtenerInformacionDiaria(string fechaMin, string fechaMax)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();
                /* VERSION 1
                strsql = @"Select FechaFiltro as Fecha,SumNegro,SumBlanco, Diferencia as 'Kg Zn arrastre' ,((Diferencia/SumNegro) * 100)  as 'Porc. Arrastre', (PromedioMin/TotalFilas) as PromMin, (PromedioMed/TotalFilas) as PromMed, (PromedioMax/TotalFilas) as PromMax,TotalFilas as 'Total Perchas Galvanizadas'  from (
select FechaFiltro, cast (Sum(KGNegro) as float) SumNegro,Sum(KGBlanco) SumBlanco ,(Sum(KGBlanco) - Sum(KGNegro)) as Diferencia,Sum(micras_min) as PromedioMin, Sum(micras_med) as PromedioMed,Sum(micras_max) as PromedioMax , (select count (*) from (select case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltroConteo from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0 ) as CalculoPerchas where FechaFiltroConteo = FechaFiltro) as TotalFilas from ( 
						select * from(
						select ((cast (PesoNegro as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGNegro',((cast (PesoBlanco as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGBlanco'
                        ,micras_min,micras_med,micras_max ,FechaFinGalvanizado, case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltro from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0) as InFo where CAST(FechaFiltro as DATE) >= '" + fechaMin + "' and  CAST(FechaFiltro as DATE) <= '" + fechaMax + "') as InfoDia group by FechaFiltro) as InfoDos";
                */
                /* VERSION 2 - CANTIDAD DE PIEZAS
                strsql = @"Select FechaFiltro as Fecha,SumNegro,SumBlanco, Diferencia as 'Kg Zn arrastre' ,((Diferencia/SumNegro) * 100)  as 'Porc. Arrastre', (PromedioMin/TotalFilas) as PromMin, (PromedioMed/TotalFilas) as PromMed, (PromedioMax/TotalFilas) as PromMax,TotalFilas as 'Total Perchas Galvanizadas',TotalCtd as 'Cantidad Piezas'  from (
                        select FechaFiltro,Sum(Cantidad)'TotalCtd' ,cast (Sum(KGNegro) as float) SumNegro,Sum(KGBlanco) SumBlanco ,(Sum(KGBlanco) - Sum(KGNegro)) as Diferencia,Sum(micras_min) as PromedioMin, Sum(micras_med) as PromedioMed,Sum(micras_max) as PromedioMax , (select count (*) from (select case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltroConteo from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0 ) as CalculoPerchas where FechaFiltroConteo = FechaFiltro) as TotalFilas from ( 
						select * from(
						select (select SUM(cantidadBiengalvaniada) from TC_GALV_Cuelgue_Etiqueta where idCuelgue = TC_GALV_Cuelgue.IdCuelgue and CodEmp = '3' group by IdCuelgue) as Cantidad,((cast (PesoNegro as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGNegro',((cast (PesoBlanco as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGBlanco'
                        ,micras_min,micras_med,micras_max ,FechaFinGalvanizado, case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltro from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0) as InFo where CAST(FechaFiltro as DATE) >= '" + fechaMin + "' and  CAST(FechaFiltro as DATE) <= '" + fechaMax + "') as InfoDia group by FechaFiltro) as InfoDos";
                */

                strsql = @"select 

	                        case when Fecha is null then format (FechaLlegada, 'dd-MM-yyyy') else format (Fecha, 'dd-MM-yyyy') end as Fecha,
	                        principal.SumNegro,
	                        Kg_Enviados as [KG Fichados],
	                        SumBlanco,
	                        [Kg Zn arrastre],
	                        [Por. Arrastre],
	                        PromMin,
	                        PromMed,
	                        PromMax,
	                        [Total Perchas Galvanizadas],
	                        [Cantidad Piezas],
	                        [Sup. Galv.]
	
	                        from 




                        (select 
	                        FechaFiltro as Fecha,
	                        SumNegro,
	                        SumBlanco, 
	                        Diferencia as 'Kg Zn arrastre' ,
	                        ((Diferencia/SumNegro) * 100)  as 'Por. Arrastre', 
	                        (PromedioMin/TotalFilas) as PromMin,
	                        (PromedioMed/TotalFilas) as PromMed, 
	                        (PromedioMax/TotalFilas) as PromMax,
	                        TotalFilas as 'Total Perchas Galvanizadas',
	                        Isnull(TotalCtd,0) as 'Cantidad Piezas',
	                        Isnull(replace(SupGalv,'.',',') + ' m²',0) as 'Sup. Galv.' 
	
                        from (
	                        select 
		                        FechaFiltro,
		                        Sum(superficie)'SupGalv',
		                        Sum(Cantidad)'TotalCtd' ,
		                        cast (Sum(KGNegro) as float) SumNegro,
		                        Sum(KGBlanco) SumBlanco ,
		                        (Sum(KGBlanco) - Sum(KGNegro)) as Diferencia,
		                        Sum(micras_min) as PromedioMin,
		                        Sum(micras_med) as PromedioMed
		                        ,Sum(micras_max) as PromedioMax , 
		                        (select 
			                        count (*) 
		                        from 
			                        (select 
				                        case when 
					                        CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' 
				                        then 
					                        dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) 
					
				                        else 
					                        CAST(FechaFinGalvanizado as DATE) 
				                        end as FechaFiltroConteo 
				
			                        from 
				                        TC_GALV_Cuelgue  
			                        where 
				                        tipo = 'Produccion' 
				                        and estado = 4 
				                        and PesoInicial >0 ) 
			                        as CalculoPerchas 

		                        where 
			                        FechaFiltroConteo = FechaFiltro) 
		
		                        as TotalFilas 
						 
	                        from ( 
		                        select 
			                        * 
		                        from(
			                        select 
				                        (select 
					                        SUM(CONVERT(decimal(12,2),replace(dbo.IME_GetSuperficieTennet_NEW(CodEmp,Marca,CantidadBIENGalvaniada,'18'),',','.'))) as superficie 
					
				                        from 
					                        TC_GALV_Cuelgue_Etiqueta 
				                        where 
					                        idCuelgue = TC_GALV_Cuelgue.IdCuelgue
					                        and CodEmp = '3' 
				                        group by IdCuelgue) 
				                        as superficie,
		
				                        (select 
					                        SUM(cantidadBiengalvaniada) 
				
				                        from 
					                        TC_GALV_Cuelgue_Etiqueta
				
				                        where 
					                        idCuelgue = TC_GALV_Cuelgue.IdCuelgue 
					                        and CodEmp = '3'
				                        group by IdCuelgue)
				                        as Cantidad,

				                        ((cast (PesoNegro as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGNegro',
				                        ((cast (PesoBlanco as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGBlanco',
				                        micras_min,
				                        micras_med,
				                        micras_max,
				                        FechaFinGalvanizado, 
						 
				                        case when 
					                        CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' 
				                        then 
					                        dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) 
				                        else 
					                        CAST(FechaFinGalvanizado as DATE) 
				                        end as FechaFiltro 
						 
			                        from 
				                        TC_GALV_Cuelgue 

			                        where 
				                        tipo = 'Produccion'
				                        and estado = 4 
				                        and PesoInicial >0)
			                        as InFo 
			
		                        where 
			                        CAST(FechaFiltro as DATE) >= '" + fechaMin + @"'
			                        and  CAST(FechaFiltro as DATE) <= '" + fechaMax + @"') 
		                        as InfoDia 
	
	                        group by FechaFiltro) 
	
	                        as InfoDos) principal

	                        full join

	                        (SELECT IME.dbo.IME_Comprobacion_Recepciones_Facturas.Recepcion, IME.dbo.IME_Comprobacion_Recepciones_Facturas.camion, IME.dbo.IME_Comprobacion_Recepciones_Facturas.Planta, IME.dbo.IME_Comprobacion_Recepciones_Facturas.ALBARAN, IME.dbo.IME_Comprobacion_Recepciones_Facturas.suAlbaran, IME.dbo.IME_Comprobacion_Recepciones_Facturas.FACTURA, IME.dbo.IME_Comprobacion_Recepciones_Facturas.FechaLlegada, IME.dbo.IME_Comprobacion_Recepciones_Facturas.Kg_Enviados, IME.dbo.IME_Comprobacion_Recepciones_Facturas.KgPlanta, IME.dbo.IME_Comprobacion_Recepciones_Facturas.Importe, IME.dbo.IME_Comprobacion_Recepciones_Facturas.TQT, IME.dbo.IME_Comprobacion_Recepciones_Facturas.KgNegro_Bascula_IME, IME.dbo.IME_Comprobacion_Recepciones_Facturas.KgBlanco_Bascula_IME
                        FROM IME.dbo.IME_Comprobacion_Recepciones_Facturas
                        WHERE (((IME.dbo.IME_Comprobacion_Recepciones_Facturas.FechaLlegada) Between '" + fechaMin + "' And '" + fechaMax + @"') AND ((IME.dbo.IME_Comprobacion_Recepciones_Facturas.origen)<>2)) and ALBARAN = -2
                        /*ORDER BY IME_Comprobacion_Recepciones_Facturas.camion*/) xd on xd.FechaLlegada = principal.Fecha";
                


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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public DataTable ObtenerInformacionProductividad(string fechaMin, string fechaMax)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"SELECT  imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia,
		        imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario as CodOperario,
        REPLACE((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario + '-' + replace(imedexsa_intranet.dbo.Aux_TiemposNetosBonos.NombreOperario, ',', '')),'-MADE-', ' ') as Operario,
		round((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto * imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono)/60,2) as Horas			
	FROM imedexsa_intranet.dbo.Aux_TiemposNetosBonos Inner Join 
					(SELECT T_PUESTOS.PUESTO, T_PUESTOS.SECC,
							Case 
								When T_MAQUINAS.TIPOP = '010' Then
									1
								Else
									0
							End as consumeMP
						FROM T_PUESTOS LEFT OUTER JOIN T_MAQUINAS
								ON T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP AND T_PUESTOS.PUESTO = T_MAQUINAS.MAQUI
						WHERE (T_PUESTOS.CodEMP = '3')
								and (T_PUESTOS.SECC <> '6')
					) as t_auxiliar 
			on imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Maquina = t_auxiliar.PUESTO Left Outer Join imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF 
			ON imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Mes = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Mes AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.idTrabajo AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Bono = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Bono 
	WHERE (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia >= '" + fechaMin + @"')
			and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia <= '" + fechaMax + @"')
			and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.codemp = '3')
			and Maquina = '850' order by Dia";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /*
        public DataTable ObtenerInformacionProductividadConsultarDos(string fechaMin, string fechaMax)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select t1.Dia,t1.CodOperario, t1.Operario,t1.Horas,t2.Categoria from (

                (SELECT  imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia,
		                        imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario as CodOperario,
                        REPLACE((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario + '-' + replace(imedexsa_intranet.dbo.Aux_TiemposNetosBonos.NombreOperario, ',', '')),'-MADE-', ' ') as Operario,
		                round((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto * imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono)/60,2) as Horas			
	                FROM imedexsa_intranet.dbo.Aux_TiemposNetosBonos Inner Join 
					                (SELECT T_PUESTOS.PUESTO, T_PUESTOS.SECC,
							                Case 
								                When T_MAQUINAS.TIPOP = '010' Then
									                1
								                Else
									                0
							                End as consumeMP
						                FROM T_PUESTOS LEFT OUTER JOIN T_MAQUINAS
								                ON T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP AND T_PUESTOS.PUESTO = T_MAQUINAS.MAQUI
						                WHERE (T_PUESTOS.CodEMP = '3')
								                and (T_PUESTOS.SECC <> '6')
					                ) as t_auxiliar 
			                on imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Maquina = t_auxiliar.PUESTO Left Outer Join imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF 
			                ON imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Mes = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Mes AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.idTrabajo AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Bono = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Bono 
	                WHERE (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia >= '" + fechaMin + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia <= '" + fechaMax + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.codemp = '3')
			                and Maquina = '850') t1
			left join 
			(select Dia,Operario,Horas,Categoria from TC_GALV_HORAS_INPRODUCTIVAS) t2
			on t1.Dia = t2.Dia and t1.CodOperario=t2.Operario) where Categoria is null";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }*/

        public DataTable ObtenerInformacionProductividadConsultar(string fechaMin, string fechaMax)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select t1.Dia,Operario,--Horas as 'Horas Total', --round(((minutosPresencia-isnull(minutos,0)) * PorcentajeTiempoBono)/60,2) as 'Horas Productivas', 
                 cast((round(((minutosPresencia-isnull(minutos,0)) * PorcentajeTiempoBono)/60,2)) as decimal(18,2))as 'Horas Total'
                --*
                from (
                SELECT  imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia,
		                                        imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario as CodOperario,
								                imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo,

                                        REPLACE((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario + '-' + replace(imedexsa_intranet.dbo.Aux_TiemposNetosBonos.NombreOperario, ',', '')),'-MADE-', ' ') as Operario,
		                                round((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto * imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono)/60,2) as Horas,
						                imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto as minutosPresencia,
						                imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono 
	                                FROM imedexsa_intranet.dbo.Aux_TiemposNetosBonos Inner Join 
					                                (SELECT T_PUESTOS.PUESTO, T_PUESTOS.SECC,
							                                Case 
								                                When T_MAQUINAS.TIPOP = '010' Then
									                                1
								                                Else
									                                0
							                                End as consumeMP
						                                FROM T_PUESTOS LEFT OUTER JOIN T_MAQUINAS
								                                ON T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP AND T_PUESTOS.PUESTO = T_MAQUINAS.MAQUI
						                                WHERE (T_PUESTOS.CodEMP = '3')
								                                and (T_PUESTOS.SECC <> '6')
					                                ) as t_auxiliar 
			                                on imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Maquina = t_auxiliar.PUESTO Left Outer Join imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF 
			                                ON imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Mes = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Mes AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.idTrabajo AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Bono = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Bono 
	                                WHERE (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia >=  '" + fechaMin + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia <= '" + fechaMax + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.codemp = '3')
			                and Maquina = '850') t1
			                left join 
							(SELECT 
							cs_cpp_0713.dbo.MJ_INTERRUPCIONES.id, 
							cs_cpp_0713.dbo.MJ_INTERRUPCIONES.mjTrabajoConBonoId, 
								cs_cpp_0713.dbo.MJ_INTERRUPCIONES.interrupcionId as interrucpion, 
								isnull(cs_cpp_0713.dbo.GN_INTERRUPCIONES.Nombre, '-') as nombreInterrupcion,
								cs_cpp_0713.dbo.MJ_INTERRUPCIONES.iFecha, 
								cs_cpp_0713.dbo.MJ_INTERRUPCIONES.fFecha,
								DATEDIFF(mi, cs_cpp_0713.dbo.MJ_INTERRUPCIONES.iFecha, cs_cpp_0713.dbo.MJ_INTERRUPCIONES.fFecha) as minutos
							FROM cs_cpp_0713.dbo.MJ_INTERRUPCIONES LEFT OUTER JOIN cs_cpp_0713.dbo.GN_INTERRUPCIONES
							ON cs_cpp_0713.dbo.MJ_INTERRUPCIONES.interrupcionid = cs_cpp_0713.dbo.GN_INTERRUPCIONES.id) 
							t2 on t1.idTrabajo = t2.mjTrabajoConBonoId";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public DataTable ObtenerInformacionProductividadComputar(string fechaMin, string fechaMax)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select Dia,Operario, t2.interrupcionId as 'Id Interrupción',nombreInterrupcion as Interrupción, minutos 'Duración Interrupción en Minutos',cast((round(((isnull(minutos,0)) * PorcentajeTiempoBono)/60,2)) as decimal(18,2))as 'Duración Interrupción en Horas'
		  
                from (
                SELECT  imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia,
		                                        imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario as CodOperario,
								                imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo,

                                        REPLACE((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario + '-' + replace(imedexsa_intranet.dbo.Aux_TiemposNetosBonos.NombreOperario, ',', '')),'-MADE-', ' ') as Operario,
		                                round((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto * imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono)/60,2) as Horas,
						                imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto as minutosPresencia,
						                imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono 
	                                FROM imedexsa_intranet.dbo.Aux_TiemposNetosBonos Inner Join 
					                                (SELECT T_PUESTOS.PUESTO, T_PUESTOS.SECC,
							                                Case 
								                                When T_MAQUINAS.TIPOP = '010' Then
									                                1
								                                Else
									                                0
							                                End as consumeMP
						                                FROM T_PUESTOS LEFT OUTER JOIN T_MAQUINAS
								                                ON T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP AND T_PUESTOS.PUESTO = T_MAQUINAS.MAQUI
						                                WHERE (T_PUESTOS.CodEMP = '3')
								                                and (T_PUESTOS.SECC <> '6')
					                                ) as t_auxiliar 
			                                on imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Maquina = t_auxiliar.PUESTO Left Outer Join imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF 
			                                ON imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Mes = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Mes AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.idTrabajo AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Bono = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Bono 
	                                WHERE (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia >=  '" + fechaMin + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia <= '" + fechaMax + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.codemp = '3')
			                and Maquina = '850') t1
			                join 
							(SELECT 
							cs_cpp_0713.dbo.MJ_INTERRUPCIONES.id, 
							CS_CPP_0713.dbo.MJ_INTERRUPCIONES.interrupcionId,
							cs_cpp_0713.dbo.MJ_INTERRUPCIONES.mjTrabajoConBonoId, 
								cs_cpp_0713.dbo.MJ_INTERRUPCIONES.interrupcionId as interrucpion, 
								isnull(cs_cpp_0713.dbo.GN_INTERRUPCIONES.Nombre, '-') as nombreInterrupcion,
								cs_cpp_0713.dbo.MJ_INTERRUPCIONES.iFecha, 
								cs_cpp_0713.dbo.MJ_INTERRUPCIONES.fFecha,
								DATEDIFF(mi, cs_cpp_0713.dbo.MJ_INTERRUPCIONES.iFecha, cs_cpp_0713.dbo.MJ_INTERRUPCIONES.fFecha) as minutos
							FROM cs_cpp_0713.dbo.MJ_INTERRUPCIONES LEFT OUTER JOIN cs_cpp_0713.dbo.GN_INTERRUPCIONES
							ON cs_cpp_0713.dbo.MJ_INTERRUPCIONES.interrupcionid = cs_cpp_0713.dbo.GN_INTERRUPCIONES.id) 
							t2 on t1.idTrabajo = t2.mjTrabajoConBonoId";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public DataTable ObtenerResultadoProductividad(string fechaMin, string fechaMax)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

               /* strsql = @"SELECT Fecha,SumNegro 'Kgs en Negro' ,SumBlanco 'Kgs en Blanco',ROUND(ISNULL(Suma,0),2) as 'Horas Trabajadas'
                FROM 
                    (Select FechaFiltro as Fecha,SumNegro,SumBlanco from (
                select FechaFiltro, cast (Sum(KGNegro) as float) SumNegro,Sum(KGBlanco) SumBlanco ,(Sum(KGBlanco) - Sum(KGNegro)) as Diferencia,Sum(micras_min) as PromedioMin, Sum(micras_med) as PromedioMed,Sum(micras_max) as PromedioMax , (select count (*) from (select case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltroConteo from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0 ) as CalculoPerchas where FechaFiltroConteo = FechaFiltro) as TotalFilas from ( 
						                select * from(
						                select ((cast (PesoNegro as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGNegro',((cast (PesoBlanco as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGBlanco'
                                        ,micras_min,micras_med,micras_max ,FechaFinGalvanizado, case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltro from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0) as InFo where CAST(FechaFiltro as DATE) >= '" + fechaMin + @"' and  CAST(FechaFiltro as DATE) <= '" + fechaMax + @"') as InfoDia group by FechaFiltro) as InfoDos) t1
                LEFT JOIN
                    (select Dia, SUM(Horas) as Suma from(
                SELECT  imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia,
		                REPLACE((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario + '-' + replace(imedexsa_intranet.dbo.Aux_TiemposNetosBonos.NombreOperario, ',', '')),'-MADE-', ' ') as Operario,
		                round((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto * imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono)/60,2) as Horas			
	                FROM imedexsa_intranet.dbo.Aux_TiemposNetosBonos Inner Join 
					                (SELECT T_PUESTOS.PUESTO, T_PUESTOS.SECC,
							                Case 
								                When T_MAQUINAS.TIPOP = '010' Then
									                1
								                Else
									                0
							                End as consumeMP
						                FROM T_PUESTOS LEFT OUTER JOIN T_MAQUINAS
								                ON T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP AND T_PUESTOS.PUESTO = T_MAQUINAS.MAQUI
						                WHERE (T_PUESTOS.CodEMP = '3')
								                and (T_PUESTOS.SECC <> '6')
					                ) as t_auxiliar 
			                on imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Maquina = t_auxiliar.PUESTO Left Outer Join imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF 
			                ON imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Mes = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Mes AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.idTrabajo AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Bono = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Bono 
	                WHERE (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia >= '" + fechaMin + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia <= '" + fechaMax + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.codemp = '3')
			                and Maquina = '850') as SUMHORASDIA group by dia) t2
                ON (t1.Fecha = t2.Dia)";*/

                /*strsql = @"				SELECT Fecha,SumNegro 'Kgs en Negro' ,SumBlanco 'Kgs en Blanco',ROUND(ISNULL(Suma,0),2) as 'Horas Trabajadas',  case when ROUND(ISNULL(Suma,0),2) = 0 then '0' else ROUND(SumBlanco/ROUND(ISNULL(Suma,0),2),0) end as 'Kgs/Horas' 
                FROM 
                    (Select FechaFiltro as Fecha,SumNegro,SumBlanco from (
                select FechaFiltro, cast (Sum(KGNegro) as float) SumNegro,Sum(KGBlanco) SumBlanco ,(Sum(KGBlanco) - Sum(KGNegro)) as Diferencia,Sum(micras_min) as PromedioMin, Sum(micras_med) as PromedioMed,Sum(micras_max) as PromedioMax , (select count (*) from (select case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltroConteo from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0 ) as CalculoPerchas where FechaFiltroConteo = FechaFiltro) as TotalFilas from ( 
						                select * from(
						                select ((cast (PesoNegro as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGNegro',((cast (PesoBlanco as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGBlanco'
                                        ,micras_min,micras_med,micras_max ,FechaFinGalvanizado, case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltro from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0) as InFo where CAST(FechaFiltro as DATE) >= '" + fechaMin + @"' and  CAST(FechaFiltro as DATE) <= '" + fechaMax + @"') as InfoDia group by FechaFiltro) as InfoDos) t3
                LEFT JOIN
                    (select Dia, SUM(Horas) as Suma from(
				select t1.Dia,t1.CodOperario, t1.Operario,t1.Horas,t2.Categoria from (

                (SELECT  imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia,
		                        imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario as CodOperario,
                        REPLACE((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario + '-' + replace(imedexsa_intranet.dbo.Aux_TiemposNetosBonos.NombreOperario, ',', '')),'-MADE-', ' ') as Operario,
		                round((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto * imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono)/60,2) as Horas			
	                FROM imedexsa_intranet.dbo.Aux_TiemposNetosBonos Inner Join 
					                (SELECT T_PUESTOS.PUESTO, T_PUESTOS.SECC,
							                Case 
								                When T_MAQUINAS.TIPOP = '010' Then
									                1
								                Else
									                0
							                End as consumeMP
						                FROM T_PUESTOS LEFT OUTER JOIN T_MAQUINAS
								                ON T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP AND T_PUESTOS.PUESTO = T_MAQUINAS.MAQUI
						                WHERE (T_PUESTOS.CodEMP = '3')
								                and (T_PUESTOS.SECC <> '6')
					                ) as t_auxiliar 
			                on imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Maquina = t_auxiliar.PUESTO Left Outer Join imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF 
			                ON imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Mes = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Mes AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.idTrabajo AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Bono = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Bono 
	                WHERE (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia >= '" + fechaMin + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia <= '" + fechaMax + @"')
			                and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.codemp = '3')
			                and Maquina = '850') t1
			left join 
			(select Dia,Operario,Horas,Categoria from TC_GALV_HORAS_INPRODUCTIVAS) t2
			on t1.Dia = t2.Dia and t1.CodOperario=t2.Operario) where Categoria is null) as SUMHORASDIA group by dia) t4
                ON (t3.Fecha = t4.Dia)";
                */

                strsql = @"SELECT Fecha,SumNegro 'Kgs en Negro' ,SumBlanco 'Kgs en Blanco',ROUND(ISNULL(Suma,0),2) as 'Horas Trabajadas',  case when ROUND(ISNULL(Suma,0),2) = 0 then '0' else ROUND(SumBlanco/ROUND(ISNULL(Suma,0),2),0) end as 'Kgs/Horas' 
                                    FROM 
                                        (Select FechaFiltro as Fecha,SumNegro,SumBlanco from (
                                    select FechaFiltro, cast (Sum(KGNegro) as float) SumNegro,Sum(KGBlanco) SumBlanco ,(Sum(KGBlanco) - Sum(KGNegro)) as Diferencia,Sum(micras_min) as PromedioMin, Sum(micras_med) as PromedioMed,Sum(micras_max) as PromedioMax , (select count (*) from (select case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltroConteo from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0 ) as CalculoPerchas where FechaFiltroConteo = FechaFiltro) as TotalFilas from ( 
						                                    select * from(
						                                    select ((cast (PesoNegro as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGNegro',((cast (PesoBlanco as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGBlanco'
                                                            ,micras_min,micras_med,micras_max ,FechaFinGalvanizado, case when CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(TC_GALV_Cuelgue.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end as FechaFiltro from TC_GALV_Cuelgue  where tipo = 'Produccion' and estado = 4 and PesoInicial >0) as InFo where CAST(FechaFiltro as DATE) >= '" + fechaMin + @"' and  CAST(FechaFiltro as DATE) <= '" + fechaMax + @"') as InfoDia group by FechaFiltro) as InfoDos) t3
                                    LEFT JOIN
                                        (Select Dia,SUM(HorasTotal) as Suma from
                    (select t1.Dia,Operario,--Horas as 'Horas Total', --round(((minutosPresencia-isnull(minutos,0)) * PorcentajeTiempoBono)/60,2) as 'Horas Productivas', 
                     cast((round(((minutosPresencia-isnull(minutos,0)) * PorcentajeTiempoBono)/60,2)) as decimal(18,2))as HorasTotal
                    --*
                    from (
                    SELECT  imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia,
		                                            imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario as CodOperario,
								                    imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo,

                                            REPLACE((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.CodOperario + '-' + replace(imedexsa_intranet.dbo.Aux_TiemposNetosBonos.NombreOperario, ',', '')),'-MADE-', ' ') as Operario,
		                                    round((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto * imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono)/60,2) as Horas,
						                    imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto as minutosPresencia,
						                    imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono 
	                                    FROM imedexsa_intranet.dbo.Aux_TiemposNetosBonos Inner Join 
					                                    (SELECT T_PUESTOS.PUESTO, T_PUESTOS.SECC,
							                                    Case 
								                                    When T_MAQUINAS.TIPOP = '010' Then
									                                    1
								                                    Else
									                                    0
							                                    End as consumeMP
						                                    FROM T_PUESTOS LEFT OUTER JOIN T_MAQUINAS
								                                    ON T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP AND T_PUESTOS.PUESTO = T_MAQUINAS.MAQUI
						                                    WHERE (T_PUESTOS.CodEMP = '3')
								                                    and (T_PUESTOS.SECC <> '6')
					                                    ) as t_auxiliar 
			                                    on imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Maquina = t_auxiliar.PUESTO Left Outer Join imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF 
			                                    ON imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Mes = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Mes AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.idTrabajo AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Bono = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Bono 
	                                    WHERE (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia >= '" + fechaMin + @"')
			                                    and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia <= '" + fechaMax + @"')
			                                    and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.codemp = '3')
			                                    and Maquina = '850') as T1 left join
							                    (SELECT 
							                    cs_cpp_0713.dbo.MJ_INTERRUPCIONES.id, 
							                    cs_cpp_0713.dbo.MJ_INTERRUPCIONES.mjTrabajoConBonoId, 
								                    cs_cpp_0713.dbo.MJ_INTERRUPCIONES.interrupcionId as interrucpion, 
								                    isnull(cs_cpp_0713.dbo.GN_INTERRUPCIONES.Nombre, '-') as nombreInterrupcion,
								                    cs_cpp_0713.dbo.MJ_INTERRUPCIONES.iFecha, 
								                    cs_cpp_0713.dbo.MJ_INTERRUPCIONES.fFecha,
								                    DATEDIFF(mi, cs_cpp_0713.dbo.MJ_INTERRUPCIONES.iFecha, cs_cpp_0713.dbo.MJ_INTERRUPCIONES.fFecha) as minutos
							                    FROM cs_cpp_0713.dbo.MJ_INTERRUPCIONES LEFT OUTER JOIN cs_cpp_0713.dbo.GN_INTERRUPCIONES
							                    ON cs_cpp_0713.dbo.MJ_INTERRUPCIONES.interrupcionid = cs_cpp_0713.dbo.GN_INTERRUPCIONES.id) 
							                    t2 on t1.idTrabajo = t2.mjTrabajoConBonoId) as TablaSUMA group by Dia) t4
                                    ON (t3.Fecha = t4.Dia)";


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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public void eliminarInproductividad(string dia, string operario)
        {
            //Delete from[gg].[dbo].TC_GALV_Cuelgue_Op_Empleado where NumEmpleado = 9999 and IdCuelgue = 247; 
            string sql = " Delete from [gg].[dbo].TC_GALV_HORAS_INPRODUCTIVAS where Dia = convert(date,'" + dia + "',103)  and Operario = '" + operario + "'; ";
            SqlConnection connection = new SqlConnection(Utils.CD.getConexion());


            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }



        public DataTable ObtenerCuelguesPorMarca(string marca)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select  IdCuelgue,cast (FechaC as Date) as 'Fecha Creacion' from TC_GALV_Cuelgue_Etiqueta where marca = '"+marca+"' order by fechac desc";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public DataTable ObtenerCuelguesPorPaquete(string paquete)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select * from(
                select  distinct IdCuelgue,cast (FechaC as Date) as 'FechaCreacion' from TC_GALV_Cuelgue_Etiqueta where paquete = '" + paquete + "') as Info order by FechaCreacion desc";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public DataTable ObtenerContenidoCuelgue(string cuelgue)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "select IdEtiqueta,Marca,Paquete,PesoUnitario, CantidadOriginal, Expedicion,Camion,OrdenPaquete from tc_galv_Cuelgue_etiqueta where IdCuelgue = '"+cuelgue+"'";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public DataTable obtenerInfoEstadisticas(string year, string mes)
        {

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("IdCuelgue");
            dataTable.Columns.Add("IdEtiqueta");
            dataTable.Columns.Add("Codigo");
            dataTable.Columns.Add("PesoPieza");
            dataTable.Columns.Add("MP");
            dataTable.Columns.Add("MP_Codigo");
            dataTable.Columns.Add("Espesor");
            dataTable.Columns.Add("PesoPercha");
            dataTable.Columns.Add("Utiles");
            dataTable.Columns.Add("PesoNegro");
            dataTable.Columns.Add("PesoNegroSinPercha");
            dataTable.Columns.Add("PesoBlanco");
            dataTable.Columns.Add("DiferenciaZinc");
            dataTable.Columns.Add("Porcentaje");
            dataTable.Columns.Add("PesoRealMarca");
            dataTable.Columns.Add("EspesorPredominante");
            dataTable.Columns.Add("MPPredominante");

            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();


                SqlCommand sqlCmd = new SqlCommand("Info_Estadisticas_Galvanizado", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 180;


                sqlCmd.Parameters.Add("@user", SqlDbType.VarChar);
                sqlCmd.Parameters["@user"].Value = Environment.UserName.ToString();

                sqlCmd.Parameters.Add("@anio", SqlDbType.VarChar);
                sqlCmd.Parameters["@anio"].Value = year;

                sqlCmd.Parameters.Add("@mes", SqlDbType.Int);
                sqlCmd.Parameters["@mes"].Value = mes;


                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow fila = dataTable.NewRow();
                    fila[0] = reader[0];
                    fila[1] = reader[1];
                    fila[2] = reader[2];
                    fila[3] = reader[3];
                    fila[4] = reader[4];
                    fila[5] = reader[5];
                    fila[6] = reader[6];
                    fila[7] = reader[7];
                    fila[8] = reader[8];
                    fila[9] = reader[9];
                    fila[10] = reader[10];
                    fila[11] = reader[11];
                    fila[12] = reader[12];
                    fila[13] = reader[13];
                    fila[14] = reader[14];
                    fila[15] = reader[15];
                    fila[16] = reader[16];


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

        //Métodos
        public DataTable obtenerTipoMaterial()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();
                /*
                strsql = @"select MP, Sum(PesoRealPorMarca) from(
                            select Agrupado.IdCuelgue,Agrupado.IdEtiqueta, Agrupado.Codigo,Agrupado.PesoPieza,MP_Codigo,Espesor,

                            case when Mp = 'M1' then 'Angulares'
                            when MP = 'M2' OR MP = 'M3' then 'Chapas'
                            when MP = 'M4' then 'Tubos'
                            when MP ='M5' Then 'Otros' end  as Mp


                            ,PesoPercha,utiles,PesoNegro,PesoNegroSinPercha, (Porcentaje*PesoNegroSinPercha) /100 as PesoRealPorMarca ,Pesoblanco,DiferenciaZinc,porcentaje,Media from(
                            Select infoCompleta.IdCuelgue,infoCompleta.IdEtiqueta, infoCompleta.Codigo,infoCompleta.PesoPieza,MP_Codigo,Espesor,Mp,PesoPercha,utiles,PesoNegro,PesoNegroSinPercha, (Porcentaje*PesoNegroSinPercha) /100 as PesoRealPorMarca ,Pesoblanco,DiferenciaZinc,porcentaje,Media from(
                            select IdCuelgue,IdEtiqueta,Codigo,pesoPieza,Mp_Codigo,Espesor,MP,PesoPercha,utiles,PesoNegro,PesoNegroSinPercha, Pesoblanco,DiferenciaZinc,
                            (
                            select porcentaje from(
                            select IdCuelgue,IdEtiqueta,PesoPieza,  round( PesoPieza * 100 / s , 5)  as  porcentaje from(
                            select IdCuelgue,IdEtiqueta,case when PesoPieza = 0 then 0.1 else pesoPieza end as PesoPieza from AUX_INFORME_GALV  where idCuelgue = temp.IdCuelgue) as p
                            cross join (select sum(PesoPieza) as s from (select IdCuelgue,IdEtiqueta,case when PesoPieza = 0 then 0.1 else pesoPieza end as PesoPieza from AUX_INFORME_GALV  where idCuelgue = temp.IdCuelgue) as p2) as t) as final where IdCuelgue= temp.IdCuelgue and IdEtiqueta = temp.IdEtiqueta) as Porcentaje
                             from AUX_INFORME_GALV temp ) as infoCompleta join
                            (select  IdCUelgue ,round (avg(espesor),0) as Media from AUX_INFORME_GALV  group by IdCuelgue) as espesorMedio on infoCompleta.IdCuelgue = espesorMedio.IdCuelgue ) as Agrupado) as consulta group by mp";*/

                /* --ANTERIOR VERSION
                strsql = @"select MP,PesoRealMarcaAgrupada, Round((PesoRealMarcaAgrupada/1000),0) as Toneladas, ((PesoRealMarcaAgrupada * 100) / (select sum(cast (PesoRealMarca as float)) from AUX_INFORME_GALV)) as Porcentaje from (
                            select Mp, SUM(PesoRealMarca)  as PesoRealMarcaAgrupada from (
                            select MP, PesoRealMarca from (
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, case when Mp = 'M1' then 'Angulares'
                            when MP = 'M2' OR MP = 'M3' then 'Chapas'
                            when MP = 'M4' then 'Tubos' else 'Otros' end  as  MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje, cast (PesoRealMarca as Float) as PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV) as Info) as Agrupado group by Mp) as InfoDef";
                */
                //VERSION PRUEBA
               /* strsql = @"select MP,PesoRealMarcaAgrupada, Round((PesoRealMarcaAgrupada/1000),0) as Toneladas, ((PesoRealMarcaAgrupada * 100) / (select sum(cast (PesoRealMarca as float)) from AUX_INFORME_GALV)) as Porcentaje from (
                            select Mp, SUM(PesoRealMarca)  as PesoRealMarcaAgrupada,MP_orden from (
                            select MP, PesoRealMarca,MP_orden from (
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, case when Mp = 'M1' then 'Angulares'
                            when MP = 'M2' OR MP = 'M3' then 'Chapas'
                            when MP = 'M4' then 'Tubos' else 'Otros' end  as  MP, 
							case when Mp = 'M1' then 1
                            when MP = 'M2' OR MP = 'M3' then 2
                            when MP = 'M4' then 3 else 4 end  as  MP_orden, 							
							MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje, cast (PesoRealMarca as Float) as PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV ) as Info) as Agrupado group by Mp,MP_orden) as InfoDef order by MP_orden";
                */
                strsql = @" select Catego,isnull(PesoRealMarcaAgrupada,0), isnull(Round((PesoRealMarcaAgrupada/1000),0),0) as Toneladas, Isnull(((PesoRealMarcaAgrupada * 100) / (select sum(cast (PesoRealMarca as float)) from AUX_INFORME_GALV)),0) as Porcentaje from (
                            select Catego, SUM(PesoRealMarca)  as PesoRealMarcaAgrupada,MP_orden from (
                            select Catego, PesoRealMarca,MP_orden from (
							select * from (
							(select case when CATEGO = 'M1' then 'Angulares'
                            when CATEGO = 'M2' OR CATEGO = 'M3' then 'Chapas'
                            when CATEGO = 'M4' then 'Tubos' else 'Otros' end  as  CATEGO,
							case when CATEGO = 'M1' then 1
                            when CATEGO = 'M2' OR CATEGO = 'M3' then 2
                            when CATEGO = 'M4' then 3 else 4 end  as  MP_orden
							from T_CATEGORIAS where CODEMP = '3' and (CATEGO = 'M1' OR CATEGO = 'M2'  OR CATEGO = 'M4' OR CATEGO = 'M5')) as categorias
							left join
							(select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, case when Mp = 'M1' then 'Angulares'
                            when MP = 'M2' OR MP = 'M3' then 'Chapas'
                            when MP = 'M4' then 'Tubos' else 'Otros' end  as  MP,							
							MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje, cast (PesoRealMarca as Float) as PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV) as informacion on categorias.CATEGO = informacion.MP) 
                            ) as Info) as Agrupado group by Catego,MP_orden) as InfoDef order by MP_orden";
                

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }




        public DataTable ObtenerProdMes()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                /*strsql = @"select tab1.Espesor, PesoRealMarcaAgrupada as 'PesoKg' , Toneladas,Porcentaje as Relacion, Consumo from ( 

                            (select Espesor, PesoRealMarcaAgrupada, Round((PesoRealMarcaAgrupada/1000),0) as Toneladas, ((PesoRealMarcaAgrupada * 100) / (select sum(cast (PesoRealMarca as float)) from AUX_INFORME_GALV)) as Porcentaje ,Orden  from (
                            Select Espesor, SUM(PesoRealMarca)  as PesoRealMarcaAgrupada,Orden  from(
                            select
                            case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)' else 'Otros'
                            end as Espesor,
							case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then 1
                            when EspesorPredominante = 4 then 2
                            when EspesorPredominante = 5 then 3
                            when EspesorPredominante = 6 then 4
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then 5
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then 6
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then 7
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then 8
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then 9
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then 10
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then 11
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then 12
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then 13 else 14
                            end as Orden,
							PesoRealMarca from (
                            select EspesorPredominante, PesoRealMarca from (
                            select  CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje,cast (PesoRealMarca as Float) as PesoRealMarca,cast (EspesorPredominante as Float) as EspesorPredominante, 
                                                     MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV) as Info) AgrupacionEspesor) as agrupado group by espesor,Orden) as Infodef) tab1
                            join
                            (select Espesor,Sum(PorcConsumoZn)/ count(*) as Consumo from (
                            Select  distinct IdCuelgue,Espesor,PorcConsumoZn from(
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc,case when PesoNegroSinPercha = 0 then 0 else (DiferenciaZinc/PesoNegroSinPercha) *100 end as 'PorcConsumoZn' ,Porcentaje, PesoRealMarca, EspesorPredominante,  MPPredominante, FechaC, UsuCre, FechaM, UsuMod from (
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)'
                            else 'Otros' end as Espesor, PesoPercha, Utiles, cast (PesoNegro as float) as PesoNegro, PesoNegroSinPercha, PesoBlanco, cast (DiferenciaZinc as float) DiferenciaZinc, Porcentaje, cast(PEsoRealMarca as DECIMAL(18, 2)) as PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV) as info) as Fin) as Pr group by espesor) tab2 on tab1.Espesor = tab2.Espesor) order by Orden";
                */
                strsql = @"select tab1.Espesor, PesoRealMarcaAgrupada as 'PesoKg' , Toneladas,Porcentaje as Relacion, Consumo,CantidadPiezas as 'Cantidad Piezas' from ( 

                            (select Espesor, PesoRealMarcaAgrupada,CantidadPiezas, Round((PesoRealMarcaAgrupada/1000),0) as Toneladas, ((PesoRealMarcaAgrupada * 100) / (select sum(cast (PesoRealMarca as float)) from AUX_INFORME_GALV)) as Porcentaje ,Orden  from (
                            Select Espesor, SUM(PesoRealMarca)  as PesoRealMarcaAgrupada,sum(cantidad) as CantidadPiezas,Orden  from(
                            select
                            case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)' else 'Otros'
                            end as Espesor,
							case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then 1
                            when EspesorPredominante = 4 then 2
                            when EspesorPredominante = 5 then 3
                            when EspesorPredominante = 6 then 4
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then 5
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then 6
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then 7
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then 8
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then 9
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then 10
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then 11
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then 12
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then 13 else 14
                            end as Orden,
							PesoRealMarca,cantidad from (
                            select EspesorPredominante, PesoRealMarca,cantidad from (
                            select  CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje,cast (PesoRealMarca as Float) as PesoRealMarca,cantidad,cast (EspesorPredominante as Float) as EspesorPredominante, 
                                                     MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV) as Info) AgrupacionEspesor) as agrupado group by espesor,Orden) as Infodef) tab1
                            join
                            (select Espesor,Sum(PorcConsumoZn)/ count(*) as Consumo from (
                            Select  distinct IdCuelgue,Espesor,PorcConsumoZn from(
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc,case when PesoNegroSinPercha = 0 then 0 else (DiferenciaZinc/PesoNegroSinPercha) *100 end as 'PorcConsumoZn' ,Porcentaje, PesoRealMarca, EspesorPredominante,  MPPredominante, FechaC, UsuCre, FechaM, UsuMod from (
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)'
                            else 'Otros' end as Espesor, PesoPercha, Utiles, cast (PesoNegro as float) as PesoNegro, PesoNegroSinPercha, PesoBlanco, cast (DiferenciaZinc as float) DiferenciaZinc, Porcentaje, cast(PEsoRealMarca as DECIMAL(18, 2)) as PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV) as info) as Fin) as Pr group by espesor) tab2 on tab1.Espesor = tab2.Espesor) order by Orden";
                



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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public DataTable ObtenerAngulares()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select tab1.Espesor, PesoRealMarcaAgrupada as 'PesoKg' , Toneladas,Porcentaje as Relacion, Consumo from ( 

                            (select Espesor, PesoRealMarcaAgrupada, Round((PesoRealMarcaAgrupada/1000),0) as Toneladas, ((PesoRealMarcaAgrupada * 100) / (select sum(cast (PesoRealMarca as float)) from AUX_INFORME_GALV)) as Porcentaje ,Orden  from (
                            Select Espesor, SUM(PesoRealMarca) as PesoRealMarcaAgrupada,Orden  from(
                            select
                            case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)' else 'Otros'
                            end as Espesor,						case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then 1
                            when EspesorPredominante = 4 then 2
                            when EspesorPredominante = 5 then 3
                            when EspesorPredominante = 6 then 4
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then 5
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then 6
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then 7
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then 8
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then 9
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then 10
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then 11
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then 12
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then 13 else 14
                            end as Orden, PesoRealMarca from (
                            select EspesorPredominante, PesoRealMarca from (
                            select  CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje,cast (PesoRealMarca as Float) as PesoRealMarca,cast (EspesorPredominante as Float) as EspesorPredominante, 
                                                     MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV where MpPredominante = 'M1') as Info) AgrupacionEspesor) as agrupado group by espesor,Orden) as Infodef) tab1
                            join
                            (select Espesor,Sum(PorcConsumoZn)/ count(*) as Consumo from (
                            Select  distinct IdCuelgue,Espesor,PorcConsumoZn from(
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc,case when PesoNegroSinPercha = 0 then 0 else (DiferenciaZinc/PesoNegroSinPercha) *100 end as 'PorcConsumoZn',Porcentaje, PesoRealMarca, EspesorPredominante,  MPPredominante, FechaC, UsuCre, FechaM, UsuMod from (
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)'
                            else 'Otros' end as Espesor, PesoPercha, Utiles, cast (PesoNegro as float) as PesoNegro, PesoNegroSinPercha, PesoBlanco, cast (DiferenciaZinc as float) DiferenciaZinc, Porcentaje, cast(PEsoRealMarca as DECIMAL(18, 2)) as PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV where MpPredominante = 'M1') as info) as Fin) as Pr group by espesor) tab2 on tab1.Espesor = tab2.Espesor) order by Orden";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public DataTable ObtenerChapas()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"
select tab1.Espesor, PesoRealMarcaAgrupada as 'PesoKg' , Toneladas,Porcentaje as Relacion, Consumo from ( 

                            (select Espesor, PesoRealMarcaAgrupada, Round((PesoRealMarcaAgrupada/1000),0) as Toneladas, ((PesoRealMarcaAgrupada * 100) / (select sum(cast (PesoRealMarca as float)) from AUX_INFORME_GALV)) as Porcentaje ,Orden  from (
                            Select Espesor, SUM(PesoRealMarca) as PesoRealMarcaAgrupada,Orden  from(
                            select
                            case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)' else 'Otros'
                            end as Espesor,						case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then 1
                            when EspesorPredominante = 4 then 2
                            when EspesorPredominante = 5 then 3
                            when EspesorPredominante = 6 then 4
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then 5
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then 6
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then 7
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then 8
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then 9
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then 10
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then 11
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then 12
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then 13 else 14
                            end as Orden, PesoRealMarca from (
                            select EspesorPredominante, PesoRealMarca from (
                            select  CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje,cast (PesoRealMarca as Float) as PesoRealMarca,cast (EspesorPredominante as Float) as EspesorPredominante, 
                                                     MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV  where MPPredominante = 'M2' or MPPredominante = 'M3') as Info) AgrupacionEspesor) as agrupado group by espesor,Orden) as Infodef) tab1
                            join
                            (select Espesor,Sum(PorcConsumoZn)/ count(*) as Consumo from (
                            Select  distinct IdCuelgue,Espesor,PorcConsumoZn from(
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc,case when PesoNegroSinPercha = 0 then 0 else (DiferenciaZinc/PesoNegroSinPercha) *100 end as 'PorcConsumoZn',Porcentaje, PesoRealMarca, EspesorPredominante,  MPPredominante, FechaC, UsuCre, FechaM, UsuMod from (
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)'
                            else 'Otros' end as Espesor, PesoPercha, Utiles, cast (PesoNegro as float) as PesoNegro, PesoNegroSinPercha, PesoBlanco, cast (DiferenciaZinc as float) DiferenciaZinc, Porcentaje, cast(PEsoRealMarca as DECIMAL(18, 2)) as PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV  where MPPredominante = 'M2' or MPPredominante = 'M3') as info) as Fin) as Pr group by espesor) tab2 on tab1.Espesor = tab2.Espesor) order by Orden";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public DataTable ObtenerTubos()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select tab1.Espesor, PesoRealMarcaAgrupada as 'PesoKg' , Toneladas,Porcentaje as Relacion, Consumo from ( 

                            (select Espesor, PesoRealMarcaAgrupada, Round((PesoRealMarcaAgrupada/1000),0) as Toneladas, ((PesoRealMarcaAgrupada * 100) / (select sum(cast (PesoRealMarca as float)) from AUX_INFORME_GALV)) as Porcentaje ,Orden  from (
                            Select Espesor, SUM(PesoRealMarca) as PesoRealMarcaAgrupada,Orden  from(
                            select
                            case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)' else 'Otros'
                            end as Espesor,						case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then 1
                            when EspesorPredominante = 4 then 2
                            when EspesorPredominante = 5 then 3
                            when EspesorPredominante = 6 then 4
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then 5
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then 6
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then 7
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then 8
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then 9
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then 10
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then 11
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then 12
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then 13 else 14
                            end as Orden, PesoRealMarca from (
                            select EspesorPredominante, PesoRealMarca from (
                            select  CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje,cast (PesoRealMarca as Float) as PesoRealMarca,cast (EspesorPredominante as Float) as EspesorPredominante, 
                                                     MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV  where MpPredominante = 'M4') as Info) AgrupacionEspesor) as agrupado group by espesor,Orden) as Infodef) tab1
                            join
                            (select Espesor,Sum(PorcConsumoZn)/ count(*) as Consumo from (
                            Select  distinct IdCuelgue,Espesor,PorcConsumoZn from(
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc,case when PesoNegroSinPercha = 0 then 0 else (DiferenciaZinc/PesoNegroSinPercha) *100 end as 'PorcConsumoZn',Porcentaje, PesoRealMarca, EspesorPredominante,  MPPredominante, FechaC, UsuCre, FechaM, UsuMod from (
                            select CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo, case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)'
                            else 'Otros' end as Espesor, PesoPercha, Utiles, cast (PesoNegro as float) as PesoNegro, PesoNegroSinPercha, PesoBlanco, cast (DiferenciaZinc as float) DiferenciaZinc, Porcentaje, cast(PEsoRealMarca as DECIMAL(18, 2)) as PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV  where MpPredominante = 'M4') as info) as Fin) as Pr group by espesor) tab2 on tab1.Espesor = tab2.Espesor) order by Orden";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

       
        
        public DataTable ObtenerCuelguesEspesor(string espesor)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select IdCuelgue,PesaPercha , Pesada1,PesoNegroSinPercha,Pesada2,DiferenciaZinc, case when PesoNegroSinPercha = 0 then 0 else Round((DiferenciaZinc/PesoNegroSinPercha) *100,2) end as PorcDiferenciaZinc, EspesorPredominante,MPPredominante from (
                            select distinct IdCuelgue,cast (PesoPercha as float) as 'PesaPercha',cast (PesoNegro as float) as 'Pesada1',cast (PesoNegroSinPercha as float) as 'PesoNegroSinPercha',cast (PesoBlanco as float) as 'Pesada2', cast (DiferenciaZinc as float) as 'DiferenciaZinc',EspesorPredominante,MPPredominante from (
                            select   CodEmp, IdCuelgue, IdEtiqueta, Codigo, PesoPieza, MP, MP_Codigo,  case when EspesorPredominante>= 1 and EspesorPredominante <= 3 then '(1-3)'
                            when EspesorPredominante = 4 then '4'
                            when EspesorPredominante = 5 then '5'
                            when EspesorPredominante = 6 then '6'
                            when EspesorPredominante >= 7 and EspesorPredominante<=8 then '(7-8)'
                            when EspesorPredominante >= 9 and EspesorPredominante<=10 then '(9-10)'
                            when EspesorPredominante >= 11 and EspesorPredominante<=12 then '(11-12)'
                            when EspesorPredominante >= 13 and EspesorPredominante<=15 then '(13-15)'
                            when EspesorPredominante >= 16 and EspesorPredominante<=18 then '(16-18)'
                            when EspesorPredominante >= 19 and EspesorPredominante<=20 then '(19-20)'
                            when EspesorPredominante >= 21 and EspesorPredominante<=25 then '(21-25)'
                            when EspesorPredominante >= 26 and EspesorPredominante<=30 then '(26-30)'
                            when EspesorPredominante >= 31 and EspesorPredominante<=35 then '(31-35)'
                            else 'Otros' end as Espesor, PesoPercha, Utiles, PesoNegro, PesoNegroSinPercha, PesoBlanco, DiferenciaZinc, Porcentaje, PesoRealMarca, EspesorPredominante, 
                            MPPredominante, FechaC, UsuCre, FechaM, UsuMod from AUX_INFORME_GALV) as Info where Espesor = '" +espesor+"' ) as Info";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public DataTable ObtenerCuelguesError(string year, string mes)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"Select distinct IdCuelgue,idPercha,PesoPercha, Pesada1,KgNegro,Pesada2,KgBlanco,ConsumoZn, case when KgNegro > 0 then (ConsumoZn/KGNegro) *100 else '-1'end as 'PorcConsumoZn',micras_min,micras_med,micras_max,Inmersion,FechaFinGalvanizado as Fecha,
                case when (Pesada1 < PesoPercha ) then 'Error: Peso de la percha superior al peso con material en negro'
                when (Pesada2 <= Pesada1) then 'Error: Peso en blanco inferior al peso en negro'
                when (Inmersion <20) then 'Error: Tiempo de inmersión muy bajo'
				when ((case when KgNegro > 0 then (ConsumoZn/KGNegro) *100 else '-1'end > 0) and (case when KgNegro > 0 then (ConsumoZn/KGNegro) *100 else '-1'end < '0.5') and (case when KgNegro > 0 then (ConsumoZn/KGNegro) *100 else '-1'end > 10))   then 'Error : Consumo < 0.5% y >10%'
                else '' end as Error
                from (
                select cg.IdCuelgue, cg.FechaC,cg.IdPercha,perc.Tipo as 'Disposicion',(cast (PesoInicial as float ) + cast (Utiles as float )) as 'PesoPercha',cast (PesoNegro as float) as 'Pesada1' ,((cast (PesoNegro as float) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGNegro',FechaFinCuelgue as 'Fecha Salida',cast (PesoBlanco as int) as 'Pesada2',((cast (PesoBlanco as int) - cast (PesoInicial as float) - cast (utiles as float))) as 'KGBlanco',(((cast (PesoBlanco as float) - cast (PesoInicial as float) - cast (utiles as float))) - ((cast (PesoNegro as float) - cast (PesoInicial as float) - cast (utiles as float)))) as 'ConsumoZn',FechaIniGalvanizado,micras_min,micras_med,micras_max, FechaFinGalvanizado,DATEDIFF(second,FechaIniGalvanizado,FechaFinGalvanizado) 'Inmersion', '' as 'Espacio',

                IdEtiqueta,Marca, (cast (replace(pesoUnitario, ',','.') as float) * CantidadOriginal) as PesoPieza,
						
						                (SELECT TOP (1) T_ARTICULOS.CATEGORIA
                                        FROM T_ORDFABM INNER JOIN T_ARTICULOS ON T_ORDFABM.CODEMP = T_ARTICULOS.CodEMP AND T_ORDFABM.CODIGO = T_ARTICULOS.CODIGO
                                        WHERE (T_ORDFABM.CODEMP = '3') AND (T_ORDFABM.ORDFAB = le.OrdFab) AND (T_ORDFABM.CTDENL > 0) AND (T_ARTICULOS.FAMILIA = 'MP')) AS MP,
						
						                (SELECT TOP (1) T_ARTICULOS.CODIGO
                                        FROM T_ORDFABM INNER JOIN T_ARTICULOS ON T_ORDFABM.CODEMP = T_ARTICULOS.CodEMP AND T_ORDFABM.CODIGO = T_ARTICULOS.CODIGO
                                        WHERE (T_ORDFABM.CODEMP = '3') AND (T_ORDFABM.ORDFAB = le.OrdFab) AND (T_ORDFABM.CTDENL > 0) AND (T_ARTICULOS.FAMILIA = 'MP')) AS MP_Codigo
                from TC_GALV_Cuelgue cg
                join TC_GALV_Percha perc on cg.IdPercha = perc.IdPercha 
                join TC_GALV_Cuelgue_Etiqueta cge on cg.IdCuelgue = cge.IdCuelgue join TC_LANZA_ETIQUETA le on cge.IdEtiqueta =le.Etiqueta
                where MONTH(case when CAST(cg.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end) = '" + mes + "' and YEAR(case when CAST(cg.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='23:59:59' then dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) else CAST(FechaFinGalvanizado as DATE) end) = '" + year + "' and cg.Estado = 4) as Info where (Pesada1 < PesoPercha ) or (Pesada2 <= Pesada1)  or (Inmersion <20) or ((case when KgNegro > 0 then (ConsumoZn/KGNegro) *100 else '-1'end > 0) and (case when KgNegro > 0 then (ConsumoZn/KGNegro) *100 else '-1'end < '0.5') and (case when KgNegro > 0 then (ConsumoZn/KGNegro) *100 else '-1'end > 10))  ";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public DataTable ObtenerControlEntrada(string fechaMin, string fechaMax)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select cg.FechaIniCuelgue,
                            case when CAST(cg.FechaIniCuelgue As Time) >='21:30:00' And  CAST(cg.FechaIniCuelgue As Time) <='23:59:59' then
                            dateadd(day,1,CAST(FechaIniCuelgue as DATE)) 
                            else CAST(FechaIniCuelgue as DATE) end as FCCD,

                            case when CAST(cg.FechaIniCuelgue As Time) >='00:00:00' And  CAST(cg.FechaIniCuelgue As Time) <='05:29:59' then 'N'
                            when CAST(cg.FechaIniCuelgue As Time) >='05:30:00' And  CAST(cg.FechaIniCuelgue As Time) <='13:29:59' then 'M'
                            when CAST(cg.FechaIniCuelgue As Time) >='13:30:00' And  CAST(cg.FechaIniCuelgue As Time) <='21:29:59' then 'T' end as FCCT,

                            cg.Idcuelgue, cg.tipo as ct, cg.idpercha, percha.tipo as pt, cg.pesoinicial as KP0_P, cg.utiles KP0_U, 
                            (cg.PesoInicial+cg.Utiles) AS KP0, cg.PesoNegro AS KP1

                            from TC_GALV_Cuelgue cg join TC_GALV_Percha percha on cg.IdPercha = percha.IdPercha 
                            where cg.FechaIniCuelgue >= '" + fechaMin + @" 21:30:00' and   cg.FechaIniCuelgue <= '" + fechaMax + @" 21:30:00' and 
                            cg.CodEmp = '3' order by cg.FechaIniCuelgue";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        public DataTable ObtenerExportacionExcel(string fechaMin, string fechaMax)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select 
                        cg.Tipo as CT,'#' as S1, cg.IdCuelgue as IdCuelg, 

                        case when CAST(cg.FechaIniCuelgue As Time) >='21:30:00' And  CAST(cg.FechaIniCuelgue As Time) <='23:59:59' then
                        dateadd(day,1,CAST(FechaIniCuelgue as DATE)) 
                        else CAST(FechaIniCuelgue as DATE) end as FCCD,

                        case when CAST(cg.FechaIniCuelgue As Time) >='00:00:00' And  CAST(cg.FechaIniCuelgue As Time) <='05:29:59' then 'N'
                        when CAST(cg.FechaIniCuelgue As Time) >='05:30:00' And  CAST(cg.FechaIniCuelgue As Time) <='13:29:59' then 'M'
                        when CAST(cg.FechaIniCuelgue As Time) >='13:30:00' And  CAST(cg.FechaIniCuelgue As Time) <='21:29:59' then 'T' end as FCCT,

                        cg.IdPercha as ID_PC, cg.tipo as PT, (cg.PesoInicial+cg.Utiles) AS KP0, cg.PesoNegro AS KP1,
                        '#' AS S2,
                        case when CAST(cg.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='23:59:59' then
                        dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) 
                        else CAST(FechaFinGalvanizado as DATE) end as FCGD1,

                        case when CAST(cg.FechaFinGalvanizado As Time) >='00:00:00' And  CAST(cg.FechaFinGalvanizado As Time) <='05:29:59' then 'N'
                        when CAST(cg.FechaFinGalvanizado As Time) >='05:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='13:29:59' then 'M'
                        when CAST(cg.FechaFinGalvanizado As Time) >='13:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='21:29:59' then 'T' end as FCGT,

                        cg.IdPercha as ID_PG, cg.PesoBlanco as KP2, '#' AS S3,

                        case when CAST(cg.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='23:59:59' then
                        dateadd(day,1,CAST(FechaFinGalvanizado as DATE)) 
                        else CAST(FechaFinGalvanizado as DATE) end as FCGD2,

                        case when CAST(cg.FechaFinGalvanizado As Time) >='21:30:00' And  CAST(cg.FechaFinGalvanizado As Time) <='23:59:59' then
                        dateadd(day,1,CAST(FechaFinGalvanizado as TIME)) 
                        else CAST(FechaFinGalvanizado as TIME) end as FCGH2,

                        cg.micras_min as m_min, cg.micras_med as m_med, cg.micras_max as m_max, '' AS M_Tipo, '' AS M_Esp,
                        ROUND(DATEDIFF(second,FechaIniGalvanizado,FechaFinGalvanizado)/60,0) 'T_Inm_m',
                        (ROUND(DATEDIFF(second,FechaIniGalvanizado,FechaFinGalvanizado),2))- ((ROUND(DATEDIFF(second,FechaIniGalvanizado,FechaFinGalvanizado)/60,0))*60) 'T_Inm_s'

                        from  TC_GALV_Cuelgue cg join TC_GALV_Percha percha on cg.IdPercha = percha.IdPercha 
                        where cg.Tipo = 'Produccion' and   cg.FechaFinGalvanizado >= '" + fechaMin + @" 21:30:00' and   cg.FechaFinGalvanizado <= '" + fechaMax + @" 21:30:00' and cg.PesoInicial > 0 and
                        cg.CodEmp = '3' order by cg.FechaFinGalvanizado";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public String obtenerCBExpedicion(string camion)
        {
            string cb = "";
            string queryString = "Select TOP (1) cb_num " +
                "FROM [imedexsa_intranet].[dbo].GL_EXPED " +
                "where camion = '" + camion + "'" +
                " order by fch_creacion desc";

            using (SqlConnection connection = new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cb = reader[0].ToString();

                    }
                }

            }

            return cb;
        }



        public DataTable ObtenerPaquetesCamion(string expedicion)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;



            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = @"select 
                        ROW_NUMBER() over (order by GL_ITEMSEXP.fch_creacion asc)  as 'Orden del paquete' , CONCAT('73',  GL_PAQUE.cb_num )  as 'Código de barras paquete' ,CONVERT(VARCHAR(24),GL_RECEP.fch_recepcion,103) as 'Fecha pistoleado MADE', recepcionSanDECA.fch_recepcion as 'Fecha recepción Imedexsa' 
                        from [imedexsa_intranet].[dbo].GL_EXPED  
                        join [imedexsa_intranet].[dbo].GL_ITEMSEXP on GL_EXPED.id = GL_ITEMSEXP.exped_id 
                        join [imedexsa_intranet].[dbo].GL_PAQUE on GL_ITEMSEXP.paque_id = GL_PAQUE.id   
                        left join (SELECT * from [imedexsa_intranet].[dbo].GL_ITEMSREC where dispositivo = 'AUTO_MADE') as recAUTO on GL_PAQUE.id =   recAUTO.paque_id
                        left join [imedexsa_intranet].[dbo].GL_RECEP on recAUTO.recep_id = GL_RECEP.id
                        left join (SELECT * from [imedexsa_intranet].[dbo].GL_ITEMSREC where dispositivo <> 'AUTO_MADE') as recSanDECA on GL_PAQUE.id =   recSanDECA.paque_id
                        left join [imedexsa_intranet].[dbo].GL_RECEP as recepcionSanDECA on recSanDECA.recep_id = recepcionSanDECA.id
                        where GL_EXPED.cb_num = '" + expedicion + "' order by 'Orden del paquete' ";

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
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }






        public void insertarHorasInproductivas(string dia, string operario, string Horas, string categoria)
        {
            int lineasAfectadas = 0;

                string sql = "INSERT INTO [dbo].[TC_GALV_HORAS_INPRODUCTIVAS]" +
                             "([Dia] " +
                             ",[Operario]" +
                             ",[Horas]" +
                             ",[Categoria])" +
                              "VALUES " +
                            "(convert(date,'" + dia+ "',103) " +
                             ",'" + operario + "' " +
                             ",REPLACE('" + Horas + "',',','.') " +
                             ",'" + categoria + "') ";

                using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(sql, connection);
                        connection.Open();
                        lineasAfectadas += command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException e)
                    {

                    }
                }
        }






    }

}
