using System;
using System.Data;
using System.Text;
using System.Data.SQLite;

namespace RF.DAL
{
    /// <summary>
    /// 数据访问类:sys_column
    /// </summary>
    public partial class sys_column
    {
        public sys_column()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(RF.Model.sys_column model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_column(");
            strSql.Append("vname,field,d_title,title,d_width,width,d_align,align,d_hide,hide,l_order,frozen,format,sort,f_type,f_url,d_f_area,f_area,d_f_col,f_col,f_order,d_f_hide,f_hide,f_required,f_placeholder,f_value,f_text)");
            strSql.Append(" values (");
            strSql.Append("@vname,@field,@d_title,@title,@d_width,@width,@d_align,@align,@d_hide,@hide,@l_order,@frozen,@format,@sort,@f_type,@f_url,@d_f_area,@f_area,@d_f_col,@f_col,@f_order,@d_f_hide,@f_hide,@f_required,@f_placeholder,@f_value,@f_text)");
            strSql.Append(";select LAST_INSERT_ROWID()");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@vname", DbType.String,100),
					new SQLiteParameter("@field", DbType.String,40),
					new SQLiteParameter("@d_title", DbType.String,40),
					new SQLiteParameter("@title", DbType.String,40),
					new SQLiteParameter("@d_width", DbType.Int32,4),
					new SQLiteParameter("@width", DbType.Int32,4),
					new SQLiteParameter("@d_align", DbType.Int32,4),
					new SQLiteParameter("@align", DbType.Int32,4),
					new SQLiteParameter("@d_hide", DbType.Int32,4),
					new SQLiteParameter("@hide", DbType.Int32,4),
					new SQLiteParameter("@l_order", DbType.Int32,4),
					new SQLiteParameter("@frozen", DbType.Int32,4),
					new SQLiteParameter("@format", DbType.String,100),
					new SQLiteParameter("@sort", DbType.Int32,4),
					new SQLiteParameter("@f_type", DbType.String,40),
					new SQLiteParameter("@f_url", DbType.String),
					new SQLiteParameter("@d_f_area", DbType.Int32,4),
					new SQLiteParameter("@f_area", DbType.Int32,4),
					new SQLiteParameter("@d_f_col", DbType.Int32,4),
					new SQLiteParameter("@f_col", DbType.Int32,4),
					new SQLiteParameter("@f_order", DbType.Int32,4),
					new SQLiteParameter("@d_f_hide", DbType.Int32,4),
					new SQLiteParameter("@f_hide", DbType.Int32,4),
					new SQLiteParameter("@f_required", DbType.Int32,4),
					new SQLiteParameter("@f_placeholder", DbType.String,40),
					new SQLiteParameter("@f_value", DbType.String,100),
					new SQLiteParameter("@f_text", DbType.String,100)};
            parameters[0].Value = model.vname;
            parameters[1].Value = model.field;
            parameters[2].Value = model.d_title;
            parameters[3].Value = model.title;
            parameters[4].Value = model.d_width;
            parameters[5].Value = model.width;
            parameters[6].Value = model.d_align;
            parameters[7].Value = model.align;
            parameters[8].Value = model.d_hide;
            parameters[9].Value = model.hide;
            parameters[10].Value = model.l_order;
            parameters[11].Value = model.frozen;
            parameters[12].Value = model.format;
            parameters[13].Value = model.sort;
            parameters[14].Value = model.f_type;
            parameters[15].Value = model.f_url;
            parameters[16].Value = model.d_f_area;
            parameters[17].Value = model.f_area;
            parameters[18].Value = model.d_f_col;
            parameters[19].Value = model.f_col;
            parameters[20].Value = model.f_order;
            parameters[21].Value = model.d_f_hide;
            parameters[22].Value = model.f_hide;
            parameters[23].Value = model.f_required;
            parameters[24].Value = model.f_placeholder;
            parameters[25].Value = model.f_value;
            parameters[26].Value = model.f_text;

            object obj = DB.HelperSQLite.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(RF.Model.sys_column model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_column set ");
            strSql.Append("vname=@vname,");
            strSql.Append("field=@field,");
            strSql.Append("d_title=@d_title,");
            strSql.Append("title=@title,");
            strSql.Append("d_width=@d_width,");
            strSql.Append("width=@width,");
            strSql.Append("d_align=@d_align,");
            strSql.Append("align=@align,");
            strSql.Append("d_hide=@d_hide,");
            strSql.Append("hide=@hide,");
            strSql.Append("l_order=@l_order,");
            strSql.Append("frozen=@frozen,");
            strSql.Append("format=@format,");
            strSql.Append("sort=@sort,");
            strSql.Append("f_type=@f_type,");
            strSql.Append("f_url=@f_url,");
            strSql.Append("d_f_area=@d_f_area,");
            strSql.Append("f_area=@f_area,");
            strSql.Append("d_f_col=@d_f_col,");
            strSql.Append("f_col=@f_col,");
            strSql.Append("f_order=@f_order,");
            strSql.Append("d_f_hide=@d_f_hide,");
            strSql.Append("f_hide=@f_hide,");
            strSql.Append("f_required=@f_required,");
            strSql.Append("f_placeholder=@f_placeholder,");
            strSql.Append("f_value=@f_value,");
            strSql.Append("f_text=@f_text");
            strSql.Append(" where id=@id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@vname", DbType.String,100),
					new SQLiteParameter("@field", DbType.String,40),
					new SQLiteParameter("@d_title", DbType.String,40),
					new SQLiteParameter("@title", DbType.String,40),
					new SQLiteParameter("@d_width", DbType.Int32,4),
					new SQLiteParameter("@width", DbType.Int32,4),
					new SQLiteParameter("@d_align", DbType.Int32,4),
					new SQLiteParameter("@align", DbType.Int32,4),
					new SQLiteParameter("@d_hide", DbType.Int32,4),
					new SQLiteParameter("@hide", DbType.Int32,4),
					new SQLiteParameter("@l_order", DbType.Int32,4),
					new SQLiteParameter("@frozen", DbType.Int32,4),
					new SQLiteParameter("@format", DbType.String,100),
					new SQLiteParameter("@sort", DbType.Int32,4),
					new SQLiteParameter("@f_type", DbType.String,40),
					new SQLiteParameter("@f_url", DbType.String),
					new SQLiteParameter("@d_f_area", DbType.Int32,4),
					new SQLiteParameter("@f_area", DbType.Int32,4),
					new SQLiteParameter("@d_f_col", DbType.Int32,4),
					new SQLiteParameter("@f_col", DbType.Int32,4),
					new SQLiteParameter("@f_order", DbType.Int32,4),
					new SQLiteParameter("@d_f_hide", DbType.Int32,4),
					new SQLiteParameter("@f_hide", DbType.Int32,4),
					new SQLiteParameter("@f_required", DbType.Int32,4),
					new SQLiteParameter("@f_placeholder", DbType.String,40),
					new SQLiteParameter("@f_value", DbType.String,100),
					new SQLiteParameter("@f_text", DbType.String,100),
					new SQLiteParameter("@id", DbType.Int32,8)};
            parameters[0].Value = model.vname;
            parameters[1].Value = model.field;
            parameters[2].Value = model.d_title;
            parameters[3].Value = model.title;
            parameters[4].Value = model.d_width;
            parameters[5].Value = model.width;
            parameters[6].Value = model.d_align;
            parameters[7].Value = model.align;
            parameters[8].Value = model.d_hide;
            parameters[9].Value = model.hide;
            parameters[10].Value = model.l_order;
            parameters[11].Value = model.frozen;
            parameters[12].Value = model.format;
            parameters[13].Value = model.sort;
            parameters[14].Value = model.f_type;
            parameters[15].Value = model.f_url;
            parameters[16].Value = model.d_f_area;
            parameters[17].Value = model.f_area;
            parameters[18].Value = model.d_f_col;
            parameters[19].Value = model.f_col;
            parameters[20].Value = model.f_order;
            parameters[21].Value = model.d_f_hide;
            parameters[22].Value = model.f_hide;
            parameters[23].Value = model.f_required;
            parameters[24].Value = model.f_placeholder;
            parameters[25].Value = model.f_value;
            parameters[26].Value = model.f_text;
            parameters[27].Value = model.id;

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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_column ");
            strSql.Append(" where id=@id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
            parameters[0].Value = id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_column ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DB.HelperSQLite.ExecuteSql(strSql.ToString());
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
        public RF.Model.sys_column GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,vname,field,d_title,title,d_width,width,d_align,align,d_hide,hide,l_order,frozen,format,sort,f_type,f_url,d_f_area,f_area,d_f_col,f_col,f_order,d_f_hide,f_hide,f_required,f_placeholder,f_value,f_text from sys_column ");
            strSql.Append(" where id=@id");
            SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
            parameters[0].Value = id;

            RF.Model.sys_column model = new RF.Model.sys_column();
            DataSet ds = DB.HelperSQLite.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public RF.Model.sys_column DataRowToModel(DataRow row)
        {
            RF.Model.sys_column model = new RF.Model.sys_column();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["vname"] != null)
                {
                    model.vname = row["vname"].ToString();
                }
                if (row["field"] != null)
                {
                    model.field = row["field"].ToString();
                }
                if (row["d_title"] != null)
                {
                    model.d_title = row["d_title"].ToString();
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["d_width"] != null && row["d_width"].ToString() != "")
                {
                    model.d_width = int.Parse(row["d_width"].ToString());
                }
                if (row["width"] != null && row["width"].ToString() != "")
                {
                    model.width = int.Parse(row["width"].ToString());
                }
                if (row["d_align"] != null && row["d_align"].ToString() != "")
                {
                    model.d_align = int.Parse(row["d_align"].ToString());
                }
                if (row["align"] != null && row["align"].ToString() != "")
                {
                    model.align = int.Parse(row["align"].ToString());
                }
                if (row["d_hide"] != null && row["d_hide"].ToString() != "")
                {
                    model.d_hide = int.Parse(row["d_hide"].ToString());
                }
                if (row["hide"] != null && row["hide"].ToString() != "")
                {
                    model.hide = int.Parse(row["hide"].ToString());
                }
                if (row["l_order"] != null && row["l_order"].ToString() != "")
                {
                    model.l_order = int.Parse(row["l_order"].ToString());
                }
                if (row["frozen"] != null && row["frozen"].ToString() != "")
                {
                    model.frozen = int.Parse(row["frozen"].ToString());
                }
                if (row["format"] != null)
                {
                    model.format = row["format"].ToString();
                }
                if (row["sort"] != null && row["sort"].ToString() != "")
                {
                    model.sort = int.Parse(row["sort"].ToString());
                }
                if (row["f_type"] != null)
                {
                    model.f_type = row["f_type"].ToString();
                }
                if (row["f_url"] != null)
                {
                    model.f_url = row["f_url"].ToString();
                }
                if (row["d_f_area"] != null && row["d_f_area"].ToString() != "")
                {
                    model.d_f_area = int.Parse(row["d_f_area"].ToString());
                }
                if (row["f_area"] != null && row["f_area"].ToString() != "")
                {
                    model.f_area = int.Parse(row["f_area"].ToString());
                }
                if (row["d_f_col"] != null && row["d_f_col"].ToString() != "")
                {
                    model.d_f_col = int.Parse(row["d_f_col"].ToString());
                }
                if (row["f_col"] != null && row["f_col"].ToString() != "")
                {
                    model.f_col = int.Parse(row["f_col"].ToString());
                }
                if (row["f_order"] != null && row["f_order"].ToString() != "")
                {
                    model.f_order = int.Parse(row["f_order"].ToString());
                }
                if (row["d_f_hide"] != null && row["d_f_hide"].ToString() != "")
                {
                    model.d_f_hide = int.Parse(row["d_f_hide"].ToString());
                }
                if (row["f_hide"] != null && row["f_hide"].ToString() != "")
                {
                    model.f_hide = int.Parse(row["f_hide"].ToString());
                }
                if (row["f_required"] != null && row["f_required"].ToString() != "")
                {
                    model.f_required = int.Parse(row["f_required"].ToString());
                }
                if (row["f_placeholder"] != null)
                {
                    model.f_placeholder = row["f_placeholder"].ToString();
                }
                if (row["f_value"] != null)
                {
                    model.f_value = row["f_value"].ToString();
                }
                if (row["f_text"] != null)
                {
                    model.f_text = row["f_text"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,vname,field,d_title,title,d_width,width,d_align,align,d_hide,hide,l_order,frozen,format,sort,f_type,f_url,d_f_area,f_area,d_f_col,f_col,f_order,d_f_hide,f_hide,f_required,f_placeholder,f_value,f_text ");
            strSql.Append(" FROM sys_column ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            parameters[0].Value = "sys_column";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQLite.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

