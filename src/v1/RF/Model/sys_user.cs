using System;
namespace RF.Model
{
	/// <summary>
	/// sys_user:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_user
	{
		public sys_user()
		{}
		#region Model
		private string _id;
		private string _u_name;
		private string _u_pwd;
		private string _u_roleid;
		private string _u_nickname;
		private string _u_sign;
		private string _u_photo;
        private int? _u_state;
        private DateTime? _u_date;
		/// <summary>
		/// 
		/// </summary>
		public string id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string u_name
		{
			set{ _u_name=value;}
			get{return _u_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string u_pwd
		{
			set{ _u_pwd=value;}
			get{return _u_pwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string u_roleid
		{
			set{ _u_roleid=value;}
			get{return _u_roleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string u_nickname
		{
			set{ _u_nickname=value;}
			get{return _u_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string u_sign
		{
			set{ _u_sign=value;}
			get{return _u_sign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string u_photo
		{
			set{ _u_photo=value;}
			get{return _u_photo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? u_state
		{
			set{ _u_state=value;}
			get{return _u_state;}
		}
        /// <summary>
        /// 
        /// </summary>
        public DateTime? u_date
        {
            set { _u_date = value; }
            get { return _u_date; }
        }
		#endregion Model

	}
}

