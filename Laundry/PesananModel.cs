using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Laundry
{
    public partial class PesananModel : DbContext
    {
        public PesananModel()
            : base("name=PesananModel")
        {
        }

        public virtual DbSet<Pesanan> Pesanans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
