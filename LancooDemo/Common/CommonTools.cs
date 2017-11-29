using System;
using System.Collections.Generic;
using System.Data;

namespace LancooDemo.Common
{
    public static class CommonTools
    {
        /// <summary>
        /// 解析从数据库获取的数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Value<T>(this DataRow row, string columnName, T defaultValue)
        {
            if (row[columnName] == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return row.Field<T>(columnName);
                }
                catch (Exception ex)
                {
                    string msg = "columnName=" + columnName + ",value=" + row[columnName].ToString()
                        + ",type=" + row[columnName].GetType();
                    throw new Exception(msg, ex);
                }
            }
        }


        public static T Value<T>(this DataRow row, int index, T defaultValue)
        {
            if (row[index] == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return row.Field<T>(index);
                }
                catch (Exception ex)
                {
                    string msg = "index=" + index + ",value=" + row[index].ToString()
                        + ",type=" + row[index].GetType();
                    throw new InvalidCastException(msg, ex);
                }
            }
        }


        public static int DayTime(this DateTime self)
        {
            return Convert.ToInt32(self.ToString("yyyyMMdd"));
        }



        public static List<K> KeyList<K, V>(this Dictionary<K, V> self)
        {
            if (self == null)
                throw new ArgumentNullException();

            List<K> list = new List<K>();
            foreach (K key in self.Keys)
                list.Add(key);

            return list;
        }

        public static List<V> ValueList<K, V>(this Dictionary<K, V> self)
        {
            if (self == null)
                throw new ArgumentNullException();

            List<V> list = new List<V>();

            foreach (V value in self.Values)
                list.Add(value);
            return list;
        }


        public static T[] Copy<T>(this T[] self)
        {
            T[] t = new T[self.Length];
            for (int i = 0; i < self.Length; i++)
                t[i] = self[i];
            return t;
        }
    }
}