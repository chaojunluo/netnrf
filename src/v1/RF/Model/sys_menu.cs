using System;
namespace RF.Model
{
	/// <summary>
	/// sys_menu:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_menu
	{
		public sys_menu()
		{}
		#region Model
		private int _id;
		private string _m_name;
		private string _m_url;
		private int? _m_pid;
		private int? _m_order;
		private string _m_icon;
		private int? _m_state;
		private string _m_button;
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
		public string m_name
		{
			set{ _m_name=value;}
			get{return _m_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string m_url
		{
			set{ _m_url=value;}
			get{return _m_url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? m_pid
		{
			set{ _m_pid=value;}
			get{return _m_pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? m_order
		{
			set{ _m_order=value;}
			get{return _m_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string m_icon
		{
			set{ _m_icon=value;}
			get{return _m_icon;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? m_state
		{
			set{ _m_state=value;}
			get{return _m_state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string m_button
		{
			set{ _m_button=value;}
			get{return _m_button;}
		}
		#endregion Model

	}
}

