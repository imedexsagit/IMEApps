using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AdvancedDataGridView;

namespace TareasFaltanDatos
{
    public partial class Form1 : Form
    {

        public static DataTable dT;
        public static DataTable dTPrimerNivel;
        public static DataTable dTSegundoNivel;
        ConsultasBD fbd = new ConsultasBD();

        public Form1()
        {
            InitializeComponent();
        }
          
        private void Form1_Load(object sender, EventArgs e)
        {
            int numeroFilas = fbd.comprobarTabla();

            if (numeroFilas == 0)
            {
                cargarFormulario();
            }
            else {
                MessageBox.Show("Las fechas se estan actualizando por otro usuario. Por favor, espera a que finalice para que no existan problemas de integridad.");
                this.btnGuardar.Enabled = false;
                this.btnRefrescar.Enabled = false;
                this.fecha.Enabled = false;
            }




        }

        private void cargarFormulario()
        {
            fbd.actualizarTabla();
            MostrarInfoArbol();

            tgFaltanDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;


            tgFaltanDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            tgFaltanDatos.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);

            tgFaltanDatos.EnableHeadersVisualStyles = false;

            tgFaltanDatos.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            tgFaltanDatos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            tgFaltanDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            tgFaltanDatos.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            tgFaltanDatos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            tgFaltanDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            tgFaltanDatos.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            tgFaltanDatos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            tgFaltanDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            foreach (TreeGridNode nodo in tgFaltanDatos.Nodes)
            {
                nodo.Cells[0].Style.BackColor = ColorTranslator.FromHtml("#bfdbff");
                foreach (TreeGridNode nodod in nodo.Nodes)
                {
                    nodod.Cells[0].Style.BackColor = Color.Ivory;
                    nodod.Cells[1].Style.BackColor = Color.Ivory;
                    nodod.Cells[2].Style.BackColor = Color.Ivory;

                    foreach (TreeGridNode nodof in nodod.Nodes)
                    {

                        if (String.IsNullOrEmpty(nodof.Cells[7].Value.ToString()))
                        {

                            nodof.Cells[7].Style.BackColor = Color.NavajoWhite;
                        }
                        else
                        {
                            nodof.Cells[7].Style.BackColor = ColorTranslator.FromHtml("#ADEBC3");
                        }

                        if (String.IsNullOrEmpty(nodof.Cells[8].Value.ToString()))
                        {

                            nodof.Cells[8].Style.BackColor = Color.NavajoWhite;
                        }
                        else
                        {
                            nodof.Cells[8].Style.BackColor = ColorTranslator.FromHtml("#ADEBC3");
                        }


                    }

                }
            }


            // tgFaltanDatos.Columns[7].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#ADEBC3");
            // tgFaltanDatos.Columns[8].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#ADEBC3");

        }





        private void button1_Click(object sender, EventArgs e)
        {
            fbd.actualizarTabla();
            MostrarInfoArbol();
        }



        private void MostrarInfoArbol()
        {
            Cursor.Current = Cursors.WaitCursor;

           

            limpiarArbol();

            dT = fbd.obtenerPedidosFD();

            
            crearArbol();

            Cursor.Current = Cursors.Default;
        
        }


        private void limpiarArbol()
        {
            tgFaltanDatos.Nodes.Clear();
        }

        private void crearArbol()
        {
            for (int x = 0; x < dT.Rows.Count; x++)
            {
                String Pedido = dT.Rows[x][0].ToString();
                String Cliente = dT.Rows[x][1].ToString();
                String Tarea = "";
                //String FechaFin = Convert.ToDateTime(dT.Rows[x][2].ToString()).ToString("dd/MM/YYYY");
                String FechaFin = dT.Rows[x][2].ToString();
                String Linea = "";
                String Descripcion = "";
                String Serie = "";
                String FechaNecesaria = "";
                String FechaPrevista = "";
                String Euro  = dT.Rows[x][3].ToString(); 
                String EuroKilo = "";
                String Kilos = dT.Rows[x][4].ToString(); 

                TreeGridNode node = tgFaltanDatos.Nodes.Add(Pedido, Cliente, Tarea,FechaFin,Linea,Descripcion,Serie,FechaNecesaria,FechaPrevista,Euro,EuroKilo,Kilos);

                añadirHoja(node,Pedido,Cliente);
            }
           // añadirHoja(node, Paquete);
        }

        private void añadirHoja(TreeGridNode nodo, string ped, string cli)
        {
            dTPrimerNivel = fbd.obtenerTareas(ped, cli);
            for (int x = 0; x < dTPrimerNivel.Rows.Count; x++)
            {
                String Pedido = ped;
                String Cliente = cli;
                String Tarea = dTPrimerNivel.Rows[x][0].ToString(); 
                String FechaFin = "";
                String Linea = "";
                String Descripcion = "";
                String Serie = "";
                String FechaNecesaria = "";
                String FechaPrevista = "";
                String Euro = dTPrimerNivel.Rows[x][1].ToString();
                String EuroKilo = "";
                String Kilos = dTPrimerNivel.Rows[x][2].ToString();

                TreeGridNode nodoHijo = nodo.Nodes.Add(Pedido, Cliente, Tarea, FechaFin, Linea, Descripcion, Serie, FechaNecesaria, FechaPrevista, Euro, EuroKilo, Kilos);
                añadirHojaSegundoNivel(nodoHijo, ped, cli, Tarea);
            }

        }

        private void añadirHojaSegundoNivel(TreeGridNode nodo, string ped, string cli,string tarea)
        {
            dTSegundoNivel = fbd.obtenerLineas(ped, cli, tarea);
            for (int x = 0; x < dTSegundoNivel.Rows.Count; x++)
            {
                String Pedido = "";
                String Cliente = "";
                String Tarea = "";
                String FechaFin = "";
                String Linea = dTSegundoNivel.Rows[x][0].ToString();
                String Descripcion = dTSegundoNivel.Rows[x][1].ToString();
                String Serie = dTSegundoNivel.Rows[x][2].ToString();
                String FechaNecesaria = dTSegundoNivel.Rows[x][6].ToString();
                String FechaPrevista = dTSegundoNivel.Rows[x][7].ToString();
                String Euro = dTSegundoNivel.Rows[x][3].ToString();
                String EuroKilo = dTSegundoNivel.Rows[x][4].ToString();
                String Kilos = dTSegundoNivel.Rows[x][5].ToString();

                TreeGridNode nodoHijo = nodo.Nodes.Add(Pedido, Cliente, Tarea, FechaFin, Linea, Descripcion, Serie, FechaNecesaria, FechaPrevista, Euro, EuroKilo, Kilos);
                
            }

        }

        private void tgFaltanDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == 7) {
                tgFaltanDatos.ClearSelection(); 

                //Inicio seleccionando celda a celda
                if (!String.IsNullOrEmpty(tgFaltanDatos.Rows[e.RowIndex].Cells[4].Value.ToString()) && (tgFaltanDatos.Rows[e.RowIndex].Cells[7].Style.BackColor.Equals(Color.NavajoWhite)))
                {

                    if (String.IsNullOrEmpty(tgFaltanDatos.Rows[e.RowIndex].Cells[7].Value.ToString()))
                    {
                        tgFaltanDatos.Rows[e.RowIndex].Cells[7].Value = fecha.Text.ToString();
                    }
                    else {
                        tgFaltanDatos.Rows[e.RowIndex].Cells[7].Value = "";
                    }

                } //Fin Seleccionar celda a celda
                else if (!String.IsNullOrEmpty(tgFaltanDatos.Rows[e.RowIndex].Cells[2].Value.ToString())) {

                    foreach (TreeGridNode nodo in tgFaltanDatos.Nodes)
                    {
                        foreach (TreeGridNode nodod in nodo.Nodes)
                        {
                            //if comprobar tarea
                            if (nodod.Cells[0].Value.ToString().Equals(tgFaltanDatos.Rows[e.RowIndex].Cells[0].Value.ToString()) && nodod.Cells[1].Value.ToString().Equals(tgFaltanDatos.Rows[e.RowIndex].Cells[1].Value.ToString()) && nodod.Cells[2].Value.ToString().Equals(tgFaltanDatos.Rows[e.RowIndex].Cells[2].Value.ToString()))
                            {
                                foreach (TreeGridNode nodof in nodod.Nodes)
                                {
                                    if (nodof.Cells[7].Style.BackColor.Equals(Color.NavajoWhite))
                                    {
                                        if (String.IsNullOrEmpty(nodof.Cells[7].Value.ToString()))
                                        {
                                            nodof.Cells[7].Value = fecha.Text.ToString();
                                        }
                                        else
                                        {
                                            nodof.Cells[7].Value = "";
                                        }
                                    }
                                    
                                }

                                 nodod.Expand();
                            }
                        }
                    }


                }
                //Fin Seleccionar celda a celda 
            }

            if (e.ColumnIndex == 8)
            {
                tgFaltanDatos.ClearSelection(); 

                //Inicio seleccionando celda a celda
                if (!String.IsNullOrEmpty(tgFaltanDatos.Rows[e.RowIndex].Cells[4].Value.ToString()) && (tgFaltanDatos.Rows[e.RowIndex].Cells[8].Style.BackColor.Equals(Color.NavajoWhite)))
                {

                    if (String.IsNullOrEmpty(tgFaltanDatos.Rows[e.RowIndex].Cells[8].Value.ToString()))
                    {
                        tgFaltanDatos.Rows[e.RowIndex].Cells[8].Value = fecha.Text.ToString();
                    }
                    else
                    {
                        tgFaltanDatos.Rows[e.RowIndex].Cells[8].Value = "";
                    }

                } //Fin Seleccionar celda a celda
                else if (!String.IsNullOrEmpty(tgFaltanDatos.Rows[e.RowIndex].Cells[2].Value.ToString()))
                {

                    foreach (TreeGridNode nodo in tgFaltanDatos.Nodes)
                    {
                        foreach (TreeGridNode nodod in nodo.Nodes)
                        {
                            //if comprobar tarea
                            if (nodod.Cells[0].Value.ToString().Equals(tgFaltanDatos.Rows[e.RowIndex].Cells[0].Value.ToString()) && nodod.Cells[1].Value.ToString().Equals(tgFaltanDatos.Rows[e.RowIndex].Cells[1].Value.ToString()) && nodod.Cells[2].Value.ToString().Equals(tgFaltanDatos.Rows[e.RowIndex].Cells[2].Value.ToString()))
                            {
                                foreach (TreeGridNode nodof in nodod.Nodes)
                                {
                                    if (nodof.Cells[8].Style.BackColor.Equals(Color.NavajoWhite))
                                    {
                                        if (String.IsNullOrEmpty(nodof.Cells[8].Value.ToString()))
                                        {
                                            nodof.Cells[8].Value = fecha.Text.ToString();
                                        }
                                        else
                                        {
                                            nodof.Cells[8].Value = "";
                                        }
                                    }

                                }

                                nodod.Expand();
                            }
                        }
                    }


                }
                //Fin Seleccionar celda a celda 
            }
        }

        private void tgFaltanDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                tgFaltanDatos.ClearSelection(); 
               
                //Inicio seleccionando celda a celda
                if (!String.IsNullOrEmpty(tgFaltanDatos.Rows[e.RowIndex].Cells[4].Value.ToString()) && (tgFaltanDatos.Rows[e.RowIndex].Cells[7].Style.BackColor.Equals(ColorTranslator.FromHtml("#ADEBC3"))))
                {
                        tgFaltanDatos.Rows[e.RowIndex].Cells[7].Value = fecha.Text.ToString();
                        tgFaltanDatos.Rows[e.RowIndex].Cells[7].Style.BackColor = Color.NavajoWhite;
                }

            }

            if (e.ColumnIndex == 8)
            {
                tgFaltanDatos.ClearSelection(); 

                //Inicio seleccionando celda a celda
                if (!String.IsNullOrEmpty(tgFaltanDatos.Rows[e.RowIndex].Cells[4].Value.ToString()) && (tgFaltanDatos.Rows[e.RowIndex].Cells[8].Style.BackColor.Equals(ColorTranslator.FromHtml("#ADEBC3"))))
                {
                    tgFaltanDatos.Rows[e.RowIndex].Cells[8].Value = fecha.Text.ToString();
                    tgFaltanDatos.Rows[e.RowIndex].Cells[8].Style.BackColor = Color.NavajoWhite;
                }

            }

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            //FORM LOAD
            cargarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
         

            foreach (TreeGridNode nodo in tgFaltanDatos.Nodes)
            {
                foreach (TreeGridNode nodod in nodo.Nodes)
                {
                        foreach (TreeGridNode nodof in nodod.Nodes)
                        {
                            //Columna Fecha Necesaria
                            if (nodof.Cells[7].Style.BackColor.Equals(Color.NavajoWhite))
                            {

                                //DataTable dTNecesaria = fbd.obtenerInfoActualizar(nodod.Cells[0].Value.ToString(), nodod.Cells[1].Value.ToString(), nodod.Cells[2].Value.ToString(), nodof.Cells[4].Value.ToString());
                                //Existe FechaNecesaria
                                string fechaNecesaria = fbd.obtenerFechaNecesaria(nodod.Cells[0].Value.ToString(), nodod.Cells[1].Value.ToString(), nodod.Cells[2].Value.ToString(), nodof.Cells[4].Value.ToString());

                                if (!fechaNecesaria.Equals(nodof.Cells[7].Value.ToString())) {
                                    DataTable dTNecesaria = fbd.obtenerInfoActualizar(nodod.Cells[0].Value.ToString(), nodod.Cells[1].Value.ToString(), nodod.Cells[2].Value.ToString(), nodof.Cells[4].Value.ToString());
                                   
                                    if (String.IsNullOrEmpty(nodof.Cells[7].Value.ToString()))
                                    {
                                        fbd.actualizarNullFechaNecesaria(dTNecesaria.Rows[0][0].ToString(), dTNecesaria.Rows[0][1].ToString());
                                    }
                                    else {
                                        fbd.actualizarFechaNecesaria(dTNecesaria.Rows[0][0].ToString(), dTNecesaria.Rows[0][1].ToString(), nodof.Cells[7].Value.ToString());
                                    }
                                }

                                //if (fbd.existeFechaNecesaria(nodod.Cells[0].Value.ToString(), nodod.Cells[1].Value.ToString(), nodod.Cells[2].Value.ToString(), nodof.Cells[4].Value.ToString()))
                                //{
                                //}
                            }

                            //Columna Fecha Prevista
                            if (nodof.Cells[8].Style.BackColor.Equals(Color.NavajoWhite))
                            {

                                string fechaPrevista = fbd.obtenerFechaPrevista(nodod.Cells[0].Value.ToString(), nodod.Cells[1].Value.ToString(), nodod.Cells[2].Value.ToString(), nodof.Cells[4].Value.ToString());

                                if (!fechaPrevista.Equals(nodof.Cells[8].Value.ToString()))
                                {
                                    DataTable dTPrevista = fbd.obtenerInfoActualizar(nodod.Cells[0].Value.ToString(), nodod.Cells[1].Value.ToString(), nodod.Cells[2].Value.ToString(), nodof.Cells[4].Value.ToString());

                                    if (String.IsNullOrEmpty(nodof.Cells[8].Value.ToString()))
                                    {
                                        fbd.actualizarNullFechaPrevista(dTPrevista.Rows[0][0].ToString(), dTPrevista.Rows[0][1].ToString());
                                    }
                                    else
                                    {
                                        fbd.actualizarFechaPrevista(dTPrevista.Rows[0][0].ToString(), dTPrevista.Rows[0][1].ToString(), nodof.Cells[8].Value.ToString());
                                    }

                                }
                                
                                //Existe fechaPrevista
                                //if (fbd.existeFechaPrevista(nodod.Cells[0].Value.ToString(), nodod.Cells[1].Value.ToString(), nodod.Cells[2].Value.ToString(), nodof.Cells[4].Value.ToString()))
                                //{
                                //}
                            }

                        }
                }
            }

            cargarFormulario();
            Cursor.Current = Cursors.Default;
        
        }

        private void tgFaltanDatos_NodeExpanded(object sender, ExpandedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Current = Cursors.Default;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Está seguro que desea salir de la aplicación?", "Aviso", MessageBoxButtons.YesNo);
            if (mensaje != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else {
                if (tgFaltanDatos.Rows.Count > 0) { 
                fbd.vaciarTabla();
                }
            }
        }




    }
}
