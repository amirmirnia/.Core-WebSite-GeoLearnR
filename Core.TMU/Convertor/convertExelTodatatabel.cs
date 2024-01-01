using System.IO;
using ExcelDataReader;
using ExcelDataReader.Exceptions;
using System.Data;

namespace Core.TMU.Convertor
{
    public class convertExelTodatatabel
    {

        public static DataTable MergeExcelFiles(string[] filePaths)
        {
            var dt = new DataTable();
            foreach (var filePath in filePaths)
            {
                try
                {
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                UseColumnDataType = true,
                                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });

                            dt.Merge(result.Tables[0]);
                        }
                    }
                }
                catch (HeaderException)
                {
                    // Ignore invalid headers
                }
            }

            return dt;
        }
    }
}
