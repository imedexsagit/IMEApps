using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace GestionPintura
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

        public DataTable obtenerEstadoBastidores()
        {
               
            DataTable table = null;

            string strsql = "";

            

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql = "SELECT IdBastidor, Estado FROM TC_PROCESO_PINTURA_BASTIDORES";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable obtenerCarros(DateTime desde, DateTime hasta, int tipo)
        {
            DataTable table = null;

            string strsql = "";

            TimeSpan ts = new TimeSpan(23, 59, 00);
            TimeSpan ts2 = new TimeSpan(00, 00, 00);

            DateTime hastaTime = hasta.Date + ts;
            DateTime desdeTime = desde.Date + ts2;
            
            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql = "SELECT IdCarro, IdBastidor, CASE Estado ";
	            strsql += " WHEN 1 then 'Iniciado' ";
	            strsql += " WHEN 2 THEN 'Fase Chorreo' ";
	            strsql += " WHEN 3 THEN 'Fase Pintura' ";
                strsql += " WHEN 4 THEN 'Finalizado' ";
                strsql += " END as ESTADO, PesoTotal, SuperficieTotal, PiezasTotal, FechaC FROM TC_PROCESO_PINTURA_CARROS where FECHAC <= '" + hastaTime.ToString("yyyy-MM-dd HH:mm") + "' and FECHAC >= '" + desdeTime.ToString("yyyy-MM-dd HH:mm") + "' ";

                switch (tipo)
                {
                    case 0: //todo
                        break;

                    case 1: //en proceso
                        strsql += " and Estado <> 4 ";
                        break;

                    case 2: //terminados
                        strsql += " and Estado = 4 ";
                        break;
                }

                strsql += " order by FechaC desc";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable obtenerContenedoresMes(DateTime desde, DateTime hasta)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql = "select distinct pl.PEDIDOS, pl.CLIENTES, pm.Packing, Contenedor, pl.PROYECTOS,  count(distinct pm.Paquete) AS Nº_PAQUETES, count(pm.Marca) as PIEZAS, sum (pm.Peso) as KILOS, sum (Superficie) AS SUPERFICIE from TC_PROCESO_PINTURA_CARROS_MARCAS pm ";
                strsql += "left join TC_PACKING_LIST pl on pl.PACKING = pm.Packing ";
                strsql += "where pm.FechaC >= '" + desde.Date.ToString("yyyy-MM-dd") + "' and pm.FechaC <= '" + hasta.Date.ToString("yyyy-MM-dd") + "' ";
                strsql += "group by pl.PEDIDOS, pl.CLIENTES, pm.Packing, Contenedor, pl.PROYECTOS ";
                strsql += "order by pl.PEDIDOS, pm.Packing ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable obtenerHorasPuestosPintura(DateTime desde, DateTime hasta)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql = "select concat('[', puesto, '] - ', t_auxiliar.DENOMI ) as PUESTO, format ( sum ((imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Neto * imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.PorcentajeTiempoBono)/60), 'N2','es-ES' )  as HORAS ";
                strsql += "FROM imedexsa_intranet.dbo.Aux_TiemposNetosBonos Inner Join  ";
                strsql += "	(SELECT T_PUESTOS.PUESTO, T_PUESTOS.SECC, ";
                strsql += "		Case  ";
                strsql += "			When T_MAQUINAS.TIPOP = '010' Then 1 ";
                strsql += "			Else 0 ";
                strsql += "			End as consumeMP, T_MAQUINAS.DENOMI ";
                strsql += "	FROM T_PUESTOS LEFT OUTER JOIN T_MAQUINAS ";
                strsql += "	ON T_PUESTOS.CodEMP = T_MAQUINAS.CODEMP AND T_PUESTOS.PUESTO = T_MAQUINAS.MAQUI ";
                strsql += "	WHERE (T_PUESTOS.CodEMP = '3') and (T_PUESTOS.SECC <> '06')) as t_auxiliar  ";
                strsql += "			on imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Maquina = t_auxiliar.PUESTO Left Outer Join imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF  ";
                strsql += "			ON imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Mes = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Mes AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.idTrabajo = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.idTrabajo AND imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Bono = imedexsa_intranet.dbo.Aux_TiemposNetosBonosOF.Bono ";
                strsql += "	WHERE (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia >= convert(datetime,'" + desde.Date + "' ,103)) ";
                strsql += "			and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.Dia < convert(datetime,'" + hasta.Date + "' ,103)) ";
                strsql += "			and (imedexsa_intranet.dbo.Aux_TiemposNetosBonos.codemp = '" + empresa + "') ";
                strsql += "			and (PUESTO = '875' or PUESTO = '876' or  PUESTO = '877' or PUESTO = '878' or PUESTO = '879' or PUESTO = '880' or PUESTO = '881' or PUESTO = '882') ";
                strsql += "	group by t_auxiliar.DENOMI, PUESTO ";
                strsql += "	order by PUESTO ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable obtenerTrazabilidadPiezasLanza(string codLanza)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql = " select ";
                strsql += "	xd2.MARCA, 	";
                strsql += "	[CANTIDAD TOTAL],	";
                strsql += "	[CANTIDAD PINTADA],	";
                strsql += "	PESO,	";
                strsql += "	[SUPERFICIE], /*FechaIniFormarCarro, FechaFinFormarCarro, FechaIniChorreo, FechaFinChorreo,*/ 	";
                strsql += "	(SELECT FORMAT (FechaIniPintura, 'dd/MM/yyyy HH:mm:ss ') as date) as [INICIO PINTURA],	";
                strsql += "	(SELECT FORMAT (FechaFinPintura, 'dd/MM/yyyy HH:mm:ss ') as date) AS [FIN PINTURA],	";
                strsql += "	case when xd2.LOTE is null then '-' else xd2.LOTE end as LOTE,	";
                strsql += "	case when pal.Colada is null then '-' else pal.Colada end AS COLADA,	";
                strsql += "	/*ct.NTRAZA,*/	";
                strsql += "	CARRO,	";
                strsql += "	PINTURA, 	";
                strsql += "	COLOR	";
                strsql += "from	";
                strsql += "(select 	";
                strsql += "	XD.MARCA, 	";
                strsql += "	cantidadPacking as [CANTIDAD TOTAL], ";
                strsql += "	cantidadCarrro  AS [CANTIDAD PINTADA],	";
                strsql += "	Peso AS PESO,	";
                strsql += "	Superficie AS [SUPERFICIE], /*FechaIniFormarCarro, FechaFinFormarCarro, FechaIniChorreo, FechaFinChorreo,*/ 	";
                strsql += "	FechaIniPintura,	";
                strsql += "	FechaFinPintura,	";
                strsql += "	(select 	";
                strsql += "		top (1) LOTE 	";
                strsql += "	from 	";
                strsql += "		TC_PINTURA_LOTES_CAMBIOS 	";
                strsql += "	WHERE 	";
                strsql += "		F_CAMBIO < FechaIniPintura 	";
                strsql += "		AND UBICACION = CR.CABINA 	";
                strsql += "	ORDER BY 	";
                strsql += "		F_CAMBIO DESC) AS LOTE,	";
                strsql += "		MC.IdCarro as CARRO,	";
                strsql += "	TIPO_PINTURA AS PINTURA, 	";
                strsql += "	COLOR_PINTURA AS COLOR	";
                strsql += "from 	";
                strsql += "	(SELECT DISTINCT	";
                strsql += "		CODLANZA, plld.PACKING, plpm.PAQUETE, plpm.MARCA	";
                strsql += "	FROM 	";
                strsql += "		TC_LANZA_PEDLIN_ORDFAB plof	";
                strsql += "		INNER JOIN TC_PACKING_LIST_LINEAS_DESGLOSE plld on plof.PEDIDO = plld.PEDIDO AND PLLD.LINEA = PLOF.LINEA	";
                strsql += "		INNER JOIN TC_PACKING_LIST_PAQUETES_MARCAS plpm on plpm.PACKING = plld.PACKING	";
                strsql += "	WHERE 	";
                strsql += "		CODLANZA = " + codLanza + "	";
                strsql += "		and plof.CODEMP = '" + empresa + "') xd	";
                strsql += "	LEFT JOIN TC_PROCESO_PINTURA_CARROS_MARCAS MC ON MC.Packing = XD.PACKING AND MC.Paquete = XD.PAQUETE AND MC.Marca = XD.MARCA	";
                strsql += "	LEFT JOIN TC_PROCESO_PINTURA_CARROS CR ON CR.IdCarro = MC.IdCarro 	";
                strsql += "	) xd2	";
                strsql += "	LEFT JOIN TC_PREALBARAN_LOTES pal on pal.Lote = xd2.LOTE	";
                strsql += "	LEFT JOIN TC_COLADAS_TRAZABILIDAD ct on ct.COLADA = pal.Colada	";
                strsql += "	ORDER BY FechaIniPintura DESC	";

                comando = new SqlCommand(strsql, conexion);
                comando.CommandTimeout = 1000;
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public DataTable obtenerValorSemana(string tipo)
        {

            DataTable table = new DataTable();
            table.Columns.Add("VALOR");
            table.Columns.Add("HORAS");

            string strsql = "";
            string tipoValor = "SuperficieTotal";
            string tipoOp = "";
            switch (tipo)
            {
                case "chorreo":
                    tipoOp = "Chorreo";
                    break;

                case "pintura":
                    tipoOp = "Pintura";
                    break;

                case "paquetesKg":
                    tipoOp = "Empaquetado";
                    tipoValor = "PesoTotal";
                    break;

                case "paquetes":
                    tipoOp = "Empaquetado";
                    break;
            }


            SqlCommand comando;
            SqlDataReader reader;

            string DATO = "";
            string horas = "";

            try
            {

                conexion.Open();

                DateTime dia = new DateTime(DateTime.Now.Year, 1, 2);


                while (dia < DateTime.Now)
                {
                    DataRow row = table.NewRow();

                    strsql = "select case when sum(" + tipoValor + ") is not null then sum(" + tipoValor + ") else 0 end as VALOR from TC_PROCESO_PINTURA_CARROS where FechaFin" + tipoOp + " is not null and FechaFin" + tipoOp + " >= '" + dia.Date.ToString("yyyy-MM-dd") + "' and FechaFin" + tipoOp + " < '" + dia.Date.AddDays(6).ToString("yyyy-MM-dd") + "'";

                    conexion.Close();

                    foreach (DataRow row2 in obtenerHorasPuestosPintura(dia.Date, dia.Date.AddDays(6)).Rows)
                    {
                        if (tipoOp == "Pintura")
                        {
                            if (row2["PUESTO"].ToString().Contains("PINTADO"))
                            {
                                horas = row2[1].ToString();
                            }
                        }
                        else
                        {
                            if (row2["PUESTO"].ToString().Contains(tipoOp.ToUpper()))
                            {
                                horas = row2[1].ToString();
                            }
                        }
                    }

                    //horas = obtenerHorasPuestosPintura(dia.Date, dia.Date.AddDays(6)).Rows[3][1].ToString();
                    conexion.Open();

                    row["HORAS"] = horas;
                    
                    dia = dia.Date.AddDays(7);

                    comando = new SqlCommand(strsql, conexion);
                    reader = comando.ExecuteReader();

                    

                    reader.Read();
                    DATO = reader[0].ToString();

                    row["VALOR"] = DATO;
                   

                    table.Rows.Add(row);
                }
                

                

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;

        }

        public DataTable obtenerValoresRatios(DateTime desde, DateTime hasta)
        {
            DataTable valores = new DataTable();

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "set ansi_warnings OFF;";
                strsql += "select 'M2 GRANALLADO' AS NOMBRE, case when sum(SuperficieTotal) is not null then sum(SuperficieTotal) else 0 end as VALOR INTO #t from TC_PROCESO_PINTURA_CARROS where FechaFinChorreo is not null and FechaFinChorreo >= '" + desde.Date.ToString("yyyy-MM-dd") + "' and FechaFinChorreo < '" + hasta.Date.ToString("yyyy-MM-dd") + "' ;";
                strsql += "insert into #t (NOMBRE, VALOR) VALUES ('M2 PINTURA', (SELECT case when sum(SuperficieTotal) is not null then sum(SuperficieTotal) else 0 end as SUPERFICIE from TC_PROCESO_PINTURA_CARROS where FechaFinPintura is not null and FechaFinPintura >= '" + desde.Date.ToString("yyyy-MM-dd") + "' and FechaFinPintura < '" + hasta.Date.ToString("yyyy-MM-dd") + "'));";
                strsql += "insert into #t (NOMBRE, VALOR) VALUES ('KG EMPAQUETADO', (SELECT case when sum(PesoTotal) is not null then sum(PesoTotal) else 0 end as SUPERFICIE from TC_PROCESO_PINTURA_CARROS where FechaFinEmpaquetado is not null and FechaFinEmpaquetado >= '" + desde.Date.ToString("yyyy-MM-dd") + "' and FechaFinEmpaquetado < '" + hasta.Date.ToString("yyyy-MM-dd") + "'));";
                strsql += "insert into #t (NOMBRE, VALOR) VALUES ('M2 EMPAQUETADO', (SELECT case when sum(SuperficieTotal) is not null then sum(SuperficieTotal) else 0 end as SUPERFICIE from TC_PROCESO_PINTURA_CARROS where FechaFinEmpaquetado is not null and FechaFinEmpaquetado >= '" + desde.Date.ToString("yyyy-MM-dd") + "' and FechaFinEmpaquetado < '" + hasta.Date.ToString("yyyy-MM-dd") + "'));";
                strsql += "set ansi_warnings ON;";
                strsql += "select * from #t;";
                strsql += "drop table #t;";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                valores = new DataTable();
                adapter.Fill(valores);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valores;
        }

        public DataTable obtenerValoresConsumoLotes(DateTime desde, DateTime hasta)
        {
            DataTable valores = new DataTable();

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "if OBJECT_ID (N'#tempCalculoLotes', N'U') IS NOT NULL ";
                strsql += "BEGIN ";
                strsql += "	drop table #tempCalculoLotes; ";
                strsql += "END; ";

                strsql += "declare @lote as varchar(20) = '-'; ";
                strsql += "declare @fDesde as datetime = '" + desde.ToString("yyyy-MM-dd") + "'; ";
                strsql += "declare @fHasta as datetime = '" + hasta.ToString("yyyy-MM-dd") + "'; ";
                strsql += "declare @fechaIniCambio as datetime; ";
                strsql += "declare @fechaFinCambio as datetime; ";
                strsql += "declare @pesoCalc as float; ";
                strsql += "declare @superfCalc as float; ";
                strsql += "declare @utilizado as int; ";
                strsql += "declare @codigo as varchar(30); ";
                strsql += "declare @fCad as datetime; ";
                strsql += "declare @cabina as int; ";
                strsql += " ";
                strsql += "create table #tempCalculoLotes ";
                strsql += "( ";
                strsql += "	lote varchar(20), ";
                strsql += "	codigo varchar(30), ";
                strsql += "	fcad datetime, ";
                strsql += "	fechaIni datetime, ";
                strsql += "	fechaFin datetime, ";
                strsql += "	kgUsados int, ";
                strsql += "	pesoTotal float, ";
                strsql += "	superficieTotal float, ";
                strsql += "	cabina int ";
                strsql += " ";
                strsql += "); ";
                strsql += " ";
                strsql += "declare crs cursor for select lote, f_cambio, UTILIZADO, UBICACION from TC_PINTURA_LOTES_CAMBIOS where F_CAMBIO >= @fDesde AND F_CAMBIO <= @fHasta ; ";
                strsql += " ";
                strsql += "open crs; ";
                strsql += "fetch next from crs into @lote, @fechaIniCambio, @utilizado, @cabina; ";
                strsql += " ";
                strsql += "while @@FETCH_STATUS = 0  ";
                strsql += "BEGIN; ";
                strsql += "	 ";
                strsql += "	set @fechaFinCambio = (SELECT  ";
                strsql += "								top(1) f_cambio  ";
                strsql += "							from  ";
                strsql += "								TC_PINTURA_LOTES_CAMBIOS  ";
                strsql += "							where ";
                strsql += "								F_CAMBIO > @fechaIniCambio ";
                strsql += "								and UBICACION = @cabina ";
                strsql += "							order by  ";
                strsql += "								F_CAMBIO asc ";
                strsql += "								); ";
                strsql += " ";
                strsql += "	select  ";
                strsql += "		@pesoCalc = sum(PesoTotal),  ";
                strsql += "		@superfCalc = sum(SuperficieTotal) ";
                strsql += "	from  ";
                strsql += "		TC_PROCESO_PINTURA_CARROS  ";
                strsql += "	where  ";
                strsql += "		FechaIniPintura <= @fechaFinCambio ";
                strsql += "		and FechaIniPintura >= @fechaIniCambio ";
                strsql += "		and CABINA = @cabina; ";
                strsql += " ";
                strsql += "	select @codigo = codigo from T_STOCKS where LOTE = @lote; ";
                strsql += "	select @fCad = FECHA_CAD from TC_PINTURA_LOTES_OTROS_DATOS where lote = @lote; ";
                strsql += " ";
                strsql += "	insert into #tempCalculoLotes values (@lote, @codigo, @fCad, @fechaIniCambio, @fechaFinCambio, @utilizado, @pesoCalc, @superfCalc, @cabina); ";
                strsql += "	fetch next from crs into @lote, @fechaIniCambio, @utilizado, @cabina; ";
                strsql += " ";
                strsql += "END; ";
                strsql += "CLOSE crs; ";
                strsql += "DEALLOCATE crs; ";
                strsql += " ";
                strsql += "select lote as LOTE, codigo AS CÓDIGO, FORMAT (fcad, 'dd/MM/yyyy') as CADUCIDAD, FORMAT (fechaIni, 'dd/MM/yyyy, HH:mm') as [INICIO], FORMAT (fechaFin, 'dd/MM/yyyy, HH:mm') as [FIN], cabina as CABINA, kgUsados as CONSUMO, pesoTotal as [PESO TOTAL (Kg)], superficieTotal as [SUPERFICIE TOTAL (m2)] from #tempCalculoLotes order by fechaIni, lote; ";
                strsql += " ";
                strsql += "drop table #tempCalculoLotes; ";


                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                valores = new DataTable();
                adapter.Fill(valores);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valores;
        }


        public DataTable obtenerDatosMarcasLotes(string desde, string hasta, string cabina)
        {
            DataTable valores = new DataTable();

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "declare @fDesde as datetime = '" + desde + "'; ";
                strsql += "declare @fHasta as datetime = '" + hasta + "'; ";
                strsql += "declare @cabina as int = " + cabina + "; ";
                strsql += " ";
                strsql += " ";
                strsql += "select  ";
                strsql += "	cm.IdCarro as CARRO,  ";
                strsql += "	cm.Marca AS MARCA,  ";
                strsql += "	pl.CODLANZA as PROYECTO,  ";
                strsql += "	cm.Packing AS PACKING, ";
                strsql += "	cm.Contenedor AS CONTENEDOR, ";
                strsql += "	cm.Paquete AS PAQUETE,  ";
                strsql += "	cm.Subpaquete AS SUBPAQUETE,  ";
                strsql += "	cm.cantidadCarrro AS CANTIDAD, ";
                strsql += "	cm.Peso AS [PESO (Kg)],  ";
                strsql += "	cm.Superficie AS [SUPERFICIE (m2)] ";
                strsql += "from  ";
                strsql += "	TC_PROCESO_PINTURA_CARROS pc  ";
                strsql += "	inner join TC_PROCESO_PINTURA_CARROS_MARCAS cm on cm.IdCarro = pc.IdCarro ";
                strsql += "	LEFT join TC_PACKING_LIST_PAQUETES_MARCAS pm on pm.PACKING = cm.Packing and pm.PAQUETE = cm.Paquete and cm.Marca = pm.MARCA ";
                strsql += "	LEFT join TC_LANZA_PEDLIN pl on pl.NUMPED = pm.PEDIDO and pl.LINEAP = pm.LINEA ";
                strsql += "where  ";
                strsql += "	FechaIniPintura >= @fDesde  ";
                strsql += "	AND FechaFinPintura <= @fHasta  ";
                strsql += "	and CABINA = @cabina ";
                strsql += "	and cm.codemp = '3' ";
                strsql += "order by ";
                strsql += "	CARRO,  ";
                strsql += "	cm.FechaC; ";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                valores = new DataTable();
                adapter.Fill(valores);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valores;
        }


        public DataTable obtenerDatosLote(string lote)
        {
            DataTable valores = new DataTable();

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "select  ";
                strsql += "	st.CODIGO, ";
                strsql += "	st.LOTE, ";
                strsql += "	STOCK , ";
                strsql += "	format (od.FECHA_CAD, 'dd/MM/yyyy') as FECHA_CAD, ";
                strsql += "	art.FAMILIA ";
                strsql += "from  ";
                strsql += "	T_STOCKS st ";
                strsql += "	left join TC_PINTURA_LOTES_OTROS_DATOS od on od.LOTE = st.LOTE ";
                strsql += "	left join T_ARTICULOS art on art.CODIGO = st.CODIGO ";
                strsql += "where  ";
                strsql += "	st.LOTE = '" + lote + "' ";
                strsql += "	and art.FAMILIA = 'PT' ";





                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                valores = new DataTable();
                adapter.Fill(valores);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valores;
        }

        public DataTable obtenerMarcasLote(string lote)
        {
            DataTable valores = new DataTable();

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "DECLARE @inicambio as datetime; ";
                strsql += "declare @cabina as varchar(2); ";
                strsql += "declare @fincambio as datetime; ";
                strsql += " ";
                strsql += "select top 0 * ";
                strsql += "into #tempMarcasPintura ";
                strsql += "from TC_PROCESO_PINTURA_CARROS_MARCAS; ";
                strsql += " ";
                strsql += "DECLARE CRS CURSOR FOR SELECT f_cambio, UBICACION FROM TC_PINTURA_LOTES_CAMBIOS WHERE LOTE = '" + lote + "'; ";
                strsql += " ";
                strsql += "OPEN CRS; ";
                strsql += " ";
                strsql += "FETCH NEXT FROM CRS into @inicambio, @cabina; ";
                strsql += " ";
                strsql += "while @@FETCH_STATUS = 0 ";
                strsql += "begin ";
                strsql += "	set @fincambio = (select top (1) f_cambio from TC_PINTURA_LOTES_CAMBIOS where F_CAMBIO > @inicambio order by F_CAMBIO asc); ";
                strsql += " ";
                strsql += "	insert into #tempMarcasPintura select * from TC_PROCESO_PINTURA_CARROS_MARCAS where IdCarro in (select IdCarro from TC_PROCESO_PINTURA_CARROS where FechaIniPintura >= @inicambio and FechaIniPintura <= @fincambio); ";
                strsql += " ";
                strsql += "	FETCH NEXT FROM CRS into @inicambio, @cabina; ";
                strsql += "end ";
                strsql += " ";
                strsql += "close crs;  ";
                strsql += "deallocate crs; ";
                strsql += " ";
                strsql += "select distinct Packing AS PACKING, Contenedor AS CONTENEDOR, Paquete AS PAQUETE, Subpaquete AS SUBPAQUETE, Marca AS MARCA, cantidadCarrro AS CANTIDAD, Peso AS PESO, Superficie AS SUPERFICIE, format(fechaC, 'dd/MM/yyyy') as FECHA  from #tempMarcasPintura order by FECHA, Packing, Paquete, Subpaquete; ";
                strsql += " ";
                strsql += "drop table #tempMarcasPintura; ";



                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                valores = new DataTable();
                adapter.Fill(valores);

                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valores;
        }

        public void consumirStock(string lote, string kilos)
        {
            DataTable valores = new DataTable();

            string strsql = "";



            SqlCommand comando;

            try
            {

                conexion.Open();

                //Resta de stock de lote
                strsql = "UPDATE T_STOCKS SET STOCK = 0 WHERE LOTE = '" + lote + "' AND CODEMP = '" + empresa + "'";
                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteNonQuery();

                //Resta de stock total de artículo
                strsql = "UPDATE T_ARTICULOS SET STKFIS = (STKFIS - " + kilos + ") WHERE CODIGO = (SELECT CODIGO FROM T_STOCKS WHERE LOTE = '" + lote + "' and CODEMP = '" + empresa + "')";

                comando = new SqlCommand(strsql, conexion);
                comando.ExecuteNonQuery();
                
                conexion.Close();

            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
