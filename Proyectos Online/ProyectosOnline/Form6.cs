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
    public partial class Form6 : Form
    {
        public static String bono;
        public static String proyecto;
        public static String eliminado;

        public Form6()
        {
            InitializeComponent();
        }


        private void Form6_Load(object sender, EventArgs e)
        {
            //pongo la version del programa
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed == true)
                this.Text += System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            else
                this.Text += Application.ProductVersion.ToString();

            proyecto = Form1.proyecto;
            bono = Form1.bono;
            textBox5.Text = bono;

            cargarProyectos();
            cargarOperacion();

            // si el proyecto y el bono vienen vacíos, deshabilitamos todos los campos
            if ((proyecto == "") && (bono == ""))
                deshabilitarCampos();

            listaPersonas();
            String usuario = Environment.UserName.ToUpper();
            String usuarios_permitidos = ConfigurationManager.AppSettings["Users_fichar_piezas"].ToString().ToUpper();
            if (usuarios_permitidos.Contains(usuario))
                btnCerrarBono.Visible = true;
        }

        
        private void cargarPuestos()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // obtenemos el puesto que tiene asignado el bono
                String strSql = "SELECT PUESTO +'   '+ DENOMI as puesto ";
                strSql += "FROM T_PUESTOS ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID - 2 veces
                strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' and PUESTO= (SELECT PUESTO FROM TC_BONO WHERE CODEMP = '" + empresaGlobal.empresaID + "' And BONO='" + bono + "') ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                if (sqlDR.Read())
                {
                    this.textBox2.Text= sqlDR[0].ToString();
                }
                sqlDR.Close();

                // obtenemos la listas de máquinas compatibles con la que tenemos en nuestro bono
                strSql = "SELECT a.PUESTO +'   '+ b.DENOMI AS DENOMIC ";
                strSql += "FROM T_OPERAC a ";
                strSql += "INNER JOIN T_PUESTOS b on a.PUESTO = b.PUESTO AND a.CodEmp = b.CodEmp ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID - 2 veces
                strSql += "WHERE a.CodEmp='" + empresaGlobal.empresaID + "' and a.OPERAC= (SELECT OPERAC FROM TC_BONO WHERE CODEMP = '" + empresaGlobal.empresaID + "' And BONO='" + bono + "') ";
                strSql += "ORDER BY a.PUESTO";
                
                sqlCmd = new SqlCommand(strSql, conexion);
                sqlDR = sqlCmd.ExecuteReader();

                while (sqlDR.Read())
                    this.comboBox1.Items.Add(sqlDR[0].ToString());
                sqlDR.Close();

            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al cargar las lista de máquinas disponibles.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }


        private void cargarOrdFab()
        {     
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            try
            {
            
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                String strSql = "SELECT c.CODIGO AS Código, c.ORDFAB AS Órden_Fabricación,c.CTDLAN AS Cantidad,  ";
                strSql += "(SELECT sum(PBUENAS_float) FROM TC_IMPORT_BONOS WHERE ORDFAB_int=a.ORDFAB and Correl_SmallInt=a.CORREL) as Fichadas ";      
                strSql += "FROM TC_BONOL a ";
                strSql += "INNER JOIN T_ORDFABO b ON a.CODEMP=b.CODEMP AND a.TIPOREG=b.TIPOREG AND a.ORDFAB=b.ORDFAB AND a.CORREL=b.CORREL ";
                strSql += "INNER JOIN T_ORDFAB c ON c.CODEMP=b.CODEMP AND c.TIPOREG=b.TIPOREG AND c.ORDFAB=b.ORDFAB ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE a.CODEMP='" + empresaGlobal.empresaID + "' AND a.BONO='" + bono + "' ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns[1].Width = 125;
                dataGridView1.Columns[2].Width = 85;
                dataGridView1.Columns[3].Width = 85;

            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al cargar las lista de órdenes de trabajo.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        
        }

  
        private void ActualizarTablas(string puesto)
        {            
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());
            if (conexion.State != ConnectionState.Open)
                conexion.Open();
            SqlTransaction transaction = conexion.BeginTransaction();
            try
            {
               
                String usuario = Environment.UserName.ToUpper();
                // Modificamos el puesto en la tabla TC_BONO
                String strSql = "";
                strSql = "UPDATE TC_BONO ";
                strSql += "SET PUESTO = '" + puesto + "', ";
                strSql += "USUMOD = '" + usuario + "', ";
                strSql += "FECHAM = GETDATE() ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' AND BONO='" + bono + "' ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.Transaction = transaction;
                sqlCmd.ExecuteScalar();


               

                // Modificamos el puesto en la tabla TC_BONOL
                strSql = "UPDATE TC_BONOL ";
                strSql += "SET PUESTO = '" + puesto + "', ";
                strSql += "USUMOD = '" + usuario + "', ";
                strSql += "FECHAM = GETDATE() ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' AND BONO='" + bono + "' ";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.Transaction = transaction;
                sqlCmd.ExecuteScalar();



                // Modificamos el puesto en la tabla T_ORDFABO
                strSql = @"UPDATE       T_ORDFABO
                            SET                PUESTO = @puesto,
                                               USUMOD = @usuario,
                                               FECHAM = GETDATE() 
                            FROM            TC_BONOL INNER JOIN
                                                        T_ORDFABO ON TC_BONOL.CODEMP = T_ORDFABO.CODEMP AND TC_BONOL.TIPOREG = T_ORDFABO.TIPOREG AND TC_BONOL.ORDFAB = T_ORDFABO.ORDFAB AND 
                                                        TC_BONOL.CORREL = T_ORDFABO.CORREL
                            WHERE        (TC_BONOL.CODEMP = @codemp) AND (TC_BONOL.BONO = @bono)";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.Transaction = transaction;
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                sqlCmd.Parameters.AddWithValue("@codemp", empresaGlobal.empresaID);
                sqlCmd.Parameters.AddWithValue("@puesto", puesto);
                sqlCmd.Parameters.AddWithValue("@bono", bono);
                sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                sqlCmd.ExecuteNonQuery();


                // Modificamos el puesto en la tabla TC_IMPORT_BONOS
                strSql = @"UPDATE       TC_IMPORT_BONOS
                            SET                MAQUINA = @puesto,
                                               USUMOD = @usuario,
                                               FECHAM = GETDATE() 
                            FROM            TC_IMPORT_BONOS INNER JOIN
                                                     TC_BONOL ON TC_IMPORT_BONOS.ORDFAB_int = TC_BONOL.ORDFAB AND TC_IMPORT_BONOS.Correl_SmallInt = TC_BONOL.CORREL AND TC_IMPORT_BONOS.CODEMP = TC_BONOL.CODEMP AND 
                                                     TC_IMPORT_BONOS.BONO = TC_BONOL.BONO
                            WHERE        (TC_IMPORT_BONOS.CODEMP = @codemp) AND (TC_IMPORT_BONOS.BONO = @bono)";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.Transaction = transaction;
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                sqlCmd.Parameters.AddWithValue("@codemp", empresaGlobal.empresaID);
                sqlCmd.Parameters.AddWithValue("@puesto", puesto);
                sqlCmd.Parameters.AddWithValue("@bono", bono);
                sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                sqlCmd.ExecuteNonQuery();



                // Modificamos el puesto en la tabla TC_SALIDAS_FAB
                strSql = @"UPDATE       TC_SALIDAS_FAB
                            SET                MAQUI = @puesto,
                                               USUMOD = @usuario,
                                               FECHAM = GETDATE() 
                            FROM            TC_SALIDAS_FAB INNER JOIN
                                                     TC_IMPORT_BONOS ON TC_SALIDAS_FAB.idFab = TC_IMPORT_BONOS.idFab AND TC_SALIDAS_FAB.CODEMP = TC_IMPORT_BONOS.CODEMP
                            WHERE        (TC_IMPORT_BONOS.CODEMP = @codemp) AND (TC_IMPORT_BONOS.BONO = @bono)";

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.Transaction = transaction;
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                sqlCmd.Parameters.AddWithValue("@codemp", empresaGlobal.empresaID);
                sqlCmd.Parameters.AddWithValue("@puesto", puesto);
                sqlCmd.Parameters.AddWithValue("@bono", bono);
                sqlCmd.Parameters.AddWithValue("@usuario", usuario);
                sqlCmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show(Form.ActiveForm, "El cambio de máquina se realizó correctamente.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al modificar la máquina del bono." + ex.GetBaseException());
            }
            finally
            {
                
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }
     

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
                MessageBox.Show(Form.ActiveForm, "No ha seleccionado ninguna máquina para realizar el cambio.");
            else
            {
                Object selectedItem = comboBox1.SelectedItem;
                String puesto = selectedItem.ToString();
                int pos = puesto.IndexOf(" ", 0);
                puesto = puesto.Substring(0, pos);

                ActualizarTablas(puesto);
                obtenerDatosBono();
                //obtenerDatosPersona();
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void cargarProyectos()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                String strSql = "SELECT CODLANZA, CAST(CODLANZA AS Varchar(5)) + '      ' + PROYECTO AS PROYECTO ";
                strSql += "FROM TC_LANZA  ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' AND (VALIDADO = 'S') ";
                strSql += "ORDER BY CODLANZA DESC ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                int cont = 0;
                int pos = 0;
                int enc = 0;
                while (sqlDR.Read())
                {
                    if (sqlDR[0].ToString() == proyecto)
                    {
                        pos = cont;
                        enc = 1;
                    }
                    this.comboBox2.Items.Add(sqlDR[1].ToString());
                    cont = cont + 1;
                }
                sqlDR.Close();

                if (enc!=0)
                    comboBox2.SelectedIndex = pos;
                 
            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al cargar los proyectos disponibles.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void cargarOperacion()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                String strSql = "SELECT DISTINCT CONVERT(VARCHAR(max),DENOMI) ";
                strSql += "FROM T_OPERAC ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' ";
                strSql += "ORDER BY CONVERT(VARCHAR(max),DENOMI) ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                while (sqlDR.Read())
                    this.comboBox3.Items.Add(sqlDR[0].ToString());

                sqlDR.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al cargar las operaciones disponibles.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }


        private void ObtenerProyectoDeBono()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // obtenemos los proyectos
                String strSql = "SELECT DISTINCT c.PROYECTO FROM TC_BONOL a ";
                strSql += "INNER JOIN T_ORDFABO b ON  a.CODEMP=b.CODEMP AND a.TIPOREG=b.TIPOREG AND a.ORDFAB=b.ORDFAB AND a.CORREL= b.CORREL ";
                strSql += "INNER JOIN T_ORDFAB c ON  c.CODEMP=b.CODEMP AND c.TIPOREG=b.TIPOREG AND c.ORDFAB=b.ORDFAB ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE a.CODEMP='" + empresaGlobal.empresaID + "' AND a.BONO='" + bono + "'";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                if (sqlDR.Read())
                    proyecto = sqlDR[0].ToString();
                sqlDR.Close();

            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al obtener el proyecto asociado a un bono.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

        }


       private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bono = "";
            cargarBonos();
            deshabilitarCampos();    
        }

    
        public Boolean permitirModificarMaquinaBono()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());
            string resultado="0";        
            
            //comprobamos si el estado de la salida de fabrica asociada al bono tiene valor 1
            //En caso afirmativo permitimos modificar el bono
            //En caso negativo le indicamos al usuario que no puede modificar la máquina asociada
            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                String strSql = "SELECT  CASE WHEN count(*)>0 THEN ";
                strSql = "SELECT  CASE WHEN count(*)>0 THEN ";
                strSql += "(CASE WHEN count(*)=sum(d.estado) THEN 1 ELSE 0 END) ";
                strSql += "ELSE 1 END AS resultado ";
                strSql += "FROM TC_BONO a ";
                strSql += "INNER JOIN TC_BONOL b ON a.CODEMP=b.CODEMP AND a.BONO = b.BONO ";
                strSql += "INNER JOIN TC_IMPORT_BONOS c ON b.CORREL = c.Correl_SmallInt AND b.ORDFAB=c.ORDFAB_int ";
                strSql += "INNER JOIN TC_SALIDAS_FAB d ON  c.idFab =  d.idFab ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE a.CODEMP= '" + empresaGlobal.empresaID + "' AND a.BONO = '" + bono + "' ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                if (sqlDR.Read())
                    resultado = sqlDR[0].ToString();
                sqlDR.Close();

            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al obtener el estado de las salidas de fábricas asociadas al bono.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            if (resultado == "1")
                return true;
            else
            {
                return false;
            }
            
        }


        private void obtenerDatosBono()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());    

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // obtenemos los proyectos
                String strSql = "SELECT a.PUESTO +'   '+ b.DENOMI, a.OPERAC, c.DENOMI,  a.PersonaAsignada, d.NOMBRE + ' ' + d.APELLIDOS ";
                strSql += "FROM TC_BONO a ";
                strSql += "INNER JOIN T_PUESTOS b ON b.CodEMP=a.CODEMP AND b.PUESTO = a.PUESTO ";
                strSql += "INNER JOIN T_OPERAC c ON c.CodEMP=a.CODEMP AND c.OPERAC=a.OPERAC AND c.PUESTO=a.PUESTO ";
                strSql += "LEFT JOIN CS_CPP_0713.dbo.CPP_EMPLEADOS d ON d.NUMERO_PERSONAL = a.PersonaAsignada ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE a.CODEMP='" + empresaGlobal.empresaID + "' AND a.BONO='" + bono + "' ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                if (sqlDR.Read())
                {
                    this.textBox3.Text= sqlDR[1].ToString() + "   " + sqlDR[2].ToString();
                    this.textBox2.Text= sqlDR[0].ToString();
                    this.textBox4.Text = sqlDR[3].ToString() + "   " + sqlDR[4].ToString();
                }
                sqlDR.Close();

            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al obtener los datos del bono.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }


        private void listaPersonas()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // obtenemos los proyectos
                String strSql = "SELECT a.NUMERO_PERSONAL AS Número_Personal, a.NOMBRE + ' ' + a.APELLIDOS AS Nombre_Completo ";
                strSql += "FROM CS_CPP_0713.dbo.CPP_EMPLEADOS a ";
                strSql += "INNER JOIN CS_CPP_0713.dbo.CPP_HISTORIAL_EMPLEADOS b ON a.NUMERO_PERSONAL = b.NUMERO_PERSONAL_EMPLEADO AND b.FECHA_BAJA IS NULL AND b.PERSONAL_DIRECTO=1 ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE a.CodEMP='" + empresaGlobal.empresaID + "' AND a.NOMBRE + ' ' + a.APELLIDOS LIKE '%" + textBox1.Text + "%' ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;

                dataGridView2.Columns[0].Width=100;
                dataGridView2.Columns[1].Width=275;
 

            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al obtener la lista de personas.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //listaPersonas();
        }

        private void button3_Click(object sender, EventArgs e)
        {

             int i = dataGridView2.CurrentRow.Index;
             String persona = dataGridView2.Rows[i].Cells[0].Value.ToString();

             modificarPersonaAsignadaBono(persona);
             obtenerDatosBono();
             //obtenerDatosPersona();
        }

        private void modificarPersonaAsignadaBono(String persona)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // Modificamos el puesto en la tabla TC_BONO
                String strSql = "UPDATE TC_BONO ";
                strSql += "SET PersonaAsignada = " + persona + " ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' AND BONO='" + bono + "' ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                sqlCmd.ExecuteScalar();

                MessageBox.Show(Form.ActiveForm, "El cambio de operario se realizó correctamente.");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al asignar el operario del bono." + ex.GetBaseException());
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }


        private void cargarBonos()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());
            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                String strSql = "SELECT DISTINCT a.BONO AS Bono, d.PROYECTO as Proyecto ";
                strSql += "FROM TC_BONO a ";
                strSql += "INNER JOIN T_OPERAC op ON a.CODEMP=op.CodEMP AND a.OPERAC=op.OPERAC ";
                //strSql += "INNER JOIN T_PUESTOS p ON p.CodEMP=a.CODEMP AND p.PUESTO=a.PUESTO ";

                if (textBox8.Text != "")
                    strSql += "INNER JOIN CS_CPP_0713.dbo.CPP_EMPLEADOS empl ON empl.NUMERO_PERSONAL=a.PersonaAsignada ";

                strSql += "INNER JOIN TC_BONOL b ON a.CODEMP=b.CODEMP AND a.BONO = b.BONO ";
                strSql += "INNER JOIN T_ORDFABO c ON c.CODEMP=b.CODEMP AND c.TIPOREG=b.TIPOREG AND c.ORDFAB=b.ORDFAB AND c.CORREL = b.CORREL ";
                strSql += "INNER JOIN T_ORDFAB d ON d.CODEMP=c.CODEMP AND d.TIPOREG=c.TIPOREG AND d.ORDFAB=c.ORDFAB ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE a.CODEMP='" + empresaGlobal.empresaID + "' ";

                //si proyecto
                if (comboBox2.SelectedIndex != -1)
                {
                    proyecto = comboBox2.SelectedItem.ToString();
                    int pos = proyecto.IndexOf("  ");
                    proyecto = proyecto.Substring(0, pos);

                    strSql += "AND d.PROYECTO = '" + proyecto + "' ";
                }

                // si bono
                if (textBox5.Text != "")
                    strSql += "AND a.BONO = '" + textBox5.Text + "' ";
  
                //Si operacion
                if (comboBox3.SelectedIndex!=-1)
                {
                    String operacion = comboBox3.SelectedItem.ToString();
                    if (operacion != "")
                        strSql += "AND CONVERT(VARCHAR(max),op.DENOMI) ='" + operacion + "' ";
                        // strSql += "AND a.OPERAC ='" + textBox6.Text + "' ";
                }
        
                //Si puesto
                if (textBox7.Text != "")
                    //strSql += "AND p.PUESTO+' '+p.DENOMI LIKE '%" + textBox7.Text + "%' ";
                    strSql += "AND a.PUESTO ='" + textBox7.Text + "' ";

                //Si persona asignada
                if (textBox8.Text != "")
                    strSql += "AND CONVERT(VARCHAR(10),empl.NUMERO_PERSONAL) +' '+ empl.NOMBRE +' '+ empl.APELLIDOS LIKE '%" + textBox8.Text + "%' ";

                if (radioButton1.Checked == true)
                    strSql += "AND a.BonoCerrado ='S' ";

                if (radioButton2.Checked == true)
                    strSql += "AND a.BonoCerrado ='N' ";

                strSql += "ORDER BY a.BONO DESC";
          
                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView3.DataSource = dt;
                dataGridView3.Columns[0].Width = 47;
                dataGridView3.Columns[1].Width = 53;
                dataGridView3.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al cargar los bonos asociados." + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }


        private void deshabilitarCampos()
        {
            //máquina
            comboBox1.SelectedIndex = -1;
            dataGridView1.Enabled = false;
            comboBox1.Enabled = false;
            button1.Enabled = false;
            button4.Enabled = false;
            label13.Text = "";
            cargarOrdFab();

            // operario
            //textBox1.Text = "";
            //textBox1.Enabled = false;
            button3.Enabled = false;
            //dataGridView2.Enabled = false;

            //bono
            this.textBox3.Text = "";
            this.textBox2.Text = "";
            this.textBox4.Text = "";
            button5.Enabled = false;
            btnCerrarBono.Enabled = false;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listaPersonas();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            bono = "";
            cargarBonos();
            deshabilitarCampos();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            bono = "";
            cargarBonos();
            deshabilitarCampos();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            bono = "";
            cargarBonos();
            deshabilitarCampos();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView3.CurrentRow.Index;
            bono = dataGridView3.Rows[i].Cells[0].Value.ToString();

            obtenerDatosBono();

            // habilitamos los campos del operario
            //textBox1.Text = "";
            //textBox1.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            btnCerrarBono.Enabled = true;
            //dataGridView2.Enabled = true;

            if (permitirModificarMaquinaBono())
            {
                comboBox1.SelectedIndex = -1;
                comboBox1.Enabled = true;
                button1.Enabled = true;
                button4.Enabled = true;
                label13.Text = "";

                comboBox1.SelectedIndex = -1;
                comboBox1.Items.Clear();
                dataGridView1.Enabled = true;
                cargarPuestos();
                cargarOrdFab();

            }
            else
            {
                // El usuario no puede modificar la máquina asociada al bono, bloqueamos todos los campos
                comboBox1.SelectedIndex = -1;
                comboBox1.Enabled = false;
                button1.Enabled = false;
                label13.Text = " -> LA MÁQUINA DE ESTE BONO NO PUEDE SER MODIFICADA. ";
            }

        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            eliminarUltimoFichaje();
            modificamosEstadoBono();
            cargarOrdFab();
        }


        private void eliminarUltimoFichaje()
        {
         SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            eliminado = "0";// variable que indica si el fichaje ha sido eliminado

            // Eliminamos la última entrada para elc bono en la tabla 
            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                // obtenemos el id en CARGADEPIEZAS que ha sido insertada por última vez MP_CARGADEPIEZAS
                String strSql = "SELECT cdp.id ";
                strSql += "FROM CS_CPP_0713.dbo.MP_TRABAJOCONBONO tcb ";
                strSql += "INNER JOIN CS_CPP_0713.dbo.MP_CARGADEPIEZAS cdp ON cdp.mjTrabajoConBonoId = tcb.id ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE tcb.CODEMP='" + empresaGlobal.empresaID + "' AND tcb.codBono='" + bono + "' ";
                strSql += "AND cdp.fecha = ";
                strSql += "(SELECT MAX(cdp.fecha) FROM CS_CPP_0713.dbo.MP_TRABAJOCONBONO tcb ";
                strSql += "INNER JOIN CS_CPP_0713.dbo.MP_CARGADEPIEZAS cdp ON cdp.mjTrabajoConBonoId = tcb.id ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += " WHERE tcb.CODEMP='" + empresaGlobal.empresaID + "' AND tcb.codBono='" + bono + "') ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                SqlDataReader sqlDR = sqlCmd.ExecuteReader();
                 
                String id="";
                if (sqlDR.Read())
                     id= sqlDR[0].ToString();
                sqlDR.Close();
           
                if (id!="")
                {    
                   // Eliminamos el último fichaje
                    strSql = "DELETE FROM CS_CPP_0713.dbo.MP_CARGADEPIEZAS ";
                    strSql += "WHERE id='" + id + "' ";

                    sqlCmd = new SqlCommand(strSql, conexion);
                    sqlCmd.ExecuteScalar();

                    eliminado = "1";

                    MessageBox.Show(Form.ActiveForm, "El último fichaje ha sido elimiando correctamente.");
  
                }
                else
                {
                    MessageBox.Show(Form.ActiveForm, "No existen fichajes que eliminar.");
                }
     

            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al borrar el último fichaje realizado en el bono. " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


            }
        }

        private void modificamosEstadoBono()
        {
            if (eliminado == "1")
            {
                SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

                try
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();

                    // obtenemos el id en CARGADEPIEZAS que ha sido insertada por última vez MP_CARGADEPIEZAS
                    String strSql = " ";
                    strSql += "SELECT BonoCerrado ";
                    strSql += "FROM TC_BONO ";
                    //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                    strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' AND BONO='" + bono + "' ";

                    SqlCommand sqlCmd = new SqlCommand(strSql, conexion);
                    string bonoCerrado = sqlCmd.ExecuteScalar().ToString();


                    if (bonoCerrado == "S")
                    {
                        // Modificamos el estado del bono
                        strSql = "UPDATE TC_BONO ";
                        strSql += "SET BonoCerrado='N' ";
                        //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                        strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' AND BONO='" + bono + "' ";

                        sqlCmd = new SqlCommand(strSql, conexion);
                        sqlCmd.ExecuteScalar();

                        MessageBox.Show(Form.ActiveForm, "El estado del bono ha sido modificado. Ahora el bono está abierto.");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show(Form.ActiveForm, "Ocurrió un error al modificar el estado del bono.");
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Close();
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            bono = "";
            cargarBonos();

            // como no hay seleccionado bono, bloqueamos todos los campos
            deshabilitarCampos();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            String persona = "NULL";

            modificarPersonaAsignadaBono(persona);
            obtenerDatosBono();
        }

        private void btnCerrarBono_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

               
                String strSql = " ";
                strSql += "SELECT BonoCerrado ";
                strSql += "FROM TC_BONO ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' AND BONO='" + bono + "' ";

                SqlCommand sqlCmd = new SqlCommand(strSql, conexion);

                string estadoBono = sqlCmd.ExecuteScalar().ToString();
                if (estadoBono == "N")
                {
                    // Modificamos el estado del bono
                    strSql = "UPDATE TC_BONO ";
                    strSql += "SET BonoCerrado='S' ";
                    //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                    strSql += "WHERE CODEMP='" + empresaGlobal.empresaID + "' AND BONO='" + bono + "' ";

                    sqlCmd = new SqlCommand(strSql, conexion);
                    sqlCmd.ExecuteScalar();

                    MessageBox.Show(Form.ActiveForm, "El estado del bono ha sido modificado. Ahora el bono está cerrado.");
                }
                else
                {
                    MessageBox.Show(Form.ActiveForm, "El bono seleccionado ya está cerrado.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al modificar el estado del bono.");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //Como hay dos radiobutton que serán ejecutados (true y false). Sólo lanzaremos el que tenga valor a true
            if (radioButton1.Checked == true)
            {
                bono = "";
                cargarBonos();
                deshabilitarCampos();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //Como hay dos radiobutton que serán ejecutados (true y false). Sólo lanzaremos el que tenga valor a true
            if (radioButton2.Checked == true)
            {
                bono = "";
                cargarBonos();
                deshabilitarCampos();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                bono = "";
                cargarBonos();
                deshabilitarCampos();
            }
        }




    }
}
