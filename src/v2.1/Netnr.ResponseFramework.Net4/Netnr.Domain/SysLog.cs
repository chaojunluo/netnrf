namespace Netnr.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysLog")]
    public partial class SysLog
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Nickname { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        [StringLength(200)]
        public string LogContent { get; set; }

        [StringLength(500)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Ip { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string BrowserName { get; set; }

        [StringLength(50)]
        public string SystemName { get; set; }

        public int? LogGroup { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }
    }
}
