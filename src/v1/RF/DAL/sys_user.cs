using System;
using System.Data;
using System.Text;
using System.Data.SQLite;

namespace RF.DAL
{
	/// <summary>
	/// 数据访问类:sys_user
	/// </summary>
	public partial class sys_user
	{
		public sys_user()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sys_user");
			strSql.Append(" where id=@id ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.String,40)			};
			parameters[0].Value = id;

			return DB.HelperSQLite.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(RF.Model.sys_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_user(");
            strSql.Append("id,u_name,u_pwd,u_roleid,u_nickname,u_sign,u_photo,u_state,u_date)");
            strSql.Append(" values (");
            strSql.Append("@id,@u_name,@u_pwd,@u_roleid,@u_nickname,@u_sign,@u_photo,@u_state,@u_date)");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.String,40),
					new SQLiteParameter("@u_name", DbType.String,40),
					new SQLiteParameter("@u_pwd", DbType.String,40),
					new SQLiteParameter("@u_roleid", DbType.String,40),
					new SQLiteParameter("@u_nickname", DbType.String,40),
					new SQLiteParameter("@u_sign", DbType.String,40),
					new SQLiteParameter("@u_photo", DbType.String,100),
					new SQLiteParameter("@u_state", DbType.Int32,4),
					new SQLiteParameter("@u_date", DbType.DateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.u_name;
            parameters[2].Value = model.u_pwd;
            parameters[3].Value = model.u_roleid;
            parameters[4].Value = model.u_nickname;
            parameters[5].Value = model.u_sign;
            parameters[6].Value = model.u_photo;
            parameters[7].Value = model.u_state;
            parameters[8].Value = model.u_date;

            int rows = DB.HelperSQLite.ExecuteSql(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(RF.Model.sys_user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_user set ");
            strSql.Append("u_name=@u_name,");
            strSql.Append("u_pwd=@u_pwd,");
            strSql.Append("u_roleid=@u_roleid,");
            strSql.Append("u_nickname=@u_nickname,");
            strSql.Append("u_sign=@u_sign,");
            strSql.Append("u_photo=@u_photo,");
            strSql.Append("u_state=@u_state,");
            strSql.Append("u_date=@u_date");
            strSql.Append(" where id=@id ");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@u_name", DbType.String,40),
					new SQLiteParameter("@u_pwd", DbType.String,40),
					new SQLiteParameter("@u_roleid", DbType.String,40),
					new SQLiteParameter("@u_nickname", DbType.String,40),
					new SQLiteParameter("@u_sign", DbType.String,40),
					new SQLiteParameter("@u_photo", DbType.String,100),
					new SQLiteParameter("@u_state", DbType.Int32,4),
					new SQLiteParameter("@u_date", DbType.DateTime),
					new SQLiteParameter("@id", DbType.String,40)};
            parameters[0].Value = model.u_name;
            parameters[1].Value = model.u_pwd;
            parameters[2].Value = model.u_roleid;
            parameters[3].Value = model.u_nickname;
            parameters[4].Value = model.u_sign;
            parameters[5].Value = model.u_photo;
            parameters[6].Value = model.u_state;
            parameters[7].Value = model.u_date;
            parameters[8].Value = model.id;

            int rows = DB.HelperSQLite.ExecuteSql(strSql.ToString(), parameters);
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
		public bool Delete(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sys_user ");
			strSql.Append(" where id=@id ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.String,40)			};
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
			strSql.Append("delete from sys_user ");
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
		public RF.Model.sys_user GetModel(string id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,u_name,u_pwd,u_roleid,u_nickname,u_sign,u_photo,u_state from sys_user ");
			strSql.Append(" where id=@id ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.String,40)			};
			parameters[0].Value = id;

			RF.Model.sys_user model=new RF.Model.sys_user();
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
		public RF.Model.sys_user DataRowToModel(DataRow row)
		{
			RF.Model.sys_user model=new RF.Model.sys_user();
			if (row != null)
			{
				if(row["id"]!=null)
				{
					model.id=row["id"].ToString();
				}
				if(row["u_name"]!=null)
				{
					model.u_name=row["u_name"].ToString();
				}
				if(row["u_pwd"]!=null)
				{
					model.u_pwd=row["u_pwd"].ToString();
				}
				if(row["u_roleid"]!=null)
				{
					model.u_roleid=row["u_roleid"].ToString();
				}
				if(row["u_nickname"]!=null)
				{
					model.u_nickname=row["u_nickname"].ToString();
				}
				if(row["u_sign"]!=null)
				{
					model.u_sign=row["u_sign"].ToString();
				}
				if(row["u_photo"]!=null)
				{
					model.u_photo=row["u_photo"].ToString();
				}
				if(row["u_state"]!=null && row["u_state"].ToString()!="")
				{
					model.u_state=int.Parse(row["u_state"].ToString());
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
			strSql.Append("select id,u_name,u_pwd,u_roleid,u_nickname,u_sign,u_photo,u_state ");
			strSql.Append(" FROM sys_user ");
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
			parameters[0].Value = "sys_user";
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

