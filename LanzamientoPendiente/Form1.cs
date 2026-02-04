using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LanzamientoPendiente
{
    public partial class Form1 : Form
    {

        ConsultaBD fbd = new ConsultaBD();
        string titulo = "";
        string pedido = "";
        

        public Form1()
        {
            InitializeComponent();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgLanzamientos.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");//ColorTranslator.FromHtml("#ADEBC3");

            dgLanzamientos.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            dgLanzamientos.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgLanzamientos.EnableHeadersVisualStyles = false;
       

            dgLanzamientos.Columns[0].Width = 240;
            dgLanzamientos.Columns[1].Width = 525;
            dgLanzamientos.Columns[2].Width = 290;
            dgLanzamientos.Columns[3].Width = 130;
            dgLanzamientos.Columns[4].Width = 130;

            dgLanzamientos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in dgLanzamientos.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        private void btnComprobarPedido_Click(object sender, EventArgs e)
        {

            if (dgLanzamientos.Rows.Count > 0)
            {
                DialogResult mensaje = MessageBox.Show("¿Está seguro que desea comprobar un nuevo pedido? Se vaciará el contenido de la tabla", "Aviso", MessageBoxButtons.YesNo);
                if (mensaje == DialogResult.Yes)
                {
                    titulo = fbd.obtenerPedido(tbPedido.Text.ToString());
                    dgLanzamientos.Rows.Clear();

                    if (titulo.Equals(""))
                    {
                        lbPedido.Text = "No existe este pedido.";
                    }
                    else
                    {
                        lbPedido.Text = titulo;
                        pedido = tbPedido.Text.ToString();
                    }

                    btnAnadirFila.Enabled = true;
                    btnQuitar.Enabled = true;
                }

            }
            else { 
            titulo = fbd.obtenerPedido(tbPedido.Text.ToString());
            dgLanzamientos.Rows.Clear();
           
            if (titulo.Equals("")) {
                lbPedido.Text = "No existe este pedido.";
            }else{
                lbPedido.Text = titulo;
                pedido = tbPedido.Text.ToString();
            }

            btnAnadirFila.Enabled = true;
            btnQuitar.Enabled = true;

            }

        }

        private void btnAñadirFila_Click(object sender, EventArgs e)
        {
            dgLanzamientos.Rows.Add(1);
            List<string> lineasDenominacion = new List<string>();
            lineasDenominacion = fbd.obtenerLineasPedido(pedido);

            DataGridViewComboBoxCell CBCell = new DataGridViewComboBoxCell();

            CBCell = (DataGridViewComboBoxCell)dgLanzamientos.Rows[dgLanzamientos.Rows.Count-1].Cells[2];
            CBCell.Items.Clear();
            Cursor.Current = Cursors.WaitCursor;
            foreach (String linea in lineasDenominacion)
            {
                CBCell.Items.Add(linea);

            }

            if (lineasDenominacion.Count > 0){ 
            CBCell.DropDownWidth = DropDownWidth(CBCell);
            }
            //CBCell.DropDownWidth = 800;
            Cursor.Current = Cursors.Default;

        }

        /*private int DropDownWidth(DataGridViewComboBoxCell myCombo)
        {
            int maxWidth = 0;
            int temp = 0;
            Label label1 = new Label();

            foreach (var obj in myCombo.Items)
            {
                label1.Text = obj.ToString();
                temp = label1.PreferredWidth;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            label1.Dispose();
            return maxWidth;
        }*/

        int DropDownWidth(DataGridViewComboBoxCell myCombo)
        {
            int maxWidth = 0, temp = 0;
            foreach (var obj in myCombo.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), myCombo.Style.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            return maxWidth;
        }

        private void dgLanzamientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            string codigo = "";
            List<string> lineasDenominacion = new List<string>();
            //tgFaltanDatos.Rows[e.RowIndex].Cells[4].Value.ToString()
            //dgLanzamientos.EditMode = DataGridViewEditMode.EditOnEnter;
            if (e.ColumnIndex == 0) {
            dgLanzamientos.BeginEdit(true);
                
            }

            if (e.ColumnIndex == 1 && e.RowIndex > -1)
            {
                SendKeys.Send("{TAB}");
                SendKeys.Send("+{TAB}");
                if (dgLanzamientos.Rows[e.RowIndex].Cells[0].Value != null)
                {

                    if (!String.IsNullOrEmpty(dgLanzamientos.Rows[e.RowIndex].Cells[0].Value.ToString()))
                    {

                        codigo = fbd.obtenerDenominacionCodigo(dgLanzamientos.Rows[e.RowIndex].Cells[0].Value.ToString());
                        if (codigo.Equals(""))
                        {

                            dgLanzamientos.Rows[e.RowIndex].Cells[1].Value = "NO EXISTE CÓDIGO!";

                        }
                        else
                        {

                            dgLanzamientos.Rows[e.RowIndex].Cells[1].Value = codigo;

                            /*
                            //RELLENAR LINEAS
                            

                            lineasDenominacion = fbd.obtenerLineasPedido(tbPedido.Text.ToString());

                            DataGridViewComboBoxCell CBCell = new DataGridViewComboBoxCell();

                            CBCell = (DataGridViewComboBoxCell)dgLanzamientos.Rows[e.RowIndex].Cells[2];
                            CBCell.Items.Clear();
                            Cursor.Current = Cursors.WaitCursor;
                            foreach (String linea in lineasDenominacion)
                            {
                                CBCell.Items.Add(linea);

                            }
                            Cursor.Current = Cursors.Default;
                            
                             // Intentar eliminar si aparece en otra fila
                            int count = CBCell.Items.Count - 1;
                            for (int i = count; i > 0; i--)
                            {
                              foreach (DataGridViewRow row in dgLanzamientos.Rows) {

                               if (row.Cells[2].Value != null)
                               { 
                                   if (CBCell.Items[i].ToString() == row.Cells[2].Value.ToString()) {
                                        CBCell.Items.RemoveAt(i);
                                   }
                               }
                              }
                               
                            }
                             //-----------------------
                             */
                            Cursor.Current = Cursors.Default;

                        }


                    }
                    else
                    {
                        MessageBox.Show("No hay ningun codigo introducido.");
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun codigo introducido.");
                }

            }
            else { 
            
            }
            
        }


        private void btnLanzar_Click(object sender, EventArgs e)
        {
            string codigo = "";
            string cuerpo = "";
            foreach (DataGridViewRow row in dgLanzamientos.Rows) { 
            //comboEMpresaMaq.Text.ToString()
                //ACTUALIZACION
                codigo = fbd.obtenerDenominacionCodigo(row.Cells[0].Value.ToString());
                 if (codigo.Equals(""))
                 {
                     row.Cells[1].Value = "NO EXISTE CÓDIGO!";
                 }
                 else
                 {
                     row.Cells[1].Value = codigo;
                 }
                //----------------

                if (row.Cells[4].Value == null)
                {
                    row.Cells[4].Value = "";
                }

                if (String.IsNullOrEmpty(row.Cells[4].Value.ToString()) || row.Cells[4].Value.ToString().Equals("X")) { 
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null || row.Cells[2].Value == null)
                    {
                        row.Cells[4].Value = 'X';
                        row.Cells[4].Style.BackColor = Color.IndianRed;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(row.Cells[0].Value.ToString()) || String.IsNullOrEmpty(row.Cells[1].Value.ToString()) || String.IsNullOrEmpty(row.Cells[2].Value.ToString()) || row.Cells[1].Value.ToString().Equals("NO EXISTE CÓDIGO!"))
                        {
                            row.Cells[4].Value = 'X';
                            row.Cells[4].Style.BackColor = Color.IndianRed;
                        }
                        else
                        {
                            //Ahora procedo a poner que estan bien ✓✓be
                           // row.Cells[4].Value = '✓';
                            int afectadas = 0;
                            string[] words = row.Cells[2].Value.ToString().Split('/');
                            string linea = words[0];
                            linea = linea.Replace(" ", "");

                            if (Convert.ToBoolean(row.Cells[3].Value))
                            {
                                //actualizarCodigoDenominacion(string linea, string pedido, string codigo, string denominacion) denominacion completa
                                afectadas = fbd.actualizarCodigoDenominacion(linea, pedido, row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                                if (afectadas == 0)
                                {
                                    row.Cells[4].Value = 'X';
                                    row.Cells[4].Style.BackColor = Color.IndianRed;
                                }
                                else {
                                    row.Cells[4].Value = '✓';
                                    row.Cells[4].Style.BackColor = Color.LightGreen;
                                }
                            }
                            else {
                                afectadas = fbd.actualizarCodigoDenominacion(linea, pedido, row.Cells[0].Value.ToString(), "");
                                if (afectadas == 0)
                                {
                                    row.Cells[4].Value = 'X';
                                    row.Cells[4].Style.BackColor = Color.IndianRed;
                                }
                                else
                                {
                                    row.Cells[4].Value = '✓';
                                    row.Cells[4].Style.BackColor = Color.LightGreen;
                                }
                            }

                        }

                    }

                }
            
            }

            cuerpo ="";
            foreach (DataGridViewRow fila in dgLanzamientos.Rows) {

                if (fila.Cells[4].Value.ToString().Equals("✓"))
                {
                    if ((!String.IsNullOrEmpty(fila.Cells[0].Value.ToString())) && (!String.IsNullOrEmpty(fila.Cells[1].Value.ToString())))
                    {
                        string[] words2 = fila.Cells[2].Value.ToString().Split('/');
                        string lin = words2[0];
                        lin = lin.Replace(" ", "");

                        if (String.IsNullOrEmpty(cuerpo)) {
                            cuerpo = "Se han metido los códigos de las siguientes líneas del pedido <b>" + pedido + "</b> que estaban en desarrollo:<br><br>";
                        }
                            cuerpo = cuerpo + "Línea: " + lin + "<br>";
                            if (Convert.ToBoolean(fila.Cells[3].Value))
                            {
                                cuerpo = cuerpo + "Denominación: " + fila.Cells[1].Value.ToString() + "<br>";
                            }
                            else {
                                string denominacion_aux = fbd.obtenerDenominacionActual(pedido, lin);
                                cuerpo = cuerpo + "Denominación: " + denominacion_aux + "<br>";
                            }
                            cuerpo = cuerpo + "Código: <b>" + fila.Cells[0].Value.ToString() + "</b><br><br>";
                    }
                }
            
            }

            if (!String.IsNullOrEmpty(cuerpo)) {
                Enviar_Correo3("CÓDIGOS DESARROLLOS PEDIDO " + titulo + ")", cuerpo, obtener_destinatarios(), obtener_concopia());
            }

            MessageBox.Show("FIN LANZAMIENTO. Revise en la última columna de la tabla si todos los códigos han sido lanzados correctamente", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }


        void Enviar_Correo3(string asunto, string cuerpo, string destinatarios, string concopia)
        {
            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            mail.Subject = asunto;
            mail.HTMLBody = cuerpo;
            mail.To = destinatarios;
            mail.CC = concopia;
            //mail.Send();
            mail.Display(false);
        }


        public string obtener_destinatarios()
        {
            string destinatarios;

            destinatarios = "jredondo@imedexsa.es;juanantonio.rivero@imedexsa.es;";

            return destinatarios;
        }

        public string obtener_concopia()
        {
            SqlConnection conexion_bd;
            SqlCommand consulta;
            SqlDataReader datosconsulta;
            string concopia, representante, strsql;
            

            //FIJOS
            concopia = "vcandela@imedexsa.es;manuel.vivas@imedexsa.es;olga.kuznyetsova@imedexsa.es;";

            //VARIABLES
            conexion_bd = new SqlConnection(Utils.CD.getConexion());
            conexion_bd.Open();

            strsql = "";
            strsql = strsql + " select   repres";
            strsql = strsql + " from     t_ordter";
            strsql = strsql + " where    codemp = '3' and tiporeg = 'cf' and numped = " + pedido;
            consulta = new SqlCommand(strsql, conexion_bd);
            datosconsulta = consulta.ExecuteReader();
            datosconsulta.Read();
            representante = datosconsulta[0].ToString();
            datosconsulta.Close();

            strsql = "";
            strsql = strsql + " select   isnull(email,''),isnull(www,'')";
            strsql = strsql + " from     t_repres";
            strsql = strsql + " where    codemp = '3' and repres = '" + representante + "'";
            consulta.CommandText = strsql;
            datosconsulta = consulta.ExecuteReader();
            datosconsulta.Read();
            if (datosconsulta[0].ToString() != "")
                concopia = concopia + datosconsulta[0].ToString() + ";";
            if (datosconsulta[1].ToString() != "")
                concopia = concopia + datosconsulta[1].ToString() + ";";
            datosconsulta.Close();

            conexion_bd.Close();

            return concopia;
        }




        private void btnQuitar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgLanzamientos.SelectedRows)
            {
                dgLanzamientos.Rows.RemoveAt(row.Index);
            }
            

        }


        private void dgLanzamientos_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //dgLanzamientos.EndEdit();
        }

        private void btnVaciar_Click(object sender, EventArgs e)
        {
            tbPedido.Text = "";
        }

    }
}
