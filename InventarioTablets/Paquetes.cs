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
    public partial class Paquetes : Form
    {


        /*#region Clase para controlar las teclas especiales
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
        #endregion*/


        #region atributos de la clase
        private String CodEmp;
        private Int32 AnioInventario;
        private String CadenaConexion;
        private String PDA;
        //private Int32 TacoMin;
        //private Int32 TacoMax;
        private String TipoMaterial;
        private String Lugar;

        private System.Data.SqlClient.SqlConnection conn;

        TextBox tb = new TextBox();

        bool mantener = false;

        //Escaner
        //private ScannerServicesDriver scsDriver = null;
        //private Scanner scanner = null;

        //Teclas
        //MessageWin msgWin = null;
        #endregion


        public Paquetes(String emp, Int32 anio, String cad, String disp, /*Int32 min, Int32 max,*/ String mat, String lugar)
        {
            this.CodEmp = emp;
            this.AnioInventario = anio;
            this.CadenaConexion = cad;
            this.PDA = disp;
            //this.TacoMin = min;
            //this.TacoMax = max;
            this.TipoMaterial = mat;
            this.Lugar = lugar;

            InitializeComponent();
        }


        private void Paquetes_Load(object sender, EventArgs e)
        {
            this.conn = new SqlConnection(this.CadenaConexion);

            this.WindowState = FormWindowState.Maximized;


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
            //if (!String.Equals(s.ToUpper(), "WINCE"))
                //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, 0);
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

                    //JRegino 26/12/2017
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

                if ((ficha % 100) == 0)
                    this.textBox1.Text = Convert.ToString(ficha / 100);
                else

                    this.textBox1.Text = Convert.ToString((ficha / 100) + 1);
            }
        }




        // This is the registered callback, but we don't want to do any real work here... Do all of your work, including enabling/disabling the scanner from ScannerCallback
        /*delegate void scanner_ScanCompleteDelegate(object sender, ScanCompleteEventArgs e);
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
                    this.textBox3.Text = e.Text;
                    //textBox3_KeyDown(this, new KeyEventArgs(Keys.Enter));
                }
            }


            // Re-enable the scanner
            System.Threading.Thread.Sleep(1000);     //dejar deshabilitado 1 segundo para que no se produzca una doble lectura
            scanner.Enabled = true;
        }


        //Capturar pulsacion tecla F1, F4
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


        private Boolean existePaquete(String paquete)
        {
            try
            {
                this.conn.Open();

                String strSql = "SELECT count(cb_num) ";
                strSql += "	        FROM imedexsa_intranet.dbo.GL_PAQUE ";
                strSql += "	        WHERE (empresa = '" + this.CodEmp + "') AND (estado = 'ACE') AND (cb_num = " + paquete + ") ";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                Int32 aux = Convert.ToInt32(comando.ExecuteScalar().ToString());    //String aux = comando.ExecuteScalar().ToString();
                comando.Dispose();
                this.conn.Close();


                if (aux == 0)
                    return false;
                else
                    return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al comprobar el paquete: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }

        }


        //JRegino 09/12/2015. Comprobar si el paquete ya se ha almacenado. Devuelve true en el caso en el que ya se haya almacenado
        private bool paqueteAlmacenado(String paquete)
        {
            try
            {
                this.conn.Open();

                String strSql = "SELECT count(Ficha) ";
                strSql += "	        FROM TC_Inventario_Contado ";
                strSql += "	        WHERE (CodEmp = '" + this.CodEmp + "') AND (Ano = " + this.AnioInventario + ") ";
                strSql += "	                AND (Categoria = 'EM') AND (Tipo = '" + this.TipoMaterial + "') AND (maquina = '" + paquete + "') ";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                Int32 aux = Convert.ToInt32(comando.ExecuteScalar());
                comando.Dispose();
                this.conn.Close();


                if (aux == 0)
                    return false;
                else
                    return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al comprobar si el paquete ya esta almacenado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return true;
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }

        }


        //guardar los datos
        private void button1_Click(object sender, EventArgs e)
        {
            #region comprobar q se han introducido todos los datos
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == ""))
            {
                MessageBox.Show("Debe rellenar todos los datos ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            #endregion

            String aux_paquete = this.textBox3.Text.Substring(2);

            if (this.existePaquete(aux_paquete) == false)
            {
                MessageBox.Show("El paquete indicado no existe, hable con el Dpto de Expediciones", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (this.paqueteAlmacenado(aux_paquete))
            {
                MessageBox.Show("El paquete ya se ha inventariado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }



            #region realizar la insercion del paquete completo
            try
            {
                this.conn.Open();

                String strSql = "INSERT INTO TC_INVENTARIO_CONTADO (ANO, CODEMP, TACO, FICHA, CODIGO, CANTIDAD, FAMILIA, CATEGORIA, maquina, PROYECTO, TIPO, Observaciones, FECHAC, usuarioc, centro) ";
                strSql += "         SELECT " + this.AnioInventario + "," + this.CodEmp + "," + this.textBox1.Text + "," + this.textBox2.Text + ",CODIGO,imedexsa_intranet.dbo.GL_ITEMSPAQ.CANTIDAD,FAMILIA,'EM', '" + aux_paquete + "', CODLANZA,'" + this.TipoMaterial + "',TC_V_LANZAMIENTO_ETIQ_PROYECTO.Etiqueta, getdate(), '" + this.PDA + "', '" + this.Lugar + "'";
                strSql += "             FROM TC_V_LANZAMIENTO_ETIQ_PROYECTO INNER JOIN imedexsa_intranet.dbo.GL_ITEMSPAQ ON imedexsa_intranet.dbo.GL_ITEMSPAQ.cb = TC_V_LANZAMIENTO_ETIQ_PROYECTO.Etiqueta INNER JOIN imedexsa_intranet.dbo.GL_PAQUE ON imedexsa_intranet.dbo.GL_ITEMSPAQ.paque_id = imedexsa_intranet.dbo.GL_PAQUE.id ";
                strSql += "             WHERE (empresa = '" + this.CodEmp + "') AND (imedexsa_intranet.dbo.GL_PAQUE.cb_num = " + aux_paquete + ")";

                System.Data.SqlClient.SqlCommand comando = new SqlCommand(strSql, this.conn);

                comando.ExecuteNonQuery();
                comando.Dispose();
                this.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Guardar los Datos: " + ex.Message, "Error Guardando datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (this.conn.State != ConnectionState.Closed)
                    this.conn.Close();
            }
            #endregion


            if (mantener)
            {
                this.textBox3.Focus();
            }
            else
            {
                this.textBox2.Focus();      //le dejo el foco al lote xa q realicen la nueva lectura
                this.textBox2.Text = "";
            }
            this.textBox3.Text = String.Empty;
            //this.textBox2.Focus();
        }

        //salir del formulario
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Paquetes_Closed(object sender, EventArgs e)
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

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            this.conn.Open();

            string strsql = "";
            strsql = strsql + " select distinct Taco as TACO, ficha AS FICHA, concat(73, maquina) AS [PAQUETE GALVA], Tipo AS TIPO, fechaC AS FECHA from TC_Inventario_contado where Categoria = 'EM' and tipo = 'Negro' and Ano = '" + AnioInventario + "' and Centro like '" + Lugar + "' and usuarioC = '" + this.PDA + "'";
            strsql = strsql + " ORDER BY fechaC DESC";

            SqlCommand comando = new SqlCommand(strsql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            DataTable table = new DataTable();
            adapter.Fill(table);

            this.conn.Close();

            this.dgvHistorial.DataSource = table;
            this.panel1.Visible = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

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

        private void btnNum0_Click(object sender, EventArgs e)
        {
            escribir(tb, "0");
        }

        private void btnNumAC_Click(object sender, EventArgs e)
        {
            if (tb.Text.Length > 0)
            {
                this.tb.Text = tb.Text.Remove(tb.Text.Length - 1);
            }
            this.tb.Select();
        }

        private void btnGuion_Click(object sender, EventArgs e)
        {
            escribir(tb, "-");
        }



        private void escribir(TextBox tbox, string texto)
        {
            tbox.Text = tbox.Text + texto;
            tbox.SelectionStart = tbox.Text.Length;
            tbox.SelectionLength = 0;
            tbox.Select();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            this.tb.BackColor = Color.White; 
            tb = this.textBox2;
            this.tb.BackColor = Color.LightGreen;
            this.tb.Select();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            this.tb.BackColor = Color.White; 
            tb = this.textBox3;
            this.tb.BackColor = Color.LightGreen;
            this.tb.Select();
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
