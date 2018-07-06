using System;
using System.Data;
using System.Text;
using System.Data.SQLite;

namespace RF.DAL
{
	/// <summary>
	/// 数据访问类:sys_role
	/// </summary>
	public partial class sys_role
	{
		public sys_role()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DB.HelperSQLite.GetMaxID("id", "sys_role"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sys_role");
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
		public int Add(RF.Model.sys_role model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sys_role(");
			strSql.Append("r_name,r_state,r_remark,r_menus,r_buttons)");
			strSql.Append(" values (");
			strSql.Append("@r_name,@r_state,@r_remark,@r_menus,@r_buttons)");
			strSql.Append(";select LAST_INSERT_ROWID()");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@r_name", DbType.String,20),
					new SQLiteParameter("@r_state", DbType.Int32,4),
					new SQLiteParameter("@r_remark", DbType.String,100),
					new SQLiteParameter("@r_menus", DbType.String),
					new SQLiteParameter("@r_buttons", DbType.String)};
			parameters[0].Value = model.r_name;
			parameters[1].Value = model.r_state;
			parameters[2].Value = model.r_remark;
			parameters[3].Value = model.r_menus;
			parameters[4].Value = model.r_buttons;

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
		public bool Update(RF.Model.sys_role model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sys_role set ");
			strSql.Append("r_name=@r_name,");
			strSql.Append("r_state=@r_state,");
			strSql.Append("r_remark=@r_remark");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@r_name", DbType.String,20),
					new SQLiteParameter("@r_state", DbType.Int32,4),
					new SQLiteParameter("@r_remark", DbType.String,100),
					new SQLiteParameter("@id", DbType.Int32,8)};
			parameters[0].Value = model.r_name;
			parameters[1].Value = model.r_state;
			parameters[2].Value = model.r_remark;
			parameters[3].Value = model.id;

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
			strSql.Append("delete from sys_role ");
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
			strSql.Append("delete from sys_role ");
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
		public RF.Model.sys_role GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,r_name,r_state,r_remark,r_menus,r_buttons from sys_role ");
			strSql.Append(" where id=@id");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			RF.Model.sys_role model=new RF.Model.sys_role();
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
		public RF.Model.sys_role DataRowToModel(DataRow row)
		{
			RF.Model.sys_role model=new RF.Model.sys_role();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["r_name"]!=null)
				{
					model.r_name=row["r_name"].ToString();
				}
				if(row["r_state"]!=null && row["r_state"].ToString()!="")
				{
					model.r_state=int.Parse(row["r_state"].ToString());
				}
				if(row["r_remark"]!=null)
				{
					model.r_remark=row["r_remark"].ToString();
				}
				if(row["r_menus"]!=null)
				{
					model.r_menus=row["r_menus"].ToString();
				}
				if(row["r_buttons"]!=null)
				{
					model.r_buttons=row["r_buttons"].ToString();
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
			strSql.Append("select id,r_name,r_state,r_remark,r_menus,r_buttons ");
			strSql.Append(" FROM sys_role ");
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
			parameters[0].Value = "sys_role";
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

