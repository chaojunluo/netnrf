using System;
using System.Data;
using System.Text;
using System.Data.SQLite;

namespace RF.DAL
{
	/// <summary>
	/// 数据访问类:sys_button
	/// </summary>
	public partial class sys_button
	{
		public sys_button()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DB.HelperSQLite.GetMaxID("id", "sys_button"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sys_button");
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
		public int Add(RF.Model.sys_button model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sys_button(");
			strSql.Append("b_title,b_id,b_class,b_icon,b_pid,b_state,b_order,b_remark)");
			strSql.Append(" values (");
			strSql.Append("@b_title,@b_id,@b_class,@b_icon,@b_pid,@b_state,@b_order,@b_remark)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@b_title", DbType.String,20),
					new SQLiteParameter("@b_id", DbType.String,40),
					new SQLiteParameter("@b_class", DbType.String,40),
					new SQLiteParameter("@b_icon", DbType.String,40),
					new SQLiteParameter("@b_pid", DbType.Int32,8),
					new SQLiteParameter("@b_state", DbType.Int32,4),
					new SQLiteParameter("@b_order", DbType.Int32,4),
					new SQLiteParameter("@b_remark", DbType.String,40)};
			parameters[0].Value = model.b_title;
			parameters[1].Value = model.b_id;
			parameters[2].Value = model.b_class;
			parameters[3].Value = model.b_icon;
			parameters[4].Value = model.b_pid;
			parameters[5].Value = model.b_state;
			parameters[6].Value = model.b_order;
			parameters[7].Value = model.b_remark;

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
		public bool Update(RF.Model.sys_button model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sys_button set ");
			strSql.Append("b_title=@b_title,");
			strSql.Append("b_id=@b_id,");
			strSql.Append("b_class=@b_class,");
			strSql.Append("b_icon=@b_icon,");
			strSql.Append("b_pid=@b_pid,");
			strSql.Append("b_state=@b_state,");
			strSql.Append("b_order=@b_order,");
			strSql.Append("b_remark=@b_remark");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@b_title", DbType.String,20),
					new SQLiteParameter("@b_id", DbType.String,40),
					new SQLiteParameter("@b_class", DbType.String,40),
					new SQLiteParameter("@b_icon", DbType.String,40),
					new SQLiteParameter("@b_pid", DbType.Int32,8),
					new SQLiteParameter("@b_state", DbType.Int32,4),
					new SQLiteParameter("@b_order", DbType.Int32,4),
					new SQLiteParameter("@b_remark", DbType.String,40),
					new SQLiteParameter("@id", DbType.Int32,8)};
			parameters[0].Value = model.b_title;
			parameters[1].Value = model.b_id;
			parameters[2].Value = model.b_class;
			parameters[3].Value = model.b_icon;
			parameters[4].Value = model.b_pid;
			parameters[5].Value = model.b_state;
			parameters[6].Value = model.b_order;
			parameters[7].Value = model.b_remark;
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
			strSql.Append("delete from sys_button ");
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
			strSql.Append("delete from sys_button ");
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
		public RF.Model.sys_button GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,b_title,b_id,b_class,b_icon,b_pid,b_state,b_order,b_remark from sys_button ");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			RF.Model.sys_button model=new RF.Model.sys_button();
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
		public RF.Model.sys_button DataRowToModel(DataRow row)
		{
			RF.Model.sys_button model=new RF.Model.sys_button();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["b_title"]!=null)
				{
					model.b_title=row["b_title"].ToString();
				}
				if(row["b_id"]!=null)
				{
					model.b_id=row["b_id"].ToString();
				}
				if(row["b_class"]!=null)
				{
					model.b_class=row["b_class"].ToString();
				}
				if(row["b_icon"]!=null)
				{
					model.b_icon=row["b_icon"].ToString();
				}
				if(row["b_pid"]!=null && row["b_pid"].ToString()!="")
				{
					model.b_pid=int.Parse(row["b_pid"].ToString());
				}
				if(row["b_state"]!=null && row["b_state"].ToString()!="")
				{
					model.b_state=int.Parse(row["b_state"].ToString());
				}
				if(row["b_order"]!=null && row["b_order"].ToString()!="")
				{
					model.b_order=int.Parse(row["b_order"].ToString());
				}
				if(row["b_remark"]!=null)
				{
					model.b_remark=row["b_remark"].ToString();
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
			strSql.Append("select id,b_title,b_id,b_class,b_icon,b_pid,b_state,b_order,b_remark ");
			strSql.Append(" FROM sys_button ");
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
			parameters[0].Value = "sys_button";
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

