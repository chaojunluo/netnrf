using System;
using System.Collections.Generic;

namespace Netnr.Domain
{
    public partial class SysUser
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string Nickname { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? Status { get; set; }
        public string Sign { get; set; }
        public int? UserGroup { get; set; }
    }
}
