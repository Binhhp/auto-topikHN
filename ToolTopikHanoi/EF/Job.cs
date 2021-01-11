namespace ToolTopikHanoi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Job")]
    public partial class Job
    {
        public int Id { get; set; }

        [Column("Job")]
        [StringLength(50)]
        public string Job1 { get; set; }
    }
}
