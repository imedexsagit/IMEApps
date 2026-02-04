using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using empresaGlobalProj;

namespace GestionSoldadura
{
    public partial class Form4 : Form
    {
        string proyect = "";

        public Form4(string proyecto)
        {
            InitializeComponent();
            this.proyect = proyecto;

            //Obtengo las inspecciones realizadas ha etiquetas de ese proyecto y las muestro.

            SqlConnection conexin = null;
            SqlCommand comand = null;
            string strsq;
            string user = Environment.UserName;


            string connString = "Data Source=srvsql02;Initial Catalog=gg;User ID=gg;Password=ostia";
            DataTable table = new DataTable();

            conexin = new SqlConnection(connString);
            conexin.Open();

            SqlDataAdapter adapter;

            strsq = " select Etiqueta, Fecha, CodOperario, Operario, CodMaquina, Maquina, Sold_PB, Sold_CD, Sold_IV,  ";
            strsq += "Sold_LF, Sold_END_UT, Sold_END_MP, Sold_END_LP, Sold_END_VT, Sold_END_ctd, VistoBuenoAutoControl, Correcto,  ";
            strsq += "NoConformidad, fechacre, usucre from TC_InspeccionEtiquetas where Etiqueta ";
            strsq += "in( SELECT E.Etiqueta from T_ORDFAB O ";
                         strsq += "FULL OUTER JOIN TC_LANZA_ETIQUETA E ";
                         strsq += "ON O.ORDFAB = E.OrdFab ";
                         strsq += "WHERE (O.CODEMP = '" + empresaGlobal.EmpresaID + "') AND O.PROYECTO = CAST('" + proyecto + "' AS varchar(10)) AND (O.TIPOREG = 'F') AND ";
                         strsq += "isNULL((SELECT TOP (1) T_ARTICULOS.CATEGORIA FROM  T_ORDFABM INNER JOIN T_ARTICULOS ";
                         strsq += "ON T_ORDFABM.CODEMP = T_ARTICULOS.CodEMP AND T_ORDFABM.CODIGO = T_ARTICULOS.CODIGO ";
			             strsq += " WHERE (T_ORDFABM.CODEMP = O.CODEMP) AND (T_ORDFABM.ORDFAB = O.ORDFAB) ";
                         strsq += "AND (T_ORDFABM.CTDENL > 0) AND (T_ARTICULOS.FAMILIA = 'MP')),'') = '') ";
                          

            comand = new SqlCommand(strsq, conexin);
            adapter = new SqlDataAdapter(comand);

            comand.CommandText = strsq;
            table.Columns.Clear();
            table.Clear();
            adapter.Fill(table);

    
            conexin.Close();

            tC_InspeccionEtiquetasDataGridView.DataSource = table;
        }

        private void tC_InspeccionEtiquetasDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //Inspecciones CJS
            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_PB"].Style.BackColor = Color.Red;


            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_CD"].Style.BackColor = Color.Red;


            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_IV"].Style.BackColor = Color.Red;


            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_LF"].Style.BackColor = Color.Red;



            //Ensayos NO destructivos CJS
            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_UT"].Style.BackColor = Color.Red;


            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_MP"].Style.BackColor = Color.Red;


            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_LP"].Style.BackColor = Color.Red;

            if (tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Value == DBNull.Value)
                this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Style.BackColor = Color.White;
            else
                if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Value) == 1)
                    this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Style.BackColor = Color.Green;
                else
                    if (Convert.ToInt32(this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Value) == 0)
                        this.tC_InspeccionEtiquetasDataGridView.Rows[e.RowIndex].Cells["Sold_END_VT"].Style.BackColor = Color.Red;
        }
    }
}
