using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InspeccionesTablets
{
    public partial class Form2 : Form
    {
        #region Atributos de la clase
        private String CodEmp;
        private String CadenaConexion;
        private String PDA;
        private String Usuario;       
        #endregion

        
        
        public Form2(String codemp,String cadenaconexion,String pda)
        {
            InitializeComponent();
            this.CodEmp = codemp;
            this.CadenaConexion = cadenaconexion;
            this.PDA = pda;
            this.Usuario = Environment.UserName;

            CargarProyectos();
            textBoxObservaciones.Enabled = false;
            buttonRevisado.Enabled = false;            
            CargarHistorico();

            Cursor.Current = Cursors.Arrow;
        }



        private void CargarProyectos() 
        {
            SqlConnection conn = new SqlConnection();

            try
            {
                String strSql = " ";
                SqlCommand sqlCmd;
                SqlDataReader sqlRdr;
                DataTable tabla = new DataTable();
                tabla.Columns.Add("ProyectoMostrar");
                tabla.Columns.Add("ProyectoValor");

                conn.ConnectionString = this.CadenaConexion;
                conn.Open();
                                
                strSql = strSql + " select	CODLANZA,PROYECTO";
                strSql = strSql + " from	TC_LANZA";
                strSql = strSql + " where	CODEMP = '" + CodEmp + "' and VALIDADO = 'S' and CODLANZA not in (select PROYECTO from TC_InspeccionProyectos where CODEMP = '" + CodEmp + "')";
                strSql = strSql + " order by CODLANZA desc";

                sqlCmd = new SqlCommand(strSql, conn);
                sqlRdr = sqlCmd.ExecuteReader();

                while (sqlRdr.Read()){                    
                    tabla.Rows.Add(sqlRdr.GetValue(0).ToString() + " - " + sqlRdr.GetValue(1).ToString(), sqlRdr.GetValue(0).ToString());
                }
               
                sqlRdr.Close();
                sqlRdr.Dispose();
                
                comboBoxProyectos.DataSource = null;
                comboBoxProyectos.DataSource = tabla;
                comboBoxProyectos.DisplayMember = "ProyectoMostrar";
                comboBoxProyectos.ValueMember = "ProyectoValor";
                comboBoxProyectos.SelectedIndex = -1;                                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Consulte con el Departamento TI: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);                
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }



        private void CargarHistorico()
        {
            SqlConnection conn = new SqlConnection();

            try
            {
                String strSql = " ";
                SqlCommand sqlCmd;
                SqlDataReader sqlRdr;                
                
                conn.ConnectionString = this.CadenaConexion;
                conn.Open();

                strSql = strSql + " select	PROYECTO,(SELECT PROYECTO FROM TC_LANZA WHERE TC_LANZA.CODEMP = '" + CodEmp + "' AND TC_LANZA.CODLANZA = TC_INSPECCIONPROYECTOS.PROYECTO),FECHAC,USUCRE,OBSERVACIONES";
                strSql = strSql + " from	TC_INSPECCIONPROYECTOS";
                strSql = strSql + " where	CODEMP = '" + CodEmp + "'";
                strSql = strSql + " order by PROYECTO desc";

                sqlCmd = new SqlCommand(strSql, conn);
                sqlRdr = sqlCmd.ExecuteReader();

                listViewHistorico.Items.Clear();

                while (sqlRdr.Read())
                {
                    ListViewItem lista = new ListViewItem(sqlRdr.GetValue(0).ToString());
                    lista.SubItems.Add(sqlRdr.GetValue(1).ToString());
                    lista.SubItems.Add(sqlRdr.GetValue(2).ToString());
                    lista.SubItems.Add(sqlRdr.GetValue(3).ToString());
                    lista.SubItems.Add(sqlRdr.GetValue(4).ToString());
                    listViewHistorico.Items.Add(lista);                    
                }

                sqlRdr.Close();
                sqlRdr.Dispose();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Consulte con el Departamento TI: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);                
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }


      
        private void comboBoxProyectos_Leave(object sender, EventArgs e)
        {
            if (comboBoxProyectos.SelectedValue == null)
            {
                textBoxObservaciones.Enabled = false;
                buttonRevisado.Enabled = false;
            }
            else
            {
                textBoxObservaciones.Enabled = true;
                buttonRevisado.Enabled = true;
            }      
        }



        private void comboBoxProyectos_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxProyectos.SelectedValue == null)
            {
                textBoxObservaciones.Enabled = false;
                buttonRevisado.Enabled = false;
            }
            else
            {
                textBoxObservaciones.Enabled = true;
                buttonRevisado.Enabled = true;
            }      
        }



        private void buttonRevisado_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                String strSql = " ";
                SqlCommand sqlCmd;
                SqlDataReader sqlRdr;
               
                conn.ConnectionString = this.CadenaConexion;
                conn.Open();

                strSql = strSql + " insert into TC_InspeccionProyectos(CODEMP,PROYECTO,OBSERVACIONES,FECHAC,USUCRE)";
                strSql = strSql + " values('" + CodEmp + "',";
                strSql = strSql + "         " + comboBoxProyectos.SelectedValue.ToString() + ",";
                if(textBoxObservaciones.Text == null || textBoxObservaciones.Text == String.Empty)
                    strSql = strSql + "      NULL,";
                else
                    strSql = strSql + "    '" + textBoxObservaciones.Text + "',";
                strSql = strSql + "        getdate(),";
                strSql = strSql + "        '" + Usuario + " / " + PDA + "')";
                
                sqlCmd = new SqlCommand(strSql, conn);
                sqlRdr = sqlCmd.ExecuteReader();
                
                sqlRdr.Close();
                sqlRdr.Dispose();

                CargarProyectos();
                textBoxObservaciones.Text = "";
                textBoxObservaciones.Enabled = false;
                buttonRevisado.Enabled = false;
                CargarHistorico();
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("Registro realizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("Error. Consulte con el Departamento TI: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);                
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        

    }
}
