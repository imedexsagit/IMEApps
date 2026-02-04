using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TorresStock
{
    public partial class Form1 : Form
    {

        private ConsultasBD fbd;

        public Form1()
        {

            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            fbd = new ConsultasBD();


            if (fbd.existeCodigo(tbCodigo.Text.ToString()))
            {
                int parsedValue;
                if (int.TryParse(tbCantidad.Text, out parsedValue))
                {
                    dgCodigoCantidad.Rows.Add(tbCodigo.Text.ToString(), parsedValue.ToString(), "", "");
                    tbCodigo.Text = "";
                    tbCantidad.Text = "";
                }
                else {
                    MessageBox.Show("El campo de cantidad no es un valor númerico.", "AVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                }

            }
            else {
                MessageBox.Show("El código introducido no existe", "AVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddPedido_Click(object sender, EventArgs e)
        {
            fbd = new ConsultasBD();

            if (fbd.existePedido(tbPedido.Text.ToString()))
            {
                using (var form = new formPedido(tbPedido.Text.ToString()))
                {
                    form.StartPosition = FormStartPosition.CenterParent;
                    var result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        DataTable dt = form.dtDef;
                              foreach (DataRow row in dt.Rows)
                              {
                                 dgCodigoCantidad.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
                             }
                             tbPedido.Text = "";

                        /*
                        string val = form.DevolverNumeroMarcado();           
                        tBoxPacking.Text = val;*/
                    }
                }




                //if (String.IsNullOrEmpty(textBox1.Text.ToString()))
                //{

                ////ESTO SI 
                //// DataTable dt = fbd.obtenerLineasPedido(tbPedido.Text.ToString());
                ////      foreach (DataRow row in dt.Rows)
               ////      {
                ////         dgCodigoCantidad.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
                ////     }
                ////    tbPedido.Text = "";
                ////ESTO SI 



                //}
                /*else
                {
                    DataTable dt = fbd.obtenerLineasEspecificasPedido(tbPedido.Text.ToString(), textBox1.Text.ToString());
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            dgCodigoCantidad.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
                        }
                        textBox1.Text = "";
                    }
                    else {
                        MessageBox.Show("La líneas para ese pedido no existen o el formato introducido en el campo línea no es el correcto", "AVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }*/
            }
            else
            {
                MessageBox.Show("El pedido introducido no existe", "AVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnEliminarFila_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgCodigoCantidad.SelectedRows)
            {
                dgCodigoCantidad.Rows.Remove(row);
            }
        }

        private void btnLimpiarTabla_Click(object sender, EventArgs e)
        {
            dgCodigoCantidad.Rows.Clear();
            dgCodigoCantidad.Refresh();
        }

        private void btnComprobarStock_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            fbd = new ConsultasBD();

            dgComprobacionStock.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            dgComprobacionStock.ColumnHeadersDefaultCellStyle.BackColor = Color.SlateBlue;
            dgComprobacionStock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgComprobacionStock.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            dgComprobacionStock.EnableHeadersVisualStyles = false;
            //dgComprobacionStock.RowTemplate.Height = 35;
            dgComprobacionStock.ReadOnly = true;
            dgComprobacionStock.RowHeadersVisible = false;
            dgComprobacionStock.AllowUserToAddRows = false;
            dgComprobacionStock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgComprobacionStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            fbd.vaciarTablaAux();

            fbd.insertarInformacionComprobacionStock(dgCodigoCantidad);
           
            if (rbTodos.Checked == true)
            {
                 DataTable dtC = fbd.obtenerStock();
                 var bindingSource = new System.Windows.Forms.BindingSource();
                 bindingSource.DataSource = dtC;
                 dgComprobacionStock.DataSource = bindingSource;
            }
            else if (rbStock.Checked == true)
            {
                DataTable dtC = fbd.obtenerStockNecesario();
                 var bindingSource = new System.Windows.Forms.BindingSource();
                 bindingSource.DataSource = dtC;
                 dgComprobacionStock.DataSource = bindingSource;
            }


            foreach (DataGridViewRow row in dgComprobacionStock.Rows)
            {
                if (Convert.ToInt32(row.Cells[2].Value.ToString()) < Convert.ToInt32(row.Cells[1].Value.ToString()))
                {

                    row.DefaultCellStyle.BackColor = Color.Salmon;
    
                }
            }

            Cursor.Current = Cursors.Default;

        }


    }
}
