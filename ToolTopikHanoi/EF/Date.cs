namespace ToolTopikHanoi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Date")]
    public partial class Date
    {
        public int Id { get; set; }

        [Column("Date")]
        public int? Date1 { get; set; }
    }
}
