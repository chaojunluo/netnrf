namespace Netnr.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysUser")]
    public partial class SysUser
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string RoleId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string UserPwd { get; set; }

        [StringLength(50)]
        public string Nickname { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string Sign { get; set; }

        public int? UserGroup { get; set; }
    }
}
