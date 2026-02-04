using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using empresaGlobalProj;

namespace ProyectosOnLine
{
    public partial class Form2 : Form
    {        
            #region atributos de la clase
        private Int32 of;

        private String operacion_seleccionada;
        private Int32 operario_seleccionado;
        private Int32 id;
        private Int32 bono_seleccionado;
        private Int32 etiqueta_seleccionada;

        private DataGridView.HitTestInfo hitTestInfo;
            #endregion

        public Form2(Int32 ordfab)
        {
            InitializeComponent();

            this.of = ordfab;
            this.Text += this.of.ToString();            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'imeDataSet.Vista_TC_IMPORTBONOS' Puede moverla o quitarla según sea necesario.
            if (empresaGlobal.empresaID == "60")
            {
                this.vista_TC_IMPORTBONOSTableAdapter.InitCommandCollection();
                this.vista_TC_IMPORTBONOSTableAdapter._commandCollection[0].CommandText = this.vista_TC_IMPORTBONOSTableAdapter._commandCollection[0].CommandText.Replace("Vista_TC_IMPORTBONOS", "Vista_TC_IMPORTBONOS_MADE");
            }
            this.vista_TC_IMPORTBONOSTableAdapter.Fill(this.imeDataSet.Vista_TC_IMPORTBONOS, this.of);
        }

        
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" no gg bro");
            String usuario = Environment.UserName.ToUpper();
            String usuarios_permitidos = ConfigurationManager.AppSettings["Users_fichar_piezas"].ToString().ToUpper();
            if (usuarios_permitidos.Contains(usuario) == false)
                return;
            

            // Load context menu on right mouse click            
            if (e.Button == MouseButtons.Right)
            {
                this.id = 0;
                this.bono_seleccionado = 0;
                this.etiqueta_seleccionada = 0;

                contextMenuStrip1.Items["cargarPiezasToolStripMenuItem1"].Enabled = true;
                contextMenuStrip1.Items["dividirFichajeToolStripMenuItem"].Enabled = true;
                contextMenuStrip1.Items["quitarPiezasToolStripMenuItem"].Enabled = true;

                this.hitTestInfo = this.dataGridView1.HitTest(e.X, e.Y);


                //JRegino 10/03/2016. Comprobacion de fecha permitida para todos los casos.
                if (this.fechaPermitida(Convert.ToDateTime(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[5].Value)) == false)
                {
                    contextMenuStrip1.Items["cargarPiezasToolStripMenuItem1"].Enabled = false;
                    contextMenuStrip1.Items["dividirFichajeToolStripMenuItem"].Enabled = false;
                    contextMenuStrip1.Items["quitarPiezasToolStripMenuItem"].Enabled = false;
                }
                else
                {
                    //Caso para que se le fichen piezas al operario -3 en la operacion clickada
                    if (this.hitTestInfo.RowIndex == this.dataGridView1.RowCount-1) 
                    {
                        if (this.hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex >= 10 && hitTestInfo.ColumnIndex <= 27)
                        {
                            this.operacion_seleccionada = this.dataGridView1.Columns[this.hitTestInfo.ColumnIndex].ToolTipText;                        
                            this.operario_seleccionado = Convert.ToInt32(ConfigurationManager.AppSettings["Operario"].ToString());

                            contextMenuStrip1.Items["dividirFichajeToolStripMenuItem"].Enabled = false;
                            contextMenuStrip1.Show(this.dataGridView1, new Point(e.X, e.Y));                        
                        }
                    }
                    else
                    {
                        this.operario_seleccionado = Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[1].Value);
                        if (this.operario_seleccionado == -1)
                            return;

                        this.id = Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[0].Value);


                        this.operacion_seleccionada = this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[4].Value.ToString();

                        if (this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[6].Value != DBNull.Value)
                            this.bono_seleccionado = Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[6].Value);
                        else
                            this.bono_seleccionado = 0;

                        if (this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[7].Value != DBNull.Value)
                        {
                            this.etiqueta_seleccionada = Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[7].Value);

                            int cantidad = Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[8].Value);
                            int cantidadfichada = Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[9].Value);


                            //JRegino 22/11/2013 Comento esta linea xq se puede dar la siguiente situación: Una of con 3 etiquetas, 2 de ellas están completamente fichadas por el operario.
                            //                                                                                  No se podría cargar la 3ª etiqueta a ese operario ya que siempre aparecería deshabilitada la opcion
                            //if (cantidad <= cantidadfichada)
                            //    contextMenuStrip1.Items["cargarPiezasToolStripMenuItem1"].Enabled = false;
                            if (cantidadfichada <= 1)
                                contextMenuStrip1.Items["dividirFichajeToolStripMenuItem"].Enabled = false;
                            if (cantidadfichada <= 0)
                                contextMenuStrip1.Items["quitarPiezasToolStripMenuItem"].Enabled = false;
                        }
                        else
                        {
                            this.etiqueta_seleccionada = 0;
                            contextMenuStrip1.Items["dividirFichajeToolStripMenuItem"].Enabled = false;
                            contextMenuStrip1.Items["quitarPiezasToolStripMenuItem"].Enabled = false;
                        }
                    }
                    
                }

                contextMenuStrip1.Show(this.dataGridView1, new Point(e.X, e.Y));                                            
            }
        }
        

        private void cargarPiezasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 carga = new Form3(this.of, this.operacion_seleccionada, this.operario_seleccionado, this.bono_seleccionado, this.etiqueta_seleccionada, this.id,1);
            if (carga.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                //this.vista_TC_IMPORTBONOSTableAdapter.Fill(this.imeDataSet.Vista_TC_IMPORTBONOS, this.of);
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void dividirFichajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form31 f = new Form31(Convert.ToInt32(dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[0].Value), this.of, Convert.ToInt32(dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[7].Value));
            if (f.ShowDialog() == DialogResult.OK)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void quitarPiezasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form32 f = new Form32(Convert.ToInt32(dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[0].Value), this.of, Convert.ToInt32(dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[7].Value));
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                //this.vista_TC_IMPORTBONOSTableAdapter.Fill(this.imeDataSet.Vista_TC_IMPORTBONOS, this.of);
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }


        //Funciona auxiliar que devuelve true si la fecha por parámetro está en el intervalo permitido por el fichero de control
        private Boolean fechaPermitida(DateTime fecha_auxiliar)
        {
            Boolean valido = false;

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            String strSql = "";
            SqlCommand sqlCmd;
            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql = "Select gg.dbo.getEsFechaControl ('" + empresaGlobal.empresaID + "', 'MS', convert(datetime, '" + fecha_auxiliar + "', 103)) ";
                sqlCmd = new SqlCommand(strSql, conexion);

                if (Convert.ToInt32(sqlCmd.ExecuteScalar()) == -1)
                    valido = false;
                else
                    valido = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrio un error al comprobar la fecha del fichaje: " + ex.Message.ToString());                
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return valido;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, DataGridViewCellEventArgs e, MouseEventArgs k)
        {
            if (k.Button == MouseButtons.Right)
            {
                Int32 indiceCalidad = this.dataGridView1.Rows[e.RowIndex].Cells.Count - 1;
                int calidadRed = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[indiceCalidad].Value);
                if (calidadRed == -1)
                {
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("Cut"));
                    m.MenuItems.Add(new MenuItem("Copy"));
                    m.MenuItems.Add(new MenuItem("Paste"));

                    int currentMouseOverRow = dataGridView1.HitTest(k.X, k.Y).RowIndex;

                    if (currentMouseOverRow >= 0)
                    {
                        m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                    }

                    m.Show(dataGridView1, new Point(k.X, k.Y));
                }
            }
        }





        #region Método antiguo para dividir un fichaje
        /*
        private void dividirFichajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 ctd = 0;
            if (InputBox("Indicar la cantidad para el nuevo fichaje", ref ctd) == DialogResult.OK)
            {
                Int32 ctd_old = Convert.ToInt32(dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[this.hitTestInfo.ColumnIndex].Value);

                if (ctd >= ctd_old)
                {
                    MessageBox.Show("Error: Cantidad Superior a la permitida");
                }
                else
                {
                    SqlConnection conexion = new SqlConnection(global::ProyectosOnLine.Properties.Settings.Default.ggConnectionString);

                    String strSql = "";
                    SqlCommand sqlCmd;

                    Int32 id = Convert.ToInt32(dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[0].Value);
                    Int32 bono = Convert.ToInt32(dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[6].Value);

                    try
                    {
                        if (conexion.State != ConnectionState.Open)
                            conexion.Open();


                        // 09/01/2014 JRegino. Se cambia el método de la division del fichaje xq si un trabajo contiene la carga de varias piezas puede fallar
                        //crear la nueva carga
                        strSql = "Insert into TC_IMPORT_BONOS (CODEMP, CODOPE, FINI, MAQUINA, CLAHOR, ORDFAB, CORREL, HINI, HFIN, TIEMPO, PBUENAS, PMALAS, MARCAIMPREVISTO, TEXTO, TURNO, FLAG, FECHAC, ";
                        strSql += "	                      FECHAM, USUCRE, USUMOD, AUTOMAN, FLAGS, ORDFABN, CODART, CANPREV, BONO, MjTrabajoConBonoId, GALVANIZADO_ITEMS, ORDFAB_int, ";
                        strSql += "	                      FINI_datetime, PBUENAS_float, Correl_SmallInt, Flag_Inventario, Flag_Coste, idFab, idConsuOper_BORRAR, id_mp_cargadepiezas, ";
                        strSql += "	                      MjTrabajoConBonoId_cambio)";
                        strSql += "	SELECT CODEMP, CODOPE, FINI, MAQUINA, CLAHOR, ORDFAB, CORREL, HINI, HFIN, TIEMPO, " + ctd + " as PBUENAS, PMALAS, MARCAIMPREVISTO, TEXTO, TURNO, FLAG, FECHAC, ";
                        strSql += "	                      FECHAM, USUCRE, USUCRE + '_div', AUTOMAN, FLAGS, ORDFABN, CODART, CANPREV, BONO, MjTrabajoConBonoId, GALVANIZADO_ITEMS, ORDFAB_int, ";
                        strSql += "	                      FINI_datetime, " + ctd + " as PBUENAS_float, Correl_SmallInt, Flag_Inventario, Flag_Coste, idFab, idConsuOper_BORRAR, id_mp_cargadepiezas, ";
                        strSql += "	                      MjTrabajoConBonoId_cambio";
                        strSql += "		FROM TC_IMPORT_BONOS";
                        //strSql += "		WHERE (id = " + id + ")	";  

                        //JRegino 17/06/2013
                        strSql += "		WHERE (MjTrabajoConBonoId = " + id + ")	";
                        strSql += "		        AND (BONO = " + bono + ") 	";
                        strSql += "		        AND (ORDFAB_int = " + this.of + ") ";
                        strSql += "		        AND (PBUENAS_float <> 0)	";

                        sqlCmd = new SqlCommand(strSql, conexion);
                        sqlCmd.ExecuteScalar();



                        //actualizar la cantidad de la carga original
                        strSql = "Update TC_IMPORT_BONOS ";
                        strSql += "		set PBUENAS = PBUENAS - " + ctd + " ,";
                        strSql += "			PBUENAS_float = PBUENAS_float - " + ctd;
                        //strSql += "		WHERE (id = " + id + ")	";  

                        //JRegino 17/06/2013
                        strSql += "		WHERE (MjTrabajoConBonoId = " + id + ")	";
                        strSql += "		        AND (BONO = " + bono + ") ";
                        strSql += "		        AND (ORDFAB_int = " + this.of + ") ";
                        strSql += "		        AND (PBUENAS_float <> " + ctd_old + ") ";   //xa no actualizar el nuevo registro si fuera la misma ctd
                        strSql += "		        AND (right(usumod, 4) <> '_div') ";

                        sqlCmd = new SqlCommand(strSql, conexion);
                        sqlCmd.ExecuteScalar();
                        

                        this.vista_TC_IMPORTBONOSTableAdapter.Fill(this.imeDataSet.Vista_TC_IMPORTBONOS, this.of);
                        MessageBox.Show("División Realizada correctamente");                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al dividir el fichaje: " + ex.ToString());                        
                    }
                    finally
                    {
                        if (conexion.State == ConnectionState.Open)
                            conexion.Close();
                    }

                }
            }
        }



        
        private DialogResult InputBox(String title, ref Int32 value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();            
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();

            try
            {
                value = Convert.ToInt32(textBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("La cantidad debe ser un número", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dialogResult = DialogResult.Cancel;
            }

            return dialogResult;
        }
        */
        #endregion

        #region Mostrar-Ocultar fichajes
        /*
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;

            if (radioButton1.Checked == true)
            {
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;

            if (radioButton2.Checked == true)
            {
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    dataGridView1.Rows[i].Visible = true;

                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value) == 0)
                        dataGridView1.Rows[i].Visible = false;
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;

            if (radioButton3.Checked == true)
            {
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    dataGridView1.Rows[i].Visible = true;

                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value) != 0)
                        dataGridView1.Rows[i].Visible = false;                    
                        
                }
            }
        }
        */
        #endregion

    }
}