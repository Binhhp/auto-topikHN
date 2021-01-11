namespace ToolTopikHanoi.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Age> Ages { get; set; }
        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<MucDich> MucDiches { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PhuongTien> PhuongTiens { get; set; }
        public virtual DbSet<Year> Years { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
