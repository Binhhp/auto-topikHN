namespace ToolTopikHanoi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string NameEng { get; set; }

        [StringLength(100)]
        public string NameKor { get; set; }

        public bool? Sex { get; set; }

        [StringLength(100)]
        public string PhoneHome { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [StringLength(350)]
        public string Address { get; set; }

        public int? DateId { get; set; }

        public int? MonthId { get; set; }

        public int? YearId { get; set; }

        public int? AgeId { get; set; }

        public int? MucDichId { get; set; }

        public int? PhuongTienId { get; set; }

        public int? JobId { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(100)]
        public string CMND { get; set; }

        public int? Topik { get; set; }
    }
}
