using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GestionVacaciones
{
    public partial class GestionVacaciones : Form
    {

        DataTable lista = null;
        DButils dbu = new DButils();
        static int añoActual = DateTime.Now.Year;

        DateTime begin = new DateTime(añoActual, 01, 01);
        DateTime end = new DateTime(añoActual, 12, 31);
        List<DateTime> festivos;
        DataTable vacaciones;
        DataView dv;
        String usuario;

        bool accesoTotal = false;
        List <string> usersAccesoTotal = new List<string> ();
        List<string> usersSoloLectura = new List<string>();

        int scrollIndex = -999;

        event System.Windows.Forms.DataGridViewCellEventHandler CellValueChanged;

        



        int colMouse = 0;
        int rowMouse = 0;
        int columnas;

        public GestionVacaciones()
        {
            InitializeComponent();
        }

        private void GestionVacaciones_Load(object sender, EventArgs e)
        {
            usersSoloLectura = dbu.obtenerSoloLectura();
            usuario = Environment.UserName;
            if (usuario == "cintia.collantes"){
                this.Size = new Size(1600, 450);
            }
            this.DoubleBuffered = true;
            cargarTabla();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            colMouse = this.dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
            rowMouse = this.dataGridView1.HitTest(e.X, e.Y).RowIndex;

            if (e.Button == MouseButtons.Left)
            {
                if (colMouse >= 0 && rowMouse <= dataGridView1.Rows.Count-1 && rowMouse >= 0)
                {
                    if (this.dataGridView1.Columns[colMouse].Name == "PUESTO" && (usuario == "alvaro.gutierrez" || usersAccesoTotal.Contains(usuario)) && !usersSoloLectura.Contains(usuario))
                    {
                        this.panel5.Visible = true;
                        this.comboPuesto.SelectedIndex = comboPuesto.FindStringExact(this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString());
                    }
                }
            }
            else
            {
                if (colMouse >= 0 && rowMouse <= dataGridView1.Rows.Count - 1 && rowMouse >= 0)
                {
                    ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();


                    if (colMouse >= columnas)
                    {
                        menu.Items.Add("Notas").Name = "nota";

                        if (!usersSoloLectura.Contains(usuario))
                        {
                            menu.Items.Add("Aprobar").Name = "Aprobar";
                            menu.Items.Add("Denegar").Name = "Denegar";
                            menu.Items.Add("Añadir Incidencia").Name = "incidencia";                            
                        }


                        if (this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString().EndsWith("h") || dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString() == "Asuntos propios")
                        {
                            menu.Items.Add("Información Horas AP día").Name = "horasAPdia";
                        }
                    }

                    menu.Items.Add("Información general Horas AP").Name = "horasAP";

                    menu.Show(dataGridView1, new Point(e.X, e.Y));

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
            DateTime date = new DateTime();
            string tipo = "";
            string estado = "";
            
            DateTime min = date;
            DateTime max = date;

            List<string> listaEmpVacas = new List<string>();
            List<string> listaLimite = new List<string>();

            switch (e.ClickedItem.Name.ToString())
            {
                case "nota":
                    if (colMouse > columnas)
                    {
                        if (this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString().Contains("(N)"))
                        {
                            date = numADia(colMouse - (columnas - 1), añoActual);
                            this.textBoxNotas.Text = dbu.notaPorDiaEmpleado(this.dataGridView1.Rows[rowMouse].Cells["NUMERO_PERSONAL"].Value.ToString(), date);
                        }
                        this.panelNotas.Visible = true;
                        this.textBoxNotas.Select();
                    }
                    break;

                case "Aprobar":

                    int dias = 0;
                    int limite = 0;
                    min = numADia(dataGridView1.SelectedCells[0].ColumnIndex - (columnas - 1), añoActual);
                    max = numADia(dataGridView1.SelectedCells[0].ColumnIndex - (columnas - 1), añoActual);;

                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (cell.ColumnIndex >= columnas)
                        {
                            date = numADia(cell.ColumnIndex - (columnas - 1), añoActual);

                            if (date < min)
                            {
                                min = date;
                            }
                            if (date > max)
                            {
                                max = date;
                            }

                            if (cell.Value.ToString().Contains("(N)"))
                            {
                                tipo = cell.Value.ToString().Remove(cell.Value.ToString().Length - 3);
                            }
                            else
                            {
                                tipo = cell.Value.ToString();
                            }

                            string jornada = "";
                            if (this.dataGridView1.Rows[cell.RowIndex].Cells["PUESTO"].Value.ToString() == "Oficinas")
                            {
                                jornada = "1765";
                            }
                            else
                            {
                                jornada = "1819";
                            }

                            if (!tipo.EndsWith("h"))
                            {
                                dias = Convert.ToInt32(this.dataGridView1.Rows[cell.RowIndex].Cells["DIAS_VACACIONES_UTILIZADOS"].Value);
                                limite = Convert.ToInt32(this.dataGridView1.Rows[cell.RowIndex].Cells["VAC"].Value)
                                    + Convert.ToInt32(this.dataGridView1.Rows[cell.RowIndex].Cells["VAA"].Value);
                                if (!listaLimite.Contains(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString()) && limite > dias)
                                {

                                    if (this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor != Color.Red && this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor != Color.Yellow)
                                    {
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.Red;
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.ForeColor = Color.White;
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = "VAC";

                                        
                                        dbu.actualizarPeticion("VAC", "Aceptado", this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString(), date, jornada, date, date, chkDiaCompleto.Checked);
                                        estado = "Aceptado";

                                        if (!listaEmpVacas.Contains(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString()))
                                        {
                                            listaEmpVacas.Add(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString());
                                        }

                                        int utilizados = Convert.ToInt32(this.dataGridView1.Rows[cell.RowIndex].Cells["DIAS_VACACIONES_UTILIZADOS"].Value);

                                        this.dataGridView1.Rows[cell.RowIndex].Cells["DIAS_VACACIONES_UTILIZADOS"].Value = (utilizados + 1).ToString();
                                        /*this.dataGridView1.Rows[cell.RowIndex].Cells["RESTANTES_VAC"].Value = (Convert.ToInt32(this.dataGridView1.Rows[cell.RowIndex].Cells["VAC"].Value) +
                                            Convert.ToInt32(this.dataGridView1.Rows[cell.RowIndex].Cells["VAA"].Value) -
                                            Convert.ToInt32(this.dataGridView1.Rows[cell.RowIndex].Cells["DIAS_UTILIZADOS"].Value)).ToString();*/
                                    }
                                }
                                else
                                {
                                    if (!listaLimite.Contains(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString())
                                        && !this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value.ToString().Contains("VAC")
                                        && this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value.ToString().Length < 4)
                                    {
                                        listaLimite.Add(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString());
                                        MessageBox.Show("Empleado " + this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value + " supera el límite de días de vacaciones. Operación inválida.", "ATENCIÓN",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                if ((dbu.obtenerHorasPeticionAP(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString(), date)
                                    + dbu.obtenerHorasAP(añoActual, this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString())
                                    <= Convert.ToDouble(this.dataGridView1.Rows[cell.RowIndex].Cells["HORAS_AP"].Value.ToString())))
                                {
                                    if (dbu.obtenerHorasPeticionAP(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString(), date) < 8)
                                    {
                                        dbu.actualizarPeticion("AP", "Aceptado", this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString(), date, jornada, date, date, false);
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.Yellow;
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.ForeColor = Color.Black;
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = "Asuntos propios";
                                    }
                                    else
                                    {
                                        dbu.actualizarPeticion("AP", "Aceptado", this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString(), date, jornada, date, date, true);
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.Yellow;
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.ForeColor = Color.Black;
                                        this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = "Asuntos propios";
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Empleado " + this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value + " supera el límite de horas de asuntos propios. Operación inválida.", "ATENCIÓN",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                
                            }
                        }
                    }

                    


                    break;

                case "Denegar":

                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (cell.ColumnIndex > columnas)
                        {
                            if (cell.Value.ToString().Contains("(N)"))
                            {
                                tipo = cell.Value.ToString().Remove(cell.Value.ToString().Length - 3);
                                cell.Value = "(N)";
                                cell.Style.ForeColor = Color.Black;
                                cell.Style.BackColor = Color.Lime;
                            }
                            else
                            {
                                tipo = cell.Value.ToString();
                                cell.Value = "R";
                                cell.Style.ForeColor = Color.White;
                                cell.Style.BackColor = Color.Black;
                            }



                            date = numADia(cell.ColumnIndex - (columnas - 1), añoActual);

                            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || this.festivos.Contains(date.Date))
                            {
                                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.Gray;
                            }



                            dbu.actualizarPeticion(tipo, "Rechazado", this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString(), date, "-", date, date, chkDiaCompleto.Checked);
                            estado = "Rechazado";

                            if (!listaEmpVacas.Contains(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString()) && (tipo == "VAC" || tipo == "V"))
                            {
                                listaEmpVacas.Add(this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString());
                            }
                        }
                        
                        
                    }

                    
                    break;

                case "incidencia":
                    bool igual = true;
                    string nump = this.dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString();
                    bool fin = false;

                    while (igual && !fin)
                    {
                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            if (this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString() != nump) igual = false;
                        }
                        fin = true;
                    }

                    if (igual)
                    {

                        this.panel1.Visible = true;
                        this.comboBoxFranjas.Items.Clear();

                        foreach (DataRow row in dbu.obtenerFranjasHorarias(this.dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString()).Rows)
                        {
                            this.comboBoxFranjas.Items.Add(row["DENOMINACION"]);
                        }

                        DateTime date1 = (numADia(colMouse - (columnas - 1), añoActual)).Date.Add(new TimeSpan(0, 0, 0));

                        this.dateTimePicker1.Value = date1;
                        this.dateTimePicker2.Value = date1;
                        this.btnAceptarIncidencia.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Seleccione sólo un empleado para establecer incidencias", "ATENCIÓN",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "horasAP":

                    this.labelNombre.Text = this.dataGridView1.Rows[rowMouse].Cells["NOMBRE"].Value.ToString() + " " + this.dataGridView1.Rows[rowMouse].Cells["APELLIDOS"].Value.ToString();
                    this.labelDpto.Text = this.dataGridView1.Rows[rowMouse].Cells["DEPARTAMENTO"].Value.ToString() + " - " + this.dataGridView1.Rows[rowMouse].Cells["PUESTO"].Value.ToString();

                    string numpersonal = this.dataGridView1.Rows[rowMouse].Cells["NUMERO_PERSONAL"].Value.ToString();

                    this.labelUsadas.Text = dbu.obtenerHorasAP(añoActual, numpersonal).ToString();
                    this.labelTotales.Text = this.dataGridView1.Rows[rowMouse].Cells["HORAS_AP"].Value.ToString();
                    this.labelRestantes.Text = (Convert.ToDouble(this.labelTotales.Text) - Convert.ToDouble(this.labelUsadas.Text)).ToString();

                    this.panel8.Visible = true;

                    break;

                case "horasAPdia":

                    this.labelNameAP.Text = this.dataGridView1.Rows[rowMouse].Cells["NOMBRE"].Value.ToString() + " " + this.dataGridView1.Rows[rowMouse].Cells["APELLIDOS"].Value.ToString();
                    DateTime fechaAP = numADia(colMouse - (columnas - 1), añoActual);
                    List<string> listaAPs = dbu.obtenerHorasAPDia(fechaAP, this.dataGridView1.Rows[rowMouse].Cells["NUMERO_PERSONAL"].Value.ToString());

                    this.labelTotalHoras.Text = listaAPs[2].ToString() + " hora(s)";
                    this.labelHIni.Text = listaAPs[0].ToString() + ":00";
                    this.labelHFin.Text = listaAPs[1].ToString() + ":00";

                    this.diaAP.Text = fechaAP.Day + "/" + fechaAP.Month + "/" + fechaAP.Year;
                    this.panel9.Visible = true;
                    break;
            }

            foreach(string codemp in listaEmpVacas)
            {
                /*DataTable aux = dv.ToTable();
                aux.PrimaryKey = new DataColumn[] { lista.Columns["NUMERO_PERSONAL"] };
                int index = lista.Rows.IndexOf(aux.Rows.Find(codemp));*/

                int index = lista.Rows.IndexOf(lista.Rows.Find(codemp));

                this.dbu.actualizarPeticionesVacas(min, max, (max - min).Days, codemp, estado);

                lista.Rows[index]["DIAS_VACACIONES_UTILIZADOS"] = dbu.diasUtilizados(codemp, añoActual.ToString());
                lista.Rows[index]["RESTANTES_VAC"] = (Convert.ToInt32(lista.Rows[index]["VAC"]) +
                                            Convert.ToInt32(lista.Rows[index]["VAA"]) -
                                            Convert.ToInt32(lista.Rows[index]["DIAS_VACACIONES_UTILIZADOS"])).ToString();
            }
        }

        private void btnCancelarNotas_Click(object sender, EventArgs e)
        {
            this.panelNotas.Visible = false;
            this.textBoxNotas.Clear();
        }

        private void btnGuardarNotas_Click(object sender, EventArgs e)
        {

            //this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value = this.textBoxNotas.Text;
            if (!this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString().Contains("(N)"))
            {
                this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value = this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString() + "(N)";
            }
            DateTime date = numADia(colMouse - (columnas - 1), añoActual);

            this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.BackColor = Color.Lime;
            dbu.insertarNotas(this.dataGridView1.Rows[rowMouse].Cells["NUMERO_PERSONAL"].Value.ToString(), date.Date, this.textBoxNotas.Text);

            if (this.textBoxNotas.Text == "")
            {
                if (this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString().Contains("(N)"))
                {
                    this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value =
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString().Remove(this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString().Length - 3);
                }

                switch (this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value.ToString())
                {
                    case "VAC":
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.BackColor = Color.Red;
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.ForeColor = Color.White;
                        break;
                    case "":
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.BackColor = Color.White;
                        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || this.festivos.Contains(date.Date))
                        {

                            dataGridView1.Columns[colMouse].DefaultCellStyle.BackColor = Color.Gray;
                        }
                        break;

                    case "P":
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.BackColor = Color.Aquamarine;
                        break;
                    case "V":
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.BackColor = Color.Aquamarine;
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.ForeColor = Color.Black;
                        break;
                    case "AP":
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.BackColor = Color.Aquamarine;
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.ForeColor = Color.Black;
                        break;
                    default:
                        this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.BackColor = Color.Yellow;
                        break;
                }

                this.dataGridView1.Rows[rowMouse].Cells[colMouse].Style.ForeColor = Color.Black;
                
            }
            this.panelNotas.Visible = false;
            this.textBoxNotas.Clear();
        }

        public void pintarVacaciones()
        {
            int index = 0;
            string nump = "";
            //List<DateTime> vacaciones = null;

            int id = 0;

            foreach (DataRow row in vacaciones.Rows)
            {
                nump = row["NUMERO_PERSONAL_EMPLEADO"].ToString();
                index = lista.Rows.IndexOf(lista.Rows.Find(row["NUMERO_PERSONAL_EMPLEADO"].ToString()));

                /*if (accesoTotal)
                {
                    index--;
                }*/
                if (index < 0)
                {
                    string test = "";
                }

                id = Convert.ToInt32(row["ID_INCIDENCIA"].ToString());
                if (index >= 0)
                {
                    if (id == 24 || id == 6)
                    {
                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Style.BackColor = Color.Red;
                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Style.ForeColor = Color.White;
                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value = "VAC";
                    }
                    else
                    {
                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Style.BackColor = Color.Yellow;
                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value = row["nombre"].ToString();
                    }
                }

                
            }

            /*foreach (DataRow row in vacaciones.Rows)
            {
                //Esto de aquí abajo funciona pero tarda un rato relativamente importante
                index = lista.Rows.IndexOf(lista.Rows.Find(row["numPersonal"]));

                for (int i = 1; i <= Convert.ToInt32(row["numDias"].ToString()) && index != -1; i++)
                {
                    this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["fechaInicio"].ToString()).Date.DayOfYear) + columnas - 1].Style.BackColor = Color.Red;
                }
            }*/

            /*
            //Este también tarda bastante cuando hay mucha gente
            foreach(DataRow row in lista.Rows)
            {
                vacaciones = dbu.vacasPorEmpleado(row["NUMERO_PERSONAL"].ToString(), "2022");

                foreach (DateTime date in vacaciones)
                {
                    this.dataGridView1.Rows[index].Cells[date.DayOfYear + columnas - 1].Style.BackColor = Color.Red;
                }
                index++;
            }*/
        }

        public void pintarIncidencias()
        {
            int index = 0;
            int id = 0;

            DataTable aps = dbu.obtenerPeticionesAP(añoActual.ToString());
            DataTable pVaca = dbu.obtenerPeticionesVacac(añoActual.ToString());

            foreach (DataRow row in aps.Rows)
            {
                
                if (row["Estado"].ToString() == "Pendiente")
                {
                    index = lista.Rows.IndexOf(lista.Rows.Find(row["numPersonal"]));
                    
                    /*if (accesoTotal)
                    {
                        index--;
                    }*/
                    if (index > -1)
                    {


                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Style.BackColor = Color.Aquamarine;
                        //this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value = "AP";
                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value = row["numHoras"] + "h";


                    }
                }

                if (row["Estado"].ToString() == "Rechazado")
                {
                    index = lista.Rows.IndexOf(lista.Rows.Find(row["numPersonal"]));

                    /*if (accesoTotal)
                    {
                        index--;
                    }*/
                    if (index > -1)
                    {


                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Style.BackColor = Color.Black;
                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Style.ForeColor = Color.White;
                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value = "R";


                    }
                }
            }
            foreach (DataRow row in pVaca.Rows)
            {
                if (row["Estado"].ToString() == "Pendiente")
                {
                    index = lista.Rows.IndexOf(lista.Rows.Find(row["numPersonal"]));
                    
                    ////if (accesoTotal) index--;
                    if (index > -1)
                    {

                        begin = Convert.ToDateTime(row["FECHAINI"].ToString()).Date;
                        end = Convert.ToDateTime(row["FECHAFIN"].ToString()).Date;

                        for (DateTime date = begin; date <= end; date = date.AddDays(1))
                        {

                            if ((row["PUESTO"].ToString() == "000" && (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)) || row["PUESTO"].ToString() != "000")
                            {
                                this.dataGridView1.Rows[index].Cells[(date.DayOfYear) + columnas - 1].Style.BackColor = Color.Aquamarine;
                                this.dataGridView1.Rows[index].Cells[(date.DayOfYear) + columnas - 1].Style.ForeColor = Color.Black;
                                this.dataGridView1.Rows[index].Cells[(date.DayOfYear) + columnas - 1].Value = "V";
                            }


                        }
                    }
                }

                if (row["Estado"].ToString() == "Rechazado")
                {
                    index = lista.Rows.IndexOf(lista.Rows.Find(row["numPersonal"]));

                    //if (accesoTotal) index--;
                    if (index > -1)
                    {

                        begin = Convert.ToDateTime(row["FECHAINI"].ToString()).Date;
                        end = Convert.ToDateTime(row["FECHAFIN"].ToString()).Date;

                        for (DateTime date = begin; date <= end; date = date.AddDays(1))
                        {

                            if ((row["PUESTO"].ToString() == "000" && (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)) || row["PUESTO"].ToString() != "000")
                            {
                                this.dataGridView1.Rows[index].Cells[(date.DayOfYear) + columnas - 1].Style.BackColor = Color.Black;
                                this.dataGridView1.Rows[index].Cells[(date.DayOfYear) + columnas - 1].Style.ForeColor = Color.White;
                                this.dataGridView1.Rows[index].Cells[(date.DayOfYear) + columnas - 1].Value = "R";
                            }


                        }
                    }
                }
            }

                
        }

        public void pintarNotas()
        {
            DataTable notas = dbu.obtenerNotas(añoActual.ToString());
            int index = 0;

            foreach (DataRow row in notas.Rows)
            {
                index = lista.Rows.IndexOf(lista.Rows.Find(row["numPersonal"])) - 1;
                if (index > -1)
                {
                    this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Style.BackColor = Color.Lime;

                    if (!this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value.ToString().Contains("(N)")){

                        this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value = this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value.ToString() + "(N)";
                        //this.dataGridView1.Rows[index].Cells[(Convert.ToDateTime(row["FECHA"].ToString()).Date.DayOfYear) + columnas - 1].Value = row["notas"].ToString();
                    }
                }
            }
        }


        public void cargarTabla()
        {
            usersAccesoTotal = dbu.obtenerAccesoTotal();


            this.comboBoxDpto.Items.Clear();
            this.comboBoxFranjas.Items.Clear();
            this.comboFiltroPuesto.Items.Clear();
            this.comboIncidencias.Items.Clear();
            this.comboJornada.Items.Clear();
            this.comboPuesto.Items.Clear();

            if (usersAccesoTotal.Contains(usuario) || usersSoloLectura.Contains(usuario))
            {
                accesoTotal = true;
                this.comboBoxDpto.Visible = true;
                this.label5.Visible = true;
                lista = dbu.obtenerPersonas(añoActual.ToString(), accesoTotal, usuario);
                this.comboBoxDpto.Items.Add("TODOS");
                this.comboFiltroPuesto.Items.Add("TODOS");
                foreach (DataRow row in dbu.obtenerDptos().Rows)
                {
                    this.comboBoxDpto.Items.Add(row["departamento"]);
                }
                this.comboBoxDpto.SelectedIndex = 0;
            }
            else
            {                                
                this.comboFiltroPuesto.Items.Add("TODOS");
                lista = dbu.obtenerPersonas(añoActual.ToString(), accesoTotal, usuario);

                this.comboBoxDpto.Visible = true;
                this.label5.Visible = true;
                this.comboBoxDpto.Items.Add("TODOS");
                foreach (DataRow row in dbu.obtenerDptosActuales(usuario).Rows)
                {
                    this.comboBoxDpto.Items.Add(row["departamento"]);
                }
                this.comboBoxDpto.SelectedIndex = 0;
            }



            DataTable puestos = new DataTable();
            if (usersAccesoTotal.Contains(usuario) || usuario == "alvaro.gutierrez")
            {
                puestos = dbu.obtenerPuestos();
            }
            else
            {
                puestos = dbu.obtenerPuestosActuales(usuario);
            }
            foreach (DataRow row in puestos.Rows)
            {
                this.comboPuesto.Items.Add(row["denomi"]);
                this.comboFiltroPuesto.Items.Add(row["denomi"]);
            }


            this.comboFiltroPuesto.SelectedIndex = 0;
            Cursor.Current = Cursors.WaitCursor;

            foreach (DataColumn col in lista.Columns)
            {
                col.ReadOnly = true;

                if ((col.ColumnName == "VAC" || col.ColumnName == "VAA") && usersAccesoTotal.Contains(usuario) && !usersSoloLectura.Contains(usuario))
                {
                    col.ReadOnly = false;
                }

                if (col.ColumnName == "PUESTO" && (usuario == "alvaro.gutierrez" || usersAccesoTotal.Contains(usuario)) && !usersSoloLectura.Contains(usuario))
                {
                    
                    col.ReadOnly = false;

                }

                if ((col.ColumnName == "DIAS_VACACIONES_UTILIZADOS" || /*col.ColumnName == "HORAS_AP_USADAS" ||*/ col.ColumnName == "RESTANTES_VAC") && !usersSoloLectura.Contains(usuario))
                {
                    col.ReadOnly = false;
                }

                if (col.ColumnName == "HORAS_AP" && usersAccesoTotal.Contains(usuario) && !usersSoloLectura.Contains(usuario))
                {
                    col.ReadOnly = false;
                }

            }

            foreach (DataRow row in dbu.obtenerTiposIncidencias().Rows)
            {
                this.comboIncidencias.Items.Add(row["nombre"]);
            }

            
            festivos = dbu.obtenerFestivos(añoActual.ToString());
            int dia = 0;
            int mes = 1;
            columnas = this.lista.Columns.Count;
            string nombremes = "";

            

            this.textBoxAnio.Text = añoActual.ToString();

            begin = new DateTime(añoActual, 01, 01);
            end = new DateTime(añoActual, 12, 31);

            int contadorIndex = 0;

            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                dia = date.Day;

                mes = date.Month;

                if (date.Day == DateTime.Now.Day && date.Month == DateTime.Now.Month)
                {
                    scrollIndex = contadorIndex + columnas;                   
                }
                contadorIndex++;

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



                /*if (mes != date.Month)
                {
                          
                }
                mes = date.Month;*/

            }

            lista.PrimaryKey = new DataColumn[] { lista.Columns["NUMERO_PERSONAL"] };

            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // or even better, use .DisableResizing. Most time consuming enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders

            // set it to false if not needed
            dataGridView1.RowHeadersVisible = false;

            this.dataGridView1.DataSource = lista;
            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(datagridview_CellFormatting);

            for (int i = 0; i < columnas; i++)
            {
                dataGridView1.Columns[i].Frozen = true;
            }

            //vacaciones = dbu.obtenerVacas("2022");
            vacaciones = dbu.obtenerIncidencias(añoActual.ToString());
        }

        public void colorLoad()
        {
            int mes = begin.Month;
            int dia;
            int cntCol = columnas;
            int cntRow = 0;

            bool altern = false;
            DataGridViewColumn dataGridViewColumn;

            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                dia = date.Day;
                dataGridViewColumn = dataGridView1.Columns[cntCol];
                dataGridView1.Columns[cntCol].DefaultCellStyle.BackColor = Color.White;


                if (mes != date.Month)
                {
                    altern = !altern;
                }

                if (altern)
                {
                    dataGridViewColumn.HeaderCell.Style.BackColor = Color.Cyan;
                }
                else
                {
                    dataGridViewColumn.HeaderCell.Style.BackColor = Color.Magenta;
                }

                /*if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || this.festivos.Contains(date.Date))
                {

                    dataGridView1.Columns[cntCol].DefaultCellStyle.BackColor = Color.Gray;
                }*/

                cntRow = 0;

                /*foreach (DataRow row in lista.Rows)
                {

                    foreach (DataRow rowAP in aps.Rows)
                    {
                        if (rowAP["FECHA"].ToString() == date.Date.ToString() && rowAP["numPersonal"].ToString() == row["NUMERO_PERSONAL"].ToString())
                        {
                            this.dataGridView1.Rows[cntRow].Cells[cntCol].Style.BackColor = Color.Yellow;
                        }
                    }
                    cntRow++;
                }*/

                dataGridView1.Columns[cntCol].Width = 35;
                mes = date.Month;
                cntCol++;

            }


            pintarIncidencias();
            pintarVacaciones();
            pintarNotas();

            this.dataGridView1.Columns["VAC"].Width = 35;
            this.dataGridView1.Columns["VAA"].Width = 35;
            this.dataGridView1.Columns["DEPARTAMENTO"].Width = 80;
            this.dataGridView1.Columns["NUMERO_PERSONAL"].Width = 50;
            this.dataGridView1.Columns["DIAS_VACACIONES_UTILIZADOS"].Width = 35;
            this.dataGridView1.Columns["HORAS_AP"].Width = 35;
            //this.dataGridView1.Columns["HORAS_AP_USADAS"].Width = 35;
            this.dataGridView1.Columns["RESTANTES_VAC"].Width = 35;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.MouseClick += new MouseEventHandler(dataGridView1_MouseClick);
            dataGridView1.RowHeadersVisible = true;

            this.dataGridView1.FirstDisplayedScrollingColumnIndex = scrollIndex;
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

        private void btnCargarAnio_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.buscador.Clear();
            añoActual = Convert.ToInt32(textBoxAnio.Text);
            cargarTabla();
            colorLoad();
            Cursor.Current = Cursors.Default;

        }

        private void textBoxBuscador_TextChanged(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            string busq = this.buscador.Text.ToString();
            string dpto = "";
            string puesto = "";
            if (this.comboBoxDpto.SelectedIndex != 0)
            {
                dpto = this.comboBoxDpto.SelectedItem.ToString();
            }
            if (this.comboFiltroPuesto.SelectedIndex != 0 && this.comboFiltroPuesto.SelectedIndex != -1)
            {
                puesto = this.comboFiltroPuesto.SelectedItem.ToString();
            }

            string filtro = "(NOMBRE LIKE '%" + this.buscador.Text + "%' OR Convert([NUMERO_PERSONAL], System.String) LIKE '%"
                + this.buscador.Text + "%' OR APELLIDOS LIKE '%" + this.buscador.Text + "%') AND (DEPARTAMENTO LIKE '%" + dpto + "%'";


            if (dpto == "") filtro += "OR DEPARTAMENTO IS NULL";
            
            filtro += ") AND (PUESTO LIKE '%" + puesto + "%' OR PUESTO IS NULL)";



            lista.DefaultView.RowFilter = filtro;
            dv = lista.DefaultView;




            /*foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex > columnas && !cell.Value.ToString().Contains("(N)"))
                    {
                        cell.Style.ForeColor = Color.Black;
                        switch (cell.Value.ToString())
                        {
                                
                            case "VAC":
                                cell.Style.BackColor = Color.Red;
                                cell.Style.ForeColor = Color.White;
                                break;
                            case "":
                                break;

                            case "P":
                                cell.Style.BackColor = Color.Aquamarine;
                                break;
                            case "V":
                                cell.Style.BackColor = Color.Aquamarine;
                                break;
                            default:
                                cell.Style.BackColor = Color.Yellow;
                                break;
                        }
                    }
                    else if (cell.Value.ToString().Contains("(N)"))
                    {
                        cell.Style.BackColor = Color.Lime;
                    }

                }
            }*/
            
            Cursor.Current = Cursors.Default;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataRow row in lista.Rows)
            {
                dbu.guardarDatos(row, false);
                row["RESTANTES_VAC"] = Convert.ToInt32(row["VAC"]) + Convert.ToInt32(row["VAA"]) - Convert.ToInt32(row["DIAS_VACACIONES_UTILIZADOS"]);
            }

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Cambios guardados correctamente", "ATENCIÓN", MessageBoxButtons.OK);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarIncidencias_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void btnAceptarIncidencia_Click(object sender, EventArgs e)
        {
            DateTime date;
            DateTime ini;
            DateTime fin;
            DataTable franjas = null;

            string id_jornada = "-";

            franjas = dbu.obtenerFranjasHorarias(this.dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString());
            id_jornada = franjas.Rows[this.comboBoxFranjas.SelectedIndex][0].ToString();
            
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells){

                date = numADia(cell.ColumnIndex -(columnas-1), añoActual);
                ini = this.dateTimePicker1.Value;
                fin = this.dateTimePicker2.Value;
                string tipo = this.comboIncidencias.Text;
                dbu.actualizarPeticion(tipo, "Aceptado", this.dataGridView1.Rows[cell.RowIndex].Cells["NUMERO_PERSONAL"].Value.ToString(), date, id_jornada, ini , fin, this.chkDiaCompleto.Checked);

                if (cell.Value.ToString().Contains("(N)"))
                {
                    cell.Value = tipo + "(N)";
                    cell.Style.BackColor = Color.Lime;
                }
                else
                {
                    cell.Value = tipo;
                    cell.Style.BackColor = Color.Yellow;
                }

                int horas = fin.Hour - ini.Hour;
                //this.dataGridView1.Rows[cell.RowIndex].Cells["HORAS_AP_USADAS"].Value = Convert.ToInt32(this.dataGridView1.Rows[cell.RowIndex].Cells["HORAS_AP_USADAS"].Value.ToString()) + horas;
                
            }

            this.panel1.Visible = false;
            
        }

        private void datagridview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            
            if (e.ColumnIndex >= columnas && !e.Value.ToString().Contains("(N)"))
            {
                e.CellStyle.ForeColor = Color.Black;

                DateTime date = numADia(e.ColumnIndex - (columnas - 1), añoActual);

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || this.festivos.Contains(date.Date))
                {
                    e.CellStyle.BackColor = Color.Gray;
                }

                if (e.Value.ToString() != "")
                {

                    if (e.Value.ToString() == "VAC")
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else if (e.Value.ToString() == "")
                    {

                    }
                    else if (e.Value.ToString() == "AP" || e.Value.ToString().EndsWith("h") || e.Value.ToString() == "V")
                    {
                        e.CellStyle.BackColor = Color.Aquamarine;
                    }
                    else if (e.Value.ToString() == "R")
                    {
                        e.CellStyle.BackColor = Color.Black;
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                    }

                    
                }
            }
            else if (e.Value.ToString().Contains("(N)"))
            {
                e.CellStyle.BackColor = Color.Lime;
            }
        }

        private void comboIncidencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnAceptarIncidencia.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
        }

        private void btnConfirmarPuesto_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridView1.Columns[colMouse].Name == "PUESTO")
            {
                this.dataGridView1.Rows[rowMouse].Cells[colMouse].Value = this.comboPuesto.Text;

                DataRowView currentDataRowView = (DataRowView)dataGridView1.Rows[rowMouse].DataBoundItem;
                DataRow row = currentDataRowView.Row;
                dbu.guardarDatos(row, true);
            }
            this.panel5.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.panel8.Visible = false;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string msg = String.Format("Cell at row {0}, column {1} value changed", e.RowIndex, e.ColumnIndex);
            MessageBox.Show(msg, "Cell Value Changed");
        }

        private void chkDiaCompleto_CheckedChanged(object sender, EventArgs e)
        {
            this.dateTimePicker1.Enabled = !this.chkDiaCompleto.Checked;
            this.dateTimePicker2.Enabled = !this.chkDiaCompleto.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.panel9.Visible = false;
        }

        
        private void btn_excel_Click(object sender, EventArgs e)
        {
            string dgvToHTMLTable = ConvertDataGridViewToHTMLWithFormatting(this.dataGridView1);
            Clipboard.SetText(dgvToHTMLTable);

            //copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR);

           
        }


        //VERSION 1
        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
            {
                Clipboard.SetDataObject(dataObj);
            }
        }


        //VERSION 2
        public string ConvertDataGridViewToHTMLWithFormatting(DataGridView dgv)
        {
            StringBuilder sb = new StringBuilder();
            //create html & table
            sb.AppendLine("<html><body><center><table border='1' cellpadding='0' cellspacing='0'>");
            sb.AppendLine("<tr>");
            //create table header
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                sb.Append(DGVHeaderCellToHTMLWithFormatting(dgv, i));
                sb.Append(DGVCellFontAndValueToHTML(dgv.Columns[i].HeaderText, dgv.Columns[i].HeaderCell.Style.Font));
                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");
            //create table body
            for (int rowIndex = 0; rowIndex < dgv.Rows.Count; rowIndex++)
            {
                sb.AppendLine("<tr>");
                foreach (DataGridViewCell dgvc in dgv.Rows[rowIndex].Cells)
                {
                    sb.AppendLine(DGVCellToHTMLWithFormatting(dgv, rowIndex, dgvc.ColumnIndex));
                    string cellValue = dgvc.Value == null ? string.Empty : dgvc.Value.ToString();
                    sb.AppendLine(DGVCellFontAndValueToHTML(cellValue, dgvc.Style.Font));
                    sb.AppendLine("</td>");
                }
                sb.AppendLine("</tr>");
            }
            //table footer & end of html file
            sb.AppendLine("</table></center></body></html>");
            return sb.ToString();
        }

        //TODO: Add more cell styles described here: https://msdn.microsoft.com/en-us/library/1yef90x0(v=vs.110).aspx
        public string DGVHeaderCellToHTMLWithFormatting(DataGridView dgv, int col)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td");
            sb.Append(DGVCellColorToHTML(dgv.Columns[col].HeaderCell.Style.ForeColor, dgv.Columns[col].HeaderCell.Style.BackColor));
            sb.Append(DGVCellAlignmentToHTML(dgv.Columns[col].HeaderCell.Style.Alignment));
            sb.Append(">");
            return sb.ToString();
        }

        public string DGVCellToHTMLWithFormatting(DataGridView dgv, int row, int col)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td");

            DateTime date = numADia(col-10, añoActual);

            Color forecolor = Color.Black;
            Color backcolor = Color.White;

            if ((date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || this.festivos.Contains(date.Date)) && (col > 10) && dgv.Rows[row].Cells[col].Value.ToString().Length <= 0)
            {
                forecolor = Color.Black;
                backcolor = Color.Gray;
            }
            else
            {

                if (dgv.Rows[row].Cells[col].Value.ToString().Length > 0 && col > 10)
                {
                    switch (dgv.Rows[row].Cells[col].Value.ToString())
                    {

                        case "VAC":
                            backcolor = Color.Red;
                            forecolor = Color.White;
                            break;
                        case "":
                            break;

                        case "AP":
                            backcolor = Color.Aquamarine;
                            break;
                        case "V":
                            backcolor = Color.Aquamarine;
                            break;
                        case "R":
                            backcolor = Color.Black;
                            forecolor = Color.White;
                            break;
                        default:
                            backcolor = Color.Yellow;
                            break;
                    }
                }

                
            }

            sb.Append(DGVCellColorToHTML(forecolor, backcolor));

            sb.Append(DGVCellAlignmentToHTML(dgv.Rows[row].Cells[col].Style.Alignment));
            sb.Append(">");
            return sb.ToString();
        }

        public string DGVCellColorToHTML(Color foreColor, Color backColor)
        {
            if (foreColor.Name == "0" && backColor.Name == "0") return string.Empty;

            StringBuilder sb = new StringBuilder();

            
            sb.Append(" style=\"");
            if (foreColor.Name != "0" && backColor.Name != "0")
            {
                sb.Append("color:#");
                sb.Append(foreColor.R.ToString("X2") + foreColor.G.ToString("X2") + foreColor.B.ToString("X2"));
                sb.Append("; background-color:#");
                sb.Append(backColor.R.ToString("X2") + backColor.G.ToString("X2") + backColor.B.ToString("X2"));
            }
            else if (foreColor.Name != "0" && backColor.Name == "0")
            {
                sb.Append("color:#");
                sb.Append(foreColor.R.ToString("X2") + foreColor.G.ToString("X2") + foreColor.B.ToString("X2"));
            }
            else //if (foreColor.Name == "0" &&  backColor.Name != "0")
            {
                sb.Append("background-color:#");
                sb.Append(backColor.R.ToString("X2") + backColor.G.ToString("X2") + backColor.B.ToString("X2"));
            }

            sb.Append(";\"");
            return sb.ToString();
        }

        public string DGVCellFontAndValueToHTML(string value, Font font)
        {
            //If no font has been set then assume its the default as someone would be expected in HTML or Excel
            if (font == null || font == this.Font && !(font.Bold | font.Italic | font.Underline | font.Strikeout)) return value;
            StringBuilder sb = new StringBuilder();
            sb.Append(" ");
            if (font.Bold) sb.Append("<b>");
            if (font.Italic) sb.Append("<i>");
            if (font.Strikeout) sb.Append("<strike>");

            //The <u> element was deprecated in HTML 4.01. The new HTML 5 tag is: text-decoration: underline
            if (font.Underline) sb.Append("<u>");

            string size = string.Empty;
            if (font.Size != this.Font.Size) size = "font-size: " + font.Size + "pt;";

            //The <font> tag is not supported in HTML5. Use CSS or a span instead. 
            if (font.FontFamily.Name != this.Font.Name)
            {
                sb.Append("<span style=\"font-family: ");
                sb.Append(font.FontFamily.Name);
                sb.Append("; ");
                sb.Append(size);
                sb.Append("\">");
            }
            sb.Append(value);
            if (font.FontFamily.Name != this.Font.Name) sb.Append("</span>");

            if (font.Underline) sb.Append("</u>");
            if (font.Strikeout) sb.Append("</strike>");
            if (font.Italic) sb.Append("</i>");
            if (font.Bold) sb.Append("</b>");

            return sb.ToString();
        }

        public string DGVCellAlignmentToHTML(DataGridViewContentAlignment align)
        {
            if (align == DataGridViewContentAlignment.NotSet) return string.Empty;

            string horizontalAlignment = string.Empty;
            string verticalAlignment = string.Empty;
            CellAlignment(align, ref horizontalAlignment, ref verticalAlignment);
            StringBuilder sb = new StringBuilder();
            sb.Append(" align='");
            sb.Append(horizontalAlignment);
            sb.Append("' valign='");
            sb.Append(verticalAlignment);
            sb.Append("'");
            return sb.ToString();
        }

        private void CellAlignment(DataGridViewContentAlignment align, ref string horizontalAlignment, ref string verticalAlignment)
        {
            switch (align)
            {
                case DataGridViewContentAlignment.MiddleRight:
                    horizontalAlignment = "right";
                    verticalAlignment = "middle";
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    horizontalAlignment = "left";
                    verticalAlignment = "middle";
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    horizontalAlignment = "centre";
                    verticalAlignment = "middle";
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    horizontalAlignment = "centre";
                    verticalAlignment = "top";
                    break;
                case DataGridViewContentAlignment.BottomCenter:
                    horizontalAlignment = "centre";
                    verticalAlignment = "bottom";
                    break;
                case DataGridViewContentAlignment.TopLeft:
                    horizontalAlignment = "left";
                    verticalAlignment = "top";
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    horizontalAlignment = "left";
                    verticalAlignment = "bottom";
                    break;
                case DataGridViewContentAlignment.TopRight:
                    horizontalAlignment = "right";
                    verticalAlignment = "top";
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    horizontalAlignment = "right";
                    verticalAlignment = "bottom";
                    break;

                default: //DataGridViewContentAlignment.NotSet
                    horizontalAlignment = "left";
                    verticalAlignment = "middle";
                    break;
            }
        }


        
    }
}
