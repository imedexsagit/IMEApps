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

namespace Imprimacion
{
    public partial class Imprimacion : Form
    {

        DataTable dt;


        List<string> listaImprimacion = new List<string>();
        List<string> listaCodigo = new List<string>();
        List<string> filtrosBusq = new List<string>();

        BindingSource almacen = new BindingSource();


        //Row Vacios
        DataGridViewRow nrValVacio = new DataGridViewRow();
        DataGridViewRow nrCode = new DataGridViewRow();
        DataGridViewRow nrImprimacion = new DataGridViewRow();


        public Imprimacion()
        {
            InitializeComponent();

            dgResultados.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgResultados.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dgResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11);
            dgResultados.EnableHeadersVisualStyles = false;
            dgResultados.RowTemplate.Height = 40;
            dgResultados.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgResultados.ReadOnly = true;


            dgResultadosTTT.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgResultadosTTT.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dgResultadosTTT.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgResultadosTTT.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11);
            dgResultadosTTT.EnableHeadersVisualStyles = false;
            dgResultadosTTT.RowTemplate.Height = 40;
            dgResultadosTTT.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgResultadosTTT.ReadOnly = true;

        }

        private void PasteClipboard()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Debug.WriteLine("Inicio de PasteClipboard.");

            if (dataGridImprimacion.CurrentCell != null)
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

                        int iFail = 0, iRow = dataGridImprimacion.CurrentCell.RowIndex;
                        int iCol = dataGridImprimacion.CurrentCell.ColumnIndex;

                        DataGridViewCell oCell;
                        if (dataGridImprimacion.Rows.Count < lines.Length - 1)
                            System.Diagnostics.Debug.WriteLine("dataGridViewTennetPaint.Rows.Count contiene: " + dataGridImprimacion.Rows.Count + " y lines.Length contiene: " + lines.Length);
                        dataGridImprimacion.Rows.Add(lines.Length); //antes estaba a menos lines.lenght - 1 pero si lo dejo así añado una de más
                        System.Diagnostics.Debug.WriteLine("el nº de línes es: " + lines.Length);
                        int it = 0;
                        foreach (string line in lines)
                        {
                            it++;
                            System.Diagnostics.Debug.WriteLine("Line contiene: " + line);

                            string[] sCells = line.Split('\t');
                            for (int i = 0; i < sCells.GetLength(0); ++i)
                            {
                                if (iCol + i < this.dataGridImprimacion.ColumnCount)
                                {
                                    System.Diagnostics.Debug.WriteLine("iCol + i contiene: " + iCol + i + " e iRow contiene " + iRow + " y el tamaño de la colección es: " + this.dataGridImprimacion.RowCount);

                                    oCell = dataGridImprimacion[iCol + i, iRow];

                                    if (!oCell.ReadOnly)
                                    {
                                        if (oCell.Value == null || oCell.Value.ToString() != sCells[i])
                                        {
                                            oCell.Value = Convert.ChangeType(sCells[i],
                                                                  oCell.ValueType);

                                        }
                                        else
                                            iFail++;

                                    }
                                }
                                else
                                { break; }
                            }
                            iRow++;

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

            }
            else
            {
                MessageBox.Show("Seleccione una celda");
            }
            System.Diagnostics.Debug.WriteLine("Fin PasteClipboard.");

            Cursor.Current = Cursors.Default;
        }



        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;


            if (dataGridImprimacion.Rows.Count > 1)
            {

                listaCodigo.Clear();
                listaImprimacion.Clear();

                //Variable para comprobar que la superficie no sea incorrecta
                bool imprimacionIncorrecta = false;
                //Variable que comprueba que el código no sea correcto
                bool existeCodigo = false;
                //Variable que comprueba que la categoría no sea errónea
                // bool catIncorrecta = false;
                //Variable que comprueba que los códigos no estén ya volcados
                bool codeVolcado = false;
                //Variable valor vacio 
                bool valorVacio = false;


                SqlConnection conexion = null;
                string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString(); //@"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;

                conexion = new SqlConnection(connString);
                conexion.Open();



                string sqlTrunc = "TRUNCATE TABLE [gg].[dbo].[aux_imprimacion_tabla]";
                SqlCommand cmd = new SqlCommand(sqlTrunc, conexion);
                cmd.ExecuteNonQuery();
                conexion.Close();

                valorVacio = esValorVacio();

                if (!valorVacio)
                {

                    bool insertado = false; 


                    string codigo = string.Empty;
                    string imprimacion = string.Empty;


                    //se insertan en las listas los códigos y las superficies       
                    foreach (DataGridViewRow row in dataGridImprimacion.Rows)
                    {

                        if (!row.Equals(dataGridImprimacion.Rows[dataGridImprimacion.Rows.Count - 1]))
                        {

                            codigo = row.Cells[0].Value.ToString().Trim();

                           string imp = row.Cells[1].Value.ToString().Trim();
                            //PONER TRIM
                           if (row.Cells[1].Value.ToString().Trim().Length <= 2 && (row.Cells[1].Value.ToString().Trim().Equals("a1") || row.Cells[1].Value.ToString().Trim().Equals("A1") || row.Cells[1].Value.ToString().Trim().Equals("a2") || row.Cells[1].Value.ToString().Trim().Equals("A2") || row.Cells[1].Value.ToString().Trim().Equals("a0") || row.Cells[1].Value.ToString().Trim().Equals("A0") || row.Cells[1].Value.ToString().Trim().Equals("a3") || row.Cells[1].Value.ToString().Trim().Equals("A3")))
                            {
                                imprimacion = row.Cells[1].Value.ToString().Trim().ToUpper();

                            }
                            else
                            {
                                imprimacionIncorrecta = true;
                                nrImprimacion = row;
                                if (row.Cells[2].Value == null)
                                {
                                    row.Cells[2].Value = "Imprimacion Incorrecta";
                                    dataGridImprimacion.CurrentCell = dataGridImprimacion.Rows[row.Index].Cells[0];
                                    dataGridImprimacion.Rows[row.Index].Selected = true;
                                }

                            }

                        }

                        listaCodigo.Add(codigo);
                        listaImprimacion.Add(imprimacion);

                    }


                    if (!imprimacionIncorrecta)
                    {

                        string strsql;
                        SqlCommand comando = null;

                        for (int i = 0; i < listaCodigo.Count; i++) {
                            conexion = new SqlConnection(connString);
                            conexion.Open();

                            //Comprobar que ese valor ya se ha insertado en la base de datos 
                            insertado = cod_Insertar_Tabla_auxiliar(listaCodigo[i]);
                            //insertado = valorRepetido(listaCodigo[i]);

                            if (!insertado)
                            {

                                strsql = " INSERT INTO [gg].[dbo].[aux_imprimacion_tabla] VALUES ('" + listaCodigo[i] + "', '" + listaImprimacion[i] + "');";
                                comando = new SqlCommand(strsql, conexion);
                                comando.ExecuteNonQuery();

                            }
                            else {
                                if (i < dataGridImprimacion.RowCount - 1 )
                                {
                                    dataGridImprimacion.Rows[i].Cells[2].Value = "Código repetido en la tabla de imprimación";
                                    dataGridImprimacion.CurrentCell = dataGridImprimacion.Rows[i].Cells[0];
                                    dataGridImprimacion.Rows[i].Selected = true;
                                }
                            
                            }

                            conexion.Close();

                            insertado = false;
                        }

                        existeCodigo = existeCodigoenBDos();

                        if (!existeCodigo)
                        {

                            //catIncorrecta = checkCategoria();
                            //if
                            codeVolcado = checkCodeVolcados(); 
                            //if
                            //Se comprueba que no haya errores 

                            if (!codeVolcado)
                            {

                                bool errores = true;
                                foreach (DataGridViewRow row in dataGridImprimacion.Rows)
                                {

                                    if (!row.Equals(dataGridImprimacion.Rows[dataGridImprimacion.Rows.Count - 1]))
                                    {

                                        if (Convert.ToString(row.Cells["columnError"].Value) != string.Empty)
                                        {

                                            errores = false;
                                            dataGridImprimacion.Update();
                                            dataGridImprimacion.Refresh();

                                        }

                                    }
                                }

                                if (errores && !imprimacionIncorrecta)
                                {


                                    //-----------------------------------------------------------------------------------
                                    //-----------------------------------------------------------------------------------
                                    //LLAMADA A PROCEDIMIENTO ALMACENADO 
                                    //-----------------------------------------------------------------------------------
                                    //-----------------------------------------------------------------------------------

                                    
                                    using (SqlConnection sql = new SqlConnection(connString))
                                    {

                                        using (SqlCommand cmdPA = new SqlCommand("aux_imprimacion_procalm", sql))
                                        {
                                            cmdPA.CommandType = CommandType.StoredProcedure;
                                            cmdPA.Parameters.Add(new SqlParameter("@codemp", empresaGlobal.empresaID));
                                            cmdPA.Parameters.Add(new SqlParameter("@usuario", Environment.UserName));
                                            sql.Open();
                                            cmdPA.ExecuteNonQuery();

                                        }


                                    }

                                    MessageBox.Show("Volcado realizado correctamente");


                                }




                            }
                            else {
                                MessageBox.Show("El código ya esta volcado "); 
                            }
                            

                        }
                        else {
                            MessageBox.Show("El código NO existe");  //+ nrCodeVolcado.Cells[0].Value.ToString() + " ya existente");
                        }


                    }
                    else {

                        MessageBox.Show("Imprimacion incorrecta");

                    }

                }
                else
                {
                    MessageBox.Show("Has insertados valores vacíos en la fila ");
                }


            }

            else
            {
                MessageBox.Show("No has introducido información");
            }
        }


        //Metodo para comprovar valores vacios
        public bool esValorVacio()
        {
            bool valorVacio = false;

            foreach (DataGridViewRow row in dataGridImprimacion.Rows) 
            {
                row.Cells[2].Value = null;
                if (!row.Equals(dataGridImprimacion.Rows[dataGridImprimacion.Rows.Count - 1])) 
                {
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                    {
                        row.Cells[2].Value = "Valor Vacio";
                        nrValVacio = row;
                        dataGridImprimacion.CurrentCell = dataGridImprimacion.Rows[row.Index].Cells[0];
                        dataGridImprimacion.Rows[row.Index].Selected = true;
                        valorVacio = true;
                    }
                } 
            }
          
            return valorVacio;

        }

        
        public bool checkCodeVolcados() {

            bool existeCode = false;

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString(); //@"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            DataTable table = new DataTable();




            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;



            strsql = " select * from [gg].[dbo].[aux_imprimacion_tabla] where codigo  in (select codigo from [gg].[dbo].T_ARTCAR where CodEMP = '3' and CARACT='19' and (VALOR = 'A1'or VALOR ='A2' or VALOR ='A0' or VALOR ='A3')) ";
            comando = new SqlCommand(strsql, conexion);

            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);


            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            conexion.Close();

            foreach (DataRow row1 in table.Rows)
            {
                foreach (DataGridViewRow row in dataGridImprimacion.Rows)
                {

                    if (!row.Equals(dataGridImprimacion.Rows[dataGridImprimacion.Rows.Count - 1]))
                    {
                        System.Diagnostics.Debug.WriteLine(" Es igual, " + row.Cells["columnCodigo"].Value.ToString() + " y " + row1["codigo"]);
                        if (row.Cells["columnCodigo"].Value.ToString().Equals(row1["codigo"].ToString()))
                        {
                            row.Cells[2].Value = "Código ya volcado";
                            nrCode = row;
                            dataGridImprimacion.CurrentCell = dataGridImprimacion.Rows[row.Index].Cells[0];
                            dataGridImprimacion.Rows[row.Index].Selected = true;
                            dataGridImprimacion.Update();
                            dataGridImprimacion.Refresh();
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


        public bool valorRepetido(string codigo)
        {
            bool valorRepetido = false;

            foreach (DataGridViewRow row in dataGridImprimacion.Rows)
            {
                row.Cells[2].Value = null;
                if (!row.Equals(dataGridImprimacion.Rows[dataGridImprimacion.Rows.Count - 1]))
                {
                    if (row.Cells[0].Equals(codigo))
                    {
                        row.Cells[2].Value = "Valor repetido";
                        nrValVacio = row;
                        dataGridImprimacion.CurrentCell = dataGridImprimacion.Rows[row.Index].Cells[0];
                        dataGridImprimacion.Rows[row.Index].Selected = true;
                        valorRepetido = true;
                    }
                }
            }

            return valorRepetido;

        }



        public bool existeCodigoenBDos()
        {
            bool existeCode = false;

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString(); //@"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
            SqlCommand comando = null;

            DataTable table = new DataTable();




            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;



            strsql = " select * from [gg].[dbo].[aux_imprimacion_tabla] where codigo not in (select codigo from T_ARTICULOS where CodEMP = '3') order by codigo";
            comando = new SqlCommand(strsql, conexion);

            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);


            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            conexion.Close();

            foreach (DataRow row1 in table.Rows)
            {
                foreach (DataGridViewRow row in dataGridImprimacion.Rows)
                {

                    if (!row.Equals(dataGridImprimacion.Rows[dataGridImprimacion.Rows.Count - 1]))
                    {
                        System.Diagnostics.Debug.WriteLine(" Es igual, " + row.Cells["columnCodigo"].Value.ToString() + " y " + row1["codigo"]);
                        if (row.Cells["columnCodigo"].Value.ToString().Equals(row1["codigo"].ToString()))
                        {
                            row.Cells[2].Value = "Código Incorrecto";
                            nrCode = row;
                            dataGridImprimacion.CurrentCell = dataGridImprimacion.Rows[row.Index].Cells[0];
                            dataGridImprimacion.Rows[row.Index].Selected = true;
                            dataGridImprimacion.Update();
                            dataGridImprimacion.Refresh();
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




        public bool cod_Insertar_Tabla_auxiliar(string codigo) {

            bool resultado = false;

            string strsql;
            SqlConnection conexion = null;
            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            SqlCommand comando = null;

            DataTable tabla_auxiliar = new DataTable();

            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;
            strsql = "select * from [gg].[dbo].[aux_imprimacion_tabla]";

            comando = new SqlCommand(strsql, conexion);

            adapter = new SqlDataAdapter(comando);


            comando.CommandText = strsql;
            tabla_auxiliar.Columns.Clear();
            tabla_auxiliar.Clear();
            adapter.Fill(tabla_auxiliar);

            conexion.Close();

            foreach(DataRow row in tabla_auxiliar.Rows){

                if (row["codigo"].Equals(codigo)) {

                    resultado = true; 
                }
            
            }

            return resultado;

        
        }


        private void dataGridImprimacion_KeyUp(object sender, KeyEventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("Key Up .");
            DataObject d = dataGridImprimacion.GetClipboardContent();
            if ((e.Control && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                PasteClipboard();

            }

        }

        private void dataGridImprimacion_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnu.Show(MousePosition);
            }
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteClipboard();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataGridImprimacion.SelectAll();
            DataObject dataObj = dataGridImprimacion.GetClipboardContent();
            Clipboard.SetDataObject(dataObj, true);
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            dataGridImprimacion.Rows.Clear();
        }

        private void btn_busq_Click(object sender, EventArgs e)
        {
            // Se desmarcan las checkbox cuando se pulse el botón de búsqueda de tal forma que no afecte a las filas que sean recuperadas
            this.uncheckBoxes();
            string busqmarca = tbox_marca.Text;
            string strSql = "";
            SqlCommand sqlCmd;
            dt = new DataTable();


            string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
            SqlConnection conexion = new SqlConnection(connString);
            this.uncheckBoxes();

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = @"SET NOCOUNT ON; 	

	                Declare @Cantidad float = 1
	                Declare @maxNivel int = 20
	                Declare @semilla int = 0

	                IF OBJECT_ID(N'tempdb..#T_Lista_Almacen', N'U') IS NOT NULL 
		                DROP TABLE #T_Lista_Almacen
    
                    SELECT '"+empresaGlobal.empresaID + @"' as CodEmp,
			                '0' as Altern,
			                0 as ID,
			                CODIGO as Codigo_Padre, 
			                @Cantidad as Cantidad_Padre, 			
			                null as ID_PADRE,
			                CODIGO as Codigo_Hijo, 
			                @Cantidad as Cantidad_Hijo, 
			                CATEGORIA as Categoria_Hijo, 
			                FAMILIA as Familia_Hijo, 
			                0 as Correlacion,
			                0 as Nivel
			                Into #T_Lista_Almacen
		                FROM T_ARTICULOS
		                WHERE CodEmp = '" + empresaGlobal.empresaID + @"'
			                AND CODIGO = '" + busqmarca + @"'

	                Declare @Nivel int = 0
                    WHILE (@Nivel <= @maxNivel) AND (@@ROWCOUNT > 0) BEGIN
                        SET @Nivel = @Nivel + 1
        
                        INSERT INTO #T_Lista_Almacen
                        SELECT '" + empresaGlobal.empresaID + @"' as CodEmp,
				                '0' as Altern,
				                @semilla + row_number() over(ORDER BY @maxNivel) as Id,				
				                T_ESTRUC.CONJUN as Codigo_Padre, 
				                T1.Cantidad_Hijo as Cantidad_Padre, 				
				                T1.ID as Id_Padre,
				                T_ESTRUC.COMPON as Codigo_Hijo, 				
				                T_ESTRUC.CTDENL as Cantidad_Hijo,		
				                T_ARTICULOS.CATEGORIA as Categoria_Hijo, 
				                T_ARTICULOS.FAMILIA Familia_Hijo, 
				                T_ESTRUC.CORREL as Correlacion, 
				                @Nivel as Nivel
			                FROM #T_Lista_Almacen T1 INNER JOIN T_ESTRUC 
					                ON T1.Codigo_Hijo = T_ESTRUC.CONJUN INNER JOIN T_ARTICULOS 
					                ON T_ESTRUC.CodEMP = T_ARTICULOS.CodEMP AND T_ESTRUC.COMPON = T_ARTICULOS.CODIGO
			                WHERE (T_ESTRUC.CodEmp = '" + empresaGlobal.empresaID + @"')
					                AND (T_ESTRUC.Altern = '0')
					                AND (T1.Nivel = @Nivel - 1) 
					                AND (T1.Familia_Hijo <> 'TO')		
					                AND (T_ARTICULOS.FAMILIA <> 'MP')	
					
		                set @semilla = @semilla + @@ROWCOUNT
                    END        
	  
	                SELECT
			                t_1.Codigo_Padre,
			                t_1.Codigo_Hijo, 
                            CASE
			                when T_ARTCAR_Imprimacion.VALOR = '???' then ''
                            else T_ARTCAR_Imprimacion.VALOR
                            end as Valor,
			                CASE
			                when T_ARTCAR_Imprimacion.VALOR = 'A0' OR T_ARTCAR_Imprimacion.VALOR = 'A1' OR T_ARTCAR_Imprimacion.VALOR = 'A2' OR T_ARTCAR_Imprimacion.VALOR = 'A3' then 'Imprimacion realizada'
			                when T_ARTCAR_Imprimacion.VALOR = '???' then 'Sin imprimacion'
			                else 'Sin imprimacion' 
			                end as Imprimacion


		                FROM #T_Lista_Almacen as t_1 Left Outer Join T_ARTICULOS
			                on t_1.CodEmp = T_ARTICULOS.CodEMP and t_1.Codigo_Hijo = T_ARTICULOS.CODIGO 
			
			                Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                FROM T_ARTCAR
										                WHERE (CARACT = '19') AND (VALOR <> '???') AND (VALOR <> 'Sin imprimación') AND (VALOR IS NOT NULL)
									                ) as T_ARTCAR_Imprimacion
			                on T_ARTICULOS.CodEMP = T_ARTCAR_Imprimacion.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Imprimacion.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Imprimacion.CATEGO		
                            WHERE T_ARTCAR_Imprimacion.VALOR IS NOT NULL AND (T_ARTCAR_Imprimacion.VALOR <>'')
		                ORDER BY t_1.Nivel, t_1.Codigo_Padre, t_1.Correlacion";


                sqlCmd = new SqlCommand(strSql, conexion);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            var bindingSource = new System.Windows.Forms.BindingSource();    
            bindingSource.DataSource = dt;
            dgResultados.DataSource = bindingSource;
            // Se ordenan las filas recuperadas por la columna valor para realizar un mejor seguimiento
            this.dgResultados.Sort(this.dgResultados.Columns["Valor"], ListSortDirection.Ascending);
            // Se llama a la función que permite pintar la columna que indica si la imprimación está hecha en verde
            this.colorize();
     
        }



        private void btnConsultar_Click(object sender, EventArgs e)
        {
            DataTable dtMod = ObtenerInformacionAModificar(tboxCodModificar.Text);
            List<String> opciones = getValoresImprimacion();

            if (!String.IsNullOrEmpty(tboxCodModificar.Text) && dtMod.Rows.Count > 0)
            {
                gBoxResul.Enabled = true;
                lbCodigo.Text = dtMod.Rows[0][2].ToString();
                lbEstado.Text = dtMod.Rows[0][1].ToString();

                foreach (String opcion in opciones)
                {
                    if (opcion.Equals(dtMod.Rows[0][0].ToString()))
                    {
                        comboOpciones.SelectedIndex = comboOpciones.FindStringExact(dtMod.Rows[0][0].ToString());
                    }
                }
            
            }else {
                gBoxResul.Enabled = false;
                lbCodigo.Text = "";
                lbEstado.Text = "Sin Imprimación.";
                comboOpciones.SelectedIndex = -1;
            
            }





        }


        public DataTable ObtenerInformacionAModificar(String codigo)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;


            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "SELECT         VALOR, 'Imprimacion Realizada.' , CODIGO FROM T_ARTCAR WHERE        (CARACT = '19') AND (CODIGO = '" + codigo + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

                return table;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<string> getValoresImprimacion()
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table;


            try
            {
                conexion = new SqlConnection(Utils.CD.getConexion());
                conexion.Open();

                strsql = "SELECT VALOR FROM T_VALCAR WHERE (CARACT = '19') AND (CODEMP = '" + empresaGlobal.empresaID + "')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                conexion.Close();

                return table.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("VALOR")).ToList(); ;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                MessageBox.Show(Form.ActiveForm, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboOpciones.Text.ToString().Equals("Sin Imprimación"))
            {
                eliminarImprimacionMarca(lbCodigo.Text.ToString());
            }
            else { 
            modificarDatosCodigo(comboOpciones.Text, lbCodigo.Text);
            }
            MessageBox.Show("Se ha modificado la imprimación del codigo", "Imprimación Modificada", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }




        public void modificarDatosCodigo(string valor, string codigo)
        {
            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;

            strsql = "update [dbo].[T_ARTCAR] set " +
                         "VALOR ='" + valor + "' " +
                         " where CARACT = '19' and CODIGO = '" + codigo + "' ";


            using (conexion = new SqlConnection(Utils.CD.getConexion()))
            {
                comando = new SqlCommand(strsql, conexion);
                conexion.Open();

                int filasEfectadas = (int)comando.ExecuteNonQuery();
                if (filasEfectadas < 1)
                {
                    Console.WriteLine("No se ha modificado la marca");
                    // todoCorrecto = false;
                }
                Console.WriteLine("marca modificada");

            }

            //return todoCorrecto;

        }


        public void eliminarImprimacionMarca(string codigo)
        {

            string strsql;
            SqlConnection conexion = null;
            SqlCommand comando;
            SqlDataAdapter adapter;

            strsql = "Delete from [dbo].[T_ARTCAR] where codemp = '3' and CARACT = '19' and CODIGO = '" + codigo + "' ";


            using (conexion = new SqlConnection(Utils.CD.getConexion()))
            {
                comando = new SqlCommand(strsql, conexion);
                conexion.Open();

                int filasEfectadas = (int)comando.ExecuteNonQuery();
                if (filasEfectadas < 1)
                {
                    Console.WriteLine("No se ha modificado la marca");
                    // todoCorrecto = false;
                }
                Console.WriteLine("Marca modificada");

            }

        }

        private void btnConsultarTTT_Click(object sender, EventArgs e)
        {
            if (existeCodigo(tbCodigoTT.Text.ToString()))
            {
                if (existeTTT(tbCodigoTT.Text.ToString()))
                {
                    if (recuperarTTT(tbCodigoTT.Text.ToString()).Equals("TT"))
                    {
                        lbResultadoTT.Text = "La característica Tapar Taladro Tierra está asignada";
                    }
                    else
                    {
                        lbResultadoTT.Text = "La característica Tapar Taladro Tierra NO está asignada";
                    }
                }
                else {

                    lbResultadoTT.Text = "La característica Tapar Taladro Tierra NO está asignada";
                }


            }
            else
            {
                MessageBox.Show("El código introducido no existe", "AVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
        }

        private void btnAsignarTTT_Click(object sender, EventArgs e)
        {

         if (existeCodigo(tbCodigoTT.Text.ToString()))
            {
            if (existeTTT(tbCodigoTT.Text.ToString()))
            {
                if (recuperarTTT(tbCodigoTT.Text.ToString()).Equals("TT"))
                {
                    // SON IGUALES NO HAGO NADA
                    //MessageBox.Show("Son iguales ");
                    MessageBox.Show("No se ha podido añadir la característica (TTT) del codigo: " + tbCodigoTT.Text.ToString() + " por que la pieza ya la tiene asignada", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //UPDATE
                    actualizarTTT(tbCodigoTT.Text.ToString(), "TT", Environment.UserName);
                    MessageBox.Show("SE HA ASIGNADO TTT AL CODIGO", "FINALIZADO!", MessageBoxButtons.OK);
                    btnConsultarTTT.PerformClick();

                }

            }
            else
            {
                //INSERT IMPRIMACIÓN
                insertarTTT(tbCodigoTT.Text.ToString(), "TT", Environment.UserName);
                MessageBox.Show("SE HA ASIGNADO TTT AL CODIGO", "FINALIZADO!", MessageBoxButtons.OK);
                btnConsultarTTT.PerformClick();
            }

        }
         else
         {
             MessageBox.Show("El código introducido no existe", "AVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }

          
        }


        private void btnQuitarTTT_Click(object sender, EventArgs e)
        {

            if (existeCodigo(tbCodigoTT.Text.ToString()))
            {

                if (existeTTT(tbCodigoTT.Text.ToString()))
                {
                    eliminarTTT(tbCodigoTT.Text.ToString());
                    MessageBox.Show("SE HA ELIMINADO TTT DEL CODIGO", "FINALIZADO!", MessageBoxButtons.OK);
                    btnConsultarTTT.PerformClick();
                }
                else
                {
                    //INSERT IMPRIMACIÓN
                    MessageBox.Show("No se ha podido ELIMINAR la característica (TTT) del codigo por que no la tiene asignada ", "AVISTO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else {
                MessageBox.Show("El código introducido no existe", "AVISO!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          

        }


        public bool existeCodigo(string codigo)
        {
            bool todoCorrecto = true;
            string paquete = "";

            string sql = "select CODIGO from t_articulos " +
                " where codemp = '" + empresaGlobal.empresaID + "' and codigo = '" + codigo + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paquete = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }

                if (paquete.Equals(""))
                {
                    todoCorrecto = false;
                }
                else
                {
                    todoCorrecto = true;
                }


            }

            return todoCorrecto;

        }

        public static bool existeTTT(string marca)
        {
            bool todoCorrecto = true;
            string codigo = "";

            string sql = "select VALOR from T_ARTCAR  " +
                "where CodEMP = '3' and CARACT = '21' and CODIGO ='" + marca + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        codigo = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }

                if (codigo.Equals(""))
                {
                    todoCorrecto = false;
                }
                else
                {
                    todoCorrecto = true;
                }


            }

            return todoCorrecto;

        }


        public static string recuperarTTT(string marca)
        {
            string codigo = "";

            string sql = "select VALOR from T_ARTCAR  " +
                "where CodEMP = '3' and CARACT = '21' and CODIGO ='" + marca + "'";

            using (SqlConnection connection =
                new SqlConnection(Utils.CD.getConexion()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        codigo = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }



            }
            return codigo;

        }


        public static void actualizarTTT(string marca, string imprimacion, string user)
        {
            String strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                //MRP
                strSql = "update T_ARTCAR ";
                strSql += "		SET valor = '" + imprimacion + "',  FECHAM = GETDATE(), USUMOD = '" + user + "' ";
                strSql += "			WHERE CodEMP = '3' and CODIGO = '" + marca + "'  and caract = '21' ";
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la imprimación del código: " + marca + "  " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }


        public static void insertarTTT(string marca, string imprimacion, string user)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = " insert into T_ARTCAR";
                strSql += " select  CODEMP,'" + marca + "', CATEGORIA,'21','" + imprimacion + "', GETDATE(), NULL, '" + user + "',null,null,0 from T_ARTICULOS where codigo = '" + marca + "' and CodEMP = '3'";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al crear la estrucutura. " + ex.Message);                
                throw new Exception("Error al volcar la imprimación. " + ex.Message + ". Codigo: " + marca + " ");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

        }


        public static void eliminarTTT(string marca)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = " delete from T_ARTCAR";
                strSql += "			WHERE CodEMP = '3' and CODIGO = '" + marca + "'  and caract = '21' ";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al crear la estrucutura. " + ex.Message);                
                throw new Exception("Error al eliminar la imprimación. " + ex.Message + ". Codigo: " + marca + " ");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

        }

        private void btnLimpiarCodigo_Click(object sender, EventArgs e)
        {
            tbCodigoTT.Text = "";
        }

        private void btnBuscarTT_Click(object sender, EventArgs e)
        {
            string busqPedido = tbPedidoTTT.Text;
            string busqCodigoPadre = tbCodigoTTT.Text;
            string strSql = "";
            SqlCommand sqlCmd;
            DataTable dt = new DataTable();


            if (!(String.IsNullOrEmpty(busqPedido)) && !(String.IsNullOrEmpty(busqCodigoPadre))) {
                MessageBox.Show("Solo se puede buscar por un criterio a la vez, elimine el pedido o el código");
            }
            else if ((String.IsNullOrEmpty(busqPedido)) && (String.IsNullOrEmpty(busqCodigoPadre)))
            {
                MessageBox.Show("Los campos necesarios para la búsqueda estan vacios.");
            }
            else
            {
                string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
                SqlConnection conexion = new SqlConnection(connString);

                try
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();

                    strSql = @"SET NOCOUNT ON; 	

	                Declare @Cantidad float = 1
	                Declare @maxNivel int = 20
	                Declare @semilla int = 0

	                IF OBJECT_ID(N'tempdb..#T_Lista_Almacen', N'U') IS NOT NULL 
		                DROP TABLE #T_Lista_Almacen
    
                    SELECT '" + empresaGlobal.empresaID + @"' as CodEmp,
			                '0' as Altern,
			                0 as ID,
			                CODIGO as Codigo_Padre, 
			                @Cantidad as Cantidad_Padre, 			
			                null as ID_PADRE,
			                CODIGO as Codigo_Hijo, 
			                @Cantidad as Cantidad_Hijo, 
			                CATEGORIA as Categoria_Hijo, 
			                FAMILIA as Familia_Hijo, 
			                0 as Correlacion,
			                0 as Nivel
			                Into #T_Lista_Almacen
		                FROM T_ARTICULOS
		                WHERE CodEmp = '" + empresaGlobal.empresaID + @"' ";

                            if(!(String.IsNullOrEmpty(busqPedido))){
                                strSql += " AND CODIGO IN (select CODIGO from T_ORDTERL where NUMPED = '" + busqPedido + "') ";
                            }else if(!(String.IsNullOrEmpty(busqCodigoPadre))){
                                strSql += " AND CODIGO = '" + busqCodigoPadre + "' ";
                            }

                            strSql += @" Declare @Nivel int = 0
                    WHILE (@Nivel <= @maxNivel) AND (@@ROWCOUNT > 0) BEGIN
                        SET @Nivel = @Nivel + 1
        
                        INSERT INTO #T_Lista_Almacen
                        SELECT '3' as CodEmp,
				                '0' as Altern,
				                @semilla + row_number() over(ORDER BY @maxNivel) as Id,				
				                T_ESTRUC.CONJUN as Codigo_Padre, 
				                T1.Cantidad_Hijo as Cantidad_Padre, 				
				                T1.ID as Id_Padre,
				                T_ESTRUC.COMPON as Codigo_Hijo, 				
				                T_ESTRUC.CTDENL as Cantidad_Hijo,		
				                T_ARTICULOS.CATEGORIA as Categoria_Hijo, 
				                T_ARTICULOS.FAMILIA Familia_Hijo, 
				                T_ESTRUC.CORREL as Correlacion, 
				                @Nivel as Nivel
			                FROM #T_Lista_Almacen T1 INNER JOIN T_ESTRUC 
					                ON T1.Codigo_Hijo = T_ESTRUC.CONJUN INNER JOIN T_ARTICULOS 
					                ON T_ESTRUC.CodEMP = T_ARTICULOS.CodEMP AND T_ESTRUC.COMPON = T_ARTICULOS.CODIGO
			                WHERE (T_ESTRUC.CodEmp = '3')
					                AND (T_ESTRUC.Altern = '0')
					                AND (T1.Nivel = @Nivel - 1) 
					                AND (T1.Familia_Hijo <> 'TO')		
					                AND (T_ARTICULOS.FAMILIA <> 'MP')	
					
		                set @semilla = @semilla + @@ROWCOUNT
                    END        
	  
	                SELECT
			                t_1.Codigo_Padre,
			                t_1.Codigo_Hijo, 
                            CASE
			                when T_ARTCAR_Imprimacion.VALOR = '???' then ''
                            else T_ARTCAR_Imprimacion.VALOR
                            end as Valor,
			                CASE
			                when T_ARTCAR_Imprimacion.VALOR = 'TT'  then 'Realizado'
			                when T_ARTCAR_Imprimacion.VALOR = '???' then 'Sin TTT'
			                else 'Sin TTT' 
			                end as Imprimacion


		                FROM #T_Lista_Almacen as t_1 Left Outer Join T_ARTICULOS
			                on t_1.CodEmp = T_ARTICULOS.CodEMP and t_1.Codigo_Hijo = T_ARTICULOS.CODIGO 
			
			                Left Outer Join (SELECT CodEMP, CODIGO, CATEGO, VALOR
										                FROM T_ARTCAR
										                WHERE (CARACT = '21')
									                ) as T_ARTCAR_Imprimacion
			                on T_ARTICULOS.CodEMP = T_ARTCAR_Imprimacion.CodEMP and T_ARTICULOS.CODIGO = T_ARTCAR_Imprimacion.CODIGO and T_ARTICULOS.CATEGORIA = T_ARTCAR_Imprimacion.CATEGO		
							where T_ARTCAR_Imprimacion.VALOR = 'TT'
		                ORDER BY t_1.Nivel, t_1.Codigo_Padre, t_1.Correlacion";


                    sqlCmd = new SqlCommand(strSql, conexion);

                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    da.Fill(dt);


                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Close();
                }

                var bindingSource = new System.Windows.Forms.BindingSource();
                bindingSource.DataSource = dt;
                dgResultadosTTT.DataSource = bindingSource;


                dgResultadosTTT.Columns["Valor"].Width = 75;

                for (int i = 0; i < dgResultadosTTT.Columns.Count; i++)
                {
                    dgResultadosTTT.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                }


                foreach (DataGridViewRow res in dgResultadosTTT.Rows)
                {


                    if (Convert.ToString(res.Cells["Imprimacion"].Value).TrimEnd().Equals("Realizado"))
                    {
                        res.Cells["Imprimacion"].Style.BackColor = Color.LightGreen;
                    }

                }


            }
        }

        private void Imprimacion_Load(object sender, EventArgs e)
        {
            comboOpciones.Items.Clear();

            foreach (string valor in getValoresImprimacion())
            {
                this.comboOpciones.Items.Add(valor);

            }

            this.comboOpciones.Items.Add("Sin imprimación");
        }

        // Método a llamar cuando se quiera pintar de color verde las casillas de la tabla que muestra el resultado, creado para evitar la duplicación de código
        private void colorize()
        {
            dgResultados.Columns["Valor"].Width = 75;

            for (int i = 0; i < dgResultados.Columns.Count; i++)
            {
                dgResultados.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            }
            foreach (DataGridViewRow res in dgResultados.Rows)
            {
                if (Convert.ToString(res.Cells["Imprimacion"].Value).TrimEnd().Equals("Imprimacion realizada"))
                {
                    res.Cells["Imprimacion"].Style.BackColor = Color.Green;
                }

            }
        }

        // Método para establecer las distintas checkBox a desmarcadas
        private void uncheckBoxes(){
            checkBoxA0.Checked = false;
            checkBoxA1.Checked = false;
            checkBoxA2.Checked = false;
            checkBoxA3.Checked = false; 
        }

        
        // Método para aplicar los filtros a los datos, creado para evitar la duplicación de código
        private void apply_Filter(){
            BindingSource bs = new BindingSource();
            bs.DataSource = dgResultados.DataSource;
            string filtro = "";
            // Si el array de los parámetros del filtro no está vacío se recorre para crear el filtro que se usará en el BindingSource
            if (filtrosBusq.Count > 0)
            {
                foreach (string apartado in filtrosBusq)
                {
                    if (filtro != "")
                    {
                        filtro += " OR ";
                    }
                    filtro += "(VALOR ='" + apartado + "')";
                }
                bs.Filter = filtro;
                dgResultados.DataSource = bs;
            }
            // Se llama a la función que permite pintar la columna que indica si la imprimación está hecha en verde
            this.colorize();
        }


        // Método para comprobar si A0 ha sido marcada, en cuyo caso se añadirá a la lista de filtros
        //o desmarcada, en cuyo caso se eliminará de la lista de filtros y se pulsará sobre el botón de búsqueda si el vector de filtros está vacío
        private void checkBoxA0_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxA0.Checked)
            {
                filtrosBusq.Add("A0");
                this.apply_Filter();
            }
            else
            {
                filtrosBusq.Remove("A0");
                if (filtrosBusq.Count == 0)
                {
                    btn_busq.PerformClick();
                }
                else
                {
                    this.apply_Filter();
                }
            }
        }

        // Método para comprobar si A1 ha sido marcada, en cuyo caso se añadirá a la lista de filtros
        //o desmarcada, en cuyo caso se eliminará de la lista de filtros y se pulsará sobre el botón de búsqueda si el vector de filtros está vacío
        private void checkBoxA1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxA1.Checked)
            {
                filtrosBusq.Add("A1");
                this.apply_Filter();
            }
            else
            {
                filtrosBusq.Remove("A1");
                if (filtrosBusq.Count == 0)
                {
                    btn_busq.PerformClick();
                }
                else
                {
                    this.apply_Filter();
                }
            }
        }

        // Método para comprobar si A2 ha sido marcada, en cuyo caso se añadirá a la lista de filtros
        //o desmarcada, en cuyo caso se eliminará de la lista de filtros y se pulsará sobre el botón de búsqueda si el vector de filtros está vacío
        private void checkBoxA2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxA2.Checked)
            {
                filtrosBusq.Add("A2");
                this.apply_Filter();
            }
            else
            {
                filtrosBusq.Remove("A2");
                if (filtrosBusq.Count == 0)
                {
                    btn_busq.PerformClick();
                }
                else
                {
                    this.apply_Filter();
                }
            }
        }

        // Método para comprobar si A3 ha sido marcada, en cuyo caso se añadirá a la lista de filtros y se aplicarán los filtros, 
        //o desmarcada, en cuyo caso se eliminará de la lista de filtros y se pulsará sobre el botón de búsqueda si el vector de filtros está vacío
        private void checkBoxA3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxA3.Checked)
            {
                filtrosBusq.Add("A3");
                this.apply_Filter();                
            }
            else
            {
                filtrosBusq.Remove("A3");
                if (filtrosBusq.Count == 0)
                {
                    btn_busq.PerformClick();
                }
                else
                {
                   this.apply_Filter();
                }
            }
        }

    }
}

