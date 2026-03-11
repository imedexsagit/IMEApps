using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;
using empresaGlobalProj;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics; 


namespace GestionSoldadura
{
    public partial class Form1 : Form
    {
        //variables del formulario

        public static string proyecto;
        int TotalCantidad = 0;
        int TotalMT = 0;
        int TotalVT = 0;
        int TotalUT = 0;
        int TotalDM = 0;
        int TotalRX = 0;
        int TotalMarcaUT = 0;
        int TotalMarcaMT = 0;
        Boolean finalizado, informe, mt,vt,ut;
        public string Empresa { get; private set; }
        public string Usuario { get; private set; }
        private DataGridView.HitTestInfo hitTestInfo;
        public string valorCliente = "";
        public string valorPedido = "";
        private string connString = "Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Carlos sanchez 21/02/2024
            //Primero desavilitamos el menu. contextMenuStrip1
            //contextMenuStrip1.Enabled = false;
            contextMenuStrip1.Items[0].Visible = true;
            contextMenuStrip1.Items[1].Visible = false;
            contextMenuStrip1.Items[2].Visible = false;
            contextMenuStrip2.Enabled = false;

            //Obtenemos las personas con permiso de escritura y habilitamos


            if (Environment.UserName == "francisco.rodrigo" || Environment.UserName == "carlos.casquero" || Environment.UserName == "calidad.soldadura")
            {
                button4.Visible = true;
            }
           



            SqlConnection conexin = null;
            SqlCommand comand = null;
            string strsq;
            string user = Environment.UserName;

            DataTable table = new DataTable();

            conexin = new SqlConnection(connString);
            conexin.Open();

            SqlDataAdapter adapter;

            strsq = " select usuario from TC_IMEAPPS_PERMISOS where APLICACION='GestionSoldaduraTotal' and codemp='" + empresaGlobal.EmpresaID + "' ";

            comand = new SqlCommand(strsq, conexin);
            adapter = new SqlDataAdapter(comand);

            comand.CommandText = strsq;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);
            
            conexin.Close();

            foreach (DataRow row in table.Rows)
            {

                string usu = Convert.ToString(row["usuario"]);

                if (usu == user)
                {
                    //Permito las cosas
                    contextMenuStrip1.Enabled = true;
                    contextMenuStrip2.Enabled = true;

                    button1.Visible = true;

                    btn_excelMT.Visible = true;
                    btn_ExcelUT.Visible = true;
                    btn_excelVT.Visible = true;

                    ensayosMT.Enabled = true;
                    ensayosDM.Enabled = true;
                    ensayosRX.Enabled = true;
                    ensayosVT.Enabled = true;
                    ensayosUT.Enabled = true;

                    richTextBox1.Enabled = true;
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                    checkBox3.Enabled = true;
                    checkBox4.Enabled = true;
                    checkBox5.Enabled = true;

                    contextMenuStrip1.Items[1].Visible = true;
                    contextMenuStrip1.Items[2].Visible = true;
                }
            }


                // visualizar inspecciones

                conexin.Open();
                strsq = " select usuario from TC_IMEAPPS_PERMISOS where (APLICACION='GestionSoldaduraInspectores' or APLICACION='GestionSoldaduraTotal') and codemp='" + empresaGlobal.EmpresaID + "' ";

                comand = new SqlCommand(strsq, conexin);
                adapter = new SqlDataAdapter(comand);

                comand.CommandText = strsq;
                table.Columns.Clear();
                table.Clear();
                adapter.Fill(table);


                conexin.Close();

                foreach (DataRow row2 in table.Rows)
                {

                    string usu2 = Convert.ToString(row2["usuario"]);
                    if (usu2 == user)
                    {
                        button3.Visible = true;
                        btn_excelMT.Visible = true;
                        btn_ExcelUT.Visible = true;
                        btn_excelVT.Visible = true;
                        contextMenuStrip1.Items[0].Enabled = true;
                        richTextBox1.Enabled = true;

                        checkBox2.Enabled = true;
                        checkBox3.Enabled = true;
                        checkBox4.Enabled = true;
                        checkBox5.Enabled = true;
                        button1.Visible = true;
                    }
                }


                Cursor.Current = Cursors.WaitCursor;
                contextMenuStrip1.Items[0].Enabled = true;
                ensayosMT.Text = "";
                ensayosVT.Text = "";
                ensayosUT.Text = "";
                ensayosDM.Text = "";
                ensayosRX.Text = "";

                labelMT.Text = "";
                labelVT.Text = "";
                labelUT.Text = "";
                labelDM.Text = "";
                labelRX.Text = "";

                faltanMT.Text = "";
                faltanVT.Text = "";
                faltanUT.Text = "";
                faltanDM.Text = "";
                faltanRX.Text = "";

                SqlConnection conexion = null;
                SqlCommand comando = null;
                string strsql;


                // string connString = "Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";
                //DataTable table = new DataTable();

                conexion = new SqlConnection(connString);
                conexion.Open();

                //SqlDataAdapter adapter;

                strsql = "SELECT CODLANZA,CAST(CODLANZA AS Varchar(5))+'      '+ PROYECTO AS PROYECTO FROM TC_LANZA WHERE (CODEMP = '3') and VALIDADO = 'S' ORDER BY CODLANZA DESC";

                comando = new SqlCommand(strsql, conexion);
                
                adapter = new SqlDataAdapter(comando);

                comando.CommandText = strsql;
                table.Columns.Clear();
                table.Clear();
                adapter.Fill(table);

                btn_ExcelUT.Enabled = false;
                btn_excelVT.Enabled = false;
                btn_excelMT.Enabled = false;

                conexion.Close();

                foreach (DataRow row3 in table.Rows)
                {
                    comboProyectos.Items.Add(row3["PROYECTO"]);
                }

                Cursor.Current = Cursors.Default;
            
        }
        private void comboProyectos_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Restauramos los valores cada vez que se seleciona un nuevo proyecto
            Empresa = empresaGlobal.empresaID;//"3";
            Usuario = Environment.UserName;
            TotalCantidad = 0;
            TotalMT = 0;
            TotalVT = 0;
            TotalUT = 0;
            TotalDM = 0;
            TotalRX = 0;
            TotalMarcaUT = 0;
            TotalMarcaMT = 0;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            string valor = comboProyectos.Text;
            string[] partes = valor.Split(' ');
            proyecto = partes[0];

            btn_ExcelUT.Enabled = true;
            btn_excelVT.Enabled = true;
            btn_excelMT.Enabled = true;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;


            SqlConnection conexion = null;
            SqlCommand comando = null;
            string strsql;

            DataTable table = new DataTable();
            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;

            strsql = " SELECT DISTINCT TC_LANZA_PEDLIN.NUMPED, T_ORDTER.RTERCERO  FROM  TC_LANZA_PEDLIN INNER JOIN T_ORDTER ";
            strsql = strsql + "ON TC_LANZA_PEDLIN.NUMPED = T_ORDTER.NUMPED  WHERE ";
            strsql = strsql + "TC_LANZA_PEDLIN.CODEMP ='" + Empresa + "'  AND TC_LANZA_PEDLIN.CODLANZA = " + proyecto + "   ORDER BY TC_LANZA_PEDLIN.NUMPED;";

            comando = new SqlCommand(strsql, conexion);
            adapter = new SqlDataAdapter(comando);
            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            conexion.Close();


            //string valorCliente = "";

            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToInt32(row["NUMPED"]) != 0)
                {
                    listBox1.Items.Add(row["NUMPED"]);
                   
                }
                valorCliente = Convert.ToString(row["RTERCERO"]);

                // int UT = Convert.ToInt32(row["UT"]);
                // if (UT > 0)
                // {
                // }
            }

            listBox2.Items.Add(valorCliente);//Se muestra el cliente al final, ya que siempre es el mismo

            if (listBox1.Items.Count > 0)
            {
                valorPedido = listBox1.Items[0].ToString();
            }

            // Se muestra los datos del proyecto en el DataGridView
            SqlConnection conexion1 = null;
            SqlCommand comando1 = null;
            string strsql1;
            Int32 estadoOF = 2; // Todas
            DataTable table1 = new DataTable();
            conexion1 = new SqlConnection(connString);
            conexion1.Open();

            //Se marca por defecto todas, ya que en principio no es necesario que selecione abiertas o cerradas, pero se deja proparado por si lo necesita en un futuro
            

            strsql1 = "EXEC gestionSoldadura '" + Empresa + "','" + proyecto + "','" + Usuario + "','" + 2 + "','" + valorCliente + "'";
            comando1 = new SqlCommand(strsql1, conexion1);
            adapter = new SqlDataAdapter(comando1);
            comando1.CommandText = strsql1;
            table1.Columns.Clear();
            table1.Clear();
            adapter.Fill(table1);
            dataGridView2.DataSource = table1;
            this.dataGridView2.Columns[0].Visible = false; // NO visulizamos cliente
            this.dataGridView2.Columns[2].Visible = false; // NO visulizamos ORDFAB
            this.dataGridView2.Columns[10].Visible = false; // No visulizamos marcarUT
            this.dataGridView2.Columns[11].Visible = false; // No visulizamos marcarMT
            dataGridView2.AllowUserToAddRows = false;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            { // Ponemos en amarillo los codigos que tengan marcadosUT y calculamos Totales
                try
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();

                    style.Font = new Font(dataGridView2.Font, FontStyle.Bold);

                    
                    // aqui pongo en verde si el valor es positivo

                    if (Convert.ToInt32(row.Cells[4].Value.ToString()) > 0)
                    {

                        row.Cells[4].Style = style;
                        row.Cells[4].Style.BackColor = Color.GreenYellow;
                        
                    }
                    if (Convert.ToInt32(row.Cells[5].Value.ToString()) > 0)
                    {

                        row.Cells[5].Style = style;
                        row.Cells[5].Style.BackColor = Color.GreenYellow;
                       
                    }
                    if (Convert.ToInt32(row.Cells[6].Value.ToString()) > 0)
                    {

                        row.Cells[6].Style = style;
                        row.Cells[6].Style.BackColor = Color.GreenYellow;
                        
                    }
                    if (Convert.ToInt32(row.Cells[7].Value.ToString()) > 0)
                    {

                        row.Cells[7].Style = style;
                        row.Cells[7].Style.BackColor = Color.GreenYellow;
                        
                    }
                    if (Convert.ToInt32(row.Cells[8].Value.ToString()) > 0)
                    {

                        row.Cells[8].Style = style;
                        row.Cells[8].Style.BackColor = Color.GreenYellow;
                       
                    }
                    if (Convert.ToInt32(row.Cells[9].Value.ToString()) > 0)
                    {

                        row.Cells[9].Style = style;
                        row.Cells[9].Style.BackColor = Color.GreenYellow;
                        
                    }

                    TotalCantidad = TotalCantidad + Int32.Parse(row.Cells[4].Value.ToString());
                    TotalMT = TotalMT + Int32.Parse(row.Cells[5].Value.ToString());
                    TotalVT = TotalVT + Int32.Parse(row.Cells[6].Value.ToString());
                    TotalUT = TotalUT + Int32.Parse(row.Cells[7].Value.ToString());
                    TotalDM = TotalDM + Int32.Parse(row.Cells[8].Value.ToString());
                    TotalRX = TotalRX + Int32.Parse(row.Cells[9].Value.ToString());
                    
                    if(Boolean.Parse(row.Cells[10].Value.ToString()) == true)
                    TotalMarcaUT = TotalMarcaUT + Convert.ToInt32(row.Cells[4].Value);

                    if (Boolean.Parse(row.Cells[11].Value.ToString()) == true)
                        TotalMarcaMT = TotalMarcaMT + Convert.ToInt32(row.Cells[4].Value);

                    if (row.Cells[10].Value.Equals(true))
                    {
                        row.Cells[1].Style.BackColor = Color.Yellow;
                    }

                    if (row.Cells[11].Value.Equals(true))
                    {
                        row.Cells[1].Style.BackColor = Color.Orange;
                    }

                    if (row.Cells[11].Value.Equals(true) && row.Cells[10].Value.Equals(true))
                    {
                        row.Cells[1].Style.BackColor = Color.Red;
                    }

                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("Error: {0}", outOfRange.Message);
                }
            }

            Totales.Text = "Totales del proyecto " + proyecto;
            textBoxCantidad.Text = Convert.ToString(TotalCantidad);
            textBoxMT.Text = Convert.ToString(TotalMT);
            textBoxVT.Text = Convert.ToString(TotalVT);
            textBoxUT.Text = Convert.ToString(TotalUT);
            textBoxDM.Text = Convert.ToString(TotalDM);
            textBoxRX.Text = Convert.ToString(TotalRX);
            textBoxMarcaUT.Text = Convert.ToString(TotalMarcaUT);
            textBoxMarcaMT.Text = Convert.ToString(TotalMarcaMT);

            labelMT.Text = Convert.ToString(TotalMT);
            labelVT.Text = Convert.ToString(TotalVT);
            labelUT.Text = Convert.ToString(TotalUT);
            labelDM.Text = Convert.ToString(TotalDM);
            labelRX.Text = Convert.ToString(TotalRX);

            conexion1.Close();

            //Obtenemos los ensayos del proyecto
            SqlConnection conexion2 = null;
            SqlCommand comando2 = null;
            string strsql2;
            DataTable table2 = new DataTable();
            conexion2 = new SqlConnection(connString);
            conexion2.Open();
            SqlDataAdapter adapter2;

            strsql2 = "SELECT MTEnsayo, VTEnsayo, UTEnsayo, DMEnsayo , RXEnsayo, finalizado, observacion  FROM  TC_SoldaduraProyecto WHERE proyecto = " + proyecto;

            comando2 = new SqlCommand(strsql2, conexion2);
            adapter2 = new SqlDataAdapter(comando2);
            comando2.CommandText = strsql2;
            table2.Columns.Clear();
            table2.Clear();
            adapter2.Fill(table2);

            conexion.Close();

            for (int i = 0; i < table2.Rows.Count; i++) //Mostramos los ensayos
            {
                ensayosMT.Text = table2.Rows[i]["MTEnsayo"].ToString();
                ensayosVT.Text = table2.Rows[i]["VTEnsayo"].ToString();
                ensayosUT.Text = table2.Rows[i]["UTEnsayo"].ToString();
                ensayosDM.Text = table2.Rows[i]["DMEnsayo"].ToString();
                ensayosRX.Text = table2.Rows[i]["RXEnsayo"].ToString();
                finalizado = Convert.ToBoolean(table2.Rows[i]["finalizado"]);
                checkBox1.Checked = finalizado;
                richTextBox1.Text = table2.Rows[i]["observacion"].ToString();
            }

            calculaRestantes();
            label27.Text = obtenerPlanCalidad(proyecto);

            //Obtener los datos de los informes
            
            SqlConnection conexion3 = null;
            SqlCommand comando3 = null;
            string strsql3;
            DataTable table3 = new DataTable();
            conexion3 = new SqlConnection(connString);
            conexion3.Open();
            SqlDataAdapter adapter3;

            strsql3 = "SELECT informes,mt,vt,ut  FROM  TC_SoldaduraProyecto_DatosInformes WHERE proyecto = " + proyecto + " and codEmp=" + Empresa;

            comando3 = new SqlCommand(strsql3, conexion3);
            adapter3 = new SqlDataAdapter(comando3);
            comando3.CommandText = strsql3;
            table3.Columns.Clear();
            table3.Clear();
            adapter3.Fill(table3);

            conexion3.Close();

            for (int i = 0; i < table3.Rows.Count; i++) //Mostramos los datos de los informes
            {
               bool informe = Convert.ToBoolean(table3.Rows[i]["informes"]);
               checkBox2.Checked = informe;

               bool mt = Convert.ToBoolean(table3.Rows[i]["mt"]);
               checkBox3.Checked = mt;

               bool vt = Convert.ToBoolean(table3.Rows[i]["vt"]);
               checkBox4.Checked = vt;

               bool ut = Convert.ToBoolean(table3.Rows[i]["ut"]);
               checkBox5.Checked = ut;
            }
        }
        // CARLOS CASQUERO 04/03/2026 - Se añade esta ejecución para que puedan buscar proyectos directamente escribiendo el número y pulsando Enter
        private void comboProyectos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                // Restauramos los valores cada vez que se seleciona un nuevo proyecto
                Empresa = empresaGlobal.empresaID;//"3";
                Usuario = Environment.UserName;
                TotalCantidad = 0;
                TotalMT = 0;
                TotalVT = 0;
                TotalUT = 0;
                TotalDM = 0;
                TotalRX = 0;
                TotalMarcaUT = 0;
                TotalMarcaMT = 0;
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                string valor = comboProyectos.Text;
                proyecto = valor;

                btn_ExcelUT.Enabled = true;
                btn_excelVT.Enabled = true;
                btn_excelMT.Enabled = true;

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;


                SqlConnection conexion = null;
                SqlCommand comando = null;
                string strsql;

                strsql = " SELECT PROYECTO FROM TC_LANZA WHERE CODEMP='"+ Empresa+"' AND CODLANZA="+proyecto;
                conexion = new SqlConnection(connString);
                conexion.Open();
                comando = new SqlCommand(strsql, conexion);
                string nombreProyecto = comando.ExecuteScalar().ToString(); 

                comboProyectos.Text = proyecto + " " +nombreProyecto;

                conexion.Close(); 

                DataTable table = new DataTable();
                conexion = new SqlConnection(connString);
                conexion.Open();

                strsql = " SELECT DISTINCT TC_LANZA_PEDLIN.NUMPED, T_ORDTER.RTERCERO  FROM  TC_LANZA_PEDLIN INNER JOIN T_ORDTER ";
                strsql = strsql + "ON TC_LANZA_PEDLIN.NUMPED = T_ORDTER.NUMPED  WHERE ";
                strsql = strsql + "TC_LANZA_PEDLIN.CODEMP ='" + Empresa + "'  AND TC_LANZA_PEDLIN.CODLANZA = " + proyecto + "   ORDER BY TC_LANZA_PEDLIN.NUMPED;";

                comando = new SqlCommand(strsql, conexion);
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(comando);
                comando.CommandText = strsql;
                table.Columns.Clear();
                table.Clear();
                adapter.Fill(table);

                conexion.Close();

                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["NUMPED"]) != 0)
                    {
                        listBox1.Items.Add(row["NUMPED"]);

                    }
                    valorCliente = Convert.ToString(row["RTERCERO"]);
                }

                listBox2.Items.Add(valorCliente);//Se muestra el cliente al final, ya que siempre es el mismo

                if (listBox1.Items.Count > 0)
                {
                    valorPedido = listBox1.Items[0].ToString();
                }

                // Se muestra los datos del proyecto en el DataGridView
                SqlConnection conexion1 = null;
                SqlCommand comando1 = null;
                string strsql1;
                Int32 estadoOF = 2; // Todas
                DataTable table1 = new DataTable();
                conexion1 = new SqlConnection(connString);
                conexion1.Open();

                //Se marca por defecto todas, ya que en principio no es necesario que selecione abiertas o cerradas, pero se deja proparado por si lo necesita en un futuro


                strsql1 = "EXEC gestionSoldadura '" + Empresa + "','" + proyecto + "','" + Usuario + "','" + 2 + "','" + valorCliente + "'";
                comando1 = new SqlCommand(strsql1, conexion1);
                adapter = new SqlDataAdapter(comando1);
                comando1.CommandText = strsql1;
                table1.Columns.Clear();
                table1.Clear();
                adapter.Fill(table1);
                dataGridView2.DataSource = table1;
                this.dataGridView2.Columns[0].Visible = false; // NO visulizamos cliente
                this.dataGridView2.Columns[2].Visible = false; // NO visulizamos ORDFAB
                this.dataGridView2.Columns[10].Visible = false; // No visulizamos marcarUT
                this.dataGridView2.Columns[11].Visible = false; // No visulizamos marcarMT
                dataGridView2.AllowUserToAddRows = false;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                { // Ponemos en amarillo los codigos que tengan marcadosUT y calculamos Totales
                    try
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();

                        style.Font = new Font(dataGridView2.Font, FontStyle.Bold);


                        // aqui pongo en verde si el valor es positivo

                        if (Convert.ToInt32(row.Cells[4].Value.ToString()) > 0)
                        {

                            row.Cells[4].Style = style;
                            row.Cells[4].Style.BackColor = Color.GreenYellow;

                        }
                        if (Convert.ToInt32(row.Cells[5].Value.ToString()) > 0)
                        {

                            row.Cells[5].Style = style;
                            row.Cells[5].Style.BackColor = Color.GreenYellow;

                        }
                        if (Convert.ToInt32(row.Cells[6].Value.ToString()) > 0)
                        {

                            row.Cells[6].Style = style;
                            row.Cells[6].Style.BackColor = Color.GreenYellow;

                        }
                        if (Convert.ToInt32(row.Cells[7].Value.ToString()) > 0)
                        {

                            row.Cells[7].Style = style;
                            row.Cells[7].Style.BackColor = Color.GreenYellow;

                        }
                        if (Convert.ToInt32(row.Cells[8].Value.ToString()) > 0)
                        {

                            row.Cells[8].Style = style;
                            row.Cells[8].Style.BackColor = Color.GreenYellow;

                        }
                        if (Convert.ToInt32(row.Cells[9].Value.ToString()) > 0)
                        {

                            row.Cells[9].Style = style;
                            row.Cells[9].Style.BackColor = Color.GreenYellow;

                        }

                        TotalCantidad = TotalCantidad + Int32.Parse(row.Cells[4].Value.ToString());
                        TotalMT = TotalMT + Int32.Parse(row.Cells[5].Value.ToString());
                        TotalVT = TotalVT + Int32.Parse(row.Cells[6].Value.ToString());
                        TotalUT = TotalUT + Int32.Parse(row.Cells[7].Value.ToString());
                        TotalDM = TotalDM + Int32.Parse(row.Cells[8].Value.ToString());
                        TotalRX = TotalRX + Int32.Parse(row.Cells[9].Value.ToString());

                        if (Boolean.Parse(row.Cells[10].Value.ToString()) == true)
                            TotalMarcaUT = TotalMarcaUT + Convert.ToInt32(row.Cells[4].Value);

                        if (Boolean.Parse(row.Cells[11].Value.ToString()) == true)
                            TotalMarcaMT = TotalMarcaMT + Convert.ToInt32(row.Cells[4].Value);

                        if (row.Cells[10].Value.Equals(true))
                        {
                            row.Cells[1].Style.BackColor = Color.Yellow;
                        }

                        if (row.Cells[11].Value.Equals(true))
                        {
                            row.Cells[1].Style.BackColor = Color.Orange;
                        }

                        if (row.Cells[11].Value.Equals(true) && row.Cells[10].Value.Equals(true))
                        {
                            row.Cells[1].Style.BackColor = Color.Red;
                        }

                    }
                    catch (ArgumentOutOfRangeException outOfRange)
                    {
                        Console.WriteLine("Error: {0}", outOfRange.Message);
                    }
                }

                Totales.Text = "Totales del proyecto " + proyecto;
                textBoxCantidad.Text = Convert.ToString(TotalCantidad);
                textBoxMT.Text = Convert.ToString(TotalMT);
                textBoxVT.Text = Convert.ToString(TotalVT);
                textBoxUT.Text = Convert.ToString(TotalUT);
                textBoxDM.Text = Convert.ToString(TotalDM);
                textBoxRX.Text = Convert.ToString(TotalRX);
                textBoxMarcaUT.Text = Convert.ToString(TotalMarcaUT);
                textBoxMarcaMT.Text = Convert.ToString(TotalMarcaMT);

                labelMT.Text = Convert.ToString(TotalMT);
                labelVT.Text = Convert.ToString(TotalVT);
                labelUT.Text = Convert.ToString(TotalUT);
                labelDM.Text = Convert.ToString(TotalDM);
                labelRX.Text = Convert.ToString(TotalRX);

                conexion1.Close();

                //Obtenemos los ensayos del proyecto
                SqlConnection conexion2 = null;
                SqlCommand comando2 = null;
                string strsql2;
                DataTable table2 = new DataTable();
                conexion2 = new SqlConnection(connString);
                conexion2.Open();
                SqlDataAdapter adapter2;

                strsql2 = "SELECT MTEnsayo, VTEnsayo, UTEnsayo, DMEnsayo , RXEnsayo, finalizado, observacion  FROM  TC_SoldaduraProyecto WHERE proyecto = " + proyecto;

                comando2 = new SqlCommand(strsql2, conexion2);
                adapter2 = new SqlDataAdapter(comando2);
                comando2.CommandText = strsql2;
                table2.Columns.Clear();
                table2.Clear();
                adapter2.Fill(table2);

                conexion.Close();

                for (int i = 0; i < table2.Rows.Count; i++) //Mostramos los ensayos
                {
                    ensayosMT.Text = table2.Rows[i]["MTEnsayo"].ToString();
                    ensayosVT.Text = table2.Rows[i]["VTEnsayo"].ToString();
                    ensayosUT.Text = table2.Rows[i]["UTEnsayo"].ToString();
                    ensayosDM.Text = table2.Rows[i]["DMEnsayo"].ToString();
                    ensayosRX.Text = table2.Rows[i]["RXEnsayo"].ToString();
                    finalizado = Convert.ToBoolean(table2.Rows[i]["finalizado"]);
                    checkBox1.Checked = finalizado;
                    richTextBox1.Text = table2.Rows[i]["observacion"].ToString();
                }

                calculaRestantes();
                label27.Text = obtenerPlanCalidad(proyecto);

                //Obtener los datos de los informes

                SqlConnection conexion3 = null;
                SqlCommand comando3 = null;
                string strsql3;
                DataTable table3 = new DataTable();
                conexion3 = new SqlConnection(connString);
                conexion3.Open();
                SqlDataAdapter adapter3;

                strsql3 = "SELECT informes,mt,vt,ut  FROM  TC_SoldaduraProyecto_DatosInformes WHERE proyecto = " + proyecto + " and codEmp=" + Empresa;

                comando3 = new SqlCommand(strsql3, conexion3);
                adapter3 = new SqlDataAdapter(comando3);
                comando3.CommandText = strsql3;
                table3.Columns.Clear();
                table3.Clear();
                adapter3.Fill(table3);

                conexion3.Close();

                for (int i = 0; i < table3.Rows.Count; i++) //Mostramos los datos de los informes
                {
                    bool informe = Convert.ToBoolean(table3.Rows[i]["informes"]);
                    checkBox2.Checked = informe;

                    bool mt = Convert.ToBoolean(table3.Rows[i]["mt"]);
                    checkBox3.Checked = mt;

                    bool vt = Convert.ToBoolean(table3.Rows[i]["vt"]);
                    checkBox4.Checked = vt;

                    bool ut = Convert.ToBoolean(table3.Rows[i]["ut"]);
                    checkBox5.Checked = ut;
                }
            }
        }

        private void abrirTodoProyectos_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }



        private void calculaRestantes()
        {
            calcularMT();
            calcularVT();
            calcularUT();
            calcularDM();
            calcularRX();
        }

        private void calcularMT()
        {
            try
            {
                //Calculamos el resultado para MT
                if (ensayosMT.Text != "")
                {
                    int resuMT = TotalMT - Int32.Parse(ensayosMT.Text);
                    if (resuMT <= 0)
                    {
                        faltanMT.ForeColor = Color.Red;
                        faltanMT.Text = Convert.ToString(resuMT);
                    }
                    else
                    {
                        faltanMT.ForeColor = Color.Green;
                        faltanMT.Text = Convert.ToString(resuMT);
                    }

                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {

                Console.WriteLine("Error: {0}", outOfRange.Message);
            }
        }

        private void calcularVT()
        {

            try
            {
                //Calculamos el resultado para VT
                if (ensayosVT.Text != "")
                {
                    int resuVT = TotalVT - Int32.Parse(ensayosVT.Text);
                    if (resuVT <= 0)
                    {
                        faltanVT.ForeColor = Color.Red;
                        faltanVT.Text = Convert.ToString(resuVT);
                    }
                    else
                    {
                        faltanVT.ForeColor = Color.Green;
                        faltanVT.Text = Convert.ToString(resuVT);
                    }

                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {

                Console.WriteLine("Error: {0}", outOfRange.Message);
            }
        }

        private void calcularUT()
        {

            try
            {
                //Calculamos el resultado para UT
                if (ensayosUT.Text != "")
                {
                    int resuUT = TotalUT - Int32.Parse(ensayosUT.Text);
                    if (resuUT <= 0)
                    {
                        faltanUT.ForeColor = Color.Red;
                        faltanUT.Text = Convert.ToString(resuUT);
                    }
                    else
                    {
                        faltanUT.ForeColor = Color.Green;
                        faltanUT.Text = Convert.ToString(resuUT);
                    }

                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {

                Console.WriteLine("Error: {0}", outOfRange.Message);
            }
        }

        private void calcularDM()
        {

            try
            {
                //Calculamos el resultado para VT
                if (ensayosDM.Text != "")
                {
                    int resuDM = TotalDM - Int32.Parse(ensayosDM.Text);
                    if (resuDM <= 0)
                    {
                        faltanDM.ForeColor = Color.Red;
                        faltanDM.Text = Convert.ToString(resuDM);
                    }
                    else
                    {
                        faltanDM.ForeColor = Color.Green;
                        faltanDM.Text = Convert.ToString(resuDM);
                    }

                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {

                Console.WriteLine("Error: {0}", outOfRange.Message);
            }
        }

        private void calcularRX()
        {

            try
            {
                //Calculamos el resultado para RX
                if (ensayosRX.Text != "")
                {
                    int resuRX = TotalRX - Int32.Parse(ensayosRX.Text);
                    if (resuRX <= 0)
                    {
                        faltanRX.ForeColor = Color.Red;
                        faltanRX.Text = Convert.ToString(resuRX);
                    }
                    else
                    {
                        faltanRX.ForeColor = Color.Green;
                        faltanRX.Text = Convert.ToString(resuRX);
                    }

                }
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {

                Console.WriteLine("Error: {0}", outOfRange.Message);
            }
        }


        private void ensayosMT_MouseCaptureChanged(object sender, EventArgs e)
        {
            ensayosMT.ForeColor = Color.Blue;
            calcularMT();
        }



        private void ensayosMT_TextChanged(object sender, EventArgs e)
        {

            calcularMT();
        }

        private void ensayosVT_MouseCaptureChanged(object sender, EventArgs e)
        {
            ensayosVT.ForeColor = Color.Blue;
            calcularVT();

        }

        private void ensayosVT_TextChanged(object sender, EventArgs e)
        {
            calcularVT();
        }

        private void ensayosUT_MouseCaptureChanged(object sender, EventArgs e)
        {
            ensayosUT.ForeColor = Color.Blue;
            calcularUT();
        }

        private void ensayosUT_TextChanged(object sender, EventArgs e)
        {
            calcularUT();
        }

        private void ensayosDM_MouseCaptureChanged(object sender, EventArgs e)
        {
            ensayosDM.ForeColor = Color.Blue;
            calcularDM();
        }

        private void ensayosDM_TextChanged(object sender, EventArgs e)
        {

            calcularDM();
        }

        private void ensayosRX_MouseCaptureChanged(object sender, EventArgs e)
        {
            ensayosRX.ForeColor = Color.Blue;
            calcularRX();
        }

        private void ensayosRX_TextChanged(object sender, EventArgs e)
        {

            calcularRX();
        }

        private void dataGridView2_MouseUp(object sender, MouseEventArgs e)
        {
            // Load context menu on right mouse click            
            if (e.Button == MouseButtons.Right)
            {
                this.hitTestInfo = this.dataGridView2.HitTest(e.X, e.Y);
                Int32 fila = hitTestInfo.RowIndex;
                Int32 columna = hitTestInfo.ColumnIndex;
                if (columna < 0)
                    return;

                switch (this.hitTestInfo.Type)
                {
                    case (DataGridViewHitTestType.Cell):

                        //Solo se va a mostrar el menu si el campo pulsado no es vacio
                        if (this.dataGridView2.Rows[fila].Cells[columna].Value.ToString() != "")
                        {


                            //Activo plano pieza
                            if (columna == 2)
                            {
                                contextMenuStrip1.Items[0].Enabled = true;
                                contextMenuStrip1.Items[1].Enabled = false;


                                contextMenuStrip1.Show(this.dataGridView2, new Point(e.X, e.Y));
                            }

                            if (columna == 1)
                            {
                                if (dataGridView2.Rows[fila].Cells[columna].Style.BackColor == Color.Yellow)
                                {
                                    this.marcarUTToolStripMenuItem.Text = "Eliminar marca UT";

                                }
                                else
                                {
                                    this.marcarUTToolStripMenuItem.Text = "Marcar UT";
                                }

                                if (dataGridView2.Rows[fila].Cells[columna].Style.BackColor == Color.Orange)
                                {
                                    this.marcarMTToolStripMenuItem.Text = "Eliminar marca MT";
                                }
                                else
                                {
                                    this.marcarMTToolStripMenuItem.Text = "Marcar MT";
                                }

                                if (dataGridView2.Rows[fila].Cells[columna].Style.BackColor == Color.Red)
                                {
                                    this.marcarUTToolStripMenuItem.Text = "Eliminar marca UT";
                                    this.marcarMTToolStripMenuItem.Text = "Eliminar marca MT";
                                }

                                contextMenuStrip1.Items[0].Enabled = true;
                                contextMenuStrip1.Items[1].Enabled = true;

                                contextMenuStrip1.Show(this.dataGridView2, new Point(e.X, e.Y));
                            }

                            if (columna > 4)
                            {
                                contextMenuStrip2.Items[0].Enabled = true;

                                contextMenuStrip2.Show(this.dataGridView2, new Point(e.X, e.Y));
                            }
                        }
                        break;
                }
            }
        }


        private void abrirPlanoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            String empresa = empresaGlobal.empresaID;
            String usuario = Environment.UserName.ToUpper();
            String codigo = this.dataGridView2.Rows[hitTestInfo.RowIndex].Cells[1].Value.ToString();


            String path = "\\\\SRVAPPS01\\PDFTMPESCRITURA$";

            ProyectosOnLine.WebService_Planos.Service ServicioWeb = new ProyectosOnLine.WebService_Planos.Service();
            String Resultado = ServicioWeb.ObtenerFile2(codigo, empresa, usuario);


            if (Resultado.StartsWith("-1"))
                MessageBox.Show(Form.ActiveForm, "Error al obtener el plano del código: " + Environment.NewLine + Resultado);
            else
            {
                //System.Diagnostics.Process.Start("IExplore.exe", path + "\\" + usuario + "\\" + Resultado);
                System.Diagnostics.Process.Start("msedge.exe", path + "\\" + usuario + "\\" + Resultado);
            }


            ServicioWeb.Dispose();
        }

        private void marcarUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            foreach (DataGridViewRow row in this.dataGridView2.SelectedRows)
            {
               int indice =  row.Index;
               marcarUT(indice);
            }
            
            this.comboProyectos_SelectedIndexChanged(sender, e);  
        }

        public void marcarUT(int indice)
        {
            int marcarut = 0;

            if (this.dataGridView2.Rows[indice].Cells[1].Value.ToString() != "")
            {

                if (this.dataGridView2.Rows[indice].Cells[1].Style.BackColor == Color.Yellow || this.dataGridView2.Rows[indice].Cells[1].Style.BackColor == Color.Red)
                {
                    this.dataGridView2.Rows[indice].Cells[1].Style.BackColor = Color.Blue;
                    this.dataGridView2.Rows[indice].Cells[1].Style.ForeColor = Color.White;
                    marcarut = 0;
                }
                else
                {
                    if (this.dataGridView2.Rows[indice].Cells[1].Style.BackColor == Color.Orange)
                    {
                        this.dataGridView2.Rows[indice].Cells[1].Style.BackColor = Color.Red;
                        marcarut = 1;
                    }
                    else
                    {
                        this.dataGridView2.Rows[indice].Cells[1].Style.BackColor = Color.Yellow;
                        marcarut = 1;

                    }

                }
                String proyecto = this.dataGridView2.Rows[indice].Cells[0].Value.ToString();
                String ordFab = this.dataGridView2.Rows[indice].Cells[2].Value.ToString();

                //obtener los ensayos del proyecto
                SqlConnection conexion2 = null;
                SqlCommand comando2 = null;
                string strsql2;

                DataTable table2 = new DataTable();

                conexion2 = new SqlConnection(connString);
                conexion2.Open();

                SqlDataAdapter adapter2;

                strsql2 = "UPDATE TC_Soldadura set marcaUT= " + Convert.ToString(marcarut);
                strsql2 = strsql2 + " where proyecto = " + proyecto + " AND OrdFab= " + ordFab;
                comando2 = new SqlCommand(strsql2, conexion2);

                System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql2);

                adapter2 = new SqlDataAdapter(comando2);

                comando2.CommandText = strsql2;
                table2.Columns.Clear();
                table2.Clear();
                adapter2.Fill(table2);

                conexion2.Close();
            }
        }

        private void modificarDatoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Int32 fila = hitTestInfo.RowIndex;
            Int32 columna = hitTestInfo.ColumnIndex;

            int valor = Convert.ToInt32(this.dataGridView2.Rows[fila].Cells[columna].Value);
            Form3 form3 = new Form3(valor);
            form3.pasado += new Form3.pasarDato(Ejecutar);
            form3.ShowDialog();
        }

        public void Ejecutar(string valor)
        {
            Int32 fila = hitTestInfo.RowIndex;
            Int32 columna = hitTestInfo.ColumnIndex;

            this.dataGridView2.Rows[fila].Cells[columna].Value = valor;
            this.dataGridView2.Rows[fila].Cells[columna].Style.ForeColor = Color.White;
            this.dataGridView2.Rows[fila].Cells[columna].Style.BackColor = Color.Blue;
            DataGridViewCell celda = this.dataGridView2.Rows[fila].Cells[columna];
            celda.Style.Font = new Font(celda.InheritedStyle.Font, FontStyle.Bold);
            recalcularTotalesyFaltantes();
        }



        public void recalcularTotalesyFaltantes()
        {

            cacularTotales();
            calculaRestantes();
        }

        public void cacularTotales()
        {

            TotalMT = 0;
            TotalVT = 0;
            TotalUT = 0;
            TotalDM = 0;
            TotalRX = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                try
                {

                    TotalMT = TotalMT + Int32.Parse(row.Cells[5].Value.ToString());
                    TotalVT = TotalVT + Int32.Parse(row.Cells[6].Value.ToString());
                    TotalUT = TotalUT + Int32.Parse(row.Cells[7].Value.ToString());
                    TotalDM = TotalDM + Int32.Parse(row.Cells[8].Value.ToString());
                    TotalRX = TotalRX + Int32.Parse(row.Cells[9].Value.ToString());

                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("Error: {0}", outOfRange.Message);
                }
            }

            textBoxMT.Text = Convert.ToString(TotalMT);
            textBoxVT.Text = Convert.ToString(TotalVT);
            textBoxUT.Text = Convert.ToString(TotalUT);
            textBoxDM.Text = Convert.ToString(TotalDM);
            textBoxRX.Text = Convert.ToString(TotalRX);

            labelMT.Text = Convert.ToString(TotalMT);
            labelVT.Text = Convert.ToString(TotalVT);
            labelUT.Text = Convert.ToString(TotalUT);
            labelDM.Text = Convert.ToString(TotalDM);
            labelRX.Text = Convert.ToString(TotalRX);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string marcas = "";

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {

                marcas = marcas + row.Cells[1].Value.ToString() + ";";

            }

            string direccion = "http://intranet.imedexsa.imedexsa.es/modulos/listaAlmacen/marcas/Visualizador_Planos.asp?tipo=1&marca=";
            string dirEmp = "&codemp=" + Empresa;


            SqlConnection conexion = null;
            SqlCommand comando = null;
            string strsql;

            DataTable table = new DataTable();

            conexion = new SqlConnection(connString);
            conexion.Open();

            SqlDataAdapter adapter;

            strsql = "select us_login from ctl_usuarios where us_email = '" + Usuario + "@imedexsa.es'";

            comando = new SqlCommand(strsql, conexion);

            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);

            adapter = new SqlDataAdapter(comando);

            comando.CommandText = strsql;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

            conexion.Close();
            string dirUser = "&user=" + table.Rows[0]["us_login"].ToString(); ;

            //System.Diagnostics.Process.Start("IExplore.exe", direccion + marcas + dirEmp + dirUser);
            System.Diagnostics.Process.Start("msedge.exe", direccion + marcas + dirEmp + dirUser);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_excelMT_Click(object sender, EventArgs e)
        {

       
            string year = DateTime.Now.Year.ToString();
            
            string ruta = "";
            OpenFileDialog seleccion = new OpenFileDialog();
            seleccion.Filter = "|*.xlsx";
            seleccion.Title = "Seleccionar Excel";
            seleccion.InitialDirectory = @"\\nas01\Documentos Calidad$\Soldadura\"+year+@"\END\MT";


            if (seleccion.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                    ruta = seleccion.FileName;
                   
                    //Create an excel application object
                    Excel.Application excelAppObj = new Excel.Application();
                    excelAppObj.DisplayAlerts = false;


                    //Abre el excel
                    //Excel.Workbook workBook = excelAppObj.Workbooks.Open(@"\\srvcifs\documentos calidad$\Soldadura\"+ year +@"\END\MT\IM-MT-"+proyecto +".xlsx", 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, false, false);
                    Excel.Workbook workBook = excelAppObj.Workbooks.Open(ruta, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, false, false);

                    //System.Diagnostics.Process.Start(ruta); // Carlos sanchez - 19/05/2021
                    //Selecciona la primera hoja del Excel. 
                    Excel.Worksheet worksheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

                    //ESTABLECER FECHA R8 --> R=18

                    DateTime hoy = DateTime.Now;
                    string fecha = hoy.ToString("dd/MM/yyyy");

                    worksheet.Cells[8, 18] = fecha;

                    //ESTABLECER CLIENTE D11 --> D=4
                    worksheet.Cells[11, 4] = valorCliente;

                    //ESTABLECER PROYECTO R11 --> R=18
                    worksheet.Cells[11, 18] = proyecto;

                    //ESTABLECER FECHA Y11 --> Y=25
                    worksheet.Cells[11, 25] = valorPedido;

                    //ESTABLECER FECHA H13 --> H=8
                    string piezas = "";
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!row.Cells[5].Value.ToString().Equals("0"))
                        {

                            string alemania = ruta.Substring(0, ruta.Length - 5);

                            int tam_cadena = alemania.Length;
                            string a = alemania.Substring((tam_cadena - 2), 2);
                            //Compruebo si el excel tiene el DE entoces en lugar de pieza escribo BAUTEIL
                            if (a.Equals("DE"))
                            {
                                piezas += row.Cells[1].Value.ToString() + " (" + row.Cells[5].Value.ToString() + " BAUTEIL); ";
                            }
                            else
                            {
                                piezas += row.Cells[1].Value.ToString() + " (" + row.Cells[5].Value.ToString() + " PIEZAS); ";
                            }
                        }
                    }
                    if (piezas.Length > 0)
                    {
                        piezas = piezas.Substring(0, piezas.Length - 2);
                        piezas = piezas + ".";
                        worksheet.Cells[13, 8] = piezas;
                    }

                    //Guarda(.xlsx format)
                   // workBook.SaveAs(ruta, Excel.XlFileFormat.xlOpenXMLWorkbook, null, null, false,
                   // false, Excel.XlSaveAsAccessMode.xlShared, false, false, null, null, null);

                    workBook.SaveAs(ruta, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, false, false,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, false, Type.Missing, Type.Missing, Type.Missing);

                    MessageBox.Show("Archivo modificado correctamente");

                    workBook.Close(false);
                    System.Diagnostics.Process.Start(ruta); // Carlos sanchez - 19/05/2021
                    
            }
            else
            {
                MessageBox.Show("AVISO: No has elegido ningun archivo");
            }
            Cursor.Current = Cursors.Default;

        }




        private void btn_excelVT_Click(object sender, EventArgs e)
        {



            string year = DateTime.Now.Year.ToString();

            string ruta = "";
            OpenFileDialog seleccion = new OpenFileDialog();
            seleccion.Filter = "|*.xlsx";
            seleccion.Title = "Seleccionar Excel";
            //seleccion.InitialDirectory = @"\\SRVCIFS\Documentos Calidad$\Soldadura\" + year + @"\END\VT";
            seleccion.InitialDirectory = @"\\nas01\Documentos Calidad$\Soldadura\" + year + @"\END\VT";


            if (seleccion.ShowDialog() == DialogResult.OK)
            {

                Cursor.Current = Cursors.WaitCursor;
                ruta = seleccion.FileName;

                //Create an excel application object
                Excel.Application excelAppObj = new Excel.Application();
                excelAppObj.DisplayAlerts = false;

                //Abre el excel
                //Excel.Workbook workBook = excelAppObj.Workbooks.Open(@"\\srvcifs\documentos calidad$\Soldadura\"+ year +@"\END\MT\IM-MT-"+proyecto +".xlsx", 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, false, false);
                Excel.Workbook workBook = excelAppObj.Workbooks.Open(ruta, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, false, false);


                //Selecciona la primera hoja del Excel. 
                Excel.Worksheet worksheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

                //ESTABLECER FECHA R8 --> R=18

                DateTime hoy = DateTime.Now;
                string fecha = hoy.ToString("dd/MM/yyyy");

                worksheet.Cells[8, 18] = fecha;

                //ESTABLECER CLIENTE D11 --> D=4
                worksheet.Cells[11, 4] = valorCliente;

                //ESTABLECER PROYECTO R11 --> R=18
                worksheet.Cells[11, 18] = proyecto;

                //ESTABLECER FECHA Y11 --> Y=25
                worksheet.Cells[11, 25] = valorPedido;

                //ESTABLECER FECHA H13 --> H=8
                string piezas = "";
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.Cells[6].Value.ToString().Equals("0"))
                    {
                        string alemania = ruta.Substring(0, ruta.Length - 5);

                        int tam_cadena = alemania.Length;
                        string a = alemania.Substring((tam_cadena - 2), 2);
                        //Compruebo si el excel tiene el DE entoces en lugar de pieza escribo BAUTEIL
                        if (a.Equals("DE"))
                        {
                            piezas += row.Cells[1].Value.ToString() + " (" + row.Cells[6].Value.ToString() + " BAUTEIL); ";
                        }
                        else
                        {
                            piezas += row.Cells[1].Value.ToString() + " (" + row.Cells[6].Value.ToString() + " PIEZAS); ";
                        }
                    }
                }
                if (piezas.Length > 0)
                {
                    piezas = piezas.Substring(0, piezas.Length - 2);
                    piezas = piezas + ".";
                    worksheet.Cells[13, 8] = piezas;
                }

                workBook.SaveAs(ruta, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, false, false,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, false, Type.Missing, Type.Missing, Type.Missing);

                MessageBox.Show("Archivo modificado correctamente");

                workBook.Close(false);
                System.Diagnostics.Process.Start(ruta); // Carlos sanchez - 19/05/2021
            }
            else {

                MessageBox.Show("AVISO: No has elegido ningun archivo");
            }
            System.Diagnostics.Process.Start(ruta); // Carlos sanchez - 19/05/2021
            Cursor.Current = Cursors.Default;
            
        }



        private void btn_ExcelUT_Click(object sender, EventArgs e)
        {
           


            string year = DateTime.Now.Year.ToString();

            string ruta = "";
            OpenFileDialog seleccion = new OpenFileDialog();
            seleccion.Filter = "|*.xlsx";
            seleccion.Title = "Seleccionar Excel";
            seleccion.InitialDirectory = @"\\nas01\Documentos Calidad$\Soldadura\" + year + @"\END\UT";


            if (seleccion.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                ruta = seleccion.FileName;


                //Create an excel application object
                Excel.Application excelAppObj = new Excel.Application();
                excelAppObj.DisplayAlerts = false;


                //Abre el excel
                //Excel.Workbook workBook = excelAppObj.Workbooks.Open(@"\\srvcifs\documentos calidad$\Soldadura\"+ year +@"\END\MT\IM-MT-"+proyecto +".xlsx", 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, false, false);
                Excel.Workbook workBook = excelAppObj.Workbooks.Open(ruta, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, false, false);


                //Selecciona la primera hoja del Excel. 
                Excel.Worksheet worksheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

                //ESTABLECER FECHA AF5 --> AF=32

                DateTime hoy = DateTime.Now;
                string fecha = hoy.ToString("dd/MM/yyyy");

                worksheet.Cells[5, 32] = fecha;

                //ESTABLECER CLIENTE E8 --> E=5
                worksheet.Cells[8, 5] = valorCliente;

                //ESTABLECER PROYECTO S8 --> S=19
                worksheet.Cells[8, 19] = proyecto;

                //ESTABLECER FECHA AC8 --> AC=29
                worksheet.Cells[8, 29] = valorPedido;

                //ESTABLECER FECHA F10 --> F=6
                string piezas = "";
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.Cells[7].Value.ToString().Equals("0"))
                    {
                        string alemania = ruta.Substring(0, ruta.Length - 5);

                        int tam_cadena = alemania.Length;
                        string a = alemania.Substring((tam_cadena - 2), 2);
                        //Compruebo si el excel tiene el DE entoces en lugar de pieza escribo BAUTEIL
                        if (a.Equals("DE"))
                        {
                            piezas += row.Cells[1].Value.ToString() + " (" + row.Cells[7].Value.ToString() + " BAUTEIL); ";
                        }
                        else
                        {
                            piezas += row.Cells[1].Value.ToString() + " (" + row.Cells[7].Value.ToString() + " PIEZAS); ";
                        }
                    }
                }
                if (piezas.Length > 0)
                {
                    piezas = piezas.Substring(0, piezas.Length - 2);
                    piezas = piezas + ".";
                    worksheet.Cells[10, 6] = piezas;
                }

                workBook.SaveAs(ruta, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, false, false,
                         Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, false, Type.Missing, Type.Missing, Type.Missing);

                MessageBox.Show("Archivo modificado correctamente");

                workBook.Close(false);
                System.Diagnostics.Process.Start(ruta); // Carlos sanchez - 19/05/2021
            } else {
                MessageBox.Show("AVISO: No has elegido ningun archivo");
            }

            System.Diagnostics.Process.Start(ruta); // Carlos sanchez - 19/05/2021
            Cursor.Current = Cursors.Default;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            // AQUI ACTULIZADO EL VALOR EN EL PROYECTO

            //obtener los ensayos del proyecto
            SqlConnection conexion2 = null;
            SqlCommand comando2 = null;
            string strsql2;


            DataTable table2 = new DataTable();

            conexion2 = new SqlConnection(connString);
            conexion2.Open();

            SqlDataAdapter adapter2;

            int bolean = 0;

            if (checkBox1.Checked == false)
            {
                bolean = 0;
            }

            if (checkBox1.Checked == true)
            {
                bolean = 1;
            }

            strsql2 = "UPDATE TC_SoldaduraProyecto set finalizado = " + bolean;
            strsql2 = strsql2 + ", FECHAM= GETDATE(), usumod='" + Usuario + "' ";
            strsql2 = strsql2 + " where Proyecto = " + proyecto;
            comando2 = new SqlCommand(strsql2, conexion2);



            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql2);

            adapter2 = new SqlDataAdapter(comando2);

            comando2.CommandText = strsql2;
            table2.Columns.Clear();
            table2.Clear();
            adapter2.Fill(table2);

            conexion2.Close();

        }

        

       // Carlos 02/01/2023
        private void marcarMTToolStripMenuItem_Click(object sender, EventArgs e)
        {

             foreach (DataGridViewRow row in this.dataGridView2.SelectedRows)
             {
               int indice =  row.Index;
               marcarMT(indice);
             }
             this.comboProyectos_SelectedIndexChanged(sender, e);
        }

        public void marcarMT(int indice)
        {
            int marcarut = 0;

            if (this.dataGridView2.Rows[indice].Cells[1].Value.ToString() != "")
            {

                if (this.dataGridView2.Rows[indice].Cells[1].Style.BackColor == Color.Orange || this.dataGridView2.Rows[indice].Cells[1].Style.BackColor == Color.Red)
                {



                    this.dataGridView2.Rows[indice].Cells[1].Style.BackColor = Color.Blue;
                    this.dataGridView2.Rows[indice].Cells[1].Style.ForeColor = Color.White;
                    marcarut = 0;

                }
                else
                {
                    if (this.dataGridView2.Rows[indice].Cells[1].Style.BackColor == Color.Yellow)
                    {
                        this.dataGridView2.Rows[indice].Cells[1].Style.BackColor = Color.Red;
                        marcarut = 1;
                    }
                    else
                    {
                        this.dataGridView2.Rows[indice].Cells[1].Style.BackColor = Color.Orange;
                        marcarut = 1;

                    }
                }
                String proyecto = this.dataGridView2.Rows[indice].Cells[0].Value.ToString();
                String ordFab = this.dataGridView2.Rows[indice].Cells[2].Value.ToString();

                //obtener los ensayos del proyecto
                SqlConnection conexion2 = null;
                SqlCommand comando2 = null;
                string strsql2;


                DataTable table2 = new DataTable();

                conexion2 = new SqlConnection(connString);
                conexion2.Open();

                SqlDataAdapter adapter2;

                strsql2 = "UPDATE TC_Soldadura set marcaMT= " + Convert.ToString(marcarut);
                strsql2 = strsql2 + " where proyecto = " + proyecto + " AND OrdFab= " + ordFab;
                comando2 = new SqlCommand(strsql2, conexion2);



                System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql2);

                adapter2 = new SqlDataAdapter(comando2);

                comando2.CommandText = strsql2;
                table2.Columns.Clear();
                table2.Clear();
                adapter2.Fill(table2);

                conexion2.Close();

            }
        }

        private String obtenerPlanCalidad(string proyecto)
        {
            string plan = "";
            String strSql = "";

            strSql = "select gg.dbo.IME_Obtener_Plan_Calidad_Formulario ('3','proyecto','" + proyecto + "') ";

            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.AppSettings["ggConnectionString"].ToString()))
            {
                SqlCommand command = new SqlCommand(strSql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        plan = reader[0].ToString();
                        //Percha p = new Percha(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                    }
                }

            }

            return plan;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(proyecto);
            form4.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // PRIMERO RECORREMOS EL  DATESET Y GUARDAMOS LAS FILAS QUE TENGAN ALGUN CAMBIO*
            Cursor.Current = Cursors.WaitCursor;

            SqlConnection conexion = null;
            SqlCommand comando = null;
            string strsql;


            DataTable table = new DataTable();

            conexion = new SqlConnection(connString);
            conexion.Open();
            SqlDataAdapter adapter;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                try
                {
                    if (row.Cells[5].Style.BackColor == Color.Blue ||
                       row.Cells[6].Style.BackColor == Color.Blue ||
                       row.Cells[7].Style.BackColor == Color.Blue ||
                       row.Cells[8].Style.BackColor == Color.Blue ||
                       row.Cells[9].Style.BackColor == Color.Blue)
                    {

                        strsql = "UPDATE  TC_Soldadura SET MT=" + row.Cells[5].Value.ToString();
                        strsql = strsql + ", VT=" + row.Cells[6].Value.ToString();
                        strsql = strsql + ", UT=" + row.Cells[7].Value.ToString();
                        strsql = strsql + ", DM=" + row.Cells[8].Value.ToString();
                        strsql = strsql + ", RX=" + row.Cells[9].Value.ToString();
                        strsql = strsql + ", FECHAM= GETDATE(), usumod='" + Usuario + "' ";
                        strsql = strsql + "WHERE Proyecto = '" + proyecto + "' and Etiqueta = '" + row.Cells[3].Value.ToString() + "' ";



                        comando = new SqlCommand(strsql, conexion);

                        System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql);

                        adapter = new SqlDataAdapter(comando);

                        comando.CommandText = strsql;
                        table.Columns.Clear();
                        table.Clear();
                        adapter.Fill(table);


                    }



                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("Error: {0}", outOfRange.Message);
                }


            }

            conexion.Close();

            //Se guarda los ensayos y totales en la tabla TC_SOLDADURAPROYECTOS
            Cursor.Current = Cursors.WaitCursor;

            SqlConnection conexion2 = null;
            SqlCommand comando2 = null;
            string strsql2;
            DataTable table2 = new DataTable();
            conexion2 = new SqlConnection(connString);
            conexion2.Open();
            SqlDataAdapter adapter2;

            strsql2 = "UPDATE  TC_SoldaduraProyecto SET MTEnsayo=" + ensayosMT.Text;
            strsql2 = strsql2 + ", VTEnsayo=" + ensayosVT.Text;
            strsql2 = strsql2 + ", UTEnsayo=" + ensayosUT.Text;
            strsql2 = strsql2 + ", DMEnsayo=" + ensayosDM.Text;
            strsql2 = strsql2 + ", RXEnsayo=" + ensayosRX.Text;
            strsql2 = strsql2 + ", MTtotales=" + labelMT.Text;
            strsql2 = strsql2 + ", VTtotales=" + labelVT.Text;
            strsql2 = strsql2 + ", UTtotales=" + labelUT.Text;
            strsql2 = strsql2 + ", DMtotales=" + labelDM.Text;
            strsql2 = strsql2 + ", RXtotales=" + labelRX.Text;
            strsql2 = strsql2 + ", observacion='" + richTextBox1.Text + "' ";
            strsql2 = strsql2 + ", FECHAM= GETDATE(), usumod='" + Usuario + "' ";
            strsql2 = strsql2 + "WHERE Proyecto = '" + proyecto + "'";

            comando2 = new SqlCommand(strsql2, conexion2);
            adapter2 = new SqlDataAdapter(comando2);

            comando2.CommandText = strsql2;
            table2.Columns.Clear();
            table2.Clear();
            adapter2.Fill(table2);

            ensayosMT.ForeColor = Color.Black;
            ensayosVT.ForeColor = Color.Black;
            ensayosUT.ForeColor = Color.Black;
            ensayosDM.ForeColor = Color.Black;
            ensayosRX.ForeColor = Color.Black;
            actulizarDatosInformes();

            this.comboProyectos_SelectedIndexChanged(sender, e);

           
            MessageBox.Show("Cambios guardados correctamente");
            Cursor.Current = Cursors.Default;

        }


        public void actulizarDatosInformes()
        {

            // AQUI ACTULIZADO EL VALOR EN EL PROYECTO

            //obtener los ensayos del proyecto
            SqlConnection conexion2 = null;
            SqlCommand comando2 = null;
            string strsql2;

            DataTable table2 = new DataTable();

            conexion2 = new SqlConnection(connString);
            conexion2.Open();

            SqlDataAdapter adapter2;

            int mt=0, vt=0, ut=0;

            int bolean = 0;

            if (checkBox2.Checked == false)
            {
                bolean = 0;
            }

            if (checkBox2.Checked == true)
            {
                bolean = 1;
            }

            if (checkBox3.Checked == false)
            {
                mt = 0;
            }

            if (checkBox3.Checked == true)
            {
                mt = 1;
            }

            if (checkBox4.Checked == false)
            {
                vt = 0;
            }

            if (checkBox4.Checked == true)
            {
                vt = 1;
            }

            if (checkBox5.Checked == false)
            {
                ut = 0;
            }

            if (checkBox5.Checked == true)
            {
                ut = 1;
            }

            strsql2 = "UPDATE TC_SoldaduraProyecto_DatosInformes set informes = " + bolean;
            strsql2 = strsql2 + " ,mt = " + mt+", ";
            strsql2 = strsql2 + " vt = " + vt + ", ";
            strsql2 = strsql2 + " ut = " + ut+", FECHAM= GETDATE(), usumod='" + Usuario + "' ";
            strsql2 = strsql2 + " where Proyecto = " + proyecto;
            comando2 = new SqlCommand(strsql2, conexion2);



            System.Diagnostics.Debug.WriteLine(" La query que se va a hacer es: " + strsql2);

            adapter2 = new SqlDataAdapter(comando2);

            comando2.CommandText = strsql2;
            table2.Columns.Clear();
            table2.Clear();
            adapter2.Fill(table2);

            conexion2.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(proyecto);
            form5.Show();
        }

    }

}