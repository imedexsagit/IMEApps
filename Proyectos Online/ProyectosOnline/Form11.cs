using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProyectosOnLine
{
    public partial class Form11 : Form
    {
        public string proyectoInforme = "";
        public string nombreProyecto = "";
        public int angular = 0, chapa = 0, cjSold = 0;

        public Form11(String proyecto, String nProyecto)
        {
            InitializeComponent();
            proyectoInforme = proyecto;
            nombreProyecto = nProyecto;
            label1.Text = "PROYECTO: " + proyecto;

        }

        private void button1_Click(object sender, EventArgs e)

        {
            if (checkBox1.Checked == true) angular = 1;
            if (checkBox2.Checked == true) chapa = 1;
            if (checkBox3.Checked == true) cjSold = 1;

            Form11informe f11i;

            if (!chkSemana.Checked)
            {
                f11i = new Form11informe(proyectoInforme, angular, chapa, cjSold, nombreProyecto);
            }
            else
            {
                f11i = new Form11informe(proyectoInforme, angular, chapa, cjSold, "-");
            }

            DialogResult res = f11i.ShowDialog();

            if (res == DialogResult.OK)
            {
                //recuperando la variable publica del formulario 2
                //min1.Text = f5g.galmin; //asignamos al texbox el dato de la variable
                //med1.Text = f5g.galmed; //asignamos al texbox el dato de la variable
                //max1.Text = f5g.galmax; //asignamos al texbox el dato de la variable
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void chkSemana_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSemana.Checked)
            {
                chkPendiente.Checked = false;
            }
        }

        private void chkPendiente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPendiente.Checked)
            {
                chkSemana.Checked = false;
            }
        }

   

       
    }
}
