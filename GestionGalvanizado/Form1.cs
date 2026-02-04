using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionGalvanizado
{
    public partial class Form1 : Form
    {

        private ConsultasBD fbd;
        public static DataTable dt;
        public static DataTable dtDiario;
        public static DataTable dtHoras;
        public static DataTable dtHoras2;
        public static DataTable dtProductividad;
        public static DataTable dtEst;

        int sumaNegro = 0;
        int sumaBlanco = 0;
        int sumaConsumo = 0;
        float porcConsumo = 0;
        float mediaMin = 0;
        float mediaMed = 0;
        float mediaMax = 0;
        float horasProductividad = 0;

        float sumaKGfichados = 0;

        float horasProd = 0;
        float horasLimpieza = 0;
        float horasMatas = 0;
        float horasReparaciones = 0;
        float horasUtiles = 0;
        float horasAverias = 0;
        float horasVarios = 0;

        int sumaNegroDia = 0;
        int sumaBlancoDia = 0;
        int sumaCantidad = 0;
        int sumaConsumoDia = 0;
        float sumaSuperficie = 0;
        float porcConsumoDia = 0;
        float mediaMinDia = 0;
        float mediaMedDia = 0;
        float mediaMaxDia = 0;
        public string marca = "";
        public string paquete = "";


        public Form1()
        {
            InitializeComponent();
            dt = new DataTable();

            if (Environment.UserName.Equals("victor.alvarez") || Environment.UserName.Equals("mario.diez") || Environment.UserName.Equals("felix.marcos") || Environment.UserName.Equals("susana.alonso") || Environment.UserName.Equals("carlosr.garcia") || Environment.UserName.Equals("franciscoj.lopez") || Environment.UserName.Equals("produccion.medeca") || Environment.UserName.Equals("angel.garcia"))
            {
                tabModificacion.Enabled = true;
                gbCategorias.Enabled = true;
            }
            else
            {
                tabModificacion.Enabled = false;
                gbCategorias.Enabled = false;
            }


            // DateTime diaAnterior = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);
            int dia = DateTime.Now.Day;
            DateTime diaAnterior;
            if (dia == 1)
            {
                diaAnterior = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            else
            {
                diaAnterior = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);
            }
            DateTime primerDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


            dateFiltroExcelMin.Value = diaAnterior;
            dateFiltroEntradaMin.Value = diaAnterior;
            dateFiltroMin.Value = primerDia;
            dateFiltroMin1.Value = primerDia;
            dateFiltroMin2.Value = primerDia;

            tBoxYear.Text = DateTime.Now.Year.ToString();
            tbAnioError.Text = DateTime.Now.Year.ToString();
            comboBox1.SelectedIndex = Convert.ToInt32(DateTime.Now.Month) - 1;
            comboBoxError.SelectedIndex = Convert.ToInt32(DateTime.Now.Month) - 1;

        }

        private void btn_consultar_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            sumaNegro = 0;
            sumaBlanco = 0;
            sumaConsumo = 0;
            porcConsumo = 0;
            mediaMin = 0;
            mediaMed = 0;
            mediaMax = 0;



            fbd = new ConsultasBD();

            string dateFormato = dateFiltro.Value.ToString("yyyy-MM-dd");
            dt = fbd.ObtenerInformacionCuelgue(dateFormato);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dt;
            dgInfoGeneralCuelgues.DataSource = bindingSource;


            // Ajustar tamaño de columnas y celdas.
            //dgInfoGeneralCuelgues.AutoResizeColumns();
            //dgInfoGeneralCuelgues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            dgInfoGeneralCuelgues.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgInfoGeneralCuelgues.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgInfoGeneralCuelgues.EnableHeadersVisualStyles = false;

            dgInfoGeneralCuelgues.Columns[0].Width = 75;
            dgInfoGeneralCuelgues.Columns[1].Width = 115;
            dgInfoGeneralCuelgues.Columns[2].Width = 65;

            dgInfoGeneralCuelgues.Columns[3].Width = 65;

            dgInfoGeneralCuelgues.Columns[4].Width = 82;
            dgInfoGeneralCuelgues.Columns[5].Width = 80;
            dgInfoGeneralCuelgues.Columns[6].Width = 70;

            dgInfoGeneralCuelgues.Columns[7].Width = 115;


            dgInfoGeneralCuelgues.Columns[8].Width = 80;


            dgInfoGeneralCuelgues.Columns[9].Width = 70;

            dgInfoGeneralCuelgues.Columns[10].Width = 80;
            dgInfoGeneralCuelgues.Columns[11].Width = 70;

            dgInfoGeneralCuelgues.Columns[12].Width = 115;

            dgInfoGeneralCuelgues.Columns[13].Width = 82;
            dgInfoGeneralCuelgues.Columns[14].Width = 82;
            dgInfoGeneralCuelgues.Columns[15].Width = 82;
            dgInfoGeneralCuelgues.Columns[16].Width = 60;
            dgInfoGeneralCuelgues.Columns[17].Width = 68;
            dgInfoGeneralCuelgues.Columns[18].Width = 80;



            dgInfoGeneralCuelgues.Columns[6].DefaultCellStyle.BackColor = Color.LightGray;
            dgInfoGeneralCuelgues.Columns[9].DefaultCellStyle.BackColor = Color.LightGray;
            dgInfoGeneralCuelgues.Columns[10].DefaultCellStyle.BackColor = Color.LightGray;
            dgInfoGeneralCuelgues.Columns[11].DefaultCellStyle.BackColor = Color.LightGray;
            dgInfoGeneralCuelgues.Columns[18].DefaultCellStyle.BackColor = Color.LightCoral;



            lb_Negro.Visible = true;
            lbBlanco.Visible = true;
            lbConsumo.Visible = true;
            lbPorcentaje.Visible = true;
            lbMin.Visible = true;
            lbMed.Visible = true;
            lbMax.Visible = true;


            foreach (DataGridViewRow row in dgInfoGeneralCuelgues.Rows)
            {

                sumaNegro += Convert.ToInt32(row.Cells[6].Value);
                sumaBlanco += Convert.ToInt32(row.Cells[9].Value);
                sumaConsumo += Convert.ToInt32(row.Cells[10].Value);

                porcConsumo += (float)Convert.ToDouble(row.Cells[11].Value);
                mediaMin += (float)Convert.ToDouble(row.Cells[13].Value);
                mediaMed += (float)Convert.ToDouble(row.Cells[14].Value);
                mediaMax += (float)Convert.ToDouble(row.Cells[15].Value);

            }



            lb_Negro.Text = sumaNegro.ToString();
            lbBlanco.Text = sumaBlanco.ToString();
            lbConsumo.Text = sumaConsumo.ToString();

            //lbPorcentaje.Text = (porcConsumo / dgInfoGeneralCuelgues.RowCount).ToString();
            if (sumaNegro > 0)
                lbPorcentaje.Text = Math.Round(((Convert.ToDouble(sumaConsumo) / Convert.ToDouble(sumaNegro)) * 100), 2).ToString();

            lbMin.Text = (mediaMin / dgInfoGeneralCuelgues.RowCount).ToString();
            lbMed.Text = (mediaMed / dgInfoGeneralCuelgues.RowCount).ToString();
            lbMax.Text = (mediaMax / dgInfoGeneralCuelgues.RowCount).ToString();

            Cursor.Current = Cursors.Default;


        }

        private void btn_Consultar_Modificar_Click(object sender, EventArgs e)
        {

            //OJO NO SE QUE OCURRE DATATABLE ERROR
            // DataTable dtMod = fbd.ObtenerInformacionAModificar(tboxIdCuelgueModificar.Text);
            //select IdEtiqueta,Marca,Paquete,PesoUnitario, CantidadOriginal, Expedicion,Camion,OrdenPaquete from tc_galv_Cuelgue_etiqueta where IdCuelgue = '17570'

            fbd = new ConsultasBD();
            DataTable dtMod = fbd.ObtenerInformacionAModificar(tboxIdCuelgueModificar.Text);

            if (!String.IsNullOrEmpty(tboxIdCuelgueModificar.Text))
            {
                tboxPesoPercha.Text = dtMod.Rows[0][0].ToString();
                tboxPesoUtiles.Text = dtMod.Rows[0][7].ToString();
                tboxPesoNegro.Text = dtMod.Rows[0][1].ToString();
                tboxPesoBlanco.Text = dtMod.Rows[0][6].ToString();
                tboxNumCuba.Text = dtMod.Rows[0][2].ToString();
                tboxMicrasMin.Text = dtMod.Rows[0][3].ToString();
                tboxMicrasMed.Text = dtMod.Rows[0][4].ToString();
                tboxMicrasMax.Text = dtMod.Rows[0][5].ToString();
                tBoxInversion.Text = dtMod.Rows[0][9].ToString();

                if (dtMod.Rows[0][8].ToString().Equals("Produccion"))
                {
                    rbProduccion.Checked = true;
                    rbRegalvanizado.Checked = false;
                    rbRefluxado.Checked = false;
                }

                if (dtMod.Rows[0][8].ToString().Equals("Regalvanizado"))
                {
                    rbProduccion.Checked = false;
                    rbRegalvanizado.Checked = true;
                    rbRefluxado.Checked = false;

                }

                if (dtMod.Rows[0][8].ToString().Equals("Refluxado"))
                {
                    rbProduccion.Checked = false;
                    rbRegalvanizado.Checked = false;
                    rbRefluxado.Checked = true;
                }

                DataTable dtContenido = new DataTable();

                dtContenido = fbd.ObtenerContenidoCuelgue(tboxIdCuelgueModificar.Text);
                var bindingSource = new System.Windows.Forms.BindingSource();
                bindingSource.DataSource = dtContenido;

                dgContenidoPercha.DataSource = bindingSource;

                dgContenidoPercha.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

                dgContenidoPercha.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

                dgContenidoPercha.EnableHeadersVisualStyles = false;

                dgContenidoPercha.Columns[0].Width = 90;
                dgContenidoPercha.Columns[1].Width = 160;
                dgContenidoPercha.Columns[2].Width = 90;

                dgContenidoPercha.Columns[3].Width = 90;

                dgContenidoPercha.Columns[4].Width = 100;
                dgContenidoPercha.Columns[5].Width = 90;
                dgContenidoPercha.Columns[6].Width = 90;

                dgContenidoPercha.Columns[7].Width = 95;


            }

            //dgContenidoPercha.AutoResizeColumns();
            //dgContenidoPercha.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


        }




        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            tboxIdCuelgueModificar.Text = "";
            tboxPesoPercha.Text = "";
            tboxPesoUtiles.Text = "";
            tboxPesoNegro.Text = "";
            tboxPesoBlanco.Text = "";
            tboxNumCuba.Text = "";
            tboxMicrasMin.Text = "";
            tboxMicrasMed.Text = "";
            tboxMicrasMax.Text = "";
            tBoxInversion.Text = "";
            rbProduccion.Checked = false;
            rbRegalvanizado.Checked = false;
            rbRefluxado.Checked = false;
            dgContenidoPercha.Columns.Clear();

        }


        private void btn_Modificar_Click(object sender, EventArgs e)
        {

            // fbd.modificarDatosCuelgue(tboxIdCuelgueModificar.Text, Convert.ToInt32(tboxPesoPercha.Text), Convert.ToInt32(tboxPesoNegro.Text), Convert.ToInt32(tboxNumCuba.Text), Convert.ToInt32(tboxMicrasMin.Text), Convert.ToInt32(tboxMicrasMed.Text),Convert.ToInt32( tboxMicrasMax.Text), Convert.ToInt32(tboxPesoBlanco.Text), Convert.ToInt32(tboxPesoUtiles.Text));
            //fbd.modificarDatosCuelgue(tboxIdCuelgueModificar.Text, Convert.ToInt32(tboxPesoPercha.Text), Convert.ToInt32(tboxPesoNegro.Text), Convert.ToInt32(tboxNumCuba.Text), Convert.ToInt32(tboxMicrasMin.Text), Convert.ToInt32(tboxMicrasMed.Text), Convert.ToInt32(tboxMicrasMed), Convert.ToInt32(tboxMicrasMax), Convert.ToInt32(tboxPesoUtiles.Text));
            // fbd.modificarDatosCuelgueDos(tboxIdCuelgueModificar.Text, tboxPesoPercha.Text, tboxPesoNegro.Text, tboxNumCuba.Text, tboxMicrasMin.Text, tboxMicrasMed.Text, tboxMicrasMax.Text, tboxPesoBlanco.Text,tboxPesoUtiles.Text);

            if (!String.IsNullOrEmpty(tboxIdCuelgueModificar.Text))
            {
                string tipo = "";

                if (rbProduccion.Checked.Equals(true))
                {
                    tipo = "Produccion";
                }
                if (rbRegalvanizado.Checked.Equals(true))
                {
                    tipo = rbRegalvanizado.Text;
                }
                if (rbRefluxado.Checked.Equals(true))
                {
                    tipo = rbRefluxado.Text;
                }


                fbd.modificarDatosCuelgue(tboxIdCuelgueModificar.Text, tboxPesoPercha.Text.Substring(0, tboxPesoPercha.Text.Length - 3), tboxPesoNegro.Text.Substring(0, tboxPesoNegro.Text.Length - 3), tboxNumCuba.Text, tboxMicrasMin.Text, tboxMicrasMed.Text, tboxMicrasMax.Text, tboxPesoBlanco.Text.Substring(0, tboxPesoBlanco.Text.Length - 3), tboxPesoUtiles.Text.Substring(0, tboxPesoUtiles.Text.Length - 3), tipo, tBoxInversion.Text);


                MessageBox.Show("Se ha modificado el cuelgue", "Cuelgue Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }


        private void btnConsultarDiario_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            fbd = new ConsultasBD();



            sumaNegroDia = 0;
            sumaCantidad = 0;
            sumaBlancoDia = 0;
            sumaConsumoDia = 0;
            porcConsumoDia = 0;
            sumaSuperficie = 0;
            mediaMinDia = 0;
            mediaMedDia = 0;
            mediaMaxDia = 0;

            sumaKGfichados = 0;

            string dateFormatoMin = dateFiltroMin.Value.ToString("yyyy-MM-dd");
            string dateFormatoMax = dateFiltroMax.Value.ToString("yyyy-MM-dd");

            dtDiario = fbd.ObtenerInformacionDiaria(dateFormatoMin, dateFormatoMax);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtDiario;
            dgInfoDiaria.DataSource = bindingSource;

            dgInfoDiaria.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgInfoDiaria.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgInfoDiaria.EnableHeadersVisualStyles = false;


            /*dgInfoDiaria.Columns[0].Width = 160;
            dgInfoDiaria.Columns[1].Width = 160;
            dgInfoDiaria.Columns[2].Width = 160;

            dgInfoDiaria.Columns[3].Width = 160;

            dgInfoDiaria.Columns[4].Width = 160;
            dgInfoDiaria.Columns[5].Width = 160;
            dgInfoDiaria.Columns[6].Width = 160;

            dgInfoDiaria.Columns[7].Width = 160;

            dgInfoDiaria.Columns[8].Width = 160;

            dgInfoDiaria.Columns[9].Width = 160;
            dgInfoDiaria.Columns[10].Width = 160;*/

            dgInfoDiaria.Columns[4].DefaultCellStyle.Format = "0.##";



            lbNegroDia.Visible = true;
            lbBlancoDia.Visible = true;
            lbPorcDia.Visible = true;
            lbMinDia.Visible = true;
            lbMedDia.Visible = true;
            lbMaxDia.Visible = true;
            lbCtd.Visible = true;
            lbSup.Visible = true;
            kgFichados.Visible = true;


            foreach (DataGridViewRow row in dgInfoDiaria.Rows)
            {
                if (!String.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                {
                    sumaNegroDia += Convert.ToInt32(row.Cells[1].Value);
                }

                if (!String.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                {
                    sumaBlancoDia += Convert.ToInt32(row.Cells[3].Value);
                }

                if (!String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                {
                    sumaKGfichados += Convert.ToInt32(row.Cells[2].Value);
                }

                if (!String.IsNullOrEmpty(row.Cells[10].Value.ToString()))
                {
                    sumaCantidad += Convert.ToInt32(row.Cells[10].Value);
                }
                //sumaConsumo += Convert.ToInt32(row.Cells[10].Value);


                if (!String.IsNullOrEmpty(row.Cells[5].Value.ToString()))
                {
                    porcConsumoDia += (float)Convert.ToDouble(row.Cells[5].Value);
                }

                if (!String.IsNullOrEmpty(row.Cells[6].Value.ToString()))
                {
                    mediaMinDia += (float)Convert.ToDouble(row.Cells[6].Value);
                }

                if (!String.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                {
                    mediaMedDia += (float)Convert.ToDouble(row.Cells[7].Value);
                }

                if (!String.IsNullOrEmpty(row.Cells[8].Value.ToString()))
                {
                    mediaMaxDia += (float)Convert.ToDouble(row.Cells[8].Value);
                }

                if (!String.IsNullOrEmpty(row.Cells[11].Value.ToString()))
                {
                    sumaSuperficie += (float)Convert.ToDouble(row.Cells[11].Value.ToString().Replace(" m²", ""));
                }

            }



            lbNegroDia.Text = sumaNegroDia.ToString();
            lbBlancoDia.Text = sumaBlancoDia.ToString();
            lbCtd.Text = sumaCantidad.ToString();
            //lbConsumo.Text = sumaConsumo.ToString();

            kgFichados.Text = sumaKGfichados.ToString();

            lbPorcDia.Text = Math.Round((porcConsumoDia / dgInfoDiaria.RowCount), 2).ToString();
            lbMinDia.Text = (mediaMinDia / dgInfoDiaria.RowCount).ToString();
            lbMedDia.Text = (mediaMedDia / dgInfoDiaria.RowCount).ToString();
            lbMaxDia.Text = (mediaMaxDia / dgInfoDiaria.RowCount).ToString();
            lbSup.Text = sumaSuperficie.ToString() + " m²";



            //dgInfoDiaria.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            //dgInfoDiaria.EnableHeadersVisualStyles = false;
            //dgInfoDiaria.RowHeadersVisible = false;

            // Ajustar tamaño de columnas y celdas.
            // dgInfoDiaria.AutoResizeColumns();
            // dgInfoDiaria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            Cursor.Current = Cursors.Default;

        }

        private void btnConsultaEst_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            fbd = new ConsultasBD();

            string mes = "";


            if (comboBox1.Text.Equals("Enero"))
            {
                mes = "1";
            }
            else if (comboBox1.Text.Equals("Febrero"))
            {
                mes = "2";
            }
            else if (comboBox1.Text.Equals("Marzo"))
            {
                mes = "3";
            }
            else if (comboBox1.Text.Equals("Abril"))
            {
                mes = "4";
            }
            else if (comboBox1.Text.Equals("Mayo"))
            {
                mes = "5";
            }
            else if (comboBox1.Text.Equals("Junio"))
            {
                mes = "6";
            }
            else if (comboBox1.Text.Equals("Julio"))
            {
                mes = "7";
            }
            else if (comboBox1.Text.Equals("Agosto"))
            {
                mes = "8";
            }
            else if (comboBox1.Text.Equals("Septiembre"))
            {
                mes = "9";
            }
            else if (comboBox1.Text.Equals("Octubre"))
            {
                mes = "10";
            }
            else if (comboBox1.Text.Equals("Noviembre"))
            {
                mes = "11";
            }
            else if (comboBox1.Text.Equals("Diciembre"))
            {
                mes = "12";
            }

            dtEst = fbd.ObtenerTiposPerchas(tBoxYear.Text, mes);

            if (dtEst.Rows.Count > 0)
            {

                resulHorizontal.Text = dtEst.Rows[0][0].ToString();
                resulVertical.Text = dtEst.Rows[1][0].ToString();
                resulTotal.Text = (Convert.ToInt32(dtEst.Rows[0][0].ToString()) + Convert.ToInt32(dtEst.Rows[1][0].ToString())).ToString() + "  cuelgues";


                DataTable dtEstadisticas = new DataTable();

                dtEstadisticas = fbd.obtenerInfoEstadisticas(tBoxYear.Text, mes);


                DataTable dtTipoMaterial = fbd.obtenerTipoMaterial();

                /*
                lbAngKg.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[0][1]), 2).ToString();
                lbChapasKg.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[1][1]), 2).ToString();
                lbTubKg.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[2][1]), 2).ToString();
                lbOtrosKg.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[3][1]), 2).ToString();
                lbTotalKg.Text = (Convert.ToDouble(lbAngKg.Text) + Convert.ToDouble(lbChapasKg.Text) + Convert.ToDouble(lbTubKg.Text) + Convert.ToDouble(lbOtrosKg.Text)).ToString();
                //lbTotalKg.Text = (Convert.ToDouble(dtTipoMaterial.Rows[0][1].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[1][1].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[2][1].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[3][1].ToString())).ToString();


                //---------------------------------------------------------------
                lbAngTm.Text = Math.Round((Convert.ToDouble(lbAngKg.Text) / 1000),0).ToString();
                lbChapasTm.Text = Math.Round((Convert.ToDouble(lbChapasKg.Text) / 1000),0).ToString();
                lbTubTm.Text = Math.Round((Convert.ToDouble(lbTubKg.Text) / 1000),0).ToString();
                lbOtrosTm.Text = Math.Round((Convert.ToDouble(lbOtrosKg.Text) / 1000),0).ToString();
                lbTotalTm.Text = Math.Round((Convert.ToDouble(lbTotalKg.Text) / 1000), 0).ToString();

                //---------------------------------------------------------------

                lbAngPorc.Text = Math.Round(((Convert.ToDouble(lbAngKg.Text) * 100) / Convert.ToDouble(lbTotalKg.Text)), 2).ToString();
                lbChapasPorc.Text = Math.Round(((Convert.ToDouble(lbChapasKg.Text) * 100) / Convert.ToDouble(lbTotalKg.Text)), 2).ToString();
                lbTubPorc.Text = Math.Round(((Convert.ToDouble(lbTubKg.Text) * 100) / Convert.ToDouble(lbTotalKg.Text)), 2).ToString();
                lbOtrosPorc.Text = Math.Round(((Convert.ToDouble(lbOtrosKg.Text) * 100) / Convert.ToDouble(lbTotalKg.Text)), 2).ToString();
                */

                //KILOGRAMOS
                lbAngKg.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[0][1]), 2).ToString();
                lbChapasKg.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[1][1]), 2).ToString();
                lbTubKg.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[2][1]), 2).ToString();
                lbOtrosKg.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[3][1]), 2).ToString();
                lbTotalKg.Text = Math.Round((Convert.ToDouble(dtTipoMaterial.Rows[0][1].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[1][1].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[2][1].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[3][1].ToString())), 2).ToString();

                //TONELADAS
                lbAngTm.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[0][2]), 2).ToString();
                lbChapasTm.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[1][2]), 2).ToString();
                lbTubTm.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[2][2]), 2).ToString();
                lbOtrosTm.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[3][2]), 2).ToString();
                lbTotalTm.Text = (Convert.ToDouble(dtTipoMaterial.Rows[0][2].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[1][2].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[2][2].ToString()) + Convert.ToDouble(dtTipoMaterial.Rows[3][2].ToString())).ToString();

                //Porcentaje

                lbAngPorc.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[0][3]), 2).ToString();
                lbChapasPorc.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[1][3]), 2).ToString();
                lbTubPorc.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[2][3]), 2).ToString();
                lbOtrosPorc.Text = Math.Round(Convert.ToDouble(dtTipoMaterial.Rows[3][3]), 2).ToString();




                DataTable dtProdPres = new DataTable();

                dtProdPres = fbd.ObtenerProdMes();

                var bindingSource = new System.Windows.Forms.BindingSource();
                bindingSource.DataSource = dtProdPres;
                dataGridEst.DataSource = bindingSource;




                dataGridEst.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

                dataGridEst.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

                dataGridEst.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
                dataGridEst.EnableHeadersVisualStyles = false;
                dataGridEst.RowHeadersVisible = false;


                dataGridEst.Columns[0].Width = 160;
                dataGridEst.Columns[1].Width = 160;
                dataGridEst.Columns[2].Width = 160;

                dataGridEst.Columns[3].Width = 160;
                dataGridEst.Columns[4].Width = 160;
                dataGridEst.Columns[5].Width = 160;

                dataGridEst.Columns[1].DefaultCellStyle.Format = "0.##";
                dataGridEst.Columns[3].DefaultCellStyle.Format = "0.##";
                dataGridEst.Columns[4].DefaultCellStyle.Format = "0.##";




                //ANGULARES
                DataTable dtAngulares = new DataTable();
                double sumaAngularKg = 0.00;
                int sumaAngularTm = 0;

                dtAngulares = fbd.ObtenerAngulares();

                var bindingSourceAng = new System.Windows.Forms.BindingSource();
                bindingSourceAng.DataSource = dtAngulares;
                dataGridAngular.DataSource = bindingSourceAng;

                dataGridAngular.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

                dataGridAngular.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

                dataGridAngular.EnableHeadersVisualStyles = false;

                dataGridAngular.Columns[1].DefaultCellStyle.Format = "0.##";
                dataGridAngular.Columns[3].DefaultCellStyle.Format = "0.##";
                dataGridAngular.Columns[4].DefaultCellStyle.Format = "0.##";

                lbKgTituloAngular.Text = lbAngKg.Text + " Kg";
                lbTmTituloAngular.Text = lbAngTm.Text + " Tm";

                foreach (DataGridViewRow row in dataGridAngular.Rows)
                {
                    sumaAngularKg += (double)Convert.ToDouble(row.Cells[1].Value);
                    sumaAngularTm += (Int32)Convert.ToDouble(row.Cells[2].Value);
                }

                lbAngularTablasKg.Text = sumaAngularKg.ToString() + " Kg"; ;
                lbAngularTablasTm.Text = sumaAngularTm.ToString() + " Tm";

                //CHAPAS
                DataTable dtChapas = new DataTable();
                double sumaChapasKg = 0.00;
                int sumaChapasTm = 0;

                dtChapas = fbd.ObtenerChapas();

                var bindingSourceCh = new System.Windows.Forms.BindingSource();
                bindingSourceCh.DataSource = dtChapas;
                dataGridChapas.DataSource = bindingSourceCh;



                dataGridChapas.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

                dataGridChapas.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

                dataGridChapas.EnableHeadersVisualStyles = false;

                dataGridChapas.Columns[1].DefaultCellStyle.Format = "0.##";
                dataGridChapas.Columns[3].DefaultCellStyle.Format = "0.##";
                dataGridChapas.Columns[4].DefaultCellStyle.Format = "0.##";

                lbKgTituloChapas.Text = lbChapasKg.Text + " Kg";
                lbTmTituloChapas.Text = lbChapasTm.Text + " Tm";

                foreach (DataGridViewRow row in dataGridChapas.Rows)
                {
                    sumaChapasKg += (double)Convert.ToDouble(row.Cells[1].Value);
                    sumaChapasTm += (Int32)Convert.ToDouble(row.Cells[2].Value);
                }

                lbChapasTablasKg.Text = sumaChapasKg.ToString() + " Kg"; ;
                lbChapasTablasTm.Text = sumaChapasTm.ToString() + " Tm";



                //TUBOS
                DataTable dtTubos = new DataTable();
                double sumaTubosKg = 0.00;
                int sumaTubosTm = 0;


                dtTubos = fbd.ObtenerTubos();

                var bindingSourceTub = new System.Windows.Forms.BindingSource();
                bindingSourceTub.DataSource = dtTubos;
                dataGridTubos.DataSource = bindingSourceTub;


                dataGridTubos.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

                dataGridTubos.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

                dataGridTubos.EnableHeadersVisualStyles = false;


                dataGridTubos.Columns[1].DefaultCellStyle.Format = "0.##";
                dataGridTubos.Columns[3].DefaultCellStyle.Format = "0.##";
                dataGridTubos.Columns[4].DefaultCellStyle.Format = "0.##";

                lbKgTituloTubos.Text = lbTubKg.Text + " Kg";
                lbTmTituloTubos.Text = lbTubTm.Text + " Tm";


                foreach (DataGridViewRow row in dataGridTubos.Rows)
                {
                    sumaTubosKg += (double)Convert.ToDouble(row.Cells[1].Value);
                    sumaTubosTm += (Int32)Convert.ToDouble(row.Cells[2].Value);
                }

                lbTubosTablasKg.Text = sumaTubosKg.ToString() + " Kg"; ;
                lbTubosTablasTm.Text = sumaTubosTm.ToString() + " Tm";



            }
            Cursor.Current = Cursors.Default;

        }

        private void btnObtenerCuelgue_Click(object sender, EventArgs e)
        {

            fbd = new ConsultasBD();


            DataTable dtEspesores = new DataTable();


            dtEspesores = fbd.ObtenerCuelguesEspesor(comboBox2.Text);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtEspesores;
            datagridConsultaPorEspesor.DataSource = bindingSource;



            datagridConsultaPorEspesor.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            datagridConsultaPorEspesor.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            datagridConsultaPorEspesor.EnableHeadersVisualStyles = false;



        }

        private void btn_consultar_error_Click(object sender, EventArgs e)
        {

            fbd = new ConsultasBD();

            string mes = "";


            if (comboBoxError.Text.Equals("Enero"))
            {
                mes = "1";
            }
            else if (comboBoxError.Text.Equals("Febrero"))
            {
                mes = "2";
            }
            else if (comboBoxError.Text.Equals("Marzo"))
            {
                mes = "3";
            }
            else if (comboBoxError.Text.Equals("Abril"))
            {
                mes = "4";
            }
            else if (comboBoxError.Text.Equals("Mayo"))
            {
                mes = "5";
            }
            else if (comboBoxError.Text.Equals("Junio"))
            {
                mes = "6";
            }
            else if (comboBoxError.Text.Equals("Julio"))
            {
                mes = "7";
            }
            else if (comboBoxError.Text.Equals("Agosto"))
            {
                mes = "8";
            }
            else if (comboBoxError.Text.Equals("Septiembre"))
            {
                mes = "9";
            }
            else if (comboBoxError.Text.Equals("Octubre"))
            {
                mes = "10";
            }
            else if (comboBoxError.Text.Equals("Noviembre"))
            {
                mes = "11";
            }
            else if (comboBoxError.Text.Equals("Diciembre"))
            {
                mes = "12";
            }

            DataTable dtError = new DataTable();
            dtError = fbd.ObtenerCuelguesError(tBoxYear.Text, mes);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtError;
            dataGridError.DataSource = bindingSource;



            dataGridError.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dataGridError.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dataGridError.EnableHeadersVisualStyles = false;

            dataGridError.Columns[0].Width = 75;
            dataGridError.Columns[1].Width = 75;
            dataGridError.Columns[2].Width = 85;

            dataGridError.Columns[3].Width = 75;

            dataGridError.Columns[4].Width = 75;
            dataGridError.Columns[5].Width = 75;
            dataGridError.Columns[6].Width = 75;

            dataGridError.Columns[7].Width = 75;


            dataGridError.Columns[8].Width = 130;


            dataGridError.Columns[9].Width = 75;

            dataGridError.Columns[10].Width = 75;
            dataGridError.Columns[11].Width = 75;

            dataGridError.Columns[12].Width = 115;

            dataGridError.Columns[13].Width = 100;
            dataGridError.Columns[14].Width = 335;

            dataGridError.Columns[14].DefaultCellStyle.BackColor = Color.LightCoral;


        }

        private void btnTrazMarca_Click(object sender, EventArgs e)
        {
            fbd = new ConsultasBD();

            DataTable dtCuelguesMarca = new DataTable();
            marca = tbTrazMarca.Text;
            dtCuelguesMarca = fbd.ObtenerCuelguesPorMarca(tbTrazMarca.Text);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtCuelguesMarca;
            dgTablaMarca.DataSource = bindingSource;


            dgTablaMarca.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgTablaMarca.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            dgTablaMarca.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            dgTablaMarca.EnableHeadersVisualStyles = false;





        }

        private void dgTablaMarca_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fbd = new ConsultasBD();

            int selectedrowindex = -1;
            DataGridViewRow selectedRow;
            int cuelgue = 0;


            DataTable dtCuelgueIndividual = new DataTable();

            selectedrowindex = dgTablaMarca.SelectedCells[0].RowIndex;
            selectedRow = dgTablaMarca.Rows[selectedrowindex];
            cuelgue = Convert.ToInt32(selectedRow.Cells[0].Value);

            dtCuelgueIndividual = fbd.ObtenerInformacionCuelgueIndividual(cuelgue);


            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtCuelgueIndividual;
            dgIndividualMarca.DataSource = bindingSource;


            dgIndividualMarca.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgIndividualMarca.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgIndividualMarca.EnableHeadersVisualStyles = false;

            dgIndividualMarca.Columns[0].Width = 75;
            dgIndividualMarca.Columns[1].Width = 115;
            dgIndividualMarca.Columns[2].Width = 65;

            dgIndividualMarca.Columns[3].Width = 65;

            dgIndividualMarca.Columns[4].Width = 82;
            dgIndividualMarca.Columns[5].Width = 80;
            dgIndividualMarca.Columns[6].Width = 70;

            dgIndividualMarca.Columns[7].Width = 115;


            dgIndividualMarca.Columns[8].Width = 80;


            dgIndividualMarca.Columns[9].Width = 70;

            dgIndividualMarca.Columns[10].Width = 80;
            dgIndividualMarca.Columns[11].Width = 70;

            dgIndividualMarca.Columns[12].Width = 115;

            dgIndividualMarca.Columns[13].Width = 82;
            dgIndividualMarca.Columns[14].Width = 82;
            dgIndividualMarca.Columns[15].Width = 82;
            dgIndividualMarca.Columns[16].Width = 45;
            dgIndividualMarca.Columns[17].Width = 68;
            dgIndividualMarca.Columns[18].Width = 80;



            dgIndividualMarca.Columns[6].DefaultCellStyle.BackColor = Color.LightGray;
            dgIndividualMarca.Columns[9].DefaultCellStyle.BackColor = Color.LightGray;
            dgIndividualMarca.Columns[10].DefaultCellStyle.BackColor = Color.LightGray;
            dgIndividualMarca.Columns[11].DefaultCellStyle.BackColor = Color.LightGray;
            dgIndividualMarca.Columns[18].DefaultCellStyle.BackColor = Color.LightCoral;



            DataTable dtContenido = new DataTable();

            dtContenido = fbd.ObtenerContenidoCuelgue(cuelgue.ToString());
            var bindingSourceDos = new System.Windows.Forms.BindingSource();
            bindingSourceDos.DataSource = dtContenido;



            dgContenidoIndividualMarca.DataSource = bindingSourceDos;

            dgContenidoIndividualMarca.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgContenidoIndividualMarca.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgContenidoIndividualMarca.EnableHeadersVisualStyles = false;


            foreach (DataGridViewRow row in dgContenidoIndividualMarca.Rows)
            {

                if (row.Cells[1].Value.ToString().Equals(marca))
                {

                    row.DefaultCellStyle.BackColor = Color.LightGray;

                }

            }

        }

        private void btnTrazPaq_Click(object sender, EventArgs e)
        {

            fbd = new ConsultasBD();

            DataTable dtCuelguesPaq = new DataTable();
            paquete = tbTrazPaq.Text;
            dtCuelguesPaq = fbd.ObtenerCuelguesPorPaquete(tbTrazPaq.Text);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtCuelguesPaq;
            dgTablaPaq.DataSource = bindingSource;


            dgTablaPaq.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgTablaPaq.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            dgTablaPaq.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            dgTablaPaq.EnableHeadersVisualStyles = false;



        }

        private void dgTablaPaq_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            fbd = new ConsultasBD();

            int selectedrowindex = -1;
            DataGridViewRow selectedRow;
            int cuelgue = 0;


            DataTable dtCuelgueIndividualPaq = new DataTable();

            selectedrowindex = dgTablaPaq.SelectedCells[0].RowIndex;
            selectedRow = dgTablaPaq.Rows[selectedrowindex];
            cuelgue = Convert.ToInt32(selectedRow.Cells[0].Value);

            dtCuelgueIndividualPaq = fbd.ObtenerInformacionCuelgueIndividual(cuelgue);


            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtCuelgueIndividualPaq;
            dgIndividualPaq.DataSource = bindingSource;


            dgIndividualPaq.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgIndividualPaq.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgIndividualPaq.EnableHeadersVisualStyles = false;

            dgIndividualPaq.Columns[0].Width = 75;
            dgIndividualPaq.Columns[1].Width = 115;
            dgIndividualPaq.Columns[2].Width = 65;

            dgIndividualPaq.Columns[3].Width = 65;

            dgIndividualPaq.Columns[4].Width = 82;
            dgIndividualPaq.Columns[5].Width = 80;
            dgIndividualPaq.Columns[6].Width = 70;

            dgIndividualPaq.Columns[7].Width = 115;


            dgIndividualPaq.Columns[8].Width = 80;


            dgIndividualPaq.Columns[9].Width = 70;

            dgIndividualPaq.Columns[10].Width = 80;
            dgIndividualPaq.Columns[11].Width = 70;

            dgIndividualPaq.Columns[12].Width = 115;

            dgIndividualPaq.Columns[13].Width = 82;
            dgIndividualPaq.Columns[14].Width = 82;
            dgIndividualPaq.Columns[15].Width = 82;
            dgIndividualPaq.Columns[16].Width = 45;
            dgIndividualPaq.Columns[17].Width = 68;
            dgIndividualPaq.Columns[18].Width = 80;



            dgIndividualPaq.Columns[6].DefaultCellStyle.BackColor = Color.LightGray;
            dgIndividualPaq.Columns[9].DefaultCellStyle.BackColor = Color.LightGray;
            dgIndividualPaq.Columns[10].DefaultCellStyle.BackColor = Color.LightGray;
            dgIndividualPaq.Columns[11].DefaultCellStyle.BackColor = Color.LightGray;
            dgIndividualPaq.Columns[18].DefaultCellStyle.BackColor = Color.LightCoral;



            DataTable dtContenidoPaq = new DataTable();

            dtContenidoPaq = fbd.ObtenerContenidoCuelgue(cuelgue.ToString());
            var bindingSourceDos = new System.Windows.Forms.BindingSource();
            bindingSourceDos.DataSource = dtContenidoPaq;



            dgContenidoIndividuaPaq.DataSource = bindingSourceDos;

            dgContenidoIndividuaPaq.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgContenidoIndividuaPaq.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgContenidoIndividuaPaq.EnableHeadersVisualStyles = false;


            foreach (DataGridViewRow row in dgContenidoIndividuaPaq.Rows)
            {

                if (row.Cells[2].Value.ToString().Equals(paquete))
                {

                    row.DefaultCellStyle.BackColor = Color.LightGray;

                }

            }




        }

        private void btnConsControl_Click(object sender, EventArgs e)
        {
            string dateFormatoMin = dateFiltroEntradaMin.Value.ToString("MM/dd/yyyy");
            string dateFormatoMax = dateTimePicker1.Value.ToString("MM/dd/yyyy");

            fbd = new ConsultasBD();

            DataTable dtControlEntrada = new DataTable();

            dtControlEntrada = fbd.ObtenerControlEntrada(dateFormatoMin, dateFormatoMax);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtControlEntrada;
            dgControlEntrada.DataSource = bindingSource;



            dgControlEntrada.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgControlEntrada.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgControlEntrada.EnableHeadersVisualStyles = false;
            dgControlEntrada.RowHeadersVisible = false;


        }

        private void button2_Click(object sender, EventArgs e)
        {

            string dateFormatoMin = dateFiltroExcelMin.Value.ToString("MM/dd/yyyy");
            string dateFormatoMax = dateTimePicker3.Value.ToString("MM/dd/yyyy");

            fbd = new ConsultasBD();

            DataTable dtExpExcel = new DataTable();

            dtExpExcel = fbd.ObtenerExportacionExcel(dateFormatoMin, dateFormatoMax);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtExpExcel;
            dgExportarExcel.DataSource = bindingSource;


            dgExportarExcel.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgExportarExcel.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgExportarExcel.EnableHeadersVisualStyles = false;
            dgExportarExcel.RowHeadersVisible = false;



        }

        private void btnObtenerPaquetes_Click(object sender, EventArgs e)
        {
            fbd = new ConsultasBD();

            string camion = tboxCamion.Text;
            string cb_expedicion = fbd.obtenerCBExpedicion(camion);

            DataTable dtPaqCam = new DataTable();

            dtPaqCam = fbd.ObtenerPaquetesCamion(cb_expedicion);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtPaqCam;
            dgPaqCam.DataSource = bindingSource;

            dgPaqCam.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgPaqCam.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgPaqCam.EnableHeadersVisualStyles = false;
            dgPaqCam.RowHeadersVisible = false;



        }

        private void btnConsultarProductividad_Click(object sender, EventArgs e)
        {

            horasProductividad = 0;

            fbd = new ConsultasBD();

            string dateFormatoMin = dateFiltroMin1.Value.ToString("yyyy-MM-dd");
            string dateFormatoMax = dateFiltroMax1.Value.ToString("yyyy-MM-dd");

            // dtHoras = fbd.ObtenerInformacionProductividad(dateFormatoMin, dateFormatoMax);
            dtHoras = fbd.ObtenerInformacionProductividadConsultar(dateFormatoMin, dateFormatoMax);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtHoras;
            dgHorasOperarios.DataSource = bindingSource;

            dgHorasOperarios.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgHorasOperarios.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgHorasOperarios.EnableHeadersVisualStyles = false;

            /*
            dgHorasOperarios.Columns[0].Width = 60;
            dgHorasOperarios.Columns[1].Visible = false;
            dgHorasOperarios.Columns[2].Width = 120;
            dgHorasOperarios.Columns[3].Width = 60;
            dgHorasOperarios.Columns[4].Visible = false;
            */

            dgHorasOperarios.Columns[0].Width = 60;
            dgHorasOperarios.Columns[1].Width = 120;
            dgHorasOperarios.Columns[2].Width = 60;


            foreach (DataGridViewRow row in dgHorasOperarios.Rows)
            {
                horasProductividad += (float)Convert.ToDouble(row.Cells[2].Value);

            }

            lbHorasTotales.Text = "Horas Totales Productivas: " + horasProductividad;

            dtProductividad = fbd.ObtenerResultadoProductividad(dateFormatoMin, dateFormatoMax);


            var bindingSourceDos = new System.Windows.Forms.BindingSource();
            bindingSourceDos.DataSource = dtProductividad;
            dgProductividad.DataSource = bindingSourceDos;

            dgProductividad.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#ADEBC3");

            dgProductividad.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            dgProductividad.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            dgProductividad.EnableHeadersVisualStyles = false;



            /* dgProductividad.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
             dgProductividad.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
             dgProductividad.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
             dgProductividad.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11);
             dgProductividad.EnableHeadersVisualStyles = false;
             dgProductividad.RowTemplate.Height = 40;
             dgProductividad.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
             dgProductividad.ReadOnly = true;*/


            int sumaNegro = 0;
            int sumaBlanco = 0;

            foreach (DataGridViewRow row in dgProductividad.Rows)
            {
                sumaNegro += Convert.ToInt32(row.Cells[1].Value);
                sumaBlanco += Convert.ToInt32(row.Cells[2].Value);

            }


            lbResNegroProductividad.Text = sumaNegro.ToString();
            lbResBlancoProductividad.Text = sumaBlanco.ToString();




            /*dtProductividad = fbd.ObtenerInformacionDiaria(dateFormatoMin, dateFormatoMax);


            int sumaNegro = 0;
            int sumaBlanco = 0;
            foreach (DataRow dr in dtProductividad.Rows)
            {
                sumaNegro += Convert.ToInt32(dr[1]);
                sumaBlanco += Convert.ToInt32(dr[2]);
            }

            lbResNegroProductividad.Text = sumaNegro.ToString();
            lbResBlancoProductividad.Text = sumaBlanco.ToString();
            */

        }

        private void btnConsultarProductividad1_Click(object sender, EventArgs e)
        {
            fbd = new ConsultasBD();
            horasProd = 0;
            horasLimpieza = 0;
            horasMatas = 0;
            horasReparaciones = 0;
            horasVarios = 0;
            horasAverias = 0;
            horasUtiles = 0;

            string dateFormatoMin = dateFiltroMin2.Value.ToString("yyyy-MM-dd");
            string dateFormatoMax = dateFiltroMax2.Value.ToString("yyyy-MM-dd");

            dtHoras2 = fbd.ObtenerInformacionProductividadComputar(dateFormatoMin, dateFormatoMax);

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtHoras2;
            dgHorasOperarios1.DataSource = bindingSource;

            dgHorasOperarios1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgHorasOperarios1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            dgHorasOperarios1.EnableHeadersVisualStyles = false;
            dgHorasOperarios1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgHorasOperarios1.Columns[1].Visible = false;



            foreach (DataGridViewRow row in dgHorasOperarios1.Rows)
            {

                /*if (String.IsNullOrEmpty(row.Cells[5].Value.ToString()))
                {
                    horasProd += (float)Convert.ToDouble(row.Cells[3].Value);
                }*/

                if (row.Cells[2].Value.ToString().Equals("11"))
                {
                    horasLimpieza += (float)Convert.ToDouble(row.Cells[5].Value);
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                }

                else if (row.Cells[2].Value.ToString().Equals("28"))
                {
                    horasMatas += (float)Convert.ToDouble(row.Cells[5].Value);
                    row.DefaultCellStyle.BackColor = Color.Olive;
                }

                else if (row.Cells[2].Value.ToString().Equals("29"))
                {
                    horasReparaciones += (float)Convert.ToDouble(row.Cells[5].Value);
                    row.DefaultCellStyle.BackColor = Color.MediumSlateBlue;
                }

                else if (row.Cells[2].Value.ToString().Equals("12"))
                {
                    horasUtiles += (float)Convert.ToDouble(row.Cells[5].Value);
                    row.DefaultCellStyle.BackColor = Color.Orchid;
                }

                else if (row.Cells[2].Value.ToString().Equals("3"))
                {
                    horasAverias += (float)Convert.ToDouble(row.Cells[5].Value);
                    row.DefaultCellStyle.BackColor = Color.Khaki;
                }

                else // (row.Cells[2].Value.ToString().Equals("Varios"))
                {
                    horasVarios += (float)Convert.ToDouble(row.Cells[5].Value);
                    row.DefaultCellStyle.BackColor = Color.Peru;
                }

            }

            // lbHorasTotales.Text = "Horas Totales Productivas: " + horasProductividad;
            //lbHProdRes.Text = horasProd.ToString();
            lbHLimpiezaRes.Text = horasLimpieza.ToString();
            lbHMatasRes.Text = horasMatas.ToString();
            lbHReparacionesRes.Text = horasReparaciones.ToString();
            lbHUtilesRes.Text = horasUtiles.ToString();
            lbHAverias.Text = horasAverias.ToString();
            lbHVariosRes.Text = horasVarios.ToString();
            lbHResultado.Text = (horasLimpieza + horasMatas + horasReparaciones + horasUtiles + horasAverias + horasVarios).ToString();

            //dgHorasOperarios1.Columns[0].Width = 60;
            // dgHorasOperarios1.Columns[1].Width = 120;
            //dgHorasOperarios1.Columns[2].Width = 60;

        }

        private void btnInproductividad_Click(object sender, EventArgs e)
        {
            fbd = new ConsultasBD();
            string categoria = "";

            if (rbLimpieza.Checked == true)
                categoria = "Limpieza";

            if (rbMatas.Checked == true)
                categoria = "Sacar Matas";

            if (rbReparaciones.Checked == true)
                categoria = "Reparaciones";

            if (rbUtiles.Checked == true)
                categoria = "Utiles";


            if (rbVarios.Checked == true)
                categoria = "Varios";

            foreach (DataGridViewRow r in dgHorasOperarios1.SelectedRows)
            {
                fbd.insertarHorasInproductivas(r.Cells[0].Value.ToString(), r.Cells[1].Value.ToString(), r.Cells[3].Value.ToString(), categoria);

            }


            btnConsultarProductividad1.PerformClick();

        }

        private void btnDeshacerInproductividad_Click(object sender, EventArgs e)
        {
            fbd = new ConsultasBD();

            foreach (DataGridViewRow r in dgHorasOperarios1.SelectedRows)
            {
                fbd.eliminarInproductividad(r.Cells[0].Value.ToString(), r.Cells[1].Value.ToString());
            }


            btnConsultarProductividad1.PerformClick();
        }


        private void btnCamion_Click(object sender, EventArgs e)
        {
            fbd = new ConsultasBD();

            string cb_expedicion = "";

            string camion = tbox_camion.Text;
            cb_expedicion = fbd.obtenerCBExpedicion(camion);

            DataTable dtPaqCamion = fbd.ObtenerPaquetesCamion(cb_expedicion);


            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dtPaqCamion;
            dgPaqCamion.DataSource = bindingSource;


            dgPaqCamion.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPaqCamion.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

            dgPaqCamion.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);

            dgPaqCamion.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            dgPaqCamion.EnableHeadersVisualStyles = false;


            foreach (DataGridViewColumn column in dgPaqCamion.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }



            foreach (DataGridViewRow row in dgPaqCamion.Rows)
            {



                row.DefaultCellStyle.BackColor = Color.IndianRed;

                if (!String.IsNullOrEmpty(row.Cells[2].Value.ToString()) && String.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                {

                    row.DefaultCellStyle.BackColor = Color.Moccasin;

                }

                if (!String.IsNullOrEmpty(row.Cells[2].Value.ToString()) && !String.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                {

                    row.DefaultCellStyle.BackColor = Color.LightGreen;

                }
            }


        }

        private void tabDiario_Click(object sender, EventArgs e)
        {

        }











    }
}
