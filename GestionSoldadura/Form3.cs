using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionSoldadura
{
    public partial class Form3 : Form
    {

        public delegate void pasarDato(string dato);
        public event pasarDato pasado; // evento de tipo pasarDato

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(int valor)
        {
            InitializeComponent();
            label1.Text = Convert.ToString(valor);
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            pasado(textBox1.Text);
            this.Close();
        }


    }
}
