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
    public partial class Form6Test : Form
    {
        string certificado="";

        public Form6Test(string certificado)
        {
            InitializeComponent();

            this.certificado = certificado;
            string NewString = certificado;

            string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "/", "<",">",":"};
            foreach (var c in charsToRemove)
            {
                certificado = certificado.Replace(c, string.Empty);
            }
            if (File.Exists("\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + certificado + "-testA.jpg"))
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath, "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + certificado + "-testA.jpg"));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "/", "<", ">", ":" };
            foreach (var c in charsToRemove)
            {
                certificado = certificado.Replace(c, string.Empty);
            }

            if (File.Exists("\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + certificado + "-testA.jpg"))
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
                Application.DoEvents();
                File.Delete("\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + certificado + "-testA.jpg");
            }

            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "Imagenes |*.jpg; *.png";
            opd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            opd.Title = "Seleccionar imagen para  el test de adherencia";

            if (opd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opd.FileName);
            }

            pictureBox1.Image.Save("\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\" + certificado + "-testA.jpg", ImageFormat.Jpeg);
        }
    }
}
