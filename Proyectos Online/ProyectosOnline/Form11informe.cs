using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Globalization;



namespace ProyectosOnLine
{
    public partial class Form11informe : Form

        
    {
        int angularGlobal = 0;
        int chapaGlobal = 0;
        int cjsGlobal = 0;
        string proyectoGlobal = "", nombreProyectoGlobal="";

        int offsetCabecera = 0;

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public Form11informe(String proyecto, int angulares, int chapa , int cjsold, string nombreProyecto)
        {
            InitializeComponent();
             SqlConnection conexion;
            conexion = new SqlConnection(Utils.CD.getConexion());

            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;

            angularGlobal = angulares;
            chapaGlobal = chapa;
            cjsGlobal = cjsold;

            proyectoGlobal = proyecto;
            nombreProyectoGlobal = nombreProyecto;


           
            if (angulares == 1)
            {
                try
                {

                    conexion.Open();

                    strsql = "";
                    string semana = "";
                    if (nombreProyecto == "-")
                    {
                        semana = "SEMANAL";
                    }
                    
                        strsql = strsql + " EXEC datosCaminosProyectos" + semana + " '" + proyecto + "', '" + angulares + "','" + chapa + "','" + cjsold + "'";
                    


                    comando = new SqlCommand(strsql, conexion);

                    comando.CommandTimeout = 900000;

                    adapter = new SqlDataAdapter(comando);
                    table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;

                    /*button1.Visible = true;
                    button2.Visible = false;
                    button3.Visible = false;*/

                    conexion.Close();

                }
                catch (Exception ex)
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Close();

                    MessageBox.Show(ex.Message, "ERROR AL CONSULTAR LOS DATOS. Consulta con el departamento de informática.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (chapa == 1)
            {
                try
                {

                    conexion.Open();

                    strsql = "";
                    string semana = "";
                    if (nombreProyecto == "-")
                    {
                        semana = "SEMANAL";
                    }

                    strsql = strsql + " EXEC datosCaminosProyectosChapa" + semana + " '" + proyecto + "','" + angulares + "','" + chapa + "','" + cjsold + "'";

                    comando = new SqlCommand(strsql, conexion);
                    comando.CommandTimeout = 900000;
                    adapter = new SqlDataAdapter(comando);
                    table = new DataTable();
                    adapter.Fill(table);

                    /*button1.Visible = false;
                    button2.Visible = true;
                    button3.Visible = false;*/

                    dataGridView1.DataSource = table;

                    conexion.Close();

                }
                catch (Exception ex)
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Close();

                    MessageBox.Show(ex.Message, "ERROR AL CONSULTAR LOS DATOS. Consulta con el departamento de informática.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            if (cjsold == 1)
            {
                try
                {

                    conexion.Open();

                    strsql = "";
                    string semana = "";
                    if (nombreProyecto == "-")
                    {
                        semana = "SEMANAL";
                    }

                    strsql = strsql + " EXEC datosCaminosProyectoscjSold" + semana + " '" + proyecto + "','" + angulares + "','" + chapa + "','" + cjsold + "'";


                    comando = new SqlCommand(strsql, conexion);
                    comando.CommandTimeout = 90000;
                    adapter = new SqlDataAdapter(comando);
                    table = new DataTable();
                    adapter.Fill(table);

                    /*button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = true;*/

                    dataGridView1.DataSource = table;

                    conexion.Close();

                }
                catch (Exception ex)
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Close();

                    MessageBox.Show(ex.Message, "ERROR AL CONSULTAR LOS DATOS. Consulta con el departamento de informática.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Creo el excel
            Cursor.Current = Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range RangeCabecera;

            offsetCabecera = 0;

            
            if(angularGlobal == 1){


                int i = 2;

                if (nombreProyectoGlobal == "-")
                {
                    offsetCabecera += 1;

                    DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    Calendar cal = new CultureInfo("es-ES").Calendar;
                    int week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                    Worksheet.Cells[1, 1].value = "ANGULARES, TUB-RED, UPNs SEMANA " + week;

                    Worksheet.Cells[i, 1].value = "PROYECTO";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]));
                    RangeCabecera.ColumnWidth = 10;
                }
                else
                {
                    Worksheet.Cells[1, 1].value = "PROYECTO: " + proyectoGlobal + nombreProyectoGlobal;
                }


                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 12+offsetCabecera]));
                RangeCabecera.Merge(true);
                RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                RangeCabecera.Font.Bold = true;
                RangeCabecera.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                RangeCabecera.Font.Size = 18;


                Worksheet.Cells[i, 1 + offsetCabecera].value = "CODIGO";
                     RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1+offsetCabecera]));
                     RangeCabecera.ColumnWidth = 10;

                     Worksheet.Cells[i, 2 + offsetCabecera].value = "PAQUETE";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 14;

                    Worksheet.Cells[i, 3 + offsetCabecera].value = "CTD";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 5;
                    Worksheet.Cells[i, 4 + offsetCabecera].value = "MP";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 7;
                    Worksheet.Cells[i, 5 + offsetCabecera].value = "LONG";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 10;
                    Worksheet.Cells[i, 6 + offsetCabecera].value = "FICEP";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 10;
                    Worksheet.Cells[i, 7 + offsetCabecera].value = "GEKA";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 10;

                    Worksheet.Cells[i, 8 + offsetCabecera].value = "FRES";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 10;
                    Worksheet.Cells[i, 9 + offsetCabecera].value = "PLEG";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 10;
                    Worksheet.Cells[i, 10 + offsetCabecera].value = "P2";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 10;
                    Worksheet.Cells[i, 11 + offsetCabecera].value = "RETALA";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 11 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 10;

                    Worksheet.Cells[i, 12 + offsetCabecera].value = "MANU";
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12 + offsetCabecera]));
                    RangeCabecera.ColumnWidth = 10;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12 + offsetCabecera]));
                    RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                    RangeCabecera.Font.Bold = true;
                    RangeCabecera.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    RangeCabecera.Font.Size = 14;

                 
                foreach (DataGridViewRow row in dataGridView1.Rows) // Recorro los datos
                    {
                    i++;

                    if (nombreProyectoGlobal == "-")
                    {
                        Worksheet.Cells[i, 1].value = row.Cells[0].Value.ToString();
                        Worksheet.Cells[1, 1].Style.WrapText = true;

                        RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]));
                        RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }

                    Worksheet.Cells[i, 1 + offsetCabecera].value = row.Cells[0 + offsetCabecera].Value.ToString();
                    Worksheet.Cells[1, 1 + offsetCabecera].Style.WrapText = true;
                   
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    Worksheet.Cells[i, 2 + offsetCabecera].value = row.Cells[1 + offsetCabecera].Value.ToString();
                    Worksheet.Cells[1, 2 + offsetCabecera].Style.WrapText = true;
                    
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    Worksheet.Cells[i, 3 + offsetCabecera].value = row.Cells[2 + offsetCabecera].Value.ToString();
                    Worksheet.Cells[1, 3 + offsetCabecera].Style.WrapText = true;
                    
                    
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    string nombreMP = "";
                    nombreMP = row.Cells["MP"].Value.ToString();
                    string[] wordsMP = nombreMP.Split('-');
                    if (wordsMP.Length > 0) Worksheet.Cells[i, 4 + offsetCabecera].value = wordsMP[0];
                    else Worksheet.Cells[i, 4 + offsetCabecera].value = "";
                    Worksheet.Cells[1, 4].Style.WrapText = true;
                    
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    Worksheet.Cells[i, 5 + offsetCabecera].value = row.Cells["LONG"].Value.ToString();
                    Worksheet.Cells[1, 5 + offsetCabecera].Style.WrapText = true;
                 
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    Worksheet.Cells[i, 6 + offsetCabecera].value = row.Cells["NOMBRE_FICEP"].Value.ToString();
                    Worksheet.Cells[1, 6 + offsetCabecera].Style.WrapText = true;
                   
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    string nombreGeka = "";
                    nombreGeka = row.Cells["NOMBRE_GEKA"].Value.ToString();
                    string[] wordsGeka = nombreGeka.Split(' ');
                    if (wordsGeka.Length > 1) Worksheet.Cells[i, 7].value = wordsGeka[0] + ' ' + wordsGeka[1];
                    else Worksheet.Cells[i, 7 + offsetCabecera].value = wordsGeka[0];
                    Worksheet.Cells[1, 7 + offsetCabecera].Style.WrapText = true;

                 
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    string nombreFres = "";
                    nombreFres = row.Cells["NOMBRE_FRES"].Value.ToString();
                    string[] wordsFres = nombreFres.Split(' ');
                    if (wordsFres.Length > 1) Worksheet.Cells[i, 8].value = wordsFres[0] + ' ' + wordsFres[1];
                    else Worksheet.Cells[i, 8 + offsetCabecera].value = wordsFres[0];
                    Worksheet.Cells[1, 8 + offsetCabecera].Style.WrapText = true; // ajusto el texto
                    
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    string nombrePleg = "";
                    nombrePleg = row.Cells["NOMBRE_PLEG"].Value.ToString();
                    string[] wordsPleg = nombrePleg.Split(' ');
                    if (wordsPleg.Length > 1) Worksheet.Cells[i, 9 + offsetCabecera].value = wordsPleg[0] + ' ' + wordsPleg[1];
                    else Worksheet.Cells[i, 9 + offsetCabecera].value = wordsPleg[0];

                    Worksheet.Cells[1, 9 + offsetCabecera].Style.WrapText = true;
                    
                  
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    string nombreP2 = "";
                    nombreP2 = row.Cells["NOMBRE_P2"].Value.ToString();
                    string[] wordsP2 = nombreP2.Split(' ');
                    if (wordsP2.Length > 1) Worksheet.Cells[i, 10 + offsetCabecera].value = wordsP2[0] + ' ' + wordsP2[1];
                    else Worksheet.Cells[i, 10 + offsetCabecera].value = wordsP2[0];
                    Worksheet.Cells[1, 10 + offsetCabecera].Style.WrapText = true;
                  
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    string nombreRetala = "";
                    nombreRetala = row.Cells["NOMBRE_RETALA"].Value.ToString();
                    string[] wordsRetala = nombreRetala.Split(' ');
                    if (wordsRetala.Length > 1) Worksheet.Cells[i, 11 + offsetCabecera].value = wordsRetala[0] + ' ' + wordsRetala[1];
                    else Worksheet.Cells[i, 11 + offsetCabecera].value = wordsRetala[0];
                 
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 11]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 11]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    string nombreManu = "";
                    nombreManu = row.Cells["NOMBRE_MANU"].Value.ToString();
                    string[] wordsManu = nombreManu.Split(' ');
                    if (wordsManu.Length > 1) Worksheet.Cells[i, 12 + offsetCabecera].value = wordsManu[0] + ' ' + wordsManu[1];
                    else Worksheet.Cells[i, 12 + offsetCabecera].value = wordsManu[0];
                  
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    if ((i % 2) != 0)
                    {
                        RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12]));
                        RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    }
                        

                    }
  
            
         string RUTA = "C:\\Users\\"+Environment.UserName+"\\Desktop\\INFORMEPROYECTOSONLINE.xlsx";
            Worksheet.SaveAs(RUTA);
            Excel.Quit();

            System.Diagnostics.Process.Start(RUTA);

            Cursor.Current = Cursors.Default;
            }



        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Creo el excel
            Cursor.Current = Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range RangeCabecera;

            /*if (nombreProyectoGlobal == "-")
            {
                offsetCabecera += 1;

                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                Calendar cal = new CultureInfo("es-ES").Calendar;
                int week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                Worksheet.Cells[1, 1].value = "ANGULARES, TUB-RED, UPNs SEMANA " + week;

                Worksheet.Cells[i, 1].value = "PROYECTO";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]));
                RangeCabecera.ColumnWidth = 10;
            }
            else
            {*/
                Worksheet.Cells[1, 1].value = "PROYECTO: " + proyectoGlobal + nombreProyectoGlobal;
            //}


            Worksheet.Cells[1, 1].value = "PROYECTO: " + proyectoGlobal + nombreProyectoGlobal;
            int i = 2;
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1,13]));
            RangeCabecera.Merge(true);
            RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            RangeCabecera.Font.Bold = true;
            RangeCabecera.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            RangeCabecera.Font.Size = 18;

                Worksheet.Cells[i, 1].value = "CODIGO";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]));
                RangeCabecera.ColumnWidth = 9;

                Worksheet.Cells[i, 2].value = "PAQUETE";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]));
                RangeCabecera.ColumnWidth = 12;

                Worksheet.Cells[i, 3].value = "CTD";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]));
                RangeCabecera.ColumnWidth = 5;

                Worksheet.Cells[i, 4].value = "MP";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]));
                RangeCabecera.ColumnWidth = 7;
                Worksheet.Cells[i, 5].value = "LONG";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]));
                RangeCabecera.ColumnWidth = 9;
                Worksheet.Cells[i, 6].value = "PLASM";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]));
                RangeCabecera.ColumnWidth = 9;
                Worksheet.Cells[i, 7].value = "CIZA";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]));
                RangeCabecera.ColumnWidth = 9;
                Worksheet.Cells[i, 8].value = "PUNZ";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]));
                RangeCabecera.ColumnWidth = 9;

                Worksheet.Cells[i, 9].value = "GRAN";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]));
                RangeCabecera.ColumnWidth = 9;
                Worksheet.Cells[i, 10].value = "FRES CH";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]));
                RangeCabecera.ColumnWidth = 9;
                Worksheet.Cells[i, 11].value = "BISEL CH";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 11]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 11]));
                RangeCabecera.ColumnWidth = 9;
                Worksheet.Cells[i, 12].value = "PLEG CH";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12]));
                RangeCabecera.ColumnWidth = 9;

                Worksheet.Cells[i, 13].value = "P2";
                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 13]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 13]));
                RangeCabecera.ColumnWidth = 9;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 13]));
                RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                RangeCabecera.Font.Bold = true;
                RangeCabecera.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                RangeCabecera.Font.Size = 14;


                foreach (DataGridViewRow row in dataGridView1.Rows) // Recorro los datos
                {
                    i++;
                    //codigo

                    Worksheet.Cells[i, 1].value = row.Cells[0].Value.ToString();
                    Worksheet.Cells[1, 1].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //paquete
                    Worksheet.Cells[i, 2].value = row.Cells[1].Value.ToString();
                    Worksheet.Cells[1, 2].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //cantidad
                    Worksheet.Cells[i, 3].value = row.Cells[2].Value.ToString();
                    Worksheet.Cells[1, 3].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //mp
                    string nombreMP = "";
                    nombreMP = row.Cells[3].Value.ToString();
                    string[] wordsMP = nombreMP.Split('-');
                    if (wordsMP.Length > 0) Worksheet.Cells[i, 4].value = wordsMP[0];
                    else Worksheet.Cells[i, 4].value = "";
                    Worksheet.Cells[1, 4].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //long
                    Worksheet.Cells[i, 5].value = row.Cells[4].Value.ToString();
                    Worksheet.Cells[1, 5].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //Plasm
                    string Plasm = "";
                    Plasm = row.Cells[5].Value.ToString();
                    string[] wordsPuntAS = Plasm.Split(' ');
                    if (wordsPuntAS.Length > 1) Worksheet.Cells[i, 6].value = wordsPuntAS[0] + ' ' + wordsPuntAS[1];
                    else Worksheet.Cells[i, 6].value = wordsPuntAS[0];
                    Worksheet.Cells[1, 6].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //Ciza
                    string Ciza = "";
                    Ciza = row.Cells[6].Value.ToString();
                    string[] wordsSoldAS = Ciza.Split(' ');
                    if (wordsSoldAS.Length > 1) Worksheet.Cells[i, 7].value = wordsSoldAS[0] + ' ' + wordsSoldAS[1];
                    else Worksheet.Cells[i, 7].value = wordsSoldAS[0];
                    Worksheet.Cells[1, 7].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //Punteo 
                    string punt = "";
                    punt = row.Cells[7].Value.ToString();
                    string[] wordsPunt = punt.Split(' ');
                    if (wordsPunt.Length > 1) Worksheet.Cells[i, 8].value = wordsPunt[0] + ' ' + wordsPunt[1];
                    else Worksheet.Cells[i, 8].value = wordsPunt[0];
                    Worksheet.Cells[1, 8].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //Gran
                    string Gran = "";
                    Gran = row.Cells[8].Value.ToString();
                    string[] wordsSold = Gran.Split(' ');
                    if (wordsSold.Length > 1) Worksheet.Cells[i, 9].value = wordsSold[0] + ' ' + wordsSold[1];
                    else Worksheet.Cells[i, 9].value = wordsSold[0];
                    Worksheet.Cells[1, 9].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //FresCh
                    string FresCh = "";
                    FresCh = row.Cells[9].Value.ToString();
                    string[] wordsFresCh = FresCh.Split(' ');
                    if (wordsFresCh.Length > 1) Worksheet.Cells[i, 10].value = wordsFresCh[0] + ' ' + wordsFresCh[1];
                    else Worksheet.Cells[i, 10].value = wordsFresCh[0];
                    Worksheet.Cells[1, 10].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //BiselCH
                    string BiselCH = "";
                    BiselCH = row.Cells[10].Value.ToString();
                    string[] wordsBiselCH = BiselCH.Split(' ');
                    if (wordsBiselCH.Length > 1) Worksheet.Cells[i, 10].value = wordsBiselCH[0] + ' ' + wordsBiselCH[1];
                    else Worksheet.Cells[i, 11].value = wordsBiselCH[0];
                    Worksheet.Cells[1, 11].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 11]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 11]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //PlagCH
                    string PlagCH = "";
                    PlagCH = row.Cells[11].Value.ToString();
                    string[] wordsPlagCH = PlagCH.Split(' ');
                    if (wordsPlagCH.Length > 1) Worksheet.Cells[i, 11].value = wordsPlagCH[0] + ' ' + wordsPlagCH[1];
                    else Worksheet.Cells[i, 12].value = wordsPlagCH[0];
                    Worksheet.Cells[1, 12].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                    //FresCh
                    string P2 = "";
                    P2 = row.Cells[9].Value.ToString();
                    string[] wordsP2 = P2.Split(' ');
                    if (wordsP2.Length > 1) Worksheet.Cells[i, 10].value = wordsP2[0] + ' ' + wordsP2[1];
                    else Worksheet.Cells[i, 13].value = wordsP2[0];
                    Worksheet.Cells[1, 13].Style.WrapText = true;

                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 13]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 13]));
                    RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;






                    if ((i % 2) != 0)
                    {
                        RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 13]));
                        RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    }

                }


                string RUTA = "C:\\Users\\" + Environment.UserName + "\\Desktop\\INFORMEPROYECTOSONLINE.xlsx";
                Worksheet.SaveAs(RUTA);
                Excel.Quit();

                System.Diagnostics.Process.Start(RUTA);

                Cursor.Current = Cursors.Default;
            }

        private void button3_Click(object sender, EventArgs e)
        {
            //Creo el excel
            Cursor.Current = Cursors.WaitCursor;
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;

            Microsoft.Office.Interop.Excel.Range RangeCabecera;

            Worksheet.Cells[1, 1].value = "PROYECTO: " + proyectoGlobal + nombreProyectoGlobal;
            int i = 2;
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 10]));
            RangeCabecera.Merge(true);            
            RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            RangeCabecera.Font.Bold = true;
            RangeCabecera.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            RangeCabecera.Font.Size = 18;


            Worksheet.Cells[i, 1].value = "CODIGO";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]));
            RangeCabecera.ColumnWidth = 10;

            Worksheet.Cells[i, 2].value = "PAQUETE";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]));
            RangeCabecera.ColumnWidth = 10;

            Worksheet.Cells[i, 3].value = "CTD";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]));
            RangeCabecera.ColumnWidth = 5;

            Worksheet.Cells[i, 4].value = "MP";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]));
            RangeCabecera.ColumnWidth = 10;
            Worksheet.Cells[i, 5].value = "LONG";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]));
            RangeCabecera.ColumnWidth = 10;
            Worksheet.Cells[i, 6].value = "Punt AS";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]));
            RangeCabecera.ColumnWidth = 10;
            Worksheet.Cells[i, 7].value = "Sold AS";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]));
            RangeCabecera.ColumnWidth = 10;
            Worksheet.Cells[i, 8].value = "Punt";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]));
            RangeCabecera.ColumnWidth = 10;

            Worksheet.Cells[i, 9].value = "Sold";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]));
            RangeCabecera.ColumnWidth = 10;
            Worksheet.Cells[i, 10].value = "P2";
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]));
            RangeCabecera.ColumnWidth = 10;
            
            RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]));
            RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            RangeCabecera.Font.Bold = true;
            RangeCabecera.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            RangeCabecera.Font.Size = 12;


            foreach (DataGridViewRow row in dataGridView1.Rows) // Recorro los datos
            {
                i++;
                //codigo

                Worksheet.Cells[i, 1].value = row.Cells[0].Value.ToString();
                Worksheet.Cells[1, 1].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //paquete
                Worksheet.Cells[i, 2].value = row.Cells[1].Value.ToString();
                Worksheet.Cells[1, 2].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 2]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //cantidad
                Worksheet.Cells[i, 3].value = row.Cells[2].Value.ToString();
                Worksheet.Cells[1, 3].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 3]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //mp
                string nombreMP = "";
                nombreMP = row.Cells[3].Value.ToString();
                string[] wordsMP = nombreMP.Split('-');
                if (wordsMP.Length > 0) Worksheet.Cells[i, 4].value = wordsMP[0];
                else Worksheet.Cells[i, 4].value = "";
                Worksheet.Cells[1, 4].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 4]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //long
                Worksheet.Cells[i, 5].value = row.Cells[4].Value.ToString();
                Worksheet.Cells[1, 5].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 5]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //Punteo AS
                string puntAS = "";
                puntAS = row.Cells[5].Value.ToString();
                string[] wordsPuntAS = puntAS.Split(' ');
                if (wordsPuntAS.Length > 1) Worksheet.Cells[i, 6].value = wordsPuntAS[0] + ' ' + wordsPuntAS[1];
                else Worksheet.Cells[i, 6].value = wordsPuntAS[0];
                Worksheet.Cells[1, 6].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 6]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //sold AS
                string soldAS = "";
                soldAS = row.Cells[6].Value.ToString();
                string[] wordsSoldAS = soldAS.Split(' ');
                if (wordsSoldAS.Length > 1) Worksheet.Cells[i, 7].value = wordsSoldAS[0] + ' ' + wordsSoldAS[1];
                else Worksheet.Cells[i, 7].value = wordsSoldAS[0];
                Worksheet.Cells[1, 7].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 7]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //Punteo 
                string punt = "";
                punt = row.Cells[7].Value.ToString();
                string[] wordsPunt = punt.Split(' ');
                if (wordsPunt.Length > 1) Worksheet.Cells[i, 8].value = wordsPunt[0] + ' ' + wordsPunt[1];
                else Worksheet.Cells[i, 8].value = wordsPunt[0];
                Worksheet.Cells[1, 8].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 8]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //sold 
                string sold = "";
                sold = row.Cells[8].Value.ToString();
                string[] wordsSold = sold.Split(' ');
                if (wordsSold.Length > 1) Worksheet.Cells[i, 9].value = wordsSold[0] + ' ' + wordsSold[1];
                else Worksheet.Cells[i, 9].value = wordsSold[0];
                Worksheet.Cells[1, 9].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 9]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------//
                //P2
                string P2 = "";
                P2 = row.Cells[9].Value.ToString();
                string[] wordsP2 = P2.Split(' ');
                if (wordsP2.Length > 1) Worksheet.Cells[i, 10].value = wordsP2[0] + ' ' + wordsP2[1];
                else Worksheet.Cells[i, 10].value = wordsP2[0];
                Worksheet.Cells[1, 10].Style.WrapText = true;

                RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 10]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
             

              

               

                if ((i % 2) != 0)
                {
                    RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i,10]));
                    RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                }

                /*      Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 1]),
                 (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, 12])).BorderAround(XlLineStyle.xlContinuous,
                 XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic,
                 XlColorIndex.xlColorIndexAutomatic);*/

            }


            string RUTA = "C:\\Users\\" + Environment.UserName + "\\Desktop\\INFORMEPROYECTOSONLINE.xlsx";
            Worksheet.SaveAs(RUTA);
            Excel.Quit();

            System.Diagnostics.Process.Start(RUTA);

            Cursor.Current = Cursors.Default;
        }






        private void EXPORTAR()
        {
            //Creo el excel

            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;

            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

            Cursor.Current = Cursors.WaitCursor;
            /*Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;*/
            Microsoft.Office.Interop.Excel.Range RangeCabecera;

            this.progressBar1.Maximum = dataGridView1.Rows.Count;
            this.progressBar1.Step = 1;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                oSheet.Cells[2, col.Index+1].value = col.HeaderText.ToUpper();
                RangeCabecera = oSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(oSheet.Cells[2, col.Index+1]), (Microsoft.Office.Interop.Excel.Range)(oSheet.Cells[2, col.Index+1]));
                RangeCabecera.ColumnWidth = col.HeaderText.Length + 6;
            }


            RangeCabecera = oSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(oSheet.Cells[2, 1]), (Microsoft.Office.Interop.Excel.Range)(oSheet.Cells[2, dataGridView1.Columns.Count]));
            RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            RangeCabecera.Font.Bold = true;
            RangeCabecera.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            RangeCabecera.Font.Size = 14;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int x = 1; x <= dataGridView1.Columns.Count; x++)
                {
                    oSheet.Cells[row.Index + 3, x].value = row.Cells[x - 1].Value.ToString();
                    oSheet.Cells[row.Index + 3, x].Style.WrapText = true;
                    
                }

                RangeCabecera = oSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(oSheet.Cells[row.Index + 3, 1]), (Microsoft.Office.Interop.Excel.Range)(oSheet.Cells[row.Index + 3, dataGridView1.Columns.Count]));
                RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                if ((row.Index % 2) != 0)
                {
                    RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                }
                this.progressBar1.PerformStep();
            }


            //string RUTA = "C:\\Users\\" + Environment.UserName + "\\Desktop\\INFORMEPROYECTOSONLINE.xlsx";
            //oWB.SaveAs(RUTA);
            //Excel.Quit();

            //System.Diagnostics.Process.Start(RUTA);

            Cursor.Current = Cursors.Default;
            



        }

        private void btnExportarGeneral_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = true;

            if (!chkRapido.Checked)
            {
                EXPORTAR();
            }
            else
            {
                ExpRapida();
            }
            this.progressBar1.Value = 0;
            this.panel1.Visible = false;
        }

        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ExpRapida()
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];

            if (nombreProyectoGlobal == "-")
            {
                offsetCabecera += 1;

                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                Calendar cal = new CultureInfo("es-ES").Calendar;
                int week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                string tipo = "";

                if (angularGlobal == 1)
                {
                    tipo = "ANGULARES, TUB-RED, UPNs SEMANA " + week;
                }
                if (chapaGlobal == 1)
                {
                    tipo = "CHAPAS SEMANA " + week;
                }
                if (cjsGlobal == 1)
                {
                    tipo = "CONJUNTOS SOLDADOS SEMANA " + week;
                }

                xlWorkSheet.Cells[1, 1].value = tipo;

            }
            else
            {
                xlWorkSheet.Cells[1, 1].value = "PROYECTO: " + proyectoGlobal + nombreProyectoGlobal;
            }

            CR = xlWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[1, dataGridView1.Columns.Count]));
            CR.Cells.Merge();
            

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                xlWorkSheet.Cells[2, col.Index + 1].value = col.HeaderText.ToUpper();
                CR = xlWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[2, col.Index + 1]), (Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[2, col.Index + 1]));
                CR.ColumnWidth = col.HeaderText.Length + 6;
            }

            CR = xlWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[2, dataGridView1.Columns.Count]));
            CR.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            CR.Font.Bold = true;
            CR.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            CR.Font.Size = 14;


            CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[3, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            CR = xlWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[3, 1]), (Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[dataGridView1.Rows.Count+3, dataGridView1.Columns.Count]));
            
            //CR.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

            CR = xlWorkSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(xlWorkSheet.Cells[dataGridView1.Rows.Count + 3, dataGridView1.Columns.Count]));
            CR.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            CR.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            CR.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            CR.Columns.AutoFit();
        }

        public string ToExcelCoordinates(string coordinates)
        {
            string first = coordinates.Substring(0, coordinates.IndexOf(','));
            int i = int.Parse(first);
            string second = coordinates.Substring(first.Length + 1);

            string str = string.Empty;
            while (i > 0)
            {
                str = alphabet[(i - 1) % 26] + str;
                i /= 26;
            }

            return str + second;
        }

        private void btnExpFormato_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = true;
            EXPORTAR();
            this.progressBar1.Value = 0;
            this.panel1.Visible = false;
        }
        
    }
}
