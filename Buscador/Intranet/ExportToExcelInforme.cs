using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Intranet
{
    static class ExportToExcelInforme
    {
        public static void DataTableToExcelInforme(this System.Data.DataTable DataTable, string ExcelFilePath = null)
        {
            try {

                int ColumnsCount;

                if (DataTable == null || (ColumnsCount = DataTable.Columns.Count) == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // Abre excel, y crea un nuevo documento
                Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();

                Workbooks libro = Excel.Workbooks;
                Workbook libroActual = libro.Add();
                //A través de la siguiente forma añado una nueva hoja
                //Excel.Workbooks.Application.Sheets.Add();
                Sheets sheets = libroActual.Worksheets;

                //Excel.Workbooks.Add();

                // Instanciamos la hora activa
                Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;
                Worksheet.Name = "Informe";

                object[] Header = new object[ColumnsCount];

                // Tratamiento de las cabeceras 
                for (int i = 0; i < ColumnsCount; i++)
                    Header[i] = DataTable.Columns[i].ColumnName;

                Microsoft.Office.Interop.Excel.Range HeaderRange = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, ColumnsCount]));
                HeaderRange.Value = Header;
                HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                HeaderRange.Font.Bold = true;

                // Anclar primera fila
                Excel.Application.ActiveWindow.SplitRow = 1;
                Excel.Application.ActiveWindow.FreezePanes = true;

                // Habilitar filtro en la primera fila
                HeaderRange.AutoFilter(1,
                                    Type.Missing,
                                    Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlAnd,
                                    Type.Missing,
                                    true);

                // Tratamiento de las celdas
                int RowsCount = DataTable.Rows.Count;
                object[,] Cells = new object[RowsCount, ColumnsCount];

                //Bucle en el que se copia el contenido del DataTable a Cells
                for (int j = 0; j < RowsCount; j++)
                {

                    for (int i = 0; i < ColumnsCount; i++)
                    {
                            Cells[j, i] = DataTable.Rows[j][i];
                    }
                }

                //Se vuelca todo el contenido de cells en el Worksheet
                Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[2, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[RowsCount + 1, ColumnsCount])).Value = Cells;

                // Establecer ancho automático para todas las columnas
                Worksheet.Columns.AutoFit();

                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        Worksheet.SaveAs(ExcelFilePath);




                        Excel.Quit();
                        //System.Windows.MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // Si no recibe ruta muestra la aplicación Excel con el documento creado
                {
                    Excel.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
    }
}
