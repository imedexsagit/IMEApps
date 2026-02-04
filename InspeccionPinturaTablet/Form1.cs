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
    public partial class Form1 : Form
    {
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();

        string certificado;

        public Form1(string certificado)
        {
            this.certificado = certificado;
           
            InitializeComponent();

            DataTable table = null;
            table = ipDB.datos1InsPaquetes(certificado);

            foreach (DataRow row in table.Rows)
            {
                textBox1.Text = Convert.ToString(row["plreclamacion"]);
                textBox2.Text = Convert.ToString(row["cpreclamacion"]);
                textBox3.Text = Convert.ToString(row["apreclamacion"]);

                if (row["plcorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["plcorrecto"]) == true)
                    {
                        radioButton1.Checked = true;
                    }
                    if (Convert.ToBoolean(row["plcorrecto"]) == false)
                    {
                        radioButton2.Checked = true;
                    }
                }

                if (row["cpcorrecto"] != DBNull.Value )
                {
                    if (Convert.ToBoolean(row["cpcorrecto"]) == true)
                    {
                        radioButton4.Checked = true;
                    }
                    if (Convert.ToBoolean(row["cpcorrecto"]) == false)
                    {
                        radioButton3.Checked = true;
                    }
                }

                if (row["apcorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["apcorrecto"]) == true)
                    {
                        radioButton6.Checked = true;
                    }
                    if (Convert.ToBoolean(row["apcorrecto"]) == false)
                    {
                        radioButton5.Checked = true;
                    }
                }

                if (row["fecha"] != DBNull.Value)
                {
                    dateTimePicker1.Text = Convert.ToString(row["fecha"]);
                }
                
            }


        }

        //Guardar cambios
        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int plcorrecto = 2;
            int cpcorrecto = 2;
            int apcorrecto = 2;

                    DateTime date = DateTime.Parse(dateTimePicker1.Text);
                    string dateFormatted = date.ToString("yyyy-MM-dd");

                    if (textBox1.Text == "") textBox1.Text = null;
                    if (textBox2.Text == "") textBox2.Text = null;
                    if (textBox3.Text == "") textBox3.Text = null;

                    if (radioButton1.Checked == true) plcorrecto = 1;
                    if (radioButton2.Checked == true) plcorrecto = 0;

                    if (radioButton4.Checked == true) cpcorrecto = 1;
                    if (radioButton3.Checked == true) cpcorrecto = 0;

                    if (radioButton6.Checked == true) apcorrecto = 1;
                    if (radioButton5.Checked == true) apcorrecto = 0;

                    ipDB.actulizar1InsPaquetes(certificado, 
                        plcorrecto,
                        textBox1.Text,
                        cpcorrecto,
                        textBox2.Text,
                        apcorrecto,
                        textBox3.Text,
                        dateFormatted);

            Cursor.Current = Cursors.Default;
            MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");

        }


    }
}
