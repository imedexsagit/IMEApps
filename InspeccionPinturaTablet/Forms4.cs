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
    public partial class Forms4 : Form
    {
        string certificado;
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();

        public Forms4(string certificado)
        {

            InitializeComponent();
            this.certificado = certificado;

            DataTable table = null;
            table = ipDB.datos4InsRecubrimiento(certificado);

            foreach (DataRow row in table.Rows)
            {
                comboBox2.Items.Clear();
                comboBox4.Items.Clear();
                comboBox6.Items.Clear();
                foreach (DataRow row1 in ipDB.obtenerColoresPintura().Rows)
                {
                    comboBox2.Items.Add(row1[0].ToString());
                    comboBox4.Items.Add(row1[0].ToString());
                    comboBox6.Items.Add(row1[0].ToString());
                }

                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
                comboBox5.Items.Clear();
                foreach (DataRow row1 in ipDB.obtenerTiposPintura().Rows)
                {
                    comboBox1.Items.Add(row1[0].ToString());
                    comboBox3.Items.Add(row1[0].ToString());
                    comboBox5.Items.Add(row1[0].ToString());
                }

                //Fechas

                if (row["fecha"] != DBNull.Value)
                {
                    dateTimePicker.Text = Convert.ToString(row["fecha"]);
                }

                
                if (row["fechaplicacion1"] != DBNull.Value)
                {
                    dateTimePicker1.Text = Convert.ToString(row["fechaplicacion1"]);
                }

                
                if (row["fechaplicacion2"] != DBNull.Value)
                {
                    dateTimePicker2.Text = Convert.ToString(row["fechaplicacion2"]);
                }

                
                if (row["fechaplicacion3"] != DBNull.Value)
                {
                    dateTimePicker3.Text = Convert.ToString(row["fechaplicacion3"]);
                }

                textBox1.Text = Convert.ToString(row["rereclamacion"]);
                textBox2.Text = Convert.ToString(row["lote1"]);
                textBox3.Text = Convert.ToString(row["lote2"]);
                textBox4.Text = Convert.ToString(row["lote3"]);
                textBox5.Text = Convert.ToString(row["secadoreclamacion"]);

                comboBox1.Text = Convert.ToString(row["tipopintura1"]);
                comboBox3.Text = Convert.ToString(row["tipopintura2"]);
                comboBox5.Text = Convert.ToString(row["tipopintura3"]);

                comboBox2.Text = Convert.ToString(row["color1"]);
                comboBox4.Text = Convert.ToString(row["color2"]);
                comboBox6.Text = Convert.ToString(row["color3"]);

                if (row["recorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["recorrecto"]) == true)
                    {
                        radioButton1.Checked = true;
                    }
                    if (Convert.ToBoolean(row["recorrecto"]) == false)
                    {
                        radioButton2.Checked = true;
                    }
                }

                if (row["secado"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["secado"]) == true)
                    {
                        radioButton4.Checked = true;
                    }
                    if (Convert.ToBoolean(row["secado"]) == false)
                    {
                        radioButton3.Checked = true;
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int recorrecto = 2;
            int secado = 2;

            DateTime date = DateTime.Parse(dateTimePicker.Text);
            string dateFormatted = date.ToString("yyyy-MM-dd");

             DateTime date1 = DateTime.Parse(dateTimePicker1.Text);
            string dateFormatted1 = date1.ToString("yyyy-MM-dd");

             DateTime date2 = DateTime.Parse(dateTimePicker2.Text);
            string dateFormatted2 = date2.ToString("yyyy-MM-dd");

             DateTime date3 = DateTime.Parse(dateTimePicker3.Text);
            string dateFormatted3 = date3.ToString("yyyy-MM-dd");

            if (textBox1.Text == "") textBox1.Text = null;
            if (textBox2.Text == "") textBox2.Text = null;
            if (textBox3.Text == "") textBox3.Text = null;
            if (textBox4.Text == "") textBox4.Text = null;
            if (textBox5.Text == "") textBox5.Text = null;

            if (comboBox1.Text == "") comboBox1.Text = null;
            if (comboBox2.Text == "") comboBox2.Text = null;
            if (comboBox3.Text == "") comboBox3.Text = null;
            if (comboBox4.Text == "") comboBox4.Text = null;
            if (comboBox5.Text == "") comboBox5.Text = null;
            if (comboBox6.Text == "") comboBox6.Text = null;

          

            if (radioButton1.Checked == true) recorrecto = 1;
            if (radioButton2.Checked == true) recorrecto = 0;

            if (radioButton4.Checked == true) secado = 1;
            if (radioButton3.Checked == true) secado = 0;
      
          

            ipDB.actulizar4InsRecubrimiento(certificado,
                recorrecto,
                textBox1.Text,
                comboBox1.Text,
                textBox2.Text,
                comboBox2.Text,
                dateFormatted1,
                comboBox3.Text,
                textBox3.Text,
                comboBox4.Text,
                dateFormatted2,
                comboBox5.Text,
                textBox4.Text,
                comboBox6.Text,
                dateFormatted3,
                secado,
                textBox5.Text,
                dateFormatted);

            Cursor.Current = Cursors.Default;
        }

        private void Forms4_Load(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());
            string strsql = "";
            DataTable table = new DataTable();

            this.comboBox1.Items.Clear();
            this.comboBox3.Items.Clear();
            this.comboBox5.Items.Clear();


            try
            {

                conexion.Open();

                strsql += "select TIPO_PINTURA FROM TC_TIPOS_PINTURA WHERE CODEMP = " + empresaGlobalProj.empresaGlobal.empresaID;
                SqlCommand cmd = new SqlCommand(strsql, conexion);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);

                conexion.Close();

                foreach (DataRow row in table.Rows)
                {
                    this.comboBox1.Items.Add(row[0].ToString());
                    this.comboBox3.Items.Add(row[0].ToString());
                    this.comboBox5.Items.Add(row[0].ToString());
                }


            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       

     
    }
}
