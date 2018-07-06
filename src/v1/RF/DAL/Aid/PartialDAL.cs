using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RF.DAL
{
    public class PartialDAL
    {
        #region 菜单、按钮、角色权限 缓存

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSysMenu()
        {
            DataTable dt = DB.CatchTo.Get("GlobalSysMenu") as DataTable;
            if (dt == null)
            {
                dt = new DAL.sys_menu().GetList("m_state=1 order by m_order").Tables[0];
                DB.CatchTo.Set("GlobalSysMenu", dt);
            }
            return dt;
        }

        /// <summary>
        /// 获取按钮
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSysButton()
        {
            DataTable dt = DB.CatchTo.Get("GlobalSysButton") as DataTable;
            if (dt == null)
            {
                dt = new DAL.sys_button().GetList("b_state=1 order by b_order").Tables[0];
                DB.CatchTo.Set("GlobalSysButton", dt);
            }
            return dt;
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <returns></returns>
        public static DataRow GetSysRole()
        {
            int roleid = Convert.ToInt32(DAL.AccountDAL.U_roleid());
            DataRow dr = DB.CatchTo.Get("role" + roleid) as DataRow;
            if (dr == null)
            {
                dr = new DAL.sys_role().GetList("r_state=1 and id=" + roleid).Tables[0].Rows[0];
                DB.CatchTo.Set("role" + roleid, dr);
            }
            return dr;
        }

        /// <summary>
        /// 清除 菜单、按钮、角色 缓存
        /// </summary>
        public static void ClearCatchFoMenuButtonRole()
        {
            DB.CatchTo.RemoveAllCache();
        }

        #endregion
    }
}