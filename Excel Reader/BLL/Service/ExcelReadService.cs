using ClosedXML.Excel;
using DAL.DataBase;
using System.Reflection;

namespace BLL.Service
{
    public class ExcelMapper
    {
        public static List<T> MapExcelToObjects<T>(string filePath) where T : new()
        {
            var result = new List<T>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); 

                var columnMapping = BuildColumnMapping(worksheet);

                foreach (var row in worksheet.RowsUsed().Skip(1)) 
                {
                    var item = MapRowToObject<T>(row, columnMapping);
                    result.Add(item);
                }
            }

            return result;
        }

        private static T MapRowToObject<T>(IXLRow row, Dictionary<string, int> columnMapping) where T : new()
        {
            var item = new T();
            var type = typeof(T);

            foreach (var mapping in columnMapping)
            {
                var columnName = mapping.Key;
                var columnIndex = mapping.Value;

                var property = type.GetProperty(columnName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    ?? throw new Exception($"Property '{columnName}' not found in class '{type.Name}'.");

                if (!property.CanWrite && !IsInitOnly(property))
                {
                    throw new Exception($"Property '{columnName}' is read-only or has no setter.");
                }

                var cellValue = row.Cell(columnIndex).GetValue<string>();

                var convertedValue = Convert.ChangeType(cellValue, property.PropertyType);

                property.SetValue(item, convertedValue);
            }

            return item;
        }

        private static bool IsInitOnly(PropertyInfo property)
        {
            return property.SetMethod != null &&
                   property.SetMethod.ReturnParameter.GetRequiredCustomModifiers()
                   .Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));
        }

        private static Dictionary<string, int> BuildColumnMapping(IXLWorksheet worksheet)
        {
            var columnMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var cell in worksheet.Row(1).Cells())
            {
                columnMapping[cell.Value.ToString().Trim()] = cell.Address.ColumnNumber;
            }

            return columnMapping;
        }
    }
}