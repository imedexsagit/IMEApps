using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ImeApps
{
    public partial class BotonAplicacion : UserControl
    {
        public delegate void BotonAplicacionClickHandler();
        [Category("Evento")]
        [Description("Ocurre cuando se valida el Texto")]
        public event BotonAplicacionClickHandler BotonAplicacionClick;

        [Description("Imagen que se mostrara en el control"), Category("Apariencia")]
        public Image Imagen
        {
            set { this.btnImagen.Image = value; }
            get { return this.btnImagen.Image; }
        }


        [Description("Texto que se mostrara en el control"), Category("Apariencia")]
        public string Texto
        {
            set { this.btnTexto.Text = value; }
            get { return this.btnTexto.Text; }
        }
        

        public BotonAplicacion()
        {
            InitializeComponent();
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            if (BotonAplicacionClick != null) BotonAplicacionClick();
        }

        private void btnTexto_Click(object sender, EventArgs e)
        {
            if (BotonAplicacionClick != null) BotonAplicacionClick();
        }
    }
}
