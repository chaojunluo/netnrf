using System;

namespace Netnr.Domain
{
	public class SysButton
	{
		public SysButton() { }
		#region Model

		public string ID { get; set; }
		public string PID { get; set; }
		/// <summary>
		/// 按钮文本
		/// <summary>
		public string BtnText { get; set; }
		/// <summary>
		/// 按钮ID
		/// <summary>
		public string BtnId { get; set; }
		/// <summary>
		/// 按钮类
		/// <summary>
		public string BtnClass { get; set; }
		/// <summary>
		/// 按钮图标
		/// <summary>
		public string BtnIcon { get; set; }
		/// <summary>
		/// 排序
		/// <summary>
		public int? BtnOrder { get; set; }
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
		public int? BtnGroup { get; set; }

		#endregion Model
	}
}
