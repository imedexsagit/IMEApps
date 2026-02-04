using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace GestorPesoClientes
{
    public partial class GestorPesoClientes : Form
    {
        DButils dbu = new DButils();
        DataTable tableOriginal;
        DataTable table;
        List<string> marcadosOG;
        List<string> marcados = new List<string>();

        public GestorPesoClientes()
        {
            InitializeComponent();
        }

        private void GestorPesoClientes_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            tableOriginal = dbu.obtenerClientes();
            tableOriginal.Columns.Add("CON_PESO", typeof(bool));
            //string nombre = "";

            table = tableOriginal;

           

            marcados = dbu.obtenerMarcados(); //se obtiene la lista de clientes marcados
            marcadosOG = new List<string>(marcados);

            foreach (DataRow row in table.Rows)
            {
                if (marcados.Contains(row["CODCLI"].ToString()))
                {
                    row["CON_PESO"] = false;
                }
                else
                {
                    row["CON_PESO"] = true;
                }
                /*nombre = Convert.ToString(row["nombre"]);
                nombre += " [" + Convert.ToString(row["abrev"]) + "]";
                ListaClientes.Items.Add(nombre);

                if (marcados.Contains(nombre)){
                    ListaClientes.SetItemChecked(ListaClientes.Items.Count - 1, true);
                }*/
                

            }
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.DataSource = table;
            this.dataGridView1.Columns[2].Width = 420;

            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            
            Cursor.Current = Cursors.Default;
        }

        private void buscador_TextChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string busq = this.buscador.Text.ToString();

            table.DefaultView.RowFilter = "NOMBRE LIKE '%" + busq + "%' OR CODCLI LIKE '%" + busq + "%' OR ABREV LIKE '%" + busq + "%'";
            /*ListaClientes.Items.Clear();
            string nombre = "";
            foreach (DataRow row in table.Rows)
            {
                nombre = Convert.ToString(row["nombre"]);
                nombre += " [" + Convert.ToString(row["abrev"]) + "]";
                if ((nombre.ToUpper()).Contains(buscador.Text.ToString().ToUpper()))
                {
                    ListaClientes.Items.Add(nombre);
                    if (marcados.Contains(nombre))
                    {
                        ListaClientes.SetItemChecked(ListaClientes.Items.Count - 1, true);
                    }
                }
            }*/
            reflejarMarcados();
            this.mostrando.Text = "TODOS";
            Cursor.Current = Cursors.Default;
        }

        private void ListaClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* marcados.Clear();
            foreach (object itemChecked in ListaClientes.CheckedItems)
            {
                marcados.Add(itemChecked.ToString());
            }*/
        }

        private void btn_todos_Click(object sender, EventArgs e)
        {
            table.DefaultView.RowFilter = "true";
           /* ListaClientes.Items.Clear();
            string nombre = "";
            foreach (DataRow row in table.Rows)
            {
                nombre = Convert.ToString(row["nombre"]);
                nombre += " [" + Convert.ToString(row["abrev"]) + "]";
                ListaClientes.Items.Add(nombre);

                if (marcados.Contains(nombre))
                {
                    ListaClientes.SetItemChecked(ListaClientes.Items.Count - 1, true);
                }
                //test = Convert.ToString(table.Rows[i]["nombre"]);


            }*/
            reflejarMarcados();
            this.mostrando.Text = "TODOS";
        }

        private void btn_marcados_Click(object sender, EventArgs e)
        {
            table.DefaultView.RowFilter = "CON_PESO = true";
            reflejarMarcados();
            this.mostrando.Text = "MARCADOS";
        }

        private void btn_desmarcados_Click(object sender, EventArgs e)
        {
            table.DefaultView.RowFilter = "CON_PESO = false";
            reflejarMarcados();
            
            this.mostrando.Text = "DESMARCADOS";
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            dbu.guardarCambios(table);
            this.marcados = dbu.obtenerMarcados();
            this.marcadosOG = new List<string>(marcados);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            if (marcados.Contains(dataGridView1.CurrentRow.Cells["CODCLI"].Value.ToString()))
            {
                //marcados.Remove(table.Rows[index][0].ToString());
                marcados.Remove(dataGridView1.CurrentRow.Cells["CODCLI"].Value.ToString());
                reflejarMarcados();
            }
            else
            {
                
                //marcados.Add(table.Rows[index][0].ToString());
                marcados.Add(dataGridView1.CurrentRow.Cells["CODCLI"].Value.ToString());
                reflejarMarcados();
            }

            
        }

        private void reflejarMarcados()
        {
            foreach (DataRow row in table.Rows)
            {
                if (marcados.Contains(row["CODCLI"].ToString()))
                {
                    row["CON_PESO"] = false;
                }
                else
                {
                    row["CON_PESO"] = true;
                }
            }

            string codcli;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["CODCLI"].Value != null)
                {
                    codcli = row.Cells["CODCLI"].Value.ToString();
                    if (marcados.Contains(codcli))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        public void colorLoad()
        {
            this.btn_todos.PerformClick();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!(marcados.All(marcadosOG.Contains) && (marcados.Count == marcadosOG.Count)))
            {
                if (MessageBox.Show("Se han detectado cambios\n¿Salir sin guardar?", "ATENCIÓN", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {

                    e.Cancel = true;

                }
            }
        } 

    }
}
