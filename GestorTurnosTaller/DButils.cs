using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using empresaGlobalProj;

namespace GestorTurnosTaller
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

        public DataTable obtenerPersonas(string anio)
        {
            DataTable table = null;

            string strsql = "";

            

            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                // Con CTE permite que la consulta sea más rápida y aparezcan los datos antes a los usuarios, en lugar de la subselect dentro de la select
                strsql += "WITH Presencias AS(SELECT mjp.numeroPersonalEmpleado, SUM(DATEDIFF(HH, (CASE WHEN DATEPART(MINUTE, eFecha) > 25 THEN DATEADD(HOUR, 1, eFecha) ELSE eFecha END), ";
                strsql += "(CASE WHEN DATEPART(MINUTE, sFecha) > 25 THEN DATEADD(HOUR, 1, sFecha) ELSE sFecha END))) AS HorasTrabajadas ";
                strsql += "FROM CS_CPP_0713.dbo.MJ_PRESENCIAS mjp WHERE sFecha IS NOT NULL AND YEAR(eFecha)=" + anio + " GROUP BY mjp.numeroPersonalEmpleado), ";
                strsql += "Programadas AS (SELECT cppt.codEmp, cppt.numPersonal, SUM(espt.DuracionHoras) AS HorasProgramadas FROM CS_CPP_0713.dbo.CPP_TURNOS_TALLER cppt ";
                strsql += "inner join CS_CPP_0713.dbo.CPP_ESPECIFIC_TURNOS espt ON cppt.TURNO=espt.IDTurno ";
                strsql += "WHERE YEAR(cppt.DIA)="+anio+" GROUP BY cppt.numPersonal, cppt.codEmp), "; 
                strsql += "AsuntosPropios AS (SELECT cppi.NUMERO_PERSONAL_EMPLEADO, SUM(DATEDIFF(HH, (CASE WHEN DATEPART(MINUTE, HORA_INICIO) > 25 THEN DATEADD(HOUR, 1, HORA_INICIO) ELSE HORA_INICIO END), "; 
                strsql += "(CASE WHEN DATEPART(MINUTE, HORA_FIN) > 25 THEN DATEADD(HOUR, 1, HORA_FIN) ELSE HORA_FIN END))) AS HorasAP ";
                strsql += "FROM CS_CPP_0713.dbo.CPP_INCIDENCIAS_INTERNAS cppi ";
                strsql += "WHERE cppi.ID_INCIDENCIA=1 AND YEAR(cppi.DIA)="+anio+" "; 
                strsql += "GROUP by cppi.NUMERO_PERSONAL_EMPLEADO) \n";
                strsql += "select NUMERO_PERSONAL as NUM, NOMBRE AS NOMB_OCULTO, APELLIDOS AS AP_OCULTOS, NUMERO_MATRICULA as MATRICULA, CONCAT (NOMBRE, ' ', APELLIDOS) AS NOMBRE, CENTRO, DENOMI as PUESTO,"; 
                strsql += "(\"H.PROGRAMADAS\"-\"H.AP\") AS \"H.PROGRAMADAS\", \"H.TRABAJADAS\", ((\"H.PROGRAMADAS\"-\"H.AP\")-\"H.TRABAJADAS\") AS \"H.RESTANTES\" ";
                //strsql += "CONCAT (APELLIDOS, ', ', NOMBRE, ' (', NUMERO_PERSONAL, '-->', NUMERO_MATRICULA, ')') AS NOMBRE, ";
                strsql += "from (select cpph.CodEmp, dep.DEPARTAMENTO, NUMERO_MATRICULA, cpph.CentroTrabajo as CENTRO, maq.DENOMI, cppe.numero_personal,cppe.nombre, cppe.apellidos, ";
                strsql += "CASE WHEN pr.HorasProgramadas IS NULL THEN 0 ELSE pr.HorasProgramadas END AS \"H.PROGRAMADAS\", ";
                strsql += "CASE WHEN p.HorasTrabajadas IS NULL THEN 0 ELSE p.HorasTrabajadas END AS \"H.TRABAJADAS\", ";
                strsql += "CASE WHEN ap.HorasAP IS NULL THEN 0 ELSE ap.HorasAP END AS \"H.AP\", ";  
                strsql += "ROW_NUMBER() OVER(PARTITION BY cppe.numero_personal ORDER BY cpph.ID) RN ";
                strsql += "from CS_CPP_0713.dbo.CPP_EMPLEADOS cppe ";
                strsql += "left outer join Presencias p ON cppe.NUMERO_PERSONAL=p.numeroPersonalEmpleado ";
                strsql += "left outer join Programadas pr ON cppe.CODEMP=pr.codEmp AND cppe.NUMERO_PERSONAL=pr.numPersonal  ";
                strsql += "left outer join AsuntosPropios ap ON cppe.NUMERO_PERSONAL=ap.NUMERO_PERSONAL_EMPLEADO ";
                strsql += "left outer join CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS cpph on cppe.NUMERO_PERSONAL = cpph.NUMERO_PERSONAL_EMPLEADO and cppe.CODEMP = cpph.CodEmp   ";
                strsql += "left outer join gg.dbo.T_PUESTOS maq on cpph.PUESTO = maq.PUESTO and cpph.CodEmp = maq.CODEMP  ";
                strsql += "left outer join CS_CPP_0713.dbo.IME_ACCESO_DEPTOS_IMEAPPS dep on cpph.ID_SECCION = dep.ID_SECCION and dep.ID_DPTO = cpph.ID_DEPARTAMENTO and cpph.CodEmp = dep.CODEMP ";
                strsql += "where cpph.CodEmp = '3' and cpph.FECHA_BAJA is null ";

                if (empresa != "60")
                {
                    strsql += "AND cppe.NOMBRE NOT LIKE '%made%' AND NUMERO_PERSONAL > 0 AND (cpph.ID_DEPARTAMENTO IN('10', '11', '12', '13', '14') OR NUMERO_PERSONAL = 873 OR NUMERO_PERSONAL = 733)";
                }
                else
                {
                    strsql += "and NUMERO_PERSONAL < 0";
                }

                strsql += "GROUP BY cpph.CodEmp,dep.DEPARTAMENTO,NUMERO_MATRICULA,cpph.CentroTrabajo,maq.DENOMI,cppe.CODEMP,cppe.numero_personal,cppe.nombre,cppe.apellidos,cpph.ID, pr.HorasProgramadas, p.HorasTrabajadas, ap.HorasAP) a ";
                strsql += "where RN = 1  and NUMERO_PERSONAL <> 10 ";

                if (Environment.UserName == "cecilio.perez")
                //if (Environment.UserName == "angel.garcia")
                {
                    strsql += " and centro NOT like 'Santiago%' and DENOMI LIKE 'SOLDADURA' ";
                }

                strsql += "order by  puesto, DEPARTAMENTO, NUMERO_PERSONAL desc ";

                

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

        public void actualizarTurnos(string numPersonal, string turno, DateTime dia, bool borrar)
        {


            string strsql = "";
            string turnoAdd = ""; 



            SqlCommand comando;

            try
            {

                conexion.Open();

                if (!borrar)
                {
                    switch (turno)
                    {
                        case "MAÑANA":
                            turnoAdd = "Turno1";
                            break;
                        case "TARDE":
                            turnoAdd = "Turno2";
                            break;
                        case "NOCHE":
                            turnoAdd = "Turno3";
                            break;
                        case "MAÑANA(10h)":
                            turnoAdd = "Turno4";
                            break;
                        case "TARDE(10h)":
                            turnoAdd = "Turno5";
                            break;
                        case "NOCHE(10h)":
                            turnoAdd = "Turno6";
                            break;
                        default:
                            break;
                    }
                    strsql += "UPDATE CS_CPP_0713.dbo.CPP_TURNOS_TALLER SET ";
                    strsql += "TURNO = '" + turnoAdd + "', FECHAM = GETDATE(), USUMOD = '" + Environment.UserName + "' ";
                    strsql += "WHERE numPersonal = '" + numPersonal + "' and dia = '" + dia.ToString("yyyy/MM/dd") + "' and codEmp = '" + empresa + "'";
                    comando = new SqlCommand(strsql, conexion);

                    if (comando.ExecuteNonQuery() == 0)
                    {
                        strsql = "insert into CS_CPP_0713.dbo.CPP_TURNOS_TALLER (codEmp, numPersonal,DIA,TURNO,USUCRE,FECHAC) values ";
                        strsql += "('" + empresa + "', '" + numPersonal + "', '" + dia.ToString("yyyy/MM/dd") + "','" + turnoAdd + "','" + Environment.UserName + "', GETDATE())";



                        comando = new SqlCommand(strsql, conexion);
                        comando.ExecuteScalar();
                    }
                }
                else
                {
                    strsql += "DELETE FROM CS_CPP_0713.dbo.CPP_TURNOS_TALLER ";
                    strsql += "WHERE numPersonal = '" + numPersonal + "' and dia = '" + dia.ToString("yyyy/MM/dd") + "' and codEmp = '" + empresa + "'";
                    comando = new SqlCommand(strsql, conexion);

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

        public DataTable obtenerTurnosDias(string anio)
        {
            DataTable table = null;

            string strsql = "";
            SqlCommand comando;
            SqlDataAdapter adapter;

            try
            {

                conexion.Open();

                //strsql += "select * from CS_CPP_0713.dbo.CPP_TURNOS_TALLER where codEmp = " + empresa + " and YEAR(DIA) = '" + anio + "'";
                strsql += " select codEmp, numPersonal, DIA, cppt.Turno AS TURNO, USUCRE, FECHAC, USUMOD, FECHAM from CS_CPP_0713.dbo.CPP_TURNOS_TALLER cpptt join CS_CPP_0713.dbo.CPP_ESPECIFIC_TURNOS cppt ON cpptt.TURNO=cppt.IDTurno ";
                strsql +=" where codEmp ="+empresa+" and YEAR(DIA) = '"+anio+"'";
                strsql +=" GROUP BY codEmp, numPersonal, DIA, cppt.Turno, USUCRE, FECHAC, USUMOD, FECHAM";


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

        public void guardarDatos(DataRow row)
        {
            string strsql = "";

            SqlCommand cmd;

            try
            {
                conexion.Open();
                strsql = "UPDATE CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS ";
                strsql += "SET PUESTO = (select top (1) puesto from gg.dbo.T_PUESTOS where DENOMI = '" + row["PUESTO"] + "' and CodEmp = '" + empresa + "') ";
                strsql += "WHERE NUMERO_PERSONAL_EMPLEADO = '" + row["NUM"] + "' and CodEmp = " + empresa + " and FECHA_BAJA is null";
                cmd = new SqlCommand(strsql, conexion);

                cmd.ExecuteScalar();
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
