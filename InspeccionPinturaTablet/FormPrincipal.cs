using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.DirectoryServices;

namespace InspeccionPinturaTablet
{
    public partial class FormPrincipal : Form
    {
        inspeccionesPinturaBD ipDB = new inspeccionesPinturaBD();
        int nuevaInsp = 0;
        string certificado;
        int finalizada;

        public FormPrincipal()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            comboBoxInsPintura.Items.Clear();
            // Aqui consulto y muestro en el combox
            DataTable table;
            table = ipDB.obtenerInspecciones();

            foreach (DataRow row in table.Rows)
            {
                certificado = Convert.ToString(row["certificado"]);
                finalizada = Convert.ToInt32(row["finalizada"]);

                if (finalizada == 1)
                {
                    certificado = certificado + " (FINALIZADA) ";
                }

                comboBoxInsPintura.Items.Add(certificado);
                
             

            }
            Cursor.Current = Cursors.Default;

        }


        private void crearInspeccion_Click(object sender, EventArgs e)
        {
            certificado = "";

            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;

            textBoxCliente.Visible = true;
            textBoxPedido.Visible = true;
            textBoxContenedor.Visible = true;
            textBoxPlanCalidad.Visible = true;
            textBoxCertificado.Visible = true;
            textBoxProyectos.Visible = true;
            textBoxPackingList.Visible = true;
            textBoxTorre.Visible = true;
            dateTimePicker1.Visible = true;
            textBoxInspector.Visible = true;
            textBoxLugar.Visible = true;

            boton1.Visible = true;
            boton2.Visible = true;
            boton3.Visible = true;
            boton4.Visible = true;
            boton5.Visible = true;
            boton6.Visible = true;
            boton7.Visible = true;
            boton8.Visible = true;


            textBoxCliente.Text = null;
            textBoxPedido.Text = null;
            textBoxPackingList.Text = null;
            textBoxContenedor.Text = null;
            textBoxPlanCalidad.Text = null;
            textBoxCertificado.Text = null;
            textBoxProyectos.Text = null;
            textBoxTorre.Text = null;
            dateTimePicker1.Text = null;
            textBoxInspector.Text = null;
            textBoxLugar.Text = null;

            comboBoxInsPintura.Text = null;

            nuevaInsp = 1;
            modificarInspecciones.Text = "Guardar Inspección";

            //EL certificado no es editable
            textBoxCertificado.Enabled = false;





        }



        //Muestro todos los datos
        private void comboBoxInsPintura_SelectedIndexChanged(object sender, EventArgs e)
        {
            //AQUI ACTULIZADO LOS VALORES DE COMOBOX Y LO IGUALO A PEDIDO,PACKING Y CONTENEDOR
            comboBoxInsPintura.Items.Clear();
            modificarInspecciones.Text = "GUARDAR CAMBIOS";
            // Aqui consulto y muestro en el combox
            DataTable tableCobox;
            tableCobox = ipDB.obtenerInspecciones();

            foreach (DataRow row in tableCobox.Rows)
            {
                certificado = Convert.ToString(row["certificado"]);

                finalizada = Convert.ToInt32(row["finalizada"]);

                if (finalizada == 1)
                {
                    certificado = certificado + " (FINALIZADA) ";
                }

                comboBoxInsPintura.Items.Add(certificado);
            }


            if (comboBoxInsPintura.Text != "")
            {
                nuevaInsp = 0;
                certificado = comboBoxInsPintura.Text;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                Finalizada.Visible = true;

                textBoxCliente.Visible = true;
                textBoxPedido.Visible = true;
                textBoxContenedor.Visible = true;
                textBoxPlanCalidad.Visible = true;
                textBoxCertificado.Visible = true;
                textBoxProyectos.Visible = true;
                textBoxPackingList.Visible = true;
                textBoxTorre.Visible = true;
                dateTimePicker1.Visible = true;
                textBoxInspector.Visible = true;
                textBoxLugar.Visible = true;

                boton1.Visible = true;
                boton2.Visible = true;
                boton3.Visible = true;
                boton4.Visible = true;
                boton5.Visible = true;
                boton6.Visible = true;
                boton7.Visible = true;
                boton8.Visible = true;

                eliminarInspeccion.Visible = true;

                //EL certificado no es editable
                textBoxCertificado.Enabled = false;

                // Busco en base de datos los valores y los muestros por los campos correspondientes
                certificado = certificado.Replace(" (FINALIZADA) ","");
                DataTable table = null;
                table = ipDB.obtenerDatosInspeccion(certificado);
                int valorFinalizada = 0;
                foreach (DataRow row in table.Rows)
                {
                    textBoxCliente.Text = Convert.ToString(row["cliente"]);
                    textBoxPedido.Text = Convert.ToString(row["pedido"]);
                    textBoxContenedor.Text = Convert.ToString(row["contenedor"]);
                    textBoxPlanCalidad.Text = Convert.ToString(row["plancalidad"]);
                    textBoxCertificado.Text = Convert.ToString(row["certificado"]);
                    textBoxProyectos.Text = Convert.ToString(row["proyecto"]);
                    textBoxPackingList.Text = Convert.ToString(row["packing"]);
                    textBoxTorre.Text = Convert.ToString(row["torre"]);
                    dateTimePicker1.Text = Convert.ToString(row["fecha"]);

                    textBoxInspector.Text = Convert.ToString(row["inspector"]);
                    textBoxLugar.Text = Convert.ToString(row["lugar"]);

                    valorFinalizada = Convert.ToInt16(row["finalizada"]);


                }

                if (valorFinalizada == 0) Finalizada.Checked = false;
                else Finalizada.Checked = true;

            }


        }



        private void guardarcambios_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;


            try
            {
                if (nuevaInsp == 1)
                {
                    DateTime date = DateTime.Parse(dateTimePicker1.Text);
                    string dateFormatted = date.ToString("yyyy-MM-dd");

                    if (textBoxCliente.Text == "") textBoxCliente.Text = null;
                    if (textBoxPedido.Text == "") textBoxPedido.Text = null;
                    if (textBoxPackingList.Text == "") textBoxPackingList.Text = null;
                    if (textBoxContenedor.Text == "") textBoxContenedor.Text = null;
                    if (textBoxPlanCalidad.Text == "") textBoxPlanCalidad.Text = null;

                    if (textBoxProyectos.Text == "") textBoxProyectos.Text = null;
                    if (textBoxTorre.Text == "") textBoxTorre.Text = null;
                    //fecha
                    if (textBoxInspector.Text == "") textBoxInspector.Text = null;
                    if (textBoxLugar.Text == "") textBoxLugar.Text = null;


                    ipDB.crearDatosInspeccion(Convert.ToString(textBoxCliente.Text),
                                               Convert.ToInt32(textBoxPedido.Text),
                                               Convert.ToInt32(textBoxPackingList.Text),
                                               Convert.ToInt32(textBoxContenedor.Text),
                                               Convert.ToString(textBoxPlanCalidad.Text),
                                               Convert.ToString("TRC-" + textBoxPedido.Text + "-" + textBoxContenedor.Text + "-" + textBoxTorre.Text),
                                               Convert.ToString(textBoxProyectos.Text),
                                               Convert.ToString(textBoxTorre.Text),
                                               dateFormatted,
                                               Convert.ToString(textBoxInspector.Text),
                                               Convert.ToString(textBoxLugar.Text));

                    modificarInspecciones.Text = "GUARDAR CAMBIOS";
                    certificado = "TRC-" + textBoxPedido.Text + "-" + textBoxContenedor.Text + "-" + textBoxTorre.Text;

                }
                else
                {
                    DateTime date = DateTime.Parse(dateTimePicker1.Text);
                    string dateFormatted = date.ToString("yyyy-MM-dd");

                    int valorFinalizada = 0;
                    if( Finalizada.Checked == true) valorFinalizada=1;


                    ipDB.actulizarDatosInspeccion(Convert.ToString(textBoxCliente.Text),
                                               Convert.ToInt32(textBoxPedido.Text),
                                               Convert.ToInt32(textBoxPackingList.Text),
                                               Convert.ToInt32(textBoxContenedor.Text),
                                               Convert.ToString(textBoxPlanCalidad.Text),
                                               Convert.ToString(textBoxCertificado.Text),
                                               Convert.ToString(textBoxProyectos.Text),
                                               Convert.ToString(textBoxTorre.Text),
                                               dateFormatted,
                                               Convert.ToString(textBoxInspector.Text),
                                               Convert.ToString(textBoxLugar.Text), valorFinalizada);
                }




                string inspePint = "TRC-" + textBoxPedido.Text + "-" + textBoxContenedor.Text + "-" + textBoxTorre.Text;
                comboBoxInsPintura.Text = inspePint;

                Cursor.Current = Cursors.Default;
                nuevaInsp = 0;


                //Vuelvo a cargar el formulario para refrescar el combobox y que aprezcan las inspecciones nuevas creadas

            }
            catch
            {

                MessageBox.Show("ERROR AL GUARDAR LA CABECERA DE LA INSPECCIÓN");
            }
        }

        private void eliminarInspeccion_Click(object sender, EventArgs e)
        {
            DialogResult resul = MessageBox.Show("¿Seguro que quiere eliminar la inspección?", "Eliminar Inspección", MessageBoxButtons.YesNo);
            if (resul == DialogResult.Yes)
            {
                //aqui el codigo para eliminar el la inspeccion;
                Cursor.Current = Cursors.WaitCursor;

                ipDB.eliminarDatosInspeccion(certificado);
                Form1_Load(sender, e);
                crearInspeccion_Click(sender, e);
                MessageBox.Show("Inspección eliminada correctamente");
                Cursor.Current = Cursors.Default;

            }

        }

        private void boton1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(certificado);
            form1.Show();
        }

        private void boton2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(certificado);
            form2.Show();
        }

        private void boton3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(certificado);
            form3.Show();
        }

        private void boton4_Click(object sender, EventArgs e)
        {
            Forms4 form4 = new Forms4(certificado);
            form4.Show();
        }

        private void boton5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(certificado);
            form5.Show();
        }

        private void boton6_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(certificado);
            form6.Show();

        }

        private void boton7_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(certificado);
            form7.Show();
        }

        private void boton8_Click(object sender, EventArgs e)
        {
            Forms8 form8 = new Forms8(certificado);
            form8.Show();
        }

        private void generarDocumento_Click2(object sender, EventArgs e)
        {
            string codEmp;
            string idioma;
            elegirEmpresa empresa = new elegirEmpresa();
            DialogResult res = empresa.ShowDialog();

            if (res == DialogResult.OK)
            {
                //recuperando la variable publica del formulario 2

                codEmp = empresa.empresa;
                idioma = empresa.idioma;
                generarDocumento_Click(sender, e, codEmp, idioma);
            }
        }
        private void generarDocumento_Click(object sender, EventArgs e, string codEmp,string idioma)
        {
            Cursor.Current = Cursors.WaitCursor;
          
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();

            string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "/", "<", ">", ":" };
            string nuevoCertificado = certificado;
            foreach (var c in charsToRemove)
            {
                nuevoCertificado = nuevoCertificado.Replace(c, string.Empty);
            }

            // AQUI PRIMERO OBTENGO LOS DATOS DE LOS DISTINTOS APARTADOS.

            // obtengo los datos del apartado 1.
            string plreclamacion = " ", cpreclamacion = "cp ", apreclamacion = "", plcorrecto = "", cpcorrecto = "", apcorrecto = "", fecha1 = "";
            DataTable table = null;
            table = ipDB.datos1InsPaquetes(certificado);
            foreach (DataRow row in table.Rows)
            {

                plreclamacion = Convert.ToString(row["plreclamacion"]);
                cpreclamacion = Convert.ToString(row["cpreclamacion"]);
                apreclamacion = Convert.ToString(row["apreclamacion"]);

                plcorrecto = Convert.ToString(row["plcorrecto"]);
                cpcorrecto = Convert.ToString(row["cpcorrecto"]);
                apcorrecto = Convert.ToString(row["apcorrecto"]);

                fecha1 = Convert.ToString(row["fecha"]);

            }
            String filename = "";
            if (codEmp == "3")
            {

                // filename = "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\Plantilla\\plantillaPinturaIMEDEXSA"; //.docx";
                // CARLOS 05/08/2025 - Se cambia la ruta de la plantilla a una ruta común para que no tenga que existir dicha plantilla en el ordenador en local del usuario en cuestión.
                filename = "\\\\nas01\\Herramientas_Comunes$\\plantilla\\plantillaPinturaIMEDEXSA";
            }

            if (codEmp == "60")
            {

                //filename = "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\Plantilla\\plantillaPinturaMADE"; //.docx";
                // CARLOS 05/08/2025 - Se cambia la ruta de la plantilla a una ruta común para que no tenga que existir dicha plantilla en el ordenador en local del usuario en cuestión.
                filename = "\\\\nas01\\Herramientas_Comunes$\\plantilla\\plantillaPinturaMADE";
            }

            switch (idioma)
            {
                case "FR":
                    filename += "_frances";
                    break;

                default:
                    break;
            }

            filename += ".docx";

            if (System.IO.File.Exists(filename) == false)
            {
                MessageBox.Show("ERROR CON LA PLANTILLA DEL DOCUMENTO. CONSULTE CON EL DEPARTAMENTO DE INFORMÁTICA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            doc = word.Documents.Add(filename);
            word.Visible = true;

            // Loop through all sections
            foreach (Microsoft.Office.Interop.Word.Section section in doc.Sections)
            {



                #region // Cargar cabecera y pie de pagina
                foreach (Microsoft.Office.Interop.Word.HeaderFooter header in section.Headers)
                {
                    //Loop through all fields
                    foreach (Microsoft.Office.Interop.Word.Field campo in header.Range.Fields)
                    {
                        Microsoft.Office.Interop.Word.Range rngFieldCode = campo.Code;
                        String fieldText = rngFieldCode.Text;

                        if (fieldText.StartsWith(" MERGEFIELD"))
                        {
                            Int32 endMerge = fieldText.IndexOf("\\");
                            Int32 fieldNameLength = fieldText.Length - endMerge;
                            String fieldName = fieldText.Substring(11, endMerge - 11);

                            // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE
                            fieldName = fieldName.Trim();

                            //aqui seria para la cabeza, que en este caso no ahi.

                        }


                    }




                    foreach (Microsoft.Office.Interop.Word.HeaderFooter pie in section.Footers)
                    {
                        //Loop through all fields
                        foreach (Microsoft.Office.Interop.Word.Field campo in pie.Range.Fields)
                        {
                            Microsoft.Office.Interop.Word.Range rngFieldCode = campo.Code;
                            String fieldText = rngFieldCode.Text;

                            if (fieldText.StartsWith(" MERGEFIELD"))
                            {
                                Int32 endMerge = fieldText.IndexOf("\\");
                                Int32 fieldNameLength = fieldText.Length - endMerge;
                                String fieldName = fieldText.Substring(11, endMerge - 11);

                                // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE
                                fieldName = fieldName.Trim();


                                if (fieldName == "lugar")
                                {
                                    campo.Select();
                                    if (textBoxLugar.Text == "") word.Selection.TypeText(" ");
                                    else word.Selection.TypeText(textBoxLugar.Text);
                                }
                                if (fieldName == "inspector")
                                {
                                    campo.Select();
                                    if (textBoxInspector.Text == "") word.Selection.TypeText(" ");
                                    else word.Selection.TypeText(textBoxInspector.Text);
                                }

                            }
                        }
                    }



                    foreach (Microsoft.Office.Interop.Word.Field campo in doc.Fields)
                    {
                        Microsoft.Office.Interop.Word.Range rngFieldCode = campo.Code;
                        String fieldText = rngFieldCode.Text;

                        if (fieldText.StartsWith(" MERGEFIELD"))
                        {
                            Int32 endMerge = fieldText.IndexOf("\\");
                            Int32 fieldNameLength = fieldText.Length - endMerge;
                            String fieldName = fieldText.Substring(11, endMerge - 11);

                            // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE
                            fieldName = fieldName.Trim();
                            if (fieldName == "inspector")
                            {
                                string nombre = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;
                                

                                campo.Select();
                                if (textBoxInspector.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(nombre);
                            }
                            if (fieldName == "lugar")
                            {
                                campo.Select();
                                if (textBoxLugar.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxLugar.Text);
                            }

                            if (fieldName == "cliente")
                            {
                                campo.Select();
                                if (textBoxCliente.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxCliente.Text);
                            }

                            if (fieldName == "pedido")
                            {
                                campo.Select();
                                if (textBoxPedido.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxPedido.Text);
                            }
                            if (fieldName == "contenedor")
                            {
                                campo.Select();
                                if (textBoxContenedor.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxContenedor.Text);
                            }
                            if (fieldName == "plancalidad")
                            {
                                campo.Select();
                                if (textBoxPlanCalidad.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxPlanCalidad.Text);
                            }
                            if (fieldName == "certificado")
                            {
                                campo.Select();
                                if (certificado == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(certificado);
                            }
                            if (fieldName == "proyecto")
                            {
                                campo.Select();
                                if (textBoxProyectos.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxProyectos.Text);
                            }
                            if (fieldName == "packing")
                            {
                                campo.Select();
                                if (textBoxPackingList.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxPackingList.Text);
                            }
                            if (fieldName == "torre")
                            {
                                campo.Select();
                                if (textBoxTorre.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxTorre.Text);
                            }
                            if (fieldName == "fecha")
                            {
                                campo.Select();

                                if (dateTimePicker1.Text == "") word.Selection.TypeText(" ");
                                else
                                {
                                    string fechaForm = dateTimePicker1.Text;
                                    word.Selection.TypeText(fechaForm);
                                }
                            }
                #endregion


                            #region // 1º Tabla primera, Inspeccion de paquetes
                            if (fieldName == "fecha1")
                            {
                                campo.Select();
                                if (fecha1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(fecha1.Substring(0,10));
                            }

                            if (fieldName == "plreclamacion")
                            {
                                campo.Select();
                                if (plreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(plreclamacion);
                            }

                            if (fieldName == "cpreclamacion")
                            {
                                campo.Select();
                                if (cpreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(cpreclamacion);
                            }

                            if (fieldName == "apreclamacion")
                            {
                                campo.Select();
                                if (apreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(apreclamacion);
                            }


                            if (fieldName == "plsi")
                            {
                                campo.Select();
                                if (plcorrecto != "")
                                {
                                    if (Convert.ToBoolean(plcorrecto) == true) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                } else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "plno")
                            {
                                campo.Select();
                                if (plcorrecto != "")
                                {
                                    if (Convert.ToBoolean(plcorrecto) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "cpsi")
                            {
                                campo.Select();
                                if (cpcorrecto != "")
                                {
                                    if (Convert.ToBoolean(cpcorrecto) == true) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                               
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "cpno")
                            {
                                campo.Select();
                                if (cpcorrecto != "")
                                {
                                    if (Convert.ToBoolean(cpcorrecto) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                               
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "apsi")
                            {
                                if (apcorrecto != "")
                                {
                                campo.Select();
                                    if (Convert.ToBoolean(apcorrecto) == true) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "apno")
                            {
                                if (apcorrecto != "")
                                {
                                campo.Select();
                                if (Convert.ToBoolean(apcorrecto) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }
                            #endregion

                            #region 2º Tabla segunda, Inspeccion granallado

                            // obtengo los datos del apartado 2.
                            DataTable table2 = null;
                            table2 = ipDB.datos2InsGranallado(certificado);

                            string aereclamacion = "", gereclamacion = "", zincinicial = "", zincfinal = "", aecorrecto = "", gecorrecto = "", zincISO = "";
                            string aeapli = "", geapli = "", fecha2 = "";
                            string zincinicialpieza1 = "", zincinicialvalor1 = "", zincinicialpieza2 = "", zincinicialvalor2 = "", zincinicialpieza3 = "", 
                                zincinicialvalor3 = "", zincinicialpieza4 = "", zincinicialvalor4 = "", zincinicialpieza5 = "", zincinicialvalor5 = "";
                            string zincgranalladopieza1 = "", zincgranalladovalor1 = "", zincgranalladopieza2 = "", zincgranalladovalor2 = "", zincgranalladopieza3 = "",
                                zincgranalladovalor3 = "", zincgranalladopieza4 = "", zincgranalladovalor4 = "", zincgranalladopieza5 = "", zincgranalladovalor5 = "";

                            string wasserpel = "", bresle = "", compresores = "";

                            foreach (DataRow row in table2.Rows)
                            {
                                aereclamacion = Convert.ToString(row["aereclamacion"]);
                                gereclamacion = Convert.ToString(row["gereclamacion"]);
                                zincinicial = Convert.ToString(row["zincinicial"]);
                                zincfinal = Convert.ToString(row["zincfinal"]);

                                zincinicialpieza1 = Convert.ToString(row["zincinicialpieza1"]); 
                                zincinicialvalor1 = Convert.ToString(row["zincinicialvalor1"]);
                                zincinicialpieza2 = Convert.ToString(row["zincinicialpieza2"]);
                                zincinicialvalor2= Convert.ToString(row["zincinicialvalor2"]);
                                zincinicialpieza3 = Convert.ToString(row["zincinicialpieza3"]);
                                zincinicialvalor3 = Convert.ToString(row["zincinicialvalor3"]);
                                zincinicialpieza4 = Convert.ToString(row["zincinicialpieza4"]);
                                zincinicialvalor4= Convert.ToString(row["zincinicialvalor4"]);
                                zincinicialpieza5 = Convert.ToString(row["zincinicialpieza5"]);
                                zincinicialvalor5 = Convert.ToString(row["zincinicialvalor5"]);

                                zincgranalladopieza1 = Convert.ToString(row["zincgranalladopieza1"]);
                                zincgranalladovalor1 = Convert.ToString(row["zincgranalladovalor1"]);
                                zincgranalladopieza2 = Convert.ToString(row["zincgranalladopieza2"]);
                                zincgranalladovalor2 = Convert.ToString(row["zincgranalladovalor2"]);
                                zincgranalladopieza3 = Convert.ToString(row["zincgranalladopieza3"]);
                                zincgranalladovalor3 = Convert.ToString(row["zincgranalladovalor3"]);
                                zincgranalladopieza4 = Convert.ToString(row["zincgranalladopieza4"]);
                                zincgranalladovalor4 = Convert.ToString(row["zincgranalladovalor4"]);
                                zincgranalladopieza5 = Convert.ToString(row["zincgranalladopieza5"]);
                                zincgranalladovalor5 = Convert.ToString(row["zincgranalladovalor5"]);

                                aecorrecto = Convert.ToString(row["aecorrecto"]);
                                gecorrecto = Convert.ToString(row["gecorrecto"]);
                                zincISO = Convert.ToString(row["zincISO"]);

                                aeapli = Convert.ToString(row["aeaplicable"]);
                                geapli = Convert.ToString(row["geaplicable"]);

                                wasserpel = Convert.ToString(row["wasserpel"]);
                                bresle = Convert.ToString(row["bresle"]);
                                compresores = Convert.ToString(row["compresores"]);

                                fecha2 = Convert.ToString(row["fecha"]);
                            }
                            if (fieldName == "fecha2")
                            {
                                campo.Select();
                                if (fecha2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(fecha2.Substring(0, 10));
                            }

                            if (fieldName == "aereclamacion")
                            {
                                campo.Select();
                                if (aereclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(aereclamacion);
                            }

                            if (fieldName == "gereclamacion")
                            {
                                campo.Select();
                                if (gereclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(gereclamacion);
                            }

                            if (fieldName == "zincinicial")
                            {
                                campo.Select();
                                if (zincinicial == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicial);
                            }

                            if (fieldName == "zincfinal")
                            {
                                campo.Select();
                                if (zincfinal == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincfinal);
                            }

                            //Piezas zinc inicial

                            if (fieldName == "piezai1")
                            {
                                campo.Select();
                                if (zincinicialpieza1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialpieza1);
                            }

                            if (fieldName == "valori1")
                            {
                                campo.Select();
                                if (zincinicialvalor1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialvalor1);
                            }

                            if (fieldName == "piezai2")
                            {
                                campo.Select();
                                if (zincinicialpieza2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialpieza2);
                            }

                            if (fieldName == "valori2")
                            {
                                campo.Select();
                                if (zincinicialvalor2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialvalor2);
                            }

                            if (fieldName == "piezai3")
                            {
                                campo.Select();
                                if (zincinicialpieza3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialpieza3);
                            }

                            if (fieldName == "valori3")
                            {
                                campo.Select();
                                if (zincinicialvalor3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialvalor3);
                            }

                            if (fieldName == "piezai4")
                            {
                                campo.Select();
                                if (zincinicialpieza4 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialpieza4);
                            }

                            if (fieldName == "valori4")
                            {
                                campo.Select();
                                if (zincinicialvalor4 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialvalor4);
                            }

                            if (fieldName == "piezai5")
                            {
                                campo.Select();
                                if (zincinicialpieza5 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialpieza5);
                            }

                            if (fieldName == "valori5")
                            {
                                campo.Select();
                                if (zincinicialvalor5 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincinicialvalor5);
                            }

                            // GRANADLLADO

                            if (fieldName == "piezag1")
                            {
                                campo.Select();
                                if (zincgranalladopieza1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladopieza1);
                            }

                            if (fieldName == "valorg1")
                            {
                                campo.Select();
                                if (zincgranalladovalor1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladovalor1);
                            }

                            if (fieldName == "piezag2")
                            {
                                campo.Select();
                                if (zincgranalladopieza2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladopieza2);
                            }

                            if (fieldName == "valorg2")
                            {
                                campo.Select();
                                if (zincgranalladovalor2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladovalor2);
                            }

                            if (fieldName == "piezag3")
                            {
                                campo.Select();
                                if (zincgranalladopieza3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladopieza3);
                            }

                            if (fieldName == "valorg3")
                            {
                                campo.Select();
                                if (zincgranalladovalor3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladovalor3);
                            }

                            if (fieldName == "piezag4")
                            {
                                campo.Select();
                                if (zincgranalladopieza4 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladopieza4);
                            }

                            if (fieldName == "valorg4")
                            {
                                campo.Select();
                                if (zincgranalladovalor3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladovalor3);
                            }

                            if (fieldName == "piezag5")
                            {
                                campo.Select();
                                if (zincgranalladopieza5 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladopieza5);
                            }

                            if (fieldName == "valorg5")
                            {
                                campo.Select();
                                if (zincgranalladovalor5 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(zincgranalladovalor5);
                            }

                            if (fieldName == "aeapli")
                            {
                                if (aeapli != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(aeapli) == true) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "geapli")
                            {
                                if (geapli != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(geapli) == true) word.Selection.TypeText("✓");else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "aeno")
                            {
                                if (plcorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(plcorrecto) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "aesi")
                            {
                                if (aecorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(aecorrecto) == true) word.Selection.TypeText("✓");else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }


                            if (fieldName == "gesi")
                            {
                                if (gecorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(gecorrecto) == true) word.Selection.TypeText("✓");else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "geno")
                            {

                                if (gecorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(gecorrecto) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "zincsi")
                            {
                                if (zincISO != "")
                                {
                                     campo.Select();
                                     if (Convert.ToBoolean(zincISO) == true) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "zincno")
                            {
                                if (zincISO != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(zincISO) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }


                            if (fieldName == "wasi")
                            {
                                if (wasserpel != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(wasserpel) == true) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "wano")
                            {

                                if (wasserpel != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(wasserpel) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "enbrasi")
                            {
                                if (bresle != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(bresle) == true) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "enbrano")
                            {

                                if (bresle != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(bresle) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "comlimastmsi")
                            {
                                if (compresores != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(compresores) == true) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "comlimastmno")
                            {

                                if (compresores != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(compresores) == false) word.Selection.TypeText("✓"); else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }
                            

                            #endregion

                            #region 3º Tabla tercera, Ensayo de evaluacion de rugosidad y limpieza superficial
                            DataTable table3 = null;
                            table3 = ipDB.datos3RuglSup(certificado);

                            string csreclamacion = "", rsreclamacion = "", cscorrecto = "", rscorrecto = "";
                            string cspiezaensayada = "", csvalor = "", csequipoensayo = "", rspiezaensayada = "", rsvalor = "", rsequipoensayo = "";
                            string csapli = "", rsapli = "", fecha3 = "";
                            string rsnserie  ="";
                            string tpreclamacion = "", tpcorrecto = "", tpapli = "", cintadh = "", fondoConst = "";
                            string test1pieza = "", test1evcantidad = "", test2pieza = "", test2evcantidad = "", test3pieza = "", test3evcantidad = "";

                            string limpieza = "", tension = "", perla = "", ensayo = "", tsp1 = "", tsp2 = "", tsp3 = "";

                            foreach (DataRow row in table3.Rows)
                            {
                                if (row["fecha"] != DBNull.Value)
                                {
                                    fecha3 = Convert.ToString(row["fecha"]);
                                }

                                csreclamacion = Convert.ToString(row["csreclamacion"]);
                                rsreclamacion = Convert.ToString(row["rsreclamacion"]);
                                tpreclamacion = Convert.ToString(row["tpreclamacion"]);

                                cscorrecto = Convert.ToString(row["cscorrecto"]);
                                csapli = Convert.ToString(row["csaplicable"]);

                                tpcorrecto = Convert.ToString(row["tpcorrecto"]);
                                tpapli = Convert.ToString(row["tpaplicable"]);

                                rscorrecto = Convert.ToString(row["cscorrecto"]);
                                rsapli = Convert.ToString(row["csaplicable"]);

                                cspiezaensayada = Convert.ToString(row["cspiezaensayada"]);
                                csvalor = Convert.ToString(row["csvalor"]);
                                csequipoensayo = Convert.ToString(row["csequipoensayo"]);

                                rspiezaensayada = Convert.ToString(row["rspiezaensayada"]);
                                rsvalor = Convert.ToString(row["rsvalor"]);
                                rsequipoensayo = Convert.ToString(row["rsequipoensayo"]);

                                cintadh = Convert.ToString(row["cntadhva"]);
                                fondoConst = Convert.ToString(row["fondoconst"]);

                                test1pieza = Convert.ToString(row["test1pieza"]);
                                test1evcantidad = Convert.ToString(row["test1evcantidad"]);

                                test2pieza = Convert.ToString(row["test2pieza"]);
                                test2evcantidad = Convert.ToString(row["test2evcantidad"]);

                                test3pieza = Convert.ToString(row["test3pieza"]);
                                test3evcantidad = Convert.ToString(row["test3evcantidad"]);
                                rsnserie = Convert.ToString(row["rsnserie"]);

                                limpieza = Convert.ToString(row["limpieza"]);
                                tension = Convert.ToString(row["tension"]);
                                perla = Convert.ToString(row["perla"]);
                                ensayo = Convert.ToString(row["ensayo"]);

                                tsp1 = Convert.ToString(row["tenSupP1"]);
                                tsp2 = Convert.ToString(row["tenSupP2"]);
                                tsp3 = Convert.ToString(row["tenSupP3"]);
                            }

                            if (fieldName == "rsnserie")
                            {
                                campo.Select();
                                if (rsnserie == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(rsnserie);
                            } 

                            if (fieldName == "fecha3")
                            {
                                campo.Select();
                                if (fecha3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(fecha3.Substring(0, 10));
                            }

                            if (fieldName == "csreclamacion")
                            {
                                campo.Select();
                                if (csreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(csreclamacion);
                            }

                            if (fieldName == "rsreclamacion")
                            {
                                campo.Select();
                                if (rsreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(rsreclamacion);
                            }

                            if (fieldName == "csapli")
                            {
                                if (csapli != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(csapli) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "csno")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(cscorrecto) == false) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "cssi")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(cscorrecto) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "rsapli")
                            {
                                if (rsapli != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(rsapli) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "rsno")
                            {
                                if (rscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(rscorrecto) == false) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "rssi")
                            {
                                if (rscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(rscorrecto) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "cspiezaensayada")
                            {
                                campo.Select();
                                if (cspiezaensayada == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(cspiezaensayada);
                            }

                            if (fieldName == "csvalor")
                            {
                                campo.Select();
                                if (csvalor == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(csvalor);
                            }

                            if (fieldName == "csequipoensayo")
                            {
                                if (csequipoensayo == "M3") csequipoensayo = "MC3-SST METODO BRESLE acc-iso 8502-6/8502/9";
                                if (csequipoensayo == "Ru2") csequipoensayo = "Ru2-mms inspection nº serie 201-170613/42175";
                                if (csequipoensayo == "E14") csequipoensayo = "E14 defelsko";

                                campo.Select();
                                if (csequipoensayo == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(csequipoensayo);
                            }

                            if (fieldName == "rspiezaensayada")
                            {
                               

                                campo.Select();
                                if (rspiezaensayada == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(rspiezaensayada);
                            }

                            if (fieldName == "rsvalor")
                            {
                                campo.Select();
                                if (rsvalor == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(rsvalor);
                            }

                            if (fieldName == "rsequipoensayo")
                            {
                                if (rsequipoensayo == "M3") rsequipoensayo = "MC3-SST METODO BRESLE acc-iso 8502-6/8502/9";
                                if (rsequipoensayo == "Ru2") rsequipoensayo = "Ru2-mms inspection nº serie 201-170613/42175";
                                if (rsequipoensayo == "E14") rsequipoensayo = "E14 defelsko";

                                campo.Select();
                                if (rsequipoensayo == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(rsequipoensayo);
                            }

                            if (fieldName == "tpreclamacion")
                            {
                                campo.Select();
                                if (rsreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(rsreclamacion);
                            }

                            if (fieldName == "tpapli")
                            {
                                if (csapli != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(csapli) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "tpno")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(cscorrecto) == false) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "tpsi")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(cscorrecto) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "cintadh")
                            {
                               
                                campo.Select();
                                if (cintadh == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(cintadh);
                            }

                            if (fieldName == "fondoConst")
                            {

                                campo.Select();
                                if (fondoConst == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(fondoConst);
                            }

                            if (fieldName == "test1pieza")
                            {
                                campo.Select();
                                if (test1pieza == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(test1pieza);
                            }

                            if (fieldName == "test1evcantidad")
                            {
                                campo.Select();
                                if (test1evcantidad == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(test1evcantidad);
                            }

                            if (fieldName == "test2pieza")
                            {
                                campo.Select();
                                if (test2pieza == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(test2pieza);
                            }

                            if (fieldName == "test2evcantidad")
                            {
                                campo.Select();
                                if (test2evcantidad == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(test2evcantidad);
                            }

                            if (fieldName == "test3pieza")
                            {
                                campo.Select();
                                if (test3pieza == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(test3pieza);
                            }

                            if (fieldName == "test3evcantidad")
                            {
                                campo.Select();
                                if (test3evcantidad == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(test3evcantidad);
                            }

                            if (fieldName == "limcno")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(limpieza) == false) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "limcsi")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(limpieza) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "tsp1")
                            {
                                campo.Select();
                                if (tsp1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tsp1);
                            }

                            if (fieldName == "tsp2")
                            {
                                campo.Select();
                                if (tsp2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tsp2);
                            }

                            if (fieldName == "tsp3")
                            {
                                campo.Select();
                                if (tsp3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tsp3);
                            }

                            if (fieldName == "eb3no")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(ensayo) == false) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "eb3si")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(ensayo) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "pa3no")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(perla) == false) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "pa3si")
                            {
                                if (cscorrecto != "")
                                {
                                    campo.Select();
                                    if (Convert.ToBoolean(perla) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                                else word.Selection.TypeText(" ");
                            }

                            #endregion

                            #region 4º Tabla cuarta,  Inspección de la aplicación de la pintura de recubrimiento
                            DataTable table4 = null;
                            table4 = ipDB.datos4InsRecubrimiento(certificado);

                            string rereclamacion = "", secadoreclamacion = "", recorrecto = "", secado = "", fecha4 = "", fechaplicacion1 = "", fechaplicacion2 = "",fechaplicacion3="";
                            string tipopintura1 = "", tipopintura2 = "", tipopintura3 = "";
                            string lote1 = "", color1 = "", lote2 = "", color2 = "", lote3 = "", color3 = "";


                            foreach (DataRow row in table4.Rows)
                            {
                                if (row["fecha"] != DBNull.Value)
                                {
                                    fecha4 = Convert.ToString(row["fecha"]);
                                }
                                if (row["fechaplicacion3"] != DBNull.Value)
                                {
                                    fechaplicacion1 = Convert.ToString(row["fechaplicacion3"]);
                                }
                                if (row["fechaplicacion2"] != DBNull.Value)
                                {
                                    fechaplicacion2 = Convert.ToString(row["fechaplicacion2"]);
                                }

                                if (row["fechaplicacion3"] != DBNull.Value)
                                {
                                    fechaplicacion3 = Convert.ToString(row["fechaplicacion3"]);
                                }

                                rereclamacion = Convert.ToString(row["rereclamacion"]);
                                secadoreclamacion = Convert.ToString(row["secadoreclamacion"]);

                                recorrecto = Convert.ToString(row["recorrecto"]);
                                secado = Convert.ToString(row["secado"]);

                                tipopintura1 = Convert.ToString(row["tipopintura1"]);
                                lote1 = Convert.ToString(row["lote1"]);
                                color1 = Convert.ToString(row["color1"]);

                                tipopintura2 = Convert.ToString(row["tipopintura2"]);
                                lote2 = Convert.ToString(row["lote2"]);
                                color2 = Convert.ToString(row["color2"]);

                                tipopintura3 = Convert.ToString(row["tipopintura3"]);
                                lote3 = Convert.ToString(row["lote3"]);
                                color3 = Convert.ToString(row["color3"]);



                            }

                            if (fieldName == "fecha11")
                            {
                                if (tipopintura1 != "")
                                {
                                    campo.Select();
                                    if (fecha4 == "") word.Selection.TypeText(" ");
                                    else word.Selection.TypeText(fecha4.Substring(0, 10));
                                }
                                else
                                {
                                    campo.Select();
                                    word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "fecha12")
                            {
                                if (tipopintura2 != "")
                                {
                                    campo.Select();
                                    if (fecha4 == "") word.Selection.TypeText(" ");
                                    else word.Selection.TypeText(fecha4.Substring(0, 10));
                                }
                                else
                                {
                                    campo.Select();
                                    word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "fecha13")
                            {
                                if (tipopintura3 != "")
                                {
                                    campo.Select();
                                    if (fecha4 == "") word.Selection.TypeText(" ");
                                    else word.Selection.TypeText(fecha4.Substring(0, 10));
                                }
                                else
                                {
                                    campo.Select();
                                    word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "fechaplicacion1")
                            {
                                if (tipopintura1 != "")
                                {
                                    campo.Select();
                                    if (fechaplicacion1 == "") word.Selection.TypeText(" ");
                                    else word.Selection.TypeText(fechaplicacion1.Substring(0, 10));
                                }
                                else
                                {
                                    campo.Select();
                                    word.Selection.TypeText(" ");
                                }
                              }

                            if (fieldName == "fechaplicacion2")
                            {
                               
                                    campo.Select();
                                    if (fechaplicacion2 == "") word.Selection.TypeText(" ");
                                    else word.Selection.TypeText(fechaplicacion2.Substring(0, 10));
                                
                            
                              
                            }

                            
                            if (fieldName == "fechaplicacion3")
                            {
                                if (tipopintura3 != "")
                                {
                                    campo.Select();
                                    if (fechaplicacion3 == "") word.Selection.TypeText(" ");
                                    else word.Selection.TypeText(fechaplicacion3.Substring(0, 10));
                                }
                                else
                                {
                                    campo.Select();
                                    word.Selection.TypeText(" ");
                                }
                            }

                          

                            if (fieldName == "rereclamacion")
                            {
                                campo.Select();
                                if (rereclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(rereclamacion);
                            }

                            if (fieldName == "secadoreclamacion")
                            {
                                campo.Select();
                                if (secadoreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(secadoreclamacion);
                            }



                            if (fieldName == "reno")
                            {
                                campo.Select();
                                if( String.IsNullOrEmpty(recorrecto)){
                                    word.Selection.TypeText(" ");
                                }else{

                                    if (Convert.ToBoolean(recorrecto) == false) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");

                                }
                            }

                            if (fieldName == "resi")
                            {

                                campo.Select();
                                if( String.IsNullOrEmpty(recorrecto)){
                                    word.Selection.TypeText(" ");
                                }else{

                                if (Convert.ToBoolean(recorrecto) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "secadono")
                            {
                               
                                campo.Select();
                                if( String.IsNullOrEmpty(recorrecto)){
                                    word.Selection.TypeText(" ");
                                }else{

                                if (Convert.ToBoolean(secado) == false) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "secadosi")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(secado) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "tipopintura1")
                            {
                                campo.Select();
                                if (tipopintura1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tipopintura1);
                            }


                            if (fieldName == "lote1")
                            {
                                campo.Select();
                                if (lote1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(lote1);
                            }

                            if (fieldName == "color1")
                            {
                                campo.Select();
                                if (color1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(color1);
                            }

                            if (fieldName == "tipopintura2")
                            {
                                campo.Select();
                                if (tipopintura2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tipopintura2);
                            }


                            if (fieldName == "lote2")
                            {
                                campo.Select();
                                if (lote2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(lote2);
                            }

                            if (fieldName == "color2")
                            {
                                campo.Select();
                                if (color2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(color2);
                            }

                            if (fieldName == "tipopintura3")
                            {
                                campo.Select();
                                if (tipopintura3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tipopintura3);
                            }


                            if (fieldName == "lote3")
                            {
                                campo.Select();
                                if (lote3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(lote3);
                            }

                            if (fieldName == "color3")
                            {
                                campo.Select();
                                if (color3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(color3);
                            }


                            #endregion

                            #region 5º Tabla quinta,  Inspección de la aplicación de la pintura espesor
                            DataTable table5 = null;
                            table5 = ipDB.datos5Espesor(certificado);

                            string eschreclamacion = "", eschaplicable = "", eschcorrecto = "";
                            string galvanizado = "", capa2 = "", capa3 = "";
                            string chequipoensayo = "", valorpeine="";

                            string escsreclamacion = "",  escscorrecto = "";
                            string min1 = "", med1 = "", max1 = "", min2 = "", med2 = "", max2 = "";
                            string escsequipoensayo5 = "";
                            
                            foreach (DataRow row in table5.Rows)
                            {
                                eschreclamacion = Convert.ToString(row["eschreclamacion"]);
                                eschaplicable = Convert.ToString(row["eschaplicable"]);
                                eschcorrecto = Convert.ToString(row["eschcorrecto"]);

                                galvanizado = Convert.ToString(row["galvanizado"]);
                                capa2 = Convert.ToString(row["capa2"]);
                                capa3 = Convert.ToString(row["capa3"]);
                                chequipoensayo = Convert.ToString(row["eschequipoEnsayo"]);


                                escsreclamacion = Convert.ToString(row["escsreclamacion"]);
                                escsequipoensayo5 = Convert.ToString(row["escsequipoEnsayo"]);
                                escscorrecto = Convert.ToString(row["escscorrecto"]);

                                min1 = Convert.ToString(row["capa1min"]);
                                med1 = Convert.ToString(row["capa1med"]);
                                max1 = Convert.ToString(row["capa1max"]);

                                min2 = Convert.ToString(row["capa2min"]);
                                med2 = Convert.ToString(row["capa2med"]);
                                max2 = Convert.ToString(row["capa2max"]);

                                valorpeine = Convert.ToString(row["valorpeine"]);

                             
                              
                            }

                           

                            if (fieldName == "chreclamacion")
                            {
                                campo.Select();
                                if (eschreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(eschreclamacion);
                            }



                            if (fieldName == "valorp")
                            {
                                campo.Select();

                                if (valorpeine == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(valorpeine);
                            }
                            if (fieldName == "chno")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(eschcorrecto) == false) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "chsi")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(eschcorrecto) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "chapli")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(eschaplicable) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "galmin")
                            {
                                campo.Select();
                                if (galvanizado == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(galvanizado);
                            }

                            if (fieldName == "galmed")
                            {
                                campo.Select();
                                if (capa2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(capa2);
                            }

                            if (fieldName == "galmax")
                            {
                                campo.Select();
                                if (capa3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(capa3);
                            }

                            if (fieldName == "pq1min")
                            {
                                campo.Select();
                                if (min1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(min1);
                            }

                            if (fieldName == "pq1med")
                            {
                                campo.Select();
                                if (med1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(med1);
                            }

                            if (fieldName == "pq1max")
                            {
                                campo.Select();
                                if (max1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(max1);
                            }

                            if (fieldName == "pq2min")
                            {
                                campo.Select();
                                if (min2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(min2);
                            }

                            if (fieldName == "pq2med")
                            {
                                campo.Select();
                                if (med2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(med2);
                            }

                            if (fieldName == "pq2max")
                            {
                                campo.Select();
                                if (max2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(max2);
                            }

                            
                            if (fieldName == "chequipoensayo")
                            {
                                if (chequipoensayo == "M3") chequipoensayo = "MC3-SST METODO BRESLE acc-iso 8502-6/8502/9";
                                if (chequipoensayo == "Ru2") chequipoensayo = "Ru2-mms inspection nº serie 201-170613/42175";
                                if (chequipoensayo == "E14") chequipoensayo = "E14 Defelsko";

                                campo.Select();

                                if (chequipoensayo == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(chequipoensayo);
                            }

                            if (fieldName == "escsreclamacion")
                            {
                                campo.Select();
                                if (escsreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(escsreclamacion);
                            }


                            if (fieldName == "escsno")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(escscorrecto) == false) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "escssi")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(escscorrecto) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "csmin")
                            {
                                campo.Select();
                                if (min1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(min1);
                            }

                            if (fieldName == "csmed")
                            {
                                campo.Select();
                                if (med1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(med1);
                            }

                            if (fieldName == "csmax")
                            {
                                campo.Select();
                                if (max1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(max1);
                            }

                            if (fieldName == "escsequipoensayo5")
                            {
                                if (escsequipoensayo5 == "M3") escsequipoensayo5 = "MC3-SST METODO BRESLE acc-iso 8502-6/8502/9";
                                if (escsequipoensayo5 == "Ru2") escsequipoensayo5 = "Ru2-mms inspection nº serie 201-170613/42175";
                                if (escsequipoensayo5 == "E14") escsequipoensayo5 = "E14 Defelsko";

                                campo.Select();
                                if (escsequipoensayo5 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(escsequipoensayo5);
                            }
                            #endregion

                            #region 6º Tabla sexta, Inspeccion final de la pintura de recubrimiento
                            DataTable table6 = null;
                            table6 = ipDB.datosa6InspFinalPinturaRecubrimiento(certificado);

                            string apreclamacion6 = "", apcorrecto6 = "", iereclamacion = "", iecorrecto = "", fecha6 = "";

                            string esreclamacion = "", escorrecto = "", esaplicable = "", gradosecado = "", esmetodo = "", esequipoensayo = "";
                            string tareclamacion = "", tacorrecto = "", taaplicable = "", tavalor = "", tametodo = "", taequipoensayo = "";

                            foreach (DataRow row in table6.Rows)
                            {
                                if (row["fecha"] != DBNull.Value)
                                {
                                    fecha6 = Convert.ToString(row["fecha"]);
                                }

                                apreclamacion6 = Convert.ToString(row["apreclamacion"]);
                                apcorrecto6 = Convert.ToString(row["apcorrecto"]);

                                iereclamacion = Convert.ToString(row["iereclamacion"]);
                                iecorrecto = Convert.ToString(row["iecorrecto"]);

                                esreclamacion = Convert.ToString(row["esreclamacion"]);
                                escorrecto = Convert.ToString(row["escorrecto"]);
                                esaplicable = Convert.ToString(row["esaplicable"]);
                                gradosecado = Convert.ToString(row["gradosecado"]);
                                esmetodo = Convert.ToString(row["esmetodo"]);
                                esequipoensayo = Convert.ToString(row["esequipoensayo"]);

                                tareclamacion = Convert.ToString(row["tareclamacion"]);
                                tacorrecto = Convert.ToString(row["tacorrecto"]);
                                taaplicable = Convert.ToString(row["taaplicable"]);
                                tavalor = Convert.ToString(row["tavalor"]);
                                tametodo = Convert.ToString(row["tametodo"]);
                                taequipoensayo = Convert.ToString(row["taequipoensayo"]);



                            }

                            if (fieldName == "fecha6")
                            {
                                campo.Select();
                                if (fecha6 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(fecha6.Substring(0, 10));
                            }

                            if (fieldName == "fechafinal")
                            {
                                campo.Select();
                                if (fecha6 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(fecha6.Substring(0, 10));
                            }

                            if (fieldName == "apreclamacion6")
                            {
                                campo.Select();
                                if (apreclamacion6 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(apreclamacion6);
                            }

                            if (fieldName == "iereclamacion")
                            {
                                campo.Select();
                                if (iereclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(iereclamacion);
                            }

                            if (fieldName == "apno6")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(apcorrecto6) == false) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "apsi6")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(apcorrecto6) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "ieno")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(iecorrecto) == false) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "iesi")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(iecorrecto) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }


                            if (fieldName == "esapli")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(esaplicable) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "essi")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(escorrecto) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "esno")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(escorrecto) == false) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "esreclamacion")
                            {
                                campo.Select();
                                if (esreclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(esreclamacion);
                            }

                            if (fieldName == "gradosecado")
                            {
                                campo.Select();
                                if (gradosecado == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(gradosecado);
                            }
                            if (fieldName == "esequipoensayo")
                            {

                                if (esequipoensayo == "M3") esequipoensayo = "MC3-SST METODO BRESLE acc-iso 8502-6/8502/9";
                                if (esequipoensayo == "Ru2") esequipoensayo = "Ru2-mms inspection nº serie 201-170613/42175";
                                if (esequipoensayo == "E14") esequipoensayo = "E14 Defelsko";
                                if (esequipoensayo == "CU1") esequipoensayo = "CU1 Neurterk";

                                if (esequipoensayo == "Discos de papel,goma y pesa > de 20 kg" || esequipoensayo == "Discos de papel,goma y pesa >20 kg"
                                    || esequipoensayo == "Discos de papel,goma y pesa> 20 kg" || esequipoensayo == "Discos de papel,goma y pesa > 20 kg"
                                    || esequipoensayo == "discos de papel, goma y pesa > 20 kg")
                                {
                                  esequipoensayo ="Papierscheiben,Gummi und Gewichte > 20kg";
                                }


                                campo.Select();
                                if (esequipoensayo == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(esequipoensayo);
                            }

                            if (fieldName == "taapli")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(taaplicable) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "tasi")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(tacorrecto) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "tano")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(tacorrecto) == false) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "tareclamacion")
                            {
                                campo.Select();
                                if (tareclamacion == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tareclamacion);
                            }

                            if (fieldName == "tavalor")
                            {
                                campo.Select();
                                if (tavalor == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tavalor);
                            }
                            if (fieldName == "taequipoensayo")
                            {

                                if (taequipoensayo == "M3") taequipoensayo = "MC3-SST METODO BRESLE acc-iso 8502-6/8502/9";
                                if (taequipoensayo == "Ru2") taequipoensayo = "Ru2-mms inspection nº serie 201-170613/42175";
                                if (taequipoensayo == "E14") taequipoensayo = "E14 defelsko";
                                if (taequipoensayo == "CU1") taequipoensayo = "CU1 Neurterk";

                                if (taequipoensayo == "Discos de papel,goma y pesa > de 20 kg" || taequipoensayo == "Discos de papel,goma y pesa >20 kg"
                                    || taequipoensayo == "Discos de papel,goma y pesa> 20 kg" || taequipoensayo == "Discos de papel,goma y pesa > 20 kg"
                                    || esequipoensayo == "discos de papel, goma y pesa > 20 kg")
                                {
                                    taequipoensayo = "Papierscheiben,Gummi und Gewichte > 20kg";
                                }


                                campo.Select();
                                if (taequipoensayo == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(taequipoensayo);
                            }

                            if (fieldName == "tametodo")
                            {
                                campo.Select();
                                if (tametodo == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tametodo);
                            }

                            if (fieldName == "esmetodo")
                            {
                                campo.Select();
                                if (esmetodo == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(esmetodo);
                            }
                            #endregion

                            #region 7º Tabla septima, Comentarios
                            DataTable table7 = null;
                            table7 = ipDB.datosa7Comentarios(certificado);

                            string comentarios = "";

                            foreach (DataRow row in table7.Rows)
                            {


                                comentarios = Convert.ToString(row["comentarios"]);


                            }

                            if (fieldName == "comentarios")
                            {
                                campo.Select();
                                if (comentarios == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(comentarios);
                            }


                            #endregion

                        }

                        String ofertaPath = "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\";
                        if (ofertaPath != "")
                        {


                            ofertaPath += nuevoCertificado + ".docx";

                            if (System.IO.File.Exists(ofertaPath) == false)
                                doc.SaveAs(ofertaPath);
                        }



                        word.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintPreview;
                        word.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintPreview;
                    }


                }

                #region Relleno las tablas del punto 5.

                DataTable table5G = null;
                table5G = ipDB.datos5EspesorGalvanizado(certificado, 1);
                Microsoft.Office.Interop.Word.Table tbl_FechasEntrega5G = doc.Tables[9];

                for (int i = 0; i < table5G.Rows.Count; i++)
                {
                    tbl_FechasEntrega5G.Cell(3 + i, 1).Range.Text = table5G.Rows[i]["packing"].ToString();
                    tbl_FechasEntrega5G.Cell(3 + i, 2).Range.Text = table5G.Rows[i]["paquete"].ToString();
                    tbl_FechasEntrega5G.Cell(3 + i, 3).Range.Text = table5G.Rows[i]["capmin"].ToString() + " µm";
                    tbl_FechasEntrega5G.Cell(3 + i, 4).Range.Text = table5G.Rows[i]["capmed"].ToString() + " µm";
                    tbl_FechasEntrega5G.Cell(3 + i, 5).Range.Text = table5G.Rows[i]["capmax"].ToString() + " µm";

                    tbl_FechasEntrega5G.Rows.Add();
                }

                DataTable table5P1 = null;
                table5P1 = ipDB.datos5EspesorPaquetes(certificado, 1);
                Microsoft.Office.Interop.Word.Table tbl_FechasEntrega5P1 = doc.Tables[10];

                for (int i = 0; i < table5P1.Rows.Count; i++)
                {
                    tbl_FechasEntrega5P1.Cell(3 + i, 1).Range.Text = table5P1.Rows[i]["packing"].ToString();
                    tbl_FechasEntrega5P1.Cell(3 + i, 2).Range.Text = table5P1.Rows[i]["paquete"].ToString();
                    tbl_FechasEntrega5P1.Cell(3 + i, 3).Range.Text = table5P1.Rows[i]["capmin"].ToString() + " µm";
                    tbl_FechasEntrega5P1.Cell(3 + i, 4).Range.Text = table5P1.Rows[i]["capmed"].ToString() + " µm";
                    tbl_FechasEntrega5P1.Cell(3 + i, 5).Range.Text = table5P1.Rows[i]["capmax"].ToString() + " µm";

                    tbl_FechasEntrega5P1.Rows.Add();
                }

                DataTable table5P2 = null;
                table5P2 = ipDB.datos5EspesorPaquetes(certificado, 2);
                Microsoft.Office.Interop.Word.Table tbl_FechasEntrega5P2 = doc.Tables[11];

                for (int i = 0; i < table5P2.Rows.Count; i++)
                {
                    tbl_FechasEntrega5P2.Cell(3 + i, 1).Range.Text = table5P2.Rows[i]["packing"].ToString();
                    tbl_FechasEntrega5P2.Cell(3 + i, 2).Range.Text = table5P2.Rows[i]["paquete"].ToString();
                    tbl_FechasEntrega5P2.Cell(3 + i, 3).Range.Text = table5P2.Rows[i]["capmin"].ToString() + " µm";
                    tbl_FechasEntrega5P2.Cell(3 + i, 4).Range.Text = table5P2.Rows[i]["capmed"].ToString() + " µm";
                    tbl_FechasEntrega5P2.Cell(3 + i, 5).Range.Text = table5P2.Rows[i]["capmax"].ToString() + " µm";

                    tbl_FechasEntrega5P2.Rows.Add();
                }


                #endregion

                #region *) Añadir la imagen de firma
                Object oBookmarkF = "Firma";
                Object oRngoBookMarkF = doc.Bookmarks[oBookmarkF].Range.Start;

                Microsoft.Office.Interop.Word.Range rngBKMarkFirmaSelectionF = doc.Range(ref oRngoBookMarkF, ref oRngoBookMarkF);

                //SELECTING THE TEXT
                String user = Environment.UserName;
                String pathFirmaF = "\\\\srvsql01\\Irati2k2\\DOCUMENTOS\\Firmas\\" + user + ".png";

                if (System.IO.File.Exists(pathFirmaF) == true)
                {
                    rngBKMarkFirmaSelectionF.Select();
                    rngBKMarkFirmaSelectionF.InlineShapes.AddPicture(pathFirmaF);
                }
                else
                {
                    MessageBox.Show("No está disponible su firma", "Firma", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion

                #region 9º) Guardar el documento de manera predeterminada Y AÑADO LAS IMAGENES

                
                //AÑADO LAS IMAGENES DEL PUNTO 3 TestADV
                Object oBookmark = "Test1";
                Object oRngoBookMark = doc.Bookmarks[oBookmark].Range.Start;

                Microsoft.Office.Interop.Word.Range rngBKMarkFirmaSelection = doc.Range(ref oRngoBookMark, ref oRngoBookMark);

                //SELECTING THE TEXT
                String pathFirma = @"R:\PINTURA\Control inspecciones internas\" + nuevoCertificado + "-test1.jpg";

                if (System.IO.File.Exists(pathFirma) == true)
                {
                    rngBKMarkFirmaSelection.Select();
                    rngBKMarkFirmaSelection.InlineShapes.AddPicture(pathFirma);
                }
                else
                {
                    MessageBox.Show("No hay imagen almacenada para el Test1", "TEST1", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                Object oBookmark2 = "Test2";
                Object oRngoBookMark2 = doc.Bookmarks[oBookmark2].Range.Start;

                Microsoft.Office.Interop.Word.Range rngBKMarkFirmaSelection2 = doc.Range(ref oRngoBookMark2, ref oRngoBookMark2);

                //SELECTING THE TEXT
                String pathFirma2 = @"R:\PINTURA\Control inspecciones internas\" + nuevoCertificado + "-test2.jpg";

                if (System.IO.File.Exists(pathFirma2) == true)
                {
                    rngBKMarkFirmaSelection2.Select();
                    rngBKMarkFirmaSelection2.InlineShapes.AddPicture(pathFirma2);
                }
                else
                {
                    MessageBox.Show("No hay imagen almacenada para el Test2", "TEST2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                Object oBookmark3 = "Test3";
                Object oRngoBookMark3 = doc.Bookmarks[oBookmark3].Range.Start;

                Microsoft.Office.Interop.Word.Range rngBKMarkFirmaSelection3 = doc.Range(ref oRngoBookMark3, ref oRngoBookMark3);

                //SELECTING THE TEXT
                String pathFirma3 = @"R:\PINTURA\Control inspecciones internas\" + nuevoCertificado + "-test3.jpg";

                if (System.IO.File.Exists(pathFirma3) == true)
                {
                    rngBKMarkFirmaSelection3.Select();
                    rngBKMarkFirmaSelection3.InlineShapes.AddPicture(pathFirma3);
                }
                else
                {
                    MessageBox.Show("No hay imagen almacenada para el Test3", "TEST3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

               
                Object oBookmark4 = "TestADV";
                Object oRngoBookMark4 = doc.Bookmarks[oBookmark4].Range.Start;

                Microsoft.Office.Interop.Word.Range rngBKMarkFirmaSelection4 = doc.Range(ref oRngoBookMark4, ref oRngoBookMark4);

                //SELECTING THE TEXT
                String pathFirma4 = @"R:\PINTURA\Control inspecciones internas\" + nuevoCertificado + "-testA.jpg";

                if (System.IO.File.Exists(pathFirma4) == true)
                {
                    rngBKMarkFirmaSelection4.Select();
                    rngBKMarkFirmaSelection4.InlineShapes.AddPicture(pathFirma4);
                }
                else
                {
                    MessageBox.Show("No hay imagen almacenada para el Test  de adherencia", "Test  de adherencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                String ofertaPath1 = "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\";
                if (ofertaPath1 != "")
                {
                    
                    ofertaPath1 += "\\" +nuevoCertificado+ ".docx";

                    if (System.IO.File.Exists(ofertaPath1) == false)
                        doc.SaveAs(ofertaPath1);
                }

                word.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintPreview;
                word.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintPreview;
                #endregion

                #region 8 º) Generamos el documento del punto 8.
                generarDocumento8_Click(sender, e, codEmp,idioma);

                #endregion
                Cursor.Current = Cursors.Default;
            }


        }

        private void generarDocumento8_Click(object sender, EventArgs e , string codEmp,string idioma)
        {
            Cursor.Current = Cursors.WaitCursor;

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();

            // AQUI PRIMERO OBTENGO LOS DATOS DE LOS DISTINTOS APARTADOS.
            string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "/", "<", ">", ":" };
            string nuevoCertificado = certificado;
            foreach (var c in charsToRemove)
            {
                nuevoCertificado = nuevoCertificado.Replace(c, string.Empty);
            }

            String filename = "";

            if (codEmp == "3")
            {
                //filename = "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\Plantilla\\registroInspCargaPlantillaIMEDEXSA";//.docx";
                filename = "\\\\nas01\\Herramientas_Comunes$\\plantilla\\registroInspCargaPlantillaIMEDEXSA";
            }

            if (codEmp == "60")
            {
               // filename = "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\Plantilla\\registroInspCargaPlantillaMADE";//.docx";
                filename = "\\\\nas01\\Herramientas_Comunes$\\plantilla\\registroInspCargaPlantillaMADE";
            }

            switch (idioma)
            {
                case "FR":
                    filename += "_frances";
                    break;

                default:
                    break;
            }

            filename += ".docx";

            if (System.IO.File.Exists(filename) == false)
            {
                MessageBox.Show("ERROR CON LA PLANTILLA DEL DOCUMENTO. CONSULTE CON EL DEPARTAMENTO DE INFORMÁTICA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            doc = word.Documents.Add(filename);
            word.Visible = true;

            // Loop through all sections
            foreach (Microsoft.Office.Interop.Word.Section section in doc.Sections)
            {
               
                foreach (Microsoft.Office.Interop.Word.HeaderFooter header in section.Headers)
                {
                    //Loop through all fields
                    foreach (Microsoft.Office.Interop.Word.Field campo in header.Range.Fields)
                    {
                        Microsoft.Office.Interop.Word.Range rngFieldCode = campo.Code;
                        String fieldText = rngFieldCode.Text;

                        if (fieldText.StartsWith(" MERGEFIELD"))
                        {
                            Int32 endMerge = fieldText.IndexOf("\\");
                            Int32 fieldNameLength = fieldText.Length - endMerge;
                            String fieldName = fieldText.Substring(11, endMerge - 11);

                            // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE
                            fieldName = fieldName.Trim();

                            //aqui seria para la cabeza, que en este caso no ahi.



                        }

                    }




                    foreach (Microsoft.Office.Interop.Word.HeaderFooter pie in section.Footers)
                    {
                        //Loop through all fields
                        foreach (Microsoft.Office.Interop.Word.Field campo in pie.Range.Fields)
                        {
                            Microsoft.Office.Interop.Word.Range rngFieldCode = campo.Code;
                            String fieldText = rngFieldCode.Text;

                            if (fieldText.StartsWith(" MERGEFIELD"))
                            {
                                Int32 endMerge = fieldText.IndexOf("\\");
                                Int32 fieldNameLength = fieldText.Length - endMerge;
                                String fieldName = fieldText.Substring(11, endMerge - 11);

                                // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE
                                fieldName = fieldName.Trim();



                            }
                        }
                    }



                    foreach (Microsoft.Office.Interop.Word.Field campo in doc.Fields)
                    {
                        Microsoft.Office.Interop.Word.Range rngFieldCode = campo.Code;
                        String fieldText = rngFieldCode.Text;

                        if (fieldText.StartsWith(" MERGEFIELD"))
                        {
                            Int32 endMerge = fieldText.IndexOf("\\");
                            Int32 fieldNameLength = fieldText.Length - endMerge;
                            String fieldName = fieldText.Substring(11, endMerge - 11);

                            // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE
                            fieldName = fieldName.Trim();

                            if (fieldName == "cliente")
                            {
                                campo.Select();
                                if (textBoxCliente.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxCliente.Text);
                            }

                            if (fieldName == "pedido")
                            {
                                campo.Select();
                                if (textBoxPedido.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxPedido.Text);
                            }
                            if (fieldName == "camion")
                            {
                                campo.Select();
                                if (textBoxContenedor.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(textBoxContenedor.Text);
                            }
                            if (fieldName == "inspector")
                            {
                                campo.Select();
                                string nombre = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;
                                if (textBoxInspector.Text == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(nombre);
                            }

                            #region 8º Tabla octava,  Inspección final del camion
                            DataTable table8 = null;
                            table8 = ipDB.datosa8InspCarga(certificado);

                            string fecha8 = "";
                            string contaguaR = "", contaguaN = "", contelimpR = "", contelimpN = "";
                            string embjR = "", estbR = "", dañoR = "", identR = "", embjN = "", estbN = "", dañoN = "", identN = "";
                            string tmfitoR = "", tmpaqR = "", tmfitoN = "", tmpaqN = "";
                            string snmanchR = "", sndesnR = "", snpinchR = "", sncenizR = "", snmanchN = "", sndesnN = "", snpinchN = "", sncenizN = "";
                            string espT1 = "", espR1 = "", espN1 = "", espT2 = "", espR2 = "", espN2 = "", espT3 = "", espR3 = "", espN3 = "", espT4 = "", espR4 = "", espN4 = "";
                            string contenedorCorrecto = "", PNC = "";
                            Boolean finalizada = false;

                            foreach (DataRow row in table8.Rows)
                            {
                                if (row["fecha"] != DBNull.Value)
                                {
                                    fecha8 = Convert.ToString(row["fecha"]);
                                }

                                contaguaR = Convert.ToString(row["contaguaResult"]);
                                contaguaN = Convert.ToString(row["conteaguaNote"]);
                                contelimpR = Convert.ToString(row["contelimpiezaResult"]);
                                contelimpN = Convert.ToString(row["contelimpiezaNote"]);

                                embjR = Convert.ToString(row["embalajeResult"]);
                                embjN = Convert.ToString(row["embalajeNote"]);
                                estbR = Convert.ToString(row["estabilidadResult"]);
                                estbN = Convert.ToString(row["estabilidadNote"]);
                                dañoR = Convert.ToString(row["dañadasResult"]);
                                dañoN = Convert.ToString(row["dañadasNote"]);
                                identR = Convert.ToString(row["identificadoResult"]);
                                identN = Convert.ToString(row["identificadoNote"]);

                                tmfitoR = Convert.ToString(row["tmfitosanitariosResult"]);
                                tmfitoN = Convert.ToString(row["tmfitosanitariosNote"]);
                                tmpaqR = Convert.ToString(row["tmpaqueteResult"]);
                                tmpaqN = Convert.ToString(row["tmpaqueteNote"]);

                                snmanchR = Convert.ToString(row["sinmanchasResult"]);
                                snmanchN = Convert.ToString(row["sinmanchasNote"]);
                                sndesnR = Convert.ToString(row["sinzonasdesnudasResult"]);
                                sndesnN = Convert.ToString(row["sinzonasdesnudasNote"]);
                                snpinchR = Convert.ToString(row["sinpinchosResult"]);
                                snpinchN = Convert.ToString(row["sinpinchosNote"]);
                                sncenizR = Convert.ToString(row["sincenizasResult"]);
                                sncenizN = Convert.ToString(row["sincenizasNote"]);

                                espT1 = Convert.ToString(row["especifiTitulo1"]);
                                espR1 = Convert.ToString(row["especifiResult1"]);
                                espN1 = Convert.ToString(row["especifiNote1"]);
                                espT2 = Convert.ToString(row["especifiTitulo2"]);
                                espR2 = Convert.ToString(row["especifiResult2"]);
                                espN2 = Convert.ToString(row["especifiNote2"]);
                                espT3 = Convert.ToString(row["especifiTitulo3"]);
                                espR3 = Convert.ToString(row["especifiResult3"]);
                                espN3 = Convert.ToString(row["especifiNote3"]);
                                espT4 = Convert.ToString(row["especifiTitulo4"]);
                                espR4 = Convert.ToString(row["especifiResult4"]);
                                espN4 = Convert.ToString(row["especifiNote4"]);

                                contenedorCorrecto = Convert.ToString(row["contenedorCorrecto"]);
                                PNC = Convert.ToString(row["pnc"]);

                            }

                            if (fieldName == "fecha8")
                            {
                                campo.Select();
                                if (fecha8 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(fecha8.Substring(0, 10));
                            }

                            if (fieldName == "contaguaR")
                            {
                                campo.Select();
                                if(contaguaR != "")
                                    if (Convert.ToBoolean(contaguaR) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "contaguaN")
                            {
                                campo.Select();
                                if (contaguaN != "")
                                {
                                    if (contaguaN == "") word.Selection.TypeText(" ");
                                }else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "contlimpR")
                            {
                                campo.Select();
                                if (contelimpR == "")
                                {
                                }
                                else
                                {
                                    if (Convert.ToBoolean(contelimpR) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "contlimpN")
                            {
                                campo.Select();
                                if (contelimpN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(contelimpN);
                            }

                            //

                            if (fieldName == "embjR")
                            {
                                campo.Select();
                                if (String.IsNullOrEmpty(embjR))
                                {
                                    word.Selection.TypeText(" ");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(embjR) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "embjN")
                            {
                                campo.Select();
                                if (String.IsNullOrEmpty(embjN))
                                {
                                    word.Selection.TypeText(" ");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(embjN) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "estbR")
                            {
                                campo.Select();
                                if (String.IsNullOrEmpty(estbR))
                                {
                                    word.Selection.TypeText(" ");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(estbR) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "estbN")
                            {
                                campo.Select();
                                if (estbN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(estbN);
                            }

                            if (fieldName == "dañoR")
                            {
                                campo.Select();
                                if (String.IsNullOrEmpty(dañoR))
                                {
                                    word.Selection.TypeText(" ");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(dañoR) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "dañoN")
                            {
                                campo.Select();
                                if (dañoN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(dañoN);
                            }

                            if (fieldName == "identR")
                            {
                                campo.Select();
                                if (String.IsNullOrEmpty(identR))
                                {
                                    word.Selection.TypeText(" ");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(identR) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "identN")
                            {
                                campo.Select();
                                if (identN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(identN);
                            }

                            //

                            if (fieldName == "tmfitoR")
                            {
                                campo.Select();
                                if (String.IsNullOrEmpty(identR))
                                {
                                    word.Selection.TypeText(" ");
                                }
                                else
                                {

                                    if (Convert.ToBoolean(tmfitoR) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "tmfitoN")
                            {
                                campo.Select();
                                if (tmfitoN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tmfitoN);
                            }

                            if (fieldName == "tmpaqR")
                            {
                                campo.Select();
                                if (String.IsNullOrEmpty(tmpaqR))
                                {
                                    word.Selection.TypeText(" ");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(tmpaqR) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "tmpaqN")
                            {
                                campo.Select();
                                if (tmpaqN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(tmpaqN);
                            }

                            //

                            if (fieldName == "snmanchR")
                            {
                                campo.Select();
                                

                                if (String.IsNullOrEmpty(snmanchR))
                                {
                                    word.Selection.TypeText(" ");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(snmanchR) == true) word.Selection.TypeText("✓");
                                    else word.Selection.TypeText(" ");
                                }
                            }

                            if (fieldName == "snmanchN")
                            {
                                campo.Select();
                                if (snmanchN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(snmanchN);
                            }

                            if (fieldName == "sndesnR")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(sndesnR) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "sndesnN")
                            {
                                campo.Select();
                                if (sndesnN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(sndesnN);
                            }

                            if (fieldName == "snpinchR")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(snpinchR) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "snpinchN")
                            {
                                campo.Select();
                                if (snpinchN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(snpinchN);
                            }

                            if (fieldName == "sncenizR")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(sncenizR) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "sncenizN")
                            {
                                campo.Select();
                                if (sncenizN == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(sncenizN);

                            }

                            //
                            if (fieldName == "esp1T")
                            {
                                campo.Select();
                                if (espT1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(espT1);

                            }

                            if (fieldName == "esp1R")
                            {
                                campo.Select();
                                if (espR1 != "")
                                {
                                    if (Convert.ToBoolean(espR1) == true) word.Selection.TypeText("✓");
                                }else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "esp1N")
                            {
                                campo.Select();
                                if (espN1 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(espN1);
                            }

                            if (fieldName == "esp2T")
                            {
                                campo.Select();
                                if (espT2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(espT2);

                            }

                            if (fieldName == "esp2R")
                            {
                                campo.Select();
                                if (espR2 != "")
                                {
                                    if (Convert.ToBoolean(espR2) == true) word.Selection.TypeText("✓");
                                }else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "esp2N")
                            {
                                campo.Select();
                                if (espN2 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(espN2);
                            }

                            if (fieldName == "esp3T")
                            {
                                campo.Select();
                                if (espT3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(espT3);

                            }

                            if (fieldName == "esp3R")
                            {
                                campo.Select();
                                if (espR3 != "")
                                {
                                    if (Convert.ToBoolean(espR3) == true) word.Selection.TypeText("✓");
                                }
                                else word.Selection.TypeText(" ");
                                    
                            }

                            if (fieldName == "esp3N")
                            {
                                campo.Select();
                                if (espN3 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(espN3);
                            }

                            if (fieldName == "esp4T")
                            {
                                campo.Select();
                                if (espT4 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(espT4);

                            }

                            if (fieldName == "esp4R")
                            {
                                campo.Select();
                                if (espR4 != "")
                                {
                                    if (Convert.ToBoolean(espR4) == true) word.Selection.TypeText("✓");
                                } else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "esp4N")
                            {
                                campo.Select();
                                if (espN4 == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(espN4);
                            }

                            if (fieldName == "PNC")
                            {
                                campo.Select();
                                if (PNC == "") word.Selection.TypeText(" ");
                                else word.Selection.TypeText(PNC);

                            }

                            if (fieldName == "CCSI")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(contenedorCorrecto) == true) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }

                            if (fieldName == "CCNO")
                            {
                                campo.Select();
                                if (Convert.ToBoolean(contenedorCorrecto) == false) word.Selection.TypeText("✓");
                                else word.Selection.TypeText(" ");
                            }
                            #endregion

                           

                        }
                    }


                }

                        #region *) Añadir la imagen de firma
                        Object oBookmarkF = "Firma";
                        Object oRngoBookMarkF = doc.Bookmarks[oBookmarkF].Range.Start;

                        Microsoft.Office.Interop.Word.Range rngBKMarkFirmaSelectionF = doc.Range(ref oRngoBookMarkF, ref oRngoBookMarkF);

                        //SELECTING THE TEXT
                        String user = Environment.UserName;
                        String pathFirmaF = "\\\\srvsql01\\Irati2k2\\DOCUMENTOS\\Firmas\\" + user + ".png";

                        if (System.IO.File.Exists(pathFirmaF) == true)
                        {
                            rngBKMarkFirmaSelectionF.Select();
                            rngBKMarkFirmaSelectionF.InlineShapes.AddPicture(pathFirmaF);
                        }
                        else
                        {
                            MessageBox.Show("No está disponible su firma", "Firma", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        #endregion

                        #region 9º) Guardar el documento de manera predeterminada si está habilitada la carpeta, y no se ha guardado ya la oferta-version.
                        String ofertaPath = "\\\\172.20.22.190\\fs06$\\Documentos Calidad\\PINTURA\\Control inspecciones internas\\";
                        if (ofertaPath != "")
                        {
                            ofertaPath += "\\InspeccionCarga-" + nuevoCertificado + ".docx";

                            if (System.IO.File.Exists(ofertaPath) == false)
                                doc.SaveAs(ofertaPath);
                        }

                        word.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintPreview;
                        word.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintPreview;
                        #endregion
                 

            } // fin metodo




        }
   
    
    
    
    
    }  
 
}
