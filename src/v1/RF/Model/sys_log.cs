using System;
namespace RF.Model
{
	/// <summary>
	/// sys_log:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_log
	{
		public sys_log()
		{}
		#region Model
		private int _id;
		private string _l_user;
		private string _l_module;
		private string _l_action;
		private DateTime? _l_datetime;
		private string _l_content;
		private string _l_ip;
		private string _l_url;
		private string _l_spare;
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
		public string l_user
		{
			set{ _l_user=value;}
			get{return _l_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string l_module
		{
			set{ _l_module=value;}
			get{return _l_module;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string l_action
		{
			set{ _l_action=value;}
			get{return _l_action;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? l_datetime
		{
			set{ _l_datetime=value;}
			get{return _l_datetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string l_content
		{
			set{ _l_content=value;}
			get{return _l_content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string l_ip
		{
			set{ _l_ip=value;}
			get{return _l_ip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string l_url
		{
			set{ _l_url=value;}
			get{return _l_url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string l_spare
		{
			set{ _l_spare=value;}
			get{return _l_spare;}
		}
		#endregion Model

	}
}

