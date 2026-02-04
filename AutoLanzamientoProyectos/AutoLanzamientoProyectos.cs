using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoLanzamientoProyectos
{
    public partial class AutoLanzamientoProyectos : Form
    {

        DButils dbu = new DButils();
        DataView dv;

        DataTable buffer = new DataTable();

        public AutoLanzamientoProyectos()
        {
            InitializeComponent();
        }

        private void AutoLanzamientoProyectos_Load(object sender, EventArgs e)
        {
            cargarTabla();
        }

        private void cargarTabla()
        {
            DataTable proyectos = dbu.getProyectos();
            buffer = dbu.getBufferLanza();
            DataTable almacenes = dbu.getAlmacenes();

            foreach (DataRow row in proyectos.Rows)
            {
                comboLanza.Items.Add(row[0].ToString() + " - " + row[1].ToString());
            }

            foreach (DataRow row in almacenes.Rows)
            {
                comboAlm.Items.Add(row[1].ToString());
            }

            dgvBuffer.DataSource = buffer;     



            
        }

        /*private void dgvBuffer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }*/

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void eliminar()
        {
            DialogResult dialogResult = MessageBox.Show("Se van a eliminar de la programación " + dgvBuffer.SelectedRows.Count + " proyectos, ¿Continuar?", "ATENCIÓN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvBuffer.SelectedRows)
                {
                    dbu.eliminarLanza(row.Cells["PROYECTO"].Value.ToString());
                }

                cargarTabla();
                formatearColor();

                MessageBox.Show("Proyectos eliminados", "ATENCIÓN");
            }

        }

        private void tbBuscador_TextChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string busq = this.tbBuscador.Text.ToString();

            string filtro = "(PROYECTO LIKE '%" + busq + "%')";
            
            buffer.DefaultView.RowFilter = filtro;
            dv = buffer.DefaultView;

            formatearColor();

            Cursor.Current = Cursors.Default;
        }

        private void formatearColor()
        {
            foreach (DataGridViewRow dgvRow in dgvBuffer.Rows)
            {
                if (dgvRow.Cells[2].Value.ToString() == "COMPLETADO")
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.LightGreen;
                    dgvRow.DefaultCellStyle.ForeColor = Color.Black;

                    if (dgvRow.Cells["OBSERVACIONES"].Value.ToString() != "-")
                    {
                        dgvRow.DefaultCellStyle.BackColor = Color.LightYellow;
                        dgvRow.DefaultCellStyle.ForeColor = Color.Black;
                    }

                }
                else
                {
                    if (dgvRow.Cells["OBSERVACIONES"].Value.ToString() != "")
                    {

                        dgvRow.DefaultCellStyle.BackColor = Color.Red;
                        dgvRow.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
        }

        private void dgvBuffer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                eliminar();
            }
        }

        private void AutoLanzamientoProyectos_Activated(object sender, EventArgs e)
        {
            formatearColor();
        }
    }
}
