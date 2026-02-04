using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Globalization;


namespace GestorTurnosTaller
{
    public partial class GestorTurnosTaller : Form
    {

        DButils dbu = new DButils();
        System.Data.DataTable lista = null;
        System.Data.DataTable listaMult = null;
        DataView dv;
        DataView dv2; 
        System.Data.DataTable turnos = null;
        List <string> listaInsertados = new List<string>();
        int columnas;
        int año = DateTime.Now.Year;

        int rowMouse = 0;
        int colMouse = 0;

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        System.Data.DataTable tableTurnosAvanz = null;


        List<string> puestos = new List<string>();


        public GestorTurnosTaller()
        {
            InitializeComponent();
        }

        

        private void GestionTurnosTaller_Load(object sender, EventArgs e)
        {

            cargarTabla(año);
        }


        private void cargarTabla(int year)
        {

            this.tbAnio.Text = año.ToString();
            this.comboCentro.Items.Clear();
            this.comboPuesto.Items.Clear();
            this.comboPuestoSeleccion.Items.Clear();

            lista = dbu.obtenerPersonas(this.tbAnio.Text);

            List<string> ubic = new List<string>();

            ubic.Add("TODOS");
            

            foreach (DataRow row in lista.Rows)
            {
                if (!ubic.Contains(row["CENTRO"].ToString()) && row["CENTRO"].ToString() != null && row["CENTRO"].ToString() != "" && row["CENTRO"].ToString() != " ")
                {
                    ubic.Add(row["CENTRO"].ToString());
                }

                if (!puestos.Contains(row["PUESTO"].ToString()))
                {
                    puestos.Add(row["PUESTO"].ToString());
                }


            }

            foreach (string centro in ubic)
            {
                comboCentro.Items.Add(centro);
            }

            
            //var puestosSort = puestos.OrderBy(o => o).ToArray();

            //puestos.Sort();

            /*foreach (string puesto in puestos)
            {
                comboPuesto.Items.Add(puesto);
            }*/
            //comboPuesto.Items.AddRange(puestosSort);


            comboCentro.SelectedIndex = 0;



            DateTime begin = new DateTime(year, 01, 01);
            DateTime end = new DateTime(year, 12, 31);
            int dia = 0;
            int mes = 1;
            string nombremes = "";
            columnas = this.lista.Columns.Count;
            int colcount = columnas;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumns(); 
            this.comboTurno2.SelectedIndex = 0;
            this.comboTurno3.SelectedIndex = 0;


            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                dia = date.Day;

                mes = date.Month;

                switch (mes)
                {
                    case 1:
                        nombremes = "ENE";
                        break;
                    case 2:
                        nombremes = "FEB";
                        break;
                    case 3:
                        nombremes = "MAR";
                        break;
                    case 4:
                        nombremes = "ABR";
                        break;
                    case 5:
                        nombremes = "MAY";
                        break;
                    case 6:
                        nombremes = "JUN";
                        break;
                    case 7:
                        nombremes = "JUL";
                        break;
                    case 8:
                        nombremes = "AGO";
                        break;
                    case 9:
                        nombremes = "SEP";
                        break;
                    case 10:
                        nombremes = "OCT";
                        break;
                    case 11:
                        nombremes = "NOV";
                        break;
                    case 12:
                        nombremes = "DIC";
                        break;

                }

                lista.Columns.Add(nombremes + " " + dia.ToString(), typeof(string));


                colcount++;



                /*if (mes != date.Month)
                {
                          
                }
                mes = date.Month;*/

            }

            lista.PrimaryKey = new DataColumn[] { lista.Columns["NUM"] };
            this.dataGridView1.DataSource = lista;

            for (int i = 0; i < columnas; i++)
            {
                dataGridView1.Columns[i].Frozen = true;
            }

            this.dataGridView1.MouseClick += new MouseEventHandler(dataGridView1_MouseClick);
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(datagridview_CellFormatting);

            turnos = dbu.obtenerTurnosDias(year.ToString());
            int index = 0;


            foreach (DataRow row in turnos.Rows)
            {
                index = lista.Rows.IndexOf(lista.Rows.Find(row["numPersonal"]));
                if (index >= 0)
                {
                    string test = ((Convert.ToDateTime(row["DIA"].ToString()).Date.DayOfYear) + columnas - 1).ToString();
                    dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["DIA"].ToString()).Date.DayOfYear) + columnas - 1].Value = row["TURNO"].ToString();
                }
            }

            puestos.Sort();

            comboPuesto.Items.Add("TODOS");
            foreach (string puesto in puestos)
            {
                comboPuesto.Items.Add(puesto);
                comboPuestoSeleccion.Items.Add(puesto);
            }
            
            this.dataGridView1.Columns["NOMB_OCULTO"].Visible = false;
            this.dataGridView1.Columns["AP_OCULTOS"].Visible = false;

            comboPuesto.SelectedIndex = 0;

        }

        private void btnMultiple_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = true;
            listaMult = dbu.obtenerPersonas(this.tbAnio.Text);
            this.dataGridView2.DataSource = listaMult;
            this.dataGridView3.Rows.Clear();

            dataGridView2.Columns["NOMB_OCULTO"].Visible = false;
            dataGridView2.Columns["AP_OCULTOS"].Visible = false;


            if (this.dataGridView3.Columns.Count == 0)
            {
                foreach (DataGridViewColumn dgvc in dataGridView2.Columns)
                {
                    dataGridView3.Columns.Add(dgvc.Clone() as DataGridViewColumn);
                }
            }


            dataGridView3.Columns["NOMB_OCULTO"].Visible = false;
            dataGridView3.Columns["AP_OCULTOS"].Visible = false;

            //btnGuardar.Visible = true;
            btnLimpiar.Visible = true;
            btnSeleccion.Visible = true;
            textBox1.Clear();
            comboRangos.SelectedIndex = 0;

            filtrar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;

            btnGuardar.Visible = false;
            btnLimpiar.Visible = false;
            btnSeleccion.Visible = false;
            textBox1.Clear();
            this.listaInsertados.Clear();

        }

        private void btnFlecha_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView2.SelectedRows)
            {
                if (!listaInsertados.Contains(row.Cells[0].Value.ToString()))
                {
                    DataGridViewRow rowCopia = (DataGridViewRow)row.Clone();
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        rowCopia.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dataGridView3.Rows.Add(rowCopia);
                    listaInsertados.Add(row.Cells[0].Value.ToString());
                }
            }

            this.dataGridView3.Sort(this.dataGridView3.Columns["NUM"], ListSortDirection.Ascending);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DateTime ini = dateTimePickerDesde.Value;
            DateTime fin = dateTimePickerHasta.Value;
            int index = 0;

            string numPersonal = "";

            while (fin.Date >= ini.Date)
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    numPersonal = row.Cells[0].Value.ToString();
                    dbu.actualizarTurnos(numPersonal, comboRangos.Text, ini, false);
                    index = lista.Rows.IndexOf(lista.Rows.Find(row.Cells[0].Value.ToString()));
                    dataGridView1.Rows[index].Cells[(ini.Date.DayOfYear) + columnas - 1].Value = null;
                }
                ini = ini.AddDays(1);
            }

            //DGVClear();
            //cargarTabla();
            this.listaInsertados.Clear();
            MessageBox.Show("Turnos guardados correctamente", "ATENCIÓN", MessageBoxButtons.OK);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.dataGridView3.Rows.Clear();
            this.listaInsertados.Clear();
        }

        private void btnSeleccion_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView3.SelectedRows)
            {
                this.dataGridView3.Rows.Remove(row);
                this.listaInsertados.Remove(row.Cells[0].Value.ToString());
            }
        }

        private void comboRangos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            
        }

        private void btnIniciarAsig_Click_1(object sender, EventArgs e)
        {
            DateTime ini = dateTimePickerDesde.Value;
            DateTime fin = dateTimePickerHasta.Value;
            int contador = 0;
            int contadorDescanso = 0;
            int index = 0;



            int contadorCombo = 1;

            int indexPuesto = this.comboPuesto.SelectedIndex;
            int indexCentro = this.comboCentro.SelectedIndex;

            this.textBox1.Text = "";
            this.comboCentro.SelectedIndex = 0;
            this.comboPuesto.SelectedIndex = 0;

            string turno = "";

            string comboTurno2 = this.comboTurno2.Text;

            bool change = false;

            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                turno = this.comboRangos.Text;
                contador = Convert.ToInt32(Math.Round(numericUpDown2.Value, 0));
                contadorDescanso = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
                while (fin.Date >= ini.Date)
                {
                    if (contador != 0)
                    {
                        

                        index = lista.Rows.IndexOf(lista.Rows.Find(row.Cells[0].Value.ToString()));

                        if (turno != "NOCHE")
                        {
                            dataGridView1.Rows[index].Cells[(ini.Date.DayOfYear) + columnas - 1].Value = turno;
                            dbu.actualizarTurnos(row.Cells[0].Value.ToString(), turno, ini, false);
                        }
                        else
                        {
                            dataGridView1.Rows[index].Cells[(ini.Date.DayOfYear - 1) + columnas - 1].Value = turno;
                            dbu.actualizarTurnos(row.Cells[0].Value.ToString(), turno, ini.AddDays(-1), false);
                        }

                        contador--;
                        contadorDescanso = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
                    }
                    else
                    {
                        contadorDescanso--;
                        dbu.actualizarTurnos(row.Cells[0].Value.ToString(), turno, ini, true);
                        index = lista.Rows.IndexOf(lista.Rows.Find(row.Cells[0].Value.ToString()));
                        dataGridView1.Rows[index].Cells[(ini.Date.DayOfYear) + columnas - 1].Value = null;
                        if (contadorDescanso == 0)
                        {
                            if (this.checkBox1.Checked)
                            {
                                while (change == false)
                                {
                                    switch (contadorCombo)
                                    {
                                        case 1:
                                            turno = this.comboTurno2.Text;
                                            contadorCombo = 2;
                                            break;
                                        case 2:
                                            turno = this.comboTurno3.Text;
                                            contadorCombo = 3;
                                            break;

                                        case 3:
                                            turno = this.comboRangos.Text;
                                            contadorCombo = 1;
                                            break;
                                    }

                                    /*if (turno == this.comboRangos.Text && !change) { turno = this.comboTurno2.Text; change = true; }
                                    if (turno == this.comboTurno2.Text && !change) { turno = this.comboTurno3.Text; change = true; }
                                    if (turno == this.comboTurno3.Text && !change) { turno = this.comboRangos.Text; change = true; }*/
                                    change = true;
                                }
                                change = false;

                                /*switch (turno)
                                {
                                    case comboTurno2:
                                        {
                                            turno = "TARDE";
                                            break;
                                        }
                                    case "TARDE":
                                        turno = "NOCHE";
                                        break;
                                    case "NOCHE":
                                        turno = "MAÑANA";
                                        break;
                                }*/
                            }

                            contador = Convert.ToInt32(Math.Round(numericUpDown2.Value, 0));
                        }
                    }

                    ini = ini.AddDays(1);
                }
                ini = dateTimePickerDesde.Value;
            }

            this.panel1.Visible = false;
            this.panel2.Visible = false;
            btnGuardar.Visible = false;
            btnLimpiar.Visible = false;
            btnSeleccion.Visible = false;


            this.comboPuesto.SelectedIndex = indexPuesto;
            this.comboCentro.SelectedIndex = indexCentro;

            //DGVClear();
            //cargarTabla();
            this.dataGridView1.Refresh();
            this.listaInsertados.Clear();
            Cursor.Current = DefaultCursor;

            MessageBox.Show("Turnos guardados correctamente", "ATENCIÓN", MessageBoxButtons.OK);
        }

        private void datagridview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

                e.CellStyle.ForeColor = Color.Black;
                switch (e.Value.ToString())
                {

                    case "MAÑANA":
                        e.CellStyle.BackColor = Color.Lime;                        
                        break;
                    case "TARDE":
                        e.CellStyle.BackColor = Color.Yellow;
                        break;
                    case "NOCHE":
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.Navy;
                        break;
                    default:
                        if ((e.ColumnIndex >= columnas) &&
                            ((numADia(e.ColumnIndex - (columnas - 1), año).DayOfWeek == DayOfWeek.Saturday)
                            || (numADia(e.ColumnIndex - (columnas - 1), año).DayOfWeek == DayOfWeek.Sunday)))
                        {
                            e.CellStyle.BackColor = Color.Gray;
                        }
                        else
                        {
                            e.CellStyle.BackColor = Color.White;
                        }
                        break;
                }

                if (e.ColumnIndex >= columnas) dataGridView1.Columns[e.ColumnIndex].Width = 65;
        }

        private void btnXPanel2_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
        }

        private void btnAuto_Click_1(object sender, EventArgs e)
        {
            this.panel2.Visible = true;
            this.label8.Text = comboRangos.Text;
            this.checkBox1.Checked = false;

            switch (comboRangos.Text)
            {

                case "MAÑANA":
                    this.label8.ForeColor = Color.Lime;
                    break;
                case "TARDE":
                    this.label8.ForeColor = Color.Yellow;
                    break;
                case "NOCHE":
                    this.label8.ForeColor = Color.Navy;
                    break;
                default:
                    this.label8.ForeColor = Color.White;
                    break;
            }

        }

        private void DGVClear()
        {
            /*this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.DataSource = lista;
            this.dataGridView1.Refresh();*/

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = columnas; i <= dataGridView1.Columns.Count-1; i++)
                {
                    row.Cells[i].Value = null;
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int colMouse = this.dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            int rowMouse = this.dataGridView1.HitTest(e.X, e.Y).RowIndex;

            if (e.Button == MouseButtons.Right)
            {
                if (colMouse >= columnas && rowMouse <= dataGridView1.Rows.Count - 1 && rowMouse >= 0)
                {
                    ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();



                    menu.Items.Add("MAÑANA").Name = "MANANA";
                    menu.Items.Add("TARDE").Name = "TARDE";
                    menu.Items.Add("NOCHE").Name = "NOCHE";

                    if (this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString() != "") menu.Items.Add("Anular asignación").Name = "Anular";


                    menu.Show(dataGridView1, new System.Drawing.Point(e.X, e.Y));

                    if (!dataGridView1.SelectedCells.Contains(this.dataGridView1.Rows[rowMouse].Cells[colMouse]))
                    {
                        dataGridView1.ClearSelection();
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Selected = true;
                    }


                    menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
                }
            }
        }

        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string turno = "";
            bool borrar = false;
            DateTime dia;
            string numPersonal = "";
            switch (e.ClickedItem.Name.ToString())
            {
                case "MANANA":
                    turno = "MAÑANA";
                    break;

                case "TARDE":
                    turno = "TARDE";
                    break;
                case "NOCHE":
                    turno = "NOCHE";
                    break;
                case "Anular":
                    turno = "";
                    borrar = true;
                    break;

            }

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                Cursor.Current = Cursors.WaitCursor;
                dia = numADia(cell.ColumnIndex - (columnas - 1), año);
                numPersonal = dataGridView1.Rows[cell.RowIndex].Cells[0].Value.ToString();
                dbu.actualizarTurnos(numPersonal, turno, dia, borrar);
                cell.Value = turno;
                Cursor.Current = Cursors.Default;
            }
            dataGridView1.Refresh();
        }

        public DateTime numADia(int numero, int año)
        {
            DateTime dia = new DateTime(año, 01, 01, 23, 59, 59);

            for (int i = 1; i < numero; i++)
            {
                dia = dia.AddDays(1);
            }

            return dia;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.label10.Visible = true;
                this.label11.Visible = true;
                this.comboTurno2.Visible = true;
                this.comboTurno3.Visible = true;
            }
            else
            {
                this.label10.Visible = false;
                this.label11.Visible = false;
                this.comboTurno2.Visible = false;
                this.comboTurno3.Visible = false;
            }
        }

        private void tbAnio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                año = Convert.ToInt32(tbAnio.Text);
                cargarTabla(año);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void comboCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void comboPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        public void filtrar()
        {
            Cursor.Current = Cursors.WaitCursor;
            string puesto = "";
            string centro = "";


            if (comboPuesto.Text != "TODOS")
            {
                puesto = comboPuesto.Text;
            }

            if (comboCentro.Text != "TODOS")
            {
                centro = comboCentro.Text;
            }

            string busq = this.textBox1.Text.ToString();
            string filtro = "(NOMBRE LIKE '%" + this.textBox1.Text + "%' OR Convert([NUM], System.String) LIKE '%"
                + this.textBox1.Text + "%') AND (CENTRO LIKE '%" + centro + "%'";

            if (centro == "") filtro += " OR CENTRO IS NULL ";

            filtro += ") AND (PUESTO LIKE '%" + puesto + "%' ";

            if (puesto == "") filtro += " OR PUESTO IS NULL ";

            filtro += ")";

            lista.DefaultView.RowFilter = filtro;
            dv = lista.DefaultView;

            if (panel1.Visible == true)
            {
                listaMult.DefaultView.RowFilter = filtro;
                dv2 = listaMult.DefaultView;
            }

            Cursor.Current = DefaultCursor;
        }

        private void tbAnio_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAvanz_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = true;
            this.comboTurnoAvanz.SelectedIndex = 0;

            if (this.dgvTurnosAvanz.DataSource == null)
            {
                tableTurnosAvanz = new System.Data.DataTable();

                tableTurnosAvanz.Columns.Add("ORDEN");
                tableTurnosAvanz.Columns.Add("TURNO");

                DataRow newrow = tableTurnosAvanz.NewRow();

                newrow[0] = "1";
                newrow[1] = label8.Text;

                tableTurnosAvanz.Rows.Add(newrow);

                this.dgvTurnosAvanz.DataSource = tableTurnosAvanz;
            }



        }

        private void btnAnadirTurno_Click(object sender, EventArgs e)
        {

            DataRow newrow = tableTurnosAvanz.NewRow();

            newrow[0] = tableTurnosAvanz.Rows.Count+1;
            newrow[1] = comboTurnoAvanz.Text;

            tableTurnosAvanz.Rows.Add(newrow);
        }

        private void btnCerrarPanelAvanz_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = false;
        }

        private void btnCerrarPanelPuestos_Click(object sender, EventArgs e)
        {
            this.panel4.Visible = false;
        }

        private void btnQuitarTurno_Click(object sender, EventArgs e)
        {
            this.tableTurnosAvanz.Rows.RemoveAt(this.dgvTurnosAvanz.SelectedRows[0].Index);

            int counter = 1;
            foreach (DataRow row in tableTurnosAvanz.Rows)
            {
                row[0] = counter;
                counter++;
            }
        }

        private void btnInicAvanz_Click(object sender, EventArgs e)
        {
            DateTime ini = dateTimePickerDesde.Value;
            DateTime fin = dateTimePickerHasta.Value;
            int contador = 0;
            int contadorDescanso = 0;
            int index = 0;

            int indexPuesto = this.comboPuesto.SelectedIndex;
            int indexCentro = this.comboCentro.SelectedIndex;

            this.textBox1.Text = "";
            this.comboPuesto.SelectedIndex = 0;
            this.comboCentro.SelectedIndex = 0;

            string turno = "";

            string comboTurno2 = this.comboTurno2.Text;

            bool change = false;
            int indexTableTurnos = 0;

            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                indexTableTurnos = 0;
                turno = this.comboRangos.Text;
                contador = Convert.ToInt32(Math.Round(numericUpDown2.Value, 0));
                contadorDescanso = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
                while (fin.Date >= ini.Date)
                {
                    if (contador != 0)
                    {

                        index = lista.Rows.IndexOf(lista.Rows.Find(row.Cells[0].Value.ToString()));

                        if (turno != "NOCHE")
                        {
                            dataGridView1.Rows[index].Cells[(ini.Date.DayOfYear) + columnas - 1].Value = turno;
                            dbu.actualizarTurnos(row.Cells[0].Value.ToString(), turno, ini, false);
                        }
                        else
                        {
                            dataGridView1.Rows[index].Cells[(ini.Date.DayOfYear - 1) + columnas - 1].Value = turno;
                            dbu.actualizarTurnos(row.Cells[0].Value.ToString(), turno, ini.AddDays(-1), false);
                        }

                        contador--;
                        contadorDescanso = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
                    }
                    else
                    {
                        contadorDescanso--;
                        dbu.actualizarTurnos(row.Cells[0].Value.ToString(), turno, ini, true);
                        index = lista.Rows.IndexOf(lista.Rows.Find(row.Cells[0].Value.ToString()));
                        dataGridView1.Rows[index].Cells[(ini.Date.DayOfYear) + columnas - 1].Value = null;
                        if (contadorDescanso == 0)
                        {
                            
                            while (change == false)
                            {
                                indexTableTurnos++;
                                if (indexTableTurnos + 1 > tableTurnosAvanz.Rows.Count)
                                {
                                    indexTableTurnos = 0;
                                }
                                turno = tableTurnosAvanz.Rows[indexTableTurnos][1].ToString();
                                change = true;
                            }
                            change = false;

                            /*switch (turno)
                            {
                                case comboTurno2:
                                    {
                                        turno = "TARDE";
                                        break;
                                    }
                                case "TARDE":
                                    turno = "NOCHE";
                                    break;
                                case "NOCHE":
                                    turno = "MAÑANA";
                                    break;
                            }*/

                            contador = Convert.ToInt32(Math.Round(numericUpDown2.Value, 0));
                        }
                    }

                    ini = ini.AddDays(1);
                }
                ini = dateTimePickerDesde.Value;
            }

            this.panel1.Visible = false;
            this.panel2.Visible = false;
            btnGuardar.Visible = false;
            btnLimpiar.Visible = false;
            btnSeleccion.Visible = false;



            this.comboPuesto.SelectedIndex = indexPuesto;
            this.comboCentro.SelectedIndex = indexCentro;

            //DGVClear();
            //cargarTabla();
            this.dataGridView1.Refresh();
            this.listaInsertados.Clear();
            Cursor.Current = DefaultCursor;

            MessageBox.Show("Turnos guardados correctamente", "ATENCIÓN", MessageBoxButtons.OK);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            this.panelExcel.Visible = true;
            Calendar cal = new CultureInfo("es-ES").Calendar;
            this.numericUpDown3.Value = cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        private void btnIniciarExcel_Click(object sender, EventArgs e)
        {

            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;


            try
            {
                List<string> anadidos = new List<string>();

                string nombreOp = "";

                int contadorM = 4;
                int contadorT = 4;
                int contadorN = 4;

                int contadorCart = 4;

                int columna = 0;

                int contadorActual = 0;

                int contadorPuesto = 0;

                int diferenciaUbic = 1;

                bool centroSoldIteracion1 = false;

                bool puestoEscrito = false;
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                string coord1 = "";
                string coord2 = "";

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[3, 3] = "MAÑANA";
                oSheet.Cells[3, 4] = "TARDE";
                oSheet.Cells[3, 5] = "NOCHE";
                //oSheet.Cells[3, 2] = "Salary";
                oSheet.Cells[3, 7] = "CARTERA DE PERSONAL";
                oSheet.get_Range("G3", "G3").Font.Bold = true;
                oSheet.get_Range("G3", "G3").Font.Size = 26;
                oSheet.get_Range("G3", "G3").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("G3", "G3").HorizontalAlignment =
                    Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                oSheet.get_Range("B2", "E2").Merge();
                oSheet.get_Range("B2", "E2").Font.Bold = true;
                oSheet.get_Range("B2", "E2").Font.Size = 26;
                oSheet.get_Range("B2", "E2").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("B2", "E2").HorizontalAlignment =
                    Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[2, 2] = "SEMANA " + numericUpDown3.Value + " " + tbAnio.Text;// +" CASAR";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.get_Range("C3", "E3").Font.Bold = true;
                oSheet.get_Range("C3", "E3").Font.Size = 22;
                oSheet.get_Range("C3", "E3").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("C3", "E3").HorizontalAlignment =
                    Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.get_Range("C3", "E3").Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                oSheet.get_Range("C3", "E3").Borders.Weight = 2d;


                int diasemana = (7 * (Convert.ToInt32(numericUpDown3.Value) - 1)) + columnas;
                diasemana += 2; //ANGEL GARCIA 26/03/2024 se aumenta un par de días la referencia de turnos para que tenga en cuenta los miércoles en lugar de los lunes

                foreach (string puesto in puestos)
                {
                    
                    if (puesto != "Oficinas"){
                        diferenciaUbic = 1;

                        while (diferenciaUbic > 0)
                        {
                            contadorPuesto = contadorM;
                            puestoEscrito = false;

                            if (puesto.ToUpper() == "SOLDADURA" && /*diferenciaUbic != 2 &&*/ !centroSoldIteracion1)
                            {
                                diferenciaUbic = 2;
                                centroSoldIteracion1 = true;
                            }
                            else
                            {
                                diferenciaUbic--;
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                contadorActual = 0;
                                columna = 0;
                                nombreOp = row.Cells["NOMB_OCULTO"].Value.ToString() + " " + row.Cells["AP_OCULTOS"].Value.ToString();

                                if (((row.Cells["PUESTO"].Value.ToString() == puesto && puesto.ToUpper() != "SOLDADURA" && diferenciaUbic == 0) ||
                                    (puesto.ToUpper() == "SOLDADURA" && row.Cells["PUESTO"].Value.ToString() == puesto && diferenciaUbic == 2 && row.Cells["CENTRO"].Value.ToString().ToUpper().Contains("CASAR")) ||
                                    (puesto.ToUpper() == "SOLDADURA" && row.Cells["PUESTO"].Value.ToString() == puesto && diferenciaUbic == 1 && row.Cells["CENTRO"].Value.ToString().ToUpper().Contains("SANTIAGO"))) && !anadidos.Contains(nombreOp))
                                {

                                        switch (row.Cells[diasemana].Value.ToString())
                                        {
                                            case "MAÑANA":
                                                contadorActual = contadorM;
                                                columna = 3;

                                                contadorM++;
                                                break;

                                            case "TARDE":
                                                contadorActual = contadorT;
                                                columna = 4;
                                                contadorT++;
                                                break;

                                            case "NOCHE":
                                                contadorActual = contadorN;
                                                columna = 5;
                                                contadorN++;
                                                break;
                                            default:
                                                contadorActual = contadorCart;
                                                contadorCart++;
                                                columna = 7;
                                                break;
                                        }

                                        if (columna != 0)
                                        {
                                            oSheet.Cells[contadorActual, columna] = row.Cells["NOMB_OCULTO"].Value.ToString() + " " + row.Cells["AP_OCULTOS"].Value.ToString();
                                            oSheet.Cells[contadorActual, columna].EntireColumn.AutoFit();
                                            oSheet.Cells[contadorActual, columna].Font.Bold = true;
                                            anadidos.Add(nombreOp);

                                            if (/*oSheet.Cells[contadorActual, 2].Value != puesto && oSheet.Cells[contadorActual-1, 2].Value != puesto*/ !puestoEscrito && columna != 7)
                                            {
                                                if (puesto.ToUpper() == "SOLDADURA" && diferenciaUbic == 2 || puesto.ToUpper() == "SOLDADURA" && diferenciaUbic == 1)
                                                {
                                                    oSheet.Cells[contadorActual, 2] = puesto + "\n (" + row.Cells["CENTRO"].Value.ToString().ToUpper() + ")";
                                                }
                                                else
                                                {
                                                    oSheet.Cells[contadorActual, 2] = puesto;
                                                }
                                            
                                                /*oSheet.Cells[contadorPuesto, 2] = puesto;
                                                oSheet.Cells[contadorPuesto, 2].EntireColumn.AutoFit();
                                                oSheet.Cells[contadorPuesto, 2].Font.Bold = true;*/

                                                oSheet.Cells[contadorActual, 2].EntireColumn.AutoFit();
                                                oSheet.Cells[contadorActual, 2].Font.Bold = true;
                                                puestoEscrito = true;
                                            }


                                            coord1 = ToExcelCoordinates(2 + "," + contadorActual);
                                            coord2 = ToExcelCoordinates(5 + "," + contadorActual);

                                            if (row.Cells[diasemana].Value.ToString().Length > 1)
                                            {
                                                oSheet.get_Range(coord1, coord2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                                            }
                                        }
                                    }
                                }

                    

                        contadorActual = contadorM;

                        if (contadorActual < contadorT)
                        {
                            contadorActual = contadorT;
                        }

                        if (contadorActual < contadorN)
                        {
                            contadorActual = contadorN;
                        }

                        //contadorPuesto--;*/

                        contadorActual--;

                        coord1 = ToExcelCoordinates(2 + "," + contadorPuesto);
                        coord2 = ToExcelCoordinates(2 + "," + contadorActual);

                        contadorActual++;

                        oSheet.get_Range(coord1, coord2).Merge();
                        oSheet.get_Range(coord1, coord2).VerticalAlignment =
                        Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        oSheet.get_Range(coord1, coord2).HorizontalAlignment =
                        Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                        contadorM = contadorActual +2;
                        contadorN = contadorActual +2;
                        contadorT = contadorActual +2;



                        }

                    }
                }

                // Create an array to multiple values at once.
                /*string[,] saNames = new string[5, 2];

                saNames[0, 0] = "John";
                saNames[0, 1] = "Smith";
                saNames[1, 0] = "Tom";

                saNames[4, 1] = "Johnson";

                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("A2", "B6").Value2 = saNames;*/

                //Fill C2:C6 with a relative formula (=A2 & " " & B2).
                /*oRng = oSheet.get_Range("C2", "C6");
                oRng.Formula = "=A2 & \" \" & B2";

                //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                oRng = oSheet.get_Range("D2", "D6");
                oRng.Formula = "=RAND()*100000";
                oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();*/

                //oXL.Visible = false;
                //oXL.UserControl = false;
                /*oWB.SaveAs("c:\\test\\test505.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);*/

                //oWB.Close();
                //oXL.Quit();
            }
            finally { }
        }

        private void btnCerrarExcel_Click(object sender, EventArgs e)
        {
            this.panelExcel.Visible = false;
        }

        public string ToExcelCoordinates(string coordinates)
        {
            string first = coordinates.Substring(0, coordinates.IndexOf(','));
            int i = int.Parse(first);
            string second = coordinates.Substring(first.Length + 1);

            string str = string.Empty;
            while (i > 0)
            {
                str = alphabet[(i - 1) % 26] + str;
                i /= 26;
            }

            return str + second;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                this.panel4.Visible = true;
                this.labelNombrePuesto.Text = dataGridView1.Rows[e.RowIndex].Cells["NOMBRE"].Value.ToString();
                this.comboPuestoSeleccion.SelectedIndex = comboPuestoSeleccion.FindStringExact(dataGridView1.Rows[e.RowIndex].Cells["PUESTO"].Value.ToString());
                colMouse = e.ColumnIndex;
                rowMouse = e.RowIndex;
            }
        }

        private void btnGuardarPuesto_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridView1.Columns[colMouse].Name == "PUESTO")
            {
                this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value = this.comboPuestoSeleccion.Text;

                DataRowView currentDataRowView = (DataRowView)dataGridView1.Rows[rowMouse].DataBoundItem;
                DataRow row = currentDataRowView.Row;
                dbu.guardarDatos(row);
            }
            this.panel4.Visible = false;
            Cursor.Current = Cursors.Default;
        }

    }
}
