using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace DivisorLotes
{
    public partial class DivisorLotes : Form
    {
        DButils dbu = new DButils();
        string almacen;
        int idog = 0;
        int iddst = 0;

        public DivisorLotes()
        {
            InitializeComponent();
        }


        private void DivisorLotes_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            combo_almacen.Items.Clear();
            // Aqui consulto y muestro en el combox
            DataTable table;
            table = dbu.obtenerAlmacenes();

            foreach (DataRow row in table.Rows)
            {
                almacen = Convert.ToString(row["razons"]);
                almacen += " #"+Convert.ToString(row["codalm"]);
                combo_almacen.Items.Add(almacen);
                combo_destino.Items.Add(almacen);

            }
            Cursor.Current = Cursors.Default;

        }

        private void BTN_DIVIDIR_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("INTRODUZCA UN PESO");
                return;
            }
            if (this.textBoxLote.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("INTRODUZCA UN LOTE");
                return;
            }
            if (this.idog == 0)
            {
                System.Windows.Forms.MessageBox.Show("SELECCIONE UN ALMACÉN DE ORIGEN");
                return;
            }
            if (this.iddst == 0)
            {
                System.Windows.Forms.MessageBox.Show("SELECCIONE UN ALMACÉN DESTINO");
                return;
            }

            if (!dbu.esMayorQueConsumo(this.textBoxLote.Text, Convert.ToInt32(this.textBox1.Text)))
            {
                System.Windows.Forms.MessageBox.Show("EL STOCK RESTANTE ES MENOR QUE EL CONSUMIDO EN OPERACIONES. INTRODUZCA UN STOCK MENOR");
                return;
            }

            if (this.textBoxLote.Text != "" && this.textBox1.Text != "" && this.iddst != 0 && this.idog != 0)
            {
                string peso = this.textBox1.Text;
                if (peso.Contains(",") || peso.Contains("."))
                {
                    System.Windows.Forms.MessageBox.Show("EL PESO DEBE SER UN NÚMERO ENTERO");
                    return;
                }
                else
                {
                    dbu.Dividir(this.textBoxLote.Text, this.textBox1.Text, idog, iddst);
                }
                
            }
        }

        private void combo_almacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            almacen = combo_almacen.Text.ToString();
            int startindex = almacen.IndexOf("#");
            string amountString = almacen.Substring(startindex+1);
            idog = Int32.Parse(amountString);
        }

        private void combo_destino_SelectedIndexChanged(object sender, EventArgs e)
        {
            almacen = combo_destino.Text.ToString();
            int startindex = almacen.IndexOf("#");
            string amountString = almacen.Substring(startindex + 1);
            iddst = Int32.Parse(amountString);
        }





        
    }
}
