using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApplication.Models;


namespace MyWebApplication.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Cash> Receipts { get; set; }
        public DbSet<Branch> Branches { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cash>().ToTable("Receipt");
            modelBuilder.Entity<Branch>().ToTable("Branch");
            modelBuilder.HasSequence<int>("ReceiptNumber", schema: "shared")
                .StartsAt(000000)
                .IncrementsBy(1);
            modelBuilder.Entity<Cash>()
                .Property(b => b.ReceiptDate)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Cash>().Property(x => x.Payment).HasDefaultValue(Payment.Cash);
            modelBuilder.Entity<Cash>().Property(x => x.CurrencyCode).HasDefaultValue(CurrencyCode.CAD);


        }
    }
}

