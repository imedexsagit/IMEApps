using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;

namespace ModificarNC
{
    public partial class ModificarNCyCAM : Form
    {
        string[] cons = new string[] { "BO", "EO", "SI", "AK", "EN", "KA", "E0", "B0", "E1", "B1", "PU", "E1","E2"};

        String[] marcas = new String[1000];
       

        public ModificarNCyCAM()
        {
            InitializeComponent();
        }

        private void btnSetPathNC_Click(object sender, EventArgs e)
        {
            txtPathNC.Text = "Z:\\xsteel\\INTERNACIONAL\\ALEMANIA";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = txtPathNC.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPathNC.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnGenerarFicherosNC_Click(object sender, EventArgs e)
        {
           

           /* Cursor.Current = Cursors.WaitCursor;
            String marcaFichero = "";
            String path = "C:\\";
            String[] arrayBo = new String[1000];

            string newpatch = "";

            try
            {
                if (txtPathNC.Text != null && txtPathNC.Text != "")
                    path = txtPathNC.Text;

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);

                Directory.CreateDirectory(path + "modificarNC");
                foreach (System.IO.FileInfo fi in di.GetFiles("*.NC"))
                {
                    for (int a = 0; a < 100; a++)
                    {
                        arrayBo[a] = "";
                    }

                    marcaFichero = fi.Name.Split('.')[0];
                    newpatch = path + "modificarNC\\" + prefijo.Text + marcaFichero + ".nc";
                    string fichero = path + "\\" + fi;

                    StreamReader sr = new StreamReader(fichero); //Abro el fichero

                    FileStream fs = File.Create(newpatch); //Creo el fichero

                    string line = sr.ReadLine();
                    int numlinea = 1;
                    //leo linea o linea
                    int borrar = 0, Bo = 0, B1 = 0, contador = 0;

                    while (line != null)
                    {
                        for (int i = 0; i < cons.Length; i++)
                        {

                            if (line == cons[i])
                            {

                                if (line == "E0" || line == "EO")
                                {
                                    borrar = 1;
                                }
                                else
                                {
                                    borrar = 0;
                                }

                                if (line == "B0" || line == "BO" || line == "B1")
                                {
                                    if (B1 == 1) B1 = 2;
                                    if (B1 == 0) B1 = 1;
                                    Bo = 1;
                                }
                                else
                                {
                                    Bo = 0;
                                }
                            }
                        }

                        if (borrar == 1)
                        {
                        }
                        else
                        {
                            if (Bo == 1)
                            {
                                if (B1 == 1)
                                {
                                    if (line == "B0")
                                    {
                                        line = "BO";
                                    }

                                    arrayBo[contador] = line;
                                    contador++;
                                }

                                if (B1 == 2)
                                {
                                    if (line == "B0" || line == "BO")
                                    {
                                        arrayBo[contador] = "BO";
                                        contador++;
                                    }
                                    else
                                    {

                                        string comprobarU = line.Substring(0, 5).Replace(" ", "");

                                        if (comprobarU == "h" || comprobarU == "u")
                                        {
                                            string nuevolinea = line;
                                            arrayBo[contador] = nuevolinea;
                                            contador++;
                                        }
                                        else
                                        {
                                            string nuevolinea = "  u" + line.Replace("-", " ").Remove(0, 1);
                                            arrayBo[contador] = nuevolinea;
                                            contador++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (numlinea == 4 || numlinea == 5)
                                {
                                    byte[] info = new UTF8Encoding(true).GetBytes("  "+prefijo.Text+line.Replace(" ","") + "\r\n");
                                    fs.Write(info, 0, info.Length);
                                }
                                else {

                                    if (line == "  S355J2+N")
                                    {
                                        line = "  S355J2N";
                                    }

                                    if (line == "EN")
                                    {
                                    }
                                    else
                                    {
                                        byte[] info = new UTF8Encoding(true).GetBytes(line + "\r\n");
                                        fs.Write(info, 0, info.Length);
                                    }
                                }
                            }
                        }

                        //Leo la nueva linea
                        line = sr.ReadLine();
                        numlinea++;
                    }

                    int contadorBo = 0;

                    while (arrayBo[contadorBo] != "")
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(arrayBo[contadorBo] + "\r\n");
                        fs.Write(info, 0, info.Length);
                        contadorBo++;
                    }

                    line = "EN";

                    byte[] info2 = new UTF8Encoding(true).GetBytes(line+ "\r\n");
                    fs.Write(info2, 0, info2.Length);
                    //Cierro el fichero
                    sr.Close();
                    fs.Close();
                   
                }
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Los archivos NC han sido modificados y generados en la ruta: " + path + "modificarCAM");
            }



            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message);
            }
            
            */
        }

       

        private void btnModificarFicherosCAM_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String marcaFichero = "";
            String path = "C:\\";
            string newpatch = "";

           

            try
            {

                path = textBox1.Text;
              
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);

                Directory.CreateDirectory(path + "modificarCAM");

                foreach (System.IO.FileInfo fi in di.GetFiles("*.CAM"))
                {

                    marcaFichero = fi.Name.Split('.')[0];

                     String [] operaciones = ObtenerOperacionesSegundarias(marcaFichero);

                     if (operaciones[0] == "")
                     {
                         // Comprobamos con el . al final de la marca
                         operaciones = ObtenerOperacionesSegundarias(marcaFichero + ".");
                     }

                     newpatch = path + "modificarCAM\\" + marcaFichero + ".CAM";
                    string fichero = path + "\\" + fi;

                    StreamReader sr = new StreamReader(fichero); //Abro el fichero

                   // FileStream fs = File.Create(newpatch); //Creo el fichero
                    System.IO.StreamWriter fs = new System.IO.StreamWriter(newpatch, false, Encoding.Default); // Creo el fichero

                    string line = sr.ReadLine();

                    //leo linea o linea


                    while (line != null)
                    {

                        if (line == "NUM_COM:") //proyecto
                        {
                            line = line + txtOFProyecto.Text;
                        }

                        if (line == "DES_COM:") //Descripcion
                        {
                            line = line + txtOFDescripcion.Text;
                        }

                        if (line == "CLI_COM:") //Diseñador
                        {
                            line = line + txtOFDiseñador.Text;
                        }

                        if (line == "IND_COM:") //Dibujo
                        {
                            line = line + txtOFDibujo.Text;
                        }

                        if (line == "DAT_COM:") //Serie
                        {
                            line = line + txtOFSerie.Text;
                        }

                        if (line == "MAT_PRO:S355J2N")
                        {
                            if (AM.Checked == true) line = "MAT_PRO:S355J2NAM";
                            if (DB.Checked == true) line = "MAT_PRO:S355J2NDB";
                        }

                        if (line == "MAT_PRO:S355J2N")
                        {
                            if (AM.Checked == true) line = "MAT_PRO:S355J2NAM";
                            if (DB.Checked == true) line = "MAT_PRO:S355J2NDB";
                        }

                        if (line == "MAT_PRO:S355J2")
                        {
                            if (AM.Checked == true) line = "MAT_PRO:S355J2AM";
                            if (DB.Checked == true) line = "MAT_PRO:S355J2DB";
                        }

                        if (line == "MAT_PRO:S355J2C")
                        {
                            if (AM.Checked == true) line = "MAT_PRO:S355J2CAM";
                            if (DB.Checked == true) line = "MAT_PRO:S355J2CDB";
                        }

                        if (line == "PROGRAM:TecnoVIEWER")
                        {
                            //Añado las operaciones si existen.
                            fs.WriteLine(line);

                            int añadir = 1;
                            string newline = "";
                            for (int i = 0; i < operaciones.Length; i++)
                            {

                                if (operaciones[i] != "")
                                {
                                    newline = "NOT_00" + Convert.ToString(añadir) + ":0,3,4,4,50.0," + Convert.ToString(añadir + 2) + "5," + operaciones[i];

                                    fs.WriteLine(newline);
                                    añadir++;
                                }

                            }

                            //Leo la nueva linea
                            line = sr.ReadLine();

                        }
                        else
                        {

                            fs.WriteLine(line);

                            //Leo la nueva linea
                            line = sr.ReadLine();
                        }

                    }

                    //Cierro el fichero
                    sr.Close();
                    fi.Delete();
                    fs.Close();
                    
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message);
            }
        }

        private void AM_CheckedChanged(object sender, EventArgs e)
        {
            if (DB.Checked == true)
            {
                DB.Checked = false;
            }
        }

        private void DB_CheckedChanged(object sender, EventArgs e)
        {
            if (AM.Checked == true)
            {
                AM.Checked = false;
            }
        }


        private void modificarCN(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            String marcaFichero = "";
            String path = "C:\\";
            String[] arrayBo = new String[1000];
            float[] resultadosChapa = new float[3];

            string newpatch = "";

            if (txtPathNC.Text != null && txtPathNC.Text != "")
                path = txtPathNC.Text;

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);

            Directory.CreateDirectory(path + "modificarNC");

            //Recorro la carpeta buscando todos los NC que contegan.
            int contadorFicheros = 0;

            foreach (System.IO.FileInfo fi in di.GetFiles("*.NC"))
            {
                marcas[contadorFicheros] = prefijo.Text + fi.Name.Split('.')[0];
                contadorFicheros++;
                resultadosChapa = esChapa(fi, path); // Compruebo si es chapa.
                crearCN(fi, path, resultadosChapa);

            }

            for (int c = 0; c < 1000; c++)
            {
                marcas[c] = "";
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Los archivos NC han sido generados y modificados en la ruta: " + path + "modificarNC");
        }

        private float[] esChapa(FileInfo fi, String path)
        {

            //Proceso el fichero actual.

            String marcaFichero = fi.Name.Split('.')[0];
            string fichero = path + "\\" + fi;
            string lineChapa = "";
            Boolean esChapa = false, datosAk=false;
            float maxAncho = 0, maxLargo = 0;

            float[] resultados = { 0, 0, 0 };

            String[] tamAgujeros = new String[100];

            StreamReader sr = new StreamReader(fichero); //Abro el fichero

            string line = sr.ReadLine();
            int numlinea = 1;
            //leo linea o linea
            

            for (int a = 0; a < 100; a++) //Elimino las lineas del fichero anterior.
            {
                tamAgujeros[a] = "";
            }

            while (line != null)
            {
                switch (line)
                {
                    case "AK":
                        // Seleciono el ancho y largo mayor.
                        datosAk = true;
                        break;
                    case "BO":
                        datosAk = false;
                        break;
                    case "B0":
                        datosAk = false;
                        break;
                    case "EO":
                        datosAk = false;
                        break;
                    case "E0":
                        datosAk = false;
                        break;
                    case "EN":
                        datosAk = false;
                        break;
                    case "B1":
                        datosAk = false;
                        break;
                    case "E1":
                        datosAk = false;
                        break;
                    case "B2":
                        datosAk = false;
                        break;
                    case "E2":
                        datosAk = false;
                        break;
                    case "KA":
                        datosAk = false;
                        break;
                    case "PU":
                        datosAk = false;
                        break;
                    case "SI":
                        datosAk = false;
                        break;

                    default:

                        if (numlinea == 8)
                        { //Compruebo si es una chapa.


                            if (line != "" &&  line.Length > 5) lineChapa = line.Substring(1, 4).Replace(" ", "");
                            if (lineChapa == "BL" ) esChapa = true;
                            if (lineChapa == "Bl") esChapa = true;
                            if (lineChapa == "Bl.") esChapa = true;
                        }

                        if (datosAk == true && esChapa == true)
                        {
                            string newline = line.Replace("  ", " ").Replace("   ", " ").Replace("    ", " ");
                            string[] words = newline.Split(' ');

                            //Obtengo el ancho
                            int indiceAncho = 3;
                            if (words[3] == "")
                            {
                                indiceAncho = 2;
                            }

                            string letraAncho = words[indiceAncho].Substring(words[indiceAncho].Length - 1, 1);
                            float comAncho = -1;

                            if (letraAncho == "u" || letraAncho == "v" || letraAncho == "o" || letraAncho == "h")
                            {
                                comAncho = float.Parse(words[indiceAncho].Remove(words[indiceAncho].Length - 1).Replace(".", ","));
                            }
                            else
                            {
                                comAncho = float.Parse(words[indiceAncho].Replace(".", ","));
                            }

                            //Obtengo el largo
                            int indiceLargo = 5;
                            if (words[5] == "")
                            {
                                indiceLargo = 4;
                            }

                            string letraLargo = words[indiceLargo].Substring(words[indiceLargo].Length - 1, 1);
                            float comLargo = -1;

                            if (letraLargo == "u" || letraLargo == "v" || letraLargo == "o" || letraLargo == "h")
                            {
                                comLargo = float.Parse(words[indiceLargo].Remove(words[indiceLargo].Length - 1).Replace(".", ","));
                            }
                            else
                            {
                                comLargo = float.Parse(words[indiceLargo].Replace(".", ","));
                            }

                            if (maxAncho < comAncho) maxAncho = comAncho;
                            if (maxLargo < comLargo) maxLargo = comLargo;

                        }
                        
                        break;
                }

                line = sr.ReadLine();
                numlinea++;

            }

            resultados[0] = Convert.ToInt16(esChapa);
            resultados[1] = maxAncho;
            resultados[2] = maxLargo;
            sr.Close();

            return resultados;
        }

        private void crearCN(FileInfo fi, String path, float[] resultados)
        {
            string marcaFichero = fi.Name.Split('.')[0];
            string newpatch = path + "modificarNC\\" + prefijo.Text + marcaFichero + ".nc";
            string fichero = path + "\\" + fi;
            String[] arrayBo = new String[500];

            for (int a = 0; a < 500; a++)
            {
                arrayBo[a] = "";
            }

            StreamReader sr = new StreamReader(fichero); //Abro el fichero
            FileStream fs = File.Create(newpatch); //Creo el fichero

            string line = sr.ReadLine(); // Leo linea a linea.
            
            int numlinea = 1;
            //leo linea o linea
            int borrar = 0, Bo = 0, B1 = 0, contador = 0;

            while (line != null)
            {
                for (int i = 0; i < cons.Length; i++)
                {

                    if (line == cons[i])
                    {

                        if (line == "E0" || line == "EO" || line == "PU" || line == "E1" || line == "E2")
                        {
                            borrar = 1;
                        }
                        else
                        {
                            borrar = 0;
                        }

                        if (line == "B0" || line == "BO" || line == "B1" || line == "B2")
                        {
                            //Compruebo si tiene algun ala asignada y en caso de que no, se la asigno automaticamente al ala u.
                            if (line == "B1" || line == "B2")
                            {
                                MessageBox.Show("Posibles errores en los taladros de la marca  "+fi+" Se recomienda revisar el archivo NC");
                            }
                            if (B1 == 1) B1 = 2;
                            if (B1 == 0) B1 = 1;
                            Bo = 1;
                        }
                        else
                        {
                            Bo = 0;
                        }
                    }
                }

                if (borrar == 1)
                {
                }
                else
                {
                    if (Bo == 1)
                    {
                        if (B1 == 1)
                        {
                            if (line == "B0")
                            {
                                line = "BO";
                            }

                            arrayBo[contador] = line;
                            contador++;
                        }

                        if (B1 == 2)
                        {
                            if (line == "B0" || line == "BO" || line == "B1" || line == "B2")
                            {
                                arrayBo[contador] = "BO";
                                contador++;
                            }
                            else
                            {

                                string comprobarU = line.Replace(" ", "");

                                string nuevaprueba = comprobarU.Substring(2,1);


                                if (nuevaprueba == "h" || nuevaprueba == "u" || nuevaprueba == "v")
                                {
                                    string nuevolinea = line;
                                    arrayBo[contador] = nuevolinea;
                                    contador++;
                                }
                                else
                                {
                                    if (resultados[0] == 1)
                                    {
                                        string nuevolinea = line.Replace("-", " ").Remove(0, 1);
                                        arrayBo[contador] = nuevolinea;
                                        contador++;
                                    }
                                    else
                                    {
                                        string nuevolinea = line.Replace("-", " ").Remove(0, 1);
                                        arrayBo[contador] = nuevolinea;
                                        contador++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (numlinea == 4 || numlinea == 5)
                        {

                            byte[] info = new UTF8Encoding(true).GetBytes("  " + prefijo.Text + line.Replace(" ", "") + "\r\n");
                            fs.Write(info, 0, info.Length);
                        }
                        else
                        {
                            if(numlinea == 10 && resultados[0] == 1 ){

                                if (resultados[1] == 0)
                                {

                                }
                                else
                                {
                                    line = "    " + Convert.ToString(resultados[1]).Replace(",", ".");
                                }

                            }

                            if (numlinea == 11 && resultados[0] == 1)
                            {

                                if (resultados[2] == 0)
                                {

                                }
                                else
                                {
                                    line = "    " + Convert.ToString(resultados[2]).Replace(",", ".");
                                }

                            }

                            if (line == "  S355J2+N")
                            {
                                line = "  S355J2N";
                            }

                            if (line == "EN")
                            {
                            }
                            else
                            {
                                byte[] info = new UTF8Encoding(true).GetBytes(line + "\r\n");
                                fs.Write(info, 0, info.Length);
                            }
                        }
                    }
                }

                //Leo la nueva linea
                line = sr.ReadLine();
                numlinea++;
            }

            int contadorBo = 0;
            string prueba = "";
            while (arrayBo[contadorBo] != "")
            {
                
                byte[] info = new UTF8Encoding(true).GetBytes(arrayBo[contadorBo] + "\r\n");
                fs.Write(info, 0, info.Length);
                contadorBo++;
            }

            line = "EN";

            byte[] info2 = new UTF8Encoding(true).GetBytes(line + "\r\n");
            fs.Write(info2, 0, info2.Length);
            //Cierro el fichero
            sr.Close();
            fs.Close();

        }

        // Manejador para ventana de aplicación
        [System.Runtime.InteropServices.DllImport("USER32.DLL", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activar una ventana de aplicación
        [System.Runtime.InteropServices.DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        //Para restaurar las ventanas
        [System.Runtime.InteropServices.DllImport("USER32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private void abrirVisor()
        {
            String pathCN = "";
            try
            {
                IntPtr visorHandle = FindWindow(null, "Sin título - CAM-DSTV Viewer");
                if (visorHandle == IntPtr.Zero)
                {
                    var process = Process.Start(@"C:\Apps\Steel\CAM-DSTV Viewer\CAM-DSTV-VIEWER.exe");
                    do
                    {
                        System.Threading.Thread.Sleep(100);
                        visorHandle = FindWindow(null, "Sin título - CAM-DSTV Viewer");

                    } while (visorHandle == IntPtr.Zero);
                }
                /*else
                {*/
                //Construimos un string con todas las marcas.

                ShowWindow(visorHandle, 1);  // Visualiza la ventana si está escondida
                ShowWindow(visorHandle, 9);  // Restaura la ventana
                SetForegroundWindow(visorHandle);  // Activa la ventana
                SendKeys.SendWait("^a");
                System.Threading.Thread.Sleep(1000);

                SendKeys.SendWait("C:\\"); //Ponemos el path
                System.Threading.Thread.Sleep(100);
                SendKeys.SendWait("%a"); //Abrimos


                pathCN = textBox1.Text + "\\*.NC";

                System.Threading.Thread.Sleep(1000);

                /*                     
                 El signo más (+), símbolo de intercalación (^), signo de porcentaje (%), tilde (~) y paréntesis () tienen significados especiales en SendKeys.Para especificar uno de estos caracteres, 
                 inclúyalo entre llaves ({}). Por ejemplo, para especificar el signo más, utilice "{+}".Para especificar caracteres de llave, utilice "{{}" y "{}}".Corchetes ([]) no tienen ningún 
                 significado especial para SendKeys, pero también deben escribirse entre llaves.En otras aplicaciones, los corchetes tienen un significado especial que puede ser importante cuando 
                 se produce el intercambio dinámico de datos (DDE).                        
                */

                pathCN = pathCN.Replace("(", "{(}");
                pathCN = pathCN.Replace(")", "{)}");
                pathCN = pathCN.Replace("+", "{+}");
                pathCN = pathCN.Replace("^", "{^}");
                pathCN = pathCN.Replace("%", "{%}");
                pathCN = pathCN.Replace("~", "{~}");
                pathCN = pathCN.Replace("[", "{[}");
                pathCN = pathCN.Replace("]", "{]}");

                SendKeys.SendWait(pathCN); //Ponemos el path
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait("%a"); //Abrimos
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait("^e"); //Seleccionamos todos los ficheros
                System.Threading.Thread.Sleep(1000);
                SendKeys.SendWait("%a"); //Abrimos                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message);
            }

        }


        private void moverCAM(string path)
        {
            String marcaFichero = "";
            String marca = "";
            String pathCAMVisor = @"C:\Apps\Steel\CAM-DSTV Viewer\CAM";
            String ficheroCAMOrigen = "";
            String pathficheroCAMDestino = "";

            try
            {
                

                //Obtengo o genero los ficheros CAM
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(pathCAMVisor);
                foreach (System.IO.FileInfo fi in di.GetFiles("*.CAM"))
                {
                    marcaFichero = fi.Name.Split('.')[0];

                    //Se podría hacer con LINQ
                    int contadorMarcas = 0;
                    string m = marcas[contadorMarcas];
                    while(m != null){
                        marca = m;
                        marca = marca.Replace(".", "");
                        if (marca == marcaFichero)
                        {
                            //Copiamos el fichero
                            ficheroCAMOrigen = fi.Name;
                            pathficheroCAMDestino = System.IO.Path.Combine(path + "\\", ficheroCAMOrigen);
                            System.IO.File.Copy(fi.FullName, pathficheroCAMDestino, true);
                            //dgr.Cells["ficheroCAMOF"].Value = pathficheroCAMDestino;
                             
                        }

                        contadorMarcas++;
                        m = marcas[contadorMarcas];
                    }

                    fi.Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message);
            }

        }


        private String[] ObtenerOperacionesSegundarias(string marca)
        {
            // Creo conexion y la base de datos.
            SqlConnection conexion = new SqlConnection("Data Source=srvsql02;Initial Catalog=gg;uid=gg;pwd=ostia;MultipleActiveResultSets=True");

            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            DataTable table = null;
            int contadorOperaciones = 0;
             String[] operaciones = new String[6];


            for(int i=0;i<6;i++){

                operaciones[i]="";
            }


            



            try
            {
                conexion.Open();

                strsql = " select valor from t_artcar ";
                strsql = strsql + " where codigo='" + marca + ".' and CARACT='17'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                string existe = "";

                for (int i = 0; i < table.Rows.Count; i++) //Mostramos los ensayos
                {
                    existe = table.Rows[i]["valor"].ToString();
                }

                if(existe == "S" ||  existe== "N" || existe == null)
                {
                    operaciones[contadorOperaciones] = "PIEZA SOLDADA";
                    contadorOperaciones++;
                }

                strsql = "";
                strsql = " select valor from t_artcar ";
                strsql = strsql + " where codigo='" + marca + "' and CARACT='17'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                string taladro = "";

                for (int i = 0; i < table.Rows.Count; i++) //Mostramos los ensayos
                {
                    taladro = table.Rows[i]["valor"].ToString();
                }

                

                if (taladro == "S")
                {
                    operaciones[contadorOperaciones] = "TALADRAR";
                    contadorOperaciones++;
                }
                else
                {

                            //Compruebo si la misma marca con el punto al final ( pieza del conjunto soldado) tiene taladro y se lo pongo.
                             strsql = "";
                            strsql = " select valor from t_artcar ";
                            strsql = strsql + " where codigo='" + marca + ".' and CARACT='17'";

                            comando = new SqlCommand(strsql, conexion);
                            adapter = new SqlDataAdapter(comando);
                            table = new DataTable();
                            adapter.Fill(table);

                            for (int i = 0; i < table.Rows.Count; i++) //Mostramos los ensayos
                            {
                                taladro = table.Rows[i]["valor"].ToString();
                            }

                            if (taladro == "S")
                            {
                                operaciones[contadorOperaciones] = "TALADRAR";
                                contadorOperaciones++;
                            }
                }


                //COMPROBAMOS SI TIENE FLEXADO, Y SI ES ASI LO INDICAMOS.

                strsql = "";
                strsql = strsql + "select count(*) from T_RUTAS where CODIGO='" + marca + "' and (OPERAC ='600001' OR OPERAC ='600002' OR OPERAC ='600005')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                int fresar = -1;

                for (int i = 0; i < table.Rows.Count; i++) //Mostramos los ensayos
                {
                    fresar = Convert.ToInt16(table.Rows[i][0].ToString());
                }

                if (fresar == 0) // Si no tiene fresado, probamos con el codigo con punto.
                {
                    strsql = "";
                    strsql = strsql + "select count(*) from T_RUTAS where CODIGO='" + marca + ".' and (OPERAC ='600001' OR OPERAC ='600002' OR OPERAC ='600005')";

                    comando = new SqlCommand(strsql, conexion);
                    adapter = new SqlDataAdapter(comando);
                    table = new DataTable();
                    adapter.Fill(table);

                    int fresarConP = -1;

                    for (int i = 0; i < table.Rows.Count; i++) //Mostramos los ensayos
                    {
                        fresarConP = Convert.ToInt16(table.Rows[i][0].ToString());
                    }

                    if (fresarConP > 0)
                    {
                        operaciones[contadorOperaciones] = "FRESAR TODA LA ARISTA";
                        contadorOperaciones++;
                    }
                }

                if (fresar > 0)
                {
                    operaciones[contadorOperaciones] = "FRESAR TODA LA ARISTA";
                    contadorOperaciones++;
                }

                //COMPROBAMOS SI TIENE DOBLADO O PLEGADO, Y SI ES ASI LO INDICAMOS.

                strsql = "";
                strsql = strsql + "select count(*) from T_RUTAS where CODIGO='" + marca + "' and (OPERAC ='300001' OR OPERAC ='300002')";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                int doblar = -1;

                for (int i = 0; i < table.Rows.Count; i++) //Mostramos los ensayos
                {
                    doblar = Convert.ToInt16(table.Rows[i][0].ToString());
                }

                if (doblar == 0)
                {
                    strsql = "";
                    strsql = strsql + "select count(*) from T_RUTAS where CODIGO='" + marca + ".' and (OPERAC ='300001' OR OPERAC ='300002')";

                    comando = new SqlCommand(strsql, conexion);
                    adapter = new SqlDataAdapter(comando);
                    table = new DataTable();
                    adapter.Fill(table);

                   

                    for (int i = 0; i < table.Rows.Count; i++) //Mostramos los ensayos
                    {
                        doblar = Convert.ToInt16(table.Rows[i][0].ToString());
                    }
                }

                if (doblar > 0)
                {
                    operaciones[contadorOperaciones] = "DOBLADO";
                    contadorOperaciones++;
                }

                //COMPROBAMOS SI ES UNA PIEZA SOLDADA

                strsql = "";
                strsql = strsql + "select T_ARTICULOS.CATEGORIA from T_ESTRUC join T_ARTICULOS on T_ARTICULOS.CODIGO = T_ESTRUC.CONJUN where COMPON='"+ marca + "'";

                comando = new SqlCommand(strsql, conexion);
                adapter = new SqlDataAdapter(comando);
                table = new DataTable();
                adapter.Fill(table);

                int conjsold = -1;

                for (int i = 0; i < table.Rows.Count; i++) //Mostramos los ensayos
                {
                    if (conjsold != 92)
                    {
                        conjsold = Convert.ToInt16(table.Rows[i][0].ToString());
                    }
                }

                if (conjsold ==92)
                {
                    operaciones[contadorOperaciones] = "PIEZA SOLDADA";
                    contadorOperaciones++;
                }


                conexion.Close();
   
                return operaciones;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                return operaciones;
                MessageBox.Show(ex.Message, "ERROR Al OBTENER LAS OPERACIONES. POR FAVOR, CONSULTE CON EL DEPARTEMENTO DE INFORMATICA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return operaciones;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < 1000; i++)
            {
                marcas[i] = null;
            }
            int contadorFicheros = 0;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(textBox1.Text);
            foreach (System.IO.FileInfo fi in di.GetFiles("*.NC"))
            {
                marcas[contadorFicheros] = fi.Name.Split('.')[0];
                contadorFicheros++;
            }

            abrirVisor();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("CIERRE el visor CAM-DSTV-Viewer para continuar con el proceso y pulse ACEPTAR");
            Cursor.Current = Cursors.WaitCursor;
            moverCAM(textBox1.Text);
            btnModificarFicherosCAM_Click(sender, e);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Los archivos CAM han sido generados y modificados en la ruta: " + textBox1.Text + "modificarNC");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Z:\\xsteel\\INTERNACIONAL\\ALEMANIA"; 
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = textBox1.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

       
    }
}
