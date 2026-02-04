using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace TiemposProyectos
{
    public partial class TiemposProyectos : Form
    {
        DButils dbu = new DButils();
        DataTable tableOrdfabs = new DataTable();
        string categoriaFiltro = "";
        bool taladrado = false;
        List<string> puestosTaladradoPunzonado = new List<string> { "103", "104", "130", "110", "131", "132", "111", "108", "105", "109", "101", "102", "106", "107", "134" };


        DataTable puestosAntiguosOF = new DataTable();



        public TiemposProyectos()
        {
            InitializeComponent();
            this.comboFiltroPuesto.Items.Clear();
            this.comboFiltroCategos.Items.Clear();
            this.comboCategoAuto.Items.Clear();

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            this.textBox1.Select();

            puestosAntiguosOF.Columns.Add("OF", typeof(string));
            puestosAntiguosOF.Columns.Add("CODIGO", typeof(string));
            puestosAntiguosOF.Columns.Add("PUESTO", typeof(string));

            //this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
        }

        private void buscar()
        {
            this.comboFiltroPuesto.Items.Clear();
            this.comboFiltroCategos.Items.Clear();

            if (textBox1.Text != "")
            {

                tableOrdfabs = dbu.obtenerOrdfabs(this.textBox1.Text);


                tableOrdfabs.Columns.Add("NUM_AGUJEROS");

                List<string> listaPuestos = new List<string>();
                List<string> listaCategos = new List<string>();

                dataGridView1.DataSource = tableOrdfabs;

                dataGridView1.Columns["TALADRADO"].Visible = false;
                dataGridView1.Columns["PESO"].Visible = false;
                dataGridView1.Columns["PUESTOANTIGUO"].Visible = false;

                string codigo;
                string FAMILIA;
                string DENOMI;
                string RUTA;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells["NUM_AGUJEROS"].Value = "-";
                    if (!listaPuestos.Contains(row.Cells[6].Value.ToString()))
                    {
                        listaPuestos.Add(row.Cells[6].Value.ToString());
                    }

                    if (!listaCategos.Contains(row.Cells[2].Value.ToString()))
                    {
                        listaCategos.Add(row.Cells[2].Value.ToString());
                    }


                    codigo = row.Cells["CODIGO"].Value.ToString();
                    FAMILIA = row.Cells["FAMILIA"].Value.ToString();
                    DENOMI = row.Cells["N_FAMILIA"].Value.ToString();

                    if (codigo[codigo.Length - 1] == '.')
                    {
                        codigo = codigo.Remove(codigo.Length - 1);
                    }

                    if (codigo == "F3555H")
                    {
                        int test = 0;
                    }

                    string pathCarpeta = @"\\nas01\piezas.oficina";

                    RUTA =  @"\\nas01\piezas.oficina\" + FAMILIA + "_" + DENOMI.Replace(" ", "_") + "\\" + codigo + ".nc";

                    //RUTA = dbu.getRutaFamilia(codigo);
                    //ANGEL GARCIA - 24/06/2024 - Se cambia el modo de indexación de carpetas, ahora se itera sobre los directorios.
                    //                            Con el método antiguo a veces no reconoce la carpeta si se ha cambiado de nombre.
                    //                            No se ve afectado el tiempo de proceso (en exceso)

                    string carpetaFamilia = "";
                    int index_ = 0;
                    foreach (string carpeta in Directory.GetDirectories(@"\\nas01\piezas.oficina"))
                    {
                        carpetaFamilia = carpeta.Substring(pathCarpeta.Length+1);
                        index_ = Math.Max(carpetaFamilia.IndexOf('_'), 0);
                        carpetaFamilia = carpetaFamilia.Substring(0, index_);
                        if (carpetaFamilia == FAMILIA)
                        {
                            RUTA = carpeta + @"\\" + codigo + ".nc";
                            break;
                        }
                    }                

                    row.Cells["NUM_AGUJEROS"].Value = GetNumAgujerosNC(RUTA);
                }
                listaPuestos.Sort();
                listaCategos.Sort();

                comboFiltroPuesto.Items.Add("TODOS");
                comboFiltroCategos.Items.Add("TODAS");
                comboCategoAuto.Items.Add("TODAS");

                foreach (string filtro in listaPuestos)
                {
                    this.comboFiltroPuesto.Items.Add(filtro);
                }

                foreach (string filtro2 in listaCategos)
                {
                    this.comboFiltroCategos.Items.Add(filtro2);
                    this.comboCategoAuto.Items.Add(filtro2);
                }

                this.comboFiltroCategos.SelectedIndex = 0;
                this.comboFiltroPuesto.SelectedIndex = 0;
                this.comboCategoAuto.SelectedIndex = 0;

                tableOrdfabs.AcceptChanges();

            }
            else
            {
                MessageBox.Show("Introduce un código de proyecto");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarCambios();
        }

        private void guardarCambios()
        {
            tableOrdfabs.DefaultView.RowFilter = string.Empty;

            

            int afectadas = 0;

            if (((DataTable)dataGridView1.DataSource).GetChanges(DataRowState.Modified) != null)
            {

                DataRowCollection modifiedRows = ((DataTable)dataGridView1.DataSource).GetChanges(DataRowState.Modified).Rows;

                this.progressBar1.Maximum = modifiedRows.Count;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Value = 0;

                this.label5.Visible = false;
                this.comboCategoAuto.Visible = false;
                this.button1.Visible = false;
                this.btnIniciarAuto.Visible = false;
                this.panel1.Visible = true;
                this.label6.Visible = true;
                Refresh();

                foreach (DataRow row in modifiedRows)
                {
                    afectadas += dbu.actualizarOrdfabs(row["ORDFAB"].ToString(), row["CODIGO"].ToString(), row["DENOMINACION"].ToString(), row["DESCRIPCION"].ToString(),
                        row["CORREL"].ToString(), row["PUESTOANTIGUO"].ToString(), row["PREPAR"].ToString(), row["FABRIC"].ToString(), row["DENOMI"].ToString(), row["PUESTO"].ToString());

                    dbu.actualizarRutas(row["CODIGO"].ToString(), row["CORREL"].ToString(), row["PUESTOANTIGUO"].ToString(), row["PREPAR"].ToString(), row["FABRIC"].ToString(), row["PUESTO"].ToString());
                    dbu.updateBonoL(row["FABRIC"].ToString(), row["PREPAR"].ToString(), row["CODIGO"].ToString(), row["ORDFAB"].ToString(), row["PUESTOANTIGUO"].ToString(), row["PUESTO"].ToString());

                    progressBar1.Value++;
                }

                MessageBox.Show("Se han guardado " + afectadas + " cambios", "CAMBIOS GUARDADOS", MessageBoxButtons.OK);
                tableOrdfabs.AcceptChanges();

                this.label5.Visible = true;
                this.comboCategoAuto.Visible = true;
                this.button1.Visible = true;
                this.btnIniciarAuto.Visible = true;
                this.panel1.Visible = false;
                this.label6.Visible = false;
            }
            else
            {
                MessageBox.Show("No hay cambios por guardar"); 
            }

            /*foreach (DataRow row in tableOrdfabs.Rows)
            {
                dbu.actualizarBonos(row["ORDFAB"].ToString(), row["CORREL"].ToString(), row["PUESTO"].ToString(), row["PREPAR"].ToString(), row["FABRIC"].ToString());
            }*/
        }

        private void filtrar()
        {
            string filtro = string.Empty;

            string puesto = " ";
            string catego = " ";

            if (tableOrdfabs.Rows.Count != 0)
            {

                if (this.comboFiltroPuesto.SelectedIndex != 0 && this.comboFiltroPuesto.SelectedIndex != -1)
                {
                    puesto = this.comboFiltroPuesto.SelectedItem.ToString();
                    filtro += "PUESTO LIKE '" + puesto + "'";
                }

                if (this.comboFiltroCategos.SelectedIndex != 0 && this.comboFiltroCategos.SelectedIndex != -1)
                {
                    catego = this.comboFiltroCategos.SelectedItem.ToString();

                    if (puesto != " ") filtro += " and ";

                    filtro += "CATEGORIA LIKE '" + catego + "' ";
                }


                if (checkBox1.Checked)
                {
                    if (puesto != " " || catego != " ") filtro += " and ";
                    filtro += "(PREPAR = 0 OR FABRIC = 0)";
                }

                tableOrdfabs.DefaultView.RowFilter = filtro;
            }

        }

        private void comboFiltroPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboFiltroCategos_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void btnCerrarPanel_Click(object sender, EventArgs e)
        {
            this.panelTiempos.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (dataGridView1.SelectedCells[0].ColumnIndex == 8 && dataGridView1.SelectedCells[0].RowIndex != -1)
            {
                this.panelTiempos.Visible = true;
            }*/
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            this.panel1.Visible = true;
            taladrado = false;
            
        }

        private int GetNumAgujerosNC(string pathFile)
        {
            int numAgujeros = 0;
            string linea;
            bool procesarAgujeros = false;

            if (File.Exists(pathFile))
            {
                using (StreamReader lector = File.OpenText(pathFile))
                {
                    try
                    {
                        do
                        {
                            linea = lector.ReadLine();
                            if (linea != null)
                            {
                                linea = linea.Trim();
                                string[] items;
                                char[] charSeparators = new char[] { ' ' };
                                items = linea.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                                if (items.Count() > 0)
                                {
                                    if (items[0] == "BO")//Ya estamos en la parte de los agujeros.                                
                                        procesarAgujeros = true;
                                    if ((items.Count() == 1) && (items[0] != "BO"))//Terminamos de procesar los agujeros.
                                        procesarAgujeros = false;

                                    if ((procesarAgujeros) && (items.Count() > 1))
                                    {
                                        //De momento solo los contamos.
                                        numAgujeros++;
                                    }
                                }
                            }
                        } while (linea != null);
                    }
                    catch (Exception e)
                    {
                        lector.Close();
                        MessageBox.Show(e.Message);
                    }

                }
            }
            else
            {
                numAgujeros = 0;
            }
            return numAgujeros;
        }

        private void btnChapas_Click(object sender, EventArgs e)
        {
            categoriaFiltro = "Chapa";
            autoCorreccion();
            this.panel1.Visible = false;
        }

        private void btnAngulares_Click(object sender, EventArgs e)
        {
            categoriaFiltro = this.comboCategoAuto.Text;
            autoCorreccion();
            this.panel1.Visible = false;
        }

        private void autoCorreccion()
        {

            DataTable valores = new DataTable();

            this.progressBar1.Maximum = dataGridView1.Rows.Count;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Value = 0;



            string test = "";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                this.progressBar1.Value++;
                test = row.Cells["PUESTO"].Value.ToString();
                //Refresh();
                if ((row.Cells[2].Value.ToString() == categoriaFiltro || categoriaFiltro == "TODAS") &&
                    ((taladrado && puestosTaladradoPunzonado.Contains(row.Cells["PUESTO"].Value.ToString())) || (!taladrado && !puestosTaladradoPunzonado.Contains(row.Cells["PUESTO"].Value.ToString()))))
                {


                    /*string codigo = row.Cells["CODIGO"].Value.ToString();
                    string FAMILIA = row.Cells["FAMILIA"].Value.ToString();
                    string DENOMI = row.Cells["N_FAMILIA"].Value.ToString();

                    if (codigo[codigo.Length-1] == '.')
                    {
                        codigo = codigo.Remove(codigo.Length - 1);
                    }

                    string RUTA = "O:\\" + FAMILIA + "_" + DENOMI.Replace(" ", "_") + "\\" + codigo + ".nc";*/

                    int numAgujeros = Convert.ToInt32(row.Cells["NUM_AGUJEROS"].Value.ToString());


                    dbu.updateAgujeros(numAgujeros, row.Cells["CODIGO"].Value.ToString());

                    if (row.Cells["PUESTO"].Value.ToString() == "111" || row.Cells["PUESTO"].Value.ToString() == "105" || row.Cells["PUESTO"].Value.ToString() == "109")
                    {
                        numAgujeros++;
                    }

                    valores = dbu.getValoresFormula(row.Cells[6].Value.ToString());

                    //T_FAB
                    if ((Convert.ToDouble(row.Cells[8].Value.ToString()) == 0 && valores.Rows.Count > 0) || taladrado)
                    {
                        if (valores.Rows[0][2].ToString() == "SI")
                        {
                            if (row.Cells["TALADRADO"].Value.ToString() == "S")
                            {
                                //row.Cells[8].Value = Math.Round((numAgujeros * Convert.ToDouble(valores.Rows[0][0].ToString())), 4);
                                //row.Cells[8].Value = Math.Round((numAgujeros * Convert.ToDouble(0.006)), 4);

                                if (puestosTaladradoPunzonado.Contains(row.Cells[6].Value.ToString()))
                                {
                                    puestosAntiguosOF.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[6].Value.ToString());

                                    if (row.Cells["CATEGORIA"].Value.ToString() == "Chapa" )
                                    {
                                        if (row.Cells[6].Value.ToString() != "111")
                                        {
                                            row.Cells[6].Value = "111";
                                        }
                                    }
                                    else 
                                    {
                                        if (!puestosTaladradoPunzonado.Contains(row.Cells[6].Value.ToString()) || row.Cells[6].Value.ToString() == "111")
                                        {

                                            row.Cells[6].Value = "104"; 
                                        }                                       
                                    }

                                    valores = dbu.getValoresFormula(row.Cells[6].Value.ToString());
                                    row.Cells[8].Value = Math.Round((numAgujeros * Convert.ToDouble(valores.Rows[0][0].ToString())), 4);

                                    row.Cells[6].Style.BackColor = Color.Lime;
                                }
                            }
                            else
                            {
                                //PUNZONADO
                                double tiempoPunz = Math.Round((numAgujeros * Convert.ToDouble(0.001283)), 4);
                                if (tiempoPunz < 0) tiempoPunz = 0;

                                row.Cells[8].Value = tiempoPunz;
                            }
                        }
                        else
                        {
                            row.Cells[8].Value = Math.Round(Convert.ToDouble(valores.Rows[0][0].ToString()), 4);
                        }
                        row.Cells[8].Style.BackColor = Color.Lime;
                    }

                    //T_PREP
                    if ((Convert.ToDouble(row.Cells[7].Value.ToString()) == 0 && valores.Rows.Count > 0) || taladrado)
                    {
                        if (Convert.ToDouble(row.Cells[7].Value.ToString()) == 0)
                        {

                            if (row.Cells["TALADRADO"].Value.ToString() == "S")
                            {
                                row.Cells[7].Value = Math.Round(Convert.ToDouble(valores.Rows[0][1].ToString()), 4);
                            }
                            else
                            {
                                if (Convert.ToDecimal(row.Cells["PESO"].Value.ToString()) <= 25)
                                {
                                    row.Cells[7].Value = Math.Round(0.0595, 4);
                                }
                                else
                                {
                                    row.Cells[7].Value = Math.Round(0.112, 4);
                                }
                            }

                            row.Cells[7].Style.BackColor = Color.Lime;
                        }


                    }

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void btnTaladrado_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            this.panel1.Visible = true;
            taladrado = true;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void TiemposProyectos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tableOrdfabs.Rows.Count > 0 && ((DataTable)dataGridView1.DataSource).GetChanges(DataRowState.Modified) != null)
            {
                DialogResult dialogResult = MessageBox.Show("Hay cambios sin guardar, ¿Salir de todas formas?", "SALIR", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //guardarCambios();
                }
                else if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }

            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buscar();
            }
        }

        

    }
}
