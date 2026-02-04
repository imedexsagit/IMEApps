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
    public partial class Form32 : Form
    {        
        private Int32 idTrabajo;
        private Int32 OrdFab;
        private Int32 MarcaCodigo;


        public Form32(Int32 id_Trabajo, Int32 of, Int32 etiqueta)
        {
            InitializeComponent();

            this.idTrabajo = id_Trabajo;
            this.OrdFab = of;
            this.MarcaCodigo = etiqueta;

            this.DialogResult = DialogResult.Cancel;
        }

        private void Form32_Load(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString());            

            String strSql = "";
            SqlCommand sqlCmd;
            SqlDataReader sqlDR;

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                    
                strSql += "SELECT CS_CPP_0713.dbo.MP_CARGADEPIEZAS.marcaCodigo as Etiqueta, CS_CPP_0713.dbo.MP_CARGADEPIEZAS.marcaCantidad, CS_CPP_0713.dbo.MP_CARGADEPIEZAS.id ";
                strSql += "	FROM TC_IMPORT_BONOS INNER JOIN CS_CPP_0713.dbo.MP_CARGADEPIEZAS ";
                strSql += "			ON TC_IMPORT_BONOS.id_mp_cargadepiezas = CS_CPP_0713.dbo.MP_CARGADEPIEZAS.id AND TC_IMPORT_BONOS.MjTrabajoConBonoId = CS_CPP_0713.dbo.MP_CARGADEPIEZAS.mjTrabajoConBonoId ";
                //JRM - Cambiar Empresa cambio ConfigurationManager.AppSettings["EMPRESA"].ToString(); por empresaGlobal.empresaID
                strSql += "	WHERE (TC_IMPORT_BONOS.CODEMP = '" + empresaGlobal.empresaID + "') ";
                strSql += "			AND (TC_IMPORT_BONOS.MjTrabajoConBonoId = " + this.idTrabajo + ") ";
                strSql += "			AND (TC_IMPORT_BONOS.ORDFAB_int = " + this.OrdFab + ") ";
                strSql += "			AND (CS_CPP_0713.dbo.MP_CARGADEPIEZAS.marcaCodigo = " + this.MarcaCodigo + ") ";
                strSql += "			AND (TC_IMPORT_BONOS.PBUENAS_float <> 0) ";
                

                sqlCmd = new SqlCommand(strSql, conexion);
                sqlDR = sqlCmd.ExecuteReader();

                ListViewItem item;

                while (sqlDR.Read())
                {
                    item = new ListViewItem();

                    item.SubItems.Add(sqlDR[0].ToString());
                    item.SubItems.Add(sqlDR[1].ToString());
                    item.SubItems.Add(sqlDR[2].ToString());                    

                    listView1.Items.Add(item);
                }

                sqlDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, "Ocurrió un error al obtener las cargas de las piezas: " + ex.ToString());
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (Int32 i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Checked == true)
                {
                    SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString() );

                    String strSql = "";
                    SqlCommand sqlCmd;

                    try
                    {
                        int id_carga = Convert.ToInt32(listView1.Items[i].SubItems[3].Text);

                        if (conexion.State != ConnectionState.Open)
                            conexion.Open();

                        //Eliminar la carga de piezas
                        strSql = "Delete ";
                        strSql += "		From CS_CPP_0713.dbo.MP_CARGADEPIEZAS ";
                        strSql += "		Where (id = " + id_carga + ")	";

                        sqlCmd = new SqlCommand(strSql, conexion);
                        sqlCmd.CommandTimeout = 600;
                        sqlCmd.ExecuteScalar();

                        MessageBox.Show(Form.ActiveForm, "Borrado realizado correctamente");                        
                        this.DialogResult = DialogResult.OK;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Form.ActiveForm, "Ocurrió un error al eliminar la carga de piezas: " + ex.ToString());
                        this.DialogResult = DialogResult.Cancel;
                    }
                    finally
                    {
                        if (conexion.State == ConnectionState.Open)
                            conexion.Close();
                    }                                        
                }
            }

            this.Close();

            Cursor.Current = Cursors.Default;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}