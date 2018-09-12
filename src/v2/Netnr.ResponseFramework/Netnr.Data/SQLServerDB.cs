using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Netnr.Data
{
    public class SQLServerDB
    {
        #region 数据库连接字符串（可自定义，默认取配置的值）
        private static string _sqlConn;
        public static string SqlConn
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_sqlConn))
                {
                    _sqlConn = GlobalVar.GetValue("ConnectionStrings:SQLServerConn");
                }
                return _sqlConn;
            }
            set => _sqlConn = value;
        }
        #endregion

        #region 查询 数据 （返回 DataSet DataTable DataReader）

        /// <summary>
        /// 查询数据集 返回DataSet
        /// </summary>
        /// <param name="cmdTxt">SQL语句</param>
        /// <param name="cmdParams">SQL参数组</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdTxt, SqlParameter[] cmdParams = null)
        {
            using (SqlConnection conn = new SqlConnection(SqlConn))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmdTxt, conn);
                if (cmdParams != null)
                {
                    foreach (SqlParameter p in cmdParams)
                    {
                        sda.SelectCommand.Parameters.Add(p);
                    }
                }
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// 查询数据集 返回DataTable
        /// </summary>
        /// <param name="cmdTxt">SQL语句</param>
        /// <param name="cmdParams">SQL参数组</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string cmdTxt, SqlParameter[] cmdParams = null)
        {
            return GetDataSet(cmdTxt, cmdParams).Tables[0];
        }

        /// <summary>
        /// 查询返回 DataReader
        /// </summary>
        /// <param name="cmdTxt">SQL语句</param>
        /// <param name="cmdParams">SQL参数组</param>
        /// <returns></returns>
        public static SqlDataReader GetDataReader(string cmdTxt, SqlParameter[] cmdParams = null)
        {
            using (SqlConnection conn = new SqlConnection(SqlConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdTxt, conn);
                if (cmdParams != null)
                {
                    foreach (SqlParameter p in cmdParams)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                SqlDataReader Reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                conn.Close();
                return Reader;
            }
        }

        /// <summary>
        /// 返回第一行第一列的值 类型为 object
        /// </summary>
        /// <param name="cmdTxt">SQL命令</param>
        /// <param name="cmdParams">SQL参数组</param>
        /// <returns>返回第一行第一列的值 object </returns>
        public static object GetScalar(string cmdTxt, SqlParameter[] cmdParams = null)
        {
            using (SqlConnection conn = new SqlConnection(SqlConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdTxt, conn);
                if (cmdParams != null)
                {
                    foreach (SqlParameter p in cmdParams)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                object obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                conn.Close();
                return obj;
            }
        }

        #endregion

        #region 增、删、改

        /// <summary>
        /// 执行 SQL 语句 返回受影响行数
        /// </summary>
        /// <param name="cmdTxt">SQL语句</param>
        /// <param name="cmdParams">SQL 参数组</param>
        public static int ExecuteNonQuery(string cmdTxt, SqlParameter[] cmdParams = null)
        {
            using (SqlConnection conn = new SqlConnection(SqlConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdTxt, conn);
                if (cmdParams != null)
                {
                    foreach (SqlParameter p in cmdParams)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                int count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                return count;
            }
        }

        /// <summary>
        /// DataTable批量插入数据库
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static bool SqlBulkCopyByDataTable(string TableName, DataTable dt)
        {
            bool result = false;
            using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(SqlConn, SqlBulkCopyOptions.UseInternalTransaction))
            {
                try
                {
                    sqlbulkcopy.DestinationTableName = TableName;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sqlbulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                    }
                    sqlbulkcopy.WriteToServer(dt);
                    result = true;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        #endregion

        #region 存储过程 DataSet、执行存储过程返回结果

        /// <summary>
        /// 执行存储过程 返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="procParams">存储过程所需参数组</param>
        /// <returns></returns>
        public static DataSet ExecProcDataSet(string procName, SqlParameter[] procParams = null)
        {
            using (SqlConnection conn = new SqlConnection(SqlConn))
            {
                SqlDataAdapter sda = new SqlDataAdapter(procName, conn);
                if (procParams != null)
                {
                    foreach (SqlParameter p in procParams)
                    {
                        sda.SelectCommand.Parameters.Add(p);
                    }
                }
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.SelectCommand.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// 执行存储过程返回Output值 
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="procParams">存储过程参数组</param>
        /// <param name="outputName">存储过程的output参数名称 默认 resultValue </param>
        /// <returns></returns>
        public static string ExecProcValue(string procName, SqlParameter[] procParams, string outputName = "resultValue")
        {
            using (SqlConnection conn = new SqlConnection(SqlConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(procName, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@" + outputName, SqlDbType.VarChar, 100));
                cmd.Parameters["@" + outputName].Direction = ParameterDirection.Output;
                if (procParams != null)
                {
                    foreach (SqlParameter p in procParams)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                cmd.ExecuteNonQuery();
                string result = cmd.Parameters["@" + outputName].Value.ToString();
                conn.Close();
                cmd.Parameters.Clear();
                return result;
            }
        }


        #endregion
    }
}
