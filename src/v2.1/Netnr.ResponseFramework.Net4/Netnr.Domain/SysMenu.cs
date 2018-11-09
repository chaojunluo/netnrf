namespace Netnr.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysMenu")]
    public partial class SysMenu
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Pid { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Url { get; set; }

        public int? MenuOrder { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public int? Status { get; set; }

        public int? MenuGroup { get; set; }
    }
}
