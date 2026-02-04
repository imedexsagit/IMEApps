using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InventarioTablets
{
    public partial class Lotes : Form
    {
        /*
        #region Clase para controlar las teclas especiales
        [DllImport("coredll.dll", EntryPoint = "MessageBeep", SetLastError = true)]
        static extern bool MessageBeep(uint type);

        class MessageWin : MessageWindow
        {
            #region p/invoke
            [DllImport("coredll.dll")]
            public static extern bool RegisterHotKey(IntPtr hWnd, // Window handle 
                int id,						                      // Hot key identifier 
                int Modifiers,					                  // Key-modifier options 
                int key);						                  // Virtual-key code

            [DllImport("coredll.dll")]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
            #endregion p/invoke

            // Event for client
            public event HotKeyPressEventHandler HotKeyPress;
            public delegate void HotKeyPressEventHandler(int idHotKey, int fuModifiers, int uVirtKey);

            private int[] HotKeyIdList;

            public MessageWin(int[] idList, int[] modifierList, int[] keyList)
            {
                HotKeyIdList = idList;
                for (int i = 0; i < idList.Length; i++)
                    RegisterHotKey(this.Hwnd, idList[i], modifierList[i], keyList[i]);  // Register to listen for hot key(s)
            }

            ~MessageWin()
            {
                for (int i = 0; i < HotKeyIdList.Length; i++)
                    UnregisterHotKey(this.Hwnd, HotKeyIdList[i]);
            }

            private const int WM_HOTKEY = 0x0312;

            private int LOWORD(int i)
            {
                return i & 0xFFFF;
            }

            private int HIWORD(int i)
            {
                return (i >> 16) & 0xFFFF;
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    if (HotKeyPress != null)    // Raise event
                        HotKeyPress((int)m.WParam, LOWORD((int)m.LParam), HIWORD((int)m.LParam));
                }
                base.WndProc(ref m);
            }
        }

        enum KeyModifiers { None = 0, Alt = 1, Control = 2, Shift = 4, Windows = 8, Modkeyup = 0x1000, }
        #endregion
        */

        #region atributos de la clase
        private String CodEmp;
        private Int32 AnioInventario;
        private Int32 MesInventario;
        private String CadenaConexion;
        private String PDA;
        //private String Lugar;
        //private Int32 TacoMin;
        //private Int32 TacoMax;
        private String TipoMaterial;
        private Int32 UltimaFicha;

        private System.Data.SqlClient.SqlConnection conn;

        TextBox tb = new TextBox();
        bool shift = false;

        bool mantener = false;

        //Escaner
        //private ScannerServicesDriver scsDriver = null;
        //private Scanner scanner = null;

        //Teclas
        //MessageWin msgWin = null;
        #endregion

        public Lotes(String emp, Int32 anio, Int32 mes, String cad, String disp, /*Int32 min, Int32 max,*/ String mat, String Lugar)
        {
            this.CodEmp = emp;
            this.AnioInventario = anio;
            this.MesInventario = mes;
            this.CadenaConexion = cad;
            this.PDA = disp;
            //this.TacoMin = min;
            //this.TacoMax = max;
            this.TipoMaterial = mat;

            UltimaFicha = 0;

            InitializeComponent();

            this.btnCerrarTeclado.PerformClick();
        }



        //TOMÁS 10/12/2018
        private void CargarZonas()
        {
            String strsql;
            SqlCommand comando;
            SqlDataReader reader;
            DataTable dt;

            strsql = "  select zona";
            strsql += " from   tc_almacen_zonas";
            strsql += " where  codemp = '" + this.CodEmp + "' and codalm = '" + this.comboBox1.SelectedValue + "' and tipo = 'mp'";
            strsql += " order by zona";

            conn.Open();
            comando = new SqlCommand(strsql, conn);
            reader = comando.ExecuteReader();
            dt = new DataTable();
            dt.Columns.Add("zona");

            while (reader.Read())
                dt.Rows.Add(reader[0].ToString());

            comboBox_zonas.ValueMember = "zona";
            comboBox_zonas.DisplayMember = "zona";
            comboBox_zonas.DataSource = dt;
            if (comboBox_zonas.Items.Count > 0)
                comboBox_zonas.SelectedIndex = 0;

            reader.Close();
            comando.Dispose();
            conn.Close();
        }

        //TOMÁS 10/12/2018
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                CargarZonas();
        }

        private void Lotes_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            this.conn = new SqlConnection(this.CadenaConexion);

            #region obtener los almacenes disponibles
            try
            {
                String strSql2 = "SELECT CODALM as id, RAZONS as almacen";
                strSql2 += " 	    FROM T_ALMACEN";
                strSql2 += " 	    WHERE (CodEMP = '" + this.CodEmp + "') and (RAZONS not like 'capellan%') and CODALM IN (1,9,5,8,11) ";

                conn.Open();
                System.Data.SqlClient.SqlCommand comando2 = new SqlCommand(strSql2, conn);

                SqlDataReader reader2 = comando2.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("almacen");

                while (reader2.Read())
                {
                    dt.Rows.Add(Convert.ToInt32(reader2[0].ToString()), reader2[1].ToString());     //comboBox1.Items.Add(reader2[0].ToString() + ": " + reader2[1].ToString());
                }

                comboBox1.ValueMember = "id";
                comboBox1.DisplayMember = "almacen";
                comboBox1.DataSource = dt;

                comboBox1.SelectedIndex = 0;    //elijo el primer almacén por defecto

                reader2.Close();
                comando2.Dispose();
                conn.Close();

                //TOMÁS 10/12/2018
                CargarZonas();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el almacén. Introdúzcalo manualmente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            #endregion

            /*#region Habilitar el scanner
            try
            {
                scsDriver = new ScannerServicesDriver();
                scanner = new Scanner(scsDriver);

                // Register a callback for scanned data...
                scanner.ScanCompleteEvent += new ScanCompleteEventHandler(scanner_ScanCompleteEvent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al habilitar el escaner: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            #endregion

            #region habilitar lectura de teclas especiales
            string s = Environment.OSVersion.Platform.ToString();
            if (String.Equals(s.ToUpper(), "WINCE"))
            {
                try
                {
                    int[] idList = { 1, 2 };
                    int[] modifierList = { (int)KeyModifiers.None, (int)KeyModifiers.None };
                    int[] keyList = { (int)Keys.F1, (int)Keys.F4 };

                    msgWin = new MessageWin(idList, modifierList, keyList);
                    msgWin.HotKeyPress += new MessageWin.HotKeyPressEventHandler(OnHotKeyPress);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al habilitar las teclas especiales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            #endregion
            */

            this.textBox2.Focus();

            //MMM 11_12_17
            /*if (!String.Equals(s.ToUpper(), "WINCE"))
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, 0);*/
        }


        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox2.Text[0].ToString() == "'" || this.textBox2.Text[0].ToString() == "-")
                {
                    this.textBox2.Text = this.textBox2.Text.Remove(0, 1);
                }
                this.textBox3.Text = String.Empty;
                this.textBox3.Focus();
            }
        }



        //Comprobar que la FICHA es válida
        private void textBox2_LostFocus(object sender, EventArgs e)
        {
            Int32 ficha = 0;

            if (this.textBox2.Text != String.Empty)
            {
                try
                {
                    //ficha = Int32.Parse(textBox2.Text);

                    //JRegino 26/12/2017. Como las tablets no lanzan el ScannerCallback, no se pone en positivo, no cambia el focus. Además si el teclado esta en español, los num. negativos los mete con un apostrofe y no con el sigo -
                    ficha = Math.Abs(Int32.Parse(textBox2.Text));
                    this.textBox2.Text = ficha.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("La FICHA debe ser un número entero válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    this.textBox1.Text = String.Empty;
                    this.textBox2.Text = String.Empty;
                    this.textBox2.Focus();

                    return;
                }

                //JRegino 11/12/2013 Comprobar que no se saltan la lectura de una ficha (al menos avisar)
                if (((ficha - UltimaFicha) > 1) && (UltimaFicha != 0))
                    MessageBox.Show("Se ha saltado la lectura de la ficha: " + Convert.ToString(UltimaFicha + 1), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);



                if ((ficha % 100) == 0)
                    this.textBox1.Text = Convert.ToString(ficha / 100);
                else
                    this.textBox1.Text = Convert.ToString((ficha / 100) + 1);
            }
        }


        /*
        // This is the registered callback, but we don't want to do any real work here... Do all of your work, including enabling/disabling the scanner from ScannerCallback
        delegate void scanner_ScanCompleteDelegate(object sender, ScanCompleteEventArgs e);
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
            // you can safely disable the scanner and then do whatever you like
            scanner.Enabled = false;


            if (textBox2.Focused == true)
            {
                Int32 aux_ficha = Convert.ToInt32(e.Text);
                if (aux_ficha > 0)
                {
                    MessageBox.Show("La lectura no corresponde a una FICHA válida de inventario", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    this.textBox2.Text = Convert.ToString(aux_ficha * -1);
                    textBox3.Focus();
                }
            }
            else
            {
                if (textBox3.Focused == true)
                {
                    if (Convert.ToInt32(e.Text) < 0)
                    {
                        MessageBox.Show("La lectura no corresponde a un Lote", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        this.textBox3.Text = e.Text;
                        textBox3_KeyDown(this, new KeyEventArgs(Keys.Enter));
                    }
                }
            }


            // Re-enable the scanner
            System.Threading.Thread.Sleep(1000);     //dejar deshabilitado 1 segundo para que no se produzca una doble lectura
            scanner.Enabled = true;
        }



        //Capturar pulsacion tecla F1(Guardar), F4 (Salir)
        void OnHotKeyPress(int idHotKey, int fuModifiers, int uVirtKey)
        {
            switch ((Keys)uVirtKey)
            {
                case Keys.F1:
                    button1_Click(this, null);
                    break;
                case Keys.F4:
                    button4_Click(this, null);
                    break;

                default:
                    break;
            }
        }*/




        //al pulsar intro sobre el lote, quiere decir que ya se ha tecleado entero el lote
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox3.Text == "0")
                {
                    this.comboBox2.Visible = true;
                    this.comboBox3.Visible = true;

                    this.comboBox3.Focus();
                }
                else
                {
                    this.comboBox2.Visible = false;
                    this.comboBox3.Visible = false;

                    this.obtenerPesoLote(this.textBox3.Text, "");
                }
            }
            else
                this.textBox4.Text = String.Empty;
        }



        private void textBox4_GotFocus(object sender, EventArgs e)
        {
            this.textBox4.SelectAll();
        }
        /*
                private void textBox4_KeyDown(object sender, KeyEventArgs e)
                {
                    if ((e.KeyCode == Keys.F1) || (e.KeyCode == Keys.Enter))    //Guardar
                        button1_Click(this, null);
                    if (e.KeyCode == Keys.F4)                                   //Salir
                        button4_Click(this, null);
                }
        */



        //seleccionar la categoria de la MP => relleno los codigos de lote = 0
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Items.Clear();

            if (this.comboBox3.Text != String.Empty)
            {
                String strSql3 = "SELECT T_STOCKS.codigo, T_ARTICULOS.denominacion";
                strSql3 += " 	    FROM T_ARTICULOS INNER JOIN T_STOCKS ON T_ARTICULOS.CodEMP = T_STOCKS.CodEMP AND T_ARTICULOS.CODIGO = T_STOCKS.CODIGO ";
                strSql3 += " 	    WHERE (T_Articulos.CodEMP = '" + this.CodEmp + "') and (T_Stocks.lote = '0') and (T_Articulos.Familia = 'MP') and (T_Articulos.Categoria = '" + this.comboBox3.Text + "')";

                conn.Open();
                System.Data.SqlClient.SqlCommand comando3 = new SqlCommand(strSql3, conn);

                SqlDataReader reader3 = comando3.ExecuteReader();

                while (reader3.Read())
                {
                    comboBox2.Items.Add(reader3[0].ToString());
                }

                reader3.Close();
                comando3.Dispose();
                conn.Close();

                comboBox2.Focus();
            }
        }


        //al elegir el articulo => hallar el stock de ese lote
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.Text != "")
                this.obtenerPesoLote(this.textBox3.Text, this.comboBox2.Text);
        }




        //obtiene el peso de un lote. Si el lote es 0 => se debe filtar x el codigo
        private void obtenerPesoLote(String lote, String codigo)
        {
            if ((lote == "0") && (codigo == String.Empty))
            {
                MessageBox.Show("Si quiere obtener el Stock del Lote 0, debe indicar el codigo del artículo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            try
            {
                //limpio los valores antiguos 
                this.label10.Text = "No EXISTE!!!";
                this.label11.Text = "No EXISTE!!!";
                this.textBox4.Text = String.Empty;
                this.textBox3.SelectAll();  //lo selecciono por si el lote fuera incorrecto


                //String strSql = "SELECT ISNULL(round(SUM(T_STOCKS.STOCK), 0), '-999') AS stock, ";
                //strSql += "             ISNULL(T_STOCKS.CODIGO, '') AS codigo, ";
                //strSql += "             ISNULL(T_ARTICULOS.DENOMINACION, '') AS denominacion, ";
                //strSql += "             (Select COUNT(*) From T_MOVIM Where CODEMP = '" + this.CodEmp + "' and LOTE = '" + lote + "' and Codigo = T_STOCKS.codigo and (TTRANS <> '3' and TTRANS <> '7' and TTRANS <> '8')) as num_movimientos";  //JRegino 11/12/2013 Aligerar la consulta incluyendo el campo codigo. strSql += "             (Select COUNT(*) From T_MOVIM Where CODEMP = '" + this.CodEmp + "' and LOTE = '" + lote + "' and (TTRANS <> '3' and TTRANS <> '7' and TTRANS <> '8')) as num_movimientos";   //strSql += "             (Select COUNT(*) From TC_SALIDAS_FAB Where CODEMP = T_STOCKS.CodEMP and LOTE = T_STOCKS.LOTE) as num_movimientos";
                //strSql += "         FROM T_STOCKS INNER JOIN T_ARTICULOS ON T_STOCKS.CodEMP = T_ARTICULOS.CodEMP AND T_STOCKS.CODIGO = T_ARTICULOS.CODIGO ";
                //strSql += "         WHERE (T_STOCKS.CodEMP = '" + this.CodEmp + "') AND (T_ARTICULOS.FAMILIA = 'MP') AND (T_STOCKS.LOTE = '" + lote + "') ";    //JRegino 11/12/2013 Quitar el filtro de obsoleto      strSql += "         WHERE (T_STOCKS.CodEMP = '" + this.CodEmp + "') AND (T_ARTICULOS.FAMILIA = 'MP') AND (T_ARTICULOS.CLAOBS = 0) AND (T_STOCKS.LOTE = '" + lote + "') ";

                //if (codigo != "")   //caso en el que el lote es 0 y quiero el stock de un articulo
                //    strSql += "             AND (T_STOCKS.CODIGO = '" + codigo + "') ";

                //strSql += "         GROUP BY T_STOCKS.codigo, T_ARTICULOS.denominacion";

                String strSql = "SELECT TOP(1) ISNULL(round(T_STOCKS.STOCK, 0), '-999') AS stock, ";
                strSql += "             ISNULL(T_STOCKS.CODIGO, '') AS codigo, ";
                strSql += "             ISNULL(T_ARTICULOS.DENOMINACION, '') AS denominacion, ";
                strSql += "             (Select COUNT(*) From T_MOVIM Where CODEMP = '" + this.CodEmp + "' and LOTE = '" + lote + "' and  (TTRANS <> '3' and TTRANS <> '7' and TTRANS <> '8')) as num_movimientos";  //JRegino 11/12/2013 Aligerar la consulta incluyendo el campo codigo. strSql += "             (Select COUNT(*) From T_MOVIM Where CODEMP = '" + this.CodEmp + "' and LOTE = '" + lote + "' and (TTRANS <> '3' and TTRANS <> '7' and TTRANS <> '8')) as num_movimientos";   //strSql += "             (Select COUNT(*) From TC_SALIDAS_FAB Where CODEMP = T_STOCKS.CodEMP and LOTE = T_STOCKS.LOTE) as num_movimientos";
                strSql += "         FROM T_STOCKS INNER JOIN T_ARTICULOS ON T_STOCKS.CodEMP = T_ARTICULOS.CodEMP AND T_STOCKS.CODIGO = T_ARTICULOS.CODIGO ";
                strSql += "         WHERE (T_STOCKS.CodEMP = '" + this.CodEmp + "') AND (T_ARTICULOS.FAMILIA = 'MP') AND (T_STOCKS.LOTE = '" + lote + "') ";    //JRegino 11/12/2013 Quitar el filtro de obsoleto      strSql += "         WHERE (T_STOCKS.CodEMP = '" + this.CodEmp + "') AND (T_ARTICULOS.FAMILIA = 'MP') AND (T_ARTICULOS.CLAOBS = 0) AND (T_STOCKS.LOTE = '" + lote + "') ";

                strSql += "         ORDER BY T_STOCKS.STOCK desc, T_STOCKS.FECHAC desc";



                this.conn.Open();
                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    String aux = reader.GetValue(0).ToString();
                    Int32 num_movimientos = Convert.ToInt32(reader.GetValue(3));

                    if (aux == "-999")
                    {
                        MessageBox.Show("El lote indicado no existe, hable con el Dpto de Compras-Calidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        this.textBox4.Text = String.Empty;
                        this.label10.Text = "No EXISTE!!!";
                        this.label11.Text = "No EXISTE!!!";

                        this.textBox3.Focus();  //foco al lote
                    }
                    else
                    {
                        if (num_movimientos == 0)       //Si el lote ha tenido movimientos <> a compras-mermas => hay que pesarlo
                            this.textBox4.Text = aux;
                        else
                            this.textBox4.Text = "0";

                        this.label10.Text = reader.GetValue(1).ToString();
                        this.label11.Text = reader.GetValue(2).ToString();

                        this.textBox4.Focus();  //foco al peso
                    }
                }

                reader.Close();

                comando.Dispose();

                this.conn.Close();


                //JRegino 11/12/2013
                if (this.label10.Text == "No EXISTE!!!")
                {
                    MessageBox.Show("El lote no tiene asociada ninguna MP, se almacenará pero indíquelo al Dpto de Compras-Calidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error al obtener el Stock del Lote", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }

        }



        //JRegino 09/12/2015. Comprobar si el lote ya se ha almacenado. Devuelve true en el caso en el que ya se haya almacenado
        private bool loteAlmacenado(String lote)
        {
            try
            {
                this.conn.Open();

                String strSql = "SELECT count(Ficha) ";
                strSql += "	        FROM TC_Inventario_Pesado ";
                strSql += "	        WHERE (Codemp = '" + this.CodEmp + "') AND (Anio = " + this.AnioInventario + ") AND (Lote = '" + lote + "') ";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                Int32 aux = Convert.ToInt32(comando.ExecuteScalar().ToString());
                comando.Dispose();
                conn.Close();


                if (aux == 0)
                    return false;
                else
                    return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al comprobar si el lote ya esta almacenado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }



        //guardar los datos
        private void button1_Click(object sender, EventArgs e)
        {
            #region comprobar q se han introducido todos los datos
            if ((textBox1.Text == String.Empty) || (textBox2.Text == String.Empty) || (textBox3.Text == String.Empty) || (textBox4.Text == String.Empty) || comboBox1.Text == String.Empty || comboBox_zonas.Text == String.Empty)
            {
                MessageBox.Show("Debe rellenar todos los datos antes de guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (textBox4.Text != String.Empty)
            {
                try
                {
                    Int32.Parse(textBox4.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("El peso debe ser un numero entero válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            //MMM 3_1_18 Corregir un posible error.
            if (label10.Text == String.Empty)
            {
                MessageBox.Show("El código no debe estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            #endregion

            if (this.loteAlmacenado(this.textBox3.Text))
            {
                MessageBox.Show("El lote ya se ha inventariado. Hable con el Dpto de Compras", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }


            try
            {
                this.conn.Open();

                //realizo la insercion de un paquete completo
                String strSql = "";
                strSql = "INSERT INTO TC_Inventario_Pesado (CODEMP, ANIO, MES, TACO, FICHA, LOTE, CODIGO, Stock, ALMACEN, PASILLO, TIPO, FECHAC, usuarioc) ";
                strSql += " VALUES ('" + CodEmp + "', " + AnioInventario + ", " + MesInventario + ", " + this.textBox1.Text + ", " + this.textBox2.Text + ", " + this.textBox3.Text + ", '" + this.label10.Text + "', " + this.textBox4.Text + ", " + this.comboBox1.SelectedValue + ", '" + this.comboBox_zonas.SelectedValue + "', '" + TipoMaterial + "', getdate(), '" + this.PDA + "')";

                System.Data.SqlClient.SqlCommand comando2 = new SqlCommand(strSql, this.conn);

                comando2.ExecuteNonQuery();

                comando2.Dispose();

                this.conn.Close();


                //JRegino 11/12/2013. Almacenar la ultima ficha leida para poder indicar en la proxima lectura si se han saltado alguna
                UltimaFicha = Convert.ToInt32(this.textBox2.Text);
            }
            catch (SqlException ex)
            {
                if (ex.Message.ToUpper().IndexOf("PRIMARY KEY") > 0)
                    MessageBox.Show("Esta ficha ya se ha introducido", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                else
                    if (ex.Message.ToUpper().IndexOf("UNIQUE INDEX") > 0)
                        MessageBox.Show("El lote ya se ha inventariado. Hable con el Dpto de Compras", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    else
                        MessageBox.Show("Error al Guardar los Datos: " + ex.Message, "Error Guardando datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }


            
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.label10.Text = "";
            this.label11.Text = "";

            if (mantener)
            {
                this.textBox3.Focus();
            }
            else
            {
                this.textBox2.Focus();      //le dejo el foco al lote xa q realicen la nueva lectura
                this.textBox2.Text = "";
            }
        }


        //salir del formulario
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Lotes_Closed(object sender, EventArgs e)
        {
            if (conn != null)
                if (conn.State != ConnectionState.Closed)
                    conn.Close();



            // You must call Dispose on each scanner class. Ensure that you deregister the callback as well...
            /*if (scanner != null)
            {
                scanner.ScanCompleteEvent -= new ScanCompleteEventHandler(scanner_ScanCompleteEvent);
                scanner.Dispose();
            }

            if (scsDriver != null)
            {
                scsDriver.Dispose();
            }


            //Liberar teclas
            if (msgWin != null)
            {
                msgWin.HotKeyPress -= new MessageWin.HotKeyPressEventHandler(OnHotKeyPress);
                msgWin.Dispose();
            }*/
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnBorrarFicha_Click(object sender, EventArgs e)
        {
            this.textBox2.Clear();
            this.textBox2.Select();
        }

        private void btnBorrarLote_Click(object sender, EventArgs e)
        {
            this.textBox3.Clear();
            this.textBox3.Select();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            this.conn.Open();

            string strsql = "";
            strsql = strsql + " SELECT Taco, Ficha, Lote, Codigo, Stock, TC_Inventario_Pesado.fechaC, RAZONS as ALMACEN ";
            strsql = strsql + "FROM TC_Inventario_Pesado left join T_ALMACEN on T_ALMACEN.CODALM = TC_Inventario_Pesado.Almacen ";
            strsql = strsql + "WHERE (Anio = " + AnioInventario + ") and TC_Inventario_Pesado.CodEmp = '" + CodEmp + "' and T_ALMACEN.CodEMP = '" + CodEmp + "' and usuarioC = '" + this.PDA + "'";
            strsql = strsql + " ORDER BY fechaC DESC";

            SqlCommand comando = new SqlCommand(strsql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            DataTable table = new DataTable();
            adapter.Fill(table);

            this.conn.Close();

            this.dgvHistorial.DataSource = table;
            this.panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.textBox2.Focus();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (this.dgvHistorial.FirstDisplayedScrollingRowIndex > 0)
            {
                this.dgvHistorial.FirstDisplayedScrollingRowIndex--;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dgvHistorial.FirstDisplayedScrollingRowIndex < this.dgvHistorial.Rows.Count && this.dgvHistorial.Rows.Count < 0)
            {
                this.dgvHistorial.FirstDisplayedScrollingRowIndex++;
            }
        }

        

        private void textBox3_Enter(object sender, EventArgs e)
        {
            this.tb.BackColor = Color.White; 
            tb = this.textBox3;
            this.tb.BackColor = Color.LightGreen;
            this.tb.Select();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            this.tb.BackColor = Color.White; 
            tb = this.textBox2;
            this.tb.BackColor = Color.LightGreen;
            this.tb.Select();
        }

        private void textBox4_Enter_1(object sender, EventArgs e)
        {
            this.tb.BackColor = Color.White; 
            tb = this.textBox4;
            this.tb.BackColor = Color.LightGreen;
            this.tb.Select();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            escribir(tb, "d");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            escribir(tb, "c");
        }

        private void P_Click(object sender, EventArgs e)
        {
            escribir(tb, "p");
        }

        private void GUION_Click(object sender, EventArgs e)
        {
             escribir(tb, "-");
        }

        private void PUNTO_Click(object sender, EventArgs e)
        {
             escribir(tb, ".");
        }

        private void COMA_Click(object sender, EventArgs e)
        {
             escribir(tb, ",");
        }

        private void M_Click(object sender, EventArgs e)
        {
            escribir(tb, "m");
        }

        private void N_Click(object sender, EventArgs e)
        {
            escribir(tb, "n");
        }

        private void B_Click(object sender, EventArgs e)
        {
            escribir(tb, "b");
        }

        private void V_Click(object sender, EventArgs e)
        {
            escribir(tb, "v");
        }

        private void X_Click(object sender, EventArgs e)
        {
            escribir(tb, "x");
        }

        private void Z_Click(object sender, EventArgs e)
        {
            escribir(tb, "z");
        }

        private void Ñ_Click(object sender, EventArgs e)
        {
            escribir(tb, "ñ");
        }

        private void L_Click(object sender, EventArgs e)
        {
            escribir(tb, "l");
        }

        private void K_Click(object sender, EventArgs e)
        {
            escribir(tb, "k");
        }

        private void J_Click(object sender, EventArgs e)
        {
            escribir(tb, "j");
        }

        private void H_Click(object sender, EventArgs e)
        {
            escribir(tb, "h");
        }

        private void G_Click(object sender, EventArgs e)
        {
            escribir(tb, "g");
        }

        private void F_Click(object sender, EventArgs e)
        {
            escribir(tb, "f");
        }

        private void S_Click(object sender, EventArgs e)
        {
            escribir(tb, "s");
        }

        private void A_Click(object sender, EventArgs e)
        {
            escribir(tb, "a");
        }

        private void SHIFT_Click(object sender, EventArgs e)
        {
            if (this.shift)
            {
                this.SHIFT.BackColor = Color.Silver;
                
            }
            else
            {
                this.SHIFT.BackColor = Color.LightGreen;
            }

            this.shift = !shift;
        }

        private void O_Click(object sender, EventArgs e)
        {

            escribir(tb, "o");
        }

        private void I_Click(object sender, EventArgs e)
        {
            escribir(tb, "i");
        }

        private void U_Click(object sender, EventArgs e)
        {
            escribir(tb, "u");
        }

        private void Y_Click(object sender, EventArgs e)
        {
            escribir(tb, "y");
        }

        private void T_Click(object sender, EventArgs e)
        {
            escribir(tb, "t");
        }

        private void R_Click(object sender, EventArgs e)
        {
            escribir(tb, "r");
        }

        private void E_Click(object sender, EventArgs e)
        {
            escribir(tb, "e");
        }

        private void W_Click(object sender, EventArgs e)
        {

            escribir(tb, "w");
        }

        private void Q_Click(object sender, EventArgs e)
        {

            escribir(tb, "q");
        }

        private void DEL_Click(object sender, EventArgs e)
        {
            if (tb.Text.Length > 0)
            {
                this.tb.Text = tb.Text.Remove(tb.Text.Length - 1);
            }
        }

        private void ESPACIO_Click(object sender, EventArgs e)
        {
            escribir(tb, " ");
        }

        private void btnNum1_Click(object sender, EventArgs e)
        {
            escribir(tb, "1");
        }

        private void btnNum2_Click(object sender, EventArgs e)
        {
            escribir(tb, "2");
        }

        private void btnNum3_Click(object sender, EventArgs e)
        {
            escribir(tb, "3");
        }

        private void btnNum4_Click(object sender, EventArgs e)
        {
            escribir(tb, "4");
        }

        private void btnNum5_Click(object sender, EventArgs e)
        {
            escribir(tb, "5");
        }

        private void btnNum6_Click(object sender, EventArgs e)
        {
            escribir(tb, "6");
        }

        private void btnNum7_Click(object sender, EventArgs e)
        {
            escribir(tb, "7");
        }

        private void btnNum8_Click(object sender, EventArgs e)
        {
             escribir(tb, "8");
        }

        private void btnNum9_Click(object sender, EventArgs e)
        {
            escribir(tb, "9");
        }

        private void btnCerrarTeclado_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
            this.tb.BackColor = Color.White; this.tb.Select();
        }

        private void btnTeclado_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = true;
        }

        private void btnNum0_Click(object sender, EventArgs e)
        {
            escribir(tb, "0");
        }

        private void escribir(TextBox tbox, string texto)
        {
            if (shift)
            {
                tbox.Text = tbox.Text + texto.ToUpper();
            }
            else
            {
                tbox.Text = tbox.Text + texto.ToLower();
            }
            tbox.SelectionStart = tbox.Text.Length;
            tbox.SelectionLength = 0;
            tbox.Select();
        }

        private void btnPeso_Click(object sender, EventArgs e)
        {
            this.obtenerPesoLote(this.textBox3.Text, "");
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(908, 293);
            this.panel2.Location = new Point(12, 255);
            this.panel2.Visible = true;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(908, 293);
            this.panel2.Location = new Point(12, 255);
            this.panel2.Visible = true;
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(190, 293);
            this.panel2.Location = new Point(740, 255);
            this.panel2.Visible = true;
        }

        private void btnMantenerFicha_Click(object sender, EventArgs e)
        {
            this.mantener = !mantener;

            if (mantener)
            {
                this.btnMantenerFicha.BackColor = Color.LightGreen;
                this.btnMantenerFicha.Text = "SÍ";
            }
            else
            {
                this.btnMantenerFicha.BackColor = Color.LightCoral;
                this.btnMantenerFicha.Text = "NO";
            }
        }

        

        




    }
}
