namespace ToolTopikHanoi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MucDich")]
    public partial class MucDich
    {
        public int Id { get; set; }

        [Column("MucDich")]
        [StringLength(150)]
        public string MucDich1 { get; set; }
    }
}
