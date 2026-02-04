using System;
using System.Linq;
using System.Windows.Forms;
using IMEDEXSA;
using System.Configuration;
using empresaGlobalProj;
using System.Drawing;

namespace ImeApps
{
    public partial class Principal : Form
    {
        //JRM - Cambiar Empresa
        //private CambiarEmpresaFRM frmSelEmpresa;

        private readonly Timer _timer = new Timer();
        string nempresa;
        //variable creada para cambiar el fondo al cambiar de empresa en ImeApps
        string empCambioFondo = "";

        //Configuration config1 = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

        public string Nempresa
        {
            get { return nempresa; }
            set
            {
                nempresa = value;
                UpdateTextBoxNombreEmpresa(nempresa);
            }
        }

        void UpdateTextBoxNombreEmpresa(string value)
        {

            this.textBoxNombreEmpresa.Text = value;

            //JMRM Añadido el 13/07/2020 para cambiar el fondo de la empresa
            if (empCambioFondo == "")
            {
                empCambioFondo = "3";
            }

            //Si cambia la empresa respecto a la variable, se actualiza el fondo
            if (empresaGlobal.empresaID != empCambioFondo)
            {
                empCambioFondo = empresaGlobal.empresaID;
                if (empresaGlobal.empresaID.Equals("3"))
                {
                    Image myimage = new Bitmap(@"M:\Recursos gráficos\Fondos ImeApps\fondoImedexsaActual.png");
                    this.BackgroundImage = myimage;
                }
                else if (empresaGlobal.empresaID.Equals("60"))
                {
                    Image myimage = new Bitmap(@"M:\Recursos gráficos\Fondos ImeApps\fondoMadeActual.png");
                    this.BackgroundImage = myimage;
                }
                else
                {
                    Image myimage = new Bitmap(@"M:\Recursos gráficos\Fondos ImeApps\fondoMadeTowerAntiguo.png");
                    this.BackgroundImage = myimage;
                }
            }
            //}
        }

        public Principal()
        {
            InitializeComponent();
            if (!empresaGlobal.showEmp)
            {
                cambiarEmpresaToolStripMenuItem.Visible = false;
            }
            

            _timer.Interval = 500;
            _timer.Tick += TimerTick;
            _timer.Enabled = true;
            //Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            this.Nempresa = empresaGlobal.nombreEmpresa;//config.AppSettings.Settings["EMPRESA"].Value;
            /*System.Diagnostics.Debug.WriteLine(" La empresaID inicialmente es: " + empresaGlobal.empresaID);
            empresaGlobal.empresaID = this.Nempresa;
            System.Diagnostics.Debug.WriteLine(" La empresaID se actualiza y ahora es: " + empresaGlobal.empresaID);*/

            System.Diagnostics.Debug.WriteLine("----------------------------------------" + this.Nempresa);

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed == true)
                this.Text += " v" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            else
                this.Text += " v" + Application.ProductVersion.ToString();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            AccesoAplicaciones formAplicaciones = new AccesoAplicaciones();
            formAplicaciones.MdiParent = this;
            formAplicaciones.Show();
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aplicacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Name == "AccesoAplicaciones")
                    this.MdiChildren[i].WindowState = FormWindowState.Normal;
                else
                    this.MdiChildren[i].WindowState = FormWindowState.Minimized;
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto = "";

            texto = texto + "ImeApps: Aplicaciones desarrolladas por el Departamento de Informática de IMEDEXSA" + System.Environment.NewLine + System.Environment.NewLine;
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed == true)
                texto = texto + "Versión: " + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() + System.Environment.NewLine;
            else
                texto = texto + "Versión: " + Application.ProductVersion.ToString() + System.Environment.NewLine;

            MessageBox.Show(texto, "Acerca de...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void categoríasCaracterísticasIngenieríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = this.MdiChildren.OfType<Form>().Where(x => x.Name == "CategoriasCaracteristicas").FirstOrDefault();
            if (f == null)
            {
                CategoriasCaracteristicas formCatCaract = new CategoriasCaracteristicas();
                formCatCaract.MdiParent = this;
                formCatCaract.Show();
            }
            else
            {
                f.WindowState = FormWindowState.Normal;
            }
        }

        //JRM - Cambiar Empresa
        private void menuItem4_Click(object sender, EventArgs e)
        {
            //this.abrirSeleccionEmpresa();
            FormElegirEmpresas fe = new FormElegirEmpresas();
            fe.Show();
        }
        //JRM - Cambiar Empresa
        void TimerTick(object sender, EventArgs e)
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            this.Nempresa = empresaGlobal.empresaID;//config.AppSettings.Settings["EMPRESA"].Value;
            
            //System.Diagnostics.Debug.WriteLine(" La empresaID inicialmente es: " + empresaGlobal.empresaID + " y el nombre de la empresa es: " + empresaGlobal.nombreEmpresa);
            empresaGlobal.empresaID = nempresa;
            //System.Diagnostics.Debug.WriteLine(" La empresaID se actualiza y ahora es: " + empresaGlobal.empresaID + " y el nombre de la empresa es: " + empresaGlobal.nombreEmpresa);
            this.textBoxNombreEmpresa.Text = empresaGlobal.empresaID + " - " + empresaGlobal.nombreEmpresa;

            if (!empresaGlobal.showEmp)
            {
                cambiarEmpresaToolStripMenuItem.Visible = false;
            }
            else
            {
                cambiarEmpresaToolStripMenuItem.Visible = true;
            }
            
            /*string val = this.Nempresa;
            if (this.Nempresa.Equals(val))
            {
                this.Text += this.Nempresa;
            }*/
            //System.Diagnostics.Debug.WriteLine("----------Actualizando con Timer----------------" + this.Nempresa);
        }

    }
}
