using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using empresaGlobalProj;
using Utils;

namespace PackingNuevo
{
    public partial class frmPaquetes : Form
    {

        #region atributos de la clase
        private String CodEmp = empresaGlobal.empresaID;
        private String PDA;
        private Int32 Operario1;
        private String Operario2;
        private Int32 Packing;
        private String Operacion;
        private String Puesto;

        private bool tempFichaje = true;

        private System.Data.SqlClient.SqlConnection conn = new SqlConnection(Utils.CD.getConexion());

        DataTable paquetes = new DataTable();

        //Escaner
        /*private ScannerServicesDriver scsDriver = null;
        private Scanner scanner = null;*/
        #endregion



        public frmPaquetes(String dispositivo, Int32 numeroPersonal1, String numeroPersonal2, Int32 numeroPacking, String operac, String maquina)
        {
            InitializeComponent();

            this.PDA = dispositivo;
            this.Operario1 = numeroPersonal1;
            this.Operario2 = numeroPersonal2;
            this.Packing = numeroPacking;
            this.Operacion = operac;
            this.Puesto = maquina;
        }

        private void frmPaquetes_Load(object sender, EventArgs e)
        {

            if (this.Operacion == "900001")
            {
                this.Text = "Montaje";
                label2.Text = "Paquetes Montados";
            }
            else
            {
                this.Text = "Carga";
                label2.Text = "Paquetes Cargados";
            }


            this.rellenarGrid();

            /*try
            {

                scsDriver = new ScannerServicesDriver();
                scanner = new Scanner(scsDriver);

                // Register a callback for scanned data...
                scanner.ScanCompleteEvent += new ScanCompleteEventHandler(scanner_ScanCompleteEvent);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al habilitar el escaner: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }*/

            this.textBox1.Focus();
        }

        #region Métodos del escaner
        // This is the registered callback, but we don't want to do any real work here... Do all of your work, including enabling/disabling the scanner from ScannerCallback
        /* delegate void scanner_ScanCompleteDelegate(object sender, ScanCompleteEventArgs e);
         void scanner_ScanCompleteEvent(object sender, PsionTeklogix.Barcode.ScanCompleteEventArgs e)
         {
             if (InvokeRequired)
             {
                 Invoke(new scanner_ScanCompleteDelegate(scanner_ScanCompleteEvent), new object[] { sender, e });
                 return;
             }

             BeginInvoke(new ScannerCallbackDelegate(ScannerCallback), new object[] { e });
         }


         // Here is where you want to do any type of database or any other type of lengthy operation that requires scanned data... You can also disable/enable the scanner from here to ensure that the user cannot operate the scanner during this time of processing...
         delegate void ScannerCallbackDelegate(ScanCompleteEventArgs e);
         private void ScannerCallback(ScanCompleteEventArgs e)
         {
             try
             {
                 // you can safely disable the scanner and then do whatever you like
                 scanner.Enabled = false;

                 if (textBox1.Focused == true)
                 {
                     string[] aux = e.Text.Split('-');

                     if (aux.Length != 3)
                     {
                         MessageBox.Show("La lectura no corresponde a un código de barras válido", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                         this.textBox1.Text = String.Empty;
                     }
                     else
                     {
                         this.textBox1.Text = e.Text;
                         this.dataGrid1.Focus();
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("La lectura de la etiqueta produjo una excepxion. " + ex.Message, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
             }
             finally
             {
                 //System.Threading.Thread.Sleep(1000);     //dejar deshabilitado 1 segundo para que no se produzca una doble lectura

                 // Re-enable the scanner
                 scanner.Enabled = true;
             }
         }*/
        #endregion




        private bool existePaquete(Int32 Paquete)
        {
            int valor = 0;

            try
            {
                String strSql;
                this.conn.Open();

                strSql = "SELECT count(*) ";
                strSql += " 	FROM TC_PACKING_LIST_PAQUETES ";
                strSql += " 	WHERE (CODEMP = '" + this.CodEmp + "') AND (PACKING = " + this.Packing.ToString() + ") AND (PAQUETE = " + Paquete.ToString() + ")";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                valor = Convert.ToInt32(comando.ExecuteScalar());
                comando.Dispose();

                if (valor > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }

            return false;
        }

        private bool paqueteAlmacenado(Int32 Paquete)
        {
            try
            {
                String strSql;
                this.conn.Open();

                strSql = "SELECT count(*) ";
                strSql += " 	FROM TC_PACKING_LIST_PAQUETES_Operaciones ";
                strSql += " 	WHERE (CODEMP = '" + this.CodEmp + "') AND (PACKING = " + this.Packing + ") AND (OPERAC = " + this.Operacion + ") AND (PAQUETE = " + Paquete.ToString() + ")";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                int valor = Convert.ToInt32(comando.ExecuteScalar());
                comando.Dispose();

                if (valor > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }

            return false;
        }


        private bool existeBulto(Int32 Bulto)
        {
            int valor = 0;

            try
            {
                String strSql;
                this.conn.Open();

                strSql = "SELECT count(*) ";
                strSql += " 	FROM TC_PACKING_LIST_CONTENEDORES_BULTOS ";
                strSql += " 	WHERE (CODEMP = '" + this.CodEmp + "') AND (PACKING = " + this.Packing.ToString() + ") AND (BULTO = " + Bulto.ToString() + ")";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                valor = Convert.ToInt32(comando.ExecuteScalar());
                comando.Dispose();

                if (valor > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }

            return false;
        }




        private void rellenarGrid()
        {
            try
            {

                this.dataGrid1.DataSource = null;
                this.dataGrid1.Rows.Clear();

                DataTable table = this.CreateDataTable();




                /*DataGridTableStyle dgts = new DataGridTableStyle();
                dgts.MappingName = table.TableName;

                DataGridCustomColumns.DataGridCustomTextBoxColumn paqColumn = new DataGridCustomColumns.DataGridCustomTextBoxColumn();
                paqColumn.Owner = this.dataGrid1;
                paqColumn.HeaderText = "Paq.";
                paqColumn.MappingName = "Paquete";
                paqColumn.Width = this.dataGrid1.Width * 15 / 100;         //% of the grid size
                paqColumn.AlternatingBackColor = Color.LightGray;
                paqColumn.ReadOnly = true;
                dgts.GridColumnStyles.Add(paqColumn);

                DataGridCustomColumns.DataGridCustomTextBoxColumn fecColumn = new DataGridCustomColumns.DataGridCustomTextBoxColumn();
                fecColumn.Owner = this.dataGrid1;
                fecColumn.HeaderText = "Fecha";
                fecColumn.MappingName = "Fecha";
                fecColumn.Width = this.dataGrid1.Width * 45 / 100;
                fecColumn.AlternatingBackColor = Color.LightGray;
                fecColumn.ReadOnly = true;
                dgts.GridColumnStyles.Add(fecColumn);

                DataGridCustomColumns.DataGridCustomTextBoxColumn ope1Column = new DataGridCustomColumns.DataGridCustomTextBoxColumn();
                ope1Column.Owner = this.dataGrid1;
                ope1Column.HeaderText = "Ope1";
                ope1Column.MappingName = "Operario1";
                ope1Column.Width = this.dataGrid1.Width * 15 / 100;
                ope1Column.AlternatingBackColor = Color.LightGray;
                ope1Column.ReadOnly = true;
                dgts.GridColumnStyles.Add(ope1Column);

                DataGridCustomColumns.DataGridCustomTextBoxColumn ope2Column = new DataGridCustomColumns.DataGridCustomTextBoxColumn();
                ope2Column.Owner = this.dataGrid1;
                ope2Column.HeaderText = "Ope2";
                ope2Column.MappingName = "Operario2";
                ope2Column.Width = this.dataGrid1.Width * 15 / 100;
                ope2Column.AlternatingBackColor = Color.LightGray;
                ope2Column.ReadOnly = true;
                dgts.GridColumnStyles.Add(ope2Column);


                DataGridCustomColumns.DataGridCustomCheckBoxColumn chkColumn = new DataGridCustomColumns.DataGridCustomCheckBoxColumn();
                chkColumn.Owner = this.dataGrid1;
                chkColumn.threeState = false;
                chkColumn.HeaderText = "Del";
                chkColumn.MappingName = "Del";
                chkColumn.Width = this.dataGrid1.Width * 10 / 100;
                chkColumn.AlternatingBackColor = Color.LightGray;
                chkColumn.ReadOnly = false;
                chkColumn.Alignment = HorizontalAlignment.Center;
                dgts.GridColumnStyles.Add(chkColumn);



                this.dataGrid1.TableStyles.Add(dgts);*/

                this.dataGrid1.RowHeadersVisible = false;
                this.dataGrid1.ColumnHeadersVisible = true;


                this.dataGrid1.DataSource = table;
                dataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGrid1.Columns[4].Width = 150;
                dataGrid1.Columns[1].Width = 300;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }
        }

        private DataTable CreateDataTable()
        {
            try
            {
                String strSql;
                this.conn.Open();

                strSql = "SELECT Paquete, Fecha, NumeroPersonal as Operario1, NumeroPersonal2 as Operario2";
                strSql += " 	FROM TC_PACKING_LIST_PAQUETES_Operaciones";
                strSql += " 	WHERE (CODEMP = '" + this.CodEmp + "') AND (PACKING = " + this.Packing + ") AND (OPERAC = '" + this.Operacion + "')";
                strSql += " 	ORDER BY PAQUETE DESC";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                DataTable table = new DataTable("Paquetes");

                //SqlDataReader reader = sqlCmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(table);

                table.Columns.Add(new DataColumn("Del", typeof(bool)));

                foreach (DataRow row in table.Rows)
                {
                    row["Del"] = false;
                }

                this.conn.Close();

                return table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }

            return null;
        }




        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (tempFichaje || CodEmp != "60")
            {
                Int32 packingLectura = -1;
                Int32 num;


                if (this.textBox1.Text == String.Empty)
                {
                    MessageBox.Show("Debe leer el codigo de barras de un paquete o de un bulto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    this.textBox1.Focus();
                    return;
                }

                Boolean esPaquete;
                String lectura = this.textBox1.Text.ToUpper();


                if (lectura.IndexOf("B") >= 0)
                    esPaquete = false;
                else
                    esPaquete = true;


                lectura = lectura.Replace("B", "");


                string[] aux = lectura.Split('-');
                if (aux.Length == 1)
                    num = Convert.ToInt32(aux[0].Trim());
                else
                {
                    packingLectura = Convert.ToInt32(aux[0].Trim());
                    num = Convert.ToInt32(aux[1].Trim());
                }

                if (packingLectura != -1 && this.Packing != packingLectura)
                {
                    MessageBox.Show("El codigo de barras leido no pertenece al packing indicado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    this.textBox1.Text = String.Empty;
                    this.textBox1.Focus();
                    return;
                }


                //Caso Paquete
                if (esPaquete == true)
                {
                    if (this.existePaquete(num) == false)
                    {
                        MessageBox.Show("El paquete no existe en ese packing", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        this.textBox1.Text = String.Empty;
                        this.textBox1.Focus();
                        return;
                    }
                    if (this.paqueteAlmacenado(num) == true)
                    {
                        MessageBox.Show("El paquete ya esta guardado para esa operacion en ese packing", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        this.textBox1.Text = String.Empty;
                        this.textBox1.Focus();
                        return;
                    }



                    if (this.guardarPaquete(num) == true)
                    {
                        MessageBox.Show("Paquete nº: " + num.ToString() + " guardado correctamente", "GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        this.textBox1.Text = String.Empty;
                        this.textBox1.Focus();
                        tempFichaje = false;
                        this.timer1.Start();
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar el paquete: " + num.ToString() + ". Si el error persiste hable con el Dpto Expediciones.");
                    }
                }
                //Caso Bulto
                else
                {
                    if (this.existeBulto(num) == false)
                    {
                        MessageBox.Show("El bulto no existe en ese packing", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        this.textBox1.Text = String.Empty;
                        this.textBox1.Focus();
                        return;
                    }


                    if (this.guardarPaquetesBulto(num) == true)
                    {
                        MessageBox.Show("Paquetes del Bulto nº: " + num.ToString() + " guardados correctamente", "GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        this.textBox1.Text = String.Empty;
                        this.textBox1.Focus();
                        tempFichaje = false;
                        this.timer1.Start();
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar el bulto: " + num.ToString() + ". Si el error persiste hable con el Dpto Expediciones.");
                    }

                }


                this.rellenarGrid();
            }
            else
            {
                MessageBox.Show("El tiempo transcurrido entre fichajes de paquetes es demasiado bajo. Inténtelo de nuevo más tarde");
            }
        }

        private bool guardarPaquete(Int32 paquete)
        {
            try
            {
                String strSql;
                this.conn.Open();

                strSql = "INSERT INTO TC_PACKING_LIST_PAQUETES_Operaciones (CODEMP, PACKING, PAQUETE, OPERAC, PUESTO, FECHA, NumeroPersonal, NumeroPersonal2, FECHAC, USUCRE) ";
                strSql += " VALUES ('" + this.CodEmp + "', " + this.Packing.ToString() + ", " + paquete + ", ";
                strSql += " '" + this.Operacion + "', '" + this.Puesto + "', getdate(), " + this.Operario1 + ", ";

                if (this.Operario2 == String.Empty)
                    strSql += " null ,";
                else
                    strSql += this.Operario2 + ", ";

                strSql += " getdate(), '" + this.PDA + "')";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                comando.ExecuteNonQuery();
                comando.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                return false;
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }
        }


        private bool guardarPaquetesBulto(Int32 bulto)
        {
            try
            {
                String strSql;
                this.conn.Open();

                strSql = "INSERT INTO TC_PACKING_LIST_PAQUETES_Operaciones (CODEMP, PACKING, PAQUETE, OPERAC, PUESTO, FECHA, NumeroPersonal, NumeroPersonal2, FECHAC, USUCRE, USUMOD) ";
                strSql += " SELECT CodEmp, PACKING, PAQUETE, ";
                strSql += " '" + this.Operacion + "', '" + this.Puesto + "', getdate(), " + this.Operario1 + ", ";

                if (this.Operario2 == String.Empty)
                    strSql += " null ,";
                else
                    strSql += this.Operario2 + ", ";

                strSql += " getdate(), '" + this.PDA + "', 'Bulto_" + bulto.ToString() + "' ";
                strSql += " 	FROM TC_PACKING_LIST_CONTENEDORES_BULTOS ";
                strSql += " 	WHERE (CODEMP = '" + this.CodEmp + "') AND (PACKING = " + this.Packing.ToString() + ") AND (BULTO = " + bulto.ToString() + ") ";
                strSql += " 	and (PAQUETE not in (Select distinct Paquete From TC_PACKING_LIST_PAQUETES_Operaciones Where CODEMP = '" + this.CodEmp + "' and PACKING = " + this.Packing.ToString() + " and OPERAC = '" + this.Operacion + "')) ";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                comando.ExecuteNonQuery();
                comando.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                return false;
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }
        }


        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Boolean refrescar = false;

            Int32 filas = ((DataTable)this.dataGrid1.DataSource).Rows.Count;
            for (int i = 0; i < filas; i++)
            {
                Boolean valor = (bool)this.dataGrid1.Rows[i].Cells[4].Value;
                if (valor == true)
                {
                    string val = this.dataGrid1.Rows[i].Cells[0].Value.ToString();
                    Int32 paquete = Convert.ToInt32(val);

                    if (MessageBox.Show("¿Está seguro de que desea eliminar el paquete: " + paquete.ToString() + "?", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        try
                        {
                            String strSql;
                            this.conn.Open();

                            strSql = "Delete ";
                            strSql += " 	FROM TC_PACKING_LIST_PAQUETES_Operaciones";
                            strSql += " 	WHERE (CODEMP = '" + this.CodEmp + "') AND (PACKING = " + this.Packing + ") AND (OPERAC = '" + this.Operacion + "')  AND (PAQUETE = " + paquete.ToString() + ")";

                            System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                            comando.ExecuteNonQuery();
                            comando.Dispose();

                            refrescar = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.ToString());
                        }
                        finally
                        {
                            if (this.conn.State != ConnectionState.Closed)
                                this.conn.Close();
                        }
                    }
                }
            }


            if (refrescar == true)
                this.rellenarGrid();
        }




        private void frmPaquetes_Closed(object sender, EventArgs e)
        {
            if (conn != null)
                if (conn.State != ConnectionState.Closed)
                    conn.Close();


            /*// You must call Dispose on each scanner class. Ensure that you deregister the callback as well...
            if (scanner != null)
            {
                scanner.ScanCompleteEvent -= new ScanCompleteEventHandler(scanner_ScanCompleteEvent);
                scanner.Dispose();
            }

            if (scsDriver != null)
            {
                scsDriver.Dispose();
            }*/

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkCasilla()
        {

            foreach (DataGridViewRow row in dataGrid1.SelectedRows)
            {
                row.Cells["Del"].Value = !(bool)row.Cells["Del"].Value;
            }
            dataGrid1.Refresh();
            //dataGrid1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.ColumnIndex == 4)
            {
                checkCasilla();
            }*/
        }

        private void dataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                checkCasilla();
            }
        }

        private void btnArriba_Click(object sender, EventArgs e)
        {
            this.dataGrid1.FirstDisplayedScrollingRowIndex = this.dataGrid1.FirstDisplayedScrollingRowIndex - 1;
        }

        private void btnAbajo_Click(object sender, EventArgs e)
        {
            this.dataGrid1.FirstDisplayedScrollingRowIndex = this.dataGrid1.FirstDisplayedScrollingRowIndex + 1;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rellenarGrid();
        }

        private void btnPendientes_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = true;

            if (Operacion == "900001")
            {
                labelTituloPanel.Text = "PAQUETES PENDIENTES DE MONTAJE";
            }
            else
            {
                labelTituloPanel.Text = "PAQUETES PENDIENTES DE CARGA";                
            }

            cargarPaquetes();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void btnIzda_Click(object sender, EventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingColumnIndex != 0) dataGridView1.FirstDisplayedScrollingColumnIndex = dataGridView1.FirstDisplayedScrollingColumnIndex - 1;
        }

        private void btnDcha_Click(object sender, EventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingColumnIndex != dataGridView1.Columns.Count) dataGridView1.FirstDisplayedScrollingColumnIndex = dataGridView1.FirstDisplayedScrollingColumnIndex + 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingRowIndex != dataGridView1.Rows.Count) dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex + 5;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingRowIndex != 0) dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex - 5;
        }

        private void cargarPaquetes()
        {
            Cursor.Current = Cursors.WaitCursor;

            System.Data.SqlClient.SqlConnection conn = new SqlConnection(Utils.CD.getConexion());

            string sql = "select * from (select distinct convert(varchar, TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA, 103) AS FECHA, ";
            sql += "TC_PACKING_LIST.PACKING, ";
            sql += "TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE,  ";

            if (Operacion == "900002")
            {
                sql += "case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002')     when 0 then NULL     else 'X'    end as CARGA_FICHADA,  ";
                sql += "(select TC_PACKING_LIST_PAQUETES_Operaciones.FECHA from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900002') AS FECHA_CARGA,  ";
            }
            else
            {
                sql += "case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_MARCAS where TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_MARCAS.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE) ";
                sql += "    when	(select COUNT(*) from TC_PACKING_LIST_PAQUETES_MARCAS where TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_MARCAS.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and TC_PACKING_LIST_PAQUETES_MARCAS.CATEGORIA in ('Tornilleria','Forro')) then 'TO' ";
                sql += "    else	case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') ";
                sql += "		        when 0 then NULL ";
                sql += "		        else 'X' ";
                sql += "	        end ";
                sql += "end as MONTAJE_FICHADO, ";
                sql += "(select TC_PACKING_LIST_PAQUETES_Operaciones.FECHAC from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = TC_PACKING_LIST.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = TC_PACKING_LIST.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE and OPERAC = '900001') AS FECHA_MONTAJE,		 ";

            }
            sql += "TC_PACKING_LIST_PAQUETES_MARCAS.PEDIDO  ";
            sql += "from TC_PACKING_LIST inner join  ";
            sql += "TC_PACKING_LIST_CONTENEDORES on TC_PACKING_LIST.CODEMP = TC_PACKING_LIST_CONTENEDORES.CODEMP and     TC_PACKING_LIST.PACKING = TC_PACKING_LIST_CONTENEDORES.PACKING  ";
            sql += "inner join TC_PACKING_LIST_CONTENEDORES_BULTOS on TC_PACKING_LIST_CONTENEDORES.CODEMP = TC_PACKING_LIST_CONTENEDORES_BULTOS.CODEMP AND TC_PACKING_LIST_CONTENEDORES.PACKING = TC_PACKING_LIST_CONTENEDORES_BULTOS.PACKING AND         TC_PACKING_LIST_CONTENEDORES.CONTENEDOR = TC_PACKING_LIST_CONTENEDORES_BULTOS.CONTENEDOR  ";
            sql += "inner join TC_PACKING_LIST_PAQUETES on TC_PACKING_LIST_CONTENEDORES_BULTOS.CODEMP = TC_PACKING_LIST_PAQUETES.CODEMP and TC_PACKING_LIST_CONTENEDORES_BULTOS.PACKING = TC_PACKING_LIST_PAQUETES.PACKING and TC_PACKING_LIST_CONTENEDORES_BULTOS.PAQUETE = TC_PACKING_LIST_PAQUETES.PAQUETE  ";
            sql += "inner join TC_PACKING_LIST_PAQUETES_MARCAS on TC_PACKING_LIST_PAQUETES.CODEMP = TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP and TC_PACKING_LIST_PAQUETES.PACKING = TC_PACKING_LIST_PAQUETES_MARCAS.PACKING and TC_PACKING_LIST_PAQUETES.PAQUETE = TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE  ";
            sql += "where TC_PACKING_LIST_CONTENEDORES.CODEMP = " + CodEmp + " and TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA >= DATEADD(DAY, -2, GETDATE()) and TC_PACKING_LIST_CONTENEDORES.FECHA_SALIDA <= GETDATE() and TC_PACKING_LIST.PACKING = '" + Packing + "') as TABLA  ";

            if (Operacion == "900001")
            {
                sql += "where MONTAJE_FICHADO is null  ";
            }
            else
            {
                sql += "where CARGA_FICHADA is null  ";
            }


            sql += "order by FECHA, PACKING ";


            SqlCommand command = new SqlCommand(sql, conn);

            //SqlDataReader reader = sqlCmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(paquetes);

            this.dataGridView1.DataSource = paquetes;


            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                //c.DefaultCellStyle.Font = new Font("Arial", 10, GraphicsUnit.Pixel);

                if (c.Name == "CARGA_FICHADA" || c.Name == "MONTAJE_FICHADO")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[c.Index].Value.ToString() == "X" || row.Cells[c.Index].Value.ToString() == "TO")
                        {
                            row.Cells[c.Index].Style.BackColor = Color.Lime;
                        }
                        else
                        {
                            row.Cells[c.Index].Style.BackColor = Color.Red;
                        }
                    }
                }

                /*foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[c.Index].Value.ToString() == valorCelda)
                    {
                        row.Cells[c.Index].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        valorCelda = row.Cells[c.Index].Value.ToString();
                    }
                }*/
            }




            Cursor.Current = Cursors.Default;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tempFichaje = true;
            timer1.Stop();
        }

        


    }
}
