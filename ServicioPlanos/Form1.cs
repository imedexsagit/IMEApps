using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ServicioPlanos
{

    public partial class Form1 : Form
    {


         private Thread t;

        private SqlConnection conexion;

        private String codemp;
        private String cadenaConexion;
        private String pathMon;
        private String pathError;
        private String pathReemplazo;
        private String pathcopiaSeguridad;
        private String buzon;
        private String tipoArchivo;

        public Form1()
        {
            InitializeComponent();

            this.codemp = "3";

            this.cadenaConexion = "Data Source=srvsql02;Initial Catalog=gg;uid=gg;pwd=ostia;";
            /*this.pathMon = @"\\SrvFiles01\Piezas.oficina\__Buzon__";
            this.pathError = @"\\SrvFiles01\Piezas.oficina\__Buzon__\errores\";
            this.pathReemplazo = @"\\SrvFiles01\\Piezas.oficina\__Buzon__\Originales_Reemplazados\";
            this.pathcopiaSeguridad = @"\\SrvFiles01\\Piezas.oficina\__Buzon__\copiaSeguridad\";
            this.buzon = @"O:\\__Buzon__\";*/

            //Nueva ruta
            this.pathMon = @"\\nas01\__Buzon__";
            this.pathError = @"\\nas01\__Buzon__\errores\";
            this.pathReemplazo = @"\\nas01\__Buzon__\Originales_Reemplazados\";
            this.pathcopiaSeguridad = @"\\nas01\__Buzon__\copiaSeguridad\";
            this.buzon = @"\\nas01\__Buzon__";
            
            this.tipoArchivo = "*.txt";

            conexion = new SqlConnection(this.cadenaConexion);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //String path = @"C:\borrar\Piezas.Oficina\000_Buzon\luisd.txt";   //Ruta completa del fichero creado

            String marca = "";
            String familia = "";

            String fichero_CAM = "";
            String fichero_NC = "";

            String extension_NC = "";
            String extension_CAM = "";

            String fichero_CAM_new = "";
            String fichero_NC_new = "";

            String directorio_FamiliaPDF = "";
            String directorio_FamiliaNC = "";
            String directorio_FamiliaCAM = "";

            Boolean conjunto = false;

             DirectoryInfo dirseguridad = new DirectoryInfo(pathcopiaSeguridad);
            FileInfo[] FicherosSeguridad = dirseguridad.GetFiles("*");
            foreach (FileInfo Fich in FicherosSeguridad)             //recorrer cada uno de los ficheros. Si existe el pdf con su CAM/NC => mover a la carpeta correspondiente
            {
                // Elimino el fichero
                File.Delete(Fich.FullName);
            }
            

            try
            {
                   DirectoryInfo dir = new DirectoryInfo(buzon);
                    FileInfo[] Ficheros = dir.GetFiles("*.pdf");
                bool correcto = true; 

                    foreach (FileInfo Fich in Ficheros)             //recorrer cada uno de los ficheros. Si existe el pdf solo o con su CAM/NC => mover a la carpeta correspondiente
                    {
                        marca = Fich.Name.Replace(Fich.Extension, "");

                        if (ExisteMarca(marca) == true) //se comprueba si existe la marca en IRATI
                        {
                            familia = DevolverFamiliaMarca(marca);
                            directorio_FamiliaPDF = DevolverPathFamilia(familia, "PDF");
                            directorio_FamiliaNC = DevolverPathFamilia(familia, "NC");
                            directorio_FamiliaCAM = DevolverPathFamilia(familia, "CAM");
                            conjunto = esConjunto(marca);


                            //Si alguno de los directorios de la familia no existe se pasa al siguiente fichero
                            if ((directorio_FamiliaPDF == "") || (directorio_FamiliaNC == "") || (directorio_FamiliaCAM == ""))
                            {
                                //bodyMail += " El fichero: " + Fich.Name + " de la familia: " + familia + " no tiene generados sus directorios </BR>";
                                break;
                            }


                            #region Caso especial de la pieza principal de los CJS. El nombre acaba en punto(.) pero el NC/CAM no. Ejemplo: 9E450018..pdf -- 9E450018.nc
                            if (marca.EndsWith(".") == true)
                            {
                                fichero_NC = Fich.FullName.Replace(Fich.Extension, "") + "NC";
                                fichero_NC_new = directorio_FamiliaNC + marca + "NC";
                                extension_NC = marca + "NC";
                                extension_CAM = marca + "CAM";

                                fichero_CAM = Fich.FullName.Replace(Fich.Extension, "") + "CAM";
                                fichero_CAM_new = directorio_FamiliaNC + marca + "CAM";


                            }
                            else
                            {
                                fichero_NC = Fich.FullName.Replace(Fich.Extension, "") + ".NC";
                                fichero_NC_new = directorio_FamiliaNC + marca + ".NC";

                                fichero_CAM = Fich.FullName.Replace(Fich.Extension, "") + ".CAM";
                                fichero_CAM_new = directorio_FamiliaNC + marca + ".CAM";

                                extension_NC = marca + ".NC";
                                extension_CAM = marca + ".CAM";
                            }
                            #endregion

                            //COMPRUEBO SI YA TIENE NC,CAM O PDF, Y SI ES ASI LO COPIO EN VERSIONES ANTERIORES.
                            DateTime localDate = DateTime.Now;
                            string date_str = localDate.ToString("dd_MM_yyyy_HH_mm_ss_");
                            string ficheroAux = this.buzon +"\\"+ marca + ".NC";
                            if (marca.EndsWith(".") == true)
                            {
                                ficheroAux = this.buzon + "\\" + marca + "NC";
                            }
                            if (File.Exists(fichero_NC_new) == true && !(conjunto)) //Para los NC
                            {
                                if (!conjunto && !File.Exists(ficheroAux))
                                {
                                    MessageBox.Show("ERROR: No se encuentra el NC de: "+marca, " BUZÓN");
                                    correcto = false; 
                                    continue;
                                }
                                else
                                {
                                    File.Copy(Path.Combine(directorio_FamiliaNC, extension_NC), Path.Combine(pathReemplazo, marca + date_str + ".NC"));
                                }
                            }

                            if (File.Exists(fichero_CAM_new) == true && !(conjunto)) //Para los CAM
                            {
                                File.Copy(Path.Combine(directorio_FamiliaNC, extension_CAM), Path.Combine(pathReemplazo, marca + date_str + ".CAM"));
                                
                            }

                            if (File.Exists(directorio_FamiliaPDF + Fich.Name) == true) //Para los PDF
                            {
                                if (!conjunto && !File.Exists(ficheroAux))
                                {
                                    MessageBox.Show("ERROR: No se encuentra el NC de: " + marca, " BUZÓN");
                                    correcto = false;
                                    continue;
                                }
                                else
                                {
                                    File.Copy(Path.Combine(directorio_FamiliaPDF, Fich.Name), Path.Combine(pathReemplazo, marca + date_str + ".pdf"));
                                    mandarEmail(Environment.UserName, marca);
                                }
                                
                            }

                            //REALIZO LA COPIA DE SEGURIDAD 

                            if (File.Exists(Path.Combine(buzon, extension_NC)) == true && !(conjunto)) //Para los NC
                            {
                                if (!conjunto && !File.Exists(ficheroAux))
                                {
                                    MessageBox.Show("ERROR: No se encuentra el NC de: " + marca, " BUZÓN");
                                    correcto = false;
                                    continue;
                                }
                                else
                                {
                                    File.Copy(Path.Combine(buzon, extension_NC), Path.Combine(pathcopiaSeguridad, extension_NC));
                                }
                            }

                            if (File.Exists(Path.Combine(buzon, extension_CAM)) == true && !(conjunto)) //Para los CAM
                            {
                                File.Copy(Path.Combine(buzon, extension_CAM), Path.Combine(pathcopiaSeguridad, extension_CAM));
                            }

                            if (File.Exists(Path.Combine(buzon, Fich.Name)) == true) //Para los PDF
                            {
                                if (!conjunto && !File.Exists(ficheroAux))
                                {
                                    MessageBox.Show("ERROR: No se encuentra el NC de: " + marca, " BUZÓN");
                                    correcto = false;
                                    continue;
                                }
                                else
                                {
                                    File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                }
                            }

                            //LO COPIO AHORA EN SU LUGAR DEFINITIVO.

                            if (File.Exists(Path.Combine(buzon, extension_NC)) == true && !(conjunto)) //Para los NC
                            {

                                if (File.Exists(fichero_NC_new))
                                {
                                    File.Delete(fichero_NC_new);
                                    File.Copy(Path.Combine(buzon, extension_NC), fichero_NC_new);
                                    File.Delete(Path.Combine(buzon, extension_NC));
                                }
                                else
                                {
                                    File.Copy(Path.Combine(buzon, extension_NC), fichero_NC_new);
                                    File.Delete(Path.Combine(buzon, extension_NC));
                                }
                                
                                //File.Move(Path.Combine(buzon, extension_NC), fichero_NC_new);
                                //actualizarPathMarca(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name);
                                registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "InsNC", "", "", Environment.UserName);
                            }

                            if (File.Exists(Path.Combine(buzon, extension_CAM)) == true && !(conjunto)) //Para los CAM
                            {
                                if (File.Exists(fichero_CAM_new))
                                {
                                    File.Delete(fichero_CAM_new);
                                    File.Copy(Path.Combine(buzon, extension_CAM), fichero_CAM_new);
                                    File.Delete(Path.Combine(buzon, extension_CAM));
                                }
                                else
                                {
                                    File.Copy(Path.Combine(buzon, extension_CAM), fichero_CAM_new);
                                    File.Delete(Path.Combine(buzon, extension_CAM));
                                }
                                //File.Move(Path.Combine(buzon, extension_CAM), fichero_CAM_new);
                                //actualizarPathMarca(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name);
                                registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "InsCAM", "", "", Environment.UserName);
                            }

                            if (File.Exists(Path.Combine(buzon, Fich.Name)) == true) //Para los PDF
                            {
                                //File.Move(Path.Combine(buzon, Fich.Name), directorio_FamiliaPDF + Fich.Name);
                                if (File.Exists(directorio_FamiliaPDF + Fich.Name))
                                {
                                    File.Delete(directorio_FamiliaPDF + Fich.Name);
                                    File.Copy(Path.Combine(buzon, Fich.Name), directorio_FamiliaPDF + Fich.Name);
                                    File.Delete(Path.Combine(buzon, Fich.Name));
                                }
                                else
                                {
                                    File.Copy(Path.Combine(buzon, Fich.Name), directorio_FamiliaPDF + Fich.Name);
                                    File.Delete(Path.Combine(buzon, Fich.Name));
                                }
                                actualizarPathMarca(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name);
                                registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "InsPDF", "", "", Environment.UserName);
                            }
                        }
                    }

                if (correcto)
                {
                    MessageBox.Show("Buzón procesado correctamente", " BUZÓN");
                }


                /*
                //Thread.Sleep(1000);  //JRegino 23/10/2012 xa que de tiempo a que se copie el fichero

             
                    //No se puede obtener quien es el propietario en el servicio!!!
                    //String user = System.IO.File.GetAccessControl(path).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString().ToUpper();
                    //String email = user.Replace(@"IMEDEXSA\", "") + "@imedexsa.es";
                    String user = Environment.UserName;
                    String email = user + "@imedexsa.es";
                    //String bodyMail = "";

                //Eliminamos la copia de seguridad
                DirectoryInfo dirseguridad = new DirectoryInfo(pathcopiaSeguridad);

                    FileInfo[] FicherosSeguridad = dirseguridad.GetFiles("*.pdf");
                    foreach (FileInfo Fich in FicherosSeguridad)             //recorrer cada uno de los ficheros. Si existe el pdf con su CAM/NC => mover a la carpeta correspondiente
                    {
                        // Elimino el fichero
                        ///File.Delete(Fich.FullName);
                    }

                    Boolean conjunto = false;

                    //DirectoryInfo dir = new DirectoryInfo(this.pathMon); O:\__Buzon__
                    DirectoryInfo dir = new DirectoryInfo(buzon);

                    FileInfo[] Ficheros = dir.GetFiles("*.pdf");
                    foreach (FileInfo Fich in Ficheros)             //recorrer cada uno de los ficheros. Si existe el pdf con su CAM/NC => mover a la carpeta correspondiente
                    {
                        marca = Fich.Name.Replace(Fich.Extension, "");

                        if (ExisteMarca(marca) == true) //se comprueba si existe la marca en IRATI
                        {
                            familia = DevolverFamiliaMarca(marca);
                            directorio_FamiliaPDF = DevolverPathFamilia(familia, "PDF");


                            //para la tornilleria
                            if (familia == "TO")
                            {
                                if (File.Exists(directorio_FamiliaPDF + Fich.Name) == true)
                                {
                                    File.Delete(this.pathReemplazo + Fich.Name);

                                    // Carlos Sanchez 
                                    if (File.Exists(directorio_FamiliaPDF + Fich.Name) == false)
                                    {
                                        if (File.Exists(buzon + Fich.Name) == true)
                                        {
                                            File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                            File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                        }
                                    }
                                    else
                                    {
                                        File.Delete(directorio_FamiliaPDF + Fich.Name);
                                        if (File.Exists(buzon + Fich.Name) == true)
                                        {
                                            File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                            File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                        }
                                    }
                                    File.Delete(Fich.FullName);


                                    actualizarPathMarca(marca, "");
                                    registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Deleted", "", "", user);
                                }

                                File.Move(Fich.FullName, directorio_FamiliaPDF + Fich.Name);
                                actualizarPathMarca(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name);
                                registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Inserted", "", "", user);
                            }

                            //para el resto de las familias
                            else
                            {
                                directorio_FamiliaNC = DevolverPathFamilia(familia, "NC");
                                directorio_FamiliaCAM = DevolverPathFamilia(familia, "CAM");

                                conjunto = esConjunto(marca);


                                //Si alguno de los directorios de la familia no existe se pasa al siguiente fichero
                                if ((directorio_FamiliaPDF == "") || (directorio_FamiliaNC == "") || (directorio_FamiliaCAM == ""))
                                {
                                    //bodyMail += " El fichero: " + Fich.Name + " de la familia: " + familia + " no tiene generados sus directorios </BR>";
                                    break;
                                }


                                #region Caso especial de la pieza principal de los CJS. El nombre acaba en punto(.) pero el NC/CAM no. Ejemplo: 9E450018..pdf -- 9E450018.nc
                                if (marca.EndsWith(".") == true)
                                {
                                    fichero_NC = Fich.FullName.Replace(Fich.Extension, "") + "NC";
                                    fichero_NC_new = directorio_FamiliaNC + marca + "NC";

                                    fichero_CAM = Fich.FullName.Replace(Fich.Extension, "") + "CAM";
                                    fichero_CAM_new = directorio_FamiliaNC + marca + "CAM";
                                }
                                else
                                {
                                    fichero_NC = Fich.FullName.Replace(Fich.Extension, "") + ".NC";
                                    fichero_NC_new = directorio_FamiliaNC + marca + ".NC";

                                    fichero_CAM = Fich.FullName.Replace(Fich.Extension, "") + ".CAM";
                                    fichero_CAM_new = directorio_FamiliaNC + marca + ".CAM";
                                }
                                #endregion



                                #region Eliminar las posibles versiones anteriores que existan de los PDF´s NC/CAM
                                if (conjunto == true)
                                {
                                    if (File.Exists(directorio_FamiliaPDF + Fich.Name) == true)
                                    {
                                        //File.Delete(directorio_FamiliaPDF + Fich.Name);
                                        //en vez de eliminar el original se lleva a la carpeta de reemplazados. 
                                        //1º) Eliminar el que pueda existir alli  
                                        File.Delete(this.pathReemplazo + Fich.Name);
                                   
                                        //2º) Mover el antiguo alli
                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathReemplazo, Fich.Name));

                                        actualizarPathMarca(marca, "");
                                        registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Deleted", "", "", user);
                                    }
                                }
                                else
                                {
                                    if ((File.Exists(directorio_FamiliaPDF + Fich.Name)) && (File.Exists(fichero_NC_new) == false) && (File.Exists(fichero_CAM_new) == false))
                                    {
                                        //1º) Eliminar el que pueda existir alli  
                                        File.Delete(this.pathReemplazo + Fich.Name);

                                        //2º) Mover el antiguo alli
                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathReemplazo, Fich.Name));
                                        actualizarPathMarca(marca, "");
                                        registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Deleted", "", "", user);
                                    }
                                    else
                                    {
                                        if (File.Exists(fichero_NC_new) == true)
                                        {
                                            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                                            File.Delete(fichero_NC_new);

                                            if (File.Exists(directorio_FamiliaPDF + Fich.Name))
                                            {
                                                File.Delete(this.pathReemplazo + Fich.Name);

                                              //  File.Move(directorio_FamiliaPDF + Fich.Name, this.pathReemplazo + Fich.Name);

                                                // Carlos Sanchez 
                                                if (File.Exists(directorio_FamiliaPDF + Fich.Name) == false)
                                                {
                                                    if (File.Exists(buzon + Fich.Name) == true)
                                                    {
                                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    File.Delete(directorio_FamiliaPDF + Fich.Name);
                                                    if (File.Exists(buzon + Fich.Name) == true)
                                                    {
                                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                                    }
                                                    
                                                }
                                                File.Delete(Fich.FullName);

                                                //File.Delete(directorio_FamiliaPDF + Fich.Name);
                                                actualizarPathMarca(marca, "");
                                                registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Deleted", directorio_FamiliaNC.Substring(directorio_FamiliaNC.IndexOf(familia)) + fichero_NC_new.Substring(fichero_NC_new.LastIndexOf("\\") + 1), "Deleted", user);
                                            }
                                            else
                                            {
                                                registrarHistoricoPlano(marca, "", "", directorio_FamiliaNC.Substring(directorio_FamiliaNC.IndexOf(familia)) + fichero_NC_new.Substring(fichero_NC_new.LastIndexOf("\\") + 1), "Deleted", user);
                                            }
                                        }

                                        if (File.Exists(fichero_CAM_new) == true)
                                        {
                                            File.Delete(fichero_CAM_new);

                                            if (File.Exists(directorio_FamiliaPDF + Fich.Name))
                                            {
                                                File.Delete(this.pathReemplazo + Fich.Name);
                                                

                                                //File.Delete(directorio_FamiliaPDF + Fich.Name);
                                                actualizarPathMarca(marca, "");
                                                registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Deleted", directorio_FamiliaCAM.Substring(directorio_FamiliaCAM.IndexOf(familia)) + fichero_CAM_new.Substring(fichero_CAM_new.LastIndexOf("\\") + 1), "Deleted", user);
                                            }
                                            else
                                            {
                                                registrarHistoricoPlano(marca, "", "", directorio_FamiliaCAM.Substring(directorio_FamiliaCAM.IndexOf(familia)) + fichero_CAM_new.Substring(fichero_CAM_new.LastIndexOf("\\") + 1), "Deleted", user);
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Copiar los PDF´s - NC/CAM a las carpetas de su familia correspondiente
                                if (conjunto == true)
                                {
                                    if ((File.Exists(fichero_NC) == false) && (File.Exists(fichero_CAM) == false))
                                    {
                                        // Carlos Sanchez 
                                        if (File.Exists(directorio_FamiliaPDF + Fich.Name) == false)
                                        {
                                            if (File.Exists(buzon + Fich.Name) == true)
                                            {
                                                File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                                File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                            }
                                           
                                        }
                                        else
                                        {
                                            File.Delete(directorio_FamiliaPDF + Fich.Name);
                                            if (File.Exists(buzon + Fich.Name) == true)
                                            {
                                                File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                                File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                            }
                                          
                                        }
                                        File.Delete(Fich.FullName);

                                        actualizarPathMarca(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name);
                                        registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Inserted", "", "", user);
                                    }
                                    else
                                    {
                                        //bodyMail += " El fichero: " + Fich.Name + " NO puede llevar asociado un fichero NC o CAM </BR>";
                                        break;
                                    }
                                }
                                else
                                {
                                    if (File.Exists(fichero_NC) == true)
                                    {
                                        if (marca.EndsWith(".") == true)
                                        {
                                            File.Copy(fichero_NC, pathcopiaSeguridad+marca+"NC");
                                        }
                                        else
                                        {
                                            File.Copy(fichero_NC, pathcopiaSeguridad + marca + ".NC");
                                        }
                                        
                                        File.Move(fichero_NC, fichero_NC_new);
                                       // File.Move(Fich.FullName, directorio_FamiliaPDF + Fich.Name);

                                        // Carlos Sanchez 
                                        if (File.Exists(directorio_FamiliaPDF + Fich.Name) == false)
                                        {
                                            
                                                if (File.Exists(buzon + Fich.Name) == true)
                                                {
                                                    File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                                    File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                                }
                                            
                                        }
                                        else
                                        {
                                           
                                                File.Delete(directorio_FamiliaPDF + Fich.Name);
                                                if (File.Exists(buzon + Fich.Name) == true)
                                                {
                                                    File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                                    File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                                }
                                            
                                        }

                                        if (File.Exists(Fich.FullName) == true)
                                        {
                                            File.Delete(Fich.FullName);
                                        }
                                        actualizarPathMarca(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name);
                                        registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Inserted", directorio_FamiliaNC.Substring(directorio_FamiliaNC.IndexOf(familia)) + fichero_NC_new.Substring(fichero_NC_new.LastIndexOf("\\") + 1), "Inserted", user);
                                    }
                                    else
                                    {
                                        if (File.Exists(fichero_CAM) == true)
                                        {
                                            if (marca.EndsWith(".") == true)
                                            {
                                                File.Copy(fichero_NC, pathcopiaSeguridad + marca + "CAM");
                                            }
                                            else
                                            {
                                                File.Copy(fichero_NC, pathcopiaSeguridad + marca + ".CAM");
                                            }
                                            File.Move(fichero_CAM, fichero_CAM_new);
                                            
                                            //File.Move(Fich.FullName, directorio_FamiliaPDF + Fich.Name);

                                            // Carlos Sanchez 
                                            if (File.Exists(directorio_FamiliaPDF + Fich.Name) == false)
                                            {
                                                if(File.Exists(buzon + Fich.Name) == true)
                                                {
                                                File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                                File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                                }
                                            }
                                            else
                                            {
                                                File.Delete(directorio_FamiliaPDF + Fich.Name);
                                                if (File.Exists(buzon + Fich.Name) == true)
                                                {
                                                    File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                                    File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                                }
                                            }
                                            File.Delete(Fich.FullName);


                                            actualizarPathMarca(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name);
                                            registrarHistoricoPlano(marca, directorio_FamiliaPDF.Substring(directorio_FamiliaPDF.IndexOf(familia)) + Fich.Name, "Inserted", directorio_FamiliaCAM.Substring(directorio_FamiliaCAM.IndexOf(familia)) + fichero_CAM_new.Substring(fichero_CAM_new.LastIndexOf("\\") + 1), "Inserted", user);
                                        }
                                        else
                                        {
                                            //bodyMail += " El fichero: " + Fich.Name + " debe llevar asociado un fichero NC o CAM </BR>";
                                            break;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }//marca
                        //else
                        //{
                        //    bodyMail += " El fichero: " + Fich.Name + " no se pudo procesar xq no existe la marca: " + marca + " en IRATI </BR>";
                        //}
                    }//foreach




                    #region Carlos Sanchez 25/01/1024 , compruebo si son conjuntos soldados y si es asi, los copio.
                    Ficheros.Initialize();
                    Ficheros = dir.GetFiles("*.*");
                    foreach (FileInfo Fich in Ficheros)
                    {

                        string nombre = Fich.Name;
                        string ultimoCaracter = nombre.Substring(nombre.Length - 5);
       
                            if (ultimoCaracter.Equals("..pdf")){

                                familia = DevolverFamiliaMarca(marca);
                                directorio_FamiliaPDF = DevolverPathFamilia(familia, "PDF");

                                // Carlos Sanchez 
                                if (File.Exists(directorio_FamiliaPDF + Fich.Name) == false)
                                {
                                    if (File.Exists(buzon + Fich.Name) == true)
                                    {
                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                    }
                                }
                                else
                                {

                                    File.Delete(directorio_FamiliaPDF + Fich.Name);
                                    if (File.Exists(buzon + Fich.Name) == true)
                                    {
                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(directorio_FamiliaPDF, Fich.Name));
                                        File.Copy(Path.Combine(buzon, Fich.Name), Path.Combine(pathcopiaSeguridad, Fich.Name));
                                    }
                                }
                                File.Delete(Fich.FullName);
                            }
                    }
                    #endregion

                    #region todo lo que quede en el buzon se lleva a la carpeta de errores
                    Ficheros.Initialize();
                    Ficheros = dir.GetFiles("*.*");
                    foreach (FileInfo Fich in Ficheros)
                    {
                        //if ((this.tipoArchivo != "*" + Path.GetExtension(Fich.FullName)) && (Path.GetExtension(Fich.FullName) != ".pdf"))
                        //    bodyMail += " El fichero: " + Fich.Name + " no se pudo procesar ya que no es un pdf </BR>";

                        File.Delete(this.pathError + Fich.Name);
                        File.Move(Fich.FullName, this.pathError + Fich.Name);
                    }
                    #endregion


                    //if (bodyMail != "")
                    //{
                    //    this.mandarEmail(email, "Error al procesar el Buzon de Piezas.Oficina", bodyMail);
                    //}*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " ERROR AL PROCESAR EL BUZÓN ");
            }
        }


       
        private bool ExisteMarca(String Marca)
        {
            int num = 0;

            String strSql = "SELECT count(*) FROM T_ARTICULOS WHERE CODEMP = '" + codemp + "' AND CODIGO = '" + Marca + "'";

            SqlCommand comando = new SqlCommand(strSql, this.conexion);

            try
            {
                conexion.Open();
                num = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry("Error al comprobar la existencia de la marca: " + Marca + "  Error: " + ex.Message);                
            }
            finally
            {
                conexion.Close();
            }

            return num == 0 ? false : true;
        }

        private String DevolverFamiliaMarca(String Marca)
        {
            String familia = "";

            String strSql = "SELECT familia FROM T_ARTICULOS WHERE CODEMP = '" + codemp + "' AND CODIGO = '" + Marca + "'";

            SqlCommand comando = new SqlCommand(strSql, this.conexion);

            try
            {
                conexion.Open();
                familia = comando.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry("Error al comprobar la familia de la marca: " + Marca + "  Error: " + ex.Message);                
            }
            finally
            {
                conexion.Close();
            }

            return familia;
        }

        private String DevolverPathFamilia(String Familia, String Tipo)
        {
            String path = "";

            String strSql = "SELECT path FROM TC_FAMILIA_PATH WHERE CODEMP = '" + codemp + "' AND FAMILIA = '" + Familia + "' and tipo = '" + Tipo + "'";

            SqlCommand comando = new SqlCommand(strSql, this.conexion);

            try
            {
                conexion.Open();
                path = comando.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry("Error al comprobar el path de la familia: " + Familia + " tipo: " + Tipo + "  Error: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return path;
        }

        private bool esConjunto(String Marca)
        {
            int num = 0;

            String strSql = "SELECT dbo.IME_EsConjunto('" + codemp + "', '" + Marca + "', '0')";

            SqlCommand comando = new SqlCommand(strSql, this.conexion);

            try
            {
                conexion.Open();
                num = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry("Error al comprobar si es un conjunto la marca: " + Marca + "  Error:" + ex.Message);                
            }
            finally
            {
                conexion.Close();
            }

            return num == 0 ? false : true;
        }

        private void actualizarPathMarca(String Marca, String path)
        {
            String strSql = "UPDATE T_ARTICULOS ";
            strSql += " SET FILEPLA = '" + path + "' WHERE CODEMP = '" + codemp + "' AND CODIGO = '" + Marca + "'";

            SqlCommand comando = new SqlCommand(strSql, this.conexion);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry("Error al actualizar el path de la marca: " + Marca + " con el path: " + path + "  Error: " + ex.Message);                
            }
            finally
            {
                conexion.Close();
            }
                 
      
        }

        private void registrarHistoricoPlano(String Marca, String path_pdf, String operacion_pdf, String path_NCCAM, String operacion_NCCAM, string usuario)
        {
            String strSql = "INSERT INTO TC_PLANOS_HISTORICO ";
            //strSql += " VALUES ('" + codemp + "', '" + Marca + "', '" + path_pdf + "', '" + operacion_pdf + "', '" + path_NCCAM + "', '" + operacion_NCCAM + "',getdate(), '" + Environment.UserName.ToString() + "')";
            strSql += " VALUES ('" + codemp + "', '" + Marca + "', '" + path_pdf + "', '" + operacion_pdf + "', '" + path_NCCAM + "', '" + operacion_NCCAM + "',getdate(), '" + usuario + "')";

            SqlCommand comando = new SqlCommand(strSql, this.conexion);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry("Error al insertar en el historico del plano de la marca: " + Marca + "   Error: " +ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void mandarEmail(String usuario, String marca)
        {

            string destinatario = "nazaret.pacheco@imedexsa.es;raquel@imedexsa.es;jfrodriguez@imedexsa.es;gruiz@imedexsa.es";

            string asunto = "Reemplazo de plano ya existente";

            string cuerpo = "El usuario " + usuario + " acaba de reemplazar el plano de la marca: " + marca + ", por uno nuevo";

            String strSql = "EXECUTE [sp_enviar_mail] 'ServicioPlano@imedexsa.es','" + destinatario + "','" + asunto + "','" + cuerpo + "', 'HTML'";

            SqlCommand comando = new SqlCommand(strSql, this.conexion);

            try
            {
                conexion.Open();
                comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry("Error al comprobar si es un conjunto soldado la marca: " + Marca + "  Error:" + ex.Message);                
            }
            finally
            {
                conexion.Close();
            }



        }


    }
}
