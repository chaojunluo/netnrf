using System;
namespace RF.Model
{
	/// <summary>
	/// sys_column:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_column
	{
		public sys_column()
		{}
        #region Model
        private int _id;
        private string _vname;
        private string _field;
        private string _d_title;
        private string _title;
        private int? _d_width = 100;
        private int? _width = 100;
        private int? _d_align = 1;
        private int? _align = 1;
        private int? _d_hide = 0;
        private int? _hide = 0;
        private int? _l_order = 100;
        private int? _frozen = 0;
        private string _format;
        private int? _sort = 0;
        private string _f_type;
        private string _f_url;
        private int? _d_f_area = 0;
        private int? _f_area = 0;
        private int? _d_f_col = 1;
        private int? _f_col = 1;
        private int? _f_order = 100;
        private int? _d_f_hide = 0;
        private int? _f_hide = 0;
        private int? _f_required = 0;
        private string _f_placeholder;
        private string _f_value;
        private string _f_text;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vname
        {
            set { _vname = value; }
            get { return _vname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string field
        {
            set { _field = value; }
            get { return _field; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string d_title
        {
            set { _d_title = value; }
            get { return _d_title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? d_width
        {
            set { _d_width = value; }
            get { return _d_width; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? width
        {
            set { _width = value; }
            get { return _width; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? d_align
        {
            set { _d_align = value; }
            get { return _d_align; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? align
        {
            set { _align = value; }
            get { return _align; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? d_hide
        {
            set { _d_hide = value; }
            get { return _d_hide; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? hide
        {
            set { _hide = value; }
            get { return _hide; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? l_order
        {
            set { _l_order = value; }
            get { return _l_order; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? frozen
        {
            set { _frozen = value; }
            get { return _frozen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string format
        {
            set { _format = value; }
            get { return _format; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string f_type
        {
            set { _f_type = value; }
            get { return _f_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string f_url
        {
            set { _f_url = value; }
            get { return _f_url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? d_f_area
        {
            set { _d_f_area = value; }
            get { return _d_f_area; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? f_area
        {
            set { _f_area = value; }
            get { return _f_area; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? d_f_col
        {
            set { _d_f_col = value; }
            get { return _d_f_col; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? f_col
        {
            set { _f_col = value; }
            get { return _f_col; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? f_order
        {
            set { _f_order = value; }
            get { return _f_order; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? d_f_hide
        {
            set { _d_f_hide = value; }
            get { return _d_f_hide; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? f_hide
        {
            set { _f_hide = value; }
            get { return _f_hide; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? f_required
        {
            set { _f_required = value; }
            get { return _f_required; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string f_placeholder
        {
            set { _f_placeholder = value; }
            get { return _f_placeholder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string f_value
        {
            set { _f_value = value; }
            get { return _f_value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string f_text
        {
            set { _f_text = value; }
            get { return _f_text; }
        }
        #endregion Model
	}
}

