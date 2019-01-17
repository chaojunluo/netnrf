namespace Netnr.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysRole")]
    public partial class SysRole
    {
        [Key]
        [StringLength(50)]
        public string SrId { get; set; }

        [StringLength(200)]
        public string SrName { get; set; }

        public int? SrStatus { get; set; }

        [StringLength(200)]
        public string SrDescribe { get; set; }

        public int? SrGroup { get; set; }

        public string SrMenus { get; set; }

        public string SrButtons { get; set; }

        public DateTime? SrCreateTime { get; set; }
    }
}
