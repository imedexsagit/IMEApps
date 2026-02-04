using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InspeccionPinturaTablet
{
   
    public partial class elegirEmpresa : Form
    {
         public string empresa="3";
         public string idioma = "DE";
        public elegirEmpresa()
        {
            InitializeComponent();
        }

        private void button_Ime_Click(object sender, EventArgs e)
        {
            empresa = "3";
        }
        
        // CARLOS 15/12/2025 - Se quita el logo de MADE en petición de Marta Hernández
        /*private void button_Made_Click(object sender, EventArgs e)
        {
             empresa = "60";
        }*/


        private void radioES_CheckedChanged(object sender, EventArgs e)
        {
            this.idioma = "ES";
        }

        private void radioEN_CheckedChanged(object sender, EventArgs e)
        {
            this.idioma = "EN";
        }

        private void radioFR_CheckedChanged(object sender, EventArgs e)
        {
            this.idioma = "FR";
        }

        private void radioDE_CheckedChanged(object sender, EventArgs e)
        {
            this.idioma = "DE";
        }
    }
}
