namespace Netnr.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysRole")]
    public partial class SysRole
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int? Status { get; set; }

        [StringLength(200)]
        public string Describe { get; set; }

        public int? RoleGroup { get; set; }

        public string Menus { get; set; }

        public string Buttons { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
