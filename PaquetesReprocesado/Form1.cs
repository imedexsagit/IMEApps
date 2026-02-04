using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaquetesReprocesado
{
    public partial class Form1 : Form
    {
        DButils dbu = new DButils();
        DataTable datos = new DataTable();
        DataTable busquedaPaqs = new DataTable();
        DataTable datosPaqueteGalva = new DataTable();

        DataTable original = new DataTable();

        DataTable seleccion = new DataTable();

        string paqSearchString = "";

        bool esCamion = false;
        string idPaquete = "";

        bool enDatos = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //this.dgvPaquetes.DataSource = dbu.getMarcasPaqueteGalv(this.textBox1.Text);
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            datos = dbu.getPaqsReprocesado();
            this.cbFiltroSede.SelectedIndex = 0;
            

            this.dgvPaquetes.DataSource = datos;


            this.datosPaqueteGalva.Columns.Add("MARCA", typeof(System.String));
            this.datosPaqueteGalva.Columns.Add("ETIQUETA", typeof(System.String));
            this.datosPaqueteGalva.PrimaryKey = new DataColumn[] { datosPaqueteGalva.Columns["ETIQUETA"] };
            this.datosPaqueteGalva.Columns.Add("CANTIDAD", typeof(System.Int32));
            this.datosPaqueteGalva.Columns.Add("PAQUETE", typeof(System.String));
            this.datosPaqueteGalva.Columns.Add("ESTADO", typeof(System.String));

            this.dgvContenido.DataSource = datosPaqueteGalva;

            busquedaPaqs = dbu.getMarcasPaqueteGalv("000", false);
            this.dgvBusqueda.DataSource = busquedaPaqs;

            this.cbExpeds.Items.Clear();
            this.cbExpeds.Items.Add("-");

            foreach (DataRow row in dbu.getExpeds().Rows)
            {
                cbExpeds.Items.Add(row[0].ToString());
            }

            this.cbExpeds.SelectedIndex = 0;
            this.cbSede.SelectedIndex = 0;

            

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnBuscarGalva.PerformClick();
            } 
        }

        private void btnBuscarGalva_Click(object sender, EventArgs e)
        {
            dgvBusqueda.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

            if (this.textBox2.Text != "")
            {
                busquedaPaqs = dbu.getMarcasPaqueteGalv(this.textBox2.Text, false);
                this.dgvBusqueda.DataSource = busquedaPaqs;
                paqSearchString = this.textBox2.Text;
                this.esCamion = false;
            }
            else if (this.tbCamion.Text != "")
            {
                busquedaPaqs = dbu.getMarcasPaqueteGalv(this.tbCamion.Text, true);
                this.dgvBusqueda.DataSource = busquedaPaqs;
                paqSearchString = this.tbCamion.Text;
                this.esCamion = true;
            }
        }

        private void btnPaqNuevo_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = true;
            this.btnPaqNuevo.Visible = false;
            this.btnEliminarPaquete.Visible = false;
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.btnCancelar.Visible = true;

            this.labelId.Text = "RE" + dbu.getNuevoId();
            

            this.datosPaqueteGalva.Rows.Clear();
            this.textBox2.Clear();
            this.busquedaPaqs.Rows.Clear();

            this.btnConfirmarPaquete.Visible = true;
            //this.btnGuardarCambios.Visible = false;

            this.btnAddMarca.Enabled = false;
            
        }

        private void btnAddMarca_Click(object sender, EventArgs e)
        {
            seleccion = busquedaPaqs.Clone();

            foreach (DataGridViewRow dgvrow in dgvBusqueda.SelectedRows)
            {
                seleccion.ImportRow(busquedaPaqs.Rows[dgvrow.Index]);
            }

            if (seleccion.Rows.Count > 0)
            {
                dgvBusqueda.Enabled = false;
                btnAddMarca.Enabled = false;
                //btnGuardarCambios.Enabled = false;
                btnEliminar.Enabled = false;
                panelCantidad.Visible = true;

                labelMarcaPanel.Text = seleccion.Rows[0]["MARCA"].ToString();
                nupCantidad.Maximum = (int)seleccion.Rows[0]["CANTIDAD"];
                nupCantidad.Value = (int)seleccion.Rows[0]["CANTIDAD"];

            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            enDatos = false;
            this.labelId.Text = "-";
            dgvBusqueda.Enabled = true;
            btnAddMarca.Enabled = true;
            //btnGuardarCambios.Enabled = true;
            btnEliminar.Enabled = true;
            panelCantidad.Visible = false;
            resetearListaPaquetes();
        }

        private void btnConfirmarPaquete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Confirmar paquete?", "ATENCIÓN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                string tipo = "BLANCO";
                if (radioNegro.Checked) tipo = "NEGRO";
                int id = dbu.insertarPaqueteReproc(cbExpeds.Text, tipo, cbSede.SelectedItem.ToString());
                
                MessageBox.Show("Paquete nuevo confirmado", "ATENCIÓN");

                //btnCancelar.PerformClick();
                btnAddMarca.Enabled = true;
                this.btnConfirmarPaquete.Visible = false;
                //this.btnGuardarCambios.Visible = true;
                idPaquete = this.labelId.Text.Substring(2);
            }
        }

        public void datosPaquete(string paqueID)
        {
            this.btnAddMarca.Enabled = true;
            this.panel1.Visible = true;
            this.btnPaqNuevo.Visible = false;
            this.btnEliminarPaquete.Visible = false;
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.btnCancelar.Visible = true;

            cbExpeds.SelectedIndex = cbExpeds.FindStringExact(dgvPaquetes.SelectedRows[0].Cells["EXPEDICIÓN"].Value.ToString());
            cbSede.SelectedIndex = cbSede.FindStringExact(dgvPaquetes.SelectedRows[0].Cells["SEDE"].Value.ToString());


            if (dgvPaquetes.SelectedRows[0].Cells["TIPO"].Value.ToString() != "BLANCO")
            {
                radioNegro.Checked = true;
            }
            else
            {
                radioBlanco.Checked = true;
            }

            this.labelId.Text = "RE" + paqueID;


            /*this.datosPaqueteGalva.Rows.Clear();
            this.textBox2.Clear();
            this.busquedaPaqs.Rows.Clear();*/

            datosPaqueteGalva = dbu.getMarcasPaqueteReproc(paqueID);

            original = datosPaqueteGalva.Clone();

            foreach (DataRow row in datosPaqueteGalva.Rows)
            {
                original.Rows.Add(row.ItemArray);
            }

            this.dgvContenido.DataSource = datosPaqueteGalva;
            this.dgvContenido.Refresh();

            this.btnConfirmarPaquete.Visible = false;
            //this.btnGuardarCambios.Visible = true;
        }

        private void dgvPaquetes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            //datosPaquete();
        }

        private void resetearListaPaquetes()
        {
            this.panel1.Visible = false;
            this.btnPaqNuevo.Visible = true;
            this.btnEliminarPaquete.Visible = true;
            this.label1.Visible = true;
            this.label2.Visible = true;
            this.btnCancelar.Visible = false;
            datos = dbu.getPaqsReprocesado();
            this.dgvPaquetes.DataSource = datos;
        }

        private void btnEliminarPaquete_Click(object sender, EventArgs e)
        {
            string ID = dgvPaquetes.SelectedRows[0].Cells["PAQUETE"].Value.ToString();
            DialogResult dialogResult = MessageBox.Show("¿Eliminar paquete " + ID + "?", "ATENCIÓN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                dbu.eliminarPaqReproc(ID);
                resetearListaPaquetes();
                MessageBox.Show("Paquete eliminado", "ATENCIÓN");
            }
        }

        private void guardarCambios()
        {
            if (this.labelId.Text != "-" && enDatos)
            {
                idPaquete = this.labelId.Text.Substring(2);
                string tipo = "BLANCO";
                if (radioNegro.Checked) tipo = "NEGRO";
                dbu.updatePaqueteReproc(idPaquete, cbExpeds.Text, tipo, cbSede.SelectedItem.ToString());
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            guardarCambios();



            /*foreach (DataRow row in datosPaqueteGalva.Rows)
            {
                if (original.AsEnumerable().Where(c => c.Field<string>("ETIQUETA").Equals(row["ETIQUETA"])).Count() == 0)
                {
                    //No la contiene, insertar en BD
                    //insertar.Rows.Add(row);
                    insertar.ImportRow(row);
                }
                
            }
            dbu.insertarMarcasPaquete(insertar, id.ToString());

            foreach (DataRow row in original.Rows)
            {
                if (datosPaqueteGalva.AsEnumerable().Where(c => c.Field<string>("ETIQUETA").Equals(row["ETIQUETA"])).Count() == 0)
                {
                    //No la contiene, eliminar de BD
                    //eliminar.Rows.Add(row);
                    eliminar.ImportRow(row);
                }

            }

            dbu.eliminarMarcasPaquete(eliminar, id);*/

            MessageBox.Show("Paquete actualizado", "ATENCIÓN");
        }

        private void dgvPaquetes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            busquedaPaqs = dbu.getMarcasPaqueteGalv("000", false);
            this.dgvBusqueda.DataSource = busquedaPaqs;
            idPaquete = dgvPaquetes.SelectedRows[0].Cells["PAQUETE"].Value.ToString().Substring(2);
            datosPaquete(idPaquete);
            enDatos = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvContenido.SelectedRows)
            {
                string etiq = row.Cells[1].Value.ToString();

                DataRow[] rem = datosPaqueteGalva.Select("ETIQUETA = '" + etiq + "'");

                if (rem[0] != null)
                {
                    datosPaqueteGalva.Rows.Remove(rem[0]);
                }

                dbu.eliminarMarcasPaquete(etiq, idPaquete);

            }
            datosPaquete(idPaquete);
            if (paqSearchString != "")
            {
                busquedaPaqs = dbu.getMarcasPaqueteGalv(paqSearchString, esCamion);
            }
            this.dgvBusqueda.DataSource = busquedaPaqs;
            
        }

        private void loopCantidad(DataRow row)
        {
            
            DataRow rowNueva = datosPaqueteGalva.NewRow();

            rowNueva["MARCA"] = row["MARCA"].ToString();
            rowNueva["ETIQUETA"] = row["ETIQUETA"].ToString();
            rowNueva["CANTIDAD"] = nupCantidad.Value;
            rowNueva["PAQUETE"] = row["PAQUETE"].ToString();
            rowNueva["ESTADO"] = "PENDIENTE";


            /*if (datosPaqueteGalva.AsEnumerable().Where(c => c.Field<string>("ETIQUETA").Equals(rowNueva["ETIQUETA"])).Count() == 0)
            {
                datosPaqueteGalva.Rows.Add(rowNueva);
            }*/

            //busquedaPaqs.Rows.Remove(busquedaPaqs.Rows[row.Index]);

            dbu.insertarMarcasPaquete(rowNueva, idPaquete);
            busquedaPaqs = dbu.getMarcasPaqueteGalv(paqSearchString, esCamion);
            this.dgvBusqueda.DataSource = busquedaPaqs;

            labelMarcaPanel.Text = row["MARCA"].ToString();
            nupCantidad.Maximum = (int)row["CANTIDAD"];
            nupCantidad.Value = (int)row["CANTIDAD"];

            datosPaquete(idPaquete);

        }

        private void btnAñad_Click(object sender, EventArgs e)
        {
            loopCantidad(seleccion.Rows[0]);
            seleccion.Rows.Remove(seleccion.Rows[0]);
            if (seleccion.Rows.Count <= 0)
            {
                dgvBusqueda.Enabled = true;
                btnAddMarca.Enabled = true;
                //btnGuardarCambios.Enabled = true;
                btnEliminar.Enabled = true;
                panelCantidad.Visible = false;

            }
            else
            {
                labelMarcaPanel.Text = seleccion.Rows[0]["MARCA"].ToString();
                nupCantidad.Maximum = (int)seleccion.Rows[0]["CANTIDAD"];
                nupCantidad.Value = (int)seleccion.Rows[0]["CANTIDAD"];
            }
        }

        private void btnCerrarCantidad_Click(object sender, EventArgs e)
        {
            dgvBusqueda.Enabled = true;
            btnAddMarca.Enabled = true;
            //btnGuardarCambios.Enabled = true;
            btnEliminar.Enabled = true;
            panelCantidad.Visible = false;
            this.seleccion.Rows.Clear();
            datosPaquete(idPaquete);
            busquedaPaqs = dbu.getMarcasPaqueteGalv(paqSearchString, esCamion);
            this.dgvBusqueda.DataSource = busquedaPaqs;
        }

        private void tbFiltro_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void checkBlanco_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBlanco.Checked && !checkNegro.Checked)
            {
                checkBlanco.Checked = true;
            }
            filtrar();
        }

        private void checkNegro_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBlanco.Checked && !checkNegro.Checked)
            {
                checkNegro.Checked = true;
            }
            filtrar();
        }

        private void filtrar()
        {
            string filtro = "";

            if (this.tbFiltro.Text != "")
            {
                filtro = "(PAQUETE LIKE '%" + this.tbFiltro.Text + "%') OR (Convert(EXPEDICIÓN, System.String) LIKE '%" + this.tbFiltro.Text + "%')";
            }

            if (!this.checkBlanco.Checked || !this.checkNegro.Checked)
            {
                if (filtro != "")
                {
                    filtro += " AND ";
                }

                if (this.checkNegro.Checked)
                {
                    filtro += "(TIPO LIKE 'NEGRO')";
                }
                else
                {
                    filtro += "(TIPO LIKE 'BLANCO')";
                }
            }

            if (cbFiltroSede.Text != "TODAS")
            {
                if (filtro != "")
                {
                    filtro += " AND ";
                }

                filtro += " (SEDE LIKE '" + cbFiltroSede.Text + "') ";
            }
            

            datos.DefaultView.RowFilter = filtro;
        }


        private void radioBlanco_CheckedChanged(object sender, EventArgs e)
        {
            guardarCambios();
        }

        private void cbExpeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            guardarCambios();
        }

        private void cbSede_SelectedIndexChanged(object sender, EventArgs e)
        {
            guardarCambios();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void tbCamion_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.tbCamion.Clear();
        }

        private void tbCamion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnBuscarGalva.PerformClick();
            } 
        }
    }
}
