using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InspeccionesTablets
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();


            if (Environment.UserName.Equals("victor.alvarez") || Environment.UserName.Equals("carlos.sanchez") || Environment.UserName.Equals("angel.Gonzalez") || Environment.UserName.Equals("joseluis.garcia") || Environment.UserName.Equals("tomas.fernandez"))
            {
                bindingNavigatorDeleteItem.Visible = true;
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == String.Empty)
            {
                // TODO: esta línea de código carga datos en la tabla 'ggDataSet.TC_InspeccionEtiquetas' Puede moverla o quitarla según sea necesario.
                this.tC_InspeccionEtiquetasTableAdapter.Fill(this.ggDataSet.TC_InspeccionEtiquetas);
            }
            else
            {
                this.tC_InspeccionEtiquetasTableAdapter.FillBy(this.ggDataSet.TC_InspeccionEtiquetas, Convert.ToInt32(this.toolStripTextBox1.Text));
            }
        }

        private void tC_InspeccionEtiquetasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Validate() == true)
                {
                    this.tC_InspeccionEtiquetasBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.ggDataSet);

                    MessageBox.Show("Datos salvados correctamente", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se puede guardar la modifcacion de la inspeccion, hay datos no válidos", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la inspeccion. " + ex.Message, "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (this.tC_InspeccionEtiquetasDataGridView.RowCount > 0)
            {
                if (MessageBox.Show("Está seguro que desea eliminar la inspeccion", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.tC_InspeccionEtiquetasBindingSource.EndEdit();
                    this.tC_InspeccionEtiquetasBindingSource.RemoveCurrent();

                    this.tC_InspeccionEtiquetasBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.ggDataSet);

                    MessageBox.Show("Inspeccion borrada correctamente", "Borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tC_InspeccionEtiquetasDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //Inspecciones CJS
            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Value == DBNull.Value)            
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Style.BackColor = Color.Red;


            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Style.BackColor = Color.Red;
            

            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Style.BackColor = Color.Red;
            

            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Style.BackColor = Color.Red;



            //Ensayos NO destructivos CJS
            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Style.BackColor = Color.Red;


            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Style.BackColor = Color.Red;


            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Style.BackColor = Color.Red;

            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Style.BackColor = Color.Red;
        
        }

    }
}
