using System;

namespace Netnr.Domain
{
	public class SysLog
	{
		public SysLog() { }
		#region Model

		public string ID { get; set; }
		/// <summary>
		/// 账号
		/// <summary>
		public string UserName { get; set; }
		/// <summary>
		/// 昵称
		/// <summary>
		public string Nickname { get; set; }
		/// <summary>
		/// 动作
		/// <summary>
		public string Action { get; set; }
		/// <summary>
		/// 内容
		/// <summary>
		public string LogContent { get; set; }
		/// <summary>
		/// 链接
		/// <summary>
		public string Url { get; set; }
		/// <summary>
		/// IP
		/// <summary>
		public string Ip { get; set; }
		/// <summary>
		/// 时间
		/// <summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 浏览器名称
		/// <summary>
		public string BrowserName { get; set; }
		/// <summary>
		/// 操作系统
		/// <summary>
		public string SystemName { get; set; }
		/// <summary>
		/// 分组
		/// <summary>
		public int? LogGroup { get; set; }
		/// <summary>
		/// 备注
		/// <summary>
		public string Remark { get; set; }

		#endregion Model
	}
}
