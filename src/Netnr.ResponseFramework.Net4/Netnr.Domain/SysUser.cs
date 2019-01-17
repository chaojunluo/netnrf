namespace Netnr.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysUser")]
    public partial class SysUser
    {
        [Key]
        [StringLength(50)]
        public string SuId { get; set; }

        [StringLength(50)]
        public string SrId { get; set; }

        [StringLength(50)]
        public string SuName { get; set; }

        [StringLength(50)]
        public string SuPwd { get; set; }

        [StringLength(50)]
        public string SuNickname { get; set; }

        public DateTime? SuCreateTime { get; set; }

        public int? SuStatus { get; set; }

        [StringLength(50)]
        public string SuSign { get; set; }

        public int? SuGroup { get; set; }
    }
}
