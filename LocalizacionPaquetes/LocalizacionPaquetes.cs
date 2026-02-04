using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using empresaGlobalProj;

namespace LocalizacionPaquetes
{
    public partial class LocalizacionPaquetes : Form
    {

        DButils dbu = new DButils();
        DataTable table;
        DataTable tableFuera;
        string[] parcela = {"-", "-", "-"};
        string codalm = "";
        bool movido = false;
        bool fuera = false;

        DataTable datosparcelas = null;
        DataTable historial = null;

        public LocalizacionPaquetes()
        {
            InitializeComponent();
        }

        private void LocalizacionPaquetes_Load(object sender, EventArgs e)
        {
            this.panelHistorial.Visible = false;
            this.panelPopup.Visible = false;
            this.panelCorrecta.Visible = false;
            table = dbu.obtenerAlmacenes();

            string almacen = "";

            foreach (DataRow row in table.Rows)
            {
                almacen = Convert.ToString(row["razons"]);
                almacen += " #" + Convert.ToString(row["codalm"]);
                comboAlmacen.Items.Add(almacen);
            }

            updateTextoParcela();
            this.usuario.Text = Environment.UserName;

            if (comboAlmacen.Items.Count != 0)
            {
                this.comboAlmacen.SelectedIndex = 0;
            }

            //this.textBox2.PreviewKeyDown += enter();

            this.AcceptButton = this.btnGuardar;
            this.textBox2.Select();
            //this.ResizeEnd += this.LocalizacionPaquetes_ResizeEnd;
            
        }

        private void comboAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCombos();
        }

        
        private void cargarCombos()
        {

            this.comboNave.Items.Clear();
            int index = comboAlmacen.SelectedIndex;

            codalm = table.Rows[index][1].ToString();

            datosparcelas = dbu.obtenerDatosParcelas(codalm);

            List<string> naves = new List<string>();
            naves.Add(datosparcelas.Rows[0][0].ToString());
            comboNave.Items.Add(datosparcelas.Rows[0][0].ToString());

            foreach (DataRow row in datosparcelas.Rows)
            {
                if (!naves.Contains(row["naves"].ToString()))
                {
                    naves.Add(row["naves"].ToString());
                    comboNave.Items.Add(row["naves"].ToString());
                }
            }

            /*if (Convert.ToInt32(datosparcelas.Rows[0][3]) > 0)
            {
                this.comboNave.Items.Add("P");
                tableFuera = dbu.obtenerDatosFuera(codalm);
            }*/

            /*if (carganaves)
            {
                int naves = Convert.ToInt32(datosparcelas.Rows[0][0]);

                for (int i = 1; i <= naves; i++)
                {
                    this.comboNave.Items.Add(i.ToString());

                }

                if (Convert.ToInt32(datosparcelas.Rows[0][3]) > 0)
                {
                    this.comboNave.Items.Add("P");
                    tableFuera = dbu.obtenerDatosFuera(codalm);
                }
            }

            foreach (char c in datosparcelas.Rows[0][1].ToString())
            {
                this.comboFila.Items.Add(c);
            }

            int columnas = Convert.ToInt32(datosparcelas.Rows[0][2]);

            for (int i = 1; i <= columnas; i++)
            {
                this.comboCol.Items.Add(i.ToString());
            }*/

            parcela[0] = "-";
            parcela[1] = "-";
            parcela[2] = "-";
            updateTextoParcela();
        }

        private void updateTextoParcela()
        {
            this.textoParcela.Text = "";

            for (int i = 0; i < 3; i++)
            {
                this.textoParcela.Text += parcela[i];
            }

            if (parcela[2].All(char.IsDigit))
            {
                if (Convert.ToInt32(parcela[2]) > 10 && movido)
                {
                    this.textoParcela.Location = new Point(this.textoParcela.Location.X - 10, this.textoParcela.Location.Y);
                    movido = false;
                }

                if (Convert.ToInt32(parcela[2]) < 10 && !movido)
                {
                    this.textoParcela.Location = new Point(this.textoParcela.Location.X + 10, this.textoParcela.Location.Y);
                    movido = true;
                }
            }


            this.textBox2.Select();
        }

        private void comboNave_SelectedIndexChanged(object sender, EventArgs e)
        {
            parcela[0] = this.comboNave.SelectedItem.ToString();


            /*if (this.comboNave.SelectedItem.ToString() == "P")
            {
                if (!fuera)
                {
                    this.parcela[1] = "-";
                    this.parcela[2] = "-";

                    this.comboFila.Items.Clear();
                    this.comboCol.Items.Clear();
                    string p1;
                    string p2;
                    foreach (DataRow row in tableFuera.Rows)
                    {
                        p1 = Convert.ToString(row["p1"]);
                        p2 = Convert.ToString(row["p2"]);

                        this.comboFila.Items.Add(p1);
                        this.comboCol.Items.Add(p2);
                    }

                    for (int i = 1; i <= Convert.ToInt32(datosparcelas.Rows[0][3]); i++)
                    {
                        this.comboFila.Items.Add(i.ToString());
                        this.comboCol.Items.Add(i.ToString());
                    }
                    fuera = true;
                }
            }
            else
            {*/
                
                this.comboFila.Items.Clear();
                this.comboCol.Items.Clear();

                List<string> secciones = new List<string>();
                List<string> columnas = new List<string>();
                foreach (DataRow row in datosparcelas.Rows)
                {
                    if (!secciones.Contains(row["filas"].ToString()) && row["naves"].ToString() == this.parcela[0])
                    {
                        secciones.Add(row["filas"].ToString());
                        comboFila.Items.Add(row["filas"].ToString());
                    }

                    if (!columnas.Contains(row["columnas"].ToString()) && row["naves"].ToString() == this.parcela[0])
                    {
                        columnas.Add(row["columnas"].ToString());
                        comboCol.Items.Add(row["columnas"].ToString());
                    }
                }

                int index = this.comboFila.Items.IndexOf(this.parcela[1]);
                this.comboFila.SelectedIndex = index;
                this.comboFila.Update();
                if (index == -1)
                {
                    this.parcela[1] = "-";
                }
                else
                {
                    this.parcela[1] = this.comboFila.Text;
                }

                index = this.comboCol.Items.IndexOf(this.parcela[2]);
                this.comboCol.SelectedIndex = index;
                this.comboCol.Update();
                if (index == -1)
                {
                    this.parcela[2] = "-";
                }
                else
                {
                    this.parcela[2] = this.comboCol.Text;
                }
                this.parcela[0] = this.comboNave.Text;
                
                //fuera = false;

            //}
            updateTextoParcela();
        }

        private void comboFila_SelectedIndexChanged(object sender, EventArgs e)
        {
            parcela[1] = this.comboFila.SelectedItem.ToString();

            this.comboCol.Items.Clear();

            List<string> columnas = new List<string>();
            foreach (DataRow row in datosparcelas.Rows)
            {
                if (!columnas.Contains(row["columnas"].ToString()) && row["naves"].ToString() == this.parcela[0] && row["filas"].ToString() == this.parcela[1])
                {
                    columnas.Add(row["columnas"].ToString());
                    comboCol.Items.Add(row["columnas"].ToString());
                }
            }

            int index = this.comboCol.Items.IndexOf(this.parcela[2]);
            this.comboCol.SelectedIndex = index;
            this.comboCol.Update();
            if (index == -1)
            {
                this.parcela[2] = "-";
            }
            else
            {
                this.parcela[2] = this.comboCol.Text;
            }
            this.parcela[1] = this.comboFila.Text;
            /*this.comboCol.Items.Clear();
            string selected = "-";
            if (comboCol.SelectedIndex != -1)
            {
                selected = comboCol.SelectedText;
            }


            foreach (DataRow row in datosparcelas.Rows)
            {
                if (fuera)
                {
                    if (row["p1"].ToString() == this.comboFila.SelectedItem.ToString())
                    {
                        this.comboCol.Items.Add(row["p2"]);
                    }
                }
                else
                {
                    if (row["filas"].ToString() == this.comboFila.SelectedItem.ToString())
                    {
                        this.comboCol.Items.Add(row["columnas"]);
                    }
                }
            }

            if (comboCol.Items.IndexOf(selected) != -1)
            {
                comboCol.SelectedIndex = comboCol.Items.IndexOf(selected);
            }

            parcela[2] = selected;

            parcela[1] = this.comboFila.SelectedItem.ToString();*/
            updateTextoParcela();
        }

        private void comboCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            parcela[2] = this.comboCol.SelectedItem.ToString();
            updateTextoParcela();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string parcelaComp = this.parcela[0] + this.parcela[1] + this.parcela[2];

            if (this.parcela[0] == "-")
            {
                this.panelPopup.Visible = true;
                this.btnPopup.Visible = true;
                this.panelPopup.BackColor = Color.Silver;
                this.etiquetaPopup.Text = "SELECCIONE UNA NAVE";
                this.etiquetaPopup.ForeColor = Color.White;
                this.etiquetaPopup.Update();
                return;
            }

            if (this.parcela[1] == "-")
            {
                this.panelPopup.Visible = true;
                this.btnPopup.Visible = true;
                this.panelPopup.BackColor = Color.Silver;
                this.etiquetaPopup.Text = "SELECCIONE UNA SECCIÓN";
                this.etiquetaPopup.ForeColor = Color.White;
                this.etiquetaPopup.Update();
                return;
            }

            if (this.parcela[2] == "-")
            {
                this.panelPopup.Visible = true;
                this.panelPopup.BackColor = Color.Silver;
                this.etiquetaPopup.Text = "SELECCIONE UN NÚMERO\nDE PARCELA";
                this.etiquetaPopup.ForeColor = Color.White;
                this.etiquetaPopup.Update();
                return;
            }

            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                this.panelPopup.Visible = true;
                this.btnPopup.Visible = true;
                this.panelPopup.BackColor = Color.Silver;
                this.etiquetaPopup.Text = "INSERTE UN PAQUETE";
                this.etiquetaPopup.ForeColor = Color.White;
                this.etiquetaPopup.Update();
                return;
            }

            if (empresaGlobal.empresaID != "60" && !dbu.existePaq(this.textBox2.Text))
            {
                this.panelPopup.Visible = true;
                this.btnPopup.Visible = true;
                this.panelPopup.BackColor = Color.Silver;
                this.etiquetaPopup.Text = "EL PAQUETE INTRODUCIDO\nNO EXISTE. \n\nINTÉNTELO DE NUEVO";
                this.etiquetaPopup.ForeColor = Color.White;
                this.etiquetaPopup.Update();
                return;
            }

            if (dbu.checkMismaParcela(this.textBox2.Text, parcelaComp, this.codalm))
            {
                this.panelPopup.Visible = true;
                this.btnPopup.Visible = true;
                this.panelPopup.BackColor = Color.Silver;
                this.etiquetaPopup.Text = "EL PAQUETE YA ESTÁ\nEN LA PARCELA SELECCIONADA.\n\n SELECCIONE UNA DIFERENTE";
                this.etiquetaPopup.ForeColor = Color.White;
                this.etiquetaPopup.Update();
                this.textBox2.Clear();
                return;
            }

            

            dbu.guardarParcela(this.textBox2.Text, this.codalm, parcela);



            this.panelCorrecta.Visible = true;
            this.label8.Update();

            Thread.Sleep(1500);

            this.panelCorrecta.Visible = false;

            this.textBox2.Clear();
            this.textBox2.Select();
        }

        private void btnCerrarPanel_Click(object sender, EventArgs e)
        {
            this.panelHistorial.Visible = false;
            this.AcceptButton = this.btnGuardar;
            this.textBox2.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            if (historial != null) this.historial.Clear();
            this.parcelaPanel.Text = "- - -";
            this.panelHistorial.Visible = true;
            this.AcceptButton = this.btnBuscarHistorial;
            this.textBox1.Select();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.textBox2.Clear();
            this.textBox2.Select();
        }


        private void btnBuscarHistorial_Click(object sender, EventArgs e)
        {
            if (dbu.existePaq(this.textBox1.Text))
            {
                this.historial = dbu.historialPaquete(this.codalm, this.textBox1.Text);

                if (this.historial.Rows.Count > 0)
                {
                    //this.paqPanel.Text = this.textBox2.Text.Replace("'", "-");
                    this.dataGridView1.Columns.Clear();
                    this.dataGridView1.DataSource = historial;
                    this.dataGridView1.Columns[1].Width = 350;
                    this.dataGridView1.Columns[2].Width = 300;



                    this.dataGridView1.AllowUserToAddRows = false;

                    this.parcelaPanel.Text = "";

                    this.parcelaPanel.Text += historial.Rows[0][0].ToString();

                    foreach (DataGridViewColumn c in dataGridView1.Columns)
                    {
                        c.DefaultCellStyle.Font = new Font("Arial", 25F, GraphicsUnit.Pixel);
                    }


                }
                else
                {
                    this.panelPopup.Visible = true;
                    this.btnPopup.Visible = true;
                    this.panelPopup.BackColor = Color.Silver;
                    this.etiquetaPopup.Text = "EL PAQUETE INTRODUCIDO\nNO TIENE HISTORIAL";
                    this.etiquetaPopup.ForeColor = Color.White;
                    this.etiquetaPopup.Update();
                }
            }
            else
            {
                this.panelPopup.Visible = true;
                this.btnPopup.Visible = true;
                this.panelPopup.BackColor = Color.Silver;
                this.etiquetaPopup.Text = "EL PAQUETE INTRODUCIDO\nNO EXISTE. \n\nINTÉNTELO DE NUEVO";
                this.etiquetaPopup.ForeColor = Color.White;
                this.etiquetaPopup.Update();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox1.Select();
        }

        private void LocalizacionPaquetes_ResizeEnd(object sender, EventArgs e)
        {
            Screen myScreen = Screen.FromControl(this);
            Rectangle area = myScreen.WorkingArea;

            this.Top = (area.Height - this.Height) / 2;
            this.Left = (area.Width - this.Width) / 2;
        }

        private void btnPopup_Click(object sender, EventArgs e)
        {
            this.panelPopup.Visible = false;
            this.textBox2.Select();
        }





    }
}
