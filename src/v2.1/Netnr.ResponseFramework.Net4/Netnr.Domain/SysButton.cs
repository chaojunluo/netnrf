namespace Netnr.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SysButton")]
    public partial class SysButton
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Pid { get; set; }

        [StringLength(20)]
        public string BtnText { get; set; }

        [StringLength(50)]
        public string BtnId { get; set; }

        [StringLength(50)]
        public string BtnClass { get; set; }

        [StringLength(50)]
        public string BtnIcon { get; set; }

        public int? BtnOrder { get; set; }

        public int? Status { get; set; }

        [StringLength(200)]
        public string Describe { get; set; }

        public int? BtnGroup { get; set; }
    }
}
