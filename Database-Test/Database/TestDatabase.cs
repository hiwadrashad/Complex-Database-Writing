using Database_Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Test.Database
{
    public class TestDatabase : DbContext
    {
        public DbSet<GrandChild> GrandChildDatabase { get; set; }
        public DbSet<Child> ChildDatabase { get; set; }
        public DbSet<Parent> ParentDatabase { get; set; }
        public DbSet<CloneParent> CloneParentDatabase { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Database-Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<CloneParent>().HasOne(a => a.Child).WithMany().HasForeignKey(i => i.ChildId);
            //modelBuilder.Entity<Child>().HasOne(a => a.GrandChild).WithMany().HasForeignKey(i => i.GrandChild);
        }
    }
}
