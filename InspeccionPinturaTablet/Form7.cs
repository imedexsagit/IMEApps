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
    public partial class Form7 : Form
    {
        string certificado;
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();

        public Form7(string certificado)
        {
             this.certificado = certificado;
            InitializeComponent();
            DataTable table = null;
            table = ipDB.datosa7Comentarios(certificado);

            foreach (DataRow row in table.Rows)
            {
                if (row["fecha"] != DBNull.Value)
                {
                    dateTimePicker.Text = Convert.ToString(row["fecha"]);
                }

                textBox1.Text = Convert.ToString(row["comentarios"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int eschcorrecto = 2;
            int escscorrecto = 2;
            int eschaplicable = 0;

            DateTime date = DateTime.Parse(dateTimePicker.Text);
            string dateFormatted = date.ToString("yyyy-MM-dd");



            if (textBox1.Text == "") textBox1.Text = null;


            ipDB.actulizar7Observaciones(certificado,
                textBox1.Text,
                dateFormatted);

            Cursor.Current = Cursors.Default;
        }
    }
}
