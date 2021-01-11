namespace ToolTopikHanoi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhuongTien")]
    public partial class PhuongTien
    {
        public int Id { get; set; }

        [Column("PhuongTien")]
        [StringLength(100)]
        public string PhuongTien1 { get; set; }
    }
}
