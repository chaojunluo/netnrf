using System;
using System.Collections.Generic;

namespace Netnr.Domain
{
    public partial class SysLog
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Nickname { get; set; }
        public string Action { get; set; }
        public string LogContent { get; set; }
        public string Url { get; set; }
        public string Ip { get; set; }
        public DateTime? CreateTime { get; set; }
        public string BrowserName { get; set; }
        public string SystemName { get; set; }
        public int? LogGroup { get; set; }
        public string Remark { get; set; }
    }
}
