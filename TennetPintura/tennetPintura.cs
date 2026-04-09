using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;
using empresaGlobalProj;

namespace TennetPintura
{
    public partial class tennetPintura : Form
    {
        CultureInfo myCIintl = new CultureInfo("es-ES", false);

        List<float> lsuperficie = new List<float>();
        List<string> lcodigo = new List<string>();

        //Row fallos
        DataGridViewRow nrCode = new DataGridViewRow();
        DataGridViewRow nrValVacio = new DataGridViewRow();
        DataGridViewRow nrSuperficie = new DataGridViewRow();
        DataGridViewRow nrCategoria = new DataGridViewRow();
        DataGridViewRow nrCodeVolcado = new DataGridViewRow();

        public tennetPintura()
        {

            InitializeComponent();
            empresaGlobal.showEmp = false;
            //dataGridViewTennetPaint.Columns["columnError"].ReadOnly = true;
            /*dataGridViewTennetPaint.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            DataObject d = dataGridViewTennetPaint.GetClipboardContent();*/

        }

        /* private void dataGridViewTennetPaint_MouseUp(object sender, MouseEventArgs e)
         {
             pegar_portapapeles(dataGridViewTennetPaint);
        
         }*/

        /*private void CopyPasteButton_Click(object sender, System.EventArgs e)
        {
            if (this.dataGridViewTennetPaint
                .GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(
                        this.dataGridViewTennetPaint.GetClipboardContent());

                    // Replace the text box contents with the clipboard text.
                    this.Text.Text = Clipboard.GetText();
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    this.TextBox1.Text =
                        "The Clipboard could not be accessed. Please try again.";
                }
            }
        }*/

        /*public void copiar_portapapeles(DataGridView datagrid)
        {
            DataObject objeto_datos = datagrid.GetClipboardContent();
            Clipboard.SetDataObject(objeto_datos);
        }

        */

        /*public static void OnDataGridViewPaste(object grid, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                System.Diagnostics.Debug.WriteLine("Dentro de PasteClipboard .");
                PasteTSV((DataGridView)grid);
            }
        }*/

        /*public static void PasteTSV(DataGridView grid)
        {
            char[] rowSplitter = { '\r', '\n' };
            char[] columnSplitter = { '\t' };

            // Get the text from clipboard 
            IDataObject dataInClipboard = Clipboard.GetDataObject();
            string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);

            // Split it into lines 
            string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

            // Get the row and column of selected cell in grid 
            int r = grid.SelectedCells[0].RowIndex;
            int c = grid.SelectedCells[0].ColumnIndex;

            // Add rows into grid to fit clipboard lines 
            if (grid.Rows.Count < (r + rowsInClipboard.Length))
            {
                grid.Rows.Add(r + rowsInClipboard.Length - grid.Rows.Count);
            }

            // Loop through the lines, split them into cells and place the values in the corresponding cell. 
            for (int iRow = 0; iRow < rowsInClipboard.Length; iRow++)
            {
                // Split row into cell values 
                string[] valuesInRow = rowsInClipboard[iRow].Split(columnSplitter);

                // Cycle through cell values 
                for (int iCol = 0; iCol < valuesInRow.Length; iCol++)
                {

                    // Assign cell value, only if it within columns of the grid 
                    if (grid.ColumnCount - 1 >= c + iCol)
                    {
                        DataGridViewCell cell = grid.Rows[r + iRow].Cells[c + iCol];

                        if (!cell.ReadOnly)
                        {
                            cell.Value = valuesInRow[iCol];
                        }
                    }
                }
            }
        }*/




        private void PasteClipboard()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Debug.WriteLine("Inicio de PasteClipboard.");
            /*if (dataGridViewTennetPaint.CurrentCell.RowIndex == null)
            {
                MessageBox.Show("Seleccione la celda ");
            }
            else
            {*/
            //Se comprueba que se haya seleccionado una celda donde copiar los elementos
            if (dataGridViewTennetPaint.CurrentCell != null)
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');

                    for (int i = 0; i < lines.Length; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(" Lines contiene: " + lines[i] + " y es la iteración: " + i);
                    }

                    string stringToRemove = "";
                    lines = lines.Where(val => val != stringToRemove).ToArray();

                    if (lines.Length > 0)
                    {
                        //dataGridView1.Rows[0].Cells[0].Selected=true; // dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        //if (dataGridViewTennetPaint.CurrentCell.RowIndex == null)
                        //{
                        int iFail = 0, iRow = dataGridViewTennetPaint.CurrentCell.RowIndex;//Convert.ToInt32(dataGridViewTennetPaint.Rows[0].Cells[0].Value);//dataGridViewTennetPaint.CurrentCell.RowIndex;
                        int iCol = dataGridViewTennetPaint.CurrentCell.ColumnIndex;
                        //}
                        DataGridViewCell oCell;
                        if (dataGridViewTennetPaint.Rows.Count < lines.Length - 1)
                            System.Diagnostics.Debug.WriteLine("dataGridViewTennetPaint.Rows.Count contiene: " + dataGridViewTennetPaint.Rows.Count + " y lines.Length contiene: " + lines.Length);
                        dataGridViewTennetPaint.Rows.Add(lines.Length); //antes estaba a menos lines.lenght - 1 pero si lo dejo así añado una de más
                        System.Diagnostics.Debug.WriteLine("el nº de línes es: " + lines.Length);
                        int it = 0;
                        foreach (string line in lines)
                        {
                            it++;
                            System.Diagnostics.Debug.WriteLine("Line contiene: " + line);
                            //if (iRow < dataGridViewTennetPaint.RowCount && line.Length > 0)
                            //{
                            string[] sCells = line.Split('\t');
                            for (int i = 0; i < sCells.GetLength(0); ++i)
                            {
                                if (iCol + i < this.dataGridViewTennetPaint.ColumnCount)
                                {
                                    System.Diagnostics.Debug.WriteLine("iCol + i contiene: " + iCol + i + " e iRow contiene " + iRow + " y el tamaño de la colección es: " + this.dataGridViewTennetPaint.RowCount);
                                    /*if (iRow >= this.dataGridViewTennetPaint.RowCount)
                                    { 
                                        dataGridViewTennetPaint.Rows.Add(); 
                                    }*/

                                    oCell = dataGridViewTennetPaint[iCol + i, iRow];

                                    if (!oCell.ReadOnly)
                                    {
                                        if (oCell.Value == null || oCell.Value.ToString() != sCells[i])
                                        {
                                            oCell.Value = Convert.ChangeType(sCells[i],
                                                                  oCell.ValueType);
                                            //  oCell.Style.BackColor = Color.Tomato;
                                        }
                                        else
                                            iFail++;
                                        //only traps a fail if the data has changed 
                                        //and you are pasting into a read only cell
                                    }
                                }
                                else
                                { break; }
                            }
                            iRow++;
                            //}
                            //else
                            //{ break; }

                            //if (lines.Length == it) {
                            /*System.Diagnostics.Debug.WriteLine("última línea: " + it + " ll " + lines.Length);
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridViewTennetPaint);
                            row.Cells[0].Value = "";
                            row.Cells[1].Value = "";
                            row.Cells[2].Value = "";
                            dataGridViewTennetPaint.Rows.Add(row); */
                            //}
                        }

                        if (iFail > 0)
                            MessageBox.Show(string.Format("{0} updates failed due" +
                                            " to read only column setting", iFail));
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("The data you pasted is in the wrong format for the cell");
                    return;
                }
                //}
                //dataGridViewTennetPaint.Rows.Add();
            }
            else
            {
                MessageBox.Show("Seleccione una celda");
            }
            System.Diagnostics.Debug.WriteLine("Fin PasteClipboard.");
            //dataGridViewTennetPaint.Rows.Add();
            Cursor.Current = Cursors.Default;
        }


        private void dataGridViewTennetPaint_KeyUp(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Key Up .");
            DataObject d = dataGridViewTennetPaint.GetClipboardContent();
            if ((e.Control && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                //System.Diagnostics.Debug.WriteLine("Insertando ...");
                PasteClipboard();
                // PasteTSV(dataGridViewTennetPaint);
            }
        }

        //Cerrar ventana
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //base.Close();
            cerrarVentana();
        }

        public void cerrarVentana()
        {
            base.Close();
        }


        private bool checkConn()
        {

            SqlConnection conexion = null;

            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();// @"Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            conexion = new SqlConnection(connString);

            try
            {
                conexion.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //System.Diagnostics.Debug.WriteLine("-DataGridView contiene estas líneas- " + dataGridViewTennetPaint.Rows.Count);
            //0. Comprobar que el DataGridView no esté vacío
            if (dataGridViewTennetPaint.Rows.Count > 1)
            {

                //limpio las listas para que no almacene códigos erróneos que se modifican
                lcodigo.Clear();
                lsuperficie.Clear();

                //Variable para comprobar que la superficie no sea incorrecta
                bool superficieIncorrecta = false;
                //Variable que comprueba que el código no sea correcto
                bool existeCodigo = false;
                //Variable que comprueba que la categoría no sea errónea
                bool catIncorrecta = false;
                //Variable que comprueba que los códigos no estén ya volcados
                bool checkCodeVolcados = false;

                System.Diagnostics.Debug.WriteLine("------------ Botón aceptar pulsado -------------");

                //Se limpia primero la tabla Volcar_Superficie_Tennet_1
                SqlConnection conexion = null;
                string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();// @"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; // db-imedexsa

                conexion = new SqlConnection(connString);
                conexion.Open();

                string sqlTrunc = "TRUNCATE TABLE [gg].[dbo].[1_Volcar_Superficie_Tennet_1]";
                SqlCommand cmd = new SqlCommand(sqlTrunc, conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();

                //Bandera valor vacío
                bool valVacio = false;
                //método comprobar valor vacío
                //se limpia la columna error
                
                /*foreach (DataGridViewRow myRow in dataGridViewTennetPaint.Rows)
                {
                    myRow.Cells[2].Value = null; // toda la columna error a null
                    if (!myRow.Equals(dataGridViewTennetPaint.Rows[dataGridViewTennetPaint.Rows.Count - 1])) 
                    {
                        if (myRow.Cells[0].Value == null || myRow.Cells[1].Value == null)
                        {
                            myRow.Cells[2].Value = "Valor Vacío";
                            valorVacio = true;
                            System.Diagnostics.Debug.WriteLine("--%%%% valor vacío");
                        }
                        else { System.Diagnostics.Debug.WriteLine("*** valor No vacío"); }
                    }
                
                }*/

                //1.- Se comprueba que no haya valores vacíos
                valVacio = valorVacio();

                if (!valVacio)
                {

                    bool insertado = false;
                    /*if (dataGridViewTennetPaint.Rows(dataGridViewTennetPaint.Rows.Count - 1).IsNewRow)
                    {
                           dataGridViewTennetPaint.Rows.RemoveAt(dataGridViewTennetPaint.Rows.Count - 1);
                }*/
                    /*DataRow dr = new DataRow();
                    dr = dataGridViewTennetPaint.Rows.GetLastRow;
                    dataGridViewTennetPaint.Rows.Remove();
                    dataGridViewTennetPaint.Rows.RemoveAt(dataGridViewTennetPaint.Rows.Count - 1);*/


                    //Listas para las consultas en la BD
                    //códigos leídos del DataGridView
                    string codeL = string.Empty;

                    //superficies leídas del DataGridView
                    float superficieL = 0;

                    int indexCodigo = 0;
                    int indexSuperficie = 1;
                    int cont = 0;
                    //se insertan en las listas los códigos y las superficies
                    foreach (DataGridViewRow row in dataGridViewTennetPaint.Rows)
                    {
                        //if (!row.Cells[indexOfYourColumn].Equals(null) || row.Cells[indexOfYourColumn] != null || row.Cells[indexOfYourColumn].Value.ToString() != "" || row.Cells[indexOfYourColumn].Value.ToString().Equals("") || row.Cells[indexOfYourColumn].Equals("") || row.Cells[indexOfYourColumn].Value.ToString() != "")
                        //Se comprueba que no sea la fila de inserción de datos
                        if (!row.Equals(dataGridViewTennetPaint.Rows[dataGridViewTennetPaint.Rows.Count - 1]))
                        {

                            //{myGrid.Rows[myGrid.Rows.Count - 1]
                            codeL = row.Cells[indexCodigo].Value.ToString();
                            //f.GetType() != typeof(float)
                            /*if (row.Cells[indexSuperficie].Value.GetType() != typeof(string))
                            {*/
                            //superficieL = Convert.ToDouble(row.Cells[indexSuperficie].Value);
                            /*}
                            else {
                                row.Cells[2].Value = "Superficie Incorrecta";
                            }*/



                            var value = row.Cells[indexSuperficie].Value.ToString();
                            float floatValue;

                            //Se comprueba que el valor obtenido del DataGridView sea float, de lo contrario se introduce error 
                            if (Single.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out floatValue))
                            {
                                //noCommas = row.Cells[indexSuperficie].Value.ToString().Replace(',', '.');
                                // superficieL = float.Parse(row.Cells[indexSuperficie].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat); //Convert.ToDouble(row.Cells[indexSuperficie].Value);
                                superficieL = float.Parse(row.Cells[indexSuperficie].Value.ToString(), new CultureInfo("es-ES"));
                            }
                            else
                            {
                                //rowInc = row;
                                superficieIncorrecta = true;
                                nrSuperficie = row;
                                //MessageBox.Show("ERror SUperficie invalid value");
                                System.Diagnostics.Debug.WriteLine("||||||||||||||| Error Superficie invalida " + row.Cells[indexSuperficie].Value.ToString());
                                System.Diagnostics.Debug.WriteLine(row.Cells[indexSuperficie].Value.ToString());
                                if (row.Cells[2].Value == null)
                                {
                                    row.Cells[2].Value = "Superficie Incorrecta";
                                }//break;
                            }

                            //superficieL = superficieL.Replace(',', '.');

                            lcodigo.Add(codeL);
                            lsuperficie.Add(superficieL);
                            System.Diagnostics.Debug.WriteLine("$$$$$$$$$$ " + row.Cells[indexSuperficie].Value.ToString() + " y superficieL: " + superficieL);
                            cont++;
                            System.Diagnostics.Debug.WriteLine("Data contiene ... " + codeL + " " + superficieL + " cont: " + cont);
                        }
                    }

                    //2.- Se comprueba que la superficie no sea incorrecta
                    if (!superficieIncorrecta)
                    {

                        /*for (int i = 0; i < lcodigo.Count; i++)
                        {

                            System.Diagnostics.Debug.WriteLine(" --------===------ lcodigo contiene " + lcodigo[i]);
                            System.Diagnostics.Debug.WriteLine("lsuperficie contiene " + lsuperficie[i] + "--------===------");
                        }*/

                        /*bool chk = checkConn();
                        System.Diagnostics.Debug.WriteLine("La conexión se ha abierto?: " + chk);*/

                        string strsql;
                        SqlCommand comando = null;

                        for (int i = 0; i < lcodigo.Count; i++)
                        {

                            conexion = new SqlConnection(connString);
                            conexion.Open();

                            //Comprobar que ese valor no se haya insertado ya en la base de datos
                            //método aparte
                            insertado = codInsertadoenVolcar_Superficie_Tennet1(lcodigo[i]);
                            System.Diagnostics.Debug.WriteLine(" código a comprobar: " + lcodigo[i]);
                            //double floatValue;
                            //double d = 1234.5;
                            bool is_double = unchecked(lsuperficie[i] == (double)lsuperficie[i]);
                            System.Diagnostics.Debug.WriteLine("is Double: " + is_double);
                            string super = Convert.ToString(lsuperficie[i]);//.Replace(',', '.');
                            super = super.Replace(',', '.');
                            //&& lsuperficie[i].GetType() == typeof(float)
                            if (!insertado) //&& dataGridViewTennetPaint.Rows[i].Cells["columnError"].Value.ToString() == null // && lsuperficie[i].GetType() != typeof(string) //isInstance(lsuperficie[i], float)) //&& lsuperficie[i] is Float
                            {
                                System.Diagnostics.Debug.WriteLine(" Dentro de !insertado, código: " + lcodigo[i] + " sup " + lsuperficie[i]);
                                /*strsql = " INSERT INTO [gg].[dbo].[1_Volcar_Superficie_Tennet_1] VALUES (@param1,@param2);";
                                comando = new SqlCommand(strsql, conexion);
                                comando.Parameters.Add("@param1", SqlDbType.VarChar).Value = lcodigo[i];
                                comando.Parameters.Add("@param2", SqlDbType.Float).Value = lsuperficie[i];*/

                                strsql = " INSERT INTO [gg].[dbo].[1_Volcar_Superficie_Tennet_1] VALUES ('" + lcodigo[i] + "', '" + super + "');";
                                comando = new SqlCommand(strsql, conexion);
                                //comando.Parameters.Add("@param1", SqlDbType.VarChar).Value = lcodigo[i];
                                //comando.Parameters.Add("@param2", SqlDbType.Float).Value = lsuperficie[i];


                                System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);

                                comando.ExecuteNonQuery();


                                //comando = new SqlCommand(strsql, conexion);
                                //comando.ExecuteNonQuery();
                                //table = new DataTable();

                                /*comando.CommandText = strsql;
                                table.Columns.Clear();
                                table.Clear();
                                adapter.Fill(table);*/


                            }
                            conexion.Close();
                            /* }
                             catch
                             {
                                 if (conexion.State == ConnectionState.Open)
                                     conexion.Close();
                                 MessageBox.Show("ERROR");
                             }*/
                            insertado = false;
                        }



                        //3. Se comprueba si los códigos existen en la base de datos
                        existeCodigo = existeCodigoenBD();

                        if (!existeCodigo)
                        {

                            //4. Se comprueba que la categoría sea la correcta
                            catIncorrecta = checkCategoria();

                            if (!catIncorrecta)
                            {

                                //5. Se comprueba que los códigos no estén volcados ya en [2_Volcar_Superficie_Tennet_2]
                                checkCodeVolcados = checkCodigosVolcados();
                                if (!checkCodeVolcados)
                                {


                                    //6. Se comprueba que no haya ningún error, se recorre la columna error y si todas están a null se hace la siguiente operación
                                    bool erroresCorregidos = true;
                                    foreach (DataGridViewRow row in dataGridViewTennetPaint.Rows)
                                    {
                                        System.Diagnostics.Debug.WriteLine(" Comprobando no hay errores ");
                                        if (!row.Equals(dataGridViewTennetPaint.Rows[dataGridViewTennetPaint.Rows.Count - 1]))
                                        {
                                            System.Diagnostics.Debug.WriteLine(" Valores de la columna Error, " + row.Cells["columnError"]);
                                            if (Convert.ToString(row.Cells["columnError"].Value) != string.Empty)
                                            {
                                                erroresCorregidos = false;
                                                dataGridViewTennetPaint.Update();
                                                dataGridViewTennetPaint.Refresh();
                                                System.Diagnostics.Debug.WriteLine(" -----------------> Errores no corregidos ");
                                                //MessageBox.Show("Errores en activo");
                                            }
                                        }
                                    }

                                    //Si no hay errores se hacen las últimas operaciones
                                    if (erroresCorregidos && !superficieIncorrecta)
                                    {

                                        //7. Si no hay errores, se borra de T_ARTCAR los artículos
                                        deleteFrom_T_ARTCAR();
                                        //8. Se inserta en T_ARTCAR
                                        insertInto_T_ARTCAR();
                                        //9. Se inserta en 2_Volcar_Superficie_Tennet_2 y se borra 1_Volcar_Superficie_Tennet_1
                                        insertInto_2Tennet();
                                        MessageBox.Show("Volcado realizado correctamente");
                                        dataGridViewTennetPaint.Rows.Clear();
                                    }

                                }//codigosvolcados
                                else
                                {
                                    MessageBox.Show("El código ya existe");// + nrCodeVolcado.Cells[0].Value.ToString() + " ya existente");
                                }

                            }//catIncorrecta
                            else
                            {
                                MessageBox.Show("Categoría incorrecta introducida en código ");// + nrCategoria.Cells[0].Value.ToString());
                            }

                        } //existecodigo
                        else {
                            MessageBox.Show("Código incorrecto introducido");// en " + nrCode.Cells[0].Value.ToString());
                        }


                    } //superficie incorrecta
                    else {
                        MessageBox.Show("Superficie incorrecta introducida");// + nrSuperficie.Cells[1].Value.ToString());
                    }

                }
                else
                {
                    MessageBox.Show("Has insertados valores vacíos en la fila ");// + nrValVacio.Index);
                }

            } //no se introducen nada
            else
            {
                MessageBox.Show("No has introducido información");
            }
            Cursor.Current = Cursors.Default;

        }

        //método que comprueba si se introducen valores vacíos
        public bool valorVacio()
        {
            bool valorVacio = false;
            foreach (DataGridViewRow myRow in dataGridViewTennetPaint.Rows)
            {
                myRow.Cells[2].Value = null; // toda la columna error a null
                if (!myRow.Equals(dataGridViewTennetPaint.Rows[dataGridViewTennetPaint.Rows.Count - 1]))
                {
                    if (myRow.Cells[0].Value == null || myRow.Cells[1].Value == null)
                    {
                        myRow.Cells[2].Value = "Valor Vacío";
                        nrValVacio = myRow;
                        valorVacio = true;
                        System.Diagnostics.Debug.WriteLine("--%%%% valor vacío");
                    }
                    else { System.Diagnostics.Debug.WriteLine("*** valor No vacío"); }
                }
            }
            return valorVacio;
        }


        public void deleteFrom_T_ARTCAR()
        {
            System.Diagnostics.Debug.WriteLine(" Método deleteFrom_T_ARTCAR ");

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString(); //@"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            conexion = new SqlConnection(connString);
            conexion.Open();

            strsql = " delete from [gg].[dbo].[T_ARTCAR] where  CodEMP = '3' and CARACT = '20' and codigo in (select codigo from [1_Volcar_Superficie_Tennet_1])";
            comando = new SqlCommand(strsql, conexion);
            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
            comando.ExecuteNonQuery();

            conexion.Close();

            System.Diagnostics.Debug.WriteLine(" Fin deleteFrom_T_ARTCAR");
        }

        public void insertInto_T_ARTCAR()
        {
            System.Diagnostics.Debug.WriteLine(" Método insertInto_T_ARTCAR ");

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString(); //@"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            conexion = new SqlConnection(connString);
            conexion.Open();

            //Se obtiene el nombre del usuario de Windows
            string userName = System.Environment.UserName;// System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            strsql = " insert into [gg].[dbo].[T_ARTCAR](CodEMP,codigo,CATEGO,CARACT,VALOR,FECHAC,USUCRE,STWF) select '3',codigo,dbo.IME_GetCategoria_Articulo('3',codigo),'20',replace(cast(superficie as varchar),'.',','),GETDATE(),'" + userName + "',0 from [gg].[dbo].[1_Volcar_Superficie_Tennet_1] order by codigo";
            comando = new SqlCommand(strsql, conexion);
            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
            comando.ExecuteNonQuery();

            conexion.Close();

            System.Diagnostics.Debug.WriteLine(" Fin insertInto_T_ARTCAR");
        }

        public void insertInto_2Tennet()
        {
            System.Diagnostics.Debug.WriteLine(" Método insertInto_2Tennet ");

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString(); //@"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            conexion = new SqlConnection(connString);
            conexion.Open();

            strsql = " insert into [gg].[dbo].[2_Volcar_Superficie_Tennet_2] select * from [gg].[dbo].[1_Volcar_Superficie_Tennet_1]";
            comando = new SqlCommand(strsql, conexion);
            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
            comando.ExecuteNonQuery();

            strsql = "truncate table [gg].[dbo].[1_Volcar_Superficie_Tennet_1]";
            comando = new SqlCommand(strsql, conexion);
            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
            comando.ExecuteNonQuery();

            conexion.Close();

            System.Diagnostics.Debug.WriteLine(" Fin insertInto_2Tennet");
        }


        public bool codInsertadoenVolcar_Superficie_Tennet1(string code)
        {
            bool enc = false;
            System.Diagnostics.Debug.WriteLine(" Método codInsertadoenVolcar_Superficie_Tennet1 ");

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();// @"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            DataTable table = new DataTable();

            //for (int i = 0; i < lcodigo.Count; i++)
            //{

            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;
                
            strsql = " select * from [gg].[dbo].[1_Volcar_Superficie_Tennet_1]";
            comando = new SqlCommand(strsql, conexion);
            //comando.Parameters.Add("@param1", SqlDbType.VarChar).Value = lcodigo[i];
            //comando.Parameters.Add("@param2", SqlDbType.Float).Value = lsuperficie[i];
            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
            //comando.ExecuteNonQuery();

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            conexion.Close();

            // }

            foreach (DataRow row1 in table.Rows)
            {

                System.Diagnostics.Debug.WriteLine(" Comparativa que se va a hacer: " + row1["codigo"] + " y " + code);
                //dataGridViewTennetPaint.Rows[i].Cells["codigo"].Value);
                if (row1["codigo"].Equals(code))
                {
                    System.Diagnostics.Debug.WriteLine(" Es igual, " + row1["codigo"] + " y " + code + " - INSERTADo");
                    enc = true;
                }

            }

            System.Diagnostics.Debug.WriteLine(" Fin codInsertadoenVolcar_Superficie_Tennet1");
            return enc;
        }

        public bool existeCodigoenBD()
        {
            bool existeCode = false;
            System.Diagnostics.Debug.WriteLine(" Método existeCodigoenBD ");
            /*--Códigos incorrectos. Poner error en grid
       select *
       from   [1_Volcar_Superficie_Tennet_1]
       where  codigo not in (select codigo from T_ARTICULOS where CodEMP = '3')
       order by codigo
       */
            /*DataTable dtEmpresas = new DataTable();

            string connString = @"Data Source=db-imedexsa;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from 1_Volcar_Superficie_Tennet_1 where  codigo not in (select codigo from T_ARTICULOS where CodEMP = '3') order by codigo", con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtEmpresas.Load(reader);
                }
            }*/

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString(); //@"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            DataTable table = new DataTable();


            // int pos = 0;

            //for (int i = 0; i < lcodigo.Count; i++)
            //{

            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;

            //            "select * from 1_Volcar_Superficie_Tennet_1 where  codigo not in (select codigo from T_ARTICULOS where CodEMP = '3') order by codigo"

            /*strsql = "";
            strsql = strsql + " select *";
            strsql = strsql + " from 1_Volcar_Superficie_Tennet_1";
            strsql = strsql + " where " + lcodigo[i] + " not in (select codigo from T_ARTICULOS where CodEMP = '3') order by codigo";*/

            //strsql = " select * from [gg].[dbo].[1_Volcar_Superficie_Tennet_1] where @param1 not in (select @param1 from T_ARTICULOS where CodEMP = '3') order by codigo";
            strsql = " select * from [gg].[dbo].[1_Volcar_Superficie_Tennet_1] where codigo not in (select codigo from T_ARTICULOS where CodEMP = '3') order by codigo";
            comando = new SqlCommand(strsql, conexion);
            //comando.Parameters.Add("@param1", SqlDbType.VarChar).Value = lcodigo[i];
            //comando.Parameters.Add("@param2", SqlDbType.Float).Value = lsuperficie[i];
            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
            //comando.ExecuteNonQuery();



            //using (comando = new SqlCommand(strsql, conexion))
            //{


            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            conexion.Close();

            //}

            /*if (table != null)
            {
                System.Diagnostics.Debug.WriteLine(" wrongData no es null y su valor inicial es " + table.Rows[0][0]);
            }
            else { System.Diagnostics.Debug.WriteLine(" wrongData es null "); }*/


            foreach (DataRow row1 in table.Rows)
            {
                foreach (DataGridViewRow row in dataGridViewTennetPaint.Rows)
                {
                    //dataGridViewTennetPaint.Rows[i].Cells["codigo"].Value);
                    if (!row.Equals(dataGridViewTennetPaint.Rows[dataGridViewTennetPaint.Rows.Count - 1]))
                    {
                        System.Diagnostics.Debug.WriteLine(" Es igual, " + row.Cells["columnCodigo"].Value.ToString() + " y " + row1["codigo"]);
                        if (row.Cells["columnCodigo"].Value.ToString().Equals(row1["codigo"].ToString()))
                        {
                            row.Cells[2].Value = "Código Incorrecto";
                            nrCode = row;
                            //dataGridViewTennetPaint.Rows[i].Cells["columnError"].Value = "Error";
                            dataGridViewTennetPaint.Update();
                            dataGridViewTennetPaint.Refresh();
                            System.Diagnostics.Debug.WriteLine(" No es igual");
                            existeCode = true;
                            break;
                        }
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(" Fin existeCodigoenBD ");
            return existeCode;
        }

        public bool checkCategoria()
        {
            bool checkCat = false;
            System.Diagnostics.Debug.WriteLine(" Método checkCategoria ");

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString(); //@"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            DataTable table = new DataTable();

            //for (int i = 0; i < lcodigo.Count; i++)
            //{

            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;

            strsql = "select *, dbo.IME_GetCategoria_Articulo('3',codigo) as Categoria from [gg].[dbo].[1_Volcar_Superficie_Tennet_1] where dbo.IME_GetCategoria_Articulo('3',codigo) not in (select catego from T_CARCAT where CodEMP = '3' and CARACT = '20') or dbo.IME_GetCategoria_Articulo('3',codigo) in (select catego from T_CARCAT where catego = '92') order by codigo";
            //Consulta que controla la categoría corregida: "select * from [gg].[dbo].[1_Volcar_Superficie_Tennet_1] where dbo.IME_GetCategoria_Articulo('3',codigo) not in (select catego from T_CARCAT where CodEMP = '3' and CARACT = '18') or dbo.IME_GetCategoria_Articulo('3',codigo) in (select catego from T_CARCAT where catego = '92') order by codigo";
            //Consulta que controla la categoría: "select *, dbo.IME_GetCategoria_Articulo('3',codigo) as Categoria from [gg].[dbo].[1_Volcar_Superficie_Tennet_1] where dbo.IME_GetCategoria_Articulo('3',codigo) not in (select catego from T_CARCAT where CodEMP = '3' and CARACT = '18') order by codigo";
            // Consulta sin controlar categoría: "select * from [gg].[dbo].[1_Volcar_Superficie_Tennet_1] where dbo.IME_GetCategoria_Articulo('3',codigo) not in (select catego from T_CARCAT where CodEMP = '3' and CARACT = '18') order by codigo";
            

            comando = new SqlCommand(strsql, conexion);
            //comando.Parameters.Add("@param1", SqlDbType.VarChar).Value = lcodigo[i];
            //comando.Parameters.Add("@param2", SqlDbType.Float).Value = lsuperficie[i];
            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
            //comando.ExecuteNonQuery();

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            conexion.Close();

            //}

            System.Diagnostics.Debug.WriteLine("******Table contiene filas:  " + table.Rows.Count + " dataGridViewTennetPaint.Rows contiene: " + dataGridViewTennetPaint.Rows.Count);
            foreach (DataRow row1 in table.Rows)
            {
                foreach (DataGridViewRow row in dataGridViewTennetPaint.Rows)
                {
                    System.Diagnostics.Debug.WriteLine("***************************************************************  La Categoría es: " + row1["Categoria"].ToString());
                    //dataGridViewTennetPaint.Rows[i].Cells["codigo"].Value);
                    if (!row.Equals(dataGridViewTennetPaint.Rows[dataGridViewTennetPaint.Rows.Count - 1]))
                    {
                        if (row.Cells["columnCodigo"].Value.ToString().Equals(row1["codigo"].ToString()))
                        {
                            System.Diagnostics.Debug.WriteLine(" Es igual, " + row.Cells["columnCodigo"].Value.ToString() + " y " + row1["codigo"]);
                            checkCat = true;
                            nrCategoria = row;
                            row.Cells[2].Value = "Error Categoría";
                            //MessageBox.Show("La categoría es incorrecta, contacta con el dpto. de Informática");
                            //dataGridViewTennetPaint.Rows[i].Cells["columnError"].Value = "Error";
                            dataGridViewTennetPaint.Update();
                            dataGridViewTennetPaint.Refresh();
                            System.Diagnostics.Debug.WriteLine(" No es igual la categoría a 20");
                            if (row1["Categoria"].ToString() == "92")
                            {
                                row.Cells[2].Value = "Error, este código es un Conjunto Soldado";
                                dataGridViewTennetPaint.Update();
                                dataGridViewTennetPaint.Refresh();
                                checkCat = true;
                                System.Diagnostics.Debug.WriteLine(" Conjunto Soldado encontrado ");
                            }
                           // break;
                        }

                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(" Fin checkCategoria");
            return checkCat;
        }

        public bool checkCodigosVolcados()
        {
            bool checkCodeVol = false;
            System.Diagnostics.Debug.WriteLine(" Método checkCodigosVolcados ");

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();// @"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            DataTable table = new DataTable();

            //for (int i = 0; i < lcodigo.Count; i++)
            //{

            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;

            strsql = " select [1_Volcar_Superficie_Tennet_1].codigo,[1_Volcar_Superficie_Tennet_1].superficie, [2_Volcar_Superficie_Tennet_2].codigo as codigo_volcado,[2_Volcar_Superficie_Tennet_2].superficie as superficie_volcada from [gg].[dbo].[1_Volcar_Superficie_Tennet_1] inner join [gg].[dbo].[2_Volcar_Superficie_Tennet_2] on [1_Volcar_Superficie_Tennet_1].codigo = [2_Volcar_Superficie_Tennet_2].codigo order by [1_Volcar_Superficie_Tennet_1].codigo";
            comando = new SqlCommand(strsql, conexion);
            //comando.Parameters.Add("@param1", SqlDbType.VarChar).Value = lcodigo[i];
            //comando.Parameters.Add("@param2", SqlDbType.Float).Value = lsuperficie[i];
            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);
            //comando.ExecuteNonQuery();

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            conexion.Close();

            // }

            foreach (DataRow row1 in table.Rows)
            {
                foreach (DataGridViewRow row in dataGridViewTennetPaint.Rows)
                {
                    //dataGridViewTennetPaint.Rows[i].Cells["codigo"].Value);
                    if (!row.Equals(dataGridViewTennetPaint.Rows[dataGridViewTennetPaint.Rows.Count - 1]))
                    {
                        System.Diagnostics.Debug.WriteLine(" Es igual, " + row.Cells["columnCodigo"].Value.ToString() + " y " + row1["codigo"]);
                        if (row.Cells["columnCodigo"].Value.ToString().Equals(row1["codigo"].ToString()))
                        {
                            checkCodeVol = true;
                            nrCodeVolcado = row;
                            row.Cells[2].Value = "Error Código Volcado, suprime esta línea";
                            //dataGridViewTennetPaint.Rows[i].Cells["columnError"].Value = "Error";
                            dataGridViewTennetPaint.Update();
                            dataGridViewTennetPaint.Refresh();

                            System.Diagnostics.Debug.WriteLine(" No es igual");
                            break;
                        }
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine(" Fin checkCodigosVolcados");
            return checkCodeVol;
        }

        public void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            // {
            //mnu.Show(MousePosition);
            //dataGridViewTennetPaint.Rows[row].Cells[col].Value = Clipboard.GetData(DataFormats.Text);
            PasteClipboard();
            // }
        }

        public void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*System.Diagnostics.Debug.WriteLine(" Copiando " + dataGridViewTennetPaint.GetClipboardContent());
            DataObject dataObj = dataGridViewTennetPaint.GetClipboardContent();
            //if (dataGridViewTennetPaint.GetClipboardContent() != null)
            //{
                System.Diagnostics.Debug.WriteLine(" Copiando 2");
                Clipboard.SetDataObject(dataObj, true);
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                string stringToRemove = "";
                lines = lines.Where(val => val != stringToRemove).ToArray();
                Clipboard.SetDataObject(lines, true);*/

            dataGridViewTennetPaint.SelectAll();
            DataObject dataObj = dataGridViewTennetPaint.GetClipboardContent();
            Clipboard.SetDataObject(dataObj, true);
            //}
        }

        /*private void dataGridViewTennetPaint_Click(object sender, EventArgs e)
        {
            mnu.Show(MousePosition);
        }*/

        private void dataGridViewTennetPaint_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnu.Show(MousePosition);
            }
        }

        private void tennetPintura_FormClosed(object sender, FormClosedEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }

        private void tennetPintura_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }

        #region Consultar y cambiar Marcas JRMM 01/07/2020
        private void button_BuscarMarca_Click(object sender, EventArgs e)
        {
            button_cambiar.Visible = false;
            pictureBox_not.Visible = false;
            pictureBox_checked.Visible = false;
            textBox_marcaEdit.Visible = false;
            //Buscador
            string marca = textBox_marca.Text;
            double valor = -2;

            //SQL
            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();// @"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            conexion = new SqlConnection(connString);



            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // Carlos Casquero 11/03/2026 - Se añade el codigo de empresa 3 puesto que si no aparece como que no se ha insertado la marca pero al existir no permite insertarla
                strsql = "Select isNull(VALOR, -1) from T_ARTCAR where CodEMP='3' AND CODIGO = '" + marca + "' and CARACT = '20'";
                SqlCommand comando = new SqlCommand(strsql, conexion);

                //Carlos Sanchez 
                String valorVacio = Convert.ToString(comando.ExecuteScalar());

                if (valorVacio != "???")
                {
                    if (valorVacio == "")
                        valor = -1;
                    else
                        valor = Convert.ToDouble(comando.ExecuteScalar());
                }
                else
                {
                    valor = -1;
                }
            }

            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            //Z41-K503
            //Carlos Casquero 09/04/2026 - Se añade que si el valor es 0, por algunos valores que se han añadido erróneamente, se permita modificar este punto
            if (valor >= 0)
            {
                pictureBox_checked.Visible = true;
                textBox_marcaEdit.Visible = true;
                //label2.Text = valor.ToString();
                textBox_marcaEdit.Text = valor.ToString();
                button_cambiar.Visible = true;
            }
            else
            {
                pictureBox_not.Visible = true;
            }

        }

        private void button_cambiar_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("¿Deseas cambiar los m2 de la marca " + textBox_marca.Text + " a " + textBox_marcaEdit.Text + " ?", "Tennet Pinturas", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //SQL
                string strsql;
                SqlConnection conexion = null;
                string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();// @"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
                conexion = new SqlConnection(connString);
                string value = textBox_marcaEdit.Text;
                string marca = textBox_marca.Text;

                try
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();

                    strsql = "UPDATE [gg].[dbo].T_ARTCAR SET [VALOR] = '" + value + "' where CodEMP='3' AND CARACT = '20' and CODIGO = '" + marca + "'";
                    SqlCommand comando = new SqlCommand(strsql, conexion);

                    comando.ExecuteNonQuery();
                    //value = Convert.ToDouble(comando.ExecuteScalar());
                }

                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
                MessageBox.Show("Operación realizada con éxito", "Tennet Pinturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
                MessageBox.Show("La operación se ha cancelado", "Tennet Pinturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion


    }

}


