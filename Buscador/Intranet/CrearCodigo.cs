using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;
using Utils;
using empresaGlobalProj;

namespace Intranet
{
    public partial class CrearCodigo : Form
    {
        private Int32 Tipo;         //0->Crear      1->Editar       2->Editar Denominaciones
        private String codigoElegido;
        private String CodEmp;
        private String Usuario;


        private String denominacion_es_original;
        private String denominacion_en_original;
        private String denominacion_fr_original;
        private String denominacion_de_original;

        public CrearCodigo(Int32 tipo, String codemp, String codigoElegido, String user)
        {
            InitializeComponent();


            this.Tipo = tipo;

            this.codigoElegido = codigoElegido;            
            this.CodEmp = codemp;
            this.Usuario = user;            
        }



        private void CrearCodigo_Load(object sender, EventArgs e)
        {
            this.txtCodigo.Text = this.codigoElegido;

            switch (this.Tipo)
            {
                case 0:
                    this.Text = "Crear Código";                    

                    // TODO: esta línea de código carga datos en la tabla 'dsCategorias.T_CATEGORIAS' Puede moverla o quitarla según sea necesario.
                    this.t_CATEGORIASTableAdapter.Fill(this.dsCategorias.T_CATEGORIAS);

                    // TODO: esta línea de código carga datos en la tabla 'dsFamilias.T_FAMILIAS' Puede moverla o quitarla según sea necesario.
                    this.t_FAMILIASTableAdapter.Fill(this.dsFamilias.T_FAMILIAS, empresaGlobal.empresaID);


                    break;
                case 1:
                    this.Text = "Editar Código";

                    this.denominacion_es_original = this.getDenominacion(this.CodEmp, this.codigoElegido);
                    this.denominacion_en_original = this.getTraduccionDenominacion(this.CodEmp, this.codigoElegido, "en");
                    this.denominacion_fr_original = this.getTraduccionDenominacion(this.CodEmp, this.codigoElegido, "fr");
                    this.denominacion_de_original = this.getTraduccionDenominacion(this.CodEmp, this.codigoElegido, "de");

                    this.txtDenominacion.Text = this.denominacion_es_original;
                    this.txtDenominacion_en.Text = this.denominacion_en_original;
                    this.txtDenominacion_fr.Text = this.denominacion_fr_original;
                    this.txtDenominacion_de.Text = this.denominacion_de_original;


                    this.btnCrear.Text = "Guardar";

                    break;
                case 2:
                    this.Text = "Editar Denominación Código";

                    this.denominacion_es_original = this.getDenominacion(this.CodEmp, this.codigoElegido);
                    this.denominacion_en_original = this.getTraduccionDenominacion(this.CodEmp, this.codigoElegido, "en");
                    this.denominacion_fr_original = this.getTraduccionDenominacion(this.CodEmp, this.codigoElegido, "fr");
                    this.denominacion_de_original = this.getTraduccionDenominacion(this.CodEmp, this.codigoElegido, "de");

                    this.txtDenominacion.Text = this.denominacion_es_original;
                    this.txtDenominacion_en.Text = this.denominacion_en_original;
                    this.txtDenominacion_fr.Text = this.denominacion_fr_original;
                    this.txtDenominacion_de.Text = this.denominacion_de_original;

                    
                    this.panel1.Visible = false;
                    this.btnCrear.Text = "Guardar";

                    break;
           }

        }


        private void btnTraducir_Click(object sender, EventArgs e)
        {
            if (this.txtDenominacion_en.Text != "" || this.txtDenominacion_fr.Text != "" || this.txtDenominacion_de.Text != "")
            {
                if (MessageBox.Show("Ya existen traducciones para la denominación de ese código. ¿Desea borrarlas para volver a traducir?", "Borrar traducciones", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.txtDenominacion_en.Text = ""; 
                    this.txtDenominacion_fr.Text = "";
                    this.txtDenominacion_de.Text = "";
                }
            }


            if (this.txtDenominacion_en.Text == "")
                this.txtDenominacion_en.Text = this.TraducirDenominacion(this.CodEmp, this.txtDenominacion.Text, "en");
            if (this.txtDenominacion_fr.Text == "")
                this.txtDenominacion_fr.Text = this.TraducirDenominacion(this.CodEmp, this.txtDenominacion.Text, "fr");
            if (this.txtDenominacion_de.Text == "")
                this.txtDenominacion_de.Text = this.TraducirDenominacion(this.CodEmp, this.txtDenominacion.Text, "de");
        }



        private void btnCrear_Click(object sender, EventArgs e)
        {

            switch (this.Tipo)
            {
                case 0:
                    if (txtCodigo.Text == "")
                    {
                        MessageBox.Show("El código a crear no puede estar vacío");
                        return;
                    }

                    if (txtDenominacion.Text == "")
                    {
                        MessageBox.Show("El código a crear no puede tener una denominación vacía");
                        return;
                    }

                    if (cmbFamilia.Text == "")
                    {
                        MessageBox.Show("El código a crear no puede tener una familia vacía");
                        return;
                    }

                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {                    
                            //Se usa la categoría TRAMO (32) ya que esta manera de crear un código solo está destinada a importar sobre él una estructura existente
                            CD.crearArticulo(this.CodEmp, txtCodigo.Text, txtDenominacion.Text, cmbFamilia.ValueMember, "32", "", this.Usuario);
                            CD.crearCaracteristicasArticulo(this.CodEmp, txtCodigo.Text, "32", 0, 0, "", false, this.Usuario);

                            //The Complete method commits the transaction. If an exception has been thrown, complete is not called and the transaction is rolled back.
                            scope.Complete();

                            MessageBox.Show("Código creado correctamente", "Crear", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al crear el código: " + ex.Message, "Crear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case 1:


                    break;

                case 2:

                    if (this.denominacion_en_original == "")
                        this.almacenarTraduccionDenominacion(this.CodEmp, this.codigoElegido, "en", this.txtDenominacion_en.Text, this.Usuario);
                    else
                        if (this.txtDenominacion_en.Text != this.denominacion_en_original)
                            this.actualizarTraduccionDenominacion(this.CodEmp, this.codigoElegido, "en", this.txtDenominacion_en.Text, this.Usuario);

                    if (this.denominacion_fr_original == "")
                        this.almacenarTraduccionDenominacion(this.CodEmp, this.codigoElegido, "fr", this.txtDenominacion_fr.Text, this.Usuario);
                    else
                        if (this.txtDenominacion_fr.Text != this.denominacion_fr_original)
                            this.actualizarTraduccionDenominacion(this.CodEmp, this.codigoElegido, "fr", this.txtDenominacion_fr.Text, this.Usuario);

                    if (this.denominacion_de_original == "")
                        this.almacenarTraduccionDenominacion(this.CodEmp, this.codigoElegido, "de", this.txtDenominacion_de.Text, this.Usuario);
                    else
                        if (this.txtDenominacion_de.Text != this.denominacion_de_original)
                            this.actualizarTraduccionDenominacion(this.CodEmp, this.codigoElegido, "de", this.txtDenominacion_de.Text, this.Usuario);

                    MessageBox.Show("Denominaciones almacenadas correctamente", "Guardar Denominación", MessageBoxButtons.OK, MessageBoxIcon.None);
                    break;
            }

            this.Close();            
        }





        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }






        #region Traducciones JRegino 05/07/2016
        private String getConexion()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ggConnectionString"].ToString();
        }

        private String getDenominacion(String CodEmp, String codigo)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "Select isnull(Denominacion, '') ";
                strSql += "     From T_ARTICULOS";
                strSql += "     Where Codemp = '" + CodEmp + "' and Codigo = '" + codigo + "' ";


                sqlCmd = new SqlCommand(strSql, conexion);

                return sqlCmd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private String TraducirDenominacion(String CodEmp, String denominacion, String idioma)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "SELECT dbo.IME_TraducirTexto('" + CodEmp + "', '" + denominacion + "', '" + idioma + "') ";

                sqlCmd = new SqlCommand(strSql, conexion);

                return sqlCmd.ExecuteScalar().ToString();
            }
            catch (Exception) { }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return "";
        }



        private String getTraduccionDenominacion(String CodEmp, String codigo, String idioma)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "Select isnull(Denominacion, '') ";
                strSql += "     From TC_ARTICULOS_IDIOMAS";
                strSql += "     Where Codemp = '" + CodEmp + "' and Codigo = '" + codigo + "' and Idioma = '" + idioma + "' ";
                

                sqlCmd = new SqlCommand(strSql, conexion);

                return sqlCmd.ExecuteScalar().ToString();
            }
            catch (Exception) {}
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return "";
        }

        private void almacenarTraduccionDenominacion(String CodEmp, String codigo, String idioma, String denominacion, String user)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "Insert Into TC_ARTICULOS_IDIOMAS ";
                strSql += " Values ('" + CodEmp + "', '" + codigo + "', '" + idioma + "', '" + denominacion + "', '" + user + "', getdate(), null, null)";

                sqlCmd = new SqlCommand(strSql, conexion);

                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void actualizarTraduccionDenominacion(String CodEmp, String codigo, String idioma, String denominacion, String user)
        {
            String strSql = "";
            SqlCommand sqlCmd;

            SqlConnection conexion = new SqlConnection(getConexion());

            try
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();

                strSql = "Update TC_ARTICULOS_IDIOMAS ";
                strSql += "     Set Denominacion = '" + denominacion + "', ";
                strSql += "          usumod = '" + user + "', ";
                strSql += "          fechamod = getdate() ";
                strSql += "     Where Codemp = '" + CodEmp + "' ";
                strSql += "         and Codigo = '" + codigo + "' ";
                strSql += "         and Idioma = '" + idioma + "' ";

                sqlCmd = new SqlCommand(strSql, conexion);

                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        #endregion

    }
}
