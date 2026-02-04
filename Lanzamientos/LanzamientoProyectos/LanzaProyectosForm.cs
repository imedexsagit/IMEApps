using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AdvancedDataGridView;
using LanzamientoProyectos.BD;
using LanzamientoProyectos.Interfaces;
using LanzamientoProyectos.Modelo;
using empresaGlobalProj;


namespace LanzamientoProyectos
{
    public partial class LanzaProyectosForm : Form, ILanzaProyectosForm
    {
        private DataTable dT; //DataTAble que almacena la lista de almacen del codigo seleccionado
        private LanzaProyectosControl control;
        private int numProyectoCargado = -1;
        private FiltroPedido filtroPedidoActual;


        public LanzaProyectosForm()
        {
            InitializeComponent();
            empresaGlobal.showEmp = false;
            dtpHorizonte.Value = DateTime.Now.AddYears(1);
            cbTipoPedido.SelectedIndex = 0;
            DesactivarBotonesSeleccionarProyecto();
            nUDCantidadLinea.Enabled = false;

        }

        #region Implementación Interfaz

        public void SetControlador(LanzaProyectosControl controlEt)
        {
            control = controlEt;
        }

        public void CargarPedidos(DataTable cargarPedidos)
        {
            //this.pedidosBS.DataMember = dataMember ;
            cbPedido.BeginUpdate();
            DataRow row = cargarPedidos.NewRow();
            row["NUMPED"] = -1;
            row["NUMPEDS"] = "";
            cargarPedidos.Rows.InsertAt(row, 0);

            cbPedido.DisplayMember = cargarPedidos.Columns["NUMPEDS"].ToString();
            cbPedido.ValueMember = cargarPedidos.Columns["NUMPED"].ToString();
            cbPedido.DataSource = cargarPedidos;

            cbPedido.SelectedItem = null;
            cbPedido.EndUpdate();
        }

        public void ProyectosCargados()
        {
            ActivarBotonesSeleccionarProyecto();
        }




        public void CargarProyectos(DataTable proyectos)
        {
            cbProyecto.BeginUpdate();
            DataRow row = proyectos.NewRow();
            row["CODLANZA"] = -1;
            row["CODLANZAS"] = "";
            proyectos.Rows.InsertAt(row, 0);

            cbProyecto.DisplayMember = proyectos.Columns["CODLANZAS"].ToString();
            cbProyecto.ValueMember = proyectos.Columns["CODLANZA"].ToString();
            cbProyecto.DataSource = proyectos;
            cbProyecto.SelectedItem = null;
            cbProyecto.EndUpdate();
        }

        public void CargarFamilias(DataTable familias)
        {
            cbFamilia.BeginUpdate();
            DataRow row = familias.NewRow();
            row["FAMILIA"] = "";
            row["FAMILIAS"] = "";
            familias.Rows.InsertAt(row, 0);

            cbFamilia.DisplayMember = familias.Columns["FAMILIAS"].ToString();
            cbFamilia.ValueMember = familias.Columns["FAMILIA"].ToString();

            cbFamilia.DataSource = familias;

            cbFamilia.SelectedItem = null;
            cbFamilia.EndUpdate();
            //cbFamilia.SelectedItem = -1;
        }


        public void CargarLineasProyecto(DataTable lineasProyecto)
        {
            dgvLineasProyecto.DataSource = lineasProyecto;

            foreach (DataGridViewColumn column in dgvLineasProyecto.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        public void CargarLineasNecesidad(DataTable lineasNecesidad)
        {
            dgvNecesidadesProyecto.DataSource = lineasNecesidad;
        }

        public void ActivarValidacion()
        {
            btnValidar.Enabled = true;
        }

        public void DesactivarValidacion()
        {
            btnValidar.Enabled = false;
        }

        public TreeGridNode AddNodoPrincipalArbol(LineaPedido lineaPedido)
        {
            var node= tgvArbolPedidos.Nodes.Add(lineaPedido.Codigo, lineaPedido.Denominacion, lineaPedido.NumPedido, lineaPedido.Linea, lineaPedido.Necesarias, lineaPedido.Lanzadas, lineaPedido.Proyecto, lineaPedido.TipoPedido);
            
            return node;
        }

        public void AddColorNodo(TreeGridNode nodo, LineaPedido lineaPedido)
        {
            nodo.DefaultCellStyle.BackColor = lineaPedido.GetColorNodo();
        }

        

        public TreeGridNode AddNodoHijo(TreeGridNode nodo, LineaPedido lineaPedido)
        {
            
            //var nodoHijo = tgvArbolPedidos.Nodes[nodo.Index].Nodes.Add(lineaPedido.Codigo, lineaPedido.Denominacion, lineaPedido.NumPedido, lineaPedido.Linea, lineaPedido.Necesarias, lineaPedido.Lanzadas,
            //    lineaPedido.Proyecto, lineaPedido.TipoPedido);
            var nodoHijo= nodo.Nodes.Add(lineaPedido.Codigo, lineaPedido.Denominacion, lineaPedido.NumPedido, lineaPedido.Linea, lineaPedido.Necesarias, lineaPedido.Lanzadas, lineaPedido.Proyecto, lineaPedido.TipoPedido);
            //var index = nodoHijo.Index;
            //var level = nodoHijo.Level;
            //nodoHijo.Expand();
            //nodoHijo.Collapse();
            return nodoHijo;
        }

        #endregion

        #region Eventos Click

        private void btnAddLineaProyecto_Click(object sender, EventArgs e)
        {
            bool actualizar = false;
            
            if (numProyectoCargado > -1)
            {
                
                foreach (TreeGridNode n in tgvArbolPedidos.SelectedRows)
                {
                    string mensajeError = "";
                    var codigoPadre = n.Level > 1 ? n.Parent.Cells["Nodo"].Value.ToString() : "";
                    if (!control.AddLineaProyecto(numProyectoCargado, tbCodigoProyecto.Text, codigoPadre, n.Cells["Nodo"].Value.ToString(), Convert.ToInt32(n.Cells["NumPedido"].Value),
                                    Convert.ToInt32(n.Cells["LineaPedido"].Value), nUDCantidadLinea.Enabled, (int)nUDCantidadLinea.Value, out mensajeError))
                    {
                        MessageBox.Show(Form.ActiveForm, mensajeError, @"Añadir código", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else {
                        actualizar = true;
                    }
                    //Color slateBlue = Color.FromName("LightGray");
                   // RecorrerHijos(n, slateBlue, false);
                }

                if (actualizar)
                {
                    if (filtroPedidoActual != null)
                    {
                        LimpiarArbol();
                        CargarLineasPedidos(filtroPedidoActual.NumPedido, filtroPedidoActual.Familia, filtroPedidoActual.TipoReg, filtroPedidoActual.Horizonte);
                    }
                }
                nUDCantidadLinea.Value = 0;
            }
        }

        private void btnRemoveLineaProyecto_Click(object sender, EventArgs e)
        {
            List<int> indicesRowsSeleccionadas = new List<int>();
            foreach (DataGridViewCell selected in dgvLineasProyecto.SelectedCells)
            {
                if (!indicesRowsSeleccionadas.Contains(selected.RowIndex))
                    indicesRowsSeleccionadas.Add(selected.RowIndex);
            }


            foreach (int indiceRow in indicesRowsSeleccionadas)
            {
                control.EliminarLineaProyecto(numProyectoCargado, Convert.ToInt32(dgvLineasProyecto.Rows[indiceRow].Cells["LINEAPRO"].Value));
            }
            control.CargarProyectoSeleccionado(numProyectoCargado);

            if (filtroPedidoActual != null)
            {
                LimpiarArbol();
                CargarLineasPedidos(filtroPedidoActual.NumPedido, filtroPedidoActual.Familia, filtroPedidoActual.TipoReg, filtroPedidoActual.Horizonte);
            }
        }

        private void btnObtenerArbol_Click(object sender, EventArgs e)
        {
            //if (cbTipoPedido.SelectedValue == null && cbPedido.SelectedValue == null && cbFamilia.SelectedValue == null)
            if ((cbTipoPedido.SelectedValue == null || cbPedido.Text == "")  && cbPedido.Text == "" && (cbFamilia.SelectedValue == null || cbFamilia.Text == ""))
            {
                MessageBox.Show(Form.ActiveForm, @"Indique algún filtro", @"Busqueda pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            LimpiarArbol();
            // Obtenemos las líneas de pedido 


            int numPedido = -1;
            string familia = "";
            string tipoReg = "";
            //if (cbPedido.SelectedItem != null)
            //    numPedido = (int)cbPedido.SelectedValue;
            if (cbPedido.Text != "")
                numPedido = Convert.ToInt32(cbPedido.Text);

            else if (cbPedido.Text != "")
            {
                MessageBox.Show(Form.ActiveForm, @"No existen pedidos con el filtro especificado", @"Busqueda pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbFamilia.SelectedItem != null)
                familia = cbFamilia.SelectedValue.ToString();

            if (cbTipoPedido.SelectedItem != null)
                tipoReg = cbTipoPedido.SelectedItem.ToString();
            DateTime horizonte = dtpHorizonte.Value;
            filtroPedidoActual = new FiltroPedido(numPedido, familia, tipoReg, horizonte);
            if (!CargarLineasPedidos(numPedido, familia, tipoReg, horizonte))
            {
                MessageBox.Show(Form.ActiveForm, @"No existen lineas de pedido con el filtro especificado", @"Busqueda pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool CargarLineasPedidos(int numPedido, string familia, string tipoReg, DateTime horizonte)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<LineaPedido> lineasPedido = control.CargarLineasPedidos(numPedido, familia, tipoReg, horizonte);
            Cursor.Current = Cursors.Arrow;
            tgvArbolPedidos.ClearSelection();

            if (lineasPedido.Count == 0) return false;

            return true;
        }

        //private bool CargarLineasPedidos(int numPedido, string familia, string tipoReg, DateTime horizonte)
        //{
        //    LineaPedido[] lineasPedido = control.CargarLineasPedidos(numPedido, familia, tipoReg, horizonte);

        //    if (lineasPedido.Length == 0) return false;

        //    foreach (LineaPedido lineaP in lineasPedido)
        //    {
        //        var codigo = lineaP.Codigo;
        //        Cursor.Current = Cursors.WaitCursor;

        //        if (codigo.Length > 0)
        //        {
                  
        //            dT = control.GetListaAlmacen("3", codigo, "ES");
        //            if (dT.Rows.Count > 0)
        //                CrearArbol(lineaP.NumPedido, lineaP.Linea, lineaP.Necesarias, lineaP.TipoPedido);
        //        }
        //        else
        //            MessageBox.Show("El código elegido no tiene caracteres");

        //        //UseWaitCursor = false;
        //        Cursor.Current = Cursors.Arrow;
        //    }
        //    return true;
        //}

        private void btnBuscarProyectos_Click(object sender, EventArgs e)
        {
            int numPedido = -1;
            int numProyecto = -1;
            string familia = "";
            string tipoReg = "";

            //if (cbPedido.SelectedItem != null)
            //    numPedido = (int)cbPedido.SelectedValue;
            if (cbPedido.Text != "")
                numPedido = Convert.ToInt32(cbPedido.Text);
            else if (cbPedido.Text != "")
            {
                MessageBox.Show(Form.ActiveForm, @"No existen proyectos con el filtro especificado", @"Busqueda Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbFamilia.SelectedItem != null)
                familia = cbFamilia.SelectedValue.ToString();

            if (cbTipoPedido.SelectedItem != null)
                tipoReg = cbTipoPedido.SelectedItem.ToString();


            //if (cbProyecto.SelectedItem != null)
            //    numProyecto = (int)cbProyecto.SelectedValue;
            if (cbProyecto.Text != "")
                numProyecto = Convert.ToInt32(cbProyecto.Text);
            

            else if (cbProyecto.Text != "")
            {
                MessageBox.Show(Form.ActiveForm, @"No existen proyectos con el filtro especificado", @"Busqueda Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string codigo = tbCodigoProyecto.Text;
            ggDataSet.TC_LANZARow row = control.SeleccionarProyecto(numProyecto, numPedido, familia, tipoReg, codigo);

            if (row != null)
            {
                RellenarCamposFiltro(row);
            }
            else
            {
                MessageBox.Show(Form.ActiveForm, @"No existen proyectos con el filtro especificado", @"Busqueda Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //calcularNecesidades
        private void btnCalcularNecesidades_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnStop.Enabled = true;
            
            //TOMÁS 25/04/2019
            /*if (control.CalcularNecesidades())
            {
                MessageBox.Show(Form.ActiveForm, "Necesidad calculada");

            }
            btnStop.Enabled = false;
            Cursor.Current = Cursors.Arrow*/

            if (control.CalcularNecesidades())
            {
                if (ComprobarRevisar())
                {
                    btnValidar.Enabled = false;
                    MessageBox.Show(Form.ActiveForm, "Hay marcas que no tienen ruta","Información",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else
                {
                    btnValidar.Enabled = true;
                    MessageBox.Show(Form.ActiveForm, "Necesidad calculada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            btnStop.Enabled = false;
            Cursor.Current = Cursors.Arrow;            
        }


        //TOMÁS 25/04/2019
        private bool ComprobarRevisar() 
        {
            bool revisar = false;
            
            foreach (DataGridViewRow row in dgvNecesidadesProyecto.Rows)
            {
                if (row.Cells["Revisar"].Value.ToString() == "Si")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    revisar = true;
                }
            }
            return revisar;
        }



        //Validar
        private void btnLanzar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (control.ValidarProyecto())
            {
                MessageBox.Show(Form.ActiveForm, "Proyecto Lanzado");
                MarcarProyectoValidado();
            }
            else
            {
                MessageBox.Show(Form.ActiveForm, @"No se puede validar un proyecto que no tiene necesidades", @"Validar Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor.Current = Cursors.Arrow;
        }

        //Invalidar
        private void btnBorrarLanzamiento_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (control.BorrarLanzamiento())
            {
                MessageBox.Show(Form.ActiveForm, "Proyecto Invalidado");

                DesmarcarProyectoValidado();
            }
            else
            {
                //MessageBox.Show(@"No se puede invalidar un proyecto que no esta validado", @"Invalidar Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(Form.ActiveForm, @"No se puede invalidar el proyecto ya que tiene etiquetas y/o bonos", @"Invalidar Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor.Current = Cursors.Arrow;
        }



        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
            DesmarcarProyectoValidado();
            if (filtroPedidoActual != null)
                filtroPedidoActual.LimpiarPedido();
        }


        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            ggDataSet.TC_LANZARow row = control.SeleccionarProyectoSiguiente();
            if (row != null)
                RellenarCamposFiltro(row);
            else
            {
                MessageBox.Show(Form.ActiveForm, @"Se ecuentra en el ultimo proyecto", @"Busqueda Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            ggDataSet.TC_LANZARow row = control.SeleccionarProyectoAnterior();
            if (row != null)
                RellenarCamposFiltro(row);
            else
            {
                MessageBox.Show(Form.ActiveForm, @"Se ecuentra en el primer proyecto", @"Busqueda Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            ggDataSet.TC_LANZARow row = control.SeleccionarProyectoUltimo();
            if (row != null)
                RellenarCamposFiltro(row);
            else
            {
                MessageBox.Show(Form.ActiveForm, @"Se ecuentra en el último proyecto", @"Busqueda Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            ggDataSet.TC_LANZARow row = control.SeleccionarProyectoPrimero();
            if (row != null)
                RellenarCamposFiltro(row);
            else
            {
                MessageBox.Show(Form.ActiveForm, @"Se ecuentra en el primer proyecto", @"Busqueda Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddCodigoManual_Click(object sender, EventArgs e)
        {
            if (numProyectoCargado <= 0)
            {
                MessageBox.Show(Form.ActiveForm, @"Seleccione un proyecto al que añadir el código.", @"Añadir código manual", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(tbCodigoManual.Text) || nudCantidad.Value == 0)
            {
                MessageBox.Show(Form.ActiveForm, @"Seleccione un articulo válido y la cantidad a añadir.", @"Añadir código manual", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            control.InsertarLineaProyecto(numProyectoCargado, tbCodigoProyecto.Text,  "MA", 0,
                    0, Convert.ToInt32(nudCantidad.Value), tbCodigoManual.Text, "");

            /*if (filtroPedidoActual != null)
            {
                LimpiarArbol();
                CargarLineasPedidos(filtroPedidoActual.NumPedido, filtroPedidoActual.Familia, filtroPedidoActual.TipoReg, filtroPedidoActual.Horizonte);
            }*/
            tbCodigoManual.Select();
        }





        private void btnNuevoProyecto_Click(object sender, EventArgs e)
        {
            //if (cbProyecto.SelectedIndex > 0)
            //{
            //    LimpiarFiltros();
            //}
            if (cbProyecto.Text != "")
            {
                LimpiarFiltros();
            }
            if (string.IsNullOrEmpty(tbCodigoProyecto.Text))
            {
                MessageBox.Show(Form.ActiveForm, @"Debe rellenar el código de proyecto", @"Nuevo Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            int numPedido = -1;
            string familia = "";
            string tipoReg = "";

            //if (cbPedido.SelectedItem != null)
            //    numPedido = (int)cbPedido.SelectedValue;
            if (cbPedido.Text != "")
                numPedido = Convert.ToInt32(cbPedido.Text);

            if (cbFamilia.SelectedItem != null)
                familia = cbFamilia.SelectedValue.ToString();

            if (cbTipoPedido.SelectedItem != null)
                tipoReg = cbTipoPedido.SelectedItem.ToString();


            string codigo = tbCodigoProyecto.Text;
            numProyectoCargado = control.CrearProyecto(codigo, tipoReg, familia, numPedido, dtpHorizonte.Value);
            //cbProyecto.SelectedIndex = cbProyecto.FindStringExact(numProyectoCargado.ToString());
            cbProyecto.Text = numProyectoCargado.ToString();
            btnBuscarProyectos_Click(sender, e);
            MessageBox.Show(Form.ActiveForm, @"El proyecto " + numProyectoCargado + @" se ha creado correctamente", @"Nuevo Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnNuevoProyectoExpedicion_Click(object sender, EventArgs e)
        {
            if (cbProyecto.Text != "")
            {
                LimpiarFiltros();
            }
            if (string.IsNullOrEmpty(tbCodigoProyecto.Text))
            {
                MessageBox.Show(Form.ActiveForm, @"Debe rellenar el código de proyecto", @"Nuevo Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            int numPedido = -1;
            string familia = "";
            string tipoReg = "";
            string codigo = tbCodigoProyecto.Text;

            if (cbPedido.Text != "")
            {
                numPedido = Convert.ToInt32(cbPedido.Text);
            }
            else
            {
                MessageBox.Show(Form.ActiveForm, @"Debe rellenar el pedido para el que desea generar su proyecto de expedicion", "Nuevo Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (cbFamilia.SelectedItem != null)
                familia = cbFamilia.SelectedValue.ToString();

            if (cbTipoPedido.SelectedItem != null)
                tipoReg = cbTipoPedido.SelectedItem.ToString();

            
            numProyectoCargado = control.CrearProyectoExpedicion(codigo, "CF", familia, numPedido, dtpHorizonte.Value);            
            cbProyecto.Text = numProyectoCargado.ToString();

            control.InsertarLineaProyecto(numProyectoCargado, codigo, "CF", numPedido, 0, 1, "Almacen", "");


            btnBuscarProyectos_Click(sender, e);
            MessageBox.Show(Form.ActiveForm, @"El proyecto " + numProyectoCargado + @" se ha creado correctamente", @"Nuevo Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }






        private void btnGuardarProyecto_Click(object sender, EventArgs e)
        {
            int numPedido = -1;
            int numProyecto;
            string familia = "";
            string tipoReg = "";
            //if (cbProyecto.SelectedItem != null)
            //    numProyecto = (int)cbProyecto.SelectedValue;
            if (cbProyecto.Text != "")
                numProyecto = Convert.ToInt32(cbProyecto.Text);

            else
            {
                MessageBox.Show(Form.ActiveForm, @"No hay ningun proyecto seleccionado", @"Nuevo Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string codigo = tbCodigoProyecto.Text;

            //if (cbPedido.SelectedItem != null)
            //    numPedido = (int)cbPedido.SelectedValue;            
            if (cbPedido.Text != "")
                numPedido = Convert.ToInt32(cbPedido.Text);

            if (cbFamilia.SelectedItem != null)
                familia = cbFamilia.SelectedValue.ToString();

            if (cbTipoPedido.SelectedItem != null)
                tipoReg = cbTipoPedido.SelectedItem.ToString();
            DateTime horizonte = dtpHorizonte.Value;

            control.GuardarDatosProyecto(numProyecto, codigo, numPedido, familia, tipoReg, horizonte);
            MessageBox.Show(Form.ActiveForm, @"Detalles del proyecto guardados correctamente.", @"Nuevo Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }



        #endregion

        #region Resto Eventos
        private void nudCantidad_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCodigoManual.Text) || !control.ExisteCodigoManual(tbCodigoManual.Text))
            {
                MessageBox.Show(Form.ActiveForm, @"Debe introducir un código válido.", @"Añadir código manual", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbCodigoManual.Select();
                return;
            }
            nudCantidad.Select(0, nudCantidad.Text.Length);
        }

        private void dgvNecesidadesProyecto_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (lblValidado.Visible)
                e.Cancel = true;
            else
            {

                if (dgvNecesidadesProyecto.Columns[e.ColumnIndex].Name != "CANTLANZA")
                {
                    e.Cancel = true;
                }
            }
        }

        private void dgvNecesidadesProyecto_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string codigo = dgvNecesidadesProyecto.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString();
            int cantidad = Convert.ToInt32(dgvNecesidadesProyecto.Rows[e.RowIndex].Cells["CANTLANZA"].Value);
            control.ActualizarCantidadCodigoNecesidades(numProyectoCargado, codigo, cantidad);
        }

        private void tbCodigoManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                nudCantidad.Select();
            }
        }

        private void nudCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAddCodigoManual.Select();
            }
        }



        private void tbCodigoManual_Enter(object sender, EventArgs e)
        {
            tbCodigoManual.Select(0, tbCodigoManual.Text.Length);
        }

        private void tgvArbolPedidos_SelectionChanged(object sender, EventArgs e)
        {
            nUDCantidadLinea.Enabled = tgvArbolPedidos.SelectedRows.Count == 1;
        }
        #endregion

        #region Funciones Auxiliares

        private void LimpiarFiltros()
        {
            tgvArbolPedidos.Nodes.Clear();
            tgvArbolPedidos.Rows.Clear();
            BorrarLanzamientos();
            cbProyecto.Enabled = true;
            //cbPedido.SelectedIndex = 0;
            cbPedido.Text = "";
            cbFamilia.SelectedIndex = 0;
            cbTipoPedido.SelectedIndex = 0;
            //cbProyecto.SelectedIndex = 0;
            cbProyecto.Text = "";
            tbCodigoProyecto.Text = "";
            dtpHorizonte.Value = DateTime.Now.AddYears(1);
            control.BorrarTablaProyectos();
            DesactivarBotonesSeleccionarProyecto();
            DesactivarValidacion();
            tbCodigoManual.Text = "";
            nudCantidad.Value = 0;
        }

        //private void RecorrerHijos(TreeGridNode n, Color color, bool s)
        //{
        //    bool s1 = s;
        //    bool parcialmenteLanzado = false;
        //    bool ascendenteLanzado = false;

        //    if (n.Selected || s)
        //    {
               
        //        if (n.Selected)
        //        {
        //            ascendenteLanzado = EstaAscendenteLanzado(n);
        //            if (Convert.ToInt32(n.Cells["Lanzadas"].Value) < Convert.ToInt32(n.Cells["Necesarias"].Value) && !ascendenteLanzado)
        //            {
        //                var cantidad = Convert.ToInt32(n.Cells["Necesarias"].Value);
        //                if (nUDCantidadLinea.Enabled && nUDCantidadLinea.Value>0)
        //                {
        //                    cantidad = (int) nUDCantidadLinea.Value;
        //                }

        //                if (cantidad + Convert.ToInt32(n.Cells["Lanzadas"].Value) <= Convert.ToInt32(n.Cells["Necesarias"].Value) )
        //                {
        //                    parcialmenteLanzado = EstaParcialmenteLanzado(n, parcialmenteLanzado);
        //                    if (!parcialmenteLanzado)
        //                        control.InsertarLineaProyecto(numProyectoCargado, tbCodigoProyecto.Text, n.Cells["TipoReg"].Value.ToString(), Convert.ToInt32(n.Cells["NumPedido"].Value),
        //                            Convert.ToInt32(n.Cells["LineaPedido"].Value), cantidad, n.Cells["Nodo"].Value.ToString(),
        //                            n.Level > 1 ? n.Parent.Cells["Nodo"].Value.ToString() : "");
        //                    else
        //                    {
        //                        MessageBox.Show(@"Este codigo  ha sido lanzado parcialmente", @"Añadir código", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show(@"Se esta intentando lanzar mas marcas de las necesarias.", @"Añadir código", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show(@"Este codigo ya ha sido lanzado en su totalidad", @"Añadir código", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
                  
        //        }
        //        //Color de añadido.
        //        foreach (DataGridViewCell cell in n.Cells)
        //        {
        //            cell.Style.BackColor = color;
        //        }
        //        s1 = true;
        //    }

        //    foreach (TreeGridNode tn in n.Nodes)
        //    {
        //        RecorrerHijos(tn, color, s1);
        //    }

            //CargarLineasPedidos(numPedido, familia, tipoReg, horizonte);
        //}

        //private bool EstaAscendenteLanzado(TreeGridNode n)
        //{
        //    if (n.Parent != null && n.Level>1)
        //    {
        //        var lanzadas = Convert.ToInt32(n.Parent.Cells["Lanzadas"].Value);
        //        var necesarias = Convert.ToInt32(n.Parent.Cells["Necesarias"].Value);
        //        if (lanzadas >= necesarias)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return EstaAscendenteLanzado(n.Parent);
        //        }
        //    }
        //    return false;
        //}

        //private bool EstaParcialmenteLanzado(TreeGridNode n, bool parcialmenteLanzado)
        //{
        //    foreach (TreeGridNode node in n.Nodes)
        //    {
        //       parcialmenteLanzado= EstaParcialmenteLanzado(node, parcialmenteLanzado);
        //        if (parcialmenteLanzado) return true;
        //        if (Convert.ToInt32(node.Cells[5].Value) > 0) //Compruebo el campo lanzadas
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}


        private void RellenarCamposFiltro(ggDataSet.TC_LANZARow row)
        {
            //cbProyecto.SelectedIndex = cbProyecto.FindStringExact(row.CODLANZA.ToString());
            cbProyecto.Text = row.CODLANZA.ToString();
            tbCodigoProyecto.Text = row.PROYECTO;
            dtpHorizonte.Value = row.HORIZONTE.Date;
            cbProyecto.Enabled = false;
            numProyectoCargado = row.CODLANZA;
            if (row.VALIDADO == "S")
            {
                MarcarProyectoValidado();
            }
            else
            {
                DesmarcarProyectoValidado();
            }

            ConsultasBD consBD = new ConsultasBD();
            CargarPedidos(consBD.ObtenerPedidosProyecto(row.CODLANZA.ToString()));
        }

        private void DesmarcarProyectoValidado()
        {
            lblValidado.Visible = false;
            btnCalcularNecesidades.Enabled = true;

            btnBorrarLanzamiento.Enabled = false;
            btnAddCodigoManual.Enabled = true;
            btnAddLineaProyecto.Enabled = true;
            btnRemoveLineaProyecto.Enabled = true;
        }

        private void MarcarProyectoValidado()
        {
            lblValidado.Visible = true;
            btnCalcularNecesidades.Enabled = false;

            btnBorrarLanzamiento.Enabled = true;
            btnAddCodigoManual.Enabled = false;
            btnAddLineaProyecto.Enabled = false;
            btnRemoveLineaProyecto.Enabled = false;
        }

        private void BorrarLanzamientos()
        {
            dgvLineasProyecto.DataSource = null;
            dgvNecesidadesProyecto.DataSource = null;
        }

        private void DesactivarBotonesSeleccionarProyecto()
        {
            btnAnterior.Enabled = false;
            btnSiguiente.Enabled = false;
            btnPrimero.Enabled = false;
            btnUltimo.Enabled = false;
        }

        private void ActivarBotonesSeleccionarProyecto()
        {
            btnAnterior.Enabled = true;
            btnSiguiente.Enabled = true;
            btnPrimero.Enabled = true;
            btnUltimo.Enabled = true;
        }

        #endregion

        #region gestión del arbol

      
        private void LimpiarArbol()
        {
            if (tgvArbolPedidos.Nodes.Count > 0)
                tgvArbolPedidos.Nodes.Clear();
        }

        #endregion
      

        public bool Desglosar_Lineas()
        {
            return checkBox_desglose_lineas.Checked;
        }

        private void LanzaProyectosForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }

    
       
    }
}