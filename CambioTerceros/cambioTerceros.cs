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
using System.Globalization;
using System.Threading.Tasks;
using empresaGlobalProj;
using System.Text.RegularExpressions;

namespace CambioTerceros
{
    public partial class cambioTerceros : Form
    {

        string strsql;
        SqlConnection conexion = null;
        string connString = ConfigurationManager.AppSettings["ggConnectionString"].ToString();// @"Data Source=srvdesarrollo;Initial Catalog=gg;User ID=gg;Password=ostia"; //ConfigurationManager.ConnectionStrings["Interface_Usuario.Properties.Settings.CS_CPP_0713ConnectionString"].ConnectionString;
        string cfsf = null;

        public cambioTerceros()
        {
            InitializeComponent();
            empresaGlobal.showEmp = false;
            comboBox1.Items.Add("CF");
            comboBox1.Items.Add("SF");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cfsf = comboBox1.SelectedItem.ToString();
            refreshPedidos(cfsf);
            refreshProvClient(cfsf);
        }


        public void refreshPedidos(string cfsf)
        {
            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlCommand cmd = new SqlCommand("select Distinct NUMPED, CONCAT(Numped,' - ', TERCERO,' - ',RTERCERO) as col from T_ORDTER where TIPOREG = '" + cfsf + "' AND CodEMP = '" + empresaGlobal.empresaID + "' ORDER BY NUMPED DESC;", conexion); //AND NUMPED > 2010000 AND NUMPED < 3000000
            //cmd.Parameters.AddWithValue("CFSF", cfsf);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conexion.Close();

            //El valor
            comboBox2.ValueMember = "NUMPED";
            //Lo que se muestra en el combo
            comboBox2.DisplayMember = "col";
            comboBox2.DataSource = dt;

            //string da = comboBoxDC.Text.ToString();
            //string x = comboBox2.SelectedItem.ToString();
            /*numPed = comboBox2.SelectedItem.ToString(); //.Text.ToString();
            Console.WriteLine("Se muestra numPed: " + numPed);
            //cleanStr = Regex.Match(cleanStr, "[0-9]{2}[Tt][0-9]+").Value;
            string cleanNumPed = Regex.Match(numPed, "^([\\S]+)").Value;
            numPed = cleanNumPed;*/

            //([-])  ^([\S]+) ([^\'s]+)
            //Console.WriteLine("Se muestra numPed:" + numPed + "y clean: " + cleanNumPed);
        }

        public void refreshProvClient(string cfsf)
        {
            
            if (cfsf == "CF")
            {
                conexion = new SqlConnection(connString);
                conexion.Open();

                SqlCommand cmd = new SqlCommand("select Distinct CODCLI, CONCAT(CODCLI, ' - ',NOMBRE) as codcliName from T_CLIENTES WHERE CodEMP = '" + empresaGlobal.empresaID + "' ORDER BY CODCLI DESC;", conexion);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conexion.Close();

                comboBox3.ValueMember = "codcliName";
                comboBox3.DisplayMember = "codcliName";
                comboBox3.DataSource = dt;

                /*provClient = comboBox3.SelectedItem.ToString();
                String str = ((DataRowView)comboBox3.SelectedItem)["CODCLI"].ToString();
                //provClient = comboBox3.Text.ToString();
                cleanProvClient = Regex.Match(provClient, "^([\\S]+)").Value;
                provClient = cleanProvClient;
                Console.WriteLine("provClient contiene: " + provClient + " y str contiene " + str);*/

            }
            else
            {
                conexion = new SqlConnection(connString);
                conexion.Open();
                SqlCommand cmd = new SqlCommand("select Distinct CODCLI, CONCAT(CODCLI, ' - ',NOMBRE) as codcliName from T_PROV WHERE CodEMP = '" + empresaGlobal.empresaID + "' ORDER BY CODCLI DESC;", conexion);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conexion.Close();

                comboBox3.ValueMember = "codcliName";
                comboBox3.DisplayMember = "codcliName";
                comboBox3.DataSource = dt;

                // var sd = comboBox3.SelectedItem.ToString();
                //provClient = comboBox3.Text.ToString();
                /*cleanProvClient = Regex.Match(provClient, "^([\\S]+)").Value;
                provClient = cleanProvClient;
                String str = ((DataRowView)comboBox3.SelectedItem)["CODCLI"].ToString();
                Console.WriteLine("provClient contiene: " + provClient + " y " + str);*/
                //C018 - BANCO BILBAO VIZCAYA
            }


        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var c1 = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            var c2numPed = comboBox2.Text;
            var c3provClient = comboBox3.GetItemText(this.comboBox3.SelectedItem);

            string cleanNumPed = Regex.Match(c2numPed, "^([\\S]+)").Value;
            c2numPed = cleanNumPed;

            string cleanProvClient = Regex.Match(c3provClient, "^([\\S]+)").Value;
            c3provClient = cleanProvClient;

            if (c1 == null || c2numPed == null || c3provClient == null)
            {
                MessageBox.Show("Has introducidos valores nulos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            conexion = new SqlConnection(connString);
            conexion.Open();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    strsql = "[dbo].[Cambiar_TerceroPedido]";
                    Console.WriteLine("La consulta que se va a ejecutar es: " + strsql);
                    SqlCommand cmd = new SqlCommand(strsql, conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodEmp", empresaGlobal.empresaID));
                    cmd.Parameters.Add(new SqlParameter("@TipoReg", c1));
                    cmd.Parameters.Add(new SqlParameter("@Pedido", c2numPed));
                    cmd.Parameters.Add(new SqlParameter("@NuevoCliente", c3provClient));
                    cmd.Parameters.Add(new SqlParameter("@NuevoClienteEntrega", c3provClient));
                    cmd.Parameters.Add(new SqlParameter("@NuevoClienteFactura", c3provClient));
                    cmd.Parameters.Add(new SqlParameter("@NuevoClientePagoCobro", c3provClient));
                    cmd.Parameters.Add(new SqlParameter("@Autorizador", "Jose"));
                    cmd.Parameters.Add("@error", SqlDbType.Char, 500);
                    cmd.Parameters["@error"].Direction = ParameterDirection.Output;
                    //strsql = "exec [dbo].[Cambiar_TerceroPedido_copy] '" + empresaGlobal.empresaID + "','" + c1 + "','" + c2numPed + "','" + c3provClient + "','" + c3provClient + "','" + c3provClient + "','" + c3provClient + "', 'JoseM'," + "@error";
                    
                    int asd = cmd.ExecuteNonQuery();

                    var message = cmd.Parameters["@error"].Value.ToString(); 
                    //SqlCommand cmd = new SqlCommand("[dbo].[Cambiar_TerceroPedido_copy]", conn);

                    // 2. set the command object so it knows to execute a stored procedure
                    //cmd.CommandType = CommandType.StoredProcedure;

                    /*int val = Convert.ToInt32(numPed);
                    Console.WriteLine("Val contiene: " + val + " y numPed: " + numPed);

                    // 3. add parameter to command, which will be passed to the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@CodEmp", empresaGlobal.empresaID));
                    cmd.Parameters.Add(new SqlParameter("@TipoReg", cfsf));
                    cmd.Parameters.Add(new SqlParameter("@Pedido", Convert.ToInt32(numPed)));
                    cmd.Parameters.Add(new SqlParameter("@NuevoCliente", provClient));
                    cmd.Parameters.Add(new SqlParameter("@NuevoClienteEntrega", provClient));
                    cmd.Parameters.Add(new SqlParameter("@NuevoClienteFactura", provClient));
                    cmd.Parameters.Add(new SqlParameter("@NuevoClientePagoCobro", provClient));
                    cmd.Parameters.Add(new SqlParameter("@Autorizador", "Jose"));

                    Console.WriteLine("Consulta es: " + cmd.ToString());
                    // execute the command
                    int asd = cmd.ExecuteNonQuery();*/
                    /*using (SqlDataReader rdr = cmd.ExecuteReader()) {
                        // iterate through results, printing each to console
                        while (rdr.Read())
                        {
                            Console.WriteLine("Resultados de la ejecución "); //+ rdr["ProductName"],rdr["Total"]);
                        }
                    }*/
                    if (message != "")
                    {
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("La ejecución se ha realizado con éxito", "Ejecución finalizada", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Form.ActiveForm, "Problemas al realizar la operación SQL: " + ex.Message.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cambioTerceros_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }

    }
}
