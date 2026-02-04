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
    public partial class Form3 : Form
    {
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();
        string certificado;

        public Form3(string certificado)
        {
            InitializeComponent();
            this.certificado = certificado;

            DataTable tableEquipos = null;
            tableEquipos = ipDB.obtenerEquiposMade();
            string EQUIPO = "";

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            comboBox1.Items.Add("Trocknung der Papierscheibe");
            comboBox1.Items.Add("Gummi");
            comboBox1.Items.Add("Pesa mayor de 20kg");

            comboBox2.Items.Add("Trocknung der Papierscheibe");
            comboBox2.Items.Add("Gummi");
            comboBox2.Items.Add("Pesa mayor de 20kg");

            comboBox1.Items.Add("PEINE ELCOMETER");
            comboBox2.Items.Add("PEINE ELCOMETER");

            foreach (DataRow row in tableEquipos.Rows)
            {
                EQUIPO = Convert.ToString(row["NEQUIPO"]);
                comboBox1.Items.Add(EQUIPO);
                comboBox2.Items.Add(EQUIPO);
            }

            
            DataTable table = null;
            table = ipDB.datos3RuglSup(certificado);

            foreach (DataRow row in table.Rows)
            {
                if (row["fecha"] != DBNull.Value)
                {
                    dateTimePicker1.Text = Convert.ToString(row["fecha"]);
                }

                textBox1.Text = Convert.ToString(row["csreclamacion"]);
                textBox2.Text = Convert.ToString(row["cspiezaensayada"]);
                comboBox1.Text = Convert.ToString(row["csequipoensayo"]);
                textBox4.Text = Convert.ToString(row["csvalor"]);

                textPz1.Text = Convert.ToString(row["tenSupP1"]);
                textPz2.Text = Convert.ToString(row["tenSupP2"]);
                textPz3.Text = Convert.ToString(row["tenSupP3"]);

                textBox3.Text = Convert.ToString(row["rsnserie"]);

                if (row["cscorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["cscorrecto"]) == true)
                    {
                        radioButton1.Checked = true;
                    }
                    if (Convert.ToBoolean(row["cscorrecto"]) == false)
                    {
                        radioButton2.Checked = true;
                    }
                }

                

                if (row["csaplicable"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["csaplicable"]) == true)
                    {
                        checkBox1.Checked = true;
                    }
                }

                textBox5.Text = Convert.ToString(row["rsreclamacion"]);
                textBox6.Text = Convert.ToString(row["rspiezaensayada"]);
                comboBox2.Text = Convert.ToString(row["rsequipoensayo"]);
                textBox8.Text = Convert.ToString(row["rsvalor"]);

                if (row["rscorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["rscorrecto"]) == true)
                    {
                        radioButton4.Checked = true;
                    }
                    if (Convert.ToBoolean(row["rscorrecto"]) == false)
                    {
                        radioButton3.Checked = true;
                    }
                }



                if (row["rsaplicable"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["rsaplicable"]) == true)
                    {
                        checkBox2.Checked = true;
                    }
                }

                textBox9.Text = Convert.ToString(row["tpreclamacion"]);
                textBox10.Text = Convert.ToString(row["cntadhva"]);
                textBox11.Text = Convert.ToString(row["fondoconst"]);
             
                if (row["tpcorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["tpcorrecto"]) == true)
                    {
                        radioButton6.Checked = true;
                    }
                    if (Convert.ToBoolean(row["tpcorrecto"]) == false)
                    {
                        radioButton5.Checked = true;
                    }
                }


                if (row["tpaplicable"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["tpaplicable"]) == true)
                    {
                        checkBox2.Checked = true;
                    }
                }

                //limpieza
                if (row["limpieza"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["limpieza"]) == true)
                    {
                        checkBox4.Checked = true;
                    }
                }

                //tension
                if (row["tension"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["tension"]) == true)
                    {
                        checkBox5.Checked = true;
                    }
                }

                //perla
                if (row["perla"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["perla"]) == true)
                    {
                        checkBox6.Checked = true;
                    }
                }

                //ensayo
                if (row["ensayo"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["ensayo"]) == true)
                    {
                        checkBox7.Checked = true;
                    }
                }


            }
        }

        private void test1_Click(object sender, EventArgs e)
        {
            Form3Test form3test = new Form3Test(1,certificado);
            form3test.Show();
        }

        private void test2_Click(object sender, EventArgs e)
        {
            Form3Test form3test = new Form3Test(2, certificado);
            form3test.Show();
        }

        private void test3_Click(object sender, EventArgs e)
        {
            Form3Test form3test = new Form3Test(3,certificado);
            form3test.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             //Actualizado todo menos los test*

            Cursor.Current = Cursors.WaitCursor;

            int cscorrecto = 2;
            int rscorrecto = 2;
            int tpcorrecto = 2;

            int csaplicable = 0;
            int tpaplicable = 0;

            int limpieza = 0;
            int tension = 0;
            int perla = 0;
            int ensayo = 0;

            DateTime date = DateTime.Parse(dateTimePicker1.Text);
            string dateFormatted = date.ToString("yyyy-MM-dd");

            if (textBox1.Text == "") textBox1.Text = null;
            if (textBox2.Text == "") textBox2.Text = null;
            if (comboBox1.Text == "") comboBox1.Text = null;
            if (textBox4.Text == "") textBox4.Text = null;
            if (textBox5.Text == "") textBox5.Text = null;
            if (textBox6.Text == "") textBox6.Text = null;
            if (comboBox2.Text == "") comboBox2.Text = null;
            if (textBox8.Text == "") textBox8.Text = null;
            if (textBox9.Text == "") textBox9.Text = null;
            if (textBox10.Text == "") textBox10.Text = null;
            if (textBox11.Text == "") textBox11.Text = null;

            if (textPz1.Text == "") textPz1.Text = null;
            if (textPz2.Text == "") textPz2.Text = null;
            if (textPz3.Text == "") textPz3.Text = null;


            if (radioButton1.Checked == true) cscorrecto = 1;
            if (radioButton2.Checked == true) cscorrecto = 0;

            if (radioButton4.Checked == true) rscorrecto = 1;
            if (radioButton3.Checked == true) rscorrecto = 0;

            if (radioButton6.Checked == true) tpcorrecto = 1;
            if (radioButton5.Checked == true) tpcorrecto = 0;

            if (checkBox1.Checked == true) csaplicable = 1;
            if (checkBox2.Checked == true) tpaplicable = 1;
            if (textBox3.Text == "") textBox3.Text = null;

            if (checkBox4.Checked == true) limpieza = 1;
            if (checkBox5.Checked == true) tension = 1;
            if (checkBox6.Checked == true) perla = 1;
            if (checkBox7.Checked == true) ensayo = 1;

            ipDB.actulizar3RuglSup(certificado,
               cscorrecto,
               rscorrecto,
               tpcorrecto,
               csaplicable,
               tpaplicable,
               textBox1.Text,
               textBox2.Text,
               comboBox1.Text,
               textBox4.Text,
               textBox5.Text,
               textBox6.Text,
               comboBox2.Text,
               textBox8.Text,
               textBox9.Text,
               textBox10.Text,
               textBox11.Text,
               dateFormatted,
               textBox3.Text,
               limpieza,
               tension,
               perla,
               ensayo,
               textPz1.Text,
               textPz2.Text,
               textPz3.Text);
            Cursor.Current = Cursors.Default;
        }

  

        
    }
}
