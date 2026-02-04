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
    public partial class Form6 : Form
    {
        string certificado;
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();

        public Form6(string certificado)
        {
            InitializeComponent();

            this.certificado = certificado;

            DataTable table = null;
            table = ipDB.datosa6InspFinalPinturaRecubrimiento(certificado);

            DataTable tableEquipos = null;
            tableEquipos = ipDB.obtenerEquiposMade();
            string EQUIPO = "";

            comboBox1.Items.Add("Trocknung der Papierscheibe");
            comboBox1.Items.Add("Gummi");
            comboBox1.Items.Add("Pesa mayor de 20kg");

            comboBox2.Items.Add("Trocknung der Papierscheibe");
            comboBox2.Items.Add("Gummi");
            comboBox2.Items.Add("Pesa mayor de 20kg");

          

            foreach (DataRow row in tableEquipos.Rows)
            {
                EQUIPO = Convert.ToString(row["NEQUIPO"]);

                comboBox1.Items.Add(EQUIPO);
                comboBox2.Items.Add(EQUIPO);
               
            }
            
           

           
            foreach (DataRow row in table.Rows)
            {

                //Fechas

                if (row["fecha"] != DBNull.Value)
                {
                    dateTimePicker.Text = Convert.ToString(row["fecha"]);
                }

                textBox1.Text = Convert.ToString(row["apreclamacion"]);

                textBox2.Text = Convert.ToString(row["esreclamacion"]);

                textBox3.Text = Convert.ToString(row["gradosecado"]);
                textBox4.Text = Convert.ToString(row["esmetodo"]);
                comboBox2.Text = Convert.ToString(row["esequipoensayo"]);

                textBox6.Text = Convert.ToString(row["tareclamacion"]);
                textBox7.Text = Convert.ToString(row["tavalor"]);
                textBox8.Text = Convert.ToString(row["tametodo"]);
                
                textBox10.Text = Convert.ToString(row["iereclamacion"]);
                comboBox1.Text = Convert.ToString(row["taequipoensayo"]);

                if (row["apcorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["apcorrecto"]) == true)
                    {
                        radioButton1.Checked = true;
                    }
                    if (Convert.ToBoolean(row["apcorrecto"]) == false)
                    {
                        radioButton2.Checked = true;
                    }
                }

                if (row["escorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["escorrecto"]) == true)
                    {
                        radioButton4.Checked = true;
                    }
                    if (Convert.ToBoolean(row["escorrecto"]) == false)
                    {
                        radioButton3.Checked = true;
                    }
                }

                if (row["tacorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["tacorrecto"]) == true)
                    {
                        radioButton6.Checked = true;
                    }
                    if (Convert.ToBoolean(row["tacorrecto"]) == false)
                    {
                        radioButton5.Checked = true;
                    }
                }

                if (row["iecorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["iecorrecto"]) == true)
                    {
                        radioButton8.Checked = true;
                    }
                    if (Convert.ToBoolean(row["iecorrecto"]) == false)
                    {
                        radioButton7.Checked = true;
                    }
                }

                if (row["esaplicable"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["esaplicable"]) == true)
                    {
                        checkBox1.Checked = true;
                    }
                }

                if (row["taaplicable"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["taaplicable"]) == true)
                    {
                        checkBox2.Checked = true;
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int apcorrecto = 2;
            int escorrecto = 2;
            int tacorrecto = 2;
            int iecorrecto = 2;

            int esaplicable = 0;
            int taaplicable = 0;

            DateTime date = DateTime.Parse(dateTimePicker.Text);
            string dateFormatted = date.ToString("yyyy-MM-dd");

            if (textBox1.Text == "") textBox1.Text = null;
            if (textBox2.Text == "") textBox2.Text = null;
            if (textBox3.Text == "") textBox3.Text = null;
            if (textBox4.Text == "") textBox4.Text = null;
            if (textBox5.Text == "") textBox5.Text = null;
            if (textBox6.Text == "") textBox6.Text = null;
            if (textBox7.Text == "") textBox7.Text = null;
            if (textBox8.Text == "") textBox8.Text = null;
           
            if (textBox10.Text == "") textBox10.Text = null;

            if (radioButton1.Checked == true) apcorrecto = 1;
            if (radioButton2.Checked == true) apcorrecto = 0;

            if (radioButton4.Checked == true) escorrecto = 1;
            if (radioButton3.Checked == true) escorrecto = 0;

            if (radioButton6.Checked == true) tacorrecto = 1;
            if (radioButton5.Checked == true) tacorrecto = 0;

            if (radioButton8.Checked == true) iecorrecto = 1;
            if (radioButton7.Checked == true) iecorrecto = 0;

            if (checkBox1.Checked == true) esaplicable = 1;
            if (checkBox1.Checked == true) taaplicable = 1;

            ipDB.actulizar6InspFinalPinturaRecubrimiento(certificado,
                apcorrecto,
                escorrecto,
                tacorrecto,
                iecorrecto,
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                comboBox2.Text,
                textBox6.Text,
                textBox7.Text,
                textBox8.Text,
                comboBox1.Text,
                textBox10.Text,
                esaplicable,
                taaplicable,
                dateFormatted);

            Cursor.Current = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6Test form6test = new Form6Test(certificado);
            form6test.Show();
        }
    }
}
