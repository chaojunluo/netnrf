using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Data;
using System.Text;

namespace RF.DAL
{
    public class SettingDAL
    {
        #region 角色

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="menus">菜单配置</param>
        /// <param name="buttons">按钮配置</param>
        /// <returns></returns>
        public static bool SaveSysRoleAuth(int roleid, string menus, string buttons)
        {
            string sql = "update sys_role set r_menus=@r_menus,r_buttons=@r_buttons where id=@roleid";
            SQLiteParameter[] param = {
                                          new SQLiteParameter("@r_menus",DbType.String),
                                          new SQLiteParameter("@r_buttons",DbType.String),
                                          new SQLiteParameter("@roleid",DbType.Int32)
                                      };
            param[0].Value = menus;
            param[1].Value = buttons;
            param[2].Value = roleid;
            int row = DB.HelperSQLite.ExecuteSql(sql, param);
            return row >= 0 ? true : false;
        }

        /// <summary>
        /// 角色修改
        /// </summary>
        public bool UpdateSysRole(RF.Model.sys_role model)
        {
            StringBuilder strSql = new StringBuilder();
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

        #endregion

        
    }
}