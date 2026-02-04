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
    public partial class Form2 : Form
    {
        string certificado;
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();

        public Form2(string certificado)
        {
            this.certificado = certificado;

            InitializeComponent();

            DataTable table = null;
            table = ipDB.datos2InsGranallado(certificado);

            foreach (DataRow row in table.Rows)
            {
                textBox1.Text = Convert.ToString(row["aereclamacion"]);
                textBox2.Text = Convert.ToString(row["gereclamacion"]);
                textBox3.Text = Convert.ToString(row["zincinicial"]);
                textBox4.Text = Convert.ToString(row["zincfinal"]);

                textBox5.Text = Convert.ToString(row["zincinicialpieza1"]);
                textBox6.Text = Convert.ToString(row["zincinicialpieza2"]);
                textBox7.Text = Convert.ToString(row["zincinicialpieza3"]);
               

                textBox10.Text = Convert.ToString(row["zincinicialvalor1"]);
                textBox11.Text = Convert.ToString(row["zincinicialvalor2"]);
                textBox12.Text = Convert.ToString(row["zincinicialvalor3"]);
              

                textBox15.Text = Convert.ToString(row["zincgranalladopieza1"]);
                textBox16.Text = Convert.ToString(row["zincgranalladopieza2"]);
                textBox17.Text = Convert.ToString(row["zincgranalladopieza3"]);
              

                textBox20.Text = Convert.ToString(row["zincgranalladovalor1"]);
                textBox21.Text = Convert.ToString(row["zincgranalladovalor2"]);
                textBox22.Text = Convert.ToString(row["zincgranalladovalor3"]);
               
                


                if (row["aecorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["aecorrecto"]) == true)
                    {
                        radioButton1.Checked = true;
                    }
                    if (Convert.ToBoolean(row["aecorrecto"]) == false)
                    {
                        radioButton2.Checked = true;
                    }
                }

                if (row["gecorrecto"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["gecorrecto"]) == true)
                    {
                        radioButton4.Checked = true;
                    }
                    if (Convert.ToBoolean(row["gecorrecto"]) == false)
                    {
                        radioButton3.Checked = true;
                    }
                }

                if (row["zincISO"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["zincISO"]) == true)
                    {
                        radioButton6.Checked = true;
                    }
                    if (Convert.ToBoolean(row["zincISO"]) == false)
                    {
                        radioButton5.Checked = true;
                    }
                }

                if (row["fecha"] != DBNull.Value)
                {
                    dateTimePicker1.Text = Convert.ToString(row["fecha"]);
                }

                if (row["aeaplicable"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["aeaplicable"]) == true)
                    {
                        checkBox1.Checked = true;
                    }
                }

                if (row["geaplicable"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["geaplicable"]) == true)
                    {
                        checkBox2.Checked = true;
                    }
                }

                if (row["wasserpel"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["wasserpel"]) == true)
                    {
                        checkBox3.Checked = true;
                    }
                }

                if (row["bresle"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["bresle"]) == true)
                    {
                        checkBox4.Checked = true;
                    }
                }

                if (row["compresores"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["compresores"]) == true)
                    {
                        checkBox5.Checked = true;
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // PRIMERO CALCULOS LAS MEDIAS DE LOS ESPESORES DE ZINC
            int totalIinicial = 0, mediaIncial = 0;
            int dividendoInicial = 0;

            if (Convert.ToInt32(textBox10.Text) != 0) dividendoInicial = dividendoInicial + 1;
            if (Convert.ToInt32(textBox11.Text) != 0) dividendoInicial = dividendoInicial + 1;
            if (Convert.ToInt32(textBox12.Text) != 0) dividendoInicial = dividendoInicial + 1;


            totalIinicial = Convert.ToInt32(textBox10.Text) + Convert.ToInt32(textBox11.Text) + Convert.ToInt32(textBox12.Text);
                         
            if (dividendoInicial != 0)
            {
                mediaIncial = totalIinicial / dividendoInicial;
            }
            textBox3.Text = Convert.ToString(mediaIncial);

            int totalGranallado = 0, mediaGranallado = 0;
            int dividendoGranallado = 0;

            if (textBox20.Text == "") textBox20.Text = "0";
            if (textBox21.Text == "") textBox21.Text = "0";
            if (textBox22.Text == "") textBox22.Text = "0";

            if (Convert.ToInt32(textBox20.Text) != 0) dividendoGranallado = dividendoGranallado + 1;
            if (Convert.ToInt32(textBox21.Text) != 0) dividendoGranallado = dividendoGranallado + 1;
            if (Convert.ToInt32(textBox22.Text) != 0) dividendoGranallado = dividendoGranallado + 1;

            totalGranallado = Convert.ToInt32(textBox20.Text) + Convert.ToInt32(textBox21.Text) + Convert.ToInt32(textBox22.Text);

            if (dividendoGranallado != 0)
            {
                mediaGranallado = totalGranallado / dividendoGranallado;
                textBox4.Text = Convert.ToString(mediaGranallado);
            }

            int aecorrecto = 2;
            int georrecto = 2;
            int zincIS0 = 2;

            int aeaplicable = 0;
            int geaplicable = 0;

            int wasserpel = 0;
            int bresle = 0;
            int compresores = 0;

            DateTime date = DateTime.Parse(dateTimePicker1.Text);
            string dateFormatted = date.ToString("yyyy-MM-dd");

            if (textBox1.Text == "") textBox1.Text = null;
            if (textBox2.Text == "") textBox2.Text = null;
            if (textBox3.Text == "") textBox3.Text = null;
            if (textBox4.Text == "") textBox4.Text = null;

            if (textBox5.Text == "") textBox5.Text = null;
            if (textBox6.Text == "") textBox6.Text = null;
            if (textBox7.Text == "") textBox7.Text = null;
          
            if (textBox10.Text == "") textBox10.Text = null;
            if (textBox11.Text == "") textBox11.Text = null;
            if (textBox12.Text == "") textBox12.Text = null;
          
            if (textBox15.Text == "") textBox15.Text = null;
            if (textBox16.Text == "") textBox16.Text = null;
            if (textBox17.Text == "") textBox17.Text = null;
            
            if (textBox20.Text == "") textBox20.Text = null;
            if (textBox21.Text == "") textBox21.Text = null;
            if (textBox22.Text == "") textBox22.Text = null;
           
            if (radioButton1.Checked == true) aecorrecto = 1;
            if (radioButton2.Checked == true) aecorrecto = 0;

            if (radioButton4.Checked == true) georrecto = 1;
            if (radioButton3.Checked == true) georrecto = 0;

            if (radioButton6.Checked == true) zincIS0 = 1;
            if (radioButton5.Checked == true) zincIS0 = 0;

            if (checkBox1.Checked == true) aeaplicable = 1;
            if (checkBox2.Checked == true) geaplicable = 1;

            if (checkBox3.Checked == true) wasserpel = 1;
            if (checkBox4.Checked == true) bresle = 1;
            if (checkBox5.Checked == true) compresores = 1;

            ipDB.actulizar2InsGranallado(certificado,
                aecorrecto,
                textBox1.Text,
                aeaplicable,
                georrecto,
                textBox2.Text,
                geaplicable,
                zincIS0,
                textBox3.Text,
                textBox4.Text,
                dateFormatted,
                textBox5.Text,
                textBox6.Text,
                textBox7.Text,
                Convert.ToInt32(textBox10.Text),
                Convert.ToInt32(textBox11.Text),
                Convert.ToInt32(textBox12.Text),
                textBox15.Text,
                textBox16.Text,
                textBox17.Text,
                Convert.ToInt32(textBox20.Text),
                Convert.ToInt32(textBox21.Text),
                Convert.ToInt32(textBox22.Text),
                wasserpel,
                bresle,
                compresores
                );

            Cursor.Current = Cursors.Default;
            MessageBox.Show("CAMBIOS GUARDADOS CORRECTAMENTE");
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            /*
            int total = 0, media = 0;

            total = Convert.ToInt32(textBox10.Text) + Convert.ToInt32(textBox11.Text) + Convert.ToInt32(textBox12.Text) 
                  + Convert.ToInt32(textBox13.Text) + Convert.ToInt32(textBox14.Text);

            media = total / 5;

            textBox3.Text = Convert.ToString(media);
             */
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        
    }
}
