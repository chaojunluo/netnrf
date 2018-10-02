using System;
using System.Collections.Generic;

namespace Netnr.Domain
{
    public partial class SysRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public string Describe { get; set; }
        public int? RoleGroup { get; set; }
        public string Menus { get; set; }
        public string Buttons { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
