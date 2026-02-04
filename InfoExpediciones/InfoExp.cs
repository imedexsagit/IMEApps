using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AdvancedDataGridView;

namespace InfoExpediciones
{
    public partial class InfoExp : Form
    {

        bool ord = false;

        float totalEmpaq = 0;
        float totalKg = 0;

        DateTime fMax = new DateTime(1999, 01, 01);
        DateTime fMin = new DateTime(2999, 01, 01);

        public InfoExp()
        {
            InitializeComponent();
        }

        private void InfoExp_Load(object sender, EventArgs e)
        {
            string strsql;
            SqlCommand comando;
            SqlDataAdapter adapter;
            System.Data.DataTable table = null;

            SqlConnection conexion = new SqlConnection(Utils.CD.getConexion());
            Cursor.Current = Cursors.WaitCursor;
            
            try
            {
                    //Actulizado
                    conexion.Open();

                    /*strsql = @"select CAST (CODLANZA AS INT) as PROYECTO, PROYECTO AS DESCRIPCION, CAST (PEDIDO AS INT) AS PEDIDO, CAST (LINEA AS INT) AS LÍNEA, cast([KG COMPLETADOS] as float) as [KG EMPAQUETADOS], cast ((round(xd2.[KG COMPLETADOS]/xd2.[KG_LINEA], 2) * 100) as float) AS [PORCENTAJE EMPAQUETADO], cast (KG_LINEA as float) AS [KG TOTALES LÍNEA], [FFINAL] as [FECHA ENTREGA]

                                from
	                                (SELECT 
		                                DISTINCT
		                                CODLANZA,
		                                PROYECTO,
		                                PEDIDO,
		                                LINEA,
		                                FFINAL,
		                                round(SUM(PESO), 2) AS [KG COMPLETADOS]--,
		


				                                ,(SELECT 
					                                ROUND (SUM(PESO*CtdLan), 2) 
				                                FROM 
					                                TC_LANZA_LISTADO_MARCAS
					                                INNER JOIN TC_LANZA_PEDLIN_ORDFAB 
						                                ON TC_LANZA_LISTADO_MARCAS.CodLanza = TC_LANZA_PEDLIN_ORDFAB.CODLANZA
						                                AND TC_LANZA_LISTADO_MARCAS.OrdFab = TC_LANZA_PEDLIN_ORDFAB.ORDFAB
						                                AND TC_LANZA_LISTADO_MARCAS.Marca = TC_LANZA_PEDLIN_ORDFAB.CODIGO
						
					                                WHERE
						                                PEDIDO = XD.PEDIDO
						                                AND LINEA = XD.LINEA
				                                GROUP BY PEDIDO, LINEA) as KG_LINEA

	                                FROM (
		                                SELECT 
		                                DISTINCT
			                                LPOF.CODLANZA,
			                                (SELECT PROYECTO FROM TC_LANZA WHERE TC_LANZA.CODLANZA = LPOF.CODLANZA) AS PROYECTO,
			                                LPOF.CODIGO,
			                                LPOF.PEDIDO,
			                                LPOF.LINEA,
			                                OL.FFINAL,
			                                PLM.PACKING, 
			                                PLM.PAQUETE,
			
			                                CASE WHEN LLM.PESO IS NULL THEN 0 ELSE 

			                                round (LLM.Peso, 2) * plm.CANTIDAD END as PESO,
			                                LLM.CtdLan as [CANTIDAD LANZADA],
			                                PLM.CANTIDAD AS [CANTIDAD PACKING],
			
			                                CASE WHEN PLM.PACKING IS NULL THEN '-' ELSE 
			                                case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_MARCAS where TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP = LPOF.CODEMP and TC_PACKING_LIST_PAQUETES_MARCAS.PACKING = PLM.PACKING and TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE = PLM.PAQUETE)
					                                when	(select COUNT(*) from TC_PACKING_LIST_PAQUETES_MARCAS where TC_PACKING_LIST_PAQUETES_MARCAS.CODEMP = LPOF.CODEMP and TC_PACKING_LIST_PAQUETES_MARCAS.PACKING = PLM.PACKING and TC_PACKING_LIST_PAQUETES_MARCAS.PAQUETE = PLM.PAQUETE and TC_PACKING_LIST_PAQUETES_MARCAS.CATEGORIA in ('Tornilleria','Forro')) then 'TO'
					                                else	case (select COUNT(*) from TC_PACKING_LIST_PAQUETES_Operaciones where TC_PACKING_LIST_PAQUETES_Operaciones.CODEMP = LPOF.CODEMP and TC_PACKING_LIST_PAQUETES_Operaciones.PACKING = PLM.PACKING and TC_PACKING_LIST_PAQUETES_Operaciones.PAQUETE = PLM.PAQUETE and OPERAC = '900001')
								                                when 0 then '-'
								                                else 'X'
							                                end
						                                END
				                                end as MONTAJE_FICHADO
		                                FROM 
			                                TC_LANZA_PEDLIN_ORDFAB LPOF
			                                LEFT JOIN TC_PACKING_LIST_PAQUETES_MARCAS PLM
				                                ON LPOF.PEDIDO = PLM.PEDIDO
				                                AND LPOF.LINEA = PLM.LINEA
				                                AND LPOF.CODIGO = PLM.MARCA
			                                LEFT JOIN T_ORDTERL OL
				                                ON LPOF.PEDIDO = OL.NUMPED
				                                AND LPOF.LINEA = OL.LINEA
			                                LEFT JOIN TC_LANZA_LISTADO_MARCAS LLM
				                                ON LLM.CodLanza = LPOF.CODLANZA
				                                AND LLM.MARCA = LPOF.CODIGO
				                                AND LLM.OrdFab  =LPOF.ORDFAB
		                                WHERE 
			                                LPOF.CodEmp = 3
			                                AND FLAG = 0
			                                and FFINAL > (dateadd(month, -6, getdate()))

		                                ) XD

	                                WHERE 
		                                MONTAJE_FICHADO = 'X'

	                                GROUP BY 
		                                CODLANZA,
		                                PROYECTO,
		                                PEDIDO,
		                                LINEA,
		                                FFINAL


		                                ) xd2

		                                order by CODLANZA";*/

                    strsql = "SELECT PROYECTO, DESCRIPCION, PEDIDO, LINEA as LÍNEA, KG_EMPAQ AS [KG EMPAQUETADOS], PORC_EMPAQ AS [PORCENTAJE EMPAQUETADO] , KG_TOTALES AS [KG TOTALES LÍNEA], convert (date, F_ENTREGA) AS [FECHA ENTREGA] FROM INFO_EXPEDICIONES";

                comando = new SqlCommand(strsql, conexion);
                comando.CommandTimeout = 900000;
                adapter = new SqlDataAdapter(comando);
                table = new System.Data.DataTable();
                adapter.Fill(table);

                //dataGridView1.Columns[7].DefaultCellStyle.Format = "dd.MM.yyyy";

                //this.dataGridView1.DataSource = table;
                
                DataGridViewProgressColumn column = new DataGridViewProgressColumn();

                /*dataGridView1.Columns.Add(column);
                dataGridView1.Columns[8].Name = "PORCENTAJE EMPAQUETADO";*/

                //string porc = "0";

                dataGridView1.ColumnCount = table.Columns.Count-1;

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (i != 5)
                    {
                        if (i > 4)
                        {
                            dataGridView1.Columns[i-1].Name = table.Columns[i].ColumnName;
                        }
                        else
                        {
                            dataGridView1.Columns[i].Name = table.Columns[i].ColumnName;
                        }
                    }

                    
                }
                
                dataGridView1.Columns.Add(column);
                dataGridView1.Columns[7].Name = "PORCENTAJE EMPAQUETADO";
                dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy";

                object[] newRow;

                totalKg = 0;
                totalEmpaq = 0;

                foreach (DataRow row in table.Rows)
                {
                    newRow = new object[] { row[0], row[1], row[2], row[3], row[4], row[6], row[7], Convert.ToInt32(row[5].ToString()) };
                    dataGridView1.Rows.Add(newRow);
                    totalEmpaq = totalEmpaq + (float)Convert.ToDouble(row[4]);
                    totalKg = totalKg + (float)Convert.ToDouble(row[6]);

                    if ((DateTime) row[7] > fMax)
                    {
                        fMax = (DateTime)row[7];
                    }

                    if ((DateTime)row[7] < fMin)
                    {
                        fMin = (DateTime)row[7];
                    }

                }

                labelEmpaq.Text = totalEmpaq.ToString("F");
                labelTotal.Text = totalKg.ToString("F");

                dtpDesde.Value = fMin;
                dtpHasta.Value = fMax;


            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();


            }

            Cursor.Current = Cursors.WaitCursor;

        }

        private void tbBusq_TextChanged(object sender, EventArgs e)
        {
            filtro();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                if (ord)
                {
                    this.dataGridView1.Sort(this.dataGridView1.Columns[7], ListSortDirection.Ascending);
                }
                else
                {
                    this.dataGridView1.Sort(this.dataGridView1.Columns[7], ListSortDirection.Descending);
                }

                ord = !ord;
            }
        }

        private float recuentoKg(int tipo)
        {
            float total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += (float)row.Cells[tipo].Value;
            }

            return total;
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            filtro();
        }

        private void filtro()
        {
            Cursor.Current = Cursors.WaitCursor;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((DateTime)row.Cells[6].Value >= dtpDesde.Value
                    && (DateTime)row.Cells[6].Value <= dtpHasta.Value)
                {
                    if ((!row.Cells[0].Value.ToString().Contains(tbBusq.Text)
                        && !row.Cells[1].Value.ToString().Contains(tbBusq.Text)
                        && !row.Cells[2].Value.ToString().Contains(tbBusq.Text))
                        && tbBusq.Text != "")
                    {
                        row.Visible = false;
                    }
                    else
                    {
                        row.Visible = true;
                    }
                }
                else
                {
                    row.Visible = false;
                }

            }

            Cursor.Current = Cursors.Default;
        }

        private void dtpDesde_ValueChanged_1(object sender, EventArgs e)
        {
            filtro();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            filtro();
        }
    }
}
