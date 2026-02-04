using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;





namespace Intranet
{
    static class InteropExcel
    {
        public static void DataTableToExcel(this System.Data.DataTable DataTable, string ExcelFilePath = null, bool bpivotTable = false, bool formulas = false)
        {
            try
            {
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
                Worksheet.Name = "Desglose";


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

                //Variables para las obtener las columnas de Peso Unitario y Peso Total
                int colPU = 0;
                int colPT = 0;

                //Int colCantidad variable para saber en qué columna está situada la columna cantidad
                int colCantidad = 0;

                //Int colCantidadTotal variable para saber en qué columna está situada la columna cantidad total
                int colCantidadTotal = 0;

                // int nlevels para almacenar el nº de niveles
                int nlevels = 0;

                //Bucle para obtener el nº de niveles y dónde se encuentra la columna cantidad y CantidadTotal
                //while (it < ncols && !flag)
                for (int it = 0; it < ColumnsCount; it++)
                {
                    if (DataTable.Columns[it].ColumnName == "Cantidad")
                    {
                        //se suma uno ya que si da por ejemplo 4, no cuenta con la columna 0, se suma 1
                        colCantidad = it;
                        //se almacena it porque contiene un valor menos de donde se guarda cantidad, entonces es el último nivel
                        nlevels = it;
                        System.Diagnostics.Debug.WriteLine(" === La columna cantidad se encuentra en la posición: " + colCantidad + " y el número de niveles es: " + nlevels);
                        //flag = true;
                    }
                    if (DataTable.Columns[it].ColumnName == "Cantidad Total")
                    {
                        colCantidadTotal = it;
                    }

                    if (DataTable.Columns[it].ColumnName == "Peso Unitario")
                    {
                        colPU = it;
                    }

                    if (DataTable.Columns[it].ColumnName == "Peso Total")
                    {
                        colPT = it;
                    }
                }


                //Bucle en el que se copia el contenido del DataTable a Cells
                for (int j = 0; j < RowsCount; j++)
                {

                    for (int i = 0; i < ColumnsCount; i++)
                    {
                        if (DataTable.Columns[i].ColumnName == "Peso Unitario" || DataTable.Columns[i].ColumnName == "Peso Total") //TOMÁS 30/11/2017. Las columnas de peso no se exportaban bien como número
                            Cells[j, i] = DataTable.Rows[j][i].ToString().Replace(",", ".");


                        else

                            Cells[j, i] = DataTable.Rows[j][i];

                    }
                }


                //en esta string está almacenado el nivel
                string levelString = "";

                //nivel actual en Int32
                int level = 0;

                //nivel superior
                int levelmas = 0;

                //código del nivel actual, se va a acceder por la fila j y el nivel que si es 3 pues el código sería el que está en nivel 2
                string codActual = "";

                //string fórmulas
                string formulaPadre = "", formulaHijo = "", formulaPadreCantidad = "", formulaHijoCantidad = "";

                //string para obtener la dirección de la celda actual del peso total, utilizado para saber el valor
                string celdaPesoTotal;
                //string para obtener la dirección de la celda cantidad utilizada en el cálculo del peso total
                string celdaCantidad;
                //string para obtener la dirección de la celda cantidad utilizada en el cálculo del peso total
                string celdaPesoUnitario;

                // if para controlar si se añaden fórmulas o no al excel
                if (formulas == true)
                {
                    //for para introducir fórmulas
                    for (int j = 0; j < RowsCount; j++)
                    {
                        //obtener el nivel en el que estamos
                        levelString = Convert.ToString(DataTable.Rows[j][DataTable.Columns.Count - 1]); //Nivel actual almacenado en esta variable
                        level = Convert.ToInt32(levelString);
                        //System.Diagnostics.Debug.WriteLine(" === El nivel actual es: " + levelString + ", el inferior es: " + (level - 1) + " y el nivel superior es: " + (level + 1));


                        //System.Diagnostics.Debug.WriteLine(" === El código es: " + DataTable.Rows[j][level].ToString());
                        codActual = DataTable.Rows[j][level].ToString();

                        if (j + 1 < RowsCount)
                        {
                            if (Convert.ToInt32(DataTable.Rows[j + 1][DataTable.Columns.Count - 1]) < DataTable.Rows.Count)
                            {
                                levelmas = Convert.ToInt32(DataTable.Rows[j + 1][DataTable.Columns.Count - 1]);
                                //System.Diagnostics.Debug.WriteLine(" === llego aquí 1, levelactual es: " + level + " levelmas:" + levelmas + " J es:" + j + " y RowCounts es " + DataTable.Rows.Count);
                            }
                            if ((level + 1) == levelmas && j + 1 < DataTable.Rows.Count)
                            {
                                //System.Diagnostics.Debug.WriteLine(" === llego aquí 2: ");
                                //celdaactual = getCellNamev2(j, i);
                                //colPU contiene la columna del peso unitario
                                formulaPadre = "=(";
                                int iterador = j + 1;
                                bool encontr = false;
                                //System.Diagnostics.Debug.WriteLine(" Variables del Bucle, iterador: " + iterador + " y bandera encontr: " + encontr);
                                while (iterador < DataTable.Rows.Count && !encontr)
                                {
                                    //System.Diagnostics.Debug.WriteLine(" === Entro Bucle While");
                                    //System.Diagnostics.Debug.WriteLine(" Último else if, muestro iterador y RowsCount: " + iterador + " y " + RowsCount);
                                    if ((level + 1) == Convert.ToInt32(DataTable.Rows[iterador][DataTable.Columns.Count - 1]))
                                    {
                                        //System.Diagnostics.Debug.WriteLine(" === llego aquí 3: ");
                                        //si quiero quitar el primer + evaluar si es el primero
                                        formulaPadre = formulaPadre + "+" + getCellNamev2(iterador, colPT);

                                        //cantidad
                                        //formulaPadreCantidad = getCellNamev2(iterador, colCantidad) + "*" + getCellNamev2(j, colCantidadTotal);
                                        //Cells[j, colCantidadTotal] = formulaPadreCantidad;

                                        //Tratamiento de la columna cantidad total para convertirla en fórmula
                                        formulaHijoCantidad = "=" + getCellNamev2(iterador, colCantidad) + "*" + getCellNamev2(j, colCantidadTotal);
                                        Cells[iterador, colCantidadTotal] = formulaHijoCantidad;

                                    }
                                    if ((level + 1) > Convert.ToInt32(DataTable.Rows[iterador][DataTable.Columns.Count - 1]) || iterador + 1 == RowsCount)
                                    {
                                        //System.Diagnostics.Debug.WriteLine(" Fin hijos se añade el último paréntesis");
                                        formulaPadre = formulaPadre + ")";
                                        encontr = true;
                                        //si encontrado encontes almaceno la fórmula en la columna peso total y reinicio fórmula
                                        //colPT es columna Peso Total
                                        Cells[j, colPT] = formulaPadre;
                                        formulaPadre = "";


                                    }
                                    iterador++;
                                }
                                //encontr = false;
                                //System.Diagnostics.Debug.WriteLine(" === Fin Bucle While");
                            }
                            else
                            { // de lo contrario es hijo
                                //System.Diagnostics.Debug.WriteLine(" NO es padre, aplico la otra fórmula");
                                celdaPesoTotal = getCellNamev2(j, colPT);
                                celdaCantidad = getCellNamev2(j, colCantidadTotal);
                                celdaPesoUnitario = getCellNamev2(j, colPU);
                                formulaHijo = "=" + celdaCantidad + "*" + celdaPesoUnitario;
                                //System.Diagnostics.Debug.WriteLine(" -__--__-----> La fórmula del Peso Total para el level 2 es: " + formulaHijo);
                                Cells[j, colPT] = formulaHijo;

                            }

                        }
                        else // última fila
                        {
                            //System.Diagnostics.Debug.WriteLine(" NO es padre, aplico la otra fórmula");
                            celdaPesoTotal = getCellNamev2(j, colPT);
                            celdaCantidad = getCellNamev2(j, colCantidadTotal);
                            celdaPesoUnitario = getCellNamev2(j, colPU);
                            formulaHijo = "=" + celdaCantidad + "*" + celdaPesoUnitario;
                            //System.Diagnostics.Debug.WriteLine(" -__--__-----> La fórmula del Peso Total para el level 2 es: " + formulaHijo);
                            Cells[j, colPT] = formulaHijo;
                        }

                    }
                }


                //Se vuelca todo el contenido de cells en el Worksheet
                Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[2, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[RowsCount + 1, ColumnsCount])).Value = Cells;

                // Establecer ancho automático para todas las columnas
                Worksheet.Columns.AutoFit();



                // Establecer en color gris celdas correspondientes a nombre de componentes de ascendientes
                int nivel;
                int posNivel = DataTable.Columns.Count - 1;
                for (int j = 0; j < RowsCount; j++)
                {
                    nivel = int.Parse(DataTable.Rows[j][posNivel].ToString());

                    if (nivel > 0)
                    {
                        Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[j + 2, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[j + 2, nivel])).Font.Color = System.Drawing.Color.LightGray;
                    }
                }


                // Pivot Table

                //El código inferior comentado es para crear una tabla dinámica, para crearla en una nueva hoja, descomentar el código inferior
                
                //Se crea una nueva hoja para la tabla dinámica
                if (bpivotTable == true)
                {
                    Worksheet sheetMMPP = Excel.Worksheets.Add();
                    sheetMMPP.Name = "MMPP";

                    Microsoft.Office.Interop.Excel.Range dataRange = Worksheet.UsedRange;
                    Microsoft.Office.Interop.Excel.PivotCache cache = (Microsoft.Office.Interop.Excel.PivotCache)libroActual.PivotCaches().Add(Microsoft.Office.Interop.Excel.XlPivotTableSourceType.xlDatabase, dataRange);
                    Microsoft.Office.Interop.Excel.PivotTable pt = (Microsoft.Office.Interop.Excel.PivotTable)sheetMMPP.PivotTables().Add(cache, sheetMMPP.get_Range("A1", "C3"), "MMPP");

                    //Se crean los pivotes para la tabla dinámica
                    //Microsoft.Office.Interop.Excel.PivotField oPivotField0 = (Microsoft.Office.Interop.Excel.PivotField)pt.PivotFields("Nivel 0");
                    //Microsoft.Office.Interop.Excel.PivotField oPivotField1 = (Microsoft.Office.Interop.Excel.PivotField)pt.PivotFields("Nivel 1");
                    //Microsoft.Office.Interop.Excel.PivotField oPivotField2 = (Microsoft.Office.Interop.Excel.PivotField)pt.PivotFields("Nivel 2");
                    Microsoft.Office.Interop.Excel.PivotField oPivotFieldPT = (Microsoft.Office.Interop.Excel.PivotField)pt.PivotFields("Peso Total");
                    Microsoft.Office.Interop.Excel.PivotField oPivotFieldMP = (Microsoft.Office.Interop.Excel.PivotField)pt.PivotFields("MP");
                    

                    //RowField se establece como Fila
                    //oPivotField0.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField;
                    //oPivotField1.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField;

                    oPivotFieldMP.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField;

                    //quitar en blancos de la tabla dinámica
                    oPivotFieldMP.PivotItems("(blank)").Visible = false;
                    oPivotFieldPT.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlDataField;
                    oPivotFieldPT.Function = Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum;
                    //Los datos que va a contener la fila, en este caso el Nivel 2
                    //oPivotFieldPT.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlDataField;
                }
                
                //oPivotField.Function = Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum;

                

                Worksheet.Activate();
                
                // Fin Pivot Table

                // Comprueba ruta donde guardar el fichero
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

         /*
         * Parámetros:
         * fila indica el nº de fila que equivale al número de celda, por ejemplo E6
         * Col es empleado para conocer la letra de la celda, se transforma el entero en letra
         * nCol indica el número máximo de col
         * nfilas indica el número máximo de filas
         */
        public static string getCellNamev2(int fila, int col)
        {
            //obtener dirección celda
            //ncols el número de columnas
            //fila j, col i
            char letraCol = 'A';
            String lcol = "x";
            letraCol = 'A';
            //Se suma col ya que es el número de col, si es 5 será A + 5 = F
            letraCol = Convert.ToChar(letraCol + col);
            //System.Diagnostics.Debug.WriteLine("********** Ejecutando Método getCellNamev2 Muestro la letra: " + letraCol);
            //Se convierte el char a String para añadir número
            lcol = Convert.ToString(letraCol);
            //System.Diagnostics.Debug.WriteLine("********** Muestro la letra convertida a String: " + letraCol);
            //Se añade el número y se suma dos ya que las primeras celdas son la cabecera
            string f = Convert.ToString(fila + 2);
            lcol = letraCol + f;
            //System.Diagnostics.Debug.WriteLine("Muestro la letra y el número: " + lcol + " Fin ejecución Método getCellNamev2");

            return lcol;
        }

    }
}