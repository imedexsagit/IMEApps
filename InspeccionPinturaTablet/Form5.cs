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
    public partial class Form5 : Form
    {
        string certificado;
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();

        private ControlAccesos.ControlAcceso controlAcceso;


        public Form5(string certificado)
        {
            InitializeComponent();

            //Comprobar si el usuario tiene permisos para ejecutar la aplicación            
           // controlAcceso = new ControlAccesos.ControlAcceso();
            //if (!controlAcceso.TieneAcceso("TomarMedidas"))
          //  {
             //   button5.Visible = false;
           // }

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
            table = ipDB.datos5Espesor(certificado);

            foreach (DataRow row in table.Rows)
            {

                //Fechas
                if (row["fecha"] != DBNull.Value)
                {
                    dateTimePicker.Text = Convert.ToString(row["fecha"]);
                }



                textBox1.Text = Convert.ToString(row["eschreclamacion"]);
                min1.Text = Convert.ToString(row["galvanizado"]);
                med1.Text = Convert.ToString(row["capa2"]);
                max1.Text = Convert.ToString(row["capa3"]);
                textBox5.Text = Convert.ToString(row["valorpeine"]);
                comboBox1.Text = Convert.ToString(row["eschequipoEnsayo"]);

                textBox6.Text = Convert.ToString(row["escsreclamacion"]);
                comboBox2.Text = Convert.ToString(row["escsequipoEnsayo"]);

                textBox2.Text = Convert.ToString(row["capa1min"]);
                textBox3.Text = Convert.ToString(row["capa1med"]);
                textBox4.Text = Convert.ToString(row["capa1max"]);

                textBox7.Text = Convert.ToString(row["capa2min"]);
                textBox9.Text = Convert.ToString(row["capa2med"]);
                textBox11.Text = Convert.ToString(row["capa2max"]);

                textBox8.Text = Convert.ToString(row["capa3min"]);
                textBox10.Text = Convert.ToString(row["capa3med"]);
                textBox12.Text = Convert.ToString(row["capa3max"]);


                if (row["eschcorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["eschcorrecto"]) == true)
                    {
                        radioButton1.Checked = true;
                    }
                    if (Convert.ToBoolean(row["eschcorrecto"]) == false)
                    {
                        radioButton2.Checked = true;
                    }
                }

                if (row["escscorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["escscorrecto"]) == true)
                    {
                        radioButton4.Checked = true;
                    }
                    if (Convert.ToBoolean(row["escscorrecto"]) == false)
                    {
                        radioButton3.Checked = true;
                    }
                }

                if (row["eschaplicable"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["eschaplicable"]) == true)
                    {
                        checkBox1.Checked = true;
                    }
                }

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
            if (textBox2.Text == "") textBox2.Text = null;
            if (textBox3.Text == "") textBox3.Text = null;
            if (textBox4.Text == "") textBox4.Text = null;
            if (textBox5.Text == "") textBox5.Text = null;
            if (comboBox1.Text == "") comboBox1.Text = null;
            if (textBox6.Text == "") textBox6.Text = null;
            if (comboBox2.Text == "") comboBox2.Text = null;

            if (min1.Text == "") min1.Text = null;
            if (med1.Text == "") med1.Text = null;
            if (max1.Text == "") max1.Text = null;

            if (textBox7.Text == "") textBox7.Text = null;
            if (textBox9.Text == "") textBox9.Text = null;
            if (textBox11.Text == "") textBox11.Text = null;

            if (textBox8.Text == "") textBox8.Text = null;
            if (textBox10.Text == "") textBox10.Text = null;
            if (textBox12.Text == "") textBox12.Text = null;

            if (radioButton1.Checked == true) eschcorrecto = 1;
            if (radioButton2.Checked == true) eschcorrecto = 0;

            if (radioButton4.Checked == true) escscorrecto = 1;
            if (radioButton3.Checked == true) escscorrecto = 0;

            if (checkBox1.Checked == true) eschaplicable = 1;

            ipDB.actulizar5Espesor(certificado,
                eschcorrecto,
                eschaplicable,
                textBox1.Text,
                min1.Text,
                med1.Text,
                max1.Text,
                textBox5.Text,
                comboBox1.Text,
                escscorrecto,
                textBox6.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox7.Text,
                textBox9.Text,
                textBox11.Text,
                textBox8.Text,
                textBox10.Text,
                textBox12.Text,
                comboBox2.Text,
                dateFormatted);




            Cursor.Current = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
         Form5paquete f5p = new Form5paquete(certificado,1);
            DialogResult res = f5p.ShowDialog();

            if (res == DialogResult.OK)
            {
                //recuperando la variable publica del formulario 2
                textBox2.Text = f5p.paqmin; //asignamos al texbox el dato de la variable
                textBox3.Text = f5p.paqmed; //asignamos al texbox el dato de la variable
                textBox4.Text = f5p.paqmax; //asignamos al texbox el dato de la variable
            }
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5galvanizado f5g = new Form5galvanizado(certificado,1);
            DialogResult res = f5g.ShowDialog();

            if (res == DialogResult.OK)
            {
                //recuperando la variable publica del formulario 2
                min1.Text = f5g.galmin; //asignamos al texbox el dato de la variable
                med1.Text = f5g.galmed; //asignamos al texbox el dato de la variable
                max1.Text = f5g.galmax; //asignamos al texbox el dato de la variable
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5paquete f5p = new Form5paquete(certificado, 2);
            DialogResult res = f5p.ShowDialog();

            if (res == DialogResult.OK)
            {
                //recuperando la variable publica del formulario 2
                textBox7.Text = f5p.paqmin; //asignamos al texbox el dato de la variable
                textBox9.Text = f5p.paqmed; //asignamos al texbox el dato de la variable
                textBox11.Text = f5p.paqmax; //asignamos al texbox el dato de la variable
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"\\nas01\AppIMEDEXSA\CargarDatosPintura\WindowsFormsApp2.application";
            p.Start();
        }

      


    }

}