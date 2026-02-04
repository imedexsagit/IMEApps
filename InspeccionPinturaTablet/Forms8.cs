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
    public partial class Forms8 : Form
    {
        string certificado;
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();
        public Forms8(string certificado)
        {
            InitializeComponent();

            this.certificado = certificado;

            DataTable table = null;
            table = ipDB.datosa8InspCarga(certificado);

            foreach (DataRow row in table.Rows)
            {

                //Fechas

                if (row["fecha"] != DBNull.Value)
                {
                    dateTimePicker.Text = Convert.ToString(row["fecha"]);
                }

                textBox.Text = Convert.ToString(row["conteaguaNote"]);
                textBox1.Text = Convert.ToString(row["contelimpiezaNote"]);

                textBox2.Text = Convert.ToString(row["embalajeNote"]);
                textBox3.Text = Convert.ToString(row["estabilidadNote"]);
                textBox4.Text = Convert.ToString(row["dañadasNote"]);
                textBox5.Text = Convert.ToString(row["identificadoNote"]);

                textBox7.Text = Convert.ToString(row["tmfitosanitariosNote"]);
                textBox8.Text = Convert.ToString(row["tmpaqueteNote"]);

                textBox9.Text = Convert.ToString(row["sinmanchasNote"]);
                textBox10.Text = Convert.ToString(row["sinzonasdesnudasNote"]);
                textBox11.Text = Convert.ToString(row["sinPinchosNote"]);
                textBox12.Text = Convert.ToString(row["sincenizasNote"]);

                if (row["contaguaResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["contaguaResult"]) == true)
                    {
                        checkBox1.Checked = true;
                    }
                }

                if (row["contelimpiezaResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["contelimpiezaResult"]) == true)
                    {
                        checkBox2.Checked = true;
                    }
                }

                if (row["embalajeResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["embalajeResult"]) == true)
                    {
                        checkBox3.Checked = true;
                    }
                }

                if (row["estabilidadResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["estabilidadResult"]) == true)
                    {
                        checkBox4.Checked = true;
                    }
                }

                if (row["dañadasResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["dañadasResult"]) == true)
                    {
                        checkBox5.Checked = true;
                    }
                }

                if (row["contelimpiezaResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["contelimpiezaResult"]) == true)
                    {
                        checkBox6.Checked = true;
                    }
                }

                if (row["tmfitosanitariosResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["tmfitosanitariosResult"]) == true)
                    {
                        checkBox7.Checked = true;
                    }
                }

                if (row["tmpaqueteResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["tmpaqueteResult"]) == true)
                    {
                        checkBox8.Checked = true;
                    }
                }

                if (row["sinmanchasResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["sinmanchasResult"]) == true)
                    {
                        checkBox9.Checked = true;
                    }
                }

                if (row["sinzonasdesnudasResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["sinzonasdesnudasResult"]) == true)
                    {
                        checkBox10.Checked = true;
                    }
                }

                if (row["sinPinchosResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["sinPinchosResult"]) == true)
                    {
                        checkBox11.Checked = true;
                    }
                }

                if (row["sincenizasResult"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["sincenizasResult"]) == true)
                    {
                        checkBox12.Checked = true;
                    }
                }

                if (row["contenedorCorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["contenedorCorrecto"]) == true)
                    {
                        checkBox13.Checked = true;
                    }
                }
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8Cliente form8 = new Form8Cliente(certificado);
            form8.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DateTime date = DateTime.Parse(dateTimePicker.Text);
            string dateFormatted = date.ToString("yyyy-MM-dd");

            int result1 = 0, result2 = 0, result3 = 0, result4 = 0,result5 = 0,result6 = 0,result7 = 0,result8 = 0,result9 = 0,
                result10 = 0, result11 = 0, result12 = 0, result13 = 0;

            if (textBox1.Text == "") textBox1.Text = null;
            if (textBox2.Text == "") textBox2.Text = null;
            if (textBox3.Text == "") textBox3.Text = null;
            if (textBox4.Text == "") textBox4.Text = null;
            if (textBox.Text == "") textBox.Text = null;
            if (textBox8.Text == "") textBox8.Text = null;
            if (textBox7.Text == "") textBox7.Text = null;
            if (textBox9.Text == "") textBox9.Text = null;
            if (textBox10.Text == "") textBox10.Text = null;
            if (textBox11.Text == "") textBox11.Text = null;
            if (textBox12.Text == "") textBox12.Text = null;


            if (checkBox1.Checked == true) result1 = 1;
            if (checkBox2.Checked == true) result2 = 1;

            if (checkBox3.Checked == true) result3 = 1;
            if (checkBox4.Checked == true) result4 = 1;
            if (checkBox5.Checked == true) result5 = 1;
            if (checkBox6.Checked == true) result6 = 1;

            if (checkBox7.Checked == true) result7 = 1;
            if (checkBox8.Checked == true) result8 = 1;
            if (checkBox9.Checked == true) result9 = 1;
            if (checkBox10.Checked == true) result10 = 1;
            if (checkBox11.Checked == true) result11 = 1;
            if (checkBox12.Checked == true) result12= 1;
            if (checkBox13.Checked == true) result13 = 1;

            ipDB.actulizar8InspFinal(certificado, result1, textBox.Text, result2, textBox1.Text,
             result3, textBox2.Text, result4, textBox3.Text,result5, textBox4.Text, result6, textBox5.Text,
             result7, textBox7.Text, result8, textBox8.Text,
             result9, textBox9.Text, result10, textBox10.Text,result11, textBox11.Text, result12, textBox12.Text,result13,textBox13.Text,dateFormatted);
            Cursor.Current = Cursors.Default;
        }

       
    }
}
