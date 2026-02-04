using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EstadoTaller.Interfaces;
using EstadoTaller.Util;
using empresaGlobalProj;

namespace EstadoTaller
{
    public partial class EstadoTallerForm : Form, IEstadoTallerForm
    {
        private EstadoTallerContol control;
        private UCGraficoBarras graficoSeleccionado;
   

        public EstadoTallerForm()
        {
            InitializeComponent();
            empresaGlobal.showEmp = false;
            comboEMpresaMaq.SelectedIndex = 0;
        }

        public void SetControlador(EstadoTallerContol controlEt)
        {
            control = controlEt;
        }

        delegate void CargarGraficasDelegate(List<Utils.DatosMaquina> listaMaquinas, double escala);

        public void CargarGraficas(List<Utils.DatosMaquina> listaMaquinas, double escala)
        {
           
            if (InvokeRequired)
            {
                BeginInvoke(new CargarGraficasDelegate(CargarGraficas), new object[] {listaMaquinas, escala});
            }
            else
            {
                var numMaquinas = listaMaquinas.Count;
                tlpGraficasMaquinas.Controls.Clear();
                tlpGraficasMaquinas.ColumnStyles.Clear();
                tlpGraficasMaquinas.Visible = false;
                tlpGraficasMaquinas.ColumnCount = numMaquinas;
                for (int i = 0; i < numMaquinas; i++)
                {
                    var column = new ColumnStyle(SizeType.Percent, 100F/numMaquinas);
                    tlpGraficasMaquinas.ColumnStyles.Add(column);
                    var grafico = new UCGraficoBarras
                    {
                        Puesto = listaMaquinas[i].puesto,
                        Denominacion = listaMaquinas[i].denominacion,
                        HorasRealizables = listaMaquinas[i].tRealizables,
                        HorasNoRealizables = listaMaquinas[i].tNoRealizables,
                        HorasInciertas = listaMaquinas[i].tInciertas,

                        KgRealizables = listaMaquinas[i].kgRealizables,
                        KgNoRealizables = listaMaquinas[i].kgNoRealizables,
                        KgInciertas = listaMaquinas[i].kgInciertas,
                        KgTotales =   listaMaquinas[i].kgMaquina,

                        Escala = escala,
                        Dock = DockStyle.Fill
                    };
                    if (graficoSeleccionado != null && graficoSeleccionado.Puesto == grafico.Puesto)
                    {
                        grafico.Seleccionado = true;
                        graficoSeleccionado = grafico;
                    }
                    grafico.GraficoBarrasClick += new EventHandler(grafico_Click);
                    tlpGraficasMaquinas.Controls.Add(grafico, i, 0);
                    grafico.AdaptarGraficaBarras();
                }
                tlpGraficasMaquinas.Visible = true;
                                

                if (graficoSeleccionado != null)
                {
                    grafico_Click(graficoSeleccionado, null);
                }
            }
        }

        private void grafico_Click(object sender, EventArgs e)
        {
            graficoSeleccionado = null;
            foreach (UCGraficoBarras ucGrafBarra in tlpGraficasMaquinas.Controls)
            {
                ucGrafBarra.Seleccionado = false;
                ucGrafBarra.Refresh();
            }
          
            var ucGraficoBarras = sender as UCGraficoBarras;
           
            if (ucGraficoBarras != null)
            {
                ucGraficoBarras.Seleccionado = true;
                ucGraficoBarras.Invalidate();
                ucGraficoBarras.Refresh();
                control.GraficoSeleccionado(ucGraficoBarras.Puesto);
                if (sCPrincipal.Panel2Collapsed)
                    sCPrincipal.Panel2Collapsed = false;
                graficoSeleccionado = ucGraficoBarras;
            }

            sCGrids.SplitterDistance = this.Width / 2;
        }




        public void CargarTimer()
        {
            //timer1.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["TiempoRefresco"]);
            //timer1.Enabled = true;
        }

        public void AgregarDataGridRealizables(DataTable realizables)
        {
            dGVRealizables.DataSource = realizables;
            var dataGridViewColumn = dGVRealizables.Columns["REALIZABLE"];
            if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
        }

        public void AgregarDataGridNoRealizalbes(DataTable noRealizables)
        {
            dGvNoRealizables.DataSource = noRealizables;
            var dataGridViewColumn = dGvNoRealizables.Columns["REALIZABLE"];
            if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //control.ActualizarEstadoTaller(false);
        }

        private void recargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //control.ActualizarEstadoTaller(false);
            //timer1.Enabled = true;
        }

        private void dGVRealizables_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (Convert.ToInt16(dGVRealizables.Rows[e.RowIndex].Cells["REALIZABLE"].Value) == 2)
                e.CellStyle.BackColor = Color.Coral;
        }

        public int getTipoMaquina()
        {
            int resultado = 0;
            if (rBAngular.Checked) resultado = 1;
            else
                if (rBChapa.Checked) resultado = 2;
                else
                    if (rBAux.Checked) resultado = 3;
                    else
                        if (rBSoldadura.Checked) resultado = 5;//La de soldadura.
                        else
                            resultado = 4; //Acabado superficial
            return resultado;
        }


        public int getMaterial()
        {
            int resultado = 0;
            if (!cBAux.Visible) resultado = 2;//Si él comboBox no está visible, queremos todos los materiales.
            else
                resultado = cBAux.SelectedIndex;            
            return resultado;
        }

        private void rBAngular_CheckedChanged(object sender, EventArgs e)
        {
            if (cBAux.Visible) cBAux.SelectedIndex = 2;
            control.ActualizarEstadoTaller(false);
            SeleccionarPrimeraMaquina();
            cBAux.Visible = false;
        }

        private void rBChapa_CheckedChanged(object sender, EventArgs e)
        {
            if (cBAux.Visible) cBAux.SelectedIndex = 2;
            control.ActualizarEstadoTaller(false);
            SeleccionarPrimeraMaquina();
            cBAux.Visible = false;
        }

        private void rBSoldadura_CheckedChanged(object sender, EventArgs e)
        {
            if (cBAux.Visible) cBAux.SelectedIndex = 2;
            control.ActualizarEstadoTaller(false);
            SeleccionarPrimeraMaquina();
            if (cBAux.SelectedIndex < 0) cBAux.SelectedIndex = 2;
            cBAux.Visible = false;
        }


        private void rbAcabado_CheckedChanged(object sender, EventArgs e)
        {
            if (cBAux.Visible) cBAux.SelectedIndex = 2;
            control.ActualizarEstadoTaller(false);
            SeleccionarPrimeraMaquina();
            if (cBAux.SelectedIndex < 0) cBAux.SelectedIndex = 2;
            cBAux.Visible = false;
        }


        private void rBAux_CheckedChanged(object sender, EventArgs e)
        {
            cBAux.Visible = true;
            cBAux.SelectedIndex = 2;
              
            control.ActualizarEstadoTaller(false);                                   

            SeleccionarPrimeraMaquina(); 
        }

        private void SeleccionarPrimeraMaquina()
        {
            //MMM Seleccionamos el primer gráfico de la lista
            if (tlpGraficasMaquinas.Controls.Count > 0)
            {
                UCGraficoBarras graficoAux = (UCGraficoBarras)tlpGraficasMaquinas.Controls[0];
                graficoAux.Seleccionado = true;
                graficoSeleccionado = graficoAux;
            }
        }

        private void cBAux_SelectedIndexChanged(object sender, EventArgs e)
        {
            control.ActualizarEstadoTaller(false);
            SeleccionarPrimeraMaquina();
        }

        private void EstadoTallerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {

            DataTable dtRealizable;
            DataTable dtNoRealizable;
            if (comboEMpresaMaq.Text.ToString() == "Imedexsa")
            {
                dtRealizable = control.obtenerMaquinasRealizable("Imedexsa");
                dtNoRealizable = control.obtenerMaquinasNoRealizable("Imedexsa");
            }
            else if (comboEMpresaMaq.Text.ToString() == "Made")
            {
                dtRealizable = control.obtenerMaquinasRealizable("Made");
                dtNoRealizable = control.obtenerMaquinasNoRealizable("Made");
            }
            else {
                dtRealizable = control.obtenerMaquinasRealizable("Todas");
                dtNoRealizable = control.obtenerMaquinasNoRealizable("Todas");
            }


            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xlsx)|*.xlsx";
            if (fichero.ShowDialog() == DialogResult.OK)
            {


                Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbooks.Add();
                Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;



                int i = 2;
                int j = 2;


                Microsoft.Office.Interop.Excel.Range RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 12]));
                RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                RangeCabecera.Font.Bold = true;
                RangeCabecera.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

                Microsoft.Office.Interop.Excel.Range RangeCabeceraNoRealizable = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 14]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 25]));
                RangeCabeceraNoRealizable.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkRed);
                RangeCabeceraNoRealizable.Font.Bold = true;
                RangeCabeceraNoRealizable.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);


                Worksheet.Cells[1, 1].value = "MAQUINA";
                Worksheet.Cells[1, 2].value = "PROYECTO";
                Worksheet.Cells[1, 3].value = "BONO";
                Worksheet.Cells[1, 4].value = "MARCA";
                Worksheet.Cells[1, 5].value = "LONGITUD";
                Worksheet.Cells[1, 6].value = "ORFAB";
                Worksheet.Cells[1, 7].value = "CANTIDAD";
                Worksheet.Cells[1, 8].value = "LANZADA";
                Worksheet.Cells[1, 9].value = "FICHADA";
                Worksheet.Cells[1, 10].value = "HORAS";
                Worksheet.Cells[1, 11].value = "MP";
                Worksheet.Cells[1, 12].value = "KG";

                Worksheet.Cells[1, 14].value = "MAQUINA";
                Worksheet.Cells[1, 15].value = "PROYECTO";
                Worksheet.Cells[1, 16].value = "BONO";
                Worksheet.Cells[1, 17].value = "MARCA";
                Worksheet.Cells[1, 18].value = "LONGITUD";
                Worksheet.Cells[1, 19].value = "ORFAB";
                Worksheet.Cells[1, 20].value = "CANTIDAD";
                Worksheet.Cells[1, 21].value = "LANZADA";
                Worksheet.Cells[1, 22].value = "FICHADA";
                Worksheet.Cells[1, 23].value = "HORAS";
                Worksheet.Cells[1, 24].value = "MP";
                Worksheet.Cells[1, 25].value = "KG";


                Cursor.Current = Cursors.WaitCursor;
                foreach (DataRow row in dtRealizable.Rows)
                {



                    Worksheet.Cells[i, 1].value = row[0].ToString();
                    Worksheet.Cells[i, 2].value = row[1].ToString();
                    Worksheet.Cells[i, 3].value = row[2].ToString();
                    Worksheet.Cells[i, 4].value = row[3].ToString();
                    Worksheet.Cells[i, 5].value = row[4].ToString();
                    Worksheet.Cells[i, 6].value = row[5].ToString();
                    Worksheet.Cells[i, 7].value = row[6].ToString();
                    Worksheet.Cells[i, 8].value = row[7].ToString();
                    Worksheet.Cells[i, 9].value = row[8].ToString();
                    Worksheet.Cells[i, 10].value = Convert.ToDouble(row[9].ToString());
                    Worksheet.Cells[i, 11].value = row[10].ToString();
                    Worksheet.Cells[i, 12].value = Convert.ToDouble(row[11].ToString());
                    //Worksheet.Cells[i, 12].value = Convert.ToDouble(row[11].ToString());

                    if (row[12].ToString().Equals("2"))
                    {
                        Worksheet.Cells[i, 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 3].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 4].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 5].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 6].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 7].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 8].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 9].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 10].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 11].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                        Worksheet.Cells[i, 12].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Coral);
                    }

                    i++;
                }


                foreach (DataRow row in dtNoRealizable.Rows)
                {
                    Worksheet.Cells[j, 14].value = row[0].ToString();
                    Worksheet.Cells[j, 15].value = row[1].ToString();
                    Worksheet.Cells[j, 16].value = row[2].ToString();
                    Worksheet.Cells[j, 17].value = row[3].ToString();
                    Worksheet.Cells[j, 18].value = row[4].ToString();
                    Worksheet.Cells[j, 19].value = row[5].ToString();
                    Worksheet.Cells[j, 20].value = row[6].ToString();
                    Worksheet.Cells[j, 21].value = row[7].ToString();
                    Worksheet.Cells[j, 22].value = row[8].ToString();
                    Worksheet.Cells[j, 23].value = Convert.ToDouble(row[9].ToString());
                    Worksheet.Cells[j, 24].value = row[10].ToString();
                    Worksheet.Cells[j, 25].value = Convert.ToDouble(row[11].ToString());

                    j++;
                }
                Cursor.Current = Cursors.Default;

                MessageBox.Show("La exportación se ha realizado con éxito");


                Worksheet.Columns.AutoFit();
                Worksheet.SaveAs(fichero.FileName);
                Excel.Quit();
            } 


            /*
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo =
                    (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                //Recorremos el DataGridView rellenando la hoja de trabajo

                hoja_trabajo.Cells[1, 1] = "PROYECTO";
                hoja_trabajo.Cells[1, 2] = "BONO";
                hoja_trabajo.Cells[1, 3] = "MARCA";
                hoja_trabajo.Cells[1, 4] = "LONGITUD";
                hoja_trabajo.Cells[1, 5] = "ORFAB";
                hoja_trabajo.Cells[1, 6] = "CANTIDAD";
                hoja_trabajo.Cells[1, 7] = "LANZADA";
                hoja_trabajo.Cells[1, 8] = "FICHADA";
                hoja_trabajo.Cells[1, 9] = "HORAS";
                hoja_trabajo.Cells[1, 10] = "MP";
                hoja_trabajo.Cells[1, 11] = "KG";


                DataTable dt = dtRealizable;
                libros_trabajo.Worksheets.Add(dt, "WorksheetName");

                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dtRealizable.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dtRealizable.Columns.Count-1; j++)
                    {
                        if (dtRealizable.Rows[i][11].ToString().Equals("2")){
                        hoja_trabajo.Cells[i + 2, j + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gold); 
                        }
                        hoja_trabajo.Cells[i + 2, j + 1] = dtRealizable.Rows[i][j].ToString();
                    }
                }
                Cursor.Current = Cursors.Default;
                libros_trabajo.SaveAs(fichero.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();
            }
            */


        }


    }
}