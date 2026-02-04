using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Microsoft.Office.Interop.Excel;
using System.Configuration;
using empresaGlobalProj;
using System.Diagnostics;

/*using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf.fonts;

using PdfSharp.Drawing;*/


namespace PlanosPedidos
{
    public partial class PlanosPedidos : Form
    {
        private PlanosPedidosBD bd;


        public PlanosPedidos()
        {
           // InitializeComponent();
           // empresaGlobal.showEmp = false;
           // bd = new PlanosPedidosBD();
            ProcessStartInfo info = new ProcessStartInfo();

            /*info.UseShellExecute = true;
            info.FileName = "setup.exe";
            info.WorkingDirectory = "\\srvfiles01\\AppIMEDEXSA\\PlanosPedidos\\";

            Process.Start(info);*/
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            //p.StartInfo.FileName = @"\\srvfiles01\AppIMEDEXSA\PlanosPedidos\setup.exe";
            p.StartInfo.FileName = @"\\nas01\Planos.Pedidos\setup.exe";
            p.Start();
            Cursor.Current = Cursors.Arrow;
            p.WaitForExit();
        }



        private void buttonCargarLineas_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string cliente, cabecera;
            bd.ObtenerClienteCabecera(textBoxPedido.Text,out cliente,out cabecera);
            textBoxCliente.Text = cliente;
            textBoxCabecera.Text = cabecera;
            dataGridViewLineas.DataSource = bd.ObtenerLineas(textBoxPedido.Text,checkBoxDesarrollo.Checked);
            dataGridViewLineas.Columns[0].Width = 50;
            dataGridViewLineas.Columns[1].Width = 175;
            dataGridViewLineas.Columns[2].Width = 100;
            dataGridViewLineas.Columns[3].Width = 800;
            if (checkBoxDesarrollo.Checked)
            {
                foreach (DataGridViewRow row in dataGridViewLineas.Rows)
                { 
                    if(row.Cells["CÓDIGO"].Value.ToString().ToUpper().IndexOf("DESARROLLO-") == 0)               
                        row.DefaultCellStyle.ForeColor = Color.Red;                    
                }
            }

            buttonSeleccionarNada_Click(sender, e);
         
            Cursor.Current = Cursors.Arrow;
        }



        private void buttonSeleccionarTodo_Click(object sender, EventArgs e)
        {
            buttonSeleccionarNada_Click(sender, e);

            foreach (DataGridViewRow row in dataGridViewLineas.Rows)            
                row.Selected = true;                         
        }



        private void buttonSeleccionarTodoExceptoDesarrollo_Click(object sender, EventArgs e)
        {
            buttonSeleccionarNada_Click(sender, e);
            
            foreach (DataGridViewRow row in dataGridViewLineas.Rows)
            {
                if (row.Cells["CÓDIGO"].Value.ToString().ToUpper().IndexOf("DESARROLLO-") == 0)                
                    row.Selected = false;                
                else                
                    row.Selected = true;                
            }
        }

        private void buttonSeleccionarNada_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewLineas.Rows)
                row.Selected = false;  
        }



        private void buttonGenerarDocumentacion_Click(object sender, EventArgs e)
        {
            string path;
            
            try
            {
                if (dataGridViewLineas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("NO HA SELECCIONADO LÍNEAS PARA LAS QUE GENERAR LA DOCUMENTACIÓN", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                /*if (textBoxNombreCarpeta.Text == "")
                {
                    MessageBox.Show("NO HA INDICADO EL NOMBRE DE LA CARPETA A GENERAR", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxNombreCarpeta.Focus();
                    return;
                }*/

                path = "L:\\" + textBoxPedido.Text + "\\5. DOCUMENTACIÓN A ENVIAR\\C. PLANOS DE MONTAJE\\" + textBoxNombreCarpeta.Text;
                if(Directory.Exists(path)){
                    DialogResult result = MessageBox.Show("YA EXISTE LA CARPETA:\n\n" + path + ".\n\n ¿DESEAS SOBRESCRIBIR LA CARPETA?", "AVISO", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                       
                    }
                    else if (result == DialogResult.No)
                    {
                        textBoxNombreCarpeta.Focus();
                        return;
                    }
                    
                    //MessageBox.Show("YA EXISTE LA CARPETA:\n\n" + path + "\n\nINDIQUE OTRO NOMBRE.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                   // textBoxNombreCarpeta.Focus();
                    //return;
                }
               
                Directory.CreateDirectory(path);

                Cursor.Current = Cursors.WaitCursor;
                CrearExcel(path);

                //Carlos Sanchez 26/01/2022 - Elimino los planos "originales" (con la coletilla 'SINMODIFICAR'), para quedar solo los nuevos.
                DirectoryInfo di = new DirectoryInfo(path + "\\PLANOS DE MONTAJE");
                FileInfo[] files = di.GetFiles();
                foreach (FileInfo file in files)
                {
                    string name = file.Name;

                    if (name.EndsWith("SINMODIFICAR.pdf"))
                    {

                        string srcEliminar = path + "\\PLANOS DE MONTAJE\\" + name;
                        try
                        {
                            File.Delete(srcEliminar);
                            if (File.Exists(srcEliminar))
                            {
                               
                            }
                            else
                            {
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al borrar archivo: " + srcEliminar, ex.ToString());
                        }

                    }
                }
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("DOCUMENTACIÓN GENERADA CORRECTAMENTE. A CONTINUACIÓN SE ABRIRÁ SU CARPETA CORRESPONDIENTE", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(path);
                textBoxNombreCarpeta.Text = "";
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);             
            }
        }


       
        private void CrearExcel(string path)
        {
            // Aqui creo los excel y copio los pdf de los planos.
            string codigo;
            int filascabecera = 1;
            int filasespacio = 1;
            int filasfiltros = 1;
            int columnacodigo = -1;

            string AVISO = "";

            Intranet.AccesoServicioPlanos servicio = new Intranet.AccesoServicioPlanos();
            
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;
            
            // Aqui modifico la cabecera
            Microsoft.Office.Interop.Excel.Range RangeCabecera = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[filascabecera, dataGridViewLineas.ColumnCount]));            
            RangeCabecera.Merge(true);            
            RangeCabecera.Value = textBoxCabecera.Text;
            RangeCabecera.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            RangeCabecera.Font.Bold = true;
            RangeCabecera.RowHeight = 100;
            RangeCabecera.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            RangeCabecera.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            object[] Header = new object[dataGridViewLineas.ColumnCount];
            for (int i = 0; i < dataGridViewLineas.ColumnCount; i++)            
            {
                Header[i] = dataGridViewLineas.Columns[i].HeaderText;
                if((columnacodigo == -1)&&(dataGridViewLineas.Columns[i].HeaderText == "CÓDIGO")){
                    columnacodigo = i;
                }                
            }            
            Microsoft.Office.Interop.Excel.Range HeaderRange = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[filascabecera + filasespacio + filasfiltros, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[filascabecera + filasespacio + filasfiltros, dataGridViewLineas.ColumnCount]));
            HeaderRange.Value = Header;
            HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            HeaderRange.Font.Bold = true;
                        
            HeaderRange.AutoFilter(1,
                                Type.Missing,
                                Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlAnd,
                                Type.Missing,
                                true);
                        
            object[,] Cells = new object[dataGridViewLineas.SelectedRows.Count, dataGridViewLineas.ColumnCount + 1];                        
            int k = -1;
            for (int j = dataGridViewLineas.SelectedRows.Count - 1; j >= 0; j--)
            {
                k++;
                for (int i = 0; i < dataGridViewLineas.ColumnCount; i++)
                {
                    Cells[k, i] = dataGridViewLineas.SelectedRows[j].Cells[i].Value;
                    if (i == columnacodigo)
                    {
                        codigo = dataGridViewLineas.SelectedRows[j].Cells[i].Value.ToString().ToUpper();
                        if (codigo.Contains("DESARROLLO-"))                        
                            Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[k + filascabecera + filasespacio + filasfiltros + 1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[k + filascabecera + filasespacio + filasfiltros + 1, dataGridViewLineas.ColumnCount])).Font.Color = System.Drawing.Color.Red;                                                    
                        else
                        {
                            if (servicio.CopiarPlano(codigo, path + "\\PLANOS DE MONTAJE")) //poner lo de nuevo
                            {
                                Cells[k, dataGridViewLineas.ColumnCount] = "OK";
                                //Carlos Sanchez 26/01/2022 -  Modifico la ultima hoja del pdf si fuese necesario.
                               AVISO = modificarExcel(path, codigo);

                               
                            }
                            else
                            {
                                Cells[k, dataGridViewLineas.ColumnCount] = "ERROR AL OBTENER EL PLANO";
                            }
                        }
                    }
                }
                    
            }
            Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[filascabecera + filasespacio + filasfiltros + 1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[filascabecera + filasespacio + filasfiltros + dataGridViewLineas.SelectedRows.Count, dataGridViewLineas.ColumnCount + 1])).Value = Cells;
            for (int i = filascabecera + filasespacio + filasfiltros + 1; i <= filascabecera + filasespacio + filasfiltros + dataGridViewLineas.SelectedRows.Count; i++)
            {
                if (Worksheet.Cells[i, dataGridViewLineas.ColumnCount + 1].value == "OK")
                {
                    Worksheet.Cells[i, dataGridViewLineas.ColumnCount + 1].value = AVISO;

                    // Aqui modifico el aspecto del AVISO para indicar que el pdf contiene al final padre y algun hijo
                    if (AVISO == "AVISO")
                    {
                        Microsoft.Office.Interop.Excel.Range RangeAviso = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, dataGridViewLineas.ColumnCount + 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, dataGridViewLineas.ColumnCount + 1]));
                        RangeAviso.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        RangeAviso.Font.Bold = true;
                    }
                    Worksheet.Hyperlinks.Add(Anchor: Worksheet.Cells[i, columnacodigo + 1], Address: path + "\\PLANOS DE MONTAJE\\" + Worksheet.Cells[i, columnacodigo + 1].value + ".pdf", SubAddress: "", TextToDisplay: Worksheet.Cells[i, columnacodigo + 1].value);
                }
                else 
                { 
                    if (Worksheet.Cells[i, dataGridViewLineas.ColumnCount + 1].value != "")
                        Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, dataGridViewLineas.ColumnCount + 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[i, dataGridViewLineas.ColumnCount + 1])).Font.Color = System.Drawing.Color.Red;                                                    
                }
            }
            Worksheet.Columns.AutoFit();

            if (textBoxNombreCarpeta.Text != "")            
                Worksheet.SaveAs(path + "\\" + textBoxPedido.Text + "_" + textBoxNombreCarpeta.Text + ".xlsx");
            else
                Worksheet.SaveAs(path + "\\" + textBoxPedido.Text + ".xlsx");
            Excel.Quit();

           
        }

        private void PlanosPedidos_FormClosing(object sender, FormClosingEventArgs e)
        {
            empresaGlobal.showEmp = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            String item;
            item = comboBox1.SelectedItem.ToString();

            String valorBusq;
            valorBusq = textBox1.Text;

            //Filtro por la columna selecionada, el valor introducido en el textBox1

            Cursor.Current = Cursors.WaitCursor;

            dataGridViewLineas.DataSource = bd.ObtenerLineasFiltro(textBoxPedido.Text, checkBoxDesarrollo.Checked,valorBusq,item);
            dataGridViewLineas.Columns[0].Width = 50;
            dataGridViewLineas.Columns[1].Width = 175;
            dataGridViewLineas.Columns[2].Width = 100;
            dataGridViewLineas.Columns[3].Width = 800;
            if (checkBoxDesarrollo.Checked)
            {
                foreach (DataGridViewRow row in dataGridViewLineas.Rows)
                {
                    if (row.Cells["CÓDIGO"].Value.ToString().ToUpper().IndexOf("DESARROLLO-") == 0)
                        row.DefaultCellStyle.ForeColor = Color.Red;
                }
            }

            buttonSeleccionarNada_Click(sender, e);

            Cursor.Current = Cursors.Arrow;



        }


        private void dataGridViewLineas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;
          
            if (a == -1)
            {

                //Pinto de rojo las lineas de desarrollo
                if (checkBoxDesarrollo.Checked)
                {
                    foreach (DataGridViewRow row in dataGridViewLineas.Rows)
                    {
                        if (row.Cells["CÓDIGO"].Value.ToString().ToUpper().IndexOf("DESARROLLO-") == 0)
                            row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                }

            }

        }

        private void dataGridViewLineas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;

            if (a == -1)
            {

                //Pinto de rojo las lineas de desarrollo
                if (checkBoxDesarrollo.Checked)
                {
                    foreach (DataGridViewRow row in dataGridViewLineas.Rows)
                    {
                        if (row.Cells["CÓDIGO"].Value.ToString().ToUpper().IndexOf("DESARROLLO-") == 0)
                            row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                }

            }
        }

       
        //Carlos Sanchez 26/01/2022 - Funcion para crear los nuevos pdf de los planos, eliminando o modificando la ultima hoja del pdf si es necesario
        private string modificarExcel(string path, string codigo)
        {
        

          string av =" ";/*
           string src = path + "\\PLANOS DE MONTAJE\\" + codigo + "SINMODIFICAR.pdf";
           string srcnew = path + "\\PLANOS DE MONTAJE\\" + codigo + ".pdf";
            
         
           int padre=0;
           int hijo=0;
           // LEER PDF
           PdfReader reader2 = new PdfReader(src);
           string strText = string.Empty;

            Document document = new Document(reader2.GetPageSizeWithRotation(1));

            // step 2: we create a writer that listens to the document

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(srcnew, FileMode.Create));

            // step 3: we open the document

            document.Open();

            PdfContentByte cb = writer.DirectContent;

            PdfImportedPage pagenew;

            int rotation;

            for (int page = 1; page <= reader2.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                String s = PdfTextExtractor.GetTextFromPage(reader2, page, its);

                s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                

                if (page != reader2.NumberOfPages) //Si no es la ultima pagina la copio igual
                {

                    document.SetPageSize(reader2.GetPageSizeWithRotation(page));
                    document.NewPage();
                    pagenew = writer.GetImportedPage(reader2, page);
                    rotation = reader2.GetPageRotation(page);

                    if (rotation == 90 || rotation == 270)
                    {
                        cb.AddTemplate(pagenew, 0, -1f, 1f, 0, 0, reader2.GetPageSizeWithRotation(page).Height);
                    }
                    else
                    {
                        cb.AddTemplate(pagenew, 1f, 0, 0, 1f, 0, 0);
                    }

                }
                else // En caso de que sea la ultima hoja
                {

                   string[] words = s.Split('\n');
                   if (words[0] != " Los siguientes artículos no tienen un pdf asociado o no están disponibles: ") //Compruebo que no es la hoja a modificar.
                   {
                       document.SetPageSize(reader2.GetPageSizeWithRotation(page));
                       document.NewPage();
                       pagenew = writer.GetImportedPage(reader2, page);
                       rotation = reader2.GetPageRotation(page);

                       if (rotation == 90 || rotation == 270)
                       {
                           cb.AddTemplate(pagenew, 0, -1f, 1f, 0, 0, reader2.GetPageSizeWithRotation(page).Height);
                       }
                       else
                       {
                           cb.AddTemplate(pagenew, 1f, 0, 0, 1f, 0, 0);
                       }
                   }
                   else //Si e la ultima hoja a modificar, analizo el caso y añado los elementos que NO SEAN TORNILLOS
                   {

                      // Creo una nueva pagina:
                       //Si es tornilleria, lo elimino.
                       List<string> noTornillos = new List<string>();

                       for (int j = 1; j < words.Length; j++)
                       {
                           string familia;
                           familia = bd.ObtenerFamilia(words[j].Substring(2, words[j].Length - 2));
                           if(familia !="TO"){

                               noTornillos.Add(words[j].Substring(2, words[j].Length - 2));

                           }
                       }
                       var MyFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                       var MyFont2 = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                  
                       //Añado la nueva pagina
                       if (noTornillos.Count > 0)
                       {
                           document.NewPage();
                           document.Add(new Paragraph("Los siguientes artículos no tienen un pdf asociado o no están disponibles:", MyFont));
                           document.Add(new Paragraph(" "));
                           foreach (string a in noTornillos)
                           {
                               document.Add(new Paragraph("- " + a, MyFont2));
                               if (a == codigo) padre = 1;
                               else hijo = 1;
                           }
                       }
                   }
                }
            }


            document.Close();
            // step 5: we close the document
            reader2.Close();
            if (padre == 1 && hijo == 1) av = "AVISO"; // Si el pdf contiene el padre algun hijo, escrbir aviso en el excel.
                          */
            return av;
           
  
        }
        
             
        
    }
}
