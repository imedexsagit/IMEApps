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
    public partial class Form8Cliente : Form
    {
        string certificado;
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();

        public Form8Cliente(string certificado)
        {
            InitializeComponent();
            this.certificado = certificado;

            DataTable table = null;
            table = ipDB.datosa8InspCarga(certificado);

            foreach (DataRow row in table.Rows)
            {

                textBox.Text = Convert.ToString(row["especifiTitulo1"]);
                textBox1.Text = Convert.ToString(row["especifiNote1"]);

                textBox2.Text = Convert.ToString(row["especifiTitulo2"]);
                textBox3.Text = Convert.ToString(row["especifiNote2"]);

                textBox4.Text = Convert.ToString(row["especifiTitulo3"]);
                textBox5.Text = Convert.ToString(row["especifiNote3"]);

                textBox6.Text = Convert.ToString(row["especifiTitulo4"]);
                textBox7.Text = Convert.ToString(row["especifiNote4"]);




                if (row["especifiResult1"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["especifiResult1"]) == true)
                    {
                        checkBox1.Checked = true;
                    }
                }

                if (row["especifiResult2"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["especifiResult2"]) == true)
                    {
                        checkBox2.Checked = true;
                    }
                }

                if (row["especifiResult3"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["especifiResult3"]) == true)
                    {
                        checkBox3.Checked = true;
                    }
                }

                if (row["especifiResult4"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(row["especifiResult4"]) == true)
                    {
                        checkBox4.Checked = true;
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int result1 = 0, result2 = 0, result3 = 0,result4 = 0;

            if (textBox1.Text == "") textBox1.Text = null;
            if (textBox2.Text == "") textBox2.Text = null;
            if (textBox3.Text == "") textBox3.Text = null;
            if (textBox4.Text == "") textBox4.Text = null;
            if (textBox.Text == "") textBox.Text = null;
            if (textBox6.Text == "") textBox6.Text = null;
            if (textBox7.Text == "") textBox7.Text = null;
      

            if (checkBox1.Checked == true) result1 = 1;
            if (checkBox2.Checked == true) result2 = 2;
            if (checkBox3.Checked == true) result3 = 3;
            if (checkBox4.Checked == true) result4 = 4;

            ipDB.actulizar8Especificaciones(certificado, textBox.Text, result1, textBox1.Text, textBox2.Text, result2, textBox3.Text,
                textBox4.Text, result3, textBox5.Text, textBox6.Text, result4, textBox7.Text);
            Cursor.Current = Cursors.Default;
        }

       
       

    
   

    } 
}
