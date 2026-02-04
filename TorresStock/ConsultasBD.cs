using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using empresaGlobalProj;


namespace TorresStock
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




        public bool existeCodigo(string codigo)
        {
            bool todoCorrecto = true;
            string paquete = "";

            string sql = "select CODIGO from t_articulos " +
                " where codemp = '" + empresaGlobal.empresaID + "' and codigo = '" + codigo + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paquete = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }

                if (paquete.Equals(""))
                {
                    todoCorrecto = false;
                }
                else
                {
                    todoCorrecto = true;
                }


            }

            return todoCorrecto;

        }


        public bool existePedido(string pedido)
        {
            bool todoCorrecto = true;
            string paquete = "";

            string sql = "select NUMPED from T_ORDTER " +
                " where codemp = '" + empresaGlobal.empresaID + "' and NUMPED = '" + pedido + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paquete = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }

                if (paquete.Equals(""))
                {
                    todoCorrecto = false;
                }
                else
                {
                    todoCorrecto = true;
                }


            }

            return todoCorrecto;

        }


        public DataTable obtenerLineasPedido(string pedido)
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

                strsql = @"SELECT       CODIGO,CTDUP, NUMPED, LINEA
                            FROM            T_ORDTERL
                            WHERE         (CODEMP = '3')  AND (NUMPED = '"+pedido+"') AND (dbo.IME_GetFamilia_Articulo('3',CODIGO) <> 'TO') AND (dbo.IME_GetFamilia_Articulo('3',CODIGO) <> 'MP') AND ((NOT (CODIGO LIKE '%DESARROLLO%')) AND (NOT (CODIGO LIKE '%PINTURA%')) AND (NOT (CODIGO LIKE '%TEXTO%')) AND (NOT (CODIGO LIKE '%PROTOTIPO%')) AND (NOT (CODIGO LIKE '%MATERIAL%')) AND (NOT (CODIGO LIKE '%POR%')))";



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

        public DataTable obtenerLineasEspecificasPedido(string pedido, string lineas)
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

                strsql = @"SELECT       CODIGO,CTDUP, NUMPED, LINEA
                            FROM            T_ORDTERL
                            WHERE         (CODEMP = '3')  AND (NUMPED = '" + pedido + "') AND (LINEA IN ("+lineas+")) AND (dbo.IME_GetFamilia_Articulo('3',CODIGO) <> 'TO') AND (dbo.IME_GetFamilia_Articulo('3',CODIGO) <> 'MP') AND ((NOT (CODIGO LIKE '%DESARROLLO%')) AND (NOT (CODIGO LIKE '%PINTURA%')) AND (NOT (CODIGO LIKE '%TEXTO%')) AND (NOT (CODIGO LIKE '%PROTOTIPO%')) AND (NOT (CODIGO LIKE '%MATERIAL%')) AND (NOT (CODIGO LIKE '%POR%')))";



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



        //Solo una conexion
        public void insertarInformacionComprobacionStock(DataGridView dgCodigoCantidadComprobacion)
        {
                string StrQuery;
                SqlConnection conn = null;
                string linea= "";
                string pedido ="";
                try
                {
                    using ( conn = new SqlConnection(Utils.CD.getConexion()))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = conn;
                            conn.Open();
                            for (int i = 0; i < dgCodigoCantidadComprobacion.Rows.Count; i++)
                            {
                                if (String.IsNullOrEmpty(dgCodigoCantidadComprobacion.Rows[i].Cells["LINEA"].Value.ToString()))
                                {
                                    linea = "-1";
                                }
                                else {
                                    linea = dgCodigoCantidadComprobacion.Rows[i].Cells["LINEA"].Value.ToString();
                                }


                                if (String.IsNullOrEmpty(dgCodigoCantidadComprobacion.Rows[i].Cells["PEDIDO"].Value.ToString()))
                                {
                                    pedido = "-1";
                                }
                                else
                                {
                                    pedido = dgCodigoCantidadComprobacion.Rows[i].Cells["PEDIDO"].Value.ToString();
                                }



                                StrQuery = @"	
	                                        declare @CodEmp varchar(2) = '3'
	                                        declare @Codigo varchar(20) = '" + dgCodigoCantidadComprobacion.Rows[i].Cells["Codigo"].Value.ToString() + @"' 
	                                        declare @CTDCOMPROBACION int = " + dgCodigoCantidadComprobacion.Rows[i].Cells["Cantidad"].Value.ToString() + @" 
	                                        declare @LINEACOMPROBACION int = " + linea + @" 
	                                        declare @NUMPEDCOMPROBACION int = " + pedido + @" 

	                                        declare @Altern varchar(1) = '0'
	                                        declare @Idioma varchar(2) = 'es'
	                                        declare @peso_tornillos int = 0

	                                        SET NOCOUNT ON;	

	                                        Declare @Cantidad float = 1
	                                        Declare @maxNivel int = 20
	                                        Declare @semilla int = 0



	                                        IF OBJECT_ID(N'tempdb..#T_COMPROBACION_STOCK', N'U') IS NOT NULL 
		                                        DROP TABLE #T_COMPROBACION_STOCK
    
                                            SELECT @CodEmp as CodEmp,
			                                        @Altern as Altern,
			                                        0 as ID,
			                                        CODIGO as Codigo_Padre, 
			                                        @Cantidad as Cantidad_Padre, 			
			                                        null as ID_PADRE,
			                                        CODIGO as Codigo_Hijo, 
			                                        @Cantidad as Cantidad_Hijo, 
			                                        CATEGORIA as Categoria_Hijo, 
			                                        FAMILIA as Familia_Hijo, 
			                                        0 as Correlacion,
			                                        0 as STKFIS,
			                                        0 as Nivel,
			                                        @codigo as CODIGOCOMPROBACION,
			                                        @CTDCOMPROBACION as CTDCOMPROBACION,
			                                        @NUMPEDCOMPROBACION as NUMPEDCOMPROBACION,
			                                        @LINEACOMPROBACION as LINEACOMPROBACION
			                                        Into #T_COMPROBACION_STOCK
		                                        FROM T_ARTICULOS
		                                        WHERE CodEmp = @CodEmp 
			                                        AND CODIGO= @codigo
			                                        --in('RUC-3045_09-EXT18','RUIF-45_09-14M','RUC-1020_09-EXT18')

	                                        Declare @Nivel int = 0
                                            WHILE (@Nivel <= @maxNivel) AND (@@ROWCOUNT > 0) BEGIN
                                                SET @Nivel = @Nivel + 1
        
                                                INSERT INTO #T_COMPROBACION_STOCK
                                                SELECT @CodEmp as CodEmp,
				                                        @Altern as Altern,
				                                        @semilla + row_number() over(ORDER BY @maxNivel) as Id,				
				                                        T_ESTRUC.CONJUN as Codigo_Padre, 
				                                        T1.Cantidad_Hijo as Cantidad_Padre, 				
				                                        T1.ID as Id_Padre,
				                                        T_ESTRUC.COMPON as Codigo_Hijo, 				
				                                        T_ESTRUC.CTDENL as Cantidad_Hijo,		--(T1.Cantidad_Hijo * T_ESTRUC.CTDENL) as Cantidad_Hijo, 
				                                        T_ARTICULOS.CATEGORIA as Categoria_Hijo, 
				                                        T_ARTICULOS.FAMILIA Familia_Hijo, 
				                                        T_ESTRUC.CORREL as Correlacion, 
				                                        T_ARTICULOS.STKFIS,
				                                        @Nivel as Nivel,
				                                        T1.CODIGOCOMPROBACION,
				                                        T1.CTDCOMPROBACION,
				                                        t1.NUMPEDCOMPROBACION,
				                                        t1.LINEACOMPROBACION
			                                        FROM #T_COMPROBACION_STOCK T1 INNER JOIN T_ESTRUC 
					                                        ON T1.Codigo_Hijo = T_ESTRUC.CONJUN INNER JOIN T_ARTICULOS 
					                                        ON T_ESTRUC.CodEMP = T_ARTICULOS.CodEMP AND T_ESTRUC.COMPON = T_ARTICULOS.CODIGO
			                                        WHERE (T_ESTRUC.CodEmp = @CodEmp)
					                                        AND (T_ESTRUC.Altern = @Altern)
					                                        AND (T1.Nivel = @Nivel - 1) 
					                                        AND (T1.Familia_Hijo <> 'TO')		--Así no descompone la estructura de los TO
					                                        AND (T_ARTICULOS.FAMILIA <> 'MP')	--Ásí no devuelve la MP por la que está formada un código
					
		                                        set @semilla = @semilla + @@ROWCOUNT
                                            END        
	
	    
	    
	                                        insert into AUX_COMPROBACION_STOCK    
	                                        SELECT 
			                                        '3' AS CODEMP,
			                                        t_1.CODIGOCOMPROBACION as CODIGO_COMPROBACION,			
			                                        t_1.CTDCOMPROBACION as CTD_COMPROBACION,
			                                        t_1.LINEACOMPROBACION as LINEA_COMPROBACION,
			                                        t_1.NUMPEDCOMPROBACION as NUMPED_COMPROBACION,
			                                        t_1.Codigo_Hijo as CODIGO_RAIZ,
			                                        (t_1.Cantidad_Hijo*t_1.Cantidad_Padre* CTDCOMPROBACION) as CantidadTotal,
			                                        t_1.STKFIS as STOCK
		                                        FROM #T_COMPROBACION_STOCK as t_1 Left Outer Join T_ARTICULOS
			                                        on t_1.CodEmp = T_ARTICULOS.CodEMP and t_1.Codigo_Hijo = T_ARTICULOS.CODIGO 
					                                        Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                                        FROM T_ARTCAR
										                                        WHERE (CARACT = '04')
									                                        ) as T_ARTCAR_Long
			                                        on T_ARTICULOS.CodEMP = T_ARTCAR_Long.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Long.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Long.CATEGO
					                                        Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                                        FROM T_ARTCAR
										                                        WHERE (CARACT = '05')
									                                        ) as T_ARTCAR_Area
			                                        on T_ARTICULOS.CodEMP = T_ARTCAR_Area.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Area.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Area.CATEGO
					                                        Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                                        FROM T_ARTCAR
										                                        WHERE (CARACT = '100')
									                                        ) as T_ARTCAR_Fabricable
			                                        on T_ARTICULOS.CodEMP = T_ARTCAR_Fabricable.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Fabricable.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Fabricable.CATEGO		

			                                        --MMM 22_05_19
			                                        Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                                        FROM T_ARTCAR
										                                        WHERE (CARACT = '17')
									                                        ) as T_ARTCAR_Taladrado
			                                        on T_ARTICULOS.CodEMP = T_ARTCAR_Taladrado.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Taladrado.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Taladrado.CATEGO		
                                                    
                                                    --VERSION ANTERIOR
			                                        --WHERE T_1.Familia_Hijo <> 'TO' AND (Codigo_Hijo NOT IN (SELECT Codigo_Padre FROM #T_COMPROBACION_STOCK where nivel <> 0))
                                                    --VERSION ACTUAL
                                                      where Categoria_Hijo <> 'TO' and ((Categoria_Hijo = '92') OR ((Categoria_Hijo <> '92') and ((select dbo.IME_GetCategoria_Articulo ('3' , Codigo_Padre)) <> '92') and (Codigo_Hijo NOT IN (SELECT Codigo_Padre FROM #T_COMPROBACION_STOCK where nivel <> 0))))
		                                        ORDER BY t_1.Nivel, t_1.Codigo_Padre, t_1.Correlacion"; 




                                comm.CommandText = StrQuery;
                                comm.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
        }


        public DataTable obtenerStock()
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

                strsql = @"select [CODIGOS NECESARIOS], [CANTIDAD NECESARIA],STOCK as 'STOCK ACTUAL',(STOCK-[CANTIDAD NECESARIA]) as 'STOCK RESULTANTE'   from(
                           SELECT         CODIGO_RAIZ 'CODIGOS NECESARIOS', SUM(CANTIDAD_TOTAL) as 'CANTIDAD NECESARIA' ,STOCK 
                            FROM            AUX_COMPROBACION_STOCK group by codigo_raiz, stock ) as TablaTotal order by [CODIGOS NECESARIOS]";



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

        public DataTable obtenerStockNecesario()
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

                strsql = @"select [CODIGOS NECESARIOS], [CANTIDAD NECESARIA],STOCK as 'STOCK ACTUAL',(STOCK-[CANTIDAD NECESARIA]) as 'STOCK RESULTANTE'   from(
                            SELECT         CODIGO_RAIZ 'CODIGOS NECESARIOS', SUM(CANTIDAD_TOTAL) as 'CANTIDAD NECESARIA' ,STOCK 
                            FROM            AUX_COMPROBACION_STOCK group by codigo_raiz, stock ) as TablaTotal where STOCK < [CANTIDAD NECESARIA] order by [CODIGOS NECESARIOS]";

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



        public void vaciarTablaAux()
        {
            //Delete from[gg].[dbo].TC_GALV_Cuelgue_Op_Empleado where NumEmpleado = 9999 and IdCuelgue = 247; 
            string sql = " TRUNCATE TABLE AUX_COMPROBACION_STOCK";
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





    }

}
