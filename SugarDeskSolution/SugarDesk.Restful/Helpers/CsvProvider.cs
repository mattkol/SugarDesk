// -----------------------------------------------------------------------
// <copyright file="CsvProvider.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Helpers
{
    using CsvHelper;
    using System.Data;
    using System.IO;

    public class CsvProvider
    {
        public static bool Import(out DataTable table, string filePath)
        {
            table = new DataTable();

            if (!filePath.FileIsValid())
            {
                return false;
            }

            using (var streamReader = new StreamReader(filePath))
            {
                var reader = new CsvReader(streamReader);

                while (reader.Read())
                {
                    var row = table.NewRow();
                    foreach (DataColumn column in table.Columns)
                    {
                        row[column.ColumnName] = reader.GetField(column.DataType, column.ColumnName);
                    }
                    table.Rows.Add(row);
                }
            }

            return ((table.Rows != null) && (table.Rows.Count > 0));
        }

        public static bool Export(DataTable table, string filePath)
        {
            if (table == null)
            {
                return false;
            }

            using (var streamWriter = new StreamWriter(filePath))
            {
                var writer = new CsvWriter(streamWriter);

                using (table)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        writer.WriteField(column.ColumnName);
                    }

                    writer.NextRecord();

                    foreach (DataRow row in table.Rows)
                    {
                        for (var i = 0; i < table.Columns.Count; i++)
                        {
                            writer.WriteField(row[i]);
                        }
                        writer.NextRecord();
                    }
                }
            }

            return filePath.FileIsValid();
        }

    }
}
