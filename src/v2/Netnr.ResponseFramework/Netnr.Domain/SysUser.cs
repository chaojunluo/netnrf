using System;

namespace Netnr.Domain
{
	public class SysUser
	{
		public SysUser() { }
		#region Model

		public string ID { get; set; }
		/// <summary>
		/// 角色
		/// <summary>
		public string RoleID { get; set; }
		/// <summary>
		/// 账号
		/// <summary>
		public string UserName { get; set; }
		/// <summary>
		/// 密码
		/// <summary>
		public string UserPwd { get; set; }
		/// <summary>
		/// 昵称
		/// <summary>
		public string Nickname { get; set; }
		/// <summary>
		/// 创建时间
		/// <summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 状态，1正常
		/// <summary>
		public int? Status { get; set; }
		/// <summary>
		/// 登录标识
		/// <summary>
		public string Sign { get; set; }
		/// <summary>
		/// 分组
		/// <summary>
		public int? UserGroup { get; set; }

		#endregion Model
	}
}
