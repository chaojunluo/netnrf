using System;
using System.Data;
using System.Text;
using System.Data.SQLite;

namespace RF.DAL
{
	/// <summary>
	/// 数据访问类:sys_log
	/// </summary>
	public partial class sys_log
	{
		public sys_log()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DB.HelperSQLite.GetMaxID("id", "sys_log"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sys_log");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			return DB.HelperSQLite.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(RF.Model.sys_log model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sys_log(");
			strSql.Append("l_user,l_module,l_action,l_datetime,l_content,l_ip,l_url,l_spare)");
			strSql.Append(" values (");
			strSql.Append("@l_user,@l_module,@l_action,@l_datetime,@l_content,@l_ip,@l_url,@l_spare)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@l_user", DbType.String,40),
					new SQLiteParameter("@l_module", DbType.String,40),
					new SQLiteParameter("@l_action", DbType.String,40),
					new SQLiteParameter("@l_datetime", DbType.DateTime),
					new SQLiteParameter("@l_content", DbType.String),
					new SQLiteParameter("@l_ip", DbType.String,40),
					new SQLiteParameter("@l_url", DbType.String,200),
					new SQLiteParameter("@l_spare", DbType.String,40)};
			parameters[0].Value = model.l_user;
			parameters[1].Value = model.l_module;
			parameters[2].Value = model.l_action;
			parameters[3].Value = model.l_datetime;
			parameters[4].Value = model.l_content;
			parameters[5].Value = model.l_ip;
			parameters[6].Value = model.l_url;
			parameters[7].Value = model.l_spare;

			object obj = DB.HelperSQLite.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(RF.Model.sys_log model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sys_log set ");
			strSql.Append("l_user=@l_user,");
			strSql.Append("l_module=@l_module,");
			strSql.Append("l_action=@l_action,");
			strSql.Append("l_datetime=@l_datetime,");
			strSql.Append("l_content=@l_content,");
			strSql.Append("l_ip=@l_ip,");
			strSql.Append("l_url=@l_url,");
			strSql.Append("l_spare=@l_spare");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@l_user", DbType.String,40),
					new SQLiteParameter("@l_module", DbType.String,40),
					new SQLiteParameter("@l_action", DbType.String,40),
					new SQLiteParameter("@l_datetime", DbType.DateTime),
					new SQLiteParameter("@l_content", DbType.String),
					new SQLiteParameter("@l_ip", DbType.String,40),
					new SQLiteParameter("@l_url", DbType.String,200),
					new SQLiteParameter("@l_spare", DbType.String,40),
					new SQLiteParameter("@id", DbType.Int32,8)};
			parameters[0].Value = model.l_user;
			parameters[1].Value = model.l_module;
			parameters[2].Value = model.l_action;
			parameters[3].Value = model.l_datetime;
			parameters[4].Value = model.l_content;
			parameters[5].Value = model.l_ip;
			parameters[6].Value = model.l_url;
			parameters[7].Value = model.l_spare;
			parameters[8].Value = model.id;

			int rows=DB.HelperSQLite.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sys_log ");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			int rows=DB.HelperSQLite.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sys_log ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DB.HelperSQLite.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public RF.Model.sys_log GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,l_user,l_module,l_action,l_datetime,l_content,l_ip,l_url,l_spare from sys_log ");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			RF.Model.sys_log model=new RF.Model.sys_log();
			DataSet ds=DB.HelperSQLite.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public RF.Model.sys_log DataRowToModel(DataRow row)
		{
			RF.Model.sys_log model=new RF.Model.sys_log();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["l_user"]!=null)
				{
					model.l_user=row["l_user"].ToString();
				}
				if(row["l_module"]!=null)
				{
					model.l_module=row["l_module"].ToString();
				}
				if(row["l_action"]!=null)
				{
					model.l_action=row["l_action"].ToString();
				}
				if(row["l_datetime"]!=null && row["l_datetime"].ToString()!="")
				{
					model.l_datetime=DateTime.Parse(row["l_datetime"].ToString());
				}
				if(row["l_content"]!=null)
				{
					model.l_content=row["l_content"].ToString();
				}
				if(row["l_ip"]!=null)
				{
					model.l_ip=row["l_ip"].ToString();
				}
				if(row["l_url"]!=null)
				{
					model.l_url=row["l_url"].ToString();
				}
				if(row["l_spare"]!=null)
				{
					model.l_spare=row["l_spare"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,l_user,l_module,l_action,l_datetime,l_content,l_ip,l_url,l_spare ");
			strSql.Append(" FROM sys_log ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DB.HelperSQLite.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@tblName", DbType.VarChar, 255),
					new SQLiteParameter("@fldName", DbType.VarChar, 255),
					new SQLiteParameter("@PageSize", DbType.Int32),
					new SQLiteParameter("@PageIndex", DbType.Int32),
					new SQLiteParameter("@IsReCount", DbType.bit),
					new SQLiteParameter("@OrderType", DbType.bit),
					new SQLiteParameter("@strWhere", DbType.VarChar,1000),
					};
			parameters[0].Value = "sys_log";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DB.HelperSQLite.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

