namespace Netnr.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysMenu")]
    public partial class SysMenu
    {
        [Key]
        [StringLength(50)]
        public string SmId { get; set; }

        [StringLength(50)]
        public string SmPid { get; set; }

        [StringLength(50)]
        public string SmName { get; set; }

        [StringLength(200)]
        public string SmUrl { get; set; }

        public int? SmOrder { get; set; }

        [StringLength(50)]
        public string SmIcon { get; set; }

        public int? SmStatus { get; set; }

        public int? SmGroup { get; set; }
    }
}
