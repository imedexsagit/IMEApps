using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using empresaGlobalProj;

namespace GestionSoldadura
{
    public partial class Form5 : Form
    {
        string proyect = "";

        public Form5(string proyecto)
        {
            InitializeComponent();
            this.proyect = proyecto;

            //Obtengo las inspecciones realizadas ha etiquetas de ese proyecto y las muestro.

            SqlConnection conexin = null;
            SqlCommand comand = null;
            string strsq;
            string user = Environment.UserName;


            string connString = "Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";
            DataTable table = new DataTable();

            conexin = new SqlConnection(connString);
            conexin.Open();

            SqlDataAdapter adapter;

            strsq = "SELECT     TC_PROYECTOS_ONLINE.PROYECTO, ";
                   strsq += " TC_PROYECTOS_ONLINE.CODIGO,";
                   strsq += " TC_PROYECTOS_ONLINE.ORDFAB,";
                   strsq += " TC_PROYECTOS_ONLINE.CTDLAN,";
                   strsq += " tc_bonol.BONO,";
                   strsq += " T_ORDFABO.OPERAC,";
                   strsq += " pre.numeroPersonalEmpleado AS 'NUMERO EMPLEADO',";
                   strsq += " em.NOMBRE +' '+ em.APELLIDOS as 'EMPLEADO'";
                   strsq += " FROM            TC_PROYECTOS_ONLINE";
                   strsq += " join T_ORDFABO on TC_PROYECTOS_ONLINE.ORDFAB = T_ORDFABO.ORDFAB";
                   strsq += " join tc_bonol on T_ORDFABO.ORDFAB = tc_bonol.ORDFAB";
                   strsq += " join CS_CPP_0713.dbo.MP_TRABAJOCONBONO as tb on tc_bonol.BONO = tb.codBono";
                   strsq += " join CS_CPP_0713.dbo.MJ_PRESENCIAS as pre on tb.mjPresenciaId = pre.id";
                   strsq += " join CS_CPP_0713.dbo.CPP_EMPLEADOS as em on pre.numeroPersonalEmpleado = em.NUMERO_PERSONAL";
                   strsq += " WHERE        (PROYECTO = '"+proyecto+"') and T_ORDFABO.puesto = 951";
 
                          

            comand = new SqlCommand(strsq, conexin);
            adapter = new SqlDataAdapter(comand);

            comand.CommandText = strsq;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

    
            conexin.Close();

            tC_InspeccionEtiquetasDataGridView.DataSource = table;
        }

   
    }
}
