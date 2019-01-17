namespace Netnr.Func.ViewModel
{
    /// <summary>
    /// JSON节点
    /// 推荐所有的JSON输出用此实体，保证一致性，即页面接收的JSON全是这种格式，方便维护。
    /// 如果不够用，自己灵活追加。
    /// </summary>
    public class JSONodeVM
    {
        public string id { get; set; }
        public string pid { get; set; }
        public string text { get; set; }

        public string ext1 { get; set; }
        public string ext2 { get; set; }
        public string ext3 { get; set; }

        public object spare1 { get; set; }
        public object spare2 { get; set; }
        public object spare3 { get; set; }
    }
}
