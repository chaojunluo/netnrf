/// <summary>
/// Tree JSON 节点
/// 推荐所有的JSON输出用此实体，保证一致性，即页面接收的JSON全是这种格式，方便维护。
/// 如果不够用，自己灵活追加。
/// </summary>
public class TreeNodeVM
{
    /// <summary>
    /// ID
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 父ID
    /// </summary>
    public string pid { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string text { get; set; }

    /// <summary>
    /// 拓展
    /// </summary>
    public string ext1 { get; set; }
    /// <summary>
    /// 拓展
    /// </summary>
    public string ext2 { get; set; }
    /// <summary>
    /// 拓展
    /// </summary>
    public string ext3 { get; set; }

    /// <summary>
    /// 备用
    /// </summary>
    public object spare1 { get; set; }
    /// <summary>
    /// 备用
    /// </summary>
    public object spare2 { get; set; }
    /// <summary>
    /// 备用
    /// </summary>
    public object spare3 { get; set; }
}