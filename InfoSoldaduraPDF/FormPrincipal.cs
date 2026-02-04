using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using PQScan.PDFToImage;
using Spire.Pdf;
using System.Drawing.Imaging;
using empresaGlobalProj;

namespace InfoSoldaduraPDF
{
    public partial class FormPrincipal : Form
    {
        string[] listaNombrePDF;
        string path_temp_original = Path.GetTempPath()+"PlanosOriginal\\";
        string path_temp_mod = Path.GetTempPath()+"PlanosMod\\";
        List<Point> listaPuntosBase = new List<Point>();//Un punto base por cada pdf
        //Point pointVertical = new Point(60, 690);
        //Point pointHorizontal = new Point(35, 730);
        bool isPDFVertical;//Vemos si el PDF está en vertical o en horizontal

        int widthPDF;
        int heightPDF;

        //Datos de las soldaduras
        public int numSoldaduraActiva = -1;
        public string text_nombre = String.Empty;
        public string text_WPS = String.Empty;
        public string text_I = String.Empty;
        public string text_Hilo = String.Empty;
        public string text_V = String.Empty;
        public string text_Nivel = String.Empty;
        public string text_linea1 = String.Empty;
        public string text_linea2 = String.Empty;
        public string text_linea3 = String.Empty;

        int numPaginaPDF = 1;

        public FormPrincipal()
        {
            InitializeComponent();
            empresaGlobal.showEmp = false;
            if (!Directory.Exists(path_temp_original))
                Directory.CreateDirectory(path_temp_original);
            if (!Directory.Exists(path_temp_mod))
                Directory.CreateDirectory(path_temp_mod);

        }

        public void inicializarPuntosBase()
        {
            listaPuntosBase.Clear();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listaPuntosBase.Add(new Point(0, 0));
            }
        }

        private void abrirPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listaNombrePDF = openFileDialog1.FileNames;
                listBox1.Items.Clear();
                foreach (string s in listaNombrePDF)
                {
                    listBox1.Items.Add(s);
                }

            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            numPaginaPDF = 1;
            //Añadimos el numero de páginas al combobox.            
            MostrarPDF();
            AddNumerosPagina();
        }

        private void MostrarPDF(string nameDefault = "")
        {
            string nameFile = string.Empty;

            if (nameDefault != "")//Forzamos a leer un pdf, solo para pruebas de desarrollo.
                nameFile = nameDefault;

            if (listBox1.SelectedIndex > -1)
                nameFile = listBox1.GetItemText(listBox1.SelectedItem);

            if (nameFile != string.Empty)
            {                                      
                string pathFile = path_temp_original + nameFile + ".pdf";
                string pathTemp = path_temp_mod + nameFile + ".pdf";
                string pathTempBmp = path_temp_mod + nameFile + ".bmp";


                //En funcion si tenemos seleccionado el check de mostrar Cajetin, mostramos el inicial o el temporal.
                if (cBMostrarCajetin.Checked)
                {
                    AddCajetinToPdf(pathFile, pathTemp, listBox1.SelectedIndex);
                    webBrowser1.Navigate(pathTemp);
                    //MostrarImagenPDF(pathTemp, pathTempBmp);
                    MostrarImagenPDF2(pathTemp, pathTempBmp);
                }
                else
                {
                    webBrowser1.Navigate(pathFile);
                    //MostrarImagenPDF(pathFile, pathTempBmp);
                    MostrarImagenPDF2(pathFile, pathTempBmp);
                }

                //Mostramos una imagen
                
                
                
                

                //////////////////////////

            }
        }

        private void MostrarImagenPDF(string pathPDF, string pathBMP)
        {
            PDFDocument doc = new PDFDocument();
            doc.LoadPDF(pathPDF);            

            if (doc.PageCount > 0)            
            {
                try
                {                                       
                    Bitmap bmp = doc.ToImage(numPaginaPDF-1);                    
                    //if (!File.Exists(pathBMP))
                    bmp.Save(pathBMP, ImageFormat.Bmp);
                    //De esta forma, liberamos el archivo bmp.
                    FileStream fs = new FileStream(pathBMP, FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                    fs.Dispose();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al crear la imagen del plano: " + e.Message);
                }
            }
            doc.Dispose();
        }

        /// <summary>
        /// Uso otra libreria para convertir de pdf a imagen, ya que con la libreria anterior, me da alguna excepcion para determinados pdf.
        /// </summary>
        /// <param name="pathPDF"></param>
        /// <param name="pathBMP"></param>
        private void MostrarImagenPDF2(string pathPDF, string pathBMP)
        {
            
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            if (File.Exists(pathPDF))
                doc.LoadFromFile(pathPDF);
            else
                MessageBox.Show("No existe el plano en PDF");
           
            if (doc.Pages.Count > 0)
            {
                try
                {
                    //System.Drawing.Image image = doc.SaveAsImage(0);                      
                    System.Drawing.Image image = doc.SaveAsImage(numPaginaPDF - 1);                    
                    image.Save(pathBMP, ImageFormat.Bmp);                    
                    //De esta forma, liberamos el archivo bmp.
                    FileStream fs = new FileStream(pathBMP, FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = System.Drawing.Image.FromStream(fs);
                    fs.Dispose();
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al crear la imagen del plano: " + e.Message);
                }
            }
            doc.Dispose();
        }

        private void AddNumerosPagina()
        {
            string nameFile = string.Empty;
            if (listBox1.SelectedIndex > -1)
                nameFile = listBox1.GetItemText(listBox1.SelectedItem);

            if (nameFile != string.Empty)
            {
                string pathFile = path_temp_original + nameFile + ".pdf";
                Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                if (File.Exists(pathFile))
                    doc.LoadFromFile(pathFile);

                int numPaginas = doc.Pages.Count;

                cmbNumPag.Items.Clear();
                for (int i = 1; i <= numPaginas; i++)
                {
                    cmbNumPag.Items.Add(i);
                }
                cmbNumPag.SelectedIndex = numPaginaPDF - 1;
                doc.Dispose();
            }

        }

        public bool AddCajetinToPdf(string inputPdfPath, string outputPdfPath, int numListaPDF)
        {

            //Comprobamos que exista el fichero de entrada.
            if (!File.Exists(inputPdfPath))
                return false;

            //variables
            string pathin = inputPdfPath;
            string pathout = outputPdfPath;

            //create PdfReader object to read from the existing document
            using (PdfReader reader = new PdfReader(pathin))
            {
                //Comprobamos que el fichero temporal outputPdfPath está libre.                
                if (IsFileLocked(new FileInfo(pathout)))
                {
                    webBrowser1.Navigate("about:blank");
                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                        Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                    File.Delete(pathout);
                }

                //create PdfStamper object to write to get the pages from reader 
                using (PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create)))
                {

                    //gettins the page size in order to substract from the iTextSharp coordinates
                    //var pageSize = reader.GetPageSize(1);
                    //iTextSharp.text.Rectangle pageSize = reader.GetPageSize(1);                    
                    iTextSharp.text.Rectangle pageSize = reader.GetPageSize(numPaginaPDF);

                    if (cBInvertirDim.Checked)
                    {//Invertimos el alto y el ancho.
                        heightPDF = Convert.ToInt32(pageSize.Width);
                        widthPDF = Convert.ToInt32(pageSize.Height);
                    }
                    else
                    {
                        widthPDF = Convert.ToInt32(pageSize.Width);
                        heightPDF = Convert.ToInt32(pageSize.Height);
                    }

                    //Vemos si el punto base está inicializado o no.
                    Point pointBase = listaPuntosBase[numListaPDF];
                    if ((pointBase.X==0) && (pointBase.Y == 0))
                    {
                        //Lo inicializamos en la parte inferior izquierda.
                        pointBase.X = 40;
                        pointBase.Y = heightPDF - 100;
                        listaPuntosBase[listBox1.SelectedIndex] = pointBase;
                    }

                    


                    // PdfContentByte from stamper to add content to the pages over the original content
                    //PdfContentByte pbover = stamper.GetOverContent(1);                   
                    PdfContentByte pbover = stamper.GetOverContent(numPaginaPDF);

                    //add content to the page using ColumnText
                    iTextSharp.text.Font font = new iTextSharp.text.Font();

                    //setting up the X and Y coordinates of the document
                    int x = pointBase.X;
                    int y = pointBase.Y;

                    pintarCajetin(pbover, font, widthPDF, heightPDF, pointBase);                

                    if (cBRegla.Checked)
                        PintarRegla(pbover, font, x, y, widthPDF, heightPDF);

                    stamper.Close();
                    reader.Close();
                }
            }
            return true;
        }

        private void pintarCajetin(PdfContentByte pbover, iTextSharp.text.Font font, int widthPDF, int heightPDF, Point pointBase)
        {
            font.Size = 8;
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
            
            int x = pointBase.X;
            int y = pointBase.Y;
            y = (int)(heightPDF - y);

            //18_04_18. Comprobamos el ancho del cajetín.
            int anchoCajetin = 150;//Valor mínimo.
            Phrase phrase = new Phrase("I: " + text_I);
            float num = ColumnText.GetWidth(phrase);
            phrase = new Phrase("V: " + text_V);
            float num2 = ColumnText.GetWidth(phrase);
            phrase = new Phrase("HILO: " + text_Hilo);
            float num3 = ColumnText.GetWidth(phrase);
            
            float max = Math.Max(Math.Max(num, num2), num3);
            if (max>110)
                anchoCajetin += ((Convert.ToInt32(max)-110));
            ////////////////////////////////////

            //Borramos el fondo donde vamos a colocar nuestro cajetín            
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(0,0);            
            rectangle.BackgroundColor = BaseColor.WHITE;
            rectangle.BorderColor = BaseColor.WHITE;
            rectangle.Right = x + anchoCajetin;
            rectangle.Top = y - 82;
            rectangle.Bottom = y + 8;
            rectangle.Left = x - 5;
            pbover.Rectangle(rectangle);
            pbover.Stroke();

            string textToAdd = "INSTRUCCION DE SOLDADURA";
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, boldFont), x, y, 0);

            textToAdd = "WPS: "+text_WPS;
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x, y - 20, 0);

            pbover.SetLineWidth(0.5);
            pbover.MoveTo(x + 83, y - 3);
            pbover.LineTo(x + 75, y - 3);
            pbover.LineTo(x + 75, y - 32);
            pbover.LineTo(x + 83, y - 32);
            pbover.Stroke();

            textToAdd = "I: " + text_I;
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x + 80, y - 10, 0);
            textToAdd = "V: " + text_V;
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x + 80, y - 20, 0);
            textToAdd = "HILO: " + text_Hilo;
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x + 80, y - 30, 0);


            textToAdd = "NIVEL: " + text_Nivel;
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x, y - 45, 0);
            textToAdd = "(ISO 5817)";
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x + 80, y - 45, 0);

            textToAdd = text_linea1;
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x, y - 60, 0);
            textToAdd = text_linea2;
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x, y - 70, 0);
            textToAdd = text_linea3;
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(textToAdd, font), x, y - 80, 0);

            pbover.SetLineWidth(1);
            pbover.MoveTo(x - 5, y + 8);
            pbover.LineTo(x + anchoCajetin, y + 8);
            pbover.LineTo(x + anchoCajetin, y - 82);
            pbover.LineTo(x - 5, y - 82);
            pbover.LineTo(x - 5, y + 8);
            pbover.Stroke();

            

        }

        private void PintarRegla(PdfContentByte pbover, iTextSharp.text.Font font, int x, int y, int widthPDF, int heightPDF)
        {
            y = (int)(heightPDF - y);
            pbover.SetLineWidth(1);
            pbover.SetColorStroke(BaseColor.RED);
            font.Color = BaseColor.RED;
            //Partimos de la parte superior izquierda del cajetín.
            int x2 = x - 5;
            int y2 = y + 8;

            //Linea inferior (y-):
            pbover.MoveTo(x2, y2);
            pbover.LineTo(x2, 0);
            int inc = -10;
            while ((y2 + inc) > 0)
            {
                pbover.MoveTo(x2, y2 + inc);
                pbover.LineTo(x2 + 5, y2 + inc);
                if (((Math.Abs(inc) / 10) % 2) > 0)//Pintamos una sí una no.
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_RIGHT, new Phrase(inc.ToString(), font), x2, y2 + inc - 3, 0);
                inc += -10;
            }

            //Linea superior (y+)
            pbover.MoveTo(x2, y2);
            pbover.LineTo(x2, heightPDF);
            inc = 10;
            while ((y2 + inc) < heightPDF)
            {
                pbover.MoveTo(x2, y2 + inc);
                pbover.LineTo(x2 + 5, y2 + inc);
                if (((inc / 10) % 2) > 0)//Pintamos una sí una no.
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_RIGHT, new Phrase(inc.ToString(), font), x2, y2 + inc - 3, 0);
                inc += 10;
            }

            //Linea izquierda(x-)
            pbover.MoveTo(x2, y2);
            pbover.LineTo(0, y2);
            inc = -10;
            while ((x2 + inc) > 0)
            {
                pbover.MoveTo(x2 + inc, y2);
                pbover.LineTo(x2 + inc, y2 + 5);
                if (((Math.Abs(inc) / 10) % 2) > 0)//Pintamos una sí una no.
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_TOP, new Phrase(inc.ToString(), font), x2 + inc - 6, y2 - 7, 0);

                inc += -10;
            }

            //Linea derecha(x+)
            pbover.MoveTo(x2, y2);
            pbover.LineTo(widthPDF, y2);
            inc = 10;
            while ((x2 + inc) < widthPDF)
            {
                pbover.MoveTo(x2 + inc, y2);
                pbover.LineTo(x2 + inc, y2 + 5);
                if (((inc / 10) % 2) > 0)//Pintamos una sí una no.
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_TOP, new Phrase(inc.ToString(), font), x2 + inc - 6, y2 - 7, 0);
                inc += 10;
            }

            pbover.Stroke();
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private void cBMostrarCajetin_CheckedChanged(object sender, EventArgs e)
        {            
            MostrarPDF();            
        }

        private void cBRegla_CheckedChanged(object sender, EventArgs e)
        {            
            MostrarPDF();            
        }

        //Desplazamos el punto de insercción del cajetín
        private void button1_Click(object sender, EventArgs e)
        {
            //Point pointVertical = new Point(60, 710);
            //Point pointHorizontal = new Point(35, 750);
            
            //Hay que ver si estamos en un PDF Vertical u Horizontal.            
            //if (isPDFVertical)
            //{
            //    pointVertical.X += Convert.ToInt32(numericUpDownHorizontal.Value);
            //    pointVertical.Y -= Convert.ToInt32(numericUpDownVertical.Value);
            //}
            //else
            //{
            //    pointHorizontal.X += Convert.ToInt32(numericUpDownHorizontal.Value);
            //    pointHorizontal.Y -= Convert.ToInt32(numericUpDownVertical.Value);
            //}

            if (listBox1.SelectedIndex > -1)
            {
                Point pointBase = listaPuntosBase[listBox1.SelectedIndex];
                pointBase.X += Convert.ToInt32(numericUpDownHorizontal.Value);
                pointBase.Y -= Convert.ToInt32(numericUpDownVertical.Value);
                listaPuntosBase[listBox1.SelectedIndex] = pointBase;

                //Volvemos a poner los valores a cero.
                numericUpDownHorizontal.Value = 0;
                numericUpDownVertical.Value = 0;

                MostrarPDF();
            }
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 320;
        }

        private void datosSoldaduraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosSoldadura();            
        }

        private void DatosSoldadura()
        {
            FormDatosSoldadura fDS = new FormDatosSoldadura();            
            fDS.numSoldaduraActiva = this.numSoldaduraActiva;
            fDS.datosSoldaduraPantalla.nombre = this.text_nombre;
            fDS.datosSoldaduraPantalla.text_WPS = this.text_WPS;
            fDS.datosSoldaduraPantalla.text_I = this.text_I;
            fDS.datosSoldaduraPantalla.text_V = this.text_V;
            fDS.datosSoldaduraPantalla.text_Hilo = this.text_Hilo;
            fDS.datosSoldaduraPantalla.text_Nivel = this.text_Nivel;
            fDS.datosSoldaduraPantalla.text_linea1 = this.text_linea1;
            fDS.datosSoldaduraPantalla.text_linea2 = this.text_linea2;
            fDS.datosSoldaduraPantalla.text_linea3 = this.text_linea3;            
            if (fDS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Pillamos la información de la soldadura.
                this.numSoldaduraActiva = fDS.numSoldaduraActiva;
                this.text_nombre = fDS.datosSoldaduraPantalla.nombre;
                this.text_WPS = fDS.datosSoldaduraPantalla.text_WPS;
                this.text_I = fDS.datosSoldaduraPantalla.text_I;
                this.text_V = fDS.datosSoldaduraPantalla.text_V;
                this.text_Hilo = fDS.datosSoldaduraPantalla.text_Hilo;
                this.text_Nivel = fDS.datosSoldaduraPantalla.text_Nivel;
                this.text_linea1 = fDS.datosSoldaduraPantalla.text_linea1;
                this.text_linea2 = fDS.datosSoldaduraPantalla.text_linea2;
                this.text_linea3 = fDS.datosSoldaduraPantalla.text_linea3;
                MostrarPDF();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DatosSoldadura();            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea añadir el cajetín de la inf. de soldadura a los PDFs seleccionados?", "Modificar PDFs", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ModificarPDFs();
                MostrarPDF();    
                MessageBox.Show("Fin de la modificación.", "Modificar PDFs", MessageBoxButtons.OK);                

            }
        }

        private void ModificarPDFs()
        {                        
            for (int i = 0; i< listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i))
                {
                    string nameFile = listBox1.GetItemText(listBox1.Items[i]);
                    string pathFileOrigi = path_temp_original + nameFile + ".pdf";
                    string pathTempMod = path_temp_mod + nameFile + ".pdf";

                    if (AddCajetinToPdf(pathFileOrigi, pathTempMod,i))
                    {
                        //Comprobamos que el fichero original pathFile está libre.                
                        if (IsFileLocked(new FileInfo(pathFileOrigi)))
                        {
                            webBrowser1.Navigate("about:blank");
                            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                                Application.DoEvents();
                            System.Threading.Thread.Sleep(100);

                        }
                        //Borramos el original
                        File.Delete(pathFileOrigi);
                        //Copiamos el modificado al orignal
                        File.Copy(pathTempMod, pathFileOrigi, true);
                        //Borramos el modificado
                        File.Delete(pathTempMod);
                    }
                }                
            }
            //Volvemos a guardar el original (ya modificado) en el servidor.
            GuardarPDFsServidor();
        }

        private void GuardarPDFsServidor()
        {
            //string pathServidor = @"\\srvfiles01\Buzon\";
            string pathServidor = @"\\nas01\Piezas.oficina\__Buzon__\";
            string fileUser = Environment.UserName+".txt";
            //foreach (string file in Directory.GetFiles(path_temp_original))
            //{
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i))
                {                     
                    //string filename = Path.GetFileName(file);
                    string filename = listBox1.GetItemText(listBox1.Items[i]);
                    string pathFileOrigi = path_temp_original + filename + ".pdf";
                    //File.Copy(file, pathServidor + filename, true);
                    File.Copy(pathFileOrigi, pathServidor + filename + ".pdf", true);
                }
            }
            //}
            var myFile = File.Create(pathServidor + fileUser);
            myFile.Close();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ggDataSetProyectos.TC_LANZA' Puede moverla o quitarla según sea necesario.
            this.tC_LANZATableAdapter.Fill(this.ggDataSetProyectos.TC_LANZA);
            this.comboBoxProyectos.ResetText();
            this.comboBoxProyectos.SelectedIndex = -1;

        }

        private void comboBoxProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProyectos.SelectedIndex > -1)
            {
                this.dT_CodigoRutaTableAdapter.Fill(this.ggDataSetProyectos.dT_CodigoRuta, comboBoxProyectos.SelectedValue.ToString());                
                ggDataSet.dT_CodigoRutaDataTable dT_CodigoRuta = this.dT_CodigoRutaTableAdapter.GetData(comboBoxProyectos.SelectedValue.ToString());

                if (dT_CodigoRuta.Count() > 0)
                {
                    ObtenerPlanos(dT_CodigoRuta);
                    inicializarPuntosBase();
                }
            
            }
    
        }

        //Nos traemos una copia de los planos de su ruta, a una carpeta path_temp_original
        private void ObtenerPlanos(ggDataSet.dT_CodigoRutaDataTable dT_CodigoRuta)
        {
            //Borramos todos los pdf del path_tmp_original y path_tmp_mod
            foreach (string file in Directory.GetFiles(path_temp_original))
            {
                if (IsFileLocked(new FileInfo(file)))
                {
                    webBrowser1.Navigate("about:blank");
                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                        Application.DoEvents();
                    System.Threading.Thread.Sleep(100);

                    
                }
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string file in Directory.GetFiles(path_temp_mod))
            {
                if (IsFileLocked(new FileInfo(file)))
                {
                    webBrowser1.Navigate("about:blank");
                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                        Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                    
                }
                File.Delete(file);
            }


            foreach (DataRow dr in dT_CodigoRuta.Rows)
            {
                string codigo = dr["CODIGO"].ToString();
                string path = dr["path"].ToString();

                string pathFileOrigen = path+codigo+".pdf";
                if (File.Exists(pathFileOrigen))
                {
                    string pathFileDestino = path_temp_original+codigo+".pdf";
                    File.Copy(pathFileOrigen, pathFileDestino,true);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
  
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
                        
            if (listBox1.SelectedIndex > -1)
            {
                Point pointBase = listaPuntosBase[listBox1.SelectedIndex];
                //pointBase.X += Convert.ToInt32(numericUpDownHorizontal.Value);
                numericUpDownHorizontal.Value = GetXPDF(e.X) - pointBase.X + 6;//+6 para que coincida con la esquina superior izquierda del cajetin
                numericUpDownVertical.Value = pointBase.Y - GetYPDF(e.Y) - 8;  //-8 para que coincida con la esquina superior izquierda del cajetin              
            }
        }

        //Convertimos coordenadas del bmp del picturebox a coordenadas del pdf.
        private int GetXPDF(int xBMP)
        {
            return (widthPDF * xBMP / pictureBox1.Width);
        }
        private int GetYPDF(int yBMP)
        {
            return (heightPDF * yBMP / pictureBox1.Height);
        }

        private void abrirConAdobePDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = @"C:\Users\mmillan\AppData\Local\Temp\PlanosOriginal\C2000.pdf";
            System.Diagnostics.Process.Start(filename);

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cBInvertirDim_CheckedChanged(object sender, EventArgs e)
        {
            //Cambiamos las coordenadas del punto base actual.
            //Point pointBase = listaPuntosBase[listBox1.SelectedIndex];
            //int aux = pointBase.X;
            //pointBase.X = pointBase.Y;
            //pointBase.Y = aux;
            //Y volvemos a pintar el pdf.
            MostrarPDF();        
        }

        //Solo para desarrollo.
        private void button4_Click_1(object sender, EventArgs e)
        {
            MostrarPDF("MS4-GA401");
        }

        private void cmbNumPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            numPaginaPDF = int.Parse(cmbNumPag.Text);
            MostrarPDF();  
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }        
    }
}
