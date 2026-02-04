using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using empresaGlobalProj;

namespace ProyectosOnLine
{
    public partial class Form5 : Form
    {
        private Int32 of;

        public Form5(Int32 ordfab)
        {
            InitializeComponent();

            this.Text += ordfab.ToString();
            this.of = ordfab;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'bonosOF.TC_BONOL' Puede moverla o quitarla según sea necesario.
            this.tC_BONOLTableAdapter.Fill(this.bonosOF.TC_BONOL, empresaGlobal.empresaID, this.of);

        }
    }
}