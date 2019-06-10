using System;

namespace Netnr.Func.ViewModel
{
    /// <summary>
    /// 通用请求方法返回对象
    /// </summary>
    public class FunctionResultVM
    {
        /// <summary>
        /// 错误码，200表示成功，-1表示异常，其它自定义
        /// </summary>
        public int code { get; set; } = 0;

        /// <summary>
        /// 消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 主体数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endTime { get; set; }

        /// <summary>
        /// 用时，毫秒
        /// </summary>
        public double useTime
        {
            get
            {
                return ((endTime ?? DateTime.Now) - startTime).TotalMilliseconds;
            }
        }

        /// <summary>
        /// 设置快捷标签，赋值code、message
        /// </summary>
        /// <param name="tag">快捷标签枚举</param>
        public void Set(FRTag tag)
        {
            code = Convert.ToInt32(tag);
            message = tag.ToString();

            endTime = DateTime.Now;
        }

        /// <summary>
        /// 设置快捷标签，赋值code、message
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="appendCatch">追加错误消息，默认true</param>
        public void Set(Exception ex, bool appendCatch = true)
        {
            code = -1;
            message = "处理出错";
            if (appendCatch)
            {
                message += "，错误消息：" + ex.Message;
            }

            endTime = DateTime.Now;
        }
    }

    /// <summary>
    /// 快捷标签枚举
    /// </summary>
    public enum FRTag
    {
        /// <summary>
        /// 成功
        /// </summary>
        success = 200,
        /// <summary>
        /// 失败
        /// </summary>
        fail = 400,
        /// <summary>
        /// 错误
        /// </summary>
        error = 500,
        /// <summary>
        /// <summary>
        /// 未授权
        /// </summary>
        unauthorized = 401,
        /// 存在
        /// </summary>
        exist = 97,
        /// <summary>
        /// 无效
        /// </summary>
        invalid = 95,
        /// <summary>
        /// 缺省
        /// </summary>
        lack = 94
    }
}
