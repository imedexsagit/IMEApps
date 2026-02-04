using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace GestionVacaciones
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

        public DataTable obtenerPersonas(string año, bool todo, string usuario)
        {
            DataTable table = null;

            string strsql = "";

            

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();


                switch (empresa)
                {
                    case "3":
                        usuario += "@imedexsa.es";
                        break;
                    case "60":
                        usuario += "@madetower.es";
                        break;
                    default:
                        break;
                }

                strsql += "select DIAS_VACACIONES as VAC, DIAS_VACAC_ANTERIOR as VAA, CENTRO, DEPARTAMENTO, DENOMI as PUESTO, NUMERO_PERSONAL, NOMBRE, APELLIDOS, ";
                strsql += "(select count(*) FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS where NUMERO_PERSONAL_EMPLEADO = NUMERO_PERSONAL and year(dia) = '" + año + "' and (ID_INCIDENCIA = 6 or ID_INCIDENCIA = 24)) as DIAS_VACACIONES_UTILIZADOS,  (DIAS_VACACIONES + DIAS_VACAC_ANTERIOR) - (select count(*) FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS where NUMERO_PERSONAL_EMPLEADO = NUMERO_PERSONAL and year(dia) = '" + año + "' and (ID_INCIDENCIA = 6 or ID_INCIDENCIA = 24)) as RESTANTES_VAC, ";
                strsql += "DIAS_AUNTOS_PROPIOS AS HORAS_AP ";
                //strsql += ", (select case when (select SUM(DATEPART(HOUR, HORA_FIN - HORA_INICIO)) FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS WHERE NUMERO_PERSONAL_EMPLEADO = NUMERO_PERSONAL AND YEAR(DIA) = '" + año + "' AND CodEmp = " + empresa + ") is null then 0 else (select SUM(DATEPART(HOUR, HORA_FIN - HORA_INICIO)) FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS WHERE NUMERO_PERSONAL_EMPLEADO = NUMERO_PERSONAL AND YEAR(DIA) = '" + año + "' AND CodEmp = " + empresa + ") end) as HORAS_AP_USADAS ";
                strsql += "from (select cpph.CodEmp, dep.DEPARTAMENTO ,cpph.CentroTrabajo as CENTRO, CPPH.DIAS_VACACIONES, cpph.DIAS_VACAC_ANTERIOR, cpph.DIAS_AUNTOS_PROPIOS, maq.DENOMI, cppe.numero_personal, cppe.nombre, cppe.apellidos, ROW_NUMBER() OVER(PARTITION BY cppe.numero_personal ORDER BY cpph.ID) RN ";
                strsql += "from CS_CPP_0713.dbo.CPP_EMPLEADOS cppe  ";
                strsql += "left outer join CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS cpph  ";
                strsql += "on cppe.NUMERO_PERSONAL = cpph.NUMERO_PERSONAL_EMPLEADO and cppe.CODEMP = cpph.CodEmp ";
                strsql += "left outer join gg.dbo.T_PUESTOS maq  ";
                strsql += "on cpph.PUESTO = maq.PUESTO and cpph.CodEmp = maq.CODEMP  ";
                strsql += "left outer join CS_CPP_0713.dbo.IME_ACCESO_DEPTOS_IMEAPPS dep ";
                strsql += "on cpph.ID_SECCION = dep.ID_SECCION and dep.ID_DPTO = cpph.ID_DEPARTAMENTO and cpph.CodEmp = dep.CODEMP ";

                if (todo)
                {
                    strsql += "where (cpph.CodEmp = '" + empresa + "' and cpph.FECHA_BAJA is null ";
                }
                else
                {
                    strsql += "where cpph.CodEmp = '" + empresa + "' and dep.Email_Responsable = '" + usuario + "' AND cpph.FECHA_BAJA is null ";
                }

                if (empresa != "60")
                {
                    strsql += "and cppe.NOMBRE not like '%made%' and cpph.CodEmp = '3' and cpph.NUMERO_PERSONAL_EMPLEADO > 0";
                }
                else
                {
                    strsql += ") or (cpph.CodEmp = '3' and cpph.NUMERO_PERSONAL_EMPLEADO < 0";
                }

                if (todo) strsql += ')';

                strsql += ") a  ";

                
                strsql += "where RN = 1  ";
                strsql += "order by  DEPARTAMENTO, NUMERO_PERSONAL, puesto desc";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);


                /*foreach (DataRow row in table.Rows)
                {
                    strsql = "exec imedexsa_intranet.dbo.Horas_AP_Trabajador '" + año + "', " + row["NUMERO_PERSONAL"].ToString() +", " + empresa;
                    comando = new SqlCommand(strsql, conexion);
                    row["HORAS_AP_USADAS"] = comando.ExecuteScalar();
                }*/

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

        

        public bool esFestivo(DateTime dia)
        {
            bool fiesta = false;
            try
            {
                conexion.Open();
                string strsql = "";
                strsql = "select cast(dia_festivo as date) from CS_CPP_0713.dbo.CPP_DIAS_FESTIVOS df ";
                strsql += "join CS_CPP_0713.dbo.CPP_CALENDARIOS_LABORALES cl on df.ID_CALENDARIO_LABORAL = cl.ID";
                strsql += " where cast(dia_festivo as date) = cast('" + dia.ToString("yyyy/MM/dd") + "' as date) and cl.CODEMP = " + empresa;
                SqlCommand cmd2 = new SqlCommand(strsql, conexion);
                cmd2.CommandTimeout = 1200;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
                cmd2.CommandText = strsql;


                var result = cmd2.ExecuteScalar();

                fiesta = result != null;
                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return fiesta;
        }

        public bool esAP(DateTime dia, string numpersonal)
        {
            bool AP = false;
            try
            {
                conexion.Open();
                string strsql = "";
                strsql+= "select * from CS_CPP_0713.dbo.CPP_AsuntosPropios_Peticiones where datediff(day, fechaSolicitada, '" + dia.ToString("yyyy/MM/dd");
                strsql+= "') = 0 and Estado not like 'Rechazado' and numPersonal = '" + numpersonal + "'";
                SqlCommand cmd2 = new SqlCommand(strsql, conexion);
                cmd2.CommandTimeout = 1200;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
                cmd2.CommandText = strsql;

                if (numpersonal == "1490" && dia.Day == 6 && dia.Month == 12)
                {
                    Console.WriteLine("test");
                }


                var result = cmd2.ExecuteScalar();

                AP = result != null;
                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return AP;
        }

        public DataTable obtenerPeticionesAP(string año)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "select ap.numPersonal, cast (ap.fechaSolicitada as date) as FECHA, ap.Estado, numHoras from CS_CPP_0713.dbo.CPP_AsuntosPropios_Peticiones ap ";
                strsql += "where /*ap.Estado not like 'Rechazado' and*/ year(fechaSolicitada) = '" + año + "'";

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

        public DataTable obtenerPeticionesVacac(string año)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "select va.numPersonal, cast (va.fechaInicio as date) as FECHAINI, cast (va.fechaFin as date) as FECHAFIN, va.Estado, (select top (1) h.PUESTO from CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS h where h.NUMERO_PERSONAL_EMPLEADO = numPersonal and FECHA_BAJA is null) as PUESTO from CS_CPP_0713.dbo.CPP_AsuntosVacaciones_Peticiones va ";
                strsql += "where /*va.Estado not like 'Rechazado' and*/ (year(fechaInicio) = '" + año + "' or year(fechaFin) = '" + año + "') order by numPersonal desc";

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

        public List<DateTime> obtenerFestivos(string año)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "select cast(dia_festivo as date) as FECHA from CS_CPP_0713.dbo.CPP_DIAS_FESTIVOS df ";
                strsql += "join CS_CPP_0713.dbo.CPP_CALENDARIOS_LABORALES cl on df.ID_CALENDARIO_LABORAL = cl.ID ";
                strsql += "where year(df.DIA_FESTIVO) = '" + año + "' and cl.CODEMP = " + empresa;

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


            return table.AsEnumerable().Select(r => r.Field<DateTime>("FECHA")).ToList();

        }


        public DataTable obtenerVacas(string año)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "select vac.numPersonal, vac.fechaInicio, vac.numDias ";
                strsql += "from CS_CPP_0713.dbo.CPP_AsuntosVacaciones_Peticiones vac ";
                strsql += "where year(fechaInicio) = '" + año + "' and vac.codemp =" + empresa;
                strsql += " order by numPersonal, fechaInicio";

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

        public List<DateTime> vacasPorEmpleado(string numpersonal, string año)
        {

            string strsql = "";
            DataTable table = null;

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "SELECT       cast(DIA as date) AS FECHA ";
                strsql += "FROM            CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS ";
                strsql += "WHERE        NUMERO_PERSONAL_EMPLEADO = '" + numpersonal + "' and CodEmp = "+ empresa +" and (ID_INCIDENCIA = 6 or ID_INCIDENCIA = 24) and YEAR(dia) = " + año;
                strsql += " order by dia";

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

            return table.AsEnumerable().Select(r => r.Field<DateTime>("FECHA")).ToList(); ;
        }

        public DataTable obtenerIncidencias(string año)
        {
            DataTable table = null;

            string strsql = "";



            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                strsql += "SELECT       NUMERO_PERSONAL_EMPLEADO, cast(DIA as date) as FECHA, ID_INCIDENCIA, GN.nombre ";
                strsql += "FROM            CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS II ";
                strsql += "left outer join CS_CPP_0713.dbo.GN_INCIDENCIAS GN ON II.ID_INCIDENCIA = GN.id ";
                strsql += "WHERE       II.CodEmp = " + empresa + "and NUMERO_PERSONAL_EMPLEADO > 0 and YEAR(dia) =" + año + "  and NUMERO_PERSONAL_EMPLEADO in (select NUMERO_PERSONAL_EMPLEADO from CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS where FECHA_BAJA is null) ";
                strsql += " order by NUMERO_PERSONAL_EMPLEADO";

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

        public int diasUtilizados(string numpersonal, string año)
        {
            
            int dias = 0;
            DataTable table = null;
            string strsql = "";



            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                strsql += "select count(*) as count FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS ";
                strsql += "WHERE NUMERO_PERSONAL_EMPLEADO = '" + numpersonal + "' and YEAR(DIA) = '" + año + "' and (ID_INCIDENCIA = 6 or ID_INCIDENCIA = 24)";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

                table = new DataTable();
                adapter.Fill(table);

                dias = Convert.ToInt32(table.Rows[0][0].ToString());

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dias;

        }

        public void actualizarPeticion(string tipo, string estado, string numpersonal, DateTime dia, string idjornada, DateTime horaInicio, DateTime horaFinal, bool diacompleto)
        {
            conexion.Open();
            try
            {
                string strsql = "";
                int tipoIncidencia = 0;
                int tipoNum = 1;
                SqlCommand cmd = new SqlCommand(strsql, conexion);

                if (tipo != "VAC" && tipo != "V")
                {
                    strsql += "select id from CS_CPP_0713.dbo.GN_INCIDENCIAS ";
                    strsql += "where CodEmp = " + empresa + " and nombre = '" + tipo + "'";
                    cmd = new SqlCommand(strsql, conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        tipoIncidencia = reader.GetInt32(0);
                    }
                    else
                    {
                        tipoIncidencia = 1;
                    }

                    strsql = "";
                    strsql += "UPDATE CS_CPP_0713.dbo.CPP_AsuntosPropios_Peticiones set Estado = '" + estado + "', fechaRevision = GETDATE() ";
                    strsql += "WHERE numPersonal = '" + numpersonal + "' and fechaSolicitada = '" + dia.Date.ToString("yyyy/MM/dd") + "' and codemp = " + empresa;

                    if (!diacompleto)
                    {
                        tipoNum = 0;
                    }

                    cmd = new SqlCommand(strsql, conexion);
                    cmd.ExecuteScalar();

                }
                else 
                {
                    strsql += "select case when ";
                    strsql += "((select count(*) as count FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS WHERE NUMERO_PERSONAL_EMPLEADO = '" + numpersonal + "' and YEAR(DIA) = '" + dia.Year + "' and (ID_INCIDENCIA = 24)) ";
                    strsql += "< (select DIAS_VACAC_ANTERIOR from CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS where NUMERO_PERSONAL_EMPLEADO = '" + numpersonal + "' AND FECHA_BAJA IS NULL)) ";
                    strsql += "then 24 else 6 end ";

                    cmd = new SqlCommand(strsql, conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        tipoIncidencia = reader.GetInt32(0);
                    }
                    else
                    {
                        tipoIncidencia = 6;
                    }

                    strsql = "SELECT * FROM CS_CPP_0713.dbo.CPP_AsuntosVacaciones_Peticiones ";
                    strsql += "WHERE numPersonal = '" + numpersonal + "' and fechaInicio <= '" + dia.Date.ToString("yyyy/MM/dd") + "' and fechaFin >= '" + dia.Date.ToString("yyyy/MM/dd") + "' and codemp = " + empresa;

                    cmd = new SqlCommand(strsql, conexion);
                    SqlDataReader reader2 = cmd.ExecuteReader();

                    if (reader2.HasRows)
                    {
                        strsql = "";
                        strsql += "UPDATE CS_CPP_0713.dbo.CPP_AsuntosVacaciones_Peticiones set Estado = '" + estado + "', fechaRevision = GETDATE() ";
                        strsql += "WHERE numPersonal = '" + numpersonal + "' and fechaInicio <= '" + dia.Date.ToString("yyyy/MM/dd") + "' and fechaFin >= '" + dia.Date.ToString("yyyy/MM/dd") + "' and codemp = " + empresa;

                    }
                    else
                    {
                        strsql = "";
                        strsql = "INSERT INTO CS_CPP_0713.dbo.CPP_AsuntosVacaciones_Peticiones (codemp, numPersonal, mjPresenciaID, fechaSolicitud, fechaInicio, fechaFin, numDias, Estado, fechaRevision, Anulado, fechaAnulacion, usu_anulador) ";
                        strsql += "select '" + empresa + "', " + numpersonal + ", 1, getdate(), CONVERT(DATETIME, '" + dia.Date.ToString("dd/MM/yyyy") + "', 103), CONVERT(DATETIME, '" + dia.Date.ToString("dd/MM/yyyy") + "', 103), 1, 'Aceptado', NULL, 0, NULL, NULL";
                    
                    }

                    
                    cmd = new SqlCommand(strsql, conexion);
                    cmd.ExecuteScalar();
                }


                strsql = "";
                if (estado == "Aceptado")
                {
                    strsql += "INSERT INTO CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS (NUMERO_PERSONAL_EMPLEADO, ID_JORNADA_LABORAL, ID_INCIDENCIA, HORA_INICIO, HORA_FIN, DIA, TIPO, CodEmp) ";

                    if (tipo == "AP")
                    {
                        strsql += "VALUES ('" + numpersonal + "', (select top(1) ID from CS_CPP_0713.dbo.CPP_JORNADAS_LABORALES where ID_RANGO_HORARIO in ( Select id_rango_horario from CS_CPP_0713.dbo.cpp_historial_empleados where NUMERO_PERSONAL_EMPLEADO = " + numpersonal + " and FECHA_BAJA is null)), '" + tipoIncidencia + "', "
                            + " (select top (1) HInicio from CS_CPP_0713.dbo.CPP_AsuntosPropios_Peticiones where cast(fechaSolicitada as date) = '" + dia.ToString("yyyy/MM/dd") + "' and numPersonal = " + numpersonal + "), "
                            + " (select top (1) HFin from CS_CPP_0713.dbo.CPP_AsuntosPropios_Peticiones where cast(fechaSolicitada as date) = '" + dia.ToString("yyyy/MM/dd") + "' and numPersonal = " + numpersonal + "), "
                            + " '" + dia.ToString("yyyy/MM/dd") + "', '" + tipoNum + "', '" + empresa + "')";
                    }
                    else
                    {
                        strsql += "VALUES ('" + numpersonal + "', (select top(1) ID from CS_CPP_0713.dbo.CPP_JORNADAS_LABORALES where ID_RANGO_HORARIO in ( Select id_rango_horario from CS_CPP_0713.dbo.cpp_historial_empleados where NUMERO_PERSONAL_EMPLEADO = " + numpersonal + " and FECHA_BAJA is null)), '" + tipoIncidencia + "', '" + horaInicio.ToString("yyyy/MM/dd H:mm:ss") + "', '" + horaFinal.ToString("yyyy/MM/dd H:mm:ss") + "', '" + dia.ToString("yyyy/MM/dd") + "', '" + tipoNum + "', '" + empresa + "')";
                    }

                    
                    cmd = new SqlCommand(strsql, conexion);

                    cmd.ExecuteScalar();
                }
                else if (estado == "Rechazado")
                {
                    strsql += "DELETE FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS ";
                    strsql += "WHERE NUMERO_PERSONAL_EMPLEADO = '" + numpersonal + "' and cast(DIA as date) = '" + dia.Date.ToString("yyyy/MM/dd") + "' and CodEmp = " + empresa;

                    cmd = new SqlCommand(strsql, conexion);

                    cmd.ExecuteScalar();
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public void insertarNotas(string numpersonal, DateTime dia, string notas)
        {
            string strsql = "";



            SqlCommand cmd;

            try
            {
                conexion.Open();

                if (notas != "")
                {
                    strsql += "UPDATE CS_CPP_0713.dbo.CPP_OBSERVACIONES SET notas = '" + notas + "', fechac = GETDATE() ";
                    strsql += " WHERE numpersonal = '" + numpersonal + "' and fechaNota = '" + dia.Date.ToString("yyyy/MM/dd") + "' and usucre = '" + Environment.UserName + "'";

                    cmd = new SqlCommand(strsql, conexion);

                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        strsql = "";

                        strsql += "INSERT INTO CS_CPP_0713.dbo.CPP_OBSERVACIONES (codemp, numpersonal, notas, fechaNota, usucre, fechac) ";
                        strsql += "VALUES ('" + empresa + "', '" + numpersonal + "', '" + notas + "', '" + dia.Date.ToString("yyyy/MM/dd") + "', '" + Environment.UserName + "', GETDATE())";
                        cmd = new SqlCommand(strsql, conexion);

                        cmd.ExecuteScalar();
                    }
                }
                else
                {
                    strsql = "";

                    strsql += "DELETE FROM CS_CPP_0713.dbo.CPP_OBSERVACIONES ";
                    strsql += " WHERE numpersonal = '" + numpersonal + "' and fechaNota = '" + dia.Date.ToString("yyyy/MM/dd") + "' and usucre = '" + Environment.UserName + "'";
                    cmd = new SqlCommand(strsql, conexion);

                    cmd.ExecuteScalar();
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable obtenerNotas(string año)
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                strsql += "select numpersonal, notas, fechanota as FECHA FROM CS_CPP_0713.dbo.CPP_OBSERVACIONES ";
                strsql += "WHERE usucre = '" + Environment.UserName + "' and YEAR(fechanota) = '" + año + "' and codemp = '" + empresa + "'";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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

        public string notaPorDiaEmpleado(string numempleado, DateTime dia)
        {
            string strsql = "";

            SqlCommand cmd;

            string notas = "";

            try
            {
                conexion.Open();

                strsql += "select  notas FROM CS_CPP_0713.dbo.CPP_OBSERVACIONES ";
                strsql += "WHERE usucre = '" + Environment.UserName + "' and fechanota = '" + dia.Date.ToString("yyyy/MM/dd") + "' and codemp = '" + empresa + "'";
                cmd = new SqlCommand(strsql, conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    notas = reader.GetString(0);
                }
                else
                {
                    notas = "";
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return notas;
        }

        public void guardarDatos(DataRow row, bool puesto)
        {
            string strsql = "";

            SqlCommand cmd;

            try
            {
                conexion.Open();
                strsql = "SELECT DIAS_VACACIONES, DIAS_VACAC_ANTERIOR, p.DENOMI, DIAS_AUNTOS_PROPIOS FROM CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS h left outer join gg.dbo.T_PUESTOS p on p.PUESTO = h.PUESTO WHERE NUMERO_PERSONAL_EMPLEADO = '" + row["NUMERO_PERSONAL"] + "' and h.CodEmp = " + empresa + " and FECHA_BAJA is null";
                cmd = new SqlCommand(strsql, conexion);

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if ((reader.GetInt32(0) != Convert.ToInt32(row["VAC"]) || reader.GetInt32(1) != Convert.ToInt32(row["VAA"]) || reader.GetInt32(3) != Convert.ToInt32(row["HORAS_AP"])) || puesto)
                {
                    strsql = "UPDATE CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS ";
                    strsql += "SET  DIAS_VACACIONES = '" + row["VAC"] + "', DIAS_VACAC_ANTERIOR = '" + row["VAA"] + "', DIAS_AUNTOS_PROPIOS = '" + row["HORAS_AP"] + "' ";

                    if (puesto)
                    {
                        strsql += ", PUESTO = (select top (1) puesto from gg.dbo.T_PUESTOS where DENOMI = '" + row["PUESTO"] + "' and CodEmp = '" + empresa + "') ";
                    }

                    strsql += "WHERE NUMERO_PERSONAL_EMPLEADO = '" + row["NUMERO_PERSONAL"] + "' and CodEmp = " + empresa + " and FECHA_BAJA is null";
                    cmd = new SqlCommand(strsql, conexion);

                    cmd.ExecuteScalar();
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable obtenerDptos()
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                strsql += "select distinct departamento FROM CS_CPP_0713.dbo.IME_DEPARTAMENTOS_PERSONAL ";
                strsql += "WHERE codemp = '" + empresa + "' order by departamento asc";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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

        public DataTable obtenerTiposIncidencias()
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                strsql += "select nombre, id from CS_CPP_0713.dbo.GN_INCIDENCIAS ";
                strsql += "WHERE codemp = '" + empresa + "'";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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

        public DataTable obtenerPuestos()
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                /*strsql += "select distinct denomi from CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS cpph left outer join gg.dbo.T_PUESTOS p ";
                strsql += "on p.PUESTO = cpph.PUESTO ";
                strsql += "WHERE p.codemp = '" + empresa + "' and denomi not like '%made%' and denomi not like '%pintu%' and denomi not like '%GEKA2%' order by denomi";*/

                strsql = "select distinct denomi from gg.dbo.T_PUESTOS p ";
                strsql += "WHERE p.codemp = '3' and denomi not like '%made%' and denomi not like '%pintu%' and denomi not like '%GEKA2%' or denomi like 'FICEP 4 (18-36) MADE' order by denomi";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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


        public List<string> obtenerAccesoTotal()
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                strsql += "SELECT usuario FROM gg.dbo.TC_GESTIONVACACIONES_PERMISOS ";
                strsql += "WHERE codemp = '" + empresa + "'";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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

            return table.AsEnumerable().Select(r => r.Field<string>("usuario")).ToList();
        }

        public List<string> obtenerSoloLectura()
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                strsql += "SELECT usuario FROM gg.dbo.TC_GESTIONVACACIONES_SOLOLECTURA ";
                strsql += "WHERE codemp = '" + empresa + "'";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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

            return table.AsEnumerable().Select(r => r.Field<string>("usuario")).ToList();
        }

        public DataTable obtenerFranjasHorarias(string numpersonal)
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                strsql += "SELECT ID, DENOMINACION ";
                strsql += "FROM CS_CPP_0713.dbo.CPP_JORNADAS_LABORALES ";
                strsql += "WHERE CPP_JORNADAS_LABORALES.ID_RANGO_HORARIO = (SELECT ID_RANGO_HORARIO FROM CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS WHERE NUMERO_PERSONAL_EMPLEADO=" + numpersonal + " AND FECHA_BAJA IS NULL)";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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

        public double obtenerHorasAP(int año, string numpersonal)
        {
            string strsql = "";
            double horas = 0;



            SqlCommand comando;

            try
            {

                conexion.Open();

                strsql += "exec imedexsa_intranet.dbo.Horas_AP_Trabajador '" + año + "', " + numpersonal + ", " + empresa;

                comando = new SqlCommand(strsql, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    horas = reader.GetDouble(0);
                }
                else
                {
                    horas = 0;
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return horas;

        }

        public List<string> obtenerHorasAPDia (DateTime dia, string numpersonal)
        {
            List<string> IniFinTotal = new List<string>();

            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                conexion.Open();
                strsql += "SELECT TOP (1) DATEPART (HOUR, HORA_INICIO) , DATEPART (HOUR, HORA_FIN), DATEDIFF(HOUR, HORA_INICIO, HORA_FIN) ";
                strsql += "FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS ";
                strsql += "WHERE DIA = '" + dia.Date.ToString("yyyy-MM-dd") + "' AND NUMERO_PERSONAL_EMPLEADO=" + numpersonal + " AND ID_INCIDENCIA = 1";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

                table = new DataTable();

                if (adapter.Fill(table) <= 0)
                {
                    strsql = "SELECT TOP (1) DATEPART (HOUR, HInicio) , DATEPART (HOUR, HFin), DATEDIFF(HOUR, HInicio, HFin) ";
                    strsql += "FROM CS_CPP_0713.dbo.CPP_AsuntosPropios_Peticiones ";
                    strsql += "WHERE fechaSolicitada = '" + dia.Date.ToString("yyyy-MM-dd") + "' AND numPersonal =" + numpersonal + " AND ESTADO not like 'Rechazado'";
                    cmd = new SqlCommand(strsql, conexion);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                }

                IniFinTotal.Add(table.Rows[0][0].ToString());
                IniFinTotal.Add(table.Rows[0][1].ToString());
                IniFinTotal.Add(table.Rows[0][2].ToString());                


                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return IniFinTotal;
        }

        public void actualizarPeticionesVacas(DateTime inicio, DateTime fin, int numdias, string numPersonal, string estado)
        {
            string strsql = "";



            SqlCommand comando;

            try
            {

                conexion.Open();

                strsql = "";
                strsql += "UPDATE CS_CPP_0713.dbo.CPP_AsuntosVacaciones_Peticiones set Estado = '" + estado + "', fechaRevision = GETDATE() ";
                strsql += "WHERE numPersonal = '" + numPersonal + "' and fechaInicio <= '" + inicio.Date.ToString("yyyy/MM/dd") + "' and fechaFin >= '" + fin.Date.ToString("yyyy/MM/dd") + "' and codemp = " + empresa;

                comando = new SqlCommand(strsql, conexion);

                if (comando.ExecuteNonQuery() <= 0)
                {
                    strsql = "";
                    strsql += "INSERT INTO  CS_CPP_0713.dbo.CPP_AsuntosVacaciones_Peticiones  (codemp, numPersonal, mjPresenciaID, fechaSolicitud, fechaInicio, fechaFin, numDias, Estado, fechaRevision, Anulado) ";
                    strsql += "VALUES(" + empresa + ", " + numPersonal + ", " + 1 + ", GETDATE(), " + inicio.Date.ToString("yyyy/MM/dd") + ", " + fin.Date.ToString("yyyy/MM/dd") + ", " + numdias + ", '" + estado + "', GETDATE(), 'False' )";
                    comando.ExecuteScalar();
                }

                

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public double obtenerHorasPeticionAP(string numpersonal, DateTime dia)
        {
            string strsql = "";
            double horas = 0;



            SqlCommand comando;

            try
            {

                conexion.Open();
                strsql = "select top (1) numHoras from CS_CPP_0713.dbo.CPP_AsuntosPropios_Peticiones where cast(fechaSolicitada as date) = '" + dia.ToString("yyyy/MM/dd") + "' and numPersonal = " + numpersonal;


                comando = new SqlCommand(strsql, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    horas = Convert.ToDouble(reader.GetInt16(0));
                }
                else
                {
                    horas = 0;
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return horas;

        }


        public DataTable obtenerDptosActuales(string usuario)
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                if (empresa == "3")
                {
                    usuario += "@imedexsa.es";
                }
                else
                {
                    usuario += "@madetower.es";
                }
                conexion.Open();
                strsql += "select distinct dep.DEPARTAMENTO from CS_CPP_0713.dbo.IME_ACCESO_DEPTOS_IMEAPPS dep where email_responsable = '" + usuario + "' order by dep.DEPARTAMENTO ASC";
                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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

        public DataTable obtenerPuestosActuales(string usuario)
        {
            DataTable table = null;
            string strsql = "";

            SqlCommand cmd;
            SqlDataAdapter adapter;

            try
            {
                if (empresa == "3")
                {
                    usuario += "@imedexsa.es";
                }
                else
                {
                    usuario += "@madetower.es";
                }

                conexion.Open();
                strsql += "select distinct denomi from CS_CPP_0713.dbo.CPP_EMPLEADOS cppe ";
                strsql += "left outer join CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS cpph on cppe.NUMERO_PERSONAL = cpph.NUMERO_PERSONAL_EMPLEADO and cppe.CODEMP = cpph.CodEmp ";
                strsql += "left outer join gg.dbo.T_PUESTOS maq  on cpph.PUESTO = maq.PUESTO and cpph.CodEmp = maq.CODEMP ";
                strsql += "left outer join CS_CPP_0713.dbo.IME_ACCESO_DEPTOS_IMEAPPS dep on cpph.ID_SECCION = dep.ID_SECCION and dep.ID_DPTO = cpph.ID_DEPARTAMENTO and cpph.CodEmp = dep.CODEMP ";
                strsql += "where cpph.CodEmp = '3' and dep.Email_Responsable = '" + usuario + "' and cpph.FECHA_BAJA is null";

                if (empresa != "60")
                {
                    strsql += " and denomi not like '%made%' and denomi not like '%pintu%' and denomi not like '%GEKA2%' order by denomi";
                }

                cmd = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(cmd);

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
    }
}
