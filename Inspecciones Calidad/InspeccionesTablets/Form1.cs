using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using empresaGlobalProj;

namespace InspeccionesTablets
{
    public partial class Form1 : Form
    {

        #region atributos de la clase
        private String CodEmp;
        private String CadenaConexion;
        private String PDA;
        //private List<String> listaMaquina;

        private String CodOperario;
        private String Operario;
        //private String CodMaquina;
        //private String Maquina;

        string  cantidad = "";


        //todas las máquinas operaciones
        private DataTable dtOriginal;

        private String URLPlano;
        #endregion

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
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                this.Text += version;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            #region Leer fichero de configuracion
            try
            {
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID;
                this.CodEmp = empresaGlobal.empresaID;//ConfigurationManager.AppSettings["EMPRESA"].ToString();
                this.CadenaConexion = ConfigurationManager.AppSettings["ggConnectionString"].ToString();
                this.PDA = System.Net.Dns.GetHostName();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al inicializar la aplicacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            
            
            SqlConnection conn = new SqlConnection();

            try
            {
                String strSql = " ";
                SqlCommand sqlCmd;
                SqlDataReader sqlRdr;

                conn.ConnectionString = this.CadenaConexion;
                conn.Open();


                #region Obtener los operarios de calidad
                strSql = "SELECT TC_V_OperariosCalidad.NUMERO_PERSONAL, TC_V_OperariosCalidad.NOMBRE + ' '+  TC_V_OperariosCalidad.APELLIDOS ";
                strSql = strSql + " FROM TC_V_OperariosCalidad ";
                strSql = strSql + " ORDER BY TC_V_OperariosCalidad.NUMERO_PERSONAL";

                sqlCmd = new SqlCommand(strSql, conn);
                sqlRdr = sqlCmd.ExecuteReader();

                while (sqlRdr.Read())
                {
                    comboBox2.Items.Add(sqlRdr.GetValue(0).ToString() + " # " + sqlRdr.GetValue(1).ToString());
                }

                sqlRdr.Close();
                sqlRdr.Dispose();
                #endregion



                #region Obtener las maquinas xa rellenar el combo del form del curso, asi solo se realiza la consulta una vez                
                this.dtOriginal = new DataTable();
                this.dtOriginal.Columns.Add("Máquina");
                this.dtOriginal.Columns.Add("Operación");

                //JRM - Cambiar Empresa cambio '3' por empresaGlobal.empresaID;
                strSql = "SELECT T_MAQUINAS.MAQUI, T_MAQUINAS.ABREV, (SELECT top (1) OPERAC FROM T_OPERAC WHERE (CodEMP = '" + empresaGlobal.empresaID + "') AND (PUESTO = T_MAQUINAS.MAQUI)) AS OPERAC";
                strSql += " FROM T_MAQUINAS";
                strSql += " WHERE (CodEMP = '" + this.CodEmp + "')";
                strSql += "	ORDER BY T_MAQUINAS.MAQUI";

                sqlCmd = new SqlCommand(strSql, conn);
                sqlRdr = sqlCmd.ExecuteReader();

                while (sqlRdr.Read())
                {                    
                    this.dtOriginal.Rows.Add(sqlRdr.GetValue(0).ToString() + " - " + sqlRdr.GetValue(1).ToString(), sqlRdr.GetValue(2).ToString());                    
                }

                sqlRdr.Close();
                sqlCmd.Dispose();

                enlazarCombo(this.dtOriginal);
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Iniciar la aplicacion. Avise a su administrador: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Close();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }




        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = "Inspecciones de Calidad. Operario: " + this.comboBox2.Text;

            string[] aux = this.comboBox2.Text.Split('#');
            this.CodOperario = aux[0].Trim();
            this.Operario = aux[1].Trim();
        }





        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.Text != String.Empty)
            {
                String marca = this.existeEtiqueta(textBox1.Text);

                if (marca == String.Empty)
                {
                    MessageBox.Show("La etiqueta no existe o no tiene marca asociada, hable con el Dpto de Produccion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.textBox1.Text = String.Empty;
                    this.textBox1.Focus();

                    this.textBoxMaterialEspecial.Text = "";
                }
                else
                {
                    this.textBox2.Text = marca;

                    DataTable dtCopia = new DataTable();
                    dtCopia.Columns.Add("Máquina");
                    dtCopia.Columns.Add("Operación");

                    //Obtener las máquinas que tiene esa OF
                    this.obtenerOperaciones(this.textBox1.Text, ref dtCopia);

                    enlazarCombo(dtCopia);

                    this.comboBox1.Focus();

                    this.textBoxMaterialEspecial.Text = GetMaterialEspecialBono(this.CodEmp, this.textBox1.Text);
                }
            }
        }

                
        
        public string GetMaterialEspecialBono(string codemp, string etiqueta)
        {
            String strSql;
            SqlCommand sqlCmd;
            SqlDataReader sqlRdr;
            SqlConnection conn = new SqlConnection();
            string material_especial;            

            try
            {
                strSql = "";
                material_especial = "";
               
                conn.ConnectionString = this.CadenaConexion;
                conn.Open();                

                strSql += " select   mensaje_bonos";
                strSql += " from     tc_pedidos_material_especial";
                strSql += " where    codemp = '" + codemp + "' and";
		        strSql += "          PEDIDO in (";
			    strSql += "                 select  NUMPED";
			    strSql += "                 from    TC_LANZA_PEDLIN";
			    strSql += "                 where	CODEMP = '" + codemp + "' and CODLANZA = (select PROYECTO from T_ORDFAB where CODEMP = '" + codemp + "' and ORDFAB = (select OrdFab from TC_LANZA_ETIQUETA where CodEmp = '" + codemp + "' and Etiqueta = " + etiqueta + ")))";

                sqlCmd = new SqlCommand(strSql,conn);
                sqlRdr = sqlCmd.ExecuteReader();

                while (sqlRdr.Read())
                {
                    material_especial = sqlRdr[0].ToString();
                }

                sqlRdr.Close();
                sqlCmd.Dispose();

                return material_especial;
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
         


        private void button2_Click(object sender, EventArgs e)
        {
            String path = ConfigurationManager.AppSettings["url_DestinoPlanos"].ToString().ToUpper();

            WebService_Planos.ServiceSoapClient ServicioWeb = new WebService_Planos.ServiceSoapClient();
            String Resultado = ServicioWeb.ObtenerFile2(this.textBox2.Text, this.CodEmp, this.PDA);


            if (Resultado.StartsWith("-1"))
                MessageBox.Show("Error al obtener el plano del código: " + Environment.NewLine + Resultado);
            else
            {
                System.Diagnostics.Process.Start(path + "\\" + this.PDA + "\\" + Resultado);
            }

            ServicioWeb.Close();            
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox8.Visible = false;

            if (this.comboBox1.SelectedIndex != -1)
            {
                if (this.comboBox1.SelectedValue.ToString() == "950001")
                {
                    groupBox1.Visible = true;
                    groupBox8.Visible = true;
                }
            }
        }



        //Asignar datos al combo de las máquinas
        private void enlazarCombo(DataTable dt)
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Máquina";
            comboBox1.ValueMember = "Operación";
            comboBox1.SelectedIndex = -1;
        }


        //Devuelve el nombre de la marca asociada a la etiqueta
        private String existeEtiqueta(String etiq)
        {
            String marca = "";
            SqlConnection conn = new SqlConnection();

            try
            {
                String strSql = " ";
                SqlCommand sqlCmd;

                conn.ConnectionString = this.CadenaConexion;
                conn.Open();

                strSql = "SELECT T_ORDFAB.CODIGO ";
                strSql += "         FROM TC_LANZA_ETIQUETA INNER JOIN T_ORDFAB ON TC_LANZA_ETIQUETA.OrdFab = T_ORDFAB.ORDFAB AND TC_LANZA_ETIQUETA.TIPOREG = T_ORDFAB.TIPOREG AND TC_LANZA_ETIQUETA.CodEmp = T_ORDFAB.CODEMP ";
                strSql += "         WHERE (TC_LANZA_ETIQUETA.CodEmp = '" + this.CodEmp + "') AND (TC_LANZA_ETIQUETA.Etiqueta = " + etiq + ")";

                sqlCmd = new SqlCommand(strSql, conn);
                marca = Convert.ToString(sqlCmd.ExecuteScalar());

                sqlCmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al comprobar la existencia de la Etiqueta: " + ex.Message, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

            return marca;
        }


        //Obtener las distintas operaciones que tiene la OrdFab de esa Etiqueta
        private void obtenerOperaciones(String etiqueta, ref DataTable dtCopia)
        {
            SqlConnection conn = new SqlConnection();

            try
            {
                String strSql = " ";
                SqlCommand sqlCmd;
                SqlDataReader sqlRdr;

                conn.ConnectionString = this.CadenaConexion;
                conn.Open();

                strSql = "SELECT distinct T_ORDFABO.OPERAC, T_ORDFABO.CORREL ";
                strSql += "    FROM TC_Lanza_Etiqueta INNER JOIN T_ORDFABO ";
                strSql += "         ON TC_LANZA_ETIQUETA.CodEmp = T_ORDFABO.CODEMP AND TC_LANZA_ETIQUETA.TIPOREG = T_ORDFABO.TIPOREG AND TC_LANZA_ETIQUETA.OrdFab = T_ORDFABO.ORDFAB ";
                strSql += "    WHERE TC_LANZA_ETIQUETA.Codemp = '" + this.CodEmp + "'";
                strSql += "         and TC_LANZA_ETIQUETA.TipoReg = 'F'";
                strSql += "         and TC_LANZA_ETIQUETA.Etiqueta = " + etiqueta.ToString() + "";
                strSql += "    	ORDER BY T_ORDFABO.CORREL ";

                sqlCmd = new SqlCommand(strSql, conn);
                sqlRdr = sqlCmd.ExecuteReader();

                while (sqlRdr.Read())
                {                    
                    for (int y = 0; y < this.dtOriginal.Rows.Count; y++)
                    {
                        if (sqlRdr.GetValue(0).ToString() == this.dtOriginal.Rows[y][1].ToString())
                            dtCopia.Rows.Add(this.dtOriginal.Rows[y][0], this.dtOriginal.Rows[y][1]);
                    }
                }

                sqlRdr.Close();
                sqlCmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las operaciones de la etiqueta. Vuelva a intentarlo " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
                this.textBox4.Enabled = false;
            else
                this.textBox4.Enabled = true;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
                this.textBox4.Enabled = true;
            else
                this.textBox4.Enabled = false;
        }



        //Salvar
        private void button1_Click(object sender, EventArgs e)
        {
            cantidad =  txt_END_ctd.Text; //Carlos

            #region comprobar q se han introducido todos los datos
            if ((textBox1.Text == String.Empty) || (comboBox1.SelectedIndex == -1) || (radioButton1.Checked == false && radioButton2.Checked == false) || (radioButton5.Checked == false && radioButton6.Checked == false))
            {
                MessageBox.Show("Debe rellenar todos los datos ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }


            if (this.comboBox1.SelectedValue.ToString() == "950001")
            {
                if ((radioButton7.Checked == false && radioButton8.Checked == false) || (radioButton9.Checked == false && radioButton10.Checked == false) || (radioButton11.Checked == false && radioButton12.Checked == false) || (radioButton13.Checked == false && radioButton14.Checked == false))
                {
                    if (MessageBox.Show("No ha indicado todos los elementos a inspeccionar de la soldadura. ¿Desea guardar de todos modos?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }

                //Si alguno es falso => el resultado debe ser falso
                if (radioButton8.Checked == true || radioButton10.Checked == true || radioButton12.Checked == true || radioButton14.Checked == true)
                {
                    if (radioButton5.Checked == true)
                    {                        
                        MessageBox.Show("El resultado de la inspección no puede ser correcta si uno de los elementos de la soldadura no lo es.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }              
            }
            #endregion


            SqlConnection conn = new SqlConnection();

            try
            {
                String strSql = " ";
                SqlCommand sqlCmd;                

                conn.ConnectionString = this.CadenaConexion;
                conn.Open();
                

                #region realizar la insercion

                string[] aux = comboBox1.Text.Split('-');
                String CodMaquina = aux[0].Trim();
                String Maquina = aux[1].Trim();


                strSql = "INSERT INTO TC_InspeccionEtiquetas (CodEmp, Fecha, CodOperario, Operario, Etiqueta, CodMaquina, Maquina, Sold_PB, Sold_CD, Sold_IV, Sold_LF, Sold_END_UT, Sold_END_MP, Sold_END_LP, Sold_END_VT, Sold_END_ctd, VistoBuenoAutoControl, Correcto, NoConformidad, fechacre, usucre) ";
                strSql += "SELECT '" + this.CodEmp + "', getdate(), " + this.CodOperario + ", '" + this.Operario + "', " + this.textBox1.Text + ", '" + CodMaquina + "', '" + Maquina + "', ";

                //Elementos CJS
                if (this.comboBox1.SelectedValue.ToString() == "950001")
                {
                    //Sold_PB
                    if (radioButton7.Checked == false && radioButton8.Checked == false)
                        strSql += " null, ";
                    else
                        strSql += Convert.ToInt32(radioButton7.Checked).ToString() + ", ";

                    //Sold_CD
                    if (radioButton9.Checked == false && radioButton10.Checked == false)
                        strSql += " null, ";
                    else
                        strSql += Convert.ToInt32(radioButton9.Checked).ToString() + ", ";

                    //Sold_IV
                    if (radioButton11.Checked == false && radioButton12.Checked == false)
                        strSql += " null, ";
                    else
                        strSql += Convert.ToInt32(radioButton11.Checked).ToString() + ", ";

                    //Sold_LF
                    if (radioButton13.Checked == false && radioButton14.Checked == false)
                        strSql += " null, ";
                    else
                        strSql += Convert.ToInt32(radioButton13.Checked).ToString() + ", ";
                }
                else
                    strSql += " null, null, null, null, ";

                //Ensayos CJS
                if (this.comboBox1.SelectedValue.ToString() == "950001")
                {
                    //Sold_END_UT
                    if (radioButton15.Checked == false && radioButton16.Checked == false)
                        strSql += " null, ";
                    else
                        strSql += Convert.ToInt32(radioButton15.Checked).ToString() + ", ";

                    //Sold_END_MP
                    if (radioButton17.Checked == false && radioButton18.Checked == false)
                        strSql += " null, ";
                    else
                        strSql += Convert.ToInt32(radioButton17.Checked).ToString() + ", ";

                    //Sold_END_LP
                    if (radioButton19.Checked == false && radioButton20.Checked == false)
                        strSql += " null, ";
                    else
                        strSql += Convert.ToInt32(radioButton19.Checked).ToString() + ", ";

                    //Sold_END_VT
                    if (radioButton21.Checked == false && radioButton22.Checked == false)
                        strSql += " null, ";
                    else
                        strSql += Convert.ToInt32(radioButton21.Checked).ToString() + ", ";

                    //Sold_END_ctd
                    if (radioButton15.Checked == false && radioButton16.Checked == false && radioButton17.Checked == false && radioButton18.Checked == false && radioButton19.Checked == false && radioButton20.Checked == false && radioButton21.Checked == false && radioButton22.Checked == false)
                        strSql += " null, ";
                    else
                    {
                        Int32 ctd;
                        if (Int32.TryParse(txt_END_ctd.Text, out ctd) == true)
                            strSql += ctd.ToString() + ", ";
                        else
                        {
                            MessageBox.Show("La cantidad indicada NO es un numero válido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                    strSql += " null, null, null, null, null, ";



                //VistoBuenoAutoControl
                strSql += Convert.ToInt32(radioButton1.Checked).ToString() + ", ";

                //Resultado
                strSql += Convert.ToInt32(radioButton5.Checked).ToString() + ", ";
                

                //NoConformidad
                if (radioButton5.Checked == true)
                    strSql += " null, ";
                else
                    strSql += "'" + this.textBox4.Text + "', ";

                strSql += "getdate(), '" + this.PDA + "'";



                sqlCmd = new SqlCommand(strSql, conn);
                sqlCmd.ExecuteNonQuery();

                sqlCmd.Dispose();

                //Carlos sanchez, actulizado los datos tb en gestion de soldadura.


                if (string.IsNullOrEmpty(textBox4.Text))
                {
                    actulizarGestionSoladura(this.CodEmp, this.textBox1.Text);
                }
                #endregion



                //reinicializar los valores del interfaz
                this.enlazarCombo(this.dtOriginal);

                this.radioButton1.Checked = false;
                this.radioButton2.Checked = false;
                this.radioButton5.Checked = false;
                this.radioButton6.Checked = false;

                this.radioButton7.Checked = false;
                this.radioButton8.Checked = false;
                this.radioButton9.Checked = false;
                this.radioButton10.Checked = false;
                this.radioButton11.Checked = false;
                this.radioButton12.Checked = false;
                this.radioButton13.Checked = false;
                this.radioButton14.Checked = false;
                this.radioButton15.Checked = false;
                this.radioButton16.Checked = false;
                this.radioButton17.Checked = false;
                this.radioButton18.Checked = false;
                this.radioButton19.Checked = false;
                this.radioButton20.Checked = false;
                this.radioButton21.Checked = false;
                this.radioButton22.Checked = false;

                this.textBox1.Text = String.Empty;
                this.textBox2.Text = String.Empty;
                this.textBox4.Text = String.Empty;
                this.txt_END_ctd.Text = String.Empty;
                this.textBox4.Enabled = false;

                this.textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Guardar los Datos: : " + ex.Message, "Error Guardando datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            
        }


        //Ver inspecciones almacenadas
        private void button3_Click(object sender, EventArgs e)
        {
            Form5 inspeciones = new Form5();
            inspeciones.ShowDialog();
        }



        private void buttonProyectos_Click(object sender, EventArgs e)
        {
            Form2 proyectos = new Form2(this.CodEmp,this.CadenaConexion,this.PDA);
            Cursor.Current = Cursors.WaitCursor;
            proyectos.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=srvsql02;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");

            con.Open();

            string  sql = " select  CONVERT( DATE,TC_LANZA_ETIQUETA.FECHAC,103)  as 'FECHA', ";
                        sql +="TC_LANZA_ETIQUETA.Etiqueta,";
                        sql +="T_ORDFAB.PROYECTO, ";
                        sql +="TC_LANZA_ETIQUETA.OrdFab, ";
                        sql +="stuff((SELECT distinct ', ' + cast(NUMPED as varchar(10)) ";
                        sql +="                                               FROM TC_LANZA_PEDLIN ";
                        sql +="                                               WHERe (CODEMP = '3') AND (TIPOREG = 'CF') and (CODLANZA = T_ORDFAB.PROYECTO) ";
                        sql +="                                               FOR XML PATH('') ";                                                                 
                        sql +="                           ), 1, 1, '') as 'PEDIDO', ";
                        sql +="isnull(TC_InspeccionEtiquetas.id,0) as 'INPECCIONADO', ";
                        sql +="T_ORDFAB.CODIGO as 'MARCA', ";
                        sql +="isnull(Maquina,'') AS 'MAQUINA', ";
                        sql +="TC_InspeccionEtiquetas.VistoBuenoAutoControl, ";
                        sql +="isnull(TC_InspeccionEtiquetas.CodOperario,'') AS 'Vº Bº Auto Control', ";
                        sql +="isnull(Operario,'') as 'Vº Bº Calidad', ";
                        sql +="isnull(NoConformidad,'') as 'NO CONFORMIDAD', ";
                        sql +="isnull(Correcto,'') as 'CORRECTO', ";
                        sql +="dbo.etiqueta_Fabricada(TC_LANZA_ETIQUETA.Etiqueta, TC_LANZA_ETIQUETA.CodEmp) as 'FABRICADO', ";
                        sql +="dbo.etiqueta_Expedida(TC_LANZA_ETIQUETA.Etiqueta, TC_LANZA_ETIQUETA.CodEmp) as 'EXPEDIDO'  ";
                        sql +="from TC_LANZA_ETIQUETA  left join ";
                        sql +="TC_InspeccionEtiquetas on TC_LANZA_ETIQUETA.Etiqueta = TC_InspeccionEtiquetas.Etiqueta ";
                        sql +="join T_ORDFAB on TC_LANZA_ETIQUETA.OrdFab = T_ORDFAB.ORDFAB ";
                        sql +="where (TC_LANZA_ETIQUETA.FECHAC < CONVERT(DATETIME,getdate(), 103))  ";
                        sql +="and (TC_LANZA_ETIQUETA.FECHAC >= CONVERT(DATETIME,'18/03/2024', 103))   ";
                        sql +="AND (TC_LANZA_ETIQUETA.CodEmp ='3')";


            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.CommandTimeout = 1600000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            var excelApplication = new Microsoft.Office.Interop.Excel.Application();
            var excelWorkBook = excelApplication.Application.Workbooks.Add(Type.Missing);
            DataColumnCollection dataColumnCollection = dt.Columns;
            for (int i = 1; i <= dt.Rows.Count + 1; i++)
            {
                for (int j = 1; j <= dt.Columns.Count; j++)
                {
                    if (i == 1)
                        excelApplication.Cells[i, j] = dataColumnCollection[j - 1].ToString();
                    else
                        excelApplication.Cells[i, j] = dt.Rows[i - 2][j - 1].ToString();
                }
            }
            excelApplication.ActiveWorkbook.SaveCopyAs("C:\\informe_calidad_etiquetas_inspecionadas.xlsx");
            excelApplication.ActiveWorkbook.Saved = true;
            excelApplication.Quit();

            MessageBox.Show("INFORME GENERADO CON EXITO en la ruta C:\\informe_calidad_etiquetas_inspecionadas.xlsx");

            con.Close();
        }


        public void actulizarGestionSoladura(string codemp, string etq)
        {
            //Primero comprueba si la etiqueta existe ya
            SqlConnection con = new SqlConnection("Data Source=srvsql02;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");

            con.Open();

            string sql = " select COUNT(*) from TC_Soldadura where Etiqueta ='" + etq+"' ";


            SqlDataAdapter adapter;
            DataTable table = new DataTable();
            SqlCommand comando = null;

            comando = new SqlCommand(sql, con);


            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + sql);

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = sql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            con.Close();


            foreach (DataRow row in table.Rows)
            {
                // Compruebo si existe la etiqueta
                bool existe = Convert.ToBoolean(row[0]);

                if (existe)
                {
                    //Añado la info a la etiqueta.

                    actulizarEtiqueta(etq);

                }
                else
                {
                    //creo el proyecyo y actulizo la etiqueta
                    crearProyecto(etq);
                    actulizarEtiqueta(etq);
                }
            }

        }

        public void actulizarEtiqueta(string etq)
        {
            //Obtengo los datos de la etquieta fichada

            SqlConnection con = new SqlConnection("Data Source=srvsql02;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");

            con.Open();

            string sql = " select top(1) * from TC_InspeccionEtiquetas where Etiqueta ='" + etq + "' ORDER BY fechacre DESC";

            SqlDataAdapter adapter;
            DataTable table = new DataTable();
            SqlCommand comando = null;

            comando = new SqlCommand(sql, con);

            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + sql);

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = sql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            con.Close();


            foreach (DataRow row in table.Rows)
            {
                string end_pm = string.IsNullOrEmpty(Convert.ToString(row["Sold_END_MP"])) ? "0" : Convert.ToString(row["Sold_END_MP"]); ; //MT
                string end_ut = string.IsNullOrEmpty(Convert.ToString(row["Sold_END_UT"])) ? "0" : Convert.ToString(row["Sold_END_UT"]); ; //UT
                string end_vt = string.IsNullOrEmpty(Convert.ToString(row["Sold_END_VT"])) ? "0" : Convert.ToString(row["Sold_END_VT"]); ; //VT
                string cd = string.IsNullOrEmpty(Convert.ToString(row["Sold_CD"])) ? "0" : Convert.ToString(row["Sold_CD"]); ; //DM
                string cant = string.IsNullOrEmpty(Convert.ToString(row["Sold_END_ctd"])) ? cantidad : Convert.ToString(row["Sold_END_ctd"]); ; //ctd


                actulizartabla(end_pm, end_ut, end_vt, cd, cant, etq);
            }
        }

        public void actulizartabla(string mt, string ut, string vt, string dm, string cantidad, string etq)
        {
            //Teniendo los valores actuales y le sumo a las tablas de gestion de soldadura.

            if (mt == "True")
            {
                string sqlmt = "update TC_Soldadura  ";
                   sqlmt += " set MT = (SELECT MT FROM TC_Soldadura WHERE Etiqueta="+etq+")+"+cantidad+" ";
                   sqlmt += " WHERE Etiqueta = " + etq + " ";

                   ejecturarSQL(sqlmt);
            }

            if (ut == "True")
            {
                string sqlmt = "update TC_Soldadura  ";
                sqlmt += " set UT = (SELECT UT FROM TC_Soldadura WHERE Etiqueta=" + etq + ")+" + cantidad + " ";
                sqlmt += " WHERE Etiqueta = " + etq + " ";

                ejecturarSQL(sqlmt);
            }

            if (vt == "True")
            {
                string sqlmt = "update TC_Soldadura  ";
                sqlmt += " set VT = (SELECT VT FROM TC_Soldadura WHERE Etiqueta=" + etq + ")+" + cantidad + " ";
                sqlmt += " WHERE Etiqueta = " + etq + " ";

                ejecturarSQL(sqlmt);
            }

            if (dm == "True")
            {
                string sqlmt = "update TC_Soldadura  ";
                sqlmt += " set DM = (SELECT DM FROM TC_Soldadura WHERE Etiqueta=" + etq + ")+" + cantidad + " ";
                sqlmt += " WHERE Etiqueta = " + etq + " ";

                ejecturarSQL(sqlmt);
            }

        }

        public void crearProyecto(string etq)
        {


            //Creo el proyecto y la etiqueta con los datos
            string proyecto = "", cliente = "";
              //1) obtengo el proyecto para la etiqueta correspondiente
            DataTable tableProyecto = new DataTable();

            string sqlProyecto = "select O.PROYECTO from T_ORDFAB O FULL OUTER JOIN TC_LANZA_ETIQUETA E ON O.ORDFAB = E.OrdFab ";
            sqlProyecto += " WHERE (O.CODEMP = '3') AND (O.TIPOREG = 'F') AND E.Etiqueta = '" + etq + "' ";

            tableProyecto=ejecturarSQL(sqlProyecto);
            foreach (DataRow row in tableProyecto.Rows)
            {
                 proyecto = Convert.ToString(row[0]);

            }


            //2) Obtengo el cliente del proyecto
            DataTable tableCliente = new DataTable();

            string sqlCliente = " SELECT DISTINCT TC_LANZA_PEDLIN.NUMPED, T_ORDTER.RTERCERO  FROM  TC_LANZA_PEDLIN INNER JOIN T_ORDTER ";
            sqlCliente = sqlCliente + "ON TC_LANZA_PEDLIN.NUMPED = T_ORDTER.NUMPED  WHERE ";
            sqlCliente = sqlCliente + "TC_LANZA_PEDLIN.CODEMP ='" + CodEmp + "'  AND TC_LANZA_PEDLIN.CODLANZA = " + proyecto + "   ORDER BY TC_LANZA_PEDLIN.NUMPED;";


            tableCliente = ejecturarSQL(sqlCliente);
            foreach (DataRow row in tableCliente.Rows)
            {
                cliente = Convert.ToString(row[0]);

            }

            // 3) Creo el proyecto llamando al procedimiento alamcenado.
            string strsql1 = "EXEC gestionSoldadura '" + CodEmp + "','" + proyecto + "','" + Environment.UserName + "','2','" + cliente + "'";

            DataTable tablefin = new DataTable();
            tablefin = ejecturarSQL(strsql1);

        }


        public DataTable ejecturarSQL(string sql)
        {
            SqlConnection con = new SqlConnection("Data Source=srvsql02;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");

            con.Open();

           


            SqlDataAdapter adapter;
            DataTable table = new DataTable();
            SqlCommand comando = null;

            comando = new SqlCommand(sql, con);


            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + sql);

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = sql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            con.Close();

            return table;
        }
    }
}
