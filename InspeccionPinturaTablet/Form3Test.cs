using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace InspeccionPinturaTablet
{
    public partial class Form3Test : Form
    {
        int test = 0;
        string certificado;

        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();

        public Form3Test(int i, string certificado)
        {
            InitializeComponent();
            test = i;
            label1.Text = label1.Text + Convert.ToString(i);
            this.certificado = certificado;

            DataTable table = null;
            table = ipDB.datos3RuglSup(certificado);

            foreach (DataRow row in table.Rows)
            {

                string columna = "test" + test + "pieza";
                string columnacantidad = "test" + test + "evcantidad";
                textBoxPE.Text = Convert.ToString(row[columna]);
                comboBox1.Text = Convert.ToString(row[columnacantidad]);
                
            }

            
            string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "/", "<", ">", ":" };
            foreach (var c in charsToRemove)
            {
                certificado = certificado.Replace(c, string.Empty);
            }
            if (File.Exists("\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + certificado + "-test" + test + ".jpg"))
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath, "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + certificado + "-test" + test + ".jpg"));
            }
           
        }
         

       
            
        private void button1_Click(object sender, EventArgs e)
        {
            int valorCombo = 0;
            if (textBoxPE.Text == "") textBoxPE.Text = null;
            if (comboBox1.Text != "") valorCombo = Convert.ToInt32(comboBox1.Text) ;
            ipDB.actulizar3InsTest(certificado, test, textBoxPE.Text, valorCombo);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string NewString = certificado;
            string[] charsToRemove = new string[] {"@", ",", ".", ";", "'", "/", "<", ">", ":" };
            foreach (var c in charsToRemove)
            {
                NewString = NewString.Replace(c, string.Empty);
            }
            if (File.Exists("\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + NewString + "-test" + test + ".jpg"))
            {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
            Application.DoEvents();
            File.Delete("\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + NewString + "-test" + test + ".jpg");
            }
            
            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "Imagenes |*.jpg; *.png";
            opd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            opd.Title = "Seleccionar imagen para inspeccion " + certificado + " TEST " + test;

            if (opd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opd.FileName);
            }

            pictureBox1.Image.Save("\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + NewString + "-test" + test + ".jpg", ImageFormat.Jpeg);
        }
    }
}
