using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPintura
{
    public partial class GestionPintura : Form
    {

        DButils dbu = new DButils();

        DataTable DTbastidores;
        double totalkg = 0;
        double totalm2 = 0;
        double totalpiezas = 0;
        double totalpaquetes = 0;

        public GestionPintura()
        {
            InitializeComponent();
            this.comboBoxEstadoCarro.SelectedIndex = 0;
            

        }

        private void btnBastidores_Click(object sender, EventArgs e)
        {
            DTbastidores = dbu.obtenerEstadoBastidores();

            this.dgvBastidores.DataSource = DTbastidores;

            string test = "";


            foreach (DataGridViewRow row in this.dgvBastidores.Rows)
            {
                test = row.Cells[1].Value.ToString();
                if (test == "Libre")
                {
                    row.DefaultCellStyle.BackColor = Color.Lime;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Coral;
                }
            }

        }

        private void btnConsultarCarros_Click(object sender, EventArgs e)
        {

            this.dgvCarros.DataSource = dbu.obtenerCarros(this.dateTimePicker1.Value, this.dateTimePicker2.Value, this.comboBoxEstadoCarro.SelectedIndex);

            double totalKg = 0;
            double totalM2 = 0;
            int totalPiezas = 0;

            foreach (DataGridViewRow row in this.dgvCarros.Rows)
            {
                totalKg += Convert.ToDouble(row.Cells[3].Value);
                totalM2 += Convert.ToDouble(row.Cells[4].Value);
                totalPiezas += Convert.ToInt32(row.Cells[5].Value);

            }

            this.labelKgCarros.Text = totalKg.ToString();
            this.labelM2Carros.Text = totalM2.ToString();
            this.labelPiezasCarros.Text = totalPiezas.ToString();
        }

        private void btnProductividad_Click(object sender, EventArgs e)
        {
            this.dgvHorasPorMaquina.DataSource = dbu.obtenerHorasPuestosPintura(this.dtpProdDesde.Value, this.dtpProdHasta.Value);
            this.dgvHorasPorMaquina.Columns[0].Width = 250;

            double horasDirectas = 0;
            double horasIndir = 0;

            foreach (DataGridViewRow row in dgvHorasPorMaquina.Rows)
            {
                if (row.Cells[0].Value.ToString().Contains("875"))
                {
                    horasIndir += Convert.ToDouble(row.Cells[1].Value);
                }
                else
                {
                    horasDirectas += Convert.ToDouble(row.Cells[1].Value);
                }
            }

            DataTable carros = new DataTable();
                
            carros = dbu.obtenerCarros(dtpProdDesde.Value, dtpProdHasta.Value, 0);

            double kgTotalesProd = 0;
            double m2TotalesProd = 0;

            foreach (DataRow row in dbu.obtenerCarros(dtpProdDesde.Value, dtpProdHasta.Value, 0).Rows)
            {
                kgTotalesProd += Convert.ToDouble(row[3]);
                m2TotalesProd += Convert.ToDouble(row[4]);
            }

            this.hDirectas.Text = horasDirectas.ToString();
            this.hIndirectas.Text = horasIndir.ToString();
            this.hTotales.Text = (horasDirectas + horasIndir).ToString();

            //double dias = (dtpProdHasta.Value - dtpProdDesde.Value).TotalDays;
            double dias = diasLaborales(dtpProdDesde.Value, dtpProdHasta.Value);

            this.m2Dia.Text = Math.Round((m2TotalesProd / dias), 2).ToString();
            this.m2HDirecta.Text = Math.Round((m2TotalesProd / horasDirectas), 2).ToString();
            this.m2hTotal.Text = Math.Round((m2TotalesProd / (horasDirectas + horasIndir)), 2).ToString();
            this.kgDia.Text = Math.Round((kgTotalesProd / horasDirectas), 2).ToString();
        }

        private int diasLaborales(DateTime desde, DateTime hasta)
        {
            int dias = 0;

            while (desde <= hasta)
            {
                if ((desde.DayOfWeek != DayOfWeek.Saturday) && (desde.DayOfWeek != DayOfWeek.Sunday))
                {
                    dias++;
                }

                desde = desde.AddDays(1);
            }

            return dias;

        }

        private void btnCamionesPintura_Click(object sender, EventArgs e)
        {
            totalkg = 0;
            totalm2 = 0;
            totalpaquetes = 0;
            totalpiezas = 0;

            this.dgvCamionesMes.DataSource = dbu.obtenerContenedoresMes(this.dtpCamionesDesde.Value, this.dtpCamionesHasta.Value);

            foreach (DataGridViewRow row in dgvCamionesMes.Rows)
            {
                totalkg += Convert.ToDouble(row.Cells[7].Value);
                totalm2 += Convert.ToDouble(row.Cells[8].Value);
                totalpiezas += Convert.ToDouble(row.Cells[6].Value);
                totalpaquetes += Convert.ToDouble(row.Cells[5].Value);
            }

            labelKgCamiones.Text = totalkg.ToString();
            labelM2Camiones.Text = totalm2.ToString();
            labelPaquetesCamiones.Text = totalpaquetes.ToString();
            labelPiezasCamiones.Text = totalpiezas.ToString();
            labelCamiones.Text = dgvCamionesMes.Rows.Count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            chartRatios.Series.Clear();
            chartRatios.Titles.Clear();


            this.panel1.Visible = true;

            this.chartRatios.Titles.Add("PRODUCTIVIDAD GRANALLADO (m2/h)");
            this.chartRatios.Titles[0].BackColor = Color.Yellow;

            this.chartRatios.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;

            this.chartRatios.ChartAreas[0].AxisX.Maximum = 13;
            this.chartRatios.ChartAreas[0].AxisX.Minimum = 1;


            this.chartRatios.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;

            this.chartRatios.ChartAreas[0].AxisY.Maximum = 20;
            this.chartRatios.ChartAreas[0].AxisY.Minimum = 0;

            this.chartRatios.ChartAreas[0].AxisX.Title = "SEMANA";
            this.chartRatios.ChartAreas[0].AxisY.Title = "M2/h";

            this.chartRatios.Series.Add("M2/h");
            chartRatios.Series["M2/h"].Color = Color.ForestGreen;
            chartRatios.Series["M2/h"].ChartArea = "ChartArea1";
            chartRatios.Series["M2/h"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int cont = 1;
            double valorpunto = 0;
            foreach (DataRow row in dbu.obtenerValorSemana("chorreo").Rows)
            {
                if (row[1].ToString() != "0.00")
                {
                    valorpunto = Convert.ToDouble(row[0].ToString()) / Convert.ToDouble(row[1].ToString());
                }
                else
                {
                    valorpunto = 0;
                }

                chartRatios.Series["M2/h"].Points.AddXY(cont, valorpunto);
                cont++;
            }*/

            calcularGrafico("chorreo", Color.Yellow);

        }

        private void btnCerrarChart_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void btnCerrarRatios_Click(object sender, EventArgs e)
        {
            this.panelRatios.Visible = false;
        }

        private void btnRatios_Click(object sender, EventArgs e)
        {
            this.labelRatiosSemanas.Text = "RATIOS POR SEMANA " + DateTime.Now.Year;

            DataTable tableRatios = new DataTable();
            DataTable valoresRatios = new DataTable();

            DateTime dia = new DateTime(DateTime.Now.Year, 1, 2);

            int counter = 1;

            double m2Gran = 0;
            double hGran = 0;
            double porcGran = 0;
            double prodGran = 0;
            double objGran = 0;
            double m2Pint = 0;
            double hPint = 0;
            double porcPint = 0;
            double prodPint = 0;
            double objPint = 0;
            double kgEmpaq = 0;
            double hEmpaq = 0;
            double porcEmpaq = 0;
            double prodEmpaq = 0;
            double objEmpaq = 0;
            double hReproc = 0;
            double porcReproc = 0;
            double hCarr = 0;
            double porcCarr = 0;
            double hMat = 0;
            double porcMat = 0;
            double hEnmasc = 0;
            double porcEnmasc = 0;
            double totalH = 0;
            double m2Empaq = 0;
            double deficitEmpaq = 0;

            dgvRatios.Rows.Clear();

            while (dia < DateTime.Now)
            {
                tableRatios = dbu.obtenerHorasPuestosPintura(dia.Date, dia.Date.AddDays(6));
                valoresRatios = dbu.obtenerValoresRatios(dia.Date, dia.Date.AddDays(6));

                totalH = 0;
                m2Gran = 0;
                hGran = 0;
                porcGran = 0;
                prodGran = 0;
                objGran = 0;
                m2Pint = 0;
                hPint = 0;
                porcPint = 0;
                prodPint = 0;
                objPint = 0;
                kgEmpaq = 0;
                hEmpaq = 0;
                porcEmpaq = 0;
                prodEmpaq = 0;
                objEmpaq = 0;
                hReproc = 0;
                porcReproc = 0;
                hCarr = 0;
                porcCarr = 0;
                hMat = 0;
                porcMat = 0;
                hEnmasc = 0;
                porcEnmasc = 0;
                m2Empaq = 0;
                deficitEmpaq = 0;


                foreach (DataRow row in tableRatios.Rows)
                {

                    string test = row[1].ToString();
                    if (row[1].ToString() != "")
                    {
                        totalH += Convert.ToDouble(row[1].ToString());


                        switch (row[0].ToString().Substring(0, 5))
                        {

                            case "[876]":
                                hCarr = Convert.ToDouble(row[1].ToString());
                                break;
                            case "[877]":
                                hMat = Convert.ToDouble(row[1].ToString());
                                break;
                            case "[878]":
                                hGran = Convert.ToDouble(row[1].ToString());
                                break;
                            case "[879]":
                                hPint = Convert.ToDouble(row[1].ToString());
                                break;
                            case "[880]":
                                hEnmasc = Convert.ToDouble(row[1].ToString());
                                break;
                            case "[881]":
                                hReproc = Convert.ToDouble(row[1].ToString());
                                break;
                            case "[882]":
                                hEmpaq = Convert.ToDouble(row[1].ToString());
                                break;

                            default:
                                break;


                        }
                    }
                }


                m2Gran = Convert.ToDouble( valoresRatios.Rows[0][1].ToString());

                porcGran = Math.Round(hGran / totalH * 100, 2);
                prodGran =  Math.Round(m2Gran / hGran);
                objGran = 17;
                m2Pint = Convert.ToDouble(valoresRatios.Rows[1][1].ToString());
                
                porcPint = Math.Round(hPint / totalH * 100, 2) ;
                prodPint =  Math.Round(m2Pint / hPint);
                objPint = 23;
                kgEmpaq = Convert.ToDouble(valoresRatios.Rows[2][1].ToString());
                
                porcEmpaq = Math.Round(hEmpaq / totalH * 100, 2) ;
                prodEmpaq =  Math.Round(kgEmpaq / hEmpaq);
                objEmpaq = 750;
                
                porcReproc = Math.Round(hReproc / totalH * 100, 2) ;
                
                porcCarr = Math.Round(hCarr / totalH * 100, 2) ;
                
                porcMat = Math.Round(hMat / totalH * 100, 2) ;
                
                porcEnmasc = Math.Round(hEnmasc / totalH * 100, 2) ;
                m2Empaq = Convert.ToDouble(valoresRatios.Rows[3][1].ToString());
                deficitEmpaq = 0;

                dgvRatios.Rows.Insert(dgvRatios.Rows.Count, counter.ToString(), m2Gran.ToString(), hGran.ToString(), porcGran.ToString() + '%', prodGran.ToString(), objGran.ToString(),
                    m2Pint.ToString(), hPint.ToString(), porcPint.ToString() + '%', prodPint.ToString(), objPint.ToString(), kgEmpaq.ToString(), hEmpaq.ToString(), porcEmpaq.ToString() + '%',
                    prodEmpaq.ToString(), objEmpaq.ToString(), hReproc.ToString(), porcReproc.ToString() + '%', hCarr.ToString(), porcCarr.ToString() + '%', hMat.ToString(),
                    porcMat.ToString() + '%', hEnmasc.ToString(), porcEnmasc.ToString() + '%', totalH.ToString(), m2Empaq.ToString(), deficitEmpaq.ToString());


                dia = dia.Date.AddDays(7);
                counter++;
            }

            dgvRatios.Columns[3].DefaultCellStyle.ForeColor = Color.Red;
            dgvRatios.Columns[4].DefaultCellStyle.BackColor = Color.Yellow;
            dgvRatios.Columns[5].DefaultCellStyle.BackColor = Color.Yellow;

            dgvRatios.Columns[8].DefaultCellStyle.ForeColor = Color.Red;
            dgvRatios.Columns[9].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
            dgvRatios.Columns[10].DefaultCellStyle.BackColor = Color.DeepSkyBlue;

            dgvRatios.Columns[13].DefaultCellStyle.ForeColor = Color.Red;
            dgvRatios.Columns[14].DefaultCellStyle.BackColor = Color.YellowGreen;
            dgvRatios.Columns[15].DefaultCellStyle.BackColor = Color.YellowGreen;
            dgvRatios.Columns[16].DefaultCellStyle.BackColor = Color.Orange;
            dgvRatios.Columns[17].DefaultCellStyle.ForeColor = Color.Red;
            
            dgvRatios.Columns[19].DefaultCellStyle.ForeColor = Color.Red;

            dgvRatios.Columns[21].DefaultCellStyle.ForeColor = Color.Red;

            dgvRatios.Columns[23].DefaultCellStyle.ForeColor = Color.Red;
            dgvRatios.Columns[24].DefaultCellStyle.BackColor = Color.Magenta;

            dgvRatios.Columns[26].DefaultCellStyle.BackColor = Color.YellowGreen;
            dgvRatios.Columns[27].HeaderCell.Style.BackColor = Color.Red;

            this.panelRatios.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            /*foreach (DataRow row in dbu.obtenerSuperficieChorreoSemana().Rows)
            {
                if (row[1].ToString() != "0.00")
                {
                    valorpunto = Convert.ToDouble(row[0].ToString()) / Convert.ToDouble(row[1].ToString());
                }
                else
                {
                    valorpunto = 0;
                }

                chartRatios.Series["M2/h"].Points.AddXY(cont, valorpunto);
                cont++;
            }*/

            calcularGrafico("pintura", Color.DeepSkyBlue);

        }

        private void calcularGrafico(string tipo, Color color)
        {
            chartRatios.Series.Clear();
            chartRatios.Titles.Clear();


            this.panel1.Visible = true;

            this.chartRatios.Titles.Add("PRODUCTIVIDAD " + tipo.ToUpper());
            this.chartRatios.Titles[0].BackColor = color;

            this.chartRatios.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;

            
            this.chartRatios.ChartAreas[0].AxisX.Minimum = 1;


            this.chartRatios.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;

           
            this.chartRatios.ChartAreas[0].AxisY.Minimum = 0;

            this.chartRatios.ChartAreas[0].AxisX.Title = "SEMANA";

            string title = "";
            if (tipo.Contains("Kg"))
            {
                title = "Kg/h";
                this.chartRatios.ChartAreas[0].AxisY.Maximum = 1500;
            }
            else
            {
                title = "M2/h";
                this.chartRatios.ChartAreas[0].AxisY.Maximum = 30;
            }

            this.chartRatios.ChartAreas[0].AxisY.Title = title;

            this.chartRatios.Series.Add(title);
            chartRatios.Series[title].Color = Color.ForestGreen;
            chartRatios.Series[title].ChartArea = "ChartArea1";
            chartRatios.Series[title].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int cont = 1;
            double valorpunto = 0;
            foreach (DataRow row in dbu.obtenerValorSemana(tipo).Rows)
            {
                if (row[1].ToString() != "0.00")
                {
                    valorpunto = Math.Round( Convert.ToDouble(row[0].ToString()) / Convert.ToDouble(row[1].ToString()), 2);
                }
                else
                {
                    valorpunto = 0;
                }

                chartRatios.Series[title].Points.AddXY(cont, valorpunto);
                cont++;
            }
            chartRatios.Series[title].BorderWidth = 2;
            chartRatios.Series[title].IsValueShownAsLabel = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            calcularGrafico("paquetesKg", Color.LimeGreen);
        }

        private void btnConsultarTraz_Click(object sender, EventArgs e)
        {
            this.dgvTraz.DataSource = dbu.obtenerTrazabilidadPiezasLanza(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dgvLotes.DataSource = dbu.obtenerValoresConsumoLotes(dtpDesdeLotes.Value, dtpHastaLotes.Value);
        }

        private void dgvLotes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void detallesLote()
        {
            this.paneDetallesLote.Visible = true;

            DateTime fDesdeDate = DateTime.ParseExact(dgvLotes.SelectedRows[0].Cells[3].Value.ToString(), "dd/MM/yyyy, HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime fHastaDate = DateTime.ParseExact(dgvLotes.SelectedRows[0].Cells[4].Value.ToString(), "dd/MM/yyyy, HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            this.dgvDetallesLote.DataSource = dbu.obtenerDatosMarcasLotes(fDesdeDate.ToString("yyyy-MM-dd HH:mm"), fHastaDate.ToString("yyyy-MM-dd HH:mm"), dgvLotes.SelectedRows[0].Cells[5].Value.ToString());

        }

        private void dgvLotes_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int colMouse = this.dgvLotes.HitTest(e.X, e.Y).ColumnIndex;
                int rowMouse = this.dgvLotes.HitTest(e.X, e.Y).RowIndex;

                
                dgvLotes.ClearSelection();
                this.dgvLotes.Rows[rowMouse].Cells[colMouse].Selected = true;

                ContextMenuStrip m = new ContextMenuStrip();
                m.Items.Add("DETALLES DE MARCAS");

                //int currentMouseOverRow = DGV.HitTest(e.X, e.Y).RowIndex;

                //if (currentMouseOverRow >= 0)
                //{
                //    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                //}

                m.Show(dgvLotes, new Point(e.X, e.Y));
                m.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);

            }
        }

        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            detallesLote();
        }

        private void dgvLotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            detallesLote();
        }

        private void btnCerrarMarcasLote_Click(object sender, EventArgs e)
        {
            this.paneDetallesLote.Visible = false;
        }

        private void btnBuscarLote_Click(object sender, EventArgs e)
        {
            if (tbLote.Text != "")
            {
                DataTable datosLote = new DataTable();
                datosLote = dbu.obtenerDatosLote(tbLote.Text);

                if (datosLote.Rows.Count > 0)
                {
                    this.labelLote.Text = datosLote.Rows[0]["LOTE"].ToString();
                    this.labelCodArt.Text = datosLote.Rows[0]["CODIGO"].ToString();
                    this.labelStock.Text = datosLote.Rows[0]["STOCK"].ToString();
                    this.labelCad.Text = datosLote.Rows[0]["FECHA_CAD"].ToString();

                    this.dgvPiezasInfoLote.DataSource = dbu.obtenerMarcasLote(tbLote.Text);
                }
                else
                {
                    MessageBox.Show("El lote introducido no existe o no es de un artículo de pintura. Introduzca uno que cumpla las condiciones", "LOTE NO ENCONTRADO");
                }
            }
        }

        private void tbLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscarLote.PerformClick();
            }
        }

        private void btnConsumido_Click(object sender, EventArgs e)
        {
            if (labelStock.Text != "0")
            {
                DialogResult dialogResult = MessageBox.Show("Se va a establecer el stock del lote a 0, ¿Continuar?", "AVISO", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    dbu.consumirStock(this.labelLote.Text, this.labelStock.Text);
                    btnBuscarLote.PerformClick();
                    MessageBox.Show("El stock del lote " + this.labelLote.Text + " se ha dado por consumido.", "LOTE CONSUMIDO");
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("El lote ya está consumido.", "ATENCIÓN");
            }
        }
        
    }
}
