using System;
namespace RF.Model
{
	/// <summary>
	/// sys_role:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_role
	{
		public sys_role()
		{}
		#region Model
		private int _id;
		private string _r_name;
		private int? _r_state;
		private string _r_remark;
		private string _r_menus;
		private string _r_buttons;
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
		public string r_name
		{
			set{ _r_name=value;}
			get{return _r_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? r_state
		{
			set{ _r_state=value;}
			get{return _r_state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string r_remark
		{
			set{ _r_remark=value;}
			get{return _r_remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string r_menus
		{
			set{ _r_menus=value;}
			get{return _r_menus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string r_buttons
		{
			set{ _r_buttons=value;}
			get{return _r_buttons;}
		}
		#endregion Model

	}
}

