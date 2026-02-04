using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using empresaGlobalProj;
using System.Diagnostics;

namespace BuscadorNC
{
    public partial class buscadorNC_MainForm : Form
    {
        string connetionString = @"Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";
        public buscadorNC_MainForm()
        {
            InitializeComponent();
            dataGridView_marcasCopiadas.Columns.Add("Marcas", "Marcas");

        }


        private void consBono_button_Click(object sender, EventArgs e)
        {
            if (bono_tb.Text != "")
            {
                string path = "";
                if (radioButtonMade.Checked == true)
                {
                    path = @"\\172.16.2.3\MDT_Adf$SPPLM_Bonos_CN\" + bono_tb.Text;
                }
                else
                {
                    path = @"X:\BonosBuscadorNC\" + bono_tb.Text;
                }

                dataGridView_marcasCopiadas.Rows.Clear();
                label_copiando.Text = "Copiando Archivos ...";
                //\\vmsrvapps\MDT_Adf$SPPLM_Bonos_CN
                //\\172.16.2.3\MDT_Adf$SPPLM_Bonos_CN

                //Definción de Variables
                //Familia obtenida
                string familia = "";

                //ANGEL GARCIA 25/03/2024 - CANTIDAD DE LA MARCA EN LA OF
                string cantidad = "";


                //ANGEL GARCIA 05/06/2024 - NUMERO DE PREPAQUETE PARA SUBDIVIDIR DENTRO DE LA CARPETA DE BONO
                string prepaquete = "";

                //Donde están los NCs que hay que copiar pathNCs
                string pathNCs = "";

                //string path_alt = @"\\vmsrvapps\MDT_Adf$SPPLM_Bonos_CN";

                //string path = @"C:\Desarrollo\pruebas\" + bono_tb.Text; //para pruebas
                if (Directory.Exists(path))
                {
                    /* MessageBox.Show("El directorio ya existe", "Directorio Existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     return;*/

                    DialogResult result = MessageBox.Show("El directorio ya existe, ¿Desea borrarlo y crearlo de nuevo? Se perderá todos los NC que contiene ese bono", "Borrar Directorio", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        Directory.Delete(path, true);
                        DirectoryInfo di = Directory.CreateDirectory(path);
                        MessageBox.Show("Directorio Creado con éxito", "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        path += @"\";

                        //Obtengo todas las marcas de ese bono
                        //List<string> ncs = new List<string>();
                        DataTable ncs = new DataTable();
                        ncs = getNCsFromBono();

                        //ProgressBar
                        progressBar_copyFiles.Visible = true;
                        // Set Minimum to 1 to represent the first file being copied.
                        progressBar_copyFiles.Minimum = 1;
                        // Set Maximum to the total number of files to copy.
                        //progressBar_copyFiles.Maximum = ncs.Count;
                        progressBar_copyFiles.Maximum = ncs.Rows.Count;
                        // Set the initial value of the ProgressBar.
                        progressBar_copyFiles.Value = 1;
                        // Set the Step property to a value of 1 to represent each file being copied.
                        progressBar_copyFiles.Step = 1;

                        //ProgressBar

                        label_copiando.Visible = true;
                        //for (int i = 0; i < ncs.Count; i++)
                        for (int i = 0; i < ncs.Rows.Count; i++)
                        {
                            //obtengo la familia del bono
                            //familia = getFamiliaNC(ncs[i].ToString());
                            familia = getFamiliaNC(ncs.Rows[i][0].ToString());

                            //busco la carpeta donde se almacenan estos NC
                            pathNCs = buscarCarpetaContieneNC(familia);

                            cantidad = ncs.Rows[i][1].ToString();

                            

                            if (chkPrePaquetes.Checked)
                            {
                                prepaquete = ncs.Rows[i][2].ToString();

                                if (!Directory.Exists(path + prepaquete + @"\"))
                                {
                                    Directory.CreateDirectory(path + prepaquete + @"\");
                                }

                                CopyNCfromTo(pathNCs, path + prepaquete + @"\", ncs.Rows[i][0].ToString(), cantidad);
                            }
                            else
                            {
                                //Se coge el NC correspondiente de ese directorio
                                //CopyNCfromTo(pathNCs, path, ncs[i].ToString());
                                CopyNCfromTo(pathNCs, path, ncs.Rows[i][0].ToString(), cantidad);

                            }

                            //this.dataGridView_marcasCopiadas.Rows.Add(ncs[i].ToString());
                            this.dataGridView_marcasCopiadas.Rows.Add(ncs.Rows[i][0].ToString());

                            progressBar_copyFiles.PerformStep();

                        }
                        marcas_copiadas.Visible = true;
                        dataGridView_marcasCopiadas.Visible = true;
                        MessageBox.Show("Los archivos NC han sido copiados", "Archivos copiados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        label_copiando.Text = "Archivos han sido copiados al 100%";
                    }
                    else
                    {
                        //...
                        MessageBox.Show("Se ha cancelado la operación, no se han creado directorios ni copiado archivos", "Operación cancelada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Close();
                    }

                }

                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    path += @"\";
                    MessageBox.Show("Directorio Creado con éxito en " + path, "Directorio creado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Obtengo todas las marcas de ese bono
                    //List<string> ncs = new List<string>();
                    DataTable ncs = new DataTable();
                    ncs = getNCsFromBono();

                    //ProgressBar
                    progressBar_copyFiles.Visible = true;
                    // Set Minimum to 1 to represent the first file being copied.
                    progressBar_copyFiles.Minimum = 1;
                    // Set Maximum to the total number of files to copy.
                    //progressBar_copyFiles.Maximum = ncs.Count;
                    progressBar_copyFiles.Maximum = ncs.Rows.Count;
                    // Set the initial value of the ProgressBar.
                    progressBar_copyFiles.Value = 1;
                    // Set the Step property to a value of 1 to represent each file being copied.
                    progressBar_copyFiles.Step = 1;

                    //ProgressBar

                    //for (int i = 0; i < ncs.Count; i++)
                    for (int i = 0; i < ncs.Rows.Count; i++)
                    {
                        //obtengo la familia del bono
                        //familia = getFamiliaNC(ncs[i].ToString());
                        familia = getFamiliaNC(ncs.Rows[i][0].ToString());

                        //busco la carpeta donde se almacenan estos NC
                        pathNCs = buscarCarpetaContieneNC(familia);

                        cantidad = ncs.Rows[i][1].ToString();

                        if (chkPrePaquetes.Checked)
                        {
                            prepaquete = ncs.Rows[i][2].ToString();

                            if (!Directory.Exists(path + prepaquete + @"\"))
                            {
                                Directory.CreateDirectory(path + prepaquete + @"\");
                            }

                            CopyNCfromTo(pathNCs, path + prepaquete + @"\", ncs.Rows[i][0].ToString(), cantidad);
                        }
                        else
                        {
                            //Se coge el NC correspondiente de ese directorio
                            //CopyNCfromTo(pathNCs, path, ncs[i].ToString());
                            CopyNCfromTo(pathNCs, path, ncs.Rows[i][0].ToString(), cantidad);

                        }

                        //listView_marcas.Items.Add(ncs[i].ToString());
                        //this.dataGridView_marcasCopiadas.Rows.Add(ncs[i].ToString());
                        this.dataGridView_marcasCopiadas.Rows.Add(ncs.Rows[i][0].ToString());
                        //listView_marcas

                        progressBar_copyFiles.PerformStep();

                    }
                    marcas_copiadas.Visible = true;
                    dataGridView_marcasCopiadas.Visible = true;
                    MessageBox.Show("Los archivos NC han sido copiados", "Archivos copiados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label_copiando.Text = "Archivos han sido copiados al 100%";
                }
            }
            else
            {
                MessageBox.Show("No has introducido ningún bono", "Inserte un Bono", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CopyNCfromTo(string sourceDir, string targetDir, string marca, string cantidadMarca)
        {
            //JMRM 23/06/2020 se hace esto para que pueda abrir archivos con puntos.
            marca = marca.Replace(".", string.Empty);
                string path = sourceDir + marca + ".NC";
                bool result = System.IO.File.Exists(path);
                if (result == true)
                {
                    string destinationPath = targetDir + marca + ".NC";

                    //corregirLineaDoblezNC(path);

                    System.IO.File.Copy(path, destinationPath, true);


                    /*using (StreamWriter sw = File.OpenText(destinationPath))
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            textolinea = sr.ReadLine();
                            if (i == 7)
                            {
                                sr.write
                            }
                        }
                    }*/

                    if (cantidadMarca != "")
                    {
                        string[] arrLine = File.ReadAllLines(destinationPath);
                        if (arrLine.Length >= 7)
                        {
                            arrLine[7] = "  " + cantidadMarca;
                            File.WriteAllLines(destinationPath, arrLine);
                        }
                    }

                }
        }

        private string buscarCarpetaContieneNC(string familia)
        {
            //string fullName = "";
            //string fullName = @"\\srvfiles01\Piezas.oficina\";
            string fullName = @"\\nas01\Piezas.oficina\";
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(fullName);
            FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos(familia + "*");

            foreach (FileSystemInfo foundFile in filesAndDirs)
            {
                fullName = foundFile.FullName + @"\";

                Console.WriteLine(fullName);
            }

            //MessageBox.Show("Carpeta encontrada es: " + fullName, "Niceeee", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return fullName;
        }

        #region Consultas a la Base de Datos GG
        /*
        private List<string> getNCsFromBono()
        {
            string bono = bono_tb.Text;
            List<String> ncs = new List<String>();

            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                string query = "Select MARCA from [gg].[dbo].[TC_BONO] bono join [gg].[dbo].[TC_BONOL] bonol on bono.BONO = bonol.BONO where bono.BONO = '" + bono + "';";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ncs.Add(reader.GetString(0));
                        }
                    }
                }

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return ncs;
        }
        */

        //ANGEL GARCIA - 25/03/2024 -> Se cambia el output de lista a DataTable para sacar la cantidad a la vez que la marca
        private DataTable getNCsFromBono()
        {
            string bono = bono_tb.Text;
            DataTable ncs = new DataTable();


            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                string query = "Select MARCA, bonol.CANTIDAD ";

                if (chkPrePaquetes.Checked)
                {
                    query += ",(select paquete from TC_LANZA_ETIQUETA where TC_LANZA_ETIQUETA.OrdFab = bonol.ordfab) as paquete ";
                }

                query += " from [gg].[dbo].[TC_BONO] bono join [gg].[dbo].[TC_BONOL] bonol on bono.BONO = bonol.BONO where bono.BONO = '" + bono + "';";
                SqlCommand comando = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                comando.CommandTimeout = 180;
                adapter.Fill(ncs);

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return ncs;
        }


        private DataTable getNCsFromProyecto()
        {
            string proyecto = this.textBoxProy.Text;
            DataTable ncs = new DataTable();

            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                string query = "Select distinct ofab.CODIGO, art.CATEGORIA, cat.DENOMI, ofab.CTDLAN  ";

                if (chkPrePaquetes.Checked)
                {
                    query += ",	(select paquete from TC_LANZA_ETIQUETA where TC_LANZA_ETIQUETA.OrdFab = ofab.ordfab) as paquete ";
                }

                query += " from [gg].[dbo].[T_ORDFAB] ofab left outer join [gg].[dbo].[T_ARTICULOS] art ON ofab.CODIGO = art.CODIGO";
                query += " left outer join [gg].[dbo].[T_CATEGORIAS] cat ON art.CATEGORIA = cat.CATEGO"; 
                query += " where ofab.PROYECTO = '" + proyecto + "' and ofab.CODEMP = " + empresaGlobal.empresaID;

                SqlCommand comando = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                adapter.Fill(ncs);

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return ncs;
        }

        private DataTable getNCsChapas()
        {
            string proyecto = this.textBoxProy.Text;
            DataTable ncs = new DataTable();

            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                string query = @"SELECT distinct 
	                                /*T_ORDFAB.ORDFAB,*/
	                                CODIGO, 
	                                CTDLAN";

                if (chkPrePaquetes.Checked)
                {
                    query += ",	(select paquete from TC_LANZA_ETIQUETA where TC_LANZA_ETIQUETA.OrdFab = t_ordfab.ordfab) as paquete ";
                }

                query += @" FROM 
	                            T_ORDFAB 
	                            INNER JOIN T_ORDFABO 
		                            ON T_ORDFAB.CODEMP = T_ORDFABO.CODEMP 
		                            AND T_ORDFAB.TIPOREG = T_ORDFABO.TIPOREG 
		                            AND T_ORDFAB.ORDFAB = T_ORDFABO.ORDFAB 
                            WHERE 
	                            T_ORDFAB.CODEMP = '3' 
	                            AND T_ORDFAB.TIPOREG = 'F' 
	                            AND t_ordfab.FLAG = '0' 
	                            and (T_ORDFABO.OPERAC = dbo.IME_GetOpCizalla() OR T_ORDFABO.OPERAC = dbo.IME_GetOpEsap() OR T_ORDFABO.OPERAC = dbo.IME_GetOpPlasma()) 
	                            AND T_ORDFAB.PROYECTO = '" + proyecto + @"' 
	                            AND NOT EXISTS ( SELECT * FROM TC_BONOL WHERE T_ORDFABO.CODEMP = TC_BONOL.CODEMP AND T_ORDFABO.TIPOREG = TC_BONOL.TIPOREG AND T_ORDFABO.ORDFAB = TC_BONOL.ORDFAB AND T_ORDFABO.CORREL = TC_BONOL.CORREL) ";

                if (radioSI.Checked)
                {
                    query += @"AND dbo.IME_FormaParteCJS('" + empresaGlobal.empresaID + "', T_ORDFAB.CODIGO, '0') = 1 ";
                }

                if (radioNO.Checked)
                {
                    query += @"AND dbo.IME_FormaParteCJS('" + empresaGlobal.empresaID + "', T_ORDFAB.CODIGO, '0') = 0 ";
                } 

                SqlCommand comando = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                adapter.Fill(ncs);

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return ncs;
        }



        private string getFamiliaNC(string marca)
        {

            string strSql = "";
            SqlCommand sqlCmd;
            SqlConnection conexion = new SqlConnection(connetionString);
            string familia = "";

            //List<string> nameofnC = new List<string>();
            //nameofnC = getNCsFromBono();
            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                //En la base de datos de desarrollo no se muestra las nuevas Plantas de Galvanizado como es MADe
                strSql = "select [gg].[dbo].IME_GetFamilia_Articulo('" + empresaGlobal.empresaID + "','" + marca + "');";
                sqlCmd = new SqlCommand(strSql, conexion);
                familia = (string)sqlCmd.ExecuteScalar();


            }

            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return familia;
        }
        #endregion

        private void dataGridView_marcasCopiadas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string path = @"\\172.16.2.3\MDT_Adf$SPPLM_Bonos_CN\" + bono_tb.Text + @"\";
            if (e.ColumnIndex == 0)
            {
                path += dataGridView_marcasCopiadas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //JMRM 23/06/2020 se hace esto para que pueda abrir archivos con puntos.
                path = path.Replace(".", string.Empty);
                path += ".NC";


                Process.Start("notepad.exe", path);
            }
            //Console.WriteLine("La fila pulsada es: " + e.RowIndex + " y la columna pulsada es " + e.ColumnIndex);
        }

        private void radioButtonMade_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonIme.Checked = !radioButtonMade.Checked;
        }

        private void radioButtonIme_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonMade.Checked = !radioButtonIme.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBoxProy.Text != "")
            {
                if (!chkChapas.Checked)
                {
                    string path = "";
                    /*if (radioButtonMade.Checked == true)
                    {
                        path = @"\\172.16.2.3\MDT_Adf$SPPLM_Bonos_CN\" + bono_tb.Text;
                    }
                    else
                    {
                        path = @"X:\BonosBuscadorNC\" + bono_tb.Text;
                    }*/

                    FolderBrowserDialog folderbrowser = new FolderBrowserDialog();

                    using (var fbd = new FolderBrowserDialog())
                    {
                        DialogResult result = fbd.ShowDialog();

                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            path = fbd.SelectedPath;
                        }
                    }

                    dataGridView_marcasCopiadas.Rows.Clear();
                    label_copiando.Text = "Copiando Archivos ...";
                    //\\vmsrvapps\MDT_Adf$SPPLM_Bonos_CN
                    //\\172.16.2.3\MDT_Adf$SPPLM_Bonos_CN

                    //Definción de Variables
                    //Familia obtenida
                    string familia = "";

                    //Donde están los NCs que hay que copiar pathNCs
                    string pathNCs = "";

                    //string path_alt = @"\\vmsrvapps\MDT_Adf$SPPLM_Bonos_CN";


                    path += @"\" + this.textBoxProy.Text;
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    path += @"\";
                    MessageBox.Show("Directorio Creado con éxito en " + path, "Directorio creado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Obtengo todas las marcas de ese bono
                    DataTable ncs = new DataTable();
                    ncs = getNCsFromProyecto();

                    //ProgressBar
                    progressBar_copyFiles.Visible = true;
                    // Set Minimum to 1 to represent the first file being copied.
                    progressBar_copyFiles.Minimum = 1;
                    // Set Maximum to the total number of files to copy.
                    progressBar_copyFiles.Maximum = ncs.Rows.Count;
                    // Set the initial value of the ProgressBar.
                    progressBar_copyFiles.Value = 1;
                    // Set the Step property to a value of 1 to represent each file being copied.
                    progressBar_copyFiles.Step = 1;

                    //ProgressBar

                    string tempdir = "";
                    foreach (DataRow row in ncs.Rows)
                    {
                        //obtengo la familia del bono
                        familia = getFamiliaNC(row[0].ToString());

                        //busco la carpeta donde se almacenan estos NC
                        pathNCs = buscarCarpetaContieneNC(familia);

                        //Se coge el NC correspondiente de ese directorio

                        tempdir = path + row[2].ToString();

                        if (!Directory.Exists(tempdir))
                        {
                            Directory.CreateDirectory(tempdir);
                        }

                        tempdir += @"\";

                        

                        CopyNCfromTo(pathNCs, tempdir, row[0].ToString(), row[3].ToString());
                        //listView_marcas.Items.Add(ncs[i].ToString());
                        this.dataGridView_marcasCopiadas.Rows.Add(row[0].ToString());
                        //listView_marcas

                        progressBar_copyFiles.PerformStep();

                    }
                    marcas_copiadas.Visible = true;
                    dataGridView_marcasCopiadas.Visible = true;
                    MessageBox.Show("Los archivos NC han sido copiados", "Archivos copiados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label_copiando.Text = "Archivos han sido copiados al 100%";
                }
                else
                {
                    string path = "";
                    /*if (radioButtonMade.Checked == true)
                    {
                        path = @"\\172.16.2.3\MDT_Adf$SPPLM_Bonos_CN\" + bono_tb.Text;
                    }
                    else
                    {
                        path = @"X:\BonosBuscadorNC\" + bono_tb.Text;
                    }*/

                    FolderBrowserDialog folderbrowser = new FolderBrowserDialog();

                    using (var fbd = new FolderBrowserDialog())
                    {
                        DialogResult result = fbd.ShowDialog();

                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            path = fbd.SelectedPath;
                        }
                    }

                    dataGridView_marcasCopiadas.Rows.Clear();
                    label_copiando.Text = "Copiando Archivos ...";
                    //\\vmsrvapps\MDT_Adf$SPPLM_Bonos_CN
                    //\\172.16.2.3\MDT_Adf$SPPLM_Bonos_CN

                    //Definción de Variables
                    //Familia obtenida
                    string familia = "";

                    //Donde están los NCs que hay que copiar pathNCs
                    string pathNCs = "";

                    //string path_alt = @"\\vmsrvapps\MDT_Adf$SPPLM_Bonos_CN";


                    path += @"\" + this.textBoxProy.Text;
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    path += @"\";
                    MessageBox.Show("Directorio Creado con éxito en " + path, "Directorio creado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Obtengo todas las marcas de ese bono
                    DataTable ncs = new DataTable();
                    ncs = getNCsChapas();

                    //ProgressBar
                    progressBar_copyFiles.Visible = true;
                    // Set Minimum to 1 to represent the first file being copied.
                    progressBar_copyFiles.Minimum = 1;
                    // Set Maximum to the total number of files to copy.
                    progressBar_copyFiles.Maximum = ncs.Rows.Count;
                    // Set the initial value of the ProgressBar.
                    progressBar_copyFiles.Value = 1;
                    // Set the Step property to a value of 1 to represent each file being copied.
                    progressBar_copyFiles.Step = 1;

                    //ProgressBar

                    string tempdir = "";
                    foreach (DataRow row in ncs.Rows)
                    {
                        //obtengo la familia del bono
                        familia = getFamiliaNC(row[0].ToString());

                        //busco la carpeta donde se almacenan estos NC
                        pathNCs = buscarCarpetaContieneNC(familia);

                        //Se coge el NC correspondiente de ese directorio

                        tempdir = path;// +row[2].ToString();

                        if (!Directory.Exists(tempdir))
                        {
                            Directory.CreateDirectory(tempdir);
                        }

                        tempdir += @"\";

                        CopyNCfromTo(pathNCs, tempdir, row[0].ToString(), row[1].ToString());
                        //listView_marcas.Items.Add(ncs[i].ToString());
                        this.dataGridView_marcasCopiadas.Rows.Add(row[0].ToString());
                        //listView_marcas

                        progressBar_copyFiles.PerformStep();

                    }
                    marcas_copiadas.Visible = true;
                    dataGridView_marcasCopiadas.Visible = true;
                    MessageBox.Show("Los archivos NC han sido copiados", "Archivos copiados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label_copiando.Text = "Archivos han sido copiados al 100%";
                }
                
            }
            else
            {
                MessageBox.Show("No has introducido ningún bono", "Inserte un Bono", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            

        }

        private void dataGridView_marcasCopiadas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkChapas_CheckedChanged(object sender, EventArgs e)
        {
            
            this.gbCJSChapas.Visible = chkChapas.Checked;
            
        }

        private void bono_tb_TextChanged(object sender, EventArgs e)
        {
            this.chkChapas.Checked = false;
        }


        private void corregirLineaDoblezNC(string path)
        {


            bool doblezEncontrada = false;
            int contadorLineas = 0;
            bool procesado = false;

            string nuevaLinea = "";

            string[] arrLine = {""};

            using (var fileStream = File.OpenRead(path))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 128))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    // Process line
                    if (line.StartsWith("KA"))
                    {
                        doblezEncontrada = true;
                    }
                    else if (doblezEncontrada == true)
                    {
                        if (line.Length < 3)
                        {
                            break;
                        }
                        else
                        {
                            int counter = 0;
                            bool resetChar = false;
                            foreach (char c in line)
                            {
                                //if (char != "" and char != " "
                                if (!char.IsWhiteSpace(c))
                                {
                                    if (!resetChar)
                                    {
                                        counter++;
                                    }
                                    resetChar = true;
                                }
                                else
                                {
                                    resetChar = false;
                                    if (counter == 5)
                                    {
                                        if (!procesado)
                                        {
                                            arrLine = File.ReadAllLines(path);
                                            procesado = true;
                                        }
                                        arrLine[contadorLineas] = nuevaLinea;
                                        break;
                                    }
                                }

                                nuevaLinea += c;
                            }
                        }

                    }

                    nuevaLinea = "";
                    contadorLineas++;
                }

            }

            if (procesado) File.WriteAllLines(path, arrLine);
        }


    }
}
