using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soporte.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    public class ConvertData
    {
        /// <summary>
        /// Convierte una lista a un DataTable
        /// </summary>
        /// <typeparam name="T">Tipo Generico</typeparam>
        /// <param name="datos">Lista de datos</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// Convierte un datatabel eun una collection de datos especifico
        /// </summary>
        /// <typeparam name="T">Tipo Generico</typeparam>
        /// <param name="datos">Datatable de datos</param>
        /// <returns></returns>
        public static List<T> ConvertirDtoToList<T>(DataTable datos) where T : class, new()
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var dataList = new List<T>(datos.Rows.Count);

            if (datos != null && datos.Rows.Count > 0)
            {
                var objFieldNames = (from PropertyInfo aProp in typeof(T).GetProperties(flags)
                                     select new { Name = aProp.Name, Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType }).ToList();

                var dataTblFieldNames = (from DataColumn aHeader in datos.Columns
                                         select new { Name = aHeader.ColumnName, Type = aHeader.DataType }).ToList();

                var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

                foreach (DataRow dataRow in datos.AsEnumerable().ToList())
                {
                    var aTSource = new T();
                    foreach (var aField in commonFields)
                    {
                        PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                        var value = (dataRow[aField.Name] == DBNull.Value) ? null : dataRow[aField.Name];
                        propertyInfos.SetValue(aTSource, value, null);
                    }

                    dataList.Add(aTSource);
                }
            }
            else
            {
                dataList = new List<T>();
            }

            return dataList;
        }

    }
}
