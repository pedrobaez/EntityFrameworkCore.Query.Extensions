using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Query.Extensions.Query
{
    internal static class Materializer
    {
        /// <summary>
        /// Materialize record using reflection, mapping to specific types
        /// </summary>
        /// <param name="types"></param>
        /// <param name="counter"></param>
        /// <param name="reader"></param>
        /// <param name="resultSetValues"></param>
        public static void MaterializeRecord(Type[] types, int counter, DbDataReader reader, IList? resultSetValues)
        {
            var item = Activator.CreateInstance(types[counter]);

            for (int inc = 0; inc < reader.FieldCount; inc++)
            {
                Type type = item.GetType();
                string name = reader.GetName(inc);
                PropertyInfo property = type.GetProperty(name);

                if (property != null && name == property.Name)
                {
                    var value = reader.GetValue(inc);
                    if (value != null && value != DBNull.Value)
                    {
                        property.SetValue(item, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType), null);
                    }
                }
            }
            resultSetValues.Add(item);
        }
    }
}
