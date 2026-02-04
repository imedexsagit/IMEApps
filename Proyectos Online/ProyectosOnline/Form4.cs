using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;
using empresaGlobalProj;
using System.Globalization;

namespace ProyectosOnLine
{
    public partial class Form4 : Form
    {
        private Int32 of;

        public Form4(Int32 ordfab)
        {
            InitializeComponent();

            this.Text += ordfab.ToString();
            this.of = ordfab;
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'etiquetas.TC_LANZA_ETIQUETA' Puede moverla o quitarla según sea necesario.
            this.tC_LANZA_ETIQUETATableAdapter.Fill(this.etiquetas.TC_LANZA_ETIQUETA, this.of, empresaGlobal.empresaID);
        }


        //Recepcionar
        private void button1_Click(object sender, EventArgs e)
        {
            //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
            String codEmp = empresaGlobal.empresaID;//ConfigurationManager.AppSettings["EMPRESA"].ToString().ToUpper();
            String userName = Environment.UserName;

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString()))
            {

                //bandera que indica si no se ha seleccionado ningún checkbox
                bool enc = false;

                SqlTransaction transaction;
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                transaction = conexion.BeginTransaction();

                String strSql = "";
                SqlCommand sqlCmd = conexion.CreateCommand();
                sqlCmd.Connection = conexion;
                sqlCmd.Transaction = transaction;

                DateTime fecha_aux = this.dateTimePicker1.Value;

                try
                {



                    #region Comprobar que la fecha elegida esta dentro del intervalo del fichero de control
                    //JRM - Cambiar Empresa cambio '3' por empresaGlobal.empresaID
                    strSql = "select gg.dbo.getEsFechaControl ('" + empresaGlobal.empresaID + "', 'MS', convert(datetime, '" + fecha_aux + "', 103)) ";
                    sqlCmd.CommandText = strSql;

                    if (Convert.ToInt32(sqlCmd.ExecuteScalar()) == -1)
                    {
                        MessageBox.Show(Form.ActiveForm, "La fecha no está permitida");
                        this.dateTimePicker1.Focus();

                        return;
                    }

                    #endregion

                    #region Comprobar que el paquete no esté ya creado

                    for (int i = 0; i < this.dataGridView1.RowCount; i++)
                    {
                        if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[0].Value) == true)
                        {
                            //se encuentra checkbox marcado
                            enc = true;
                            //se obtiene ordfab según la celda sobre la que se ha hecho click en el form1
                            string ofg = Form1.ordfabGlobal;// this.dataGridView1[i].Cells["ctdaficharDataGridViewTextBoxColumn"].Value.ToString();
                            var etiqueta = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                            //Consultar anterior - JRM Etiquetas
                            /*strSql = @"select count(*) from imedexsa_intranet.dbo.GL_ITEMSPAQ where paque_id IN (SELECT     paque_id
                                                                                                              FROM       imedexsa_intranet.dbo.GL_ITEMSPAQ
                                                                                                            WHERE        (cb = " + etiqueta + "))";
                           */
                            /*strSql = @"SELECT ofb.CTDLAN - (SELECT        Sum(Cantidad) AS Expr1
                                   FROM            imedexsa_intranet.dbo.GL_ITEMSPAQ
                                   WHERE        (cb = " + etiqueta + ")) AS ctd_a_fichar FROM TC_LANZA_ETIQUETA le JOIN T_ORDFAB ofb on le.OrdFab = ofb.ORDFAB WHERE (le.OrdFab = " + ofg + ") AND (le.CodEmp = '3') AND (le.TIPOREG = 'F')";
                            */

                            //se cambia 3 por empresaGlobal 03/07/2020 JMRM
                            strSql = @"SELECT Cantidad - (SELECT        ISNULL(sum(imedexsa_intranet.dbo.GL_ITEMSPAQ.Cantidad),0) AS Expr1
                                FROM            imedexsa_intranet.dbo.GL_ITEMSPAQ
                                WHERE        (cb = " + etiqueta + ") AND imedexsa_intranet.dbo.GL_ITEMSPAQ.paque_id IN (SELECT paque_id FROM imedexsa_intranet.dbo.GL_ITEMSREC)) AS ctd_a_fichar FROM TC_LANZA_ETIQUETA le JOIN T_ORDFAB ofb on le.OrdFab = ofb.ORDFAB WHERE (le.OrdFab = " + ofg + ") AND (le.CodEmp = '" + empresaGlobal.empresaID + "') AND (le.TIPOREG = 'F') ORDER BY Etiqueta";


                            sqlCmd.CommandText = strSql;
                            Console.WriteLine(" -> Query form4: " + strSql);

                            var cantidad = sqlCmd.ExecuteScalar();
                            if (cantidad == null) { Console.WriteLine(" -> Cantidad NULLLLLLLLLLLLLLLLl"); }


                            int cantidadInt = Convert.ToInt32(cantidad);

                            int cantidadIntroducida = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[2].Value);
                            /*MessageBox.Show(Form.ActiveForm, "cantidadInt: " + cantidadInt + " - canInt: " + cantidadIntroducida);
                            Console.WriteLine(" -> Cantidad contiene: " + cantidad);
                            Console.WriteLine(" -> Etiqueta contiene: " + etiqueta);
                            Console.WriteLine(" -> CantidadIntroducida contiene: " + cantidadIntroducida);*/
                            //Evitar que se introduzca una cantidad mayor a la disponible
                            if (cantidadIntroducida > cantidadInt)
                            {
                                MessageBox.Show(Form.ActiveForm, "El valor introducido es mayor al tamaño máximo.\r\n", "Valor Erróneo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }

                            //Se comprueba que no se introduzca un valor diferente a Integer
                            int intValue;
                            string tryInt = Convert.ToString(cantidadIntroducida);
                            if (!int.TryParse(tryInt, out intValue))
                            {
                                Console.WriteLine(" -> Valor introducido diferente a Integer");
                                MessageBox.Show(Form.ActiveForm, "El valor introducido diferente a Integer", "Valor Erróneo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }

                            //Se comprueba que no se introduzca el valor 0
                            if (cantidadIntroducida == 0)
                            {
                                Console.WriteLine(" -> Cantidad contiene");
                                MessageBox.Show(Form.ActiveForm, "El valor introducido es 0", "Valor Erróneo",
       MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }

                            //Se comprueba que no se introduzca valor -1
                            if (cantidadIntroducida < 0)
                            {
                                Console.WriteLine(" -> Cantidad contiene");
                                MessageBox.Show(Form.ActiveForm, "El valor introducido es negativo", "Valor Erróneo",
       MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }


                            //Avisar de que se introduzcan valores igual a 0 en la base de datos
                            /*if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[0].Value) == true)
                            {
                                if (cantidadIntroducida == 0)
                                {
                                    MessageBox.Show(Form.ActiveForm, "El valor introducido es 0.\r\n Introduzca un valor correcto", "Valor introducido 0",
               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    //transaction.Rollback();
                                    //return;
                                }
                            }*/

                            //se debe de comprobar aquí que el número de pedidos virtuales no supere el total de la cantidad de la etiqueta

                            //No se comprueba que existan varias etiquetas en ITEMSPAq
                            /*if (cantidad > 1)
                            {
                                MessageBox.Show(Form.ActiveForm, "Esta marca esta insertada en un paquete que contiene otras marcas.\r\n Si desea recepcionarlo debe deshacer primero el paquete", "Recepción Paquete",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }*/
                        }
                    }

                    if (!enc) {
                        Console.WriteLine(" -> No se ha marcado ningún checkbox");
                        MessageBox.Show(Form.ActiveForm, "No se ha seleccionado ninguna etiqueta", "Etiquetas no seleccionadas",
MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                        return;
                    }

                    #endregion



                    #region crear paquete y asignar piezas

                    strSql = "INSERT INTO imedexsa_intranet.dbo.GL_PAQUE (cb_pre, cb_num, estado, fch_creacion, dispositivo, empresa) ";
                    strSql += " 	Select 'PIN', MAX(cb_num) + 1, 'ACE', convert(datetime, '" + fecha_aux + "', 103), 'T-" + userName + "', empresa ";
                    strSql += " 		From imedexsa_intranet.dbo.GL_PAQUE ";
                    strSql += " 		Where (empresa = '" + codEmp + "') ";
                    strSql += " 		Group By empresa ";

                    sqlCmd.CommandText = strSql;
                    sqlCmd.ExecuteScalar();

                    //obtener el numero de paquete generado
                    strSql = "Select MAX(id) ";
                    strSql += "     From imedexsa_intranet.dbo.GL_PAQUE ";
                    strSql += "	    Where (empresa = '" + codEmp + "') ";

                    sqlCmd.CommandText = strSql;
                    Int32 numPaquete = Convert.ToInt32(sqlCmd.ExecuteScalar());



                    #region asignar marcas al paquete

                    for (int i = 0; i < this.dataGridView1.RowCount; i++)
                    {
                        if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[0].Value) == true)
                        {
                            strSql = "INSERT INTO imedexsa_intranet.dbo.GL_ITEMSPAQ (paque_id, cb, cantidad, fch_creacion, dispositivo) ";
                            strSql += "     values (" + numPaquete + ", " + this.dataGridView1.Rows[i].Cells[1].Value.ToString() + ", " + this.dataGridView1.Rows[i].Cells[2].Value.ToString() +
                                      " , getdate(), 'T-" + userName + "') ";

                            sqlCmd.CommandText = strSql;
                            sqlCmd.ExecuteScalar();
                        }
                    }

                    #endregion


                    #endregion



                    #region crear expedicion

                    strSql = "INSERT INTO imedexsa_intranet.dbo.GL_EXPED (cod_empresa, cb_num, dispositivo, fch_creacion, estado, destino, camion, fch_envio)";
                    strSql += "		Select cod_empresa, MAX(cb_num) + 1, 'T-" + userName + "', convert(datetime, '" + fecha_aux + "', 103), 1, 2, -1, getdate() ";
                    strSql += "			From imedexsa_intranet.dbo.GL_EXPED ";
                    strSql += "			Where (cod_empresa = '" + codEmp + "') ";
                    strSql += "			Group by cod_empresa ";

                    sqlCmd.CommandText = strSql;
                    sqlCmd.ExecuteScalar();


                    //obtener el numero de expedicion generado
                    strSql = "Select MAX(id) ";
                    strSql += "     From imedexsa_intranet.dbo.GL_EXPED ";
                    strSql += "	    Where (cod_empresa = '" + codEmp + "') ";

                    sqlCmd.CommandText = strSql;
                    Int32 numExped = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    //asociar el paquete creado a la expedicion
                    strSql = "INSERT INTO imedexsa_intranet.dbo.GL_ITEMSEXP (paque_id, exped_id, fch_creacion, dispositivo, cod_empresa) ";
                    strSql += " 	VALUES(" + numPaquete + ", " + numExped + ", getdate(), 'T-" + userName + "', '" + codEmp + "') ";

                    sqlCmd.CommandText = strSql;
                    sqlCmd.ExecuteScalar();

                    #endregion


                    #region crear recepcion

                    strSql = "INSERT INTO imedexsa_intranet.dbo.GL_RECEP (exped_id, dispositivo, fch_creacion, estado, origen, camion, cod_empresa, fch_recepcion, albaran_camion, lin_albaran_camion) ";
                    strSql += "		VALUES (" + numExped + ", 'T-" + userName + "', convert(datetime, '" + fecha_aux + "', 103), 'ABI', 2, -1, '" + codEmp + "', GETDATE(), -1, 1) ";

                    sqlCmd.CommandText = strSql;
                    sqlCmd.ExecuteScalar();


                    //obtener el numero de recepcion generado
                    strSql = "Select MAX(id) ";
                    strSql += "     From imedexsa_intranet.dbo.GL_RECEP ";
                    strSql += "	    Where (cod_empresa = '" + codEmp + "') ";

                    sqlCmd.CommandText = strSql;
                    Int32 numRecep = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    //asociar el paquete creado a la recepcion.
                    strSql = "INSERT INTO imedexsa_intranet.dbo.GL_ITEMSREC (paque_id, recep_id, fch_creacion, dispositivo) ";
                    strSql += " 	VALUES(" + numPaquete + ", " + numRecep + ", getdate(), 'T-" + userName + "') ";

                    sqlCmd.CommandText = strSql;
                    sqlCmd.ExecuteScalar();

                    #endregion


                    #region cerrar recepcion

                    strSql = "UPDATE imedexsa_intranet.dbo.GL_RECEP ";
                    strSql += "	SET estado = 'FIN' ";
                    strSql += "	WHERE (id = " + numRecep + ") AND (cod_empresa = '" + codEmp + "') ";

                    sqlCmd.CommandText = strSql;
                    sqlCmd.ExecuteScalar();

                    #endregion

                    transaction.Commit();

                    MessageBox.Show(Form.ActiveForm, "Recepción generada correctamente");
                    this.DialogResult = DialogResult.OK;

                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(Form.ActiveForm, "Ocurrio un error al realizar la recepción: " + ex.ToString());
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }


        }

    }
}