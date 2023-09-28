using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BTBuoi6.Model
{
    public partial class EmployeesContextDB : DbContext
    {
        public EmployeesContextDB()
            : base("name=EmployeesContextDB")
        {
        }

        public virtual DbSet<Nhanvien> Nhanviens { get; set; }
        public virtual DbSet<Phongban> Phongbans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.MaNV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Nhanvien>()
                .Property(e => e.MaPB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Phongban>()
                .Property(e => e.MaPB)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
