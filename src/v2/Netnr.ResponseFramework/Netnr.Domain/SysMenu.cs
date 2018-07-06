using System;

namespace Netnr.Domain
{
	public class SysMenu
	{
		public SysMenu() { }
		#region Model

		public string ID { get; set; }
		public string PID { get; set; }
		/// <summary>
		/// 名称
		/// <summary>
		public string Name { get; set; }
		/// <summary>
		/// 链接
		/// <summary>
		public string Url { get; set; }
		/// <summary>
		/// 排序
		/// <summary>
		public int? MenuOrder { get; set; }
		/// <summary>
		/// 图标
		/// <summary>
		public string Icon { get; set; }
		/// <summary>
		/// 状态，1启用
		/// <summary>
		public int? Status { get; set; }
		/// <summary>
		/// 分组
		/// <summary>
		public int? MenuGroup { get; set; }

		#endregion Model
	}
}
