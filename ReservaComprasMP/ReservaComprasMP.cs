using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using empresaGlobalProj;

namespace ReservaComprasMP
{
    public partial class ReservaComprasMP : Form
    {
        //Atributos
        public string empresa;
        public string usuario;
        private ReservaComprasMP_BD bd;
        private int flag_compras;
        private List<string> lista_pedidos_lineas_compras_seleccionadas;
        

        //Métodos
        public ReservaComprasMP()
        {                        
            InitializeComponent();
            empresaGlobal.showEmp = false;
            //JRM - Cambiar Empresa cambio "3"; por empresaGlobal.empresaID
            empresa = empresaGlobal.empresaID;//"3";
            usuario = Environment.UserName;
            bd = new ReservaComprasMP_BD();
            flag_compras = 0;
            lista_pedidos_lineas_compras_seleccionadas = new List<string>();

            CargarProveedores();
            CargarClientes();            

            //Líneas compra
            dataGridView_lineas_compra.Columns["PEDIDO"].Width = 50;
            dataGridView_lineas_compra.Columns["LÍNEA"].Width = 45;
            dataGridView_lineas_compra.Columns["CÓDIGO"].Width = 130;
            dataGridView_lineas_compra.Columns["DENOMINACIÓN"].Width = 193;
            dataGridView_lineas_compra.Columns["PEDIDA"].Width = 55;
            dataGridView_lineas_compra.Columns["SERVIDA"].Width = 60;
            dataGridView_lineas_compra.Columns["ENTREGA"].Width = 70;
            dataGridView_lineas_compra.Columns["ABIERTA"].Width = 60;
            dataGridView_lineas_compra.Columns["existe"].Visible = false;

            //Líneas venta
            dataGridView_lineas_venta.Columns["PEDIDO"].Width = 50;
            dataGridView_lineas_venta.Columns["LÍNEA"].Width = 44;
            dataGridView_lineas_venta.Columns["CÓDIGO"].Width = 130;
            dataGridView_lineas_venta.Columns["DENOMINACIÓN"].Width = 190;
            dataGridView_lineas_venta.Columns["PEDIDA"].Width = 54;
            dataGridView_lineas_venta.Columns["SERVIDA"].Width = 59;
            dataGridView_lineas_venta.Columns["ENTREGA"].Width = 69;
            dataGridView_lineas_venta.Columns["ABIERTA"].Width = 58;

            //Lotes
            dataGridView_lotes.Columns["PEDIDO"].Width = 50;
            dataGridView_lotes.Columns["LÍNEA"].Width = 45;                        
            dataGridView_lotes.Columns["ALB_PROV"].Width = 71;
            dataGridView_lotes.Columns["ALB_IRATI"].Width = 71;
            dataGridView_lotes.Columns["PAQUETE"].Width = 106;
            dataGridView_lotes.Columns["PESO_PREV"].Width = 76;
            dataGridView_lotes.Columns["LOTE"].Width = 51;
            dataGridView_lotes.Columns["SITUACIÓN"].Width = 81;
            dataGridView_lotes.Columns["BARRAS"].Width = 61;
            dataGridView_lotes.Columns["STOCK"].Width = 51;
            
            //Reservas
            dataGridView_reservas.Columns["id"].Visible = false;                        
            dataGridView_reservas.Columns["P_COMPRA"].Width = 70;
            dataGridView_reservas.Columns["L_COMPRA"].Width = 70;
            dataGridView_reservas.Columns["LOTE"].Width = 50;
            dataGridView_reservas.Columns["P_VENTA"].Width = 70;
            dataGridView_reservas.Columns["L_VENTA"].Width = 70;
            dataGridView_reservas.Columns["CÓDIGO"].Width = 130;
            dataGridView_reservas.Columns["DENOMINACIÓN"].Width = 190;
            dataGridView_reservas.Columns["PEDIDA"].Width = 54;
            dataGridView_reservas.Columns["ENTREGA"].Width = 69;
            dataGridView_reservas.Columns["RESERVA_POR"].Width = 95;
            dataGridView_reservas.Columns["FECHA"].Width = 100;
            dataGridView_reservas.Columns["existe"].Visible = false;            
        }
        


        public void CargarProveedores()
        {
            DataTable table;

            table = bd.ObtenerProveedoresClientes(empresa,"proveedor");

            DataRow row = table.NewRow();
            row["CODCLI"] = -1;
            row["NOMBRE"] = "";
            table.Rows.InsertAt(row, 0);

            comboBox_proveedor.ValueMember = table.Columns["CODCLI"].ToString();
            comboBox_proveedor.DisplayMember = table.Columns["NOMBRE"].ToString();            
            comboBox_proveedor.DataSource = table;                                    
        }



        public void CargarClientes()
        {
            DataTable table;

            table = bd.ObtenerProveedoresClientes(empresa,"cliente");

            DataRow row = table.NewRow();
            row["CODCLI"] = -1;
            row["NOMBRE"] = "";
            table.Rows.InsertAt(row, 0);

            comboBox_cliente.ValueMember = table.Columns["CODCLI"].ToString();
            comboBox_cliente.DisplayMember = table.Columns["NOMBRE"].ToString();
            comboBox_cliente.DataSource = table;
        }


    
        public void CargarMPs(string proveedor)
        {
            DataTable table;

            table = bd.ObtenerMPs(empresa, proveedor, checkBox_lineas_cerradas_compra.Checked);

            DataRow row = table.NewRow();            
            row["CODIGO"] = "";
            table.Rows.InsertAt(row, 0);

            comboBox_mp.ValueMember = table.Columns["CODIGO"].ToString();
            comboBox_mp.DisplayMember = table.Columns["CODIGO"].ToString();
            comboBox_mp.DataSource = table;
        } 



        private void comboBox_proveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            
            DataTable table;

            table = bd.ObtenerPedidosProveedorCliente(empresa, "proveedor", comboBox_proveedor.SelectedValue.ToString(), checkBox_lineas_cerradas_compra.Checked);

            DataRow row = table.NewRow();
            row["NUMPED"] = "";
            table.Rows.InsertAt(row, 0);

            comboBox_pedido_compra.ValueMember = table.Columns["NUMPED"].ToString();
            comboBox_pedido_compra.DisplayMember = table.Columns["NUMPED"].ToString();
            comboBox_pedido_compra.DataSource = table;

            CargarMPs(comboBox_proveedor.SelectedValue.ToString());

            if (comboBox_pedido_venta.SelectedValue != null)
            {
                string pedido_venta = comboBox_pedido_venta.SelectedValue.ToString();
                if (pedido_venta != "")
                {
                    dataGridView_lineas_venta.DataSource = bd.ObtenerLineasPedido(empresa, "cliente", pedido_venta, checkBox_lineas_cerradas_venta.Checked, lista_pedidos_lineas_compras_seleccionadas,"");
                    dataGridView_lineas_venta.ClearSelection();
                }
            }

            this.Cursor = Cursors.Arrow;
        }



        private void comboBox_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            
            DataTable table;

            table = bd.ObtenerPedidosProveedorCliente(empresa, "cliente", comboBox_cliente.SelectedValue.ToString(),checkBox_lineas_cerradas_venta.Checked);

            DataRow row = table.NewRow();
            row["NUMPED"] = "";
            table.Rows.InsertAt(row, 0);

            comboBox_pedido_venta.ValueMember = table.Columns["NUMPED"].ToString();
            comboBox_pedido_venta.DisplayMember = table.Columns["NUMPED"].ToString();
            comboBox_pedido_venta.DataSource = table;

            this.Cursor = Cursors.Arrow;
        }


                     
        private void comboBox_pedido_compras_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pedido,mp;

            flag_compras = 1;

            pedido = comboBox_pedido_compra.SelectedValue.ToString();           
            if (pedido == "")
                pedido = "-1";

            if (comboBox_mp.SelectedValue == null)
                mp = "";
            else
                mp = comboBox_mp.SelectedValue.ToString();
            
            dataGridView_lineas_compra.DataSource = bd.ObtenerLineasPedido(empresa, "proveedor", pedido, checkBox_lineas_cerradas_compra.Checked, lista_pedidos_lineas_compras_seleccionadas,mp);
            dataGridView_lineas_compra.ClearSelection();
            lista_pedidos_lineas_compras_seleccionadas.Clear();
            dataGridView_lotes.DataSource = bd.ObtenerLotesLineaPedido(empresa, lista_pedidos_lineas_compras_seleccionadas);
            Cargar_Reservas();

            //Mostrar diferencias
            if ((comboBox_pedido_compra.SelectedValue.ToString() != "") && (comboBox_mp.SelectedValue == null || comboBox_mp.SelectedValue.ToString() == ""))           
                Marcar_Lineas_Compra_Nuevas_Modificadas_Eliminadas();                         
        }



        private void comboBox_mp_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_pedido_compras_SelectedIndexChanged(sender, e);
        }



        private void comboBox_pedido_venta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pedido;

            pedido = comboBox_pedido_venta.SelectedValue.ToString();
            if (pedido == "")
            {
                dataGridView_lineas_venta.DataSource = bd.ObtenerLineasPedido(empresa, "cliente", "-1", checkBox_lineas_cerradas_venta.Checked, lista_pedidos_lineas_compras_seleccionadas,"");            
            }
            else
            {
                dataGridView_lineas_venta.DataSource = bd.ObtenerLineasPedido(empresa, "cliente", pedido, checkBox_lineas_cerradas_venta.Checked, lista_pedidos_lineas_compras_seleccionadas,"");
                dataGridView_lineas_venta.ClearSelection();
            }
        }


      
        private void dataGridView_lineas_compra_SelectionChanged(object sender, EventArgs e)
        {
            if (flag_compras == 0)
            {             
                lista_pedidos_lineas_compras_seleccionadas.Clear();
                foreach (DataGridViewRow row in dataGridView_lineas_compra.SelectedRows)
                    lista_pedidos_lineas_compras_seleccionadas.Add(row.Cells["PEDIDO"].Value.ToString() + "-" + row.Cells["LÍNEA"].Value.ToString());

                dataGridView_lotes.DataSource = bd.ObtenerLotesLineaPedido(empresa,lista_pedidos_lineas_compras_seleccionadas);
                dataGridView_lotes.ClearSelection();

                Cargar_Reservas();

                string pedido_venta = comboBox_pedido_venta.SelectedValue.ToString();
                if (pedido_venta != "")
                {
                    dataGridView_lineas_venta.DataSource = bd.ObtenerLineasPedido(empresa, "cliente", pedido_venta, checkBox_lineas_cerradas_venta.Checked, lista_pedidos_lineas_compras_seleccionadas,"");
                    dataGridView_lineas_venta.ClearSelection();
                }                                
            }
            flag_compras = 0;
        }



        private void button_reservar_Click(object sender, EventArgs e)
        {
            if (dataGridView_lineas_compra.SelectedRows.Count == 0) {
                MessageBox.Show(Form.ActiveForm, "No ha seleccionado líneas de compra", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dataGridView_lineas_venta.SelectedRows.Count == 0) {
                MessageBox.Show(Form.ActiveForm, "No ha seleccionado líneas de venta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            if (dataGridView_lotes.SelectedRows.Count == 0)
            {
                foreach (DataGridViewRow linea_compra in dataGridView_lineas_compra.SelectedRows)
                {
                    foreach (DataGridViewRow linea_venta in dataGridView_lineas_venta.SelectedRows)
                        bd.InsertarReserva(empresa, linea_compra.Cells["PEDIDO"].Value.ToString(), linea_compra.Cells["LÍNEA"].Value.ToString(), "", linea_venta.Cells["PEDIDO"].Value.ToString(), linea_venta.Cells["LÍNEA"].Value.ToString(), linea_venta.Cells["CÓDIGO"].Value.ToString(), linea_venta.Cells["DENOMINACIÓN"].Value.ToString(), linea_venta.Cells["PEDIDA"].Value.ToString(), linea_venta.Cells["ENTREGA"].Value.ToString(), usuario);                    
                }
            }
            else
            {
                foreach (DataGridViewRow lote in dataGridView_lotes.SelectedRows)
                {
                    foreach (DataGridViewRow linea_venta in dataGridView_lineas_venta.SelectedRows)
                        bd.InsertarReserva(empresa, lote.Cells["PEDIDO"].Value.ToString(), lote.Cells["LÍNEA"].Value.ToString(), lote.Cells["LOTE"].Value.ToString(), linea_venta.Cells["PEDIDO"].Value.ToString(), linea_venta.Cells["LÍNEA"].Value.ToString(),linea_venta.Cells["CÓDIGO"].Value.ToString(), linea_venta.Cells["DENOMINACIÓN"].Value.ToString(), linea_venta.Cells["PEDIDA"].Value.ToString(), linea_venta.Cells["ENTREGA"].Value.ToString(), usuario);
                }
            }

            Cargar_Reservas();
            
            dataGridView_lineas_venta.DataSource = bd.ObtenerLineasPedido(empresa, "cliente", comboBox_pedido_venta.SelectedValue.ToString(), checkBox_lineas_cerradas_venta.Checked, lista_pedidos_lineas_compras_seleccionadas,"");
            dataGridView_lineas_venta.ClearSelection();

            //Hago copia del pedido para marcar las diferencias
            if (comboBox_mp.SelectedValue == null || comboBox_mp.SelectedValue.ToString() == "")
            {
                if(!bd.ExisteCopiaPedidoCompras(empresa,comboBox_pedido_compra.SelectedValue.ToString()))
                {                
                    foreach (DataGridViewRow linea_compra in dataGridView_lineas_compra.Rows)
                    {
                        if(linea_compra.Cells["ABIERTA"].Value.ToString() == "SÍ")                                 
                            bd.InsertarCopiaLineaPedidoCompras(empresa, linea_compra.Cells["PEDIDO"].Value.ToString(), linea_compra.Cells["LÍNEA"].Value.ToString(), linea_compra.Cells["CÓDIGO"].Value.ToString(), linea_compra.Cells["DENOMINACIÓN"].Value.ToString(), linea_compra.Cells["PEDIDA"].Value.ToString(), linea_compra.Cells["ENTREGA"].Value.ToString(), usuario);                 
                    }
                }
            }

            this.Cursor = Cursors.Arrow;
        }



        private void button_no_reservar_Click(object sender, EventArgs e)
        {

            if (dataGridView_reservas.SelectedRows.Count == 0)
            {
                MessageBox.Show(Form.ActiveForm, "No ha seleccionado las reservas a quitar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string ids = "";
            
            foreach (DataGridViewRow reserva in dataGridView_reservas.SelectedRows)
            {
                if (ids == "")
                    ids = reserva.Cells["id"].Value.ToString();
                else
                    ids = ids + "," + reserva.Cells["id"].Value.ToString();                                
            }

            bd.EliminarReserva(empresa, ids);

            Cargar_Reservas();

            string pedido_venta = comboBox_pedido_venta.SelectedValue.ToString();
            if (pedido_venta != "")
            {
                dataGridView_lineas_venta.DataSource = bd.ObtenerLineasPedido(empresa, "cliente", pedido_venta, checkBox_lineas_cerradas_venta.Checked, lista_pedidos_lineas_compras_seleccionadas,"");
                dataGridView_lineas_venta.ClearSelection();
            }
        }



        private void checkBox_lineas_cerradas_compra_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_proveedor_SelectedIndexChanged(sender, e);
        }



        private void checkBox_lineas_cerradas_venta_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_cliente_SelectedIndexChanged(sender, e);
        }



        private void Marcar_Reservas_Lineas_Venta_Eliminadas()
        {
            foreach (DataGridViewRow reserva in dataGridView_reservas.Rows)
            {
                if (reserva.Cells["existe"].Value.ToString() == "0")
                {
                    reserva.Cells["P_VENTA"].Style.BackColor = Color.Red;
                    reserva.Cells["L_VENTA"].Style.BackColor = Color.Red;
                    reserva.Cells["CÓDIGO"].Style.BackColor = Color.Red;
                    reserva.Cells["DENOMINACIÓN"].Style.BackColor = Color.Red;
                    reserva.Cells["PEDIDA"].Style.BackColor = Color.Red;
                    reserva.Cells["ENTREGA"].Style.BackColor = Color.Red;
                }
            }
        }



        private void Marcar_Reservas_Lineas_Venta_Modificadas()
        {
            DataTable table;
            
            foreach (DataGridViewRow reserva in dataGridView_reservas.Rows)
            {
                if (reserva.Cells["existe"].Value.ToString() != "0")
                {
                    table = bd.ObtenerUnaLineaPedido(empresa, "cliente", reserva.Cells["P_VENTA"].Value.ToString(), reserva.Cells["L_VENTA"].Value.ToString());

                    if (reserva.Cells["CÓDIGO"].Value.ToString() != table.Rows[0]["CODIGO"].ToString())
                        reserva.Cells["CÓDIGO"].Style.BackColor = Color.Orange;
                    if (reserva.Cells["DENOMINACIÓN"].Value.ToString() != table.Rows[0]["DENOMINACION"].ToString())
                        reserva.Cells["DENOMINACIÓN"].Style.BackColor = Color.Orange;
                    if (reserva.Cells["PEDIDA"].Value.ToString() != table.Rows[0]["CTDUP"].ToString())
                        reserva.Cells["PEDIDA"].Style.BackColor = Color.Orange;
                    if (reserva.Cells["ENTREGA"].Value.ToString() != table.Rows[0]["FFINAL"].ToString())
                        reserva.Cells["ENTREGA"].Style.BackColor = Color.Orange;
                }                    
            }
        }




        private void Cargar_Reservas()
        {
            dataGridView_reservas.DataSource = bd.ObtenerReservasLineaPedido(empresa, lista_pedidos_lineas_compras_seleccionadas);
            dataGridView_reservas.ClearSelection();
            Marcar_Reservas_Lineas_Venta_Eliminadas();
            Marcar_Reservas_Lineas_Venta_Modificadas();
        }



        private void Marcar_Lineas_Compra_Nuevas_Modificadas_Eliminadas()
        {
            DataTable table;

            if (bd.ExisteCopiaPedidoCompras(empresa, comboBox_pedido_compra.SelectedValue.ToString()))
            {
                foreach (DataGridViewRow linea_compra in dataGridView_lineas_compra.Rows)
                {
                    if (linea_compra.Cells["existe"].Value.ToString() == "n")
                        linea_compra.DefaultCellStyle.BackColor = Color.Red;
                    else
                    {
                        table = bd.ObtenerCopiaLineaPedidoCompras(empresa, linea_compra.Cells["PEDIDO"].Value.ToString(), linea_compra.Cells["LÍNEA"].Value.ToString());
                        if (table.Rows.Count == 0)
                            linea_compra.DefaultCellStyle.BackColor = Color.Green;
                        else
                        {
                            if (linea_compra.Cells["CÓDIGO"].Value.ToString() != table.Rows[0]["CODIGO_COMPRA"].ToString())
                                linea_compra.Cells["CÓDIGO"].Style.BackColor = Color.Orange;
                            if (linea_compra.Cells["DENOMINACIÓN"].Value.ToString() != table.Rows[0]["DENOMINACION_COMPRA"].ToString())
                                linea_compra.Cells["DENOMINACIÓN"].Style.BackColor = Color.Orange;
                            if (linea_compra.Cells["PEDIDA"].Value.ToString() != table.Rows[0]["CANTIDAD_COMPRA"].ToString())
                                linea_compra.Cells["PEDIDA"].Style.BackColor = Color.Orange;
                            if (linea_compra.Cells["ENTREGA"].Value.ToString() != table.Rows[0]["FECHA_COMPRA"].ToString())
                                linea_compra.Cells["ENTREGA"].Style.BackColor = Color.Orange;
                        }
                    }
                }
            }
        }

        

        private void button_semaforo_eliminar_Click(object sender, EventArgs e)
        {
            string lineas;
            
            lineas = "";
            foreach (DataGridViewRow linea_compra in dataGridView_lineas_compra.Rows)
            {
                if (linea_compra.DefaultCellStyle.BackColor == Color.Red)
                {
                    if (lineas == "")
                        lineas = linea_compra.Cells["LÍNEA"].Value.ToString();
                    else
                        lineas = lineas + "," + linea_compra.Cells["LÍNEA"].Value.ToString();
                }                   
            }

            if (lineas != "")
            {
                bd.EliminarLineasCompraCopiaReservas(empresa, comboBox_pedido_compra.SelectedValue.ToString(), lineas);
                comboBox_pedido_compras_SelectedIndexChanged(sender,e);
            }
                
        }



        private void button_semaforo_insertar_Click(object sender, EventArgs e)
        {
            bool actualizar = false;
            
            foreach (DataGridViewRow linea_compra in dataGridView_lineas_compra.Rows)
            {
                if (linea_compra.DefaultCellStyle.BackColor == Color.Green)
                {
                    bd.InsertarCopiaLineaPedidoCompras(empresa, linea_compra.Cells["PEDIDO"].Value.ToString(), linea_compra.Cells["LÍNEA"].Value.ToString(), linea_compra.Cells["CÓDIGO"].Value.ToString(), linea_compra.Cells["DENOMINACIÓN"].Value.ToString(), linea_compra.Cells["PEDIDA"].Value.ToString(), linea_compra.Cells["ENTREGA"].Value.ToString(), usuario);
                    actualizar = true;                                        
                }
            }

            if (actualizar)                            
                comboBox_pedido_compras_SelectedIndexChanged(sender, e);            
        }



        private void button_semaforo_actualizar_Click(object sender, EventArgs e)
        {
            bool actualizar = false;

            foreach (DataGridViewRow linea_compra in dataGridView_lineas_compra.Rows)
            {
                if ((linea_compra.Cells["CÓDIGO"].Style.BackColor == Color.Orange) || (linea_compra.Cells["DENOMINACIÓN"].Style.BackColor == Color.Orange) || (linea_compra.Cells["PEDIDA"].Style.BackColor == Color.Orange) || (linea_compra.Cells["ENTREGA"].Style.BackColor == Color.Orange))
                {
                    bd.ActualizarCopiaLineaPedidoCompras(empresa, linea_compra.Cells["PEDIDO"].Value.ToString(), linea_compra.Cells["LÍNEA"].Value.ToString(), linea_compra.Cells["CÓDIGO"].Value.ToString(), linea_compra.Cells["DENOMINACIÓN"].Value.ToString(), linea_compra.Cells["PEDIDA"].Value.ToString(), linea_compra.Cells["ENTREGA"].Value.ToString(), usuario);
                    actualizar = true;
                }
            }

            if (actualizar)
                comboBox_pedido_compras_SelectedIndexChanged(sender, e);
        }

        private void ReservaComprasMP_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }

    
                                
    }
}
