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
    public partial class formPedido : Form
    {
        string pedido;
        private ConsultasBD fbd;
        public DataTable dtDef = new DataTable();


        public formPedido(string pedidoAux)
        {
            InitializeComponent();


            fbd = new ConsultasBD();

            pedido = pedidoAux;

            DataTable dt = fbd.obtenerLineasPedido(pedido);
            foreach (DataRow row in dt.Rows)
            {
                dgLineasAux.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), true);
            }
        }



        private void btnAñadirLineas_Click(object sender, EventArgs e)
        {

            dtDef.Columns.Add("Codigo");
            dtDef.Columns.Add("Cantidad");
            dtDef.Columns.Add("Pedido");
            dtDef.Columns.Add("Linea");

            foreach (DataGridViewRow row in dgLineasAux.Rows)
            {
                if (row.Cells[4].Value.Equals(true)) {
                    dtDef.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                    
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnMarcarTodas_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgLineasAux.Rows)
            {
                row.Cells[4].Value = true;
            }
        }

        private void btnDesmarcar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgLineasAux.Rows)
            {
                row.Cells[4].Value = false;
            }
        }
    }
}
