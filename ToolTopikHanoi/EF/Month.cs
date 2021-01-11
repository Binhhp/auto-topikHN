namespace ToolTopikHanoi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Month")]
    public partial class Month
    {
        public int Id { get; set; }

        [Column("Month")]
        public int? Month1 { get; set; }
    }
}
