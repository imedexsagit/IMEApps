using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace InfoSoldaduraPDF
{

   

    public partial class FormDatosSoldadura : Form
    {

        //Para guardar la informacion de la soldadura guardada en los archivos de texto.
        public List<DatosSoldadura> listaDatosSoldadura = new List<DatosSoldadura>();
        public int numSoldaduraActiva = -1;
        //Para guardar la informacion de las soldaduras de los txt.
        public DatosSoldadura datosSoldaduraPantalla = new DatosSoldadura();

        //Fichero para guardar las soldaduras
        string ficheroSoldaduras = Path.GetTempPath() + "\\ficheroSoldaduras.txt";
        

        public FormDatosSoldadura()
        {
            InitializeComponent();                  
            //Leer las soldaduras del fichero de texto.
            LeerFichero();
        }

        private void LeerFichero()
        {
            String line; 
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(ficheroSoldaduras);
                
                line = sr.ReadLine();                
                while (line == "INICIO")
                {
                    DatosSoldadura ds = new DatosSoldadura();
                    //write the lie to console window
                    line = sr.ReadLine();
                    ds.nombre = line;
                    line = sr.ReadLine();
                    ds.text_WPS = line;
                    line = sr.ReadLine();
                    ds.text_I = line;
                    line = sr.ReadLine();
                    ds.text_Hilo = line;
                    line = sr.ReadLine();
                    ds.text_V = line;
                    line = sr.ReadLine();
                    ds.text_Nivel = line;
                    line = sr.ReadLine();
                    ds.text_linea1 = line;
                    line = sr.ReadLine();
                    ds.text_linea2 = line;
                    line = sr.ReadLine();
                    ds.text_linea3 = line;
                    line = sr.ReadLine();//Leemos FIN

                    if (line == "FIN")
                    {
                        listaDatosSoldadura.Add(ds);
                        //Añadirlo al menu
                        ToolStripItem subItem = new ToolStripMenuItem(ds.nombre);
                        subItem.Click += new EventHandler(subItem_Click);
                        listaToolStripMenuItem.DropDownItems.Add(subItem);
                    }

                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                Console.ReadLine();
            }


            catch (Exception e)
            {
                //Console.WriteLine("Exception: " + e.Message);
                MessageBox.Show(e.Message);
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            datosSoldaduraPantalla.nombre = txtNombre.Text;
            datosSoldaduraPantalla.text_WPS = txtWPS.Text;
            datosSoldaduraPantalla.text_I = txtI.Text;
            datosSoldaduraPantalla.text_Hilo = txtHilo.Text;
            datosSoldaduraPantalla.text_V = txtV.Text;
            datosSoldaduraPantalla.text_Nivel = txtNivel.Text;
            datosSoldaduraPantalla.text_linea1 = txtLinea1.Text;
            datosSoldaduraPantalla.text_linea2 = txtLinea2.Text;
            datosSoldaduraPantalla.text_linea3 = txtLinea3.Text;

        }

        private void FormDatosSoldadura_Shown(object sender, EventArgs e)
        {            
            MostrarSoldaduraActiva();            
        }


        private void MostrarSoldaduraActiva(int numSoldadura = -1)
        {
            if (numSoldadura > -1)
            {
                txtNombre.Text = listaDatosSoldadura[numSoldaduraActiva].nombre;
                txtWPS.Text = listaDatosSoldadura[numSoldaduraActiva].text_WPS;
                txtI.Text = listaDatosSoldadura[numSoldaduraActiva].text_I;
                txtHilo.Text = listaDatosSoldadura[numSoldaduraActiva].text_Hilo;
                txtV.Text = listaDatosSoldadura[numSoldaduraActiva].text_V;
                txtNivel.Text = listaDatosSoldadura[numSoldaduraActiva].text_Nivel;
                txtLinea1.Text = listaDatosSoldadura[numSoldaduraActiva].text_linea1;
                txtLinea2.Text = listaDatosSoldadura[numSoldaduraActiva].text_linea2;
                txtLinea3.Text = listaDatosSoldadura[numSoldaduraActiva].text_linea3;
            }
            else
            {
                txtNombre.Text = datosSoldaduraPantalla.nombre;
                txtWPS.Text = datosSoldaduraPantalla.text_WPS;
                txtI.Text = datosSoldaduraPantalla.text_I;
                txtHilo.Text = datosSoldaduraPantalla.text_Hilo;
                txtV.Text = datosSoldaduraPantalla.text_V;
                txtNivel.Text = datosSoldaduraPantalla.text_Nivel;
                txtLinea1.Text = datosSoldaduraPantalla.text_linea1;
                txtLinea2.Text = datosSoldaduraPantalla.text_linea2;
                txtLinea3.Text = datosSoldaduraPantalla.text_linea3;
            }
        }
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosSoldadura ds = new DatosSoldadura();
            ds.nombre = txtNombre.Text;
            ds.text_WPS = txtWPS.Text;
            ds.text_I = txtI.Text;
            ds.text_Hilo = txtHilo.Text;
            ds.text_V = txtV.Text;
            ds.text_Nivel = txtNivel.Text;
            ds.text_linea1 = txtLinea1.Text;
            ds.text_linea2 = txtLinea2.Text;
            ds.text_linea3 = txtLinea3.Text;

            listaDatosSoldadura.Add(ds);
            numSoldaduraActiva = listaDatosSoldadura.IndexOf(ds);

            //Lo guardamos en un fichero de texto.
            GuardarFichero();

            //Creamos un item en el menú.
            ToolStripItem subItem = new ToolStripMenuItem(ds.nombre);
            subItem.Click += new EventHandler(subItem_Click);
            listaToolStripMenuItem.DropDownItems.Add(subItem);
            MessageBox.Show("Datos guardados");
        }

        private void GuardarFichero()
        {
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(ficheroSoldaduras);

                //Write a line of text
                foreach (DatosSoldadura ds in listaDatosSoldadura)
                {
                    sw.WriteLine("INICIO");
                    sw.WriteLine(ds.nombre);
                    sw.WriteLine(ds.text_WPS);
                    sw.WriteLine(ds.text_I);
                    sw.WriteLine(ds.text_Hilo);
                    sw.WriteLine(ds.text_V);
                    sw.WriteLine(ds.text_Nivel);
                    sw.WriteLine(ds.text_linea1);
                    sw.WriteLine(ds.text_linea2);
                    sw.WriteLine(ds.text_linea3);
                    sw.WriteLine("FIN");
                }

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                //Console.WriteLine("Exception: " + e.Message);
                MessageBox.Show(e.Message);
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
                
            }
        }

        void subItem_Click(object sender, EventArgs e)
        {
            ToolStripItem subItem = (ToolStripMenuItem)sender;
            AbrirDatosSoldadura(subItem.Text);
        }

        private void AbrirDatosSoldadura(string nombre)
        {
            DatosSoldadura ds = listaDatosSoldadura.Where(s => s.nombre == nombre).First();
            numSoldaduraActiva = listaDatosSoldadura.IndexOf(ds);
            MostrarSoldaduraActiva(numSoldaduraActiva);
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (numSoldaduraActiva > -1)
            {
                string nombre = listaDatosSoldadura[numSoldaduraActiva].nombre;
                if (MessageBox.Show("¿Desea eliminar los datos de la soldadura: "+ nombre +"?", "Datos Soldadura", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    listaDatosSoldadura.RemoveAt(numSoldaduraActiva);
                    GuardarFichero();
                    listaToolStripMenuItem.DropDownItems.RemoveAt(numSoldaduraActiva);


                    if (listaDatosSoldadura.Count > 0)
                        numSoldaduraActiva = 0;
                    else
                        numSoldaduraActiva = -1;

                    MostrarSoldaduraActiva(numSoldaduraActiva);

                }
            }
            else
                MessageBox.Show("No se ha seleccionado ninguna soldadura de la lista");
        }
    }

    public class DatosSoldadura
    {
        public string nombre;
        public string text_WPS;
        public string text_I;
        public string text_Hilo;
        public string text_V;
        public string text_Nivel;
        public string text_linea1;
        public string text_linea2;
        public string text_linea3;
    }
}
