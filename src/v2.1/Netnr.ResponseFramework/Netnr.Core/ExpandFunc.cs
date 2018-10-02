using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

public static class ExpandFunc
{
    #region object ⇋ json

    /// <summary>
    /// object 转 JSON 字符串
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="DateTimeFormat">时间格式化</param>
    /// <returns></returns>
    public static string ToJson(this object obj, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
    {
        Newtonsoft.Json.Converters.IsoDateTimeConverter dtFmt = new Newtonsoft.Json.Converters.IsoDateTimeConverter
        {
            DateTimeFormat = DateTimeFormat
        };
        return Newtonsoft.Json.JsonConvert.SerializeObject(obj, dtFmt);
    }

    /// <summary>
    /// 解析 JSON字符串 为JObject对象
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <returns>JObject对象</returns>
    public static Newtonsoft.Json.Linq.JObject ToJObject(this string json)
    {
        return Newtonsoft.Json.Linq.JObject.Parse(json);
    }

    #endregion

    #region JSON转义
    /// <summary>
    /// 字符串 JSON转义
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string OfJson(this string s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s.ToCharArray()[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\""); break;
                case '\\':
                    sb.Append("\\\\"); break;
                case '/':
                    sb.Append("\\/"); break;
                case '\b':
                    sb.Append("\\b"); break;
                case '\f':
                    sb.Append("\\f"); break;
                case '\n':
                    sb.Append("\\n"); break;
                case '\r':
                    sb.Append("\\r"); break;
                case '\t':
                    sb.Append("\\t"); break;
                default:
                    sb.Append(c); break;
            }
        }
        return sb.ToString();
    }

    #endregion

    #region 解析 JToken 里面的键转为字符串 null值返回空字符串

    /// <summary>
    /// 把jArray里面的json对象转为字符串
    /// </summary>
    /// <param name="jt"></param>
    /// <returns></returns>
    public static string ToStringOrEmpty(this Newtonsoft.Json.Linq.JToken jt)
    {
        try
        {
            return jt == null ? "" : jt.ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    #endregion

    #region 实体 ⇋ 表

    /// <summary>
    /// 实体转为表
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="list">对象</param>
    /// <returns></returns>
    public static DataTable ToDataTable<T>(this List<T> list)
    {
        Type elementType = typeof(T);
        var t = new DataTable();
        elementType.GetProperties().ToList().ForEach(propInfo => t.Columns.Add(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType));
        foreach (T item in list)
        {
            var row = t.NewRow();
            elementType.GetProperties().ToList().ForEach(propInfo => row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value);
            t.Rows.Add(row);
        }
        return t;
    }

    /// <summary>
    /// 表转为实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static List<T> ToModel<T>(this DataTable table) where T : class, new()
    {
        var list = new List<T>();
        foreach (DataRow dr in table.Rows)
        {
            var model = new T();
            foreach (DataColumn dc in dr.Table.Columns)
            {
                object drValue = dr[dc.ColumnName];

                var pi = model.GetType().GetProperties().Where(x => x.Name.ToLower() == dc.ColumnName.ToLower()).FirstOrDefault();

                Type type = pi.PropertyType;
                if (pi.PropertyType.FullName.Contains("System.Nullable"))
                {
                    type = Type.GetType("System." + pi.PropertyType.FullName.Split(',')[0].Split('.')[2]);
                }

                if (pi != null && pi.CanWrite && (drValue != null && !Convert.IsDBNull(drValue)))
                {
                    try
                    {
                        drValue = Convert.ChangeType(drValue, type);
                        pi.SetValue(model, drValue, null);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            list.Add(model);
        }
        return list;
    }

    #endregion

    #region SQL值单引号转移
    /// <summary>
    /// SQL单引号转义
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string OfSql(this string s)
    {
        return s.Replace("'", "''");
    }

    #endregion

    #region Ascii、Unicode 编码转换

    /// <summary>
    /// 字符串 Unicode编码
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToUnicode(this string s)
    {
        string val = "";
        byte[] bytes = Encoding.Unicode.GetBytes(s);
        for (int i = 0; i < bytes.Length; i++)
        {
            val += @"\u" + bytes[i + 1].ToString("x2") + bytes[i].ToString("x2");
            i = bytes.Length - i > 1 ? i += 1 : i;
        }
        return val;
    }

    /// <summary>
    /// Unicode 转码字符串
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToUnUnicode(this string s)
    {
        try
        {
            string val = "";
            string[] sts = s.Remove(0, 2).Replace("\\", "").Split('u');
            for (int i = 0; i < sts.Length; i++)
            {
                val += (char)int.Parse(sts[i], System.Globalization.NumberStyles.HexNumber);
            }
            return val;
        }
        catch (Exception)
        {
            return s;
        }
    }

    /// <summary>
    /// 字符串 Ascii编码
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToAscii(this string s)
    {
        string val = "";
        char[] chs = s.ToCharArray();
        for (int i = 0; i < chs.Length; i++)
        {
            val += "&#" + (int)chs[i] + ";";
        }
        return val;
    }

    /// <summary>
    /// Ascii 转字符串
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToUnAscii(this string s)
    {
        try
        {
            string val = "";
            string[] sts = s.Remove(s.Length - 1, 1).Replace("&#", "").Split(';');
            for (int i = 0; i < sts.Length; i++)
            {
                val += ((char)Convert.ToInt32(sts[i])).ToString();
            }
            return val;
        }
        catch (Exception)
        {
            return s;
        }
    }

    #endregion
}
