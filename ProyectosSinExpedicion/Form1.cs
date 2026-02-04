using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProyectosSinExpedicion
{
    public partial class Form1 : Form
    {

        private ConsultasBD fbd;


        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Consultar_Modificar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fbd = new ConsultasBD();
            string proyecto = tboxProyecto.Text;
        
            fbd.recuperarFichajesSinExpedicion(proyecto);

            DataTable dt = fbd.obtenerInformacionProyectos(proyecto);

            
            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = dt;
            dgProyectosSinExpedicion.DataSource = bindingSource;


            dgProyectosSinExpedicion.Columns[5].HeaderCell.Style.BackColor = Color.SkyBlue;
            dgProyectosSinExpedicion.Columns[6].HeaderCell.Style.BackColor = Color.SkyBlue;
            dgProyectosSinExpedicion.Columns[7].HeaderCell.Style.BackColor = Color.PowderBlue;
            dgProyectosSinExpedicion.Columns[8].HeaderCell.Style.BackColor = Color.PowderBlue;
            dgProyectosSinExpedicion.Columns[9].HeaderCell.Style.BackColor = Color.PowderBlue;
            dgProyectosSinExpedicion.Columns[10].HeaderCell.Style.BackColor = Color.Yellow;
            dgProyectosSinExpedicion.Columns[11].HeaderCell.Style.BackColor = Color.Yellow;
            dgProyectosSinExpedicion.Columns[12].HeaderCell.Style.BackColor = Color.Yellow;

            dgProyectosSinExpedicion.Columns[13].HeaderCell.Style.BackColor = Color.LightYellow;
            dgProyectosSinExpedicion.Columns[14].HeaderCell.Style.BackColor = Color.LightYellow;
            dgProyectosSinExpedicion.Columns[15].HeaderCell.Style.BackColor = Color.LightYellow;
            dgProyectosSinExpedicion.Columns[16].HeaderCell.Style.BackColor = Color.LightYellow;

            dgProyectosSinExpedicion.Columns[17].HeaderCell.Style.BackColor = Color.WhiteSmoke;
            dgProyectosSinExpedicion.Columns[18].HeaderCell.Style.BackColor = Color.WhiteSmoke;
            dgProyectosSinExpedicion.Columns[19].HeaderCell.Style.BackColor = Color.WhiteSmoke;
            dgProyectosSinExpedicion.Columns[20].HeaderCell.Style.BackColor = Color.WhiteSmoke;


            dgProyectosSinExpedicion.Columns[21].HeaderCell.Style.BackColor = Color.SandyBrown;
            dgProyectosSinExpedicion.Columns[22].HeaderCell.Style.BackColor = Color.SandyBrown;
            dgProyectosSinExpedicion.Columns[23].HeaderCell.Style.BackColor = Color.SandyBrown;
            dgProyectosSinExpedicion.Columns[24].HeaderCell.Style.BackColor = Color.SandyBrown;

            dgProyectosSinExpedicion.Columns[25].HeaderCell.Style.BackColor = Color.WhiteSmoke;
            dgProyectosSinExpedicion.Columns[26].HeaderCell.Style.BackColor = Color.WhiteSmoke;

            for (int fila = 0; fila < dgProyectosSinExpedicion.Rows.Count; fila++)
            {
                for (int col = 5; col < dgProyectosSinExpedicion.Rows[fila].Cells.Count-1; col++)
                {
                    if (dgProyectosSinExpedicion.Rows[fila].Cells[col].Value.ToString().Equals("0"))
                    {
                        dgProyectosSinExpedicion.Rows[fila].Cells[col].Style.BackColor = Color.Red;
                    }
                    else if (dgProyectosSinExpedicion.Rows[fila].Cells[col].Value.ToString().Equals("-1"))
                    {
                        dgProyectosSinExpedicion.Rows[fila].Cells[col].Style.BackColor = Color.LightGray;
                        dgProyectosSinExpedicion.Rows[fila].Cells[col].Value = "";
                    }
                }
            }

            foreach (DataGridViewRow row in dgProyectosSinExpedicion.Rows)
            {

                if (row.Cells[26].Value.ToString().Equals("0"))
                {
                    row.Cells[26].Value = "";
                }
                else if (row.Cells[26].Value.ToString().Equals("1"))
                {
                    row.Cells[26].Style.BackColor = Color.Green;
                    row.Cells[26].Value = "";
                 
                }
                else {
                    row.Cells[26].Style.BackColor = Color.Red;
                    row.Cells[26].Value = "";
                }


            }



             Cursor.Current = Cursors.Default;
        }
    }
}
