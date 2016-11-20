// -----------------------------------------------------------------------
// <copyright file="ExcelProvider.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Helpers
{
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using System;
    using System.Data;
    using System.IO;

    public class ExcelProvider
    {
        public static bool Import(out DataTable  table, string filePath)
        {
            table = new DataTable();

            if (!filePath.FileIsValid()) 
            {
                return false;
            }

            XSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new XSSFWorkbook(file);
            }

            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                table.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                if (row == null)
                {
                    break;
                }
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            return ((table.Rows != null) && (table.Rows.Count > 0)); 
        }

        public static bool Export(DataTable table, string filePath)
        {
            if (table == null)
            {
                return false;
            }

            using (table)
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                IRow headerRow = sheet.CreateRow(0);
                foreach (DataColumn column in table.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                    sheet.AutoSizeColumn(column.Ordinal);
                }

                ICellStyle dateTimeStyle = DateTimeFormat(workbook);

                int rowIndex = 1;
                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in table.Columns)
                    {

                        var cell = dataRow.CreateCell(column.Ordinal);

                        Type type = row[column].GetType();
                        switch (type.ToString())
                        {
                            case "System.Boolean":
                                {
                                    bool boolValue = default(bool);
                                    if (row[column] != null)
                                    {
                                        bool.TryParse(row[column].ToString(), out boolValue);
                                    }
                                    cell.SetCellValue(boolValue);
                                }
                                break;
                            case "System.Double":
                                {
                                    double doubleValue = default(double);
                                    if (row[column] != null)
                                    {
                                        double.TryParse(row[column].ToString(), out doubleValue);
                                    }
                                    cell.SetCellValue(doubleValue);
                                }
                                break;
                            case "System.DateTime":
                                {
                                    DateTime dateValue = DateTime.MinValue;
                                    if (row[column] != null)
                                    {
                                        DateTime.TryParse(row[column].ToString(), out dateValue);
                                    }
                                    cell.SetCellValue(dateValue);
                                    cell.CellStyle = dateTimeStyle;
                                    sheet.SetColumnWidth(column.Ordinal, (sheet.GetColumnWidth(column.Ordinal) + 100));
                                }
                                break;

                            default:
                                {
                                    if (row[column] != null)
                                    {
                                        cell.SetCellValue(row[column].ToString());
                                    }
                                    else
                                    {
                                        cell.SetCellValue(string.Empty);
                                    }
                                }
                                break;
                        }
                    }
                    rowIndex++;
                }

                FileStream fileStream = File.Create(filePath);
                workbook.Write(fileStream);
                fileStream.Close();

                return filePath.FileIsValid();
            }
        }

        private static ICellStyle DateTimeFormat(IWorkbook workbook)
        {
            var dateTimeFormat = workbook.CreateDataFormat();
            var style = workbook.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.DataFormat = dateTimeFormat.GetFormat("MM/dd/yyyy HH:mm:ss");

            return style;
        }
    }
}
