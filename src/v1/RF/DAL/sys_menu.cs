using System;
using System.Data;
using System.Text;
using System.Data.SQLite;

namespace RF.DAL
{
	/// <summary>
	/// 数据访问类:sys_menu
	/// </summary>
	public partial class sys_menu
	{
		public sys_menu()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DB.HelperSQLite.GetMaxID("id", "sys_menu"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sys_menu");
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
		public int Add(RF.Model.sys_menu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sys_menu(");
			strSql.Append("m_name,m_url,m_pid,m_order,m_icon,m_state,m_button)");
			strSql.Append(" values (");
			strSql.Append("@m_name,@m_url,@m_pid,@m_order,@m_icon,@m_state,@m_button)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@m_name", DbType.String),
					new SQLiteParameter("@m_url", DbType.String,200),
					new SQLiteParameter("@m_pid", DbType.Int32,8),
					new SQLiteParameter("@m_order", DbType.Int32,4),
					new SQLiteParameter("@m_icon", DbType.String,50),
					new SQLiteParameter("@m_state", DbType.Int32,4),
					new SQLiteParameter("@m_button", DbType.String,1000)};
			parameters[0].Value = model.m_name;
			parameters[1].Value = model.m_url;
			parameters[2].Value = model.m_pid;
			parameters[3].Value = model.m_order;
			parameters[4].Value = model.m_icon;
			parameters[5].Value = model.m_state;
			parameters[6].Value = model.m_button;

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
		public bool Update(RF.Model.sys_menu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sys_menu set ");
			strSql.Append("m_name=@m_name,");
			strSql.Append("m_url=@m_url,");
			strSql.Append("m_pid=@m_pid,");
			strSql.Append("m_order=@m_order,");
			strSql.Append("m_icon=@m_icon,");
			strSql.Append("m_state=@m_state,");
			strSql.Append("m_button=@m_button");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@m_name", DbType.String),
					new SQLiteParameter("@m_url", DbType.String,200),
					new SQLiteParameter("@m_pid", DbType.Int32,8),
					new SQLiteParameter("@m_order", DbType.Int32,4),
					new SQLiteParameter("@m_icon", DbType.String,50),
					new SQLiteParameter("@m_state", DbType.Int32,4),
					new SQLiteParameter("@m_button", DbType.String,1000),
					new SQLiteParameter("@id", DbType.Int32,8)};
			parameters[0].Value = model.m_name;
			parameters[1].Value = model.m_url;
			parameters[2].Value = model.m_pid;
			parameters[3].Value = model.m_order;
			parameters[4].Value = model.m_icon;
			parameters[5].Value = model.m_state;
			parameters[6].Value = model.m_button;
			parameters[7].Value = model.id;

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
			strSql.Append("delete from sys_menu ");
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
			strSql.Append("delete from sys_menu ");
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
		public RF.Model.sys_menu GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,m_name,m_url,m_pid,m_order,m_icon,m_state,m_button from sys_menu ");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			RF.Model.sys_menu model=new RF.Model.sys_menu();
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
		public RF.Model.sys_menu DataRowToModel(DataRow row)
		{
			RF.Model.sys_menu model=new RF.Model.sys_menu();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["m_name"]!=null)
				{
					model.m_name=row["m_name"].ToString();
				}
				if(row["m_url"]!=null)
				{
					model.m_url=row["m_url"].ToString();
				}
				if(row["m_pid"]!=null && row["m_pid"].ToString()!="")
				{
					model.m_pid=int.Parse(row["m_pid"].ToString());
				}
				if(row["m_order"]!=null && row["m_order"].ToString()!="")
				{
					model.m_order=int.Parse(row["m_order"].ToString());
				}
				if(row["m_icon"]!=null)
				{
					model.m_icon=row["m_icon"].ToString();
				}
				if(row["m_state"]!=null && row["m_state"].ToString()!="")
				{
					model.m_state=int.Parse(row["m_state"].ToString());
				}
				if(row["m_button"]!=null)
				{
					model.m_button=row["m_button"].ToString();
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
			strSql.Append("select id,m_name,m_url,m_pid,m_order,m_icon,m_state,m_button ");
			strSql.Append(" FROM sys_menu ");
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
			parameters[0].Value = "sys_menu";
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

