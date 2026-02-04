using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using empresaGlobalProj;

namespace ProyectosOnLine
{
    public partial class Form3 : Form
    {
        private Int32 of;
        private List<int> listaOF = new List<int>();
        private String operacion;
        private Int32 operario;

        private Int32 id;
        private Int32 bono;
        private Int32 etiqueta;
        private Int32 tipo; 
        

        public Form3(Int32 ordfab, String operacion, Int32 operario, Int32 bono, Int32 etique, Int32 id, Int32 tipo)
        {
            InitializeComponent();

            this.of = ordfab;
            this.operacion = operacion;
            this.operario = operario;


            this.id = id;
            this.bono = bono;
            this.etiqueta = etique;

            this.tipo = tipo;

            this.Text += ordfab.ToString();
            this.textBox1.Text = this.operario.ToString();

            if (this.id != 0)
            {
                this.dateTimePicker1.Visible = false;                
            }            
        }

        public Form3(List<int> ordfab, String operacion, Int32 operario, Int32 bono, Int32 etique, Int32 id, Int32 tipo)
        {
            InitializeComponent();

            this.listaOF = ordfab;
            this.operacion = operacion;
            this.operario = operario;


            this.id = id;
            this.bono = bono;
            this.etiqueta = etique;

            this.tipo = tipo;


            this.Text += ordfab.ToString();
            this.textBox1.Text = this.operario.ToString();

            if (this.id != 0)
            {
                this.dateTimePicker1.Visible = false;
            }
        }





        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'bonoEtiquetas.TC_V_BONO_ETIQUETA' Puede moverla o quitarla según sea necesario.
            if (tipo == 2) {
                for (int i = 0; i < listaOF.Count; i++) {
                    this.tC_V_BONO_ETIQUETATableAdapter.FillTodas(this.bonoEtiquetas.TC_V_BONO_ETIQUETA, empresaGlobal.empresaID, listaOF[i], this.operacion);          
               }

            }else{
                this.tC_V_BONO_ETIQUETATableAdapter.Fill(this.bonoEtiquetas.TC_V_BONO_ETIQUETA, empresaGlobal.empresaID, this.of, this.operacion);        
            }

        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            this.filtrar();
        }


        //filtrar por un bono y puede que etiqueta
        private void filtrar()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (this.bono != 0)
                {
                    if (this.etiqueta == 0)
                    {
                        if (Convert.ToInt32(this.dataGridView1.Rows[i].Cells[1].Value) != this.bono)
                            this.dataGridView1.Rows[i].Visible = false;
                    }
                    else
                    {
                        if (Convert.ToInt32(this.dataGridView1.Rows[i].Cells[1].Value) != this.bono && Convert.ToInt32(this.dataGridView1.Rows[i].Cells[2].Value) != this.etiqueta)
                            this.dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
        }




        //Insertar piezas al operario indicado
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());

            String userName = Environment.UserName;
            String operario = this.textBox1.Text;
            //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
            String codEmp = empresaGlobal.empresaID;//ConfigurationManager.AppSettings["EMPRESA"].ToString().ToUpper();
            

            if (userName.Length > 8)
                userName = userName.Substring(0, 8);


            String strSql = "";
            SqlCommand sqlCmd;


            Boolean existe_presencia = false;
            Int32 id_tarjeta = 0;
            Int32 id_presencia = 0;
            Int32 id_trabajo = 0;

            DateTime fecha_aux = this.dateTimePicker1.Value;


            
            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();


                //Caso para fichar las piezas a un operario concreto
                if (this.id != 0)
                {
                    #region Comprobar que la fecha del trabajo para añadir piezas a un opeario esta dentro del intervalo del fichero de control
                    DateTime fecha_auxiliar;
                    strSql = " Select iFecha";
                    strSql += "     From CS_CPP_0713.dbo.MP_TRABAJOCONBONO ";
                    strSql += "     Where id = " + this.id;

                    sqlCmd = new SqlCommand(strSql, conexion);
                    fecha_auxiliar = Convert.ToDateTime(sqlCmd.ExecuteScalar());
                    

                    strSql = "select gg.dbo.getEsFechaControl ('" + codEmp + "', 'MS', convert(datetime, '" + fecha_auxiliar + "', 103)) ";
                    sqlCmd = new SqlCommand(strSql, conexion);

                    if (Convert.ToInt32(sqlCmd.ExecuteScalar()) == -1)
                    {
                        MessageBox.Show(Form.ActiveForm, "La fecha para añadir piezas al trabajo de ese operario no está permitida");
                        this.dateTimePicker1.Focus();

                        return;
                    }
                    #endregion


                    for (int i = 0; i < this.dataGridView1.RowCount; i++)
                    {
                        if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[0].Value) == true)
                        {
                            //strSql = " Insert Into MP_CARGADEPIEZAS ";
                            //strSql += "	Select gg.dbo.tc_import_bonos.MjTrabajoConBonoId, " + this.dataGridView1.Rows[i].Cells[2].Value.ToString() + ", " + Convert.ToInt32(this.dataGridView1.Rows[i].Cells[3].Value) + ", ";
                            //strSql += "         'T-" + userName + "', dateadd(SS, -1, MP_TRABAJOCONBONO.fFecha), 0, 0, null";
                            //strSql += "     From gg.dbo.tc_import_bonos inner join MP_TRABAJOCONBONO On gg.dbo.tc_import_bonos.MjTrabajoConBonoId = MP_TRABAJOCONBONO.id ";
                            //strSql += "     Where gg.dbo.tc_import_bonos.id = " + this.id;

                            //JRegino 17/06/2013
                            strSql = " Insert Into CS_CPP_0713.dbo.MP_CARGADEPIEZAS (mjTrabajoConBonoId, marcaCodigo, marcaCantidad, terminal, fecha, offLine, autorizado, autorizacionId) ";
                            strSql += "	Values (" +this.id + ", " + this.dataGridView1.Rows[i].Cells[2].Value.ToString() + ", " + Convert.ToInt32(this.dataGridView1.Rows[i].Cells[3].Value) + ", 'T-" + userName + "', dateadd(SS, -1, convert(datetime, '"+ fecha_auxiliar +"', 103)), 0, 0, null)";


                            sqlCmd = new SqlCommand(strSql, conexion);                            
                            sqlCmd.ExecuteScalar();
                        }
                    }
                }
                else
                {
                    #region Comprobar que la fecha elegida esta dentro del intervalo del fichero de control
                    strSql = "select gg.dbo.getEsFechaControl ('" + codEmp + "', 'MS', convert(datetime, '" + fecha_aux + "', 103)) ";
                    sqlCmd = new SqlCommand(strSql, conexion);

                    if (Convert.ToInt32(sqlCmd.ExecuteScalar()) == -1)
                    {
                        MessageBox.Show(Form.ActiveForm, "La fecha no está permitida");
                        this.dateTimePicker1.Focus();

                        return;
                    }
                    #endregion


                    DateTime entrada_presencia = new DateTime(fecha_aux.Year, fecha_aux.Month, fecha_aux.Day, 8, 0, 0);
                    DateTime inicio_trabajo = new DateTime(fecha_aux.Year, fecha_aux.Month, fecha_aux.Day, 8, 1, 0);



                    #region Entrada Presencia
                    strSql = " SELECT MAX(id) AS id_presencia ";
                    strSql += "     FROM cs_cpp_0713.dbo.MJ_PRESENCIAS ";
                    strSql += "     WHERE (numeroPersonalEmpleado = " + operario + ") ";
                    strSql += "             AND ( cast(eFecha as Date) = cast(convert(datetime, '" + entrada_presencia + "', 103) as Date) ) ";

                    sqlCmd = new SqlCommand(strSql, conexion);
                    try
                    {
                        id_presencia = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    }
                    catch (Exception)
                    {
                        id_presencia = 0;
                    }



                    if (id_presencia > 0)
                        existe_presencia = true;
                    else
                    {
                        existe_presencia = false;

                        strSql = " SELECT cs_cpp_0713.dbo.CPP_TARJETAS.ID ";
                        strSql += "     FROM cs_cpp_0713.dbo.CPP_TARJETAS INNER JOIN cs_cpp_0713.dbo.CPP_HISTORIAL_EMPLEADOS ON  ";
                        strSql += "         cs_cpp_0713.dbo.CPP_TARJETAS.ID_HISTORIAL_EMPLEADO = cs_cpp_0713.dbo.CPP_HISTORIAL_EMPLEADOS.ID ";
                        strSql += "     WHERE (cs_cpp_0713.dbo.CPP_TARJETAS.FECHA_BAJA IS NULL) AND (cs_cpp_0713.dbo.CPP_HISTORIAL_EMPLEADOS.NUMERO_PERSONAL_EMPLEADO = " + operario + ") ";

                        sqlCmd = new SqlCommand(strSql, conexion);

                        id_tarjeta = Convert.ToInt32(sqlCmd.ExecuteScalar());




                        sqlCmd = new SqlCommand("cs_cpp_0713.dbo.spGuardarMarcajePresenciaEntrada", conexion);
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@CODIDENTIFICACION", SqlDbType.BigInt);
                        sqlCmd.Parameters["@CODIDENTIFICACION"].Value = id_tarjeta;

                        sqlCmd.Parameters.Add("@EOFFLINE", SqlDbType.Bit);
                        sqlCmd.Parameters["@EOFFLINE"].Value = 0;

                        sqlCmd.Parameters.Add("@ETERMINAL", SqlDbType.VarChar);
                        sqlCmd.Parameters["@ETERMINAL"].Value = "T-" + userName;

                        sqlCmd.Parameters.Add("@EAUTORIZADO", SqlDbType.Bit);
                        sqlCmd.Parameters["@EAUTORIZADO"].Value = 0;

                        sqlCmd.Parameters.Add("@EAUTORIZACIONID", SqlDbType.BigInt);
                        sqlCmd.Parameters["@EAUTORIZACIONID"].Value = 0;

                        sqlCmd.Parameters.Add("@EFECHA", SqlDbType.DateTime);
                        entrada_presencia = entrada_presencia.AddSeconds(1);
                        sqlCmd.Parameters["@EFECHA"].Value = entrada_presencia;

                        sqlCmd.Parameters.Add("@NUMEROPERSONAL", SqlDbType.Int);
                        sqlCmd.Parameters["@NUMEROPERSONAL"].Value = operario;

                        sqlCmd.Parameters.Add("@FRANJAHORARIA", SqlDbType.Int);
                        sqlCmd.Parameters["@FRANJAHORARIA"].Value = 95;

                        sqlCmd.Parameters.Add("@FALTAPUNTUALIDAD", SqlDbType.VarChar);
                        sqlCmd.Parameters["@FALTAPUNTUALIDAD"].Value = 'N';

                        sqlCmd.ExecuteNonQuery();



                        strSql = " SELECT MAX(id) AS id_presencia ";
                        strSql += "     FROM cs_cpp_0713.dbo.MJ_PRESENCIAS ";
                        strSql += "     WHERE (numeroPersonalEmpleado = " + operario + ") ";

                        sqlCmd = new SqlCommand(strSql, conexion);
                        id_presencia = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    }





                    strSql = " SELECT efecha ";
                    strSql += "     FROM cs_cpp_0713.dbo.MJ_PRESENCIAS ";
                    strSql += "     WHERE (id = " + id_presencia + ") ";
                    sqlCmd = new SqlCommand(strSql, conexion);
                    entrada_presencia = Convert.ToDateTime(sqlCmd.ExecuteScalar());



                    strSql = " SELECT MAX(fFecha) ";
                    strSql += "     FROM cs_cpp_0713.dbo.MP_TRABAJOCONBONO ";
                    strSql += "     WHERE (mjPresenciaId = " + id_presencia + ") ";
                    sqlCmd = new SqlCommand(strSql, conexion);
                    try
                    {
                        inicio_trabajo = Convert.ToDateTime(sqlCmd.ExecuteScalar()).AddSeconds(1);
                    }
                    catch (Exception)
                    {
                        inicio_trabajo = entrada_presencia.AddSeconds(1);
                    }

                    #endregion



                    Int32 bono_anterior = 0;
                    Int32 bono_actual = 0;

                    for (int i = 0; i < this.dataGridView1.RowCount; i++)
                    {
                        if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[0].Value) == true)    //Si está seleccionado el checkbox
                        {
                            bono_actual = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[1].Value);

                            if (bono_actual != bono_anterior)
                            {
                                #region Inicio Trabajo
                                sqlCmd = new SqlCommand("cs_cpp_0713.dbo.spGuardarMarcajeInicioTrabajo", conexion);
                                sqlCmd.CommandType = CommandType.StoredProcedure;

                                sqlCmd.Parameters.Add("@MJPRESENCIAID", SqlDbType.BigInt);
                                sqlCmd.Parameters["@MJPRESENCIAID"].Value = id_presencia;

                                sqlCmd.Parameters.Add("@CODBONO", SqlDbType.Int);
                                sqlCmd.Parameters["@CODBONO"].Value = bono_actual;

                                sqlCmd.Parameters.Add("@TIPOBONO", SqlDbType.Int);
                                sqlCmd.Parameters["@TIPOBONO"].Value = 1;

                                sqlCmd.Parameters.Add("@IOFFLINE", SqlDbType.Bit);
                                sqlCmd.Parameters["@IOFFLINE"].Value = 0;

                                sqlCmd.Parameters.Add("@ITERMINAL", SqlDbType.VarChar);
                                sqlCmd.Parameters["@ITERMINAL"].Value = "T-" + userName;

                                sqlCmd.Parameters.Add("@IAUTORIZADO", SqlDbType.Bit);
                                sqlCmd.Parameters["@IAUTORIZADO"].Value = 0;

                                sqlCmd.Parameters.Add("@IAUTORIZACIONID", SqlDbType.BigInt);
                                sqlCmd.Parameters["@IAUTORIZACIONID"].Value = DBNull.Value;

                                sqlCmd.Parameters.Add("@IFECHA", SqlDbType.DateTime);
                                inicio_trabajo = inicio_trabajo.AddSeconds(1);
                                sqlCmd.Parameters["@IFECHA"].Value = inicio_trabajo;

                                sqlCmd.Parameters.Add("@CODEMP", SqlDbType.VarChar);
                                sqlCmd.Parameters["@CODEMP"].Value = codEmp;

                                sqlCmd.ExecuteNonQuery();



                                strSql = " SELECT MAX(id) ";
                                strSql += "     FROM cs_cpp_0713.dbo.MP_TRABAJOCONBONO ";
                                strSql += "     WHERE (mjPresenciaId = " + id_presencia + ") ";
                                sqlCmd = new SqlCommand(strSql, conexion);
                                id_trabajo = Convert.ToInt32(sqlCmd.ExecuteScalar());
                                #endregion
                            }

                                #region Carga Piezas
                            try
                            {                                
                                sqlCmd = new SqlCommand("cs_cpp_0713.dbo.spGuardarMarcajeCargaPiezas", conexion);
                                sqlCmd.CommandType = CommandType.StoredProcedure;


                                sqlCmd.Parameters.Add("@MJTRABAJOID", SqlDbType.BigInt);
                                sqlCmd.Parameters["@MJTRABAJOID"].Value = id_trabajo;

                                sqlCmd.Parameters.Add("@MARCACODIGO", SqlDbType.VarChar);
                                sqlCmd.Parameters["@MARCACODIGO"].Value = this.dataGridView1.Rows[i].Cells[2].Value.ToString();

                                sqlCmd.Parameters.Add("@MARCACANTIDAD", SqlDbType.Int);
                                sqlCmd.Parameters["@MARCACANTIDAD"].Value = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[3].Value);

                                sqlCmd.Parameters.Add("@OFFLINE", SqlDbType.Bit);
                                sqlCmd.Parameters["@OFFLINE"].Value = 0;

                                sqlCmd.Parameters.Add("@TERMINAL", SqlDbType.VarChar);
                                sqlCmd.Parameters["@TERMINAL"].Value = "T-" + userName;

                                sqlCmd.Parameters.Add("@AUTORIZADO", SqlDbType.Bit);
                                sqlCmd.Parameters["@AUTORIZADO"].Value = 0;

                                sqlCmd.Parameters.Add("@AUTORIZACIONID", SqlDbType.BigInt);
                                sqlCmd.Parameters["@AUTORIZACIONID"].Value = DBNull.Value;

                                sqlCmd.Parameters.Add("@FECHA", SqlDbType.DateTime);
                                inicio_trabajo = inicio_trabajo.AddSeconds(1);
                                sqlCmd.Parameters["@FECHA"].Value = inicio_trabajo;


                                sqlCmd.ExecuteNonQuery();                                
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(Form.ActiveForm, "Ocurrio un error al guardar la carga de piezas: " + ex.Message.ToString());
                                this.DialogResult = DialogResult.Cancel;
                            }
                                #endregion

                            if (bono_actual != bono_anterior)
                            {
                                #region Fin Trabajo
                                sqlCmd = new SqlCommand("cs_cpp_0713.dbo.spGuardarMarcajeFinTrabajo", conexion);
                                sqlCmd.CommandType = CommandType.StoredProcedure;

                                sqlCmd.Parameters.Add("@ID", SqlDbType.BigInt);
                                sqlCmd.Parameters["@ID"].Value = id_trabajo;

                                sqlCmd.Parameters.Add("@fOFFLINE", SqlDbType.Bit);
                                sqlCmd.Parameters["@fOFFLINE"].Value = DBNull.Value;

                                sqlCmd.Parameters.Add("@fTERMINAL", SqlDbType.VarChar);
                                sqlCmd.Parameters["@fTERMINAL"].Value = "T-" + userName;

                                sqlCmd.Parameters.Add("@fAUTORIZADO", SqlDbType.Bit);
                                sqlCmd.Parameters["@fAUTORIZADO"].Value = 0;

                                sqlCmd.Parameters.Add("@fAUTORIZACIONID", SqlDbType.BigInt);
                                sqlCmd.Parameters["@fAUTORIZACIONID"].Value = DBNull.Value;

                                sqlCmd.Parameters.Add("@fFECHA", SqlDbType.DateTime);
                                inicio_trabajo = inicio_trabajo.AddSeconds(1);
                                sqlCmd.Parameters["@fFECHA"].Value = inicio_trabajo;

                                sqlCmd.Parameters.Add("@MPRIMACODIGO", SqlDbType.VarChar);
                                sqlCmd.Parameters["@MPRIMACODIGO"].Value = DBNull.Value;

                                sqlCmd.Parameters.Add("@MPRIMACANTIDAD", SqlDbType.Int);
                                sqlCmd.Parameters["@MPRIMACANTIDAD"].Value = DBNull.Value;

                                sqlCmd.Parameters.Add("@NUMPARTE", SqlDbType.Int);
                                sqlCmd.Parameters["@NUMPARTE"].Value = DBNull.Value;

                                sqlCmd.Parameters.Add("@CODEMP", SqlDbType.VarChar);
                                sqlCmd.Parameters["@CODEMP"].Value = codEmp;

                                sqlCmd.ExecuteNonQuery();
                                #endregion
                            }

                            bono_anterior = bono_actual;
                        }
                    }

                    #region Salida Presencia
                    if (existe_presencia == true)
                    {
                        inicio_trabajo = inicio_trabajo.AddSeconds(1);

                        strSql = " Update cs_cpp_0713.dbo.MJ_PRESENCIAS ";
                        strSql += "     Set sFecha =  convert(datetime, '" + inicio_trabajo + "', 103)";
                        strSql += "     WHERE (id = " + id_presencia + ") ";
                        sqlCmd = new SqlCommand(strSql, conexion);

                        sqlCmd.ExecuteScalar();
                    }
                    else
                    {
                        sqlCmd = new SqlCommand("cs_cpp_0713.dbo.spGuardarMarcajePresenciaSalida", conexion);
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@ID", SqlDbType.BigInt);
                        sqlCmd.Parameters["@ID"].Value = id_presencia;

                        sqlCmd.Parameters.Add("@CODIDENTIFICACION", SqlDbType.VarChar);
                        sqlCmd.Parameters["@CODIDENTIFICACION"].Value = id_tarjeta;

                        sqlCmd.Parameters.Add("@SOFFLINE", SqlDbType.Bit);
                        sqlCmd.Parameters["@SOFFLINE"].Value = 0;

                        sqlCmd.Parameters.Add("@STERMINAL", SqlDbType.VarChar);
                        sqlCmd.Parameters["@STERMINAL"].Value = "T-" + userName;

                        sqlCmd.Parameters.Add("@SAUTORIZADO", SqlDbType.Bit);
                        sqlCmd.Parameters["@SAUTORIZADO"].Value = 0;

                        sqlCmd.Parameters.Add("@SAUTORIZACIONID", SqlDbType.BigInt);
                        sqlCmd.Parameters["@SAUTORIZACIONID"].Value = DBNull.Value;

                        sqlCmd.Parameters.Add("@SFECHA", SqlDbType.DateTime);
                        inicio_trabajo = inicio_trabajo.AddSeconds(1);
                        sqlCmd.Parameters["@SFECHA"].Value = inicio_trabajo;

                        sqlCmd.ExecuteNonQuery();


                        //xa que no vuelva a crear una entrada
                        existe_presencia = true;
                    }
                    #endregion
                }

                MessageBox.Show(Form.ActiveForm, "Marcaje guardado correctamente");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrio un error al guardar el fichaje: " + ex.Message.ToString());
                this.DialogResult = DialogResult.Cancel;                
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();

                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_todo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = true;
                row.Selected = false;
            }
        }

        private void btn_nada_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = false;
                row.Selected = false;
            }
        }


    }
}