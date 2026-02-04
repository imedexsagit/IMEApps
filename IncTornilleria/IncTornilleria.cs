using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace IncTornilleria
{
    public partial class IncTornilleria : Form
    {

        DButils dbu = new DButils();

        public IncTornilleria()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int existe = this.dbu.existePadre(this.textBox1.Text);
            switch (existe)
            {
                case 0:
                    MessageBox.Show("El código padre introducido no existe en la base de datos. Inténtelo de nuevo.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 2:
                    MessageBox.Show("El código padre introducido ya tiene asociados componentes. Introduzca otro código.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 3:
                    MessageBox.Show("El código padre introducido no corresponde a tornillería. Introduzca otro código.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 4:
                    MessageBox.Show("El código padre introducido no se corresponde con un incremento de tornillería. Introduzca otro código.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

            }

            if (existe != 1)
            {
                return;
            }

            if (todoOK())
            {
                int incr = 10;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index != dataGridView1.Rows.Count-1)
                    {
                        dbu.guardarIncrementos(row.Cells[0].Value.ToString(), this.textBox1.Text, Convert.ToInt32(row.Cells[1].Value.ToString()), incr);                        
                        incr += 10;
                    }
                }

                MessageBox.Show("Incrementos de tornillería guardados correctamente.", "ATENCIÓN", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Hay códigos cuyo estado no es correcto. Revise las líneas en rojo.", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPegar_Click(object sender, EventArgs e)
        {
            string estado = "";
            DataObject o = (DataObject)Clipboard.GetDataObject();
            if (o.GetDataPresent(DataFormats.StringFormat))
            {
                string[] pastedRows = Regex.Split(o.GetData(DataFormats.StringFormat).ToString().TrimEnd("\r\n".ToCharArray()), "\r");
                int j = 0;
                try { j = dataGridView1.CurrentRow.Index; }
                catch { }
                foreach (string pastedRow in pastedRows)
                {
                    DataGridViewRow r = new DataGridViewRow();
                    r.CreateCells(dataGridView1, pastedRow.Split(new char[] { '\t' }));
                    dataGridView1.Rows.Insert(j, r);
                    if (j > 0) dataGridView1.Rows[j].Cells[0].Value = dataGridView1.Rows[j].Cells[0].Value.ToString().Remove(0, 1);

                    if (dataGridView1.Rows[j].Cells[1].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[j].Cells[1].Value.ToString()) && esNumero(dataGridView1.Rows[j].Cells[1].Value.ToString()))
                    {
                        estado = dbu.estadoTornillo(dataGridView1.Rows[j].Cells[0].Value.ToString());
                    }
                    else
                    {
                        estado = "CANTIDAD INCORRECTA";
                    }
                    
                    dataGridView1.Rows[j].Cells[2].Value = estado;

                    if (estado != "CORRECTO")
                    {
                        dataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        dataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    j++;
                }
            }
        }

        bool todoOK()
        {
            bool OK = true;
            int contador = 1;
            string estado = "";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index != dataGridView1.Rows.Count - 1)
                {
                    if (row.Cells[1].Value != null && !string.IsNullOrEmpty(row.Cells[1].Value.ToString()) && esNumero(row.Cells[1].Value.ToString()))
                    {
                        estado = dbu.estadoTornillo(row.Cells[0].Value.ToString());
                    }
                    else
                    {
                        estado = "CANTIDAD INCORRECTA";
                    }

                    

                    row.Cells[2].Value = estado;

                    if (estado != "CORRECTO")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }

            while (OK && (contador < dataGridView1.Rows.Count))
            {
                if (dataGridView1.Rows[contador-1].Cells[2].Value.ToString() != "CORRECTO") OK = false;
                contador++;
            }

            return OK;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void btnLimpiarSeleccion_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private bool esNumero(string strNum)
        {
            if (strNum == null)
            {
                return false;
            }
            try
            {
                int d = Int32.Parse(strNum);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }
    }
}
