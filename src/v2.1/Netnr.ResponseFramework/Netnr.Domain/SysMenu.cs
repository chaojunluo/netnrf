using System;
using System.Collections.Generic;

namespace Netnr.Domain
{
    public partial class SysMenu
    {
        public string Id { get; set; }
        public string Pid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? MenuOrder { get; set; }
        public string Icon { get; set; }
        public int? Status { get; set; }
        public int? MenuGroup { get; set; }
    }
}
