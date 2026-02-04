using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InspeccionPinturaTablet
{
    public partial class FormMedidas : Form
    {
        string certificado;
        int w;
        

        public FormMedidas()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("1", "#"); // dataGridView2.Columns.Add(«Nombre_columna», «Texto_a_mostrar_en_el_Grid»);
            dataGridView1.Columns.Add("1", "N");
            dataGridView1.Columns.Add("1", "Zn");
            dataGridView1.Columns.Add("1", "Tiempo");

            for (int i = 0; i < 100; i++)
            {
                dataGridView1.Rows.Add(1);
            }
        }


           
    }
}
