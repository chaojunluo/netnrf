using System;
namespace RF.Model
{
	/// <summary>
	/// sys_button:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_button
	{
		public sys_button()
		{}
		#region Model
		private int _id;
		private string _b_title;
		private string _b_id;
		private string _b_class;
		private string _b_icon;
		private int? _b_pid;
		private int? _b_state;
		private int? _b_order;
		private string _b_remark;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string b_title
		{
			set{ _b_title=value;}
			get{return _b_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string b_id
		{
			set{ _b_id=value;}
			get{return _b_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string b_class
		{
			set{ _b_class=value;}
			get{return _b_class;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string b_icon
		{
			set{ _b_icon=value;}
			get{return _b_icon;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? b_pid
		{
			set{ _b_pid=value;}
			get{return _b_pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? b_state
		{
			set{ _b_state=value;}
			get{return _b_state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? b_order
		{
			set{ _b_order=value;}
			get{return _b_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string b_remark
		{
			set{ _b_remark=value;}
			get{return _b_remark;}
		}
		#endregion Model

	}
}

