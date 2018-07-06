using System;

namespace Netnr.Domain
{
	public class SysRole
	{
		public SysRole() { }
		#region Model

		public string ID { get; set; }
		/// <summary>
		/// 名称
		/// <summary>
		public string Name { get; set; }
		/// <summary>
		/// 状态，1启用
		/// <summary>
		public int? Status { get; set; }
		/// <summary>
		/// 描述
		/// <summary>
		public string Describe { get; set; }
		/// <summary>
		/// 分组
		/// <summary>
		public int? RoleGroup { get; set; }
		/// <summary>
		/// 菜单
		/// <summary>
		public string Menus { get; set; }
		/// <summary>
		/// 按钮
		/// <summary>
		public string Buttons { get; set; }
		/// <summary>
		/// 创建时间
		/// <summary>
		public DateTime? CreateTime { get; set; }

		#endregion Model
	}
}
