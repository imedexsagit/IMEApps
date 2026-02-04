using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using empresaGlobalProj;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;


namespace ProyectosOnLine
{
    public partial class Form1 : Form
    {
        private DataGridView.HitTestInfo hitTestInfo;
        private Boolean rePintar;
        public static String bono;
        public static String proyecto;
        private bool proyectoParado;
        //variable que almacena la ordfab según la fila sobre la que se ha hecho click
        public static string ordfabGlobal;
        private ConsultasBD fbd;
        public string nproyecto = "";
        public Form1()
        {
            InitializeComponent();
            empresaGlobal.showEmp = false;

            //pongo la version del programa
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed == true)
                this.Text += System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            else
            {
                //this.Text += Application.ProductVersion.ToString();
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                this.Text += version;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.rePintar = true;

            // TODO: esta línea de código carga datos en la tabla 'proyectos.TC_LANZA' Puede moverla o quitarla según sea necesario.
            this.tC_LANZATableAdapter.Fill(this.proyectos.TC_LANZA, empresaGlobal.empresaID);
            this.comboBox1.ResetText();

            // TODO: esta línea de código carga datos en la tabla 'pedidos.T_ORDTER' Puede moverla o quitarla según sea necesario.
            this.t_ORDTERTableAdapter.Fill(this.pedidos.T_ORDTER, empresaGlobal.empresaID);
            this.comboBox2.ResetText();

            //this.comboBox1.Focus();
            this.comboBox1.Select();
            this.comboBox1.SelectedIndex = -1;  // Añadido JPedrero



            //Poner colores a las cabeceras del grid para distinguir entre CyP(Blue), CH(Yellow), CJS(Salmon)
            dataGridView1.Columns[7].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
            dataGridView1.Columns[8].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
            dataGridView1.Columns[9].HeaderCell.Style.BackColor = Color.LightBlue;
            dataGridView1.Columns[10].HeaderCell.Style.BackColor = Color.LightBlue;
            dataGridView1.Columns[11].HeaderCell.Style.BackColor = Color.LightBlue;
            
            dataGridView1.Columns[12].HeaderCell.Style.BackColor = Color.Yellow;
            dataGridView1.Columns[13].HeaderCell.Style.BackColor = Color.Yellow;
            dataGridView1.Columns[14].HeaderCell.Style.BackColor = Color.Yellow;
            dataGridView1.Columns[15].HeaderCell.Style.BackColor = Color.LightYellow;
            dataGridView1.Columns[16].HeaderCell.Style.BackColor = Color.LightYellow;
            dataGridView1.Columns[17].HeaderCell.Style.BackColor = Color.LightYellow;
            dataGridView1.Columns[18].HeaderCell.Style.BackColor = Color.LightYellow;

            dataGridView1.Columns[23].HeaderCell.Style.BackColor = Color.LightSalmon;            
            dataGridView1.Columns[24].HeaderCell.Style.BackColor = Color.LightSalmon;
            dataGridView1.Columns[25].HeaderCell.Style.BackColor = Color.LightSalmon;
            dataGridView1.Columns[26].HeaderCell.Style.BackColor = Color.LightSalmon;

            dataGridView1.Columns[28].HeaderCell.Style.BackColor = Color.Peru;
            dataGridView1.Columns[29].HeaderCell.Style.BackColor = Color.Peru;
            dataGridView1.Columns[30].HeaderCell.Style.BackColor = Color.Peru;

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.textBox2.Text = "";
            this.textBox3.Text = "";

            //String proyecto = "";
            proyecto = "";
            Int32 estadoOF = 0;
            Int32 estadoExp = 0;
            Int32 M1 = 0;
            Int32 M2 = 0;
            Int32 M3 = 0;
            Int32 M4 = 0;
            Int32 M5 = 0;
            Int32 CS = 0;
            Int32 SE = 0;
            String marca = "";

            
            //this.dataGridView1.DataSource = null;

            marca = Convert.ToString(textBox2.Text);

            if (comboBox1.SelectedValue == null)
            {
                this.rePintar = false;                
                return;
            }
            else
            {
                this.rePintar = true;

                nproyecto = comboBox1.Text;
                nproyecto = nproyecto.Remove(0, 6);
                proyecto = comboBox1.SelectedValue.ToString();
                if (radioButton1.Checked == true)
                    estadoOF = 0;
                if (radioButton2.Checked == true)
                    estadoOF = 1;
                if (radioButton3.Checked == true)
                    estadoOF = 2;


                if (rbNoExpedidas.Checked == true)
                    estadoExp = 0;
                if (rbExpedidas.Checked == true)
                    estadoExp = 1;
                if (rbTodasExp.Checked == true)
                    estadoExp = 2;
                if (rbSinExpedicion.Checked == true)
                    estadoExp = 3;




                if (cbM1.Checked == false && cbM2.Checked == false && cbM3.Checked == false && cbM4.Checked == false && cbM5.Checked == false && cbCS.Checked == false)
                {
                    MessageBox.Show("Selecciona una categoria");
                }
                else {
                    if (cbM1.Checked == true)
                    {
                        M1 = 1;
                    }
                    else {
                        M1 = 0;
                    }
                    if (cbM2.Checked == true)
                    {
                        M2 = 1;
                    }
                    else
                    {
                        M2 = 0;
                    }
                    if (cbM3.Checked == true)
                    {
                        M3 = 1;
                    }
                    else
                    {
                        M3 = 0;
                    }
                    if (cbM4.Checked == true)
                    {
                        M4 = 1;
                    }
                    else
                    {
                        M4 = 0;
                    }
                    if (cbM5.Checked == true)
                    {
                        M5 = 1;
                    }
                    else
                    {
                        M5 = 0;
                    }
                    if (cbCS.Checked == true)
                    {
                        CS = 1;
                    }
                    else
                    {
                        CS = 0;
                    }
   
                }


                if (cbSinExpedicion.Checked == true)
                {
                    fbd = new ConsultasBD();

                    Cursor.Current = Cursors.WaitCursor;
                    fbd = new ConsultasBD();

                    fbd.recuperarFichajesSinExpedicion(proyecto);

                    DataTable dt = fbd.obtenerInformacionProyectos(proyecto);


                    var bindingSource = new System.Windows.Forms.BindingSource();
                    bindingSource.DataSource = dt;
                    dataGridView1.DataSource = bindingSource;

                    BindingSource bs = (BindingSource)dataGridView1.DataSource; // Se convierte el DataSource 
                    DataTable dt2 = (DataTable)bs.DataSource;

                    DataRow datarow;
                    datarow = dt2.NewRow(); //Con esto le indica que es una nueva fila

                    
                    this.dataGridView1.Focus();


                    this.listBox1.ClearSelected();
                    this.listBox2.ClearSelected();



                    lbPlan.Text = obtenerPlanCalidad(proyecto);


                    Cursor = Cursors.Default;
                   // this.dataGridView1.DataSource = null;


                }
                else
                {
                    dataGridView1.DataSource = null;
                    Cursor = Cursors.WaitCursor;

                    //ConsultarEstadoProyecto();
                    fbd = new ConsultasBD();


                    //ANTERIOR VERSION DE PROYECTOS ONLINE
                    //DataTable dtPO = fbd.obtenerInformacionProyectosOnline( empresaGlobal.empresaID, proyecto, estadoOF, M1, M2, M3, M4, M5, CS, marca,estadoExp);
                    //NUEVA VERSION DE PROYECTOS ONLINE
                    DataTable dtPO = fbd.obtenerInformacionProyectosOnlineNEW(empresaGlobal.empresaID, proyecto, estadoOF, M1, M2, M3, M4, M5, CS, marca, estadoExp); 


                    var bindingSource = new System.Windows.Forms.BindingSource();
                    bindingSource.DataSource = dtPO;
                    dataGridView1.DataSource = bindingSource;
                    


                    // TODO: esta línea de código carga datos en la tabla 'pedidos_Proyecto.TC_LANZA_PEDLIN' Puede moverla o quitarla según sea necesario.
                    this.tC_LANZA_PEDLINTableAdapter.Fill(this.pedidos_Proyecto.TC_LANZA_PEDLIN, empresaGlobal.empresaID, Convert.ToInt32(comboBox1.SelectedValue));

                    // TODO: esta línea de código carga datos en la tabla 'ggDataSet.T_ORDFAB' Puede moverla o quitarla según sea necesario.
                    if (empresaGlobal.empresaID == "60")
                    {
                        this.t_ORDFABTableAdapter.InitCommandCollection();
                        this.t_ORDFABTableAdapter._commandCollection[0].CommandText = this.t_ORDFABTableAdapter._commandCollection[0].CommandText.Replace("Vista_TC_IMPORTBONOS", "Vista_TC_IMPORTBONOS_MADE");
                    }
                    //this.t_ORDFABTableAdapter.Fill(this.ggDataSet.T_ORDFAB, empresaGlobal.empresaID, proyecto, estadoOF, M1, M2, M3, M4, M5, CS, marca);


                    this.dataGridView1.Focus();

                    this.mostrar_porcentaje(estadoOF);


                    this.listBox1.ClearSelected();
                    this.listBox2.ClearSelected();



                    lbPlan.Text = obtenerPlanCalidad(proyecto);


                    Cursor = Cursors.Default;

                }


                if (Convert.ToInt32(comboBox1.SelectedValue) < 25144 && empresaGlobal.empresaID.Equals('3'))
                    MessageBox.Show("Para aligerar la carga de los datos, se han limitado los resultados a proyectos generados a partir del 01/01/2015." + Environment.NewLine + "Si desea consultar un proyecto anterior, < 25145, por favor hable con el Dpto Informática.", "Proyecto antiguo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.textBox2.Focus();
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null)
                this.comboBox2.SelectedValue = listBox1.SelectedValue;
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'proyectos_pedido.TC_LANZA_PEDLIN' Puede moverla o quitarla según sea necesario.
            this.tC_LANZA_PEDLINTableAdapter1.Fill(this.proyectos_pedido.TC_LANZA_PEDLIN, empresaGlobal.empresaID, Convert.ToInt32(comboBox2.SelectedValue));


            this.listBox2.ClearSelected();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedValue != null)
                this.comboBox1.SelectedValue = listBox2.SelectedValue;
        }




        private void mostrar_porcentaje(int estadoOF)
        {
            Int32 x = 0;
            Int32 i = 0;

            this.textBox1.Visible = false;

            if (estadoOF == 2) 
            {
                this.textBox1.Visible = true;


                for (i = 0; i < this.dataGridView1.RowCount; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[5].Value.ToString() == "1")
                    {
                        x++;
                    }
                }
                if (i > 0)
                {
                    this.textBox1.Text = Convert.ToString((x * 100) / i) + "%";
                }
            }

        }

/*        
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {           
            if (this.dataGridView1.Rows[e.RowIndex].Cells[3].Value != DBNull.Value)
            {
                if ((Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[3].Value) == 1) && (this.dataGridView1.Rows[e.RowIndex].Cells[0].Style.BackColor != Color.LightGreen))
                {
                    //Cambiar color a una celda                    
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightGreen;
                    
                    //Cambiar color a una fila
                    //dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                }
                else
                {
                    Int32 ctd_lan = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);

                    for (Int32 i = 4; i < this.dataGridView1.Rows[e.RowIndex].Cells.Count; i++)
                    {
                        if ((this.dataGridView1.Rows[e.RowIndex].Cells[i].Value != DBNull.Value) && (Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[i].Value) < ctd_lan))
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells[i].Style.BackColor = Color.Red;
                        }
                    }
                }

                this.dataGridView1.FirstDisplayedCell.Selected = false;
            }
        }
*/

        //Mejor rendimiento que el método CellFormatting
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (this.rePintar == true)  //JRegino 27/01/2016. Asi se evita que se cambie el color de una celda cuando la acaban de fichar manualmente y no se quiere refrescar todavia
            {
                if (this.dataGridView1.Rows[e.RowIndex].Cells[5].Value != DBNull.Value)
                {
                    if ((Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[5].Value) == 1) && (Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[6].Value) == 0) && (this.dataGridView1.Rows[e.RowIndex].Cells[0].Style.BackColor != Color.LightGreen))
                    {
                        this.dataGridView1.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightGreen;    //Si of cerrada y consumo dado => Naranja
                    }
                    else
                    {
                        if (Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[6].Value) >= 1)
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.Orange;    //Cambiar color a naranja si la of tiene pdte de procesar su consumo de MP
                        }



                        //JRegino 27/06/2017. Resaltar la inspección de calidad según el resultado
                        Int32 indiceCalidad = this.dataGridView1.Rows[e.RowIndex].Cells.Count - 1;

                        int caseSwitch = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[indiceCalidad].Value);
                        switch (caseSwitch)
                        {
                            case -1:
                                this.dataGridView1.Rows[e.RowIndex].Cells[indiceCalidad].Style.BackColor = Color.Red;
                                this.dataGridView1.Rows[e.RowIndex].Cells[indiceCalidad].Style.ForeColor = Color.Red;
                                break;
                            case 0:
                                this.dataGridView1.Rows[e.RowIndex].Cells[indiceCalidad].Style.BackColor = Color.White;
                                this.dataGridView1.Rows[e.RowIndex].Cells[indiceCalidad].Style.ForeColor = Color.White;
                                break;
                            case 1:
                                this.dataGridView1.Rows[e.RowIndex].Cells[indiceCalidad].Style.BackColor = Color.Green;
                                this.dataGridView1.Rows[e.RowIndex].Cells[indiceCalidad].Style.ForeColor = Color.Green;                                
                                break;
                            default:                                
                                break;
                        }
                        //JRegino 27/06/2017. Resaltar la inspección de calidad



                        Int32 ctd_lan = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);

                        for (Int32 i = 7; i < this.dataGridView1.Rows[e.RowIndex].Cells.Count-1; i++)
                        {
                            if ((this.dataGridView1.Rows[e.RowIndex].Cells[i].Value != DBNull.Value) && (Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[i].Value) < ctd_lan))
                            {
                                if (Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[i].Value) < 0)    //JRegino 30/05/2013 Caso en el que la of no tiene bono emitido para esa operacion
                                {
                                    this.dataGridView1.Rows[e.RowIndex].Cells[i].Style.BackColor = Color.LightGray;
                                    this.dataGridView1.Rows[e.RowIndex].Cells[i].Style.ForeColor = Color.LightGray;
                                    this.dataGridView1.Rows[e.RowIndex].Cells[i].Style.SelectionBackColor = Color.LightGray;
                                    this.dataGridView1.Rows[e.RowIndex].Cells[i].Style.SelectionForeColor = Color.LightGray;
                                }
                                else
                                {
                                    this.dataGridView1.Rows[e.RowIndex].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                        }
                    }

                    this.dataGridView1.FirstDisplayedCell.Selected = false;
                }
            }
        }




        //mostrar todos los fichajes relacionados con esa OF
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.rePintar = false;
                Form2 form2 = new Form2(Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[1].Value));

                if (form2.ShowDialog() == DialogResult.OK)
                {
                    //this.rePintar = false;
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Yellow;
                 
                    //this.comboBox1_SelectedIndexChanged(this, null);
                }
                else
                    this.rePintar = true;
            }
        }

        //Controlar la pulsacion del boton dcho sobre una marca operacion
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            String usuario = Environment.UserName.ToUpper();
            String usuarios_permitidos = ConfigurationManager.AppSettings["Users_fichar_piezas"].ToString().ToUpper();

            // Load context menu on right mouse click            
            if (e.Button == MouseButtons.Right)
            {
                this.hitTestInfo = this.dataGridView1.HitTest(e.X, e.Y);
                Int32 fila = hitTestInfo.RowIndex;
                Int32 columna = hitTestInfo.ColumnIndex;
                if (columna < 0)
                    return;

                switch (this.hitTestInfo.Type)
                {
                    case (DataGridViewHitTestType.Cell):

                        //Solo se va a mostrar el menu si el campo pulsado no es vacio
                        if (this.dataGridView1.Rows[fila].Cells[columna].Value.ToString() != "")
                        {
                            //Fichar
                            if ((columna >= 6 && columna <= this.dataGridView1.Columns.Count - 4) && (columna != this.dataGridView1.Columns.Count - 5) && (usuarios_permitidos.Contains(usuario)))
                            {
                                contextMenuStrip1.Items[0].Enabled = true;
                                contextMenuStrip1.Items[1].Enabled = true;  
                                contextMenuStrip1.Items[2].Enabled = false;  
                                contextMenuStrip1.Show(this.dataGridView1, new Point(e.X, e.Y));
                            }

                            //Exterior
                            if ((columna == this.dataGridView1.Columns.Count - 5) && (usuarios_permitidos.Contains(usuario)) && Convert.ToInt32(this.dataGridView1.Rows[fila].Cells[columna].Value) == Convert.ToInt32(this.dataGridView1.Rows[fila].Cells[2].Value))
                                //MessageBox.Show(Form.ActiveForm, "eaaaaaaaaaaaa.");
                                contextMenuExterior.Show(this.dataGridView1, new Point(e.X, e.Y));


                            //Galvanizar
                            if ((columna == this.dataGridView1.Columns.Count - 2) && (usuarios_permitidos.Contains(usuario)))
                                //MessageBox.Show(Form.ActiveForm, "eaaaaaaaaaaaa.");
                                contextMenuStrip2.Show(this.dataGridView1, new Point(e.X, e.Y));

                            //Otras
                            if (columna == 1)
                            {
                                if (usuarios_permitidos.Contains(usuario) == false || Convert.ToInt32(this.dataGridView1.Rows[fila].Cells[5].Value) == 1)
                                    contextMenuStrip3.Items[2].Enabled = false;                                    
                                else
                                    contextMenuStrip3.Items[2].Enabled = true;

                                contextMenuStrip3.Show(this.dataGridView1, new Point(e.X, e.Y));
                            }
                            //JRM - ProyectosOnline - NoConformidad
                            //Antes 30
                            if (columna == 31 && this.dataGridView1.Rows[fila].Cells[columna].Value.ToString() == "-1")
                            {
                                contextMenuStripNC.Items[0].Enabled = true;
                                contextMenuStripNC.Show(this.dataGridView1, new Point(e.X, e.Y));
                            }

                        }
                        break;

                    case (DataGridViewHitTestType.ColumnHeader):

                        //Solo se va a mostrar el menu si hay un bono seleccionado, si el usuario tiene permisos y si ha selecionado una operacion válida
                        if ((columna >= 6 && columna <= this.dataGridView1.Columns.Count - 4) && (usuarios_permitidos.Contains(usuario)))
                        {                            
                            Int32 numBono;
                            Int32.TryParse(this.textBox3.Text, out numBono);

                            if (numBono > 0)
                            {
                                //1º) filtrar por si no lo hubiera hecho
                                button1_Click(this, null);

                                    //2º) mostra el menu
                                if ((columna >= 6 && columna <= this.dataGridView1.Columns.Count - 4) && (usuarios_permitidos.Contains(usuario)))
                                {
                                    contextMenuStrip1.Items[0].Enabled = false;
                                    contextMenuStrip1.Items[1].Enabled = false;
                                    contextMenuStrip1.Items[2].Enabled = true;  
                                    contextMenuStrip1.Show(this.dataGridView1, new Point(e.X, e.Y));
                                }
                                                                        
                            }
                        }
                        break;
                }

            }



            #region Hasta el 28/11/2018
            /* 
            // Load context menu on right mouse click            
            if (e.Button == MouseButtons.Right)
            {
                this.hitTestInfo = this.dataGridView1.HitTest(e.X, e.Y);
                if (this.hitTestInfo.RowIndex < 0 || this.hitTestInfo.ColumnIndex < 0)
                    return;


                String usuario = Environment.UserName.ToUpper();
                String usuarios_permitidos = ConfigurationManager.AppSettings["Users_fichar_piezas"].ToString().ToUpper();


                //Solo se va a mostrar el menu si el campo pulsado no es vacio
                if (this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[this.hitTestInfo.ColumnIndex].Value.ToString() != "")
                {
                    if ((this.hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex >= 6 && hitTestInfo.ColumnIndex <= 26) && (usuarios_permitidos.Contains(usuario)))
                        contextMenuStrip1.Show(this.dataGridView1, new Point(e.X, e.Y));

                    if ((this.hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == this.dataGridView1.Columns.Count - 2) && (usuarios_permitidos.Contains(usuario)))
                        contextMenuStrip2.Show(this.dataGridView1, new Point(e.X, e.Y));

                    if (this.hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == 1 && Convert.ToInt32(this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[5].Value) == 0)
                    {
                        if (usuarios_permitidos.Contains(usuario))
                            contextMenuStrip3.Items[2].Enabled = true;
                        else
                            contextMenuStrip3.Items[2].Enabled = false;

                        contextMenuStrip3.Show(this.dataGridView1, new Point(e.X, e.Y));
                    }
                    
                }
            }
            */
            #endregion
        }



        //filtrar por nombre de la marca o del bono
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;

            String marca = this.textBox2.Text.ToUpper();
            String bonoString = this.textBox3.Text;
            Int32 bono = 0;
            
            List<String> listaOFs = new List<String>();

            if (bonoString != "")
            {
                if (Int32.TryParse(bonoString, out bono))
                {
                    SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

                    String strSql = "";
                    SqlCommand sqlCmd;

                    try
                    {
                        if (conexion.State != ConnectionState.Open)
                            conexion.Open();

                        //Obtener las ofs del bono
                        //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString().ToUpper() por empresaGlobal.empresaID
                        strSql = "SELECT distinct ORDFAB ";
                        strSql += "	FROM TC_BONOL ";
                        strSql += "		Where (Codemp = '" + empresaGlobal.empresaID + "') ";
                        strSql += "		    and (TipoReg = 'f') ";
                        strSql += "		    and (Bono = " + bonoString + ") ";

                        sqlCmd = new SqlCommand(strSql, conexion);


                        SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                        while (sqlDR.Read())
                        {
                            listaOFs.Add(sqlDR[0].ToString());
                        }

                        sqlDR.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Form.ActiveForm, "Ocurrió un error al obtener las OFs del Bono: " + ex.ToString());
                        return;
                    }
                    finally
                    {
                        if (conexion.State == ConnectionState.Open)
                            conexion.Close();
                    }
                }
                else
                {
                    MessageBox.Show(Form.ActiveForm, "El bono indicado no es número.");
                    return;
                }
            }
            


            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                this.dataGridView1.Rows[i].Visible = true;

                if (marca != "")
                {
                    if (this.dataGridView1.Rows[i].Cells[0].Value.ToString().ToUpper().Contains(marca) == false)
                        this.dataGridView1.Rows[i].Visible = false;
                }
                if (bonoString != "")
                {
                    if (listaOFs.Contains(this.dataGridView1.Rows[i].Cells[1].Value.ToString()) == false)
                        this.dataGridView1.Rows[i].Visible = false;
                }
            }
            
        }

        //Quitar el filtro de la marca o del bono
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;

            this.textBox2.Text = "";
            this.textBox3.Text = "";

            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                this.dataGridView1.Rows[i].Visible = true;
            }
        }



        private void añadirPiezasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.rePintar = false;
            Form3 carga = new Form3(Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[1].Value), this.dataGridView1.Columns[this.hitTestInfo.ColumnIndex].ToolTipText, Convert.ToInt32(ConfigurationManager.AppSettings["Operario"].ToString()), 0, 0, 0,1);

            //tras una carga posicionar en el mismo lugar
            if (carga.ShowDialog() == DialogResult.OK)
            {                
                //this.rePintar = false;
                this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[this.hitTestInfo.ColumnIndex].Style.BackColor = Color.Yellow;

                //this.comboBox1_SelectedIndexChanged(this, null);
                //this.dataGridView1.CurrentCell = this.dataGridView1[this.hitTestInfo.ColumnIndex, this.hitTestInfo.RowIndex];
            }
            else
                this.rePintar = true;
        }


        //Jregino 29/11/2018
        private void ficharPiezasDelBonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.rePintar = false;
            Form33 carga = new Form33(this.dataGridView1.Columns[this.hitTestInfo.ColumnIndex].ToolTipText, Convert.ToInt32(ConfigurationManager.AppSettings["Operario"].ToString()), Convert.ToInt32(textBox3.Text));
            
            if (carga.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < this.dataGridView1.RowCount; i++)
                {
                    if (this.dataGridView1.Rows[i].Visible == true)
                        this.dataGridView1.Rows[i].Cells[this.hitTestInfo.ColumnIndex].Style.BackColor = Color.Yellow;
                }                    
            }
            else
                this.rePintar = true;
        }




        private void galvanizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.rePintar = false;
            Form4 recep = new Form4(Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[1].Value));

            if (recep.ShowDialog() == DialogResult.OK)
            {
                //this.rePintar = false;
                this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[this.hitTestInfo.ColumnIndex].Style.BackColor = Color.Yellow;            

                //this.comboBox1_SelectedIndexChanged(this, null);
                //this.dataGridView1.CurrentCell = this.dataGridView1[this.hitTestInfo.ColumnIndex, this.hitTestInfo.RowIndex];
            }
            else
                this.rePintar = true;
        }


        // Abrir incidencia pieza errónea
        private void abrirIncidencia_Click(object sender, EventArgs e)
        {

            int colHit = this.hitTestInfo.ColumnIndex;
            int rowHit = this.hitTestInfo.RowIndex;

            System.Diagnostics.Debug.WriteLine("Column seleccionada: " + colHit + " current row es: " + rowHit);
            string nOrdFab = dataGridView1.Rows[rowHit].Cells[1].Value.ToString();

            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            String strSql = "";
            SqlCommand comando;
            SqlDataAdapter adapter;

            //Consulta en la base de datos
            //SELECT ie.NoConformidad FROM [gg].[dbo].[TC_LANZA_ETIQUETA] le JOIN [gg].[dbo].[TC_InspeccionEtiquetas] ie ON le.Etiqueta = ie.Etiqueta WHERE [OrdFab] = '1841600' AND ie.Etiqueta is not null AND ie.NoConformidad is not null;

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                DataTable table = new DataTable();

                strSql = "SELECT ie.NoConformidad FROM TC_LANZA_ETIQUETA le JOIN TC_InspeccionEtiquetas ie ON le.Etiqueta = ie.Etiqueta";
                strSql += " WHERE (OrdFab = '" + nOrdFab + "')";
                strSql += " AND (ie.Etiqueta is not null) AND (ie.NoConformidad is not null) AND (ie.Correcto = '0')";

                comando = new SqlCommand(strSql, conexion);
                adapter = new SqlDataAdapter(comando);

                comando.CommandText = strSql;
                table.Columns.Clear();
                table.Clear();

                adapter.Fill(table);

                string idError = table.Rows[0][0].ToString();

                //MessageBox.Show(Form.ActiveForm, xdxdx + " -nOrdFab es: " + nOrdFab + " || rowHit" + rowHit + " colHit: " + colHit); //

                string cleanStr = idError;
                
                //cleanStr = Regex.Match(cleanStr, "\\d{2}\\w\\d{0,9}+").Value;
                cleanStr = Regex.Match(cleanStr, "[0-9]{2}[Tt][0-9]+").Value;
                //MessageBox.Show(Form.ActiveForm, idError + " pasa a ser " + cleanStr + " -nOrdFab es: " + nOrdFab + " || rowHit" + rowHit + " colHit: " + colHit); //
                if (cleanStr.Equals("") || cleanStr.Equals(null))
                {
                    MessageBox.Show(Form.ActiveForm, "No existe parte de No Conformidad asociado: " + idError);
                }
                else
                {
                    System.Diagnostics.Process.Start("http://sistemacalidad/IX-PGI-05/PARTES%20DE%20NO%20CONFORMIDAD/" + cleanStr + ".xml");
                }

                //http://sistemacalidad/IX-PGI-05/PARTES%20DE%20NO%20CONFORMIDAD/20T48.xml
                //System.Diagnostics.Process.Start("http://google.com");
                
                conexion.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, "No hay datos disponibles"); // + ex.ToString());
            }
            
        }


        private void abrirPlanoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID;
            String empresa = empresaGlobal.empresaID;//ConfigurationManager.AppSettings["EMPRESA"].ToString();
            String usuario = Environment.UserName.ToUpper();
            String codigo = this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[0].Value.ToString();
            String path = ConfigurationManager.AppSettings["url_DestinoPlanos"].ToString().ToUpper();

            ProyectosOnLine.WebService_Planos.Service ServicioWeb = new ProyectosOnLine.WebService_Planos.Service();
            String Resultado = ServicioWeb.ObtenerFile2(codigo, empresa, usuario);


            if (Resultado.StartsWith("-1"))
                MessageBox.Show(Form.ActiveForm, "Error al obtener el plano del código: " + Environment.NewLine + Resultado);
            else
            {
                //System.Diagnostics.Process.Start("IExplore.exe", path + "\\" + usuario + "\\" + Resultado); 
                System.Diagnostics.Process.Start("msedge.exe", path + "\\" + usuario + "\\" + Resultado);
            }


            ServicioWeb.Dispose();
        }

        private void mostrarBonosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 bonos = new Form5(Convert.ToInt32(this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[1].Value));
            bonos.ShowDialog();
        }

        private void cerrarOrdFabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Form.ActiveForm, "Está seguro que desea cerrar la OF???", "Cierre de OF", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show(Form.ActiveForm, "Una vez cerrada no se podrá hacer nada con ella", "Cierre de OF. Confirmación!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

                    String strSql = "";
                    SqlCommand sqlCmd;

                    try
                    {
                        if (conexion.State != ConnectionState.Open)
                            conexion.Open();

                        //Cerrar la OF
                        //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                        strSql = "Update T_ORDFAB ";
                        strSql += "	 set FLAG = 1, usumod = 'T-" + Environment.UserName + "', fecham = getdate()";
                        strSql += "		Where (Codemp = '" + empresaGlobal.empresaID + "') ";
                        strSql += "		    and (TipoReg = 'f') ";
                        strSql += "		    and (OrdFab = " + Convert.ToInt32(this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[1].Value) + ") ";
                        


                        sqlCmd = new SqlCommand(strSql, conexion);
                        sqlCmd.ExecuteScalar();


                        this.rePintar = false;
                        this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[this.hitTestInfo.ColumnIndex].Style.BackColor = Color.Yellow;            
                        //this.comboBox1_SelectedIndexChanged(this, null);
                        //this.dataGridView1.CurrentCell = this.dataGridView1[this.hitTestInfo.ColumnIndex, this.hitTestInfo.RowIndex];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Form.ActiveForm, "Ocurrió un error al cerrar la Orden de Fabricación: " + ex.ToString());
                    }
                    finally
                    {
                        if (conexion.State == ConnectionState.Open)
                            conexion.Close();
                    }

                }
            }
        }

        

        //JRegino 27/01/2016. Si se pulsa F5, refrescar datos
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && this.comboBox1.Text != "")
                this.comboBox1_SelectedIndexChanged(this, null);
        }

        //Jpedrero 25/04/2016, nueva funcionalidad que permite modificar la máquina de un bono
        private void button3_Click(object sender, EventArgs e)
        {
            proyecto = "";
          
            if (comboBox1.SelectedIndex > -1)
            {
                proyecto = comboBox1.SelectedValue.ToString();
            }
            
            bono = this.textBox3.Text;
            

            Form6 frm = new Form6();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
         }

        #region Proyecto PARADO

        private void ConsultarEstadoProyecto()
        {
            if (comboBox1.SelectedItem != null)
            {
                SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

                String strSql = "";
                SqlCommand sqlCmd;

                try
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();

                    //Cerrar la OF
                    //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                    strSql = " Select PARADO from TC_LANZA ";
                    strSql += " Where Codemp = '" + empresaGlobal.empresaID + "' ";
                    strSql += "		  and codlanza =  " + comboBox1.SelectedValue;

                    sqlCmd = new SqlCommand(strSql, conexion);

                    proyectoParado =Convert.ToBoolean(sqlCmd.ExecuteScalar());
                    ComprobarEstadoProyecto();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Form.ActiveForm, "Ocurrió un error al activar/parar el proyecto: " + ex.ToString());
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Close();
                }
            }
        }



        private void btnParado_Click(object sender, EventArgs e)
        {            
            if (comboBox1.SelectedItem != null)
            {
                proyectoParado = !proyectoParado;

                SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

                String strSql = "";
                SqlCommand sqlCmd;

                try
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();

                    //Cerrar la OF
                    //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                    strSql = "Update TC_LANZA ";
                    strSql += "	 set PARADO = " + (proyectoParado ?1:0) + ", usumod = 'T-" + Environment.UserName + "', fecham = getdate()";
                    strSql += "		Where Codemp = '" + empresaGlobal.empresaID + "' ";
                    strSql += "		    and codlanza =  " + comboBox1.SelectedValue;
                   



                    sqlCmd = new SqlCommand(strSql, conexion);
                    sqlCmd.ExecuteScalar();
                    ComprobarEstadoProyecto();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Form.ActiveForm, "Ocurrió un error al activar/parar el proyecto: " + ex.ToString());
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Close();
                }
            }
        }

       /* private void abrirPlanoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Probando: ");
            //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID;
            /*String empresa = empresaGlobal.empresaID;//ConfigurationManager.AppSettings["EMPRESA"].ToString();
            String usuario = Environment.UserName.ToUpper();
            String codigo = this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[0].Value.ToString();
            String path = ConfigurationManager.AppSettings["url_DestinoPlanos"].ToString().ToUpper();

            ProyectosOnLine.WebService_Planos.Service ServicioWeb = new ProyectosOnLine.WebService_Planos.Service();
            String Resultado = ServicioWeb.ObtenerFile2(codigo, empresa, usuario);


            if (Resultado.StartsWith("-1"))
                MessageBox.Show(Form.ActiveForm, "Error al obtener el plano del código: " + Environment.NewLine + Resultado);
            else
            {
                System.Diagnostics.Process.Start("IExplore.exe", path + "\\" + usuario + "\\" + Resultado);
            }


            ServicioWeb.Dispose();*/
        //}


        /*        private void dataGridView1_MouseClick(object sender, DataGridViewCellEventArgs e, MouseEventArgs k)
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
        }*/

        private void ComprobarEstadoProyecto()
        {
          /*
            lblParado.Visible = proyectoParado;
            String usuario = Environment.UserName.ToUpper();
            String usuarios_permitidos = ConfigurationManager.AppSettings["Users_fichar_piezas"].ToString().ToUpper();
            if (usuarios_permitidos.Contains(usuario))
                btnParado.Visible = true;
            if (proyectoParado)
            {
                btnParado.Text = "Activar";
                this.dataGridView1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;


            }
            else
            {
                btnParado.Text = "Parar";
                this.dataGridView1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
          */
        }
        #endregion

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            this.hitTestInfo = this.dataGridView1.HitTest(e.X, e.Y);
            Debug.AutoFlush = true;

            if (this.hitTestInfo != null)
            {
                if (this.hitTestInfo.RowIndex != -1 && this.hitTestInfo.ColumnIndex != -1 && this.hitTestInfo.ColumnX != -1 && this.hitTestInfo.RowY != -1)
                {
                    ordfabGlobal = Convert.ToString(dataGridView1[1, this.hitTestInfo.RowIndex].Value);

                }
                else { Console.WriteLine("hitTest fuera de límites"); }
                Console.WriteLine(" ------------ Form1 ordGlobal es: " + ordfabGlobal);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }

        private void btnTodas_Click(object sender, EventArgs e)
        {
            cbM1.Checked = true;
            cbM2.Checked = true;
            cbM3.Checked = true;
            cbM4.Checked = true;
            cbM5.Checked = true;
            cbCS.Checked = true;
        }

        private void btnDesmarcar_Click(object sender, EventArgs e)
        {
            cbM1.Checked = false;
            cbM2.Checked = false;
            cbM3.Checked = false;
            cbM4.Checked = false;
            cbM5.Checked = false;
            cbCS.Checked = false;
        }

        private void añadirTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> listaOF = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!(String.IsNullOrEmpty((Convert.ToString(row.Cells[this.hitTestInfo.ColumnIndex].Value)))))
                {
                    listaOF.Add(Convert.ToInt32(row.Cells[1].Value));
                    
                }
            }

            this.rePintar = false;
            Form3 carga = new Form3(listaOF, this.dataGridView1.Columns[this.hitTestInfo.ColumnIndex].ToolTipText, Convert.ToInt32(ConfigurationManager.AppSettings["Operario"].ToString()), 0, 0, 0,2);

            //tras una carga posicionar en el mismo lugar
            if (carga.ShowDialog() == DialogResult.OK)
            {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (!(String.IsNullOrEmpty((Convert.ToString(row.Cells[this.hitTestInfo.ColumnIndex].Value)))))
                        {
                            row.Cells[this.hitTestInfo.ColumnIndex].Style.BackColor = Color.Yellow;
                        }

                    }

            }
            else
                this.rePintar = true;

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void recuperarOperacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> listaOF = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!(String.IsNullOrEmpty((Convert.ToString(row.Cells[this.hitTestInfo.ColumnIndex].Value)))) && (Convert.ToInt32(row.Cells[this.hitTestInfo.ColumnIndex].Value)) == (Convert.ToInt32(row.Cells[2].Value)))
                {
                    listaOF.Add(Convert.ToInt32(row.Cells[1].Value));

                }
            }


            this.rePintar = false;

           // Form3 carga = new Form3(Convert.ToInt32(this.dataGridView1.Rows[this.hitTestInfo.RowIndex].Cells[1].Value), this.dataGridView1.Columns[this.hitTestInfo.ColumnIndex].ToolTipText, Convert.ToInt32(ConfigurationManager.AppSettings["Operario"].ToString()), 0, 0, 0, 1);
            Form35 carga = new Form35(listaOF, this.dataGridView1.Columns[this.hitTestInfo.ColumnIndex].ToolTipText);

            //tras una carga posicionar en el mismo lugar
            if (carga.ShowDialog() == DialogResult.OK)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (!(String.IsNullOrEmpty((Convert.ToString(row.Cells[this.hitTestInfo.ColumnIndex].Value)))))
                    {
                        row.Cells[this.hitTestInfo.ColumnIndex].Style.BackColor = Color.Yellow;
                    }

                }
            }
            else
                this.rePintar = true;




        }

        private String obtenerPlanCalidad(string proyecto)
        {
            string plan = "";
            String strSql = "";

            strSql = "select gg.dbo.IME_Obtener_Plan_Calidad_Formulario ('3','proyecto','" + proyecto + "') ";

            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString()))
            {
                SqlCommand command = new SqlCommand(strSql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        plan = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }

            }

            return plan;

        }

        private void E_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            // Creamos un objeto Excel.
            Excel.Application Mi_Excel = default(Excel.Application);
            // Creamos un objeto WorkBook. Para crear el documento Excel.           
            Excel.Workbook LibroExcel = default(Excel.Workbook);
            // Creamos un objeto WorkSheet. Para crear la hoja del documento.
            Excel.Worksheet HojaExcel = default(Excel.Worksheet);

            // Iniciamos una instancia a Excel, y Hacemos visibles para ver como se va creando el reporte, 
            // podemos hacerlo visible al final si se desea.
            Mi_Excel = new Excel.Application();
            Mi_Excel.Visible = true;

            /* Ahora creamos un nuevo documento y seleccionamos la primera hoja del 
             * documento en la cual crearemos nuestro informe. 
             */
            // Creamos una instancia del Workbooks de excel.            
            LibroExcel = Mi_Excel.Workbooks.Add();
            // Creamos una instancia de la primera hoja de trabajo de excel            
            HojaExcel = LibroExcel.Worksheets[1];
            HojaExcel.Visible = Excel.XlSheetVisibility.xlSheetVisible;

            // Hacemos esta hoja la visible en pantalla 
            // (como seleccionamos la primera esto no es necesario
            // si seleccionamos una diferente a la primera si lo
            // necesitariamos).
            HojaExcel.Activate();

            // Crear el encabezado de nuestro informe.
            // La primera línea une las celdas y las convierte un en una sola.            
            // HojaExcel.Range["A1:E1"].Merge();
            // La segunda línea Asigna el nombre del encabezado.
            //HojaExcel.Range["A1:E1"].Value = "----------------------------------------------";
            // La tercera línea asigna negrita al titulo.
            //HojaExcel.Range["A1:E1"].Font.Bold = true;
            // La cuarta línea signa un Size a titulo de 15.
            //HojaExcel.Range["A1:E1"].Font.Size = 15;

            // Crear el subencabezado de nuestro informe

            String proyecto = comboBox1.SelectedValue.ToString();
            HojaExcel.Range["A1:AD1"].Merge();
            HojaExcel.Range["A1:AD1"].Value = " DATOS DEL PROYECTO " + proyecto;
            HojaExcel.Range["A1:AD1"].Font.Italic = true;
            HojaExcel.Range["A1:AD1"].Font.Bold = true;
            HojaExcel.Range["A1:AD1"].Font.Size = 17;
            HojaExcel.Range["A1:AD1"].Interior.Color = System.Drawing.Color.Aquamarine;


            Excel.Range objCelda = HojaExcel.Range["A2", Type.Missing];
            objCelda.Value = "CODIGO";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["B2", Type.Missing];
            objCelda.Value = "OrdFab";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["C2", Type.Missing];
            objCelda.Value = "Ctd";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["D2", Type.Missing];
            objCelda.Value = "Materia Prima";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["E2", Type.Missing];
            objCelda.Value = "Long";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["F2", Type.Missing];
            objCelda.Value = "Fice";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["G2", Type.Missing];
            objCelda.Value = "Serr";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["H2", Type.Missing];
            objCelda.Value = "Fres";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["I2", Type.Missing];
            objCelda.Value = "Geka";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["J2", Type.Missing];
            objCelda.Value = "Pleg";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["K2", Type.Missing];
            objCelda.Value = "Plasm";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["L2", Type.Missing];
            objCelda.Value = "Ciza";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["M2", Type.Missing];
            objCelda.Value = "Punz";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["N2", Type.Missing];
            objCelda.Value = "Gran";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["O2", Type.Missing];
            objCelda.Value = "FresCH";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["P2", Type.Missing];
            objCelda.Value = "BiselCH";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["Q2", Type.Missing];
            objCelda.Value = "PlegCH";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["R2", Type.Missing];
            objCelda.Value = "P2";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["S2", Type.Missing];
            objCelda.Value = "P3";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["T2", Type.Missing];
            objCelda.Value = "Retala";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["U2", Type.Missing];
            objCelda.Value = "Manu";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["V2", Type.Missing];
            objCelda.Value = "PunAS";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["W2", Type.Missing];
            objCelda.Value = "SolAS";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["X2", Type.Missing];
            objCelda.Value = "Punteo";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["Y2", Type.Missing];
            objCelda.Value = "Sold";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["Z2", Type.Missing];
            objCelda.Value = "Exte";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["AA2", Type.Missing];
            objCelda.Value = "Expe";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["AB2", Type.Missing];
            objCelda.Value = "Galv";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["AC2", Type.Missing];
            objCelda.Value = "Rece";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;

            objCelda = HojaExcel.Range["AD2", Type.Missing];
            objCelda.Value = "Calid";
            objCelda.Interior.Color = System.Drawing.Color.Aqua;
            objCelda.Font.Bold = true;
            objCelda.Font.Size = 13;


            objCelda.EntireColumn.NumberFormat = "###,###,###.00";


            int i = 3;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                string A = "A" + i.ToString();
                objCelda = HojaExcel.Range[A, Type.Missing];
                objCelda.Value = row.Cells[0].Value.ToString();


                string B = "B" + i.ToString();
                objCelda = HojaExcel.Range[B, Type.Missing];
                objCelda.Value = row.Cells[1].Value.ToString();

                string C = "C" + i.ToString();
                objCelda = HojaExcel.Range[C, Type.Missing];
                objCelda.Value = row.Cells[2].Value.ToString();

             

                string D = "D" + i.ToString();
                objCelda = HojaExcel.Range[D, Type.Missing];
                objCelda.Value = row.Cells[3].Value.ToString();

             

                string E = "E" + i.ToString();
                objCelda = HojaExcel.Range[E, Type.Missing];
                objCelda.Value = row.Cells[4].Value.ToString();

              

                string F = "F" + i.ToString();
                objCelda = HojaExcel.Range[F, Type.Missing];
                objCelda.Value = row.Cells[7].Value.ToString();

                if (row.Cells[7].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string G = "G" + i.ToString();
                objCelda = HojaExcel.Range[G, Type.Missing];
                objCelda.Value = row.Cells[8].Value.ToString();

                if (row.Cells[8].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string H = "H" + i.ToString();
                objCelda = HojaExcel.Range[H, Type.Missing];
                objCelda.Value = row.Cells[9].Value.ToString();

                if (row.Cells[9].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string I = "I" + i.ToString();
                objCelda = HojaExcel.Range[I, Type.Missing];
                objCelda.Value = row.Cells[10].Value.ToString();

                if (row.Cells[10].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string J = "J" + i.ToString();
                objCelda = HojaExcel.Range[J, Type.Missing];
                objCelda.Value = row.Cells[11].Value.ToString();

                if (row.Cells[11].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string K = "K" + i.ToString();
                objCelda = HojaExcel.Range[K, Type.Missing];
                objCelda.Value = row.Cells[12].Value.ToString();

                if (row.Cells[12].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string L = "L" + i.ToString();
                objCelda = HojaExcel.Range[L, Type.Missing];
                objCelda.Value = row.Cells[13].Value.ToString();

                if (row.Cells[13].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string M = "M" + i.ToString();
                objCelda = HojaExcel.Range[M, Type.Missing];
                objCelda.Value = row.Cells[14].Value.ToString();

                if (row.Cells[14].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string N = "N" + i.ToString();
                objCelda = HojaExcel.Range[N, Type.Missing];
                objCelda.Value = row.Cells[15].Value.ToString();

                if (row.Cells[15].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string O = "O" + i.ToString();
                objCelda = HojaExcel.Range[O, Type.Missing];
                objCelda.Value = row.Cells[16].Value.ToString();

                if (row.Cells[16].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string P = "P" + i.ToString();
                objCelda = HojaExcel.Range[P, Type.Missing];
                objCelda.Value = row.Cells[17].Value.ToString();

                if (row.Cells[17].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string Q = "Q" + i.ToString();
                objCelda = HojaExcel.Range[Q, Type.Missing];
                objCelda.Value = row.Cells[18].Value.ToString();

                if (row.Cells[18].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string R = "R" + i.ToString();
                objCelda = HojaExcel.Range[R, Type.Missing];
                objCelda.Value = row.Cells[19].Value.ToString();

                if (row.Cells[19].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string S = "S" + i.ToString();
                objCelda = HojaExcel.Range[S, Type.Missing];
                objCelda.Value = row.Cells[20].Value.ToString();

                if (row.Cells[20].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string T = "T" + i.ToString();
                objCelda = HojaExcel.Range[T, Type.Missing];
                objCelda.Value = row.Cells[21].Value.ToString();

                if (row.Cells[21].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string U = "U" + i.ToString();
                objCelda = HojaExcel.Range[U, Type.Missing];
                objCelda.Value = row.Cells[22].Value.ToString();

                if (row.Cells[22].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string V = "V" + i.ToString();
                objCelda = HojaExcel.Range[V, Type.Missing];
                objCelda.Value = row.Cells[23].Value.ToString();

                if (row.Cells[23].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string W = "W" + i.ToString();
                objCelda = HojaExcel.Range[W, Type.Missing];
                objCelda.Value = row.Cells[24].Value.ToString();

                if (row.Cells[24].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string X = "X" + i.ToString();
                objCelda = HojaExcel.Range[X, Type.Missing];
                objCelda.Value = row.Cells[25].Value.ToString();

                if (row.Cells[25].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string Y = "Y" + i.ToString();
                objCelda = HojaExcel.Range[Y, Type.Missing];
                objCelda.Value = row.Cells[26].Value.ToString();

                if (row.Cells[26].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string Z = "Z" + i.ToString();
                objCelda = HojaExcel.Range[Z, Type.Missing];
                objCelda.Value = row.Cells[27].Value.ToString();

                if (row.Cells[27].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string AA = "AA" + i.ToString();
                objCelda = HojaExcel.Range[AA, Type.Missing];
                objCelda.Value = row.Cells[28].Value.ToString();

                if (row.Cells[28].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string AB = "AB" + i.ToString();
                objCelda = HojaExcel.Range[AB, Type.Missing];
                objCelda.Value = row.Cells[29].Value.ToString();

                if (row.Cells[29].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string AC = "AC" + i.ToString();
                objCelda = HojaExcel.Range[AC, Type.Missing];
                objCelda.Value = row.Cells[30].Value.ToString();

                if (row.Cells[30].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                string AD = "AD" + i.ToString();
                objCelda = HojaExcel.Range[AD, Type.Missing];
                objCelda.Value = row.Cells[31].Value.ToString();

                if (row.Cells[31].Value.ToString() == "0") objCelda.Interior.Color = System.Drawing.Color.Red;

                i++;

          

                Cursor.Current = Cursors.Default;

            }

        }

        private void cbSinExpedicion_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSinExpedicion.Checked == true)
            {
                radioButton3.Checked = true;
                groupBox3.Enabled = false;
                
                cbM1.Checked = true;
                cbM2.Checked = true;
                cbM3.Checked = true;
                cbM4.Checked = true;
                cbM5.Checked = true;
                cbCS.Checked = true;
                gbFiltroCategoria.Enabled = false;

                rbTodasExp.Checked = true;
                groupBox4.Enabled = false;

            }
            else {
                radioButton3.Checked = true;
                groupBox3.Enabled = true;

                cbM1.Checked = true;
                cbM2.Checked = true;
                cbM3.Checked = true;
                cbM4.Checked = true;
                cbM5.Checked = true;
                cbCS.Checked = true;
                gbFiltroCategoria.Enabled = true;

                rbTodasExp.Checked = true;
                groupBox4.Enabled = true;
            
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
           

            Form11 f11 = new Form11(proyecto,nproyecto);
            DialogResult res = f11.ShowDialog();

            if (res == DialogResult.OK)
            {
                //recuperando la variable publica del formulario 2
                //min1.Text = f5g.galmin; //asignamos al texbox el dato de la variable
                //med1.Text = f5g.galmed; //asignamos al texbox el dato de la variable
                //max1.Text = f5g.galmax; //asignamos al texbox el dato de la variable
            }
        }
    }
}