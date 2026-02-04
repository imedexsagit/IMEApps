using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TraductorCodigos
{
    public partial class TraductorCodigos : Form
    {
        DButils dbu = new DButils();
        
        
        public TraductorCodigos()
        {
            InitializeComponent();
        }

        private void TraductorCodigos_Load(object sender, EventArgs e)
        {
            /*this.comboClientes1.Items.Clear();

            foreach (DataRow row in dbu.obtenerClientes().Rows)
            {
                this.comboClientes1.Items.Add(row["CLIENTE"].ToString());
            }*/
        }

        

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                trad();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trad();            
        }

        private void trad()
        {
            DataTable datos = null;

            //string cliente = this.comboClientes1.SelectedItem.ToString();

            datos = dbu.traducir(this.textBox1.Text);

            if (datos.Rows.Count == 0)
            {
                MessageBox.Show("Código " + this.textBox1.Text + " no encontrado en la base de datos. Inténtelo de nuevo.", "ATENCIÓN",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (datos.Rows.Count > 1)
                {
                    this.comboClientes1.Items.Clear();
                    this.comboClientes1.Visible = true;
                    this.label3.Visible = true;
                    this.labelCliente.Visible = false;

                    foreach (DataRow row in dbu.obtenerClientesCodigo(this.textBox1.Text).Rows)
                    {
                        this.comboClientes1.Items.Add(row["CLIENTE"].ToString());
                    }
                }
                else
                {
                    this.comboClientes1.Visible = false;
                    this.labelCliente.Visible = true;
                    this.label3.Visible = true;
                    this.labelCliente.Text = datos.Rows[0][1].ToString();
                    this.tbTranslate.Text = datos.Rows[0][0].ToString();
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.comboBoxCliente.Items.Clear();
            this.textBoxCod.Clear();
            this.textBoxCodCLi.Clear();

            foreach (DataRow row in dbu.obtenerClientes().Rows)
            {
                this.comboBoxCliente.Items.Add(row["CLIENTE"].ToString());
            }

            this.comboBoxCliente.SelectedIndex = 0;
            this.panel1.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            dbu.nuevoCodigo(this.textBoxCodCLi.Text, this.textBoxCod.Text, this.comboBoxCliente.Text);
            
        }


        private void tbTranslate_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.comboClientes1.Visible = false;
            this.label3.Visible = false;
            this.labelCliente.Visible = false;
            this.tbTranslate.Clear();
        }

        private void comboClientes1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tbTranslate.Text = dbu.traducirConCliente(this.textBox1.Text,this.comboClientes1.SelectedItem.ToString());
        }
    }
}
