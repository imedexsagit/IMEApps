using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Data.SqlClient;

namespace InventarioTablets
{
    public partial class Prin : Form
    {


        

       
        enum KeyModifiers { None = 0, Alt = 1, Control = 2, Shift = 4, Windows = 8, Modkeyup = 0x1000, }



        #region atributos de la clase
        private String CodEmp;
        private Int32 AnioInventario;
        private Int32 MesInventario;
        private String CadenaConexion;
        private String PDA;
        private String Lugar;
        //private Int32 TacoMin;
        //private Int32 TacoMax;
        private String MatMP;
        private String MatTO;
        private String MatNegro;
        private String MatCurso;
        private String MatBlanco;


        private ArrayList listaMaquina;


        //Teclas
        #endregion



        public Prin()
        {
            InitializeComponent();
        }

        private void Prin_Load(object sender, EventArgs e)
        {

            
            try
            {
                this.obtenerValoresFicheroConfiguracion();


                this.label5.Text = this.AnioInventario.ToString();
                this.label3.Text = this.PDA;

                string ssid = showConnectedId();

                

                if (ssid.Length < 30 && ssid != "")
                {
                    if (ssid.ToUpper().Contains("IMEDEXSA"))
                    {
                        if (!ssid.ToUpper().Contains("SC"))
                        {
                            comboBox2.SelectedIndex = comboBox2.FindStringExact("CC");
                            this.labelUbic.Text = "CASAR DE CÁCERES";
                            this.Lugar = comboBox2.Text;
                        }
                        else
                        {
                            comboBox2.SelectedIndex = comboBox2.FindStringExact("SC");
                            this.labelUbic.Text = "SANTIAGO DEL CAMPO";
                            this.Lugar = comboBox2.Text;
                        }
                    }
                    else
                    {
                        comboBox2.SelectedIndex = comboBox2.FindStringExact("MC");
                        this.labelUbic.Text = "MEDINA DEL CAMPO";
                        this.Lugar = comboBox2.Text;
                    }
                }
                else
                {
                    comboBox2.Visible = true;
                    labelUbic.Visible = false;
                    ssid = "-";
                }

                this.labelSSID.Text = ssid;

                /*#region habilitar lectura de teclas especiales

                string s = Environment.OSVersion.Platform.ToString();
                //MessageBox.Show("Plataforma: " + s);
                if (String.Equals(s.ToUpper(), "WINCE"))
                {
                    try
                    {
                        int[] idList = { 1, 2, 3, 4, 5, 6 };
                        int[] modifierList = { (int)KeyModifiers.None, (int)KeyModifiers.None, (int)KeyModifiers.None, (int)KeyModifiers.None, (int)KeyModifiers.None, (int)KeyModifiers.None };
                        int[] keyList = { (int)Keys.F5, (int)Keys.F6, (int)Keys.F7, (int)Keys.F8, (int)Keys.F9, (int)Keys.F10 };

                        msgWin = new MessageWin(idList, modifierList, keyList);
                        msgWin.HotKeyPress += new MessageWin.HotKeyPressEventHandler(OnHotKeyPress);
                    }
                    catch (Exception ex)
                    {
                        //throw new Exception("Error al habilitar las teclas especiales: " + ex.Message);

                        //JRegino 11/12/2017                        
                        MessageBox.Show("Error al habilitar las teclas especiales: " + ex.Message);
                    }
                }
                #endregion

                if (!String.Equals(s.ToUpper(), "WINCE"))
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, 0);
                */

                #region Obtener las maquinas xa rellenar el combo del form del curso, asi solo se realiza la consulta una vez
                listaMaquina = new ArrayList();
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(this.CadenaConexion); ;

                try
                {
                    conn.Open();

                    String strSql = " ";
                    strSql = " Select ";
                    strSql += "t_aux.PUESTO as Maqui, ";
                    strSql += "(Select ABREV From T_OPERAC Where CodEMP = t_aux.CodEMP and OPERAC = t_aux.OPERAC and PUESTO = t_aux.PUESTO)  ";
                    strSql += "From ";
                    strSql += "	( SELECT ";
                    strSql += "		T_OPERAC.CodEmp, ";
                    strSql += "		min(T_OPERAC.PUESTO) as PUESTO, ";
                    strSql += "		OPERAC ";
                    strSql += "	FROM T_OPERAC INNER JOIN T_MAQUINAS ON T_OPERAC.PUESTO = T_MAQUINAS.PUESTO ";
                    strSql += "	WHERE (T_OPERAC.CodEMP = '" + CodEmp + "') ";
                    strSql += "		and (T_OPERAC.PUESTO not in('000', '800', '900', '970', '971', '995', '1010', '1011', '701', '901', '980')) AND PROVEEDOR LIKE (select case when ('" + Lugar + "' LIKE 'CC') OR ('" + Lugar + "' LIKE 'SC') THEN 'IMEDEXSA' ELSE 'MADE' END) ";
                    strSql += "	GROUP BY T_OPERAC.CodEmp, OPERAC   ) ";
                    strSql += "	as t_aux ";
                    strSql += "Order By t_aux.PUESTO";


                    System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(strSql, conn);

                    System.Data.SqlClient.SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        listaMaquina.Add(reader.GetValue(0).ToString() + "-->" + reader.GetValue(1).ToString());
                    }

                    reader.Close();

                    comando.Dispose();


                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Iniciar la aplicacion. Avise a su administrador: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                Close();    //cierro la app xa q no pueda haber errores
            }

            this.WindowState = FormWindowState.Maximized;

        }



        private void obtenerValoresFicheroConfiguracion()
        {

            DataSet set = new DataSet();

            try
            {
                /*String aux = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                aux = aux + "\\Config.xml";
                set.ReadXml(aux);

                this.CodEmp = set.Tables["config"].Rows[0]["CodEmp"].ToString();
                this.CadenaConexion = set.Tables["config"].Rows[0]["ConStrSQL"].ToString();

                //this.AnioInventario = Convert.ToInt32(set.Tables["config"].Rows[0]["Anio"]);
                //this.MesInventario = Convert.ToInt32(set.Tables["config"].Rows[0]["Mes"]);
                 * 
                 * */

                this.CodEmp = empresaGlobalProj.empresaGlobal.empresaID;
                this.CadenaConexion = Utils.CD.getConexion();
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(this.CadenaConexion);

                
                this.MesInventario = 12;// DateTime.Now.Month;



                this.PDA = System.Net.Dns.GetHostName();    //set.Tables["config"].Rows[0]["PDA"].ToString();


                string strSql = "SELECT * FROM CONFIG_DISP_INVENTARIO WHERE DISPOSITIVO = '" + PDA + "'";

                conn.Open();
                SqlCommand comando = new SqlCommand(strSql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    AnioInventario = Convert.ToInt32(table.Rows[0][2].ToString());
                    CodEmp = table.Rows[0][0].ToString();

                    strSql = "UPDATE CONFIG_DISP_INVENTARIO SET ULTIMA_SESION = GETDATE() WHERE CODEMP = '" + CodEmp + "' AND DISPOSITIVO = '" + PDA + "'";
                }
                else
                {
                    this.AnioInventario = 2024; //DateTime.Now.Year;
                    strSql = "INSERT INTO CONFIG_DISP_INVENTARIO (CODEMP, DISPOSITIVO, ANIO, ULTIMA_SESION) VALUES ('" + CodEmp + "', '" + PDA + "', " + AnioInventario + ", GETDATE())";
                }

                comando = new SqlCommand(strSql, conn);
                comando.ExecuteNonQuery();

                
                if (CodEmp == "3")
                {
                    labelEmp.Text = "IMEDEXSA";
                }
                else
                {
                    labelEmp.Text = "MADE";
                }

                this.label5.Text = this.AnioInventario.ToString();

                this.MatMP = "MP";
                this.MatTO = "TO";
                this.MatNegro = "Negro";
                this.MatCurso = "Curso";
                this.MatBlanco = "Blanco";

                this.Lugar = String.Empty;

                set = null;
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (set != null)
                    set = null;
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Lugar = comboBox2.Text;

            listaMaquina = new ArrayList();
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(this.CadenaConexion); ;

            try
            {
                conn.Open();

                String strSql = " ";
                strSql = " Select ";
                strSql += "t_aux.PUESTO as Maqui, ";
                strSql += "(Select ABREV From T_OPERAC Where CodEMP = t_aux.CodEMP and OPERAC = t_aux.OPERAC and PUESTO = t_aux.PUESTO)  ";
                strSql += "From ";
                strSql += "	( SELECT ";
                strSql += "		T_OPERAC.CodEmp, ";
                strSql += "		min(T_OPERAC.PUESTO) as PUESTO, ";
                strSql += "		OPERAC ";
                strSql += "	FROM T_OPERAC INNER JOIN T_MAQUINAS ON T_OPERAC.PUESTO = T_MAQUINAS.PUESTO ";
                strSql += "	WHERE (T_OPERAC.CodEMP = '" + CodEmp + "') ";
                strSql += "		and (T_OPERAC.PUESTO not in('000', '800', '900', '970', '971', '995', '1010', '1011', '701', '901', '980')) AND PROVEEDOR LIKE (select case when ('" + Lugar + "' LIKE 'CC') OR ('" + Lugar + "' LIKE 'SC') THEN 'IMEDEXSA' ELSE 'MADE' END) ";
                strSql += "	GROUP BY T_OPERAC.CodEmp, OPERAC   ) ";
                strSql += "	as t_aux ";
                strSql += "Order By t_aux.PUESTO";


                System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand(strSql, conn);

                System.Data.SqlClient.SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    listaMaquina.Add(reader.GetValue(0).ToString() + "-->" + reader.GetValue(1).ToString());
                }

                reader.Close();

                comando.Dispose();


                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }


        //Capturar pulsacion teclas: F5(Lotes), F6(Tornilleria), F7(Curso), F8(Negro), F9(Blanco Empaq), F10(Blanco Packing)
        /*void OnHotKeyPress(int idHotKey, int fuModifiers, int uVirtKey)
        {

            switch ((Keys)uVirtKey)
            {
                case Keys.F5:
                    button1_Click(this, null);
                    break;
                case Keys.F6:
                    button5_Click(this, null);
                    break;
                case Keys.F7:
                    button2_Click(this, null);
                    break;
                case Keys.F8:
                    button3_Click(this, null);
                    break;
                case Keys.F9:
                    button4_Click(this, null);
                    break;
                case Keys.F10:
                    button6_Click(this, null);
                    break;

                default:
                    break;
            }
        }*/



        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Lugar == String.Empty)
            {
                MessageBox.Show("Debe indicar el Lugar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Lotes lotes = new Lotes(this.CodEmp, this.AnioInventario, this.MesInventario, this.CadenaConexion, this.PDA, /*this.TacoMin, this.TacoMax,*/ this.MatMP, this.Lugar);
                lotes.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Lugar == String.Empty)
            {
                MessageBox.Show("Debe indicar el Lugar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Curso curso = new Curso(this.CodEmp, this.AnioInventario, this.CadenaConexion, this.PDA, /*this.TacoMin, this.TacoMax,*/ this.MatCurso, this.listaMaquina, this.Lugar);
                curso.ShowDialog();
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.Lugar == String.Empty)
            {
                MessageBox.Show("Debe indicar el Lugar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Paquetes paquetes = new Paquetes(this.CodEmp, this.AnioInventario, this.CadenaConexion, this.PDA, /*this.TacoMin, this.TacoMax,*/ this.MatNegro, this.Lugar);
                paquetes.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.Lugar == String.Empty)
            {
                MessageBox.Show("Debe indicar el Lugar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Galvanizado galvanizados = new Galvanizado(this.CodEmp, this.AnioInventario, this.CadenaConexion, this.PDA, /*this.TacoMin, this.TacoMax,*/ this.MatBlanco, this.Lugar);
                galvanizados.ShowDialog();
            }
        }

        //JRegino 16/12/2015. Añadida la opción de añadir la Tornilleria a través de su Código de Barras.
        private void button5_Click(object sender, EventArgs e)
        {
            if (this.Lugar == String.Empty)
            {
                MessageBox.Show("Debe indicar el Lugar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Tornilleria tornilleria = new Tornilleria(this.CodEmp, this.AnioInventario, this.CadenaConexion, this.PDA, /*this.TacoMin, this.TacoMax,*/ this.MatTO, this.Lugar);
                tornilleria.ShowDialog();
            }
        }

        //JRegino 20/12/2016. Añadida la opción de añadir los paquetes del packing
        private void button6_Click(object sender, EventArgs e)
        {
            if (this.Lugar == String.Empty)
            {
                MessageBox.Show("Debe indicar el Lugar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Packing tornilleria = new Packing(this.CodEmp, this.AnioInventario, this.CadenaConexion, this.PDA, /*this.TacoMin, this.TacoMax,*/ this.MatBlanco, this.Lugar);
                tornilleria.ShowDialog();
            }
        }

        //JRegino 29/12/2017. Añadida la opción de añadir Forros en blanco a través de lectura de códigos de barras (como los TO)
        private void button7_Click(object sender, EventArgs e)
        {
            if (this.Lugar == String.Empty)
            {
                MessageBox.Show("Debe indicar el Lugar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Forros frmforros = new Forros(this.CodEmp, this.AnioInventario, this.CadenaConexion, this.PDA, /*this.TacoMin, this.TacoMax,*/ this.MatBlanco, this.Lugar);
                frmforros.ShowDialog();
            }
        }

        private string showConnectedId()
        {
            string s = "-";
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "netsh.exe";
                p.StartInfo.Arguments = "wlan show interfaces";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();

                s = p.StandardOutput.ReadToEnd();
                string s1 = s.Substring(s.IndexOf("SSID"));
                s1 = s1.Substring(s1.IndexOf(":"));
                s1 = s1.Substring(2, s1.IndexOf("\n")).Trim();

                string s2 = s.Substring(s.IndexOf("Signal"));
                s2 = s2.Substring(s2.IndexOf(":"));
                s2 = s2.Substring(2, s2.IndexOf("\n")).Trim();

                p.WaitForExit();

                return s1;
            }
            catch (Exception e)
            {

            }

            return s;
        }

    }
}
