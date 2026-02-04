using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;


namespace TareasFaltanDatos
{
    class ConsultasBD
    {

        //Métodos
        public void actualizarTabla()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;

            /*
            strsql = @"	declare @codemp	varchar(2) = '3'
	                declare @area		varchar(50) = (select area from ime_pl_area )
	                declare @fecha_inicio	date = (select CAST(GETDATE() as DATE) as FECHA)
	                declare @fecha_fin		date = (select CAST(GETDATE() +6 as DATE) as FECHA)
	                --declare @pedidoPlanificado int = (select num_pedido from ime_pl_proyectos where area = @area and cast(num_pedido as varchar)  = nombre)
	                declare @estado varchar(5) = 'FD'

	                truncate table TC_TAREAS_FALTAN_DATOS

	                INSERT INTO TC_TAREAS_FALTAN_DATOS
	                exec IME_PL_Peso_OT_A_Fabricacion @codemp,@area,@fecha_inicio,@fecha_fin, '', @estado";
             * */

            strsql = @"		declare @codemp	varchar(2) = '3'
	                declare @area		varchar(50) = (select area from ime_pl_area )
	                declare @fecha_inicio	date = (select CAST(GETDATE() as DATE) as FECHA)
	                declare @fecha_fin		date = (select CAST(GETDATE() +6 as DATE) as FECHA)
	                --declare @pedidoPlanificado int = (select num_pedido from ime_pl_proyectos where area = @area and cast(num_pedido as varchar)  = nombre)
	                declare @estado varchar(5) = 'FD'

	                truncate table TC_TAREAS_FALTAN_DATOS

			   IF OBJECT_ID(N'tempdb..#T_PROYECTOS_ONLINE_AUX_OF_BONOS', N'U') IS NOT NULL 
					DROP TABLE #tabla_aux_faltan_datos

				create table #tabla_aux_faltan_datos(
					pedido			int,
					cliente			varchar(255),
					tarea			varchar(255),
					fecha_fin		date,
					linea			int,
					descripcion		varchar(255),
					serie			varchar(50),
					precio_total	float,
					precio_kg		float,
					peso_total		float,
					tipo			varchar(20),
				)



	                INSERT INTO #tabla_aux_faltan_datos
	                exec IME_PL_Peso_OT_A_Fabricacion @codemp,@area,@fecha_inicio,@fecha_fin, '', @estado

					insert into TC_TAREAS_FALTAN_DATOS
					select pedido,cliente,tarea,#tabla_aux_faltan_datos.fecha_fin,linea,#tabla_aux_faltan_datos.descripcion,#tabla_aux_faltan_datos.serie,precio_total,precio_kg,peso_total,tipo,fechaNecesaria,fechaPrevista from #tabla_aux_faltan_datos
					join IME_PL_Tareas on #tabla_aux_faltan_datos.pedido = IME_PL_Tareas.Num_Pedido and #tabla_aux_faltan_datos.tarea = IME_PL_Tareas.descripcion 
				    join IME_PL_Lineas_Pedido on IME_PL_Tareas.ID_Tarea = IME_PL_Lineas_Pedido.ID_Tarea and IME_PL_Lineas_Pedido.Num_Linea_Pedido = #tabla_aux_faltan_datos.linea";


            using (conexion = new SqlConnection(Utils.CD.getConexion()))
            {
                comando = new SqlCommand(strsql, conexion);
                conexion.Open();

                int filasEfectadas = (int)comando.ExecuteNonQuery();
                if (filasEfectadas < 1)
                {
                    Console.WriteLine("No se ha realizado la actualizacion");
                    // todoCorrecto = false;
                }
                Console.WriteLine("actualizacion aplicada");

            }

            //return todoCorrecto;

        }

        public DataTable obtenerPedidosFD()
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

                strsql = @"	select pedido, cliente, convert(varchar, max(fecha_fin), 103) as 'FECHA FIN', sum(precio_total) as '€', sum(peso_total) as Kg from TC_TAREAS_FALTAN_DATOS group by pedido,cliente order by pedido";

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

        public DataTable obtenerTareas(string pedido, string clientes)
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

                strsql = "	select tarea, sum(precio_total) as '€', sum(peso_total) from TC_TAREAS_FALTAN_DATOS where pedido = '" + pedido + "' and cliente = '" + clientes + "' group by tarea";

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

        public DataTable obtenerLineas(string pedido, string clientes, string tarea)
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

                strsql = "	select linea,descripcion,serie,precio_total,precio_kg,peso_total,convert(varchar, fechaNecesaria, 103) as fechaNecesaria,convert(varchar, fechaPrevista, 103) as fechaPrevista from TC_TAREAS_FALTAN_DATOS where pedido = '" + pedido + "' and cliente = '" + clientes + "' and  tarea = '" + tarea + "'";
                
                /*
                strsql = @"	select linea,TC_TAREAS_FALTAN_DATOS.descripcion,TC_TAREAS_FALTAN_DATOS.serie,precio_total,precio_kg,peso_total,fechaNecesaria,fechaPrevista from TC_TAREAS_FALTAN_DATOS 
	            join IME_PL_Tareas on TC_TAREAS_FALTAN_DATOS.pedido = IME_PL_Tareas.Num_Pedido and TC_TAREAS_FALTAN_DATOS.tarea = IME_PL_Tareas.descripcion 
	            join IME_PL_Lineas_Pedido on IME_PL_Tareas.ID_Tarea = IME_PL_Lineas_Pedido.ID_Tarea and IME_PL_Lineas_Pedido.Num_Linea_Pedido = TC_TAREAS_FALTAN_DATOS.linea
	            where  pedido = '" + pedido + "' and cliente = '" + clientes + "' and  tarea = '" + tarea + "'";
                */

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


        public  bool existeFechaNecesaria(string pedido, string cliente, string tarea, string linea)
        {
            bool todoCorrecto = true;
            string codigo = "";

            string sql = "select fechaNecesaria from TC_TAREAS_FALTAN_DATOS " +
                "where pedido = '" + pedido + "' and cliente = '" + cliente + "' and  tarea = '" + tarea + "' and linea = '" + linea + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        codigo = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }

                if (codigo.Equals(""))
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

        public String obtenerFechaNecesaria(string pedido, string cliente, string tarea, string linea)
        {
            string codigo = "";

            string sql = "select fechaNecesaria from TC_TAREAS_FALTAN_DATOS " +
                "where pedido = '" + pedido + "' and cliente = '" + cliente + "' and  tarea = '" + tarea + "' and linea = '" + linea + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        codigo = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                    }
                }

            }

            return codigo;

        }

        public String obtenerFechaPrevista(string pedido, string cliente, string tarea, string linea)
        {
            string fecha = "";

            string sql = "select fechaPrevista from TC_TAREAS_FALTAN_DATOS " +
                "where pedido = '" + pedido + "' and cliente = '" + cliente + "' and  tarea = '" + tarea + "' and linea = '" + linea + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fecha = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                    }
                }

            }

            return fecha;

        }


        public DataTable obtenerInfoActualizar(string pedido, string clientes, string tarea, string linea)
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

                strsql = @"	select IME_PL_Tareas.ID_Tarea,linea
                            from TC_TAREAS_FALTAN_DATOS
                            join IME_PL_Tareas on TC_TAREAS_FALTAN_DATOS.pedido = IME_PL_Tareas.Num_Pedido and TC_TAREAS_FALTAN_DATOS.tarea = IME_PL_Tareas.descripcion 
                            join IME_PL_Lineas_Pedido on IME_PL_Tareas.ID_Tarea = IME_PL_Lineas_Pedido.ID_Tarea and IME_PL_Lineas_Pedido.Num_Linea_Pedido = TC_TAREAS_FALTAN_DATOS.linea
                             where pedido = '" + pedido + "' and cliente = '" + clientes + "' and  tarea = '" + tarea + "' and linea = '" + linea + "'";

                /*
                strsql = @"	select linea,TC_TAREAS_FALTAN_DATOS.descripcion,TC_TAREAS_FALTAN_DATOS.serie,precio_total,precio_kg,peso_total,fechaNecesaria,fechaPrevista from TC_TAREAS_FALTAN_DATOS 
                join IME_PL_Tareas on TC_TAREAS_FALTAN_DATOS.pedido = IME_PL_Tareas.Num_Pedido and TC_TAREAS_FALTAN_DATOS.tarea = IME_PL_Tareas.descripcion 
                join IME_PL_Lineas_Pedido on IME_PL_Tareas.ID_Tarea = IME_PL_Lineas_Pedido.ID_Tarea and IME_PL_Lineas_Pedido.Num_Linea_Pedido = TC_TAREAS_FALTAN_DATOS.linea
                where  pedido = '" + pedido + "' and cliente = '" + clientes + "' and  tarea = '" + tarea + "'";
                */

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

        public void actualizarNullFechaNecesaria(string idTarea, string linea)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                //MRP
                strSql = "update IME_PL_Lineas_Pedido ";
                strSql += "		set fechaNecesaria = NULL ";
                strSql += "			WHERE ID_Tarea = " + idTarea + "  and Num_Linea_Pedido =" + linea + " ";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la fecha " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public void actualizarNullFechaPrevista(string idTarea, string linea)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                //MRP
                strSql = "update IME_PL_Lineas_Pedido ";
                strSql += "		set fechaPrevista = NULL ";
                strSql += "			WHERE ID_Tarea = " + idTarea + "  and Num_Linea_Pedido =" + linea + " ";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la fecha " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }



        public  void actualizarFechaNecesaria(string idTarea, string linea, string fechaNecesaria)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                //MRP
                strSql = "update IME_PL_Lineas_Pedido "; 
                strSql += "		set fechaNecesaria = convert(datetime, '" + fechaNecesaria + "' , 103) ";
                strSql += "			WHERE ID_Tarea = " + idTarea + "  and Num_Linea_Pedido =" + linea + " ";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la fecha: " + fechaNecesaria + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public void actualizarFechaPrevista(string idTarea, string linea, string fechaPrevista)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                //MRP
                strSql = "update IME_PL_Lineas_Pedido ";
                strSql += "		set fechaPrevista = convert(datetime, '" + fechaPrevista + "' , 103) ";
                strSql += "			WHERE ID_Tarea = " + idTarea + "  and Num_Linea_Pedido =" + linea + " ";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la fecha: " + fechaPrevista + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }





        public bool existeFechaPrevista(string pedido, string cliente, string tarea, string linea)
        {
            bool todoCorrecto = true;
            string codigo = "";

            string sql = "select fechaPrevista from TC_TAREAS_FALTAN_DATOS " +
                "where pedido = '" + pedido + "' and cliente = '" + cliente + "' and  tarea = '" + tarea + "' and linea = '" + linea + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        codigo = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }

                if (codigo.Equals(""))
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

        public int comprobarTabla()
        {
            int numeroFilas = 0;

            string sql = "select count(*) from TC_TAREAS_FALTAN_DATOS";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        numeroFilas = Convert.ToInt32(reader[0].ToString());
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                    }
                }

            }

            return numeroFilas;

        }



        public void vaciarTabla()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;

            strsql = @"truncate table TC_TAREAS_FALTAN_DATOS";


            using (conexion = new SqlConnection(Utils.CD.getConexion()))
            {
                comando = new SqlCommand(strsql, conexion);
                conexion.Open();

                int filasEfectadas = (int)comando.ExecuteNonQuery();
                if (filasEfectadas < 1)
                {
                    Console.WriteLine("No se ha realizado la eliminacion");
                    // todoCorrecto = false;
                }
                Console.WriteLine("eliminacion aplicada");

            }

            //return todoCorrecto;

        }



    }
}
