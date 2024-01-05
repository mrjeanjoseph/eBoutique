using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ImportExcelFile.Controllers {
    public static class Extensions {
        public static DataTable ToDataTable<T>(this List<T> dtItems) {
            DataTable dt = new DataTable(typeof(T).Name);

            //Get all the properties  
            PropertyInfo[] localProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in localProperties) { 
                var t = (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(p.PropertyType) : p.PropertyType);
                //Setting column names as Property names  
                dt.Columns.Add(p.Name, t);
            }
            foreach (T item in dtItems) {
                var values = new object[localProperties.Length];
                for (int i = 0; i < localProperties.Length; i++) {
                    //inserting property values to datatable rows  
                    values[i] = localProperties[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
}