using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EstadoTaller
{
    public partial class UCGraficoBarras : UserControl
    {
        private double horasRealizables;
        private double horasNoRealizables;
        private double horasInciertas;
        private double horasTotales;

        private double kgRealizables;
        private double kgNoRealizables;
        private double kgInciertas;
        private double kgTotales;


        private double escala;
        private string puesto;
        public bool Seleccionado;
        private string denominacion;

        //JRM
        ToolTip toolTip1 = new ToolTip();


        public event EventHandler GraficoBarrasClick;

        public UCGraficoBarras()
        {
            InitializeComponent();
        }

        public double Escala
        {
            get { return escala; }
            set { escala = value; }
        }

        public string Puesto
        {
            get { return puesto; }
            set { puesto = value; }
        }

        public string Denominacion
        {
            get { return denominacion; }
            set { denominacion = value; }
        }




        public double HorasRealizables
        {
            set { horasRealizables = value; }
            get { return horasRealizables; }
        }
        public double HorasNoRealizables
        {
            set { horasNoRealizables = value; }
            get { return horasNoRealizables; }
        }
        public double HorasInciertas
        {
            get { return horasInciertas; }
            set { horasInciertas = value; }
        }




        public double KgRealizables
        {
            get { return kgRealizables; }
            set { kgRealizables = value; }
        }
        public double KgNoRealizables
        {
            get { return kgNoRealizables; }
            set { kgNoRealizables = value; }
        }
        public double KgInciertas
        {
            get { return kgInciertas; }
            set { kgInciertas = value; }
        }
        public double KgTotales
        {
            get { return kgTotales; }
            set { kgTotales = value; }
        }

        public void AdaptarGraficaBarras()
        {
            horasTotales = horasRealizables + horasInciertas + horasNoRealizables;
            var porcentajeHorasRealizadas = horasRealizables/escala*100;
            var porcentajeHorasInciertas = horasInciertas / escala * 100;
            var porcentajeHorasNoRealizadas = horasNoRealizables / escala * 100;
            tableLayoutPanel1.RowStyles.Clear();




            lblPuesto.Text = string.Format("{0} ({1})", puesto, denominacion);
            lblPuesto.Font = new Font("Arial", 9);
            //setFont(new Font("Serif", Font.BOLD, 24));
            lblHorasRealizables.Text = string.Format("{0}h", Math.Round(horasRealizables).ToString("N0")) + " - " + string.Format("{0}Kg", Math.Round(kgRealizables).ToString("N0"));
            lblHorasInciertas.Text = string.Format("{0}h", Math.Round(horasInciertas).ToString("N0")) + " - " + string.Format("{0}Kg", Math.Round(kgInciertas).ToString("N0"));
            lblHorasNoRealizadas.Text = string.Format("{0}h", Math.Round(horasNoRealizables).ToString("N0")) + " - " + string.Format("{0}Kg", Math.Round(kgNoRealizables).ToString("N0"));

            lblTotalHoras.Text = string.Format("{0}h-{1}Kg", Math.Round(horasTotales).ToString("N0"), Math.Round(KgTotales).ToString("N0"));
            if (horasTotales > 0)
                lblTotalHoras.Text += Environment.NewLine + Math.Round(KgTotales / horasTotales).ToString("N0") + "Kg/H";
            else
                lblTotalHoras.Text += Environment.NewLine + " - Kg/H";

            // Set up the ToolTip text for this.lblPuesto.
            this.toolTip1.SetToolTip(this.lblPuesto, "Máquina " + this.lblPuesto.Text);


            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            var sumaPorcentajes = 100 - porcentajeHorasNoRealizadas - porcentajeHorasInciertas - porcentajeHorasRealizadas;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, (float) (sumaPorcentajes>=0?sumaPorcentajes:0)));
            pHorasNoRealizables.Visible = porcentajeHorasNoRealizadas != 0 ;
            pHorasInciertas.Visible = porcentajeHorasInciertas != 0;
            pHorasRealizables.Visible = porcentajeHorasRealizadas != 0;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, (float)porcentajeHorasNoRealizadas));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, (float)porcentajeHorasInciertas));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, (float)porcentajeHorasRealizadas));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        }


        private void UCGraficoBarras_Click(object sender, EventArgs e)
        {
            if (GraficoBarrasClick != null)
            {
                GraficoBarrasClick(this, e);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (Seleccionado)
                using (var pen = new Pen(Color.Red, 2))
                {
                    pen.DashStyle = DashStyle.Solid;
                    e.Graphics.DrawRectangle(pen, e.ClipRectangle);
                }
        }
    }
}