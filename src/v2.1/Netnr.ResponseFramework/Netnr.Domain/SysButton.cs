using System;
using System.Collections.Generic;

namespace Netnr.Domain
{
    public partial class SysButton
    {
        public string Id { get; set; }
        public string Pid { get; set; }
        public string BtnText { get; set; }
        public string BtnId { get; set; }
        public string BtnClass { get; set; }
        public string BtnIcon { get; set; }
        public int? BtnOrder { get; set; }
        public int? Status { get; set; }
        public string Describe { get; set; }
        public int? BtnGroup { get; set; }
    }
}
