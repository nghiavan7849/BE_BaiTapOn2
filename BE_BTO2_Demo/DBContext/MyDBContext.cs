using BE_BTO2_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_BTO2_Demo.DBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() { }

        public MyDBContext(DbContextOptions<MyDBContext> options): base (options) { }

        public virtual DbSet<Intern> Interns { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<AllowAccess> AllowAccess { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithOne(r => r.User)
                .HasForeignKey<User>(u => u.RoleId);
            modelBuilder.Entity<AllowAccess>()
                .HasOne(a => a.Role)
                .WithMany(r => r.AllowAccesses)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
