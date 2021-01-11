namespace ToolTopikHanoi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Year")]
    public partial class Year
    {
        public int Id { get; set; }

        [Column("Year")]
        public int? Year1 { get; set; }
    }
}
