using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CarRental.Models
{
    public partial class CarRentContext : DbContext
    {
        public CarRentContext()
        {
        }

        public CarRentContext(DbContextOptions<CarRentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DatXe> DatXes { get; set; }
        public virtual DbSet<Loaixe> Loaixes { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<ThanhToan> ThanhToans { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local);initial catalog=CarRent; trusted_connection=yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.TenKhach)
                    .HasName("PK__Customer__BF09D2131990F816");

                entity.ToTable("Customer");

                entity.Property(e => e.TenKhach).HasMaxLength(100);

                entity.Property(e => e.DiaChi).HasMaxLength(300);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Pass).HasMaxLength(20);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SDT");
            });

            modelBuilder.Entity<DatXe>(entity =>
            {
                entity.ToTable("DatXe");

                entity.Property(e => e.BienSo).HasMaxLength(20);

                entity.Property(e => e.NgayDat).HasColumnType("date");

                entity.Property(e => e.NgayTra).HasColumnType("date");

                entity.Property(e => e.Sdt).HasMaxLength(11);

                entity.Property(e => e.TenKhach).HasMaxLength(100);

                entity.HasOne(d => d.BienSoNavigation)
                    .WithMany(p => p.DatXes)
                    .HasForeignKey(d => d.BienSo)
                    .HasConstraintName("FK__DatXe__BienSo__534D60F1");

                entity.HasOne(d => d.TenKhachNavigation)
                    .WithMany(p => p.DatXes)
                    .HasForeignKey(d => d.TenKhach)
                    .HasConstraintName("FK__DatXe__TenKhach__52593CB8");
            });

            modelBuilder.Entity<Loaixe>(entity =>
            {
                entity.HasKey(e => e.TenLoai)
                    .HasName("PK__Loaixe__E29B1043A3464C41");

                entity.ToTable("Loaixe");

                entity.Property(e => e.TenLoai).HasMaxLength(200);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.ToTable("NhanVien");

                entity.Property(e => e.CaLam).HasMaxLength(200);

                entity.Property(e => e.ChucVu).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(400);

                entity.Property(e => e.Pass).HasMaxLength(20);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.Ten).HasMaxLength(50);
            });

            modelBuilder.Entity<ThanhToan>(entity =>
            {
                entity.HasKey(e => e.IdBill)
                    .HasName("PK__ThanhToa__24A2D64D3B7E6823");

                entity.ToTable("ThanhToan");

                entity.Property(e => e.BienSo).HasMaxLength(20);

                entity.Property(e => e.NgayIn).HasColumnType("datetime");

                entity.Property(e => e.TenKhach).HasMaxLength(100);

                entity.HasOne(d => d.BienSoNavigation)
                    .WithMany(p => p.ThanhToans)
                    .HasForeignKey(d => d.BienSo)
                    .HasConstraintName("FK__ThanhToan__BienS__5812160E");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.ThanhToans)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__ThanhToan__Id__571DF1D5");

                entity.HasOne(d => d.TenKhachNavigation)
                    .WithMany(p => p.ThanhToans)
                    .HasForeignKey(d => d.TenKhach)
                    .HasConstraintName("FK__ThanhToan__TenKh__5629CD9C");
            });

            modelBuilder.Entity<Xe>(entity =>
            {
                entity.HasKey(e => e.BienSo)
                    .HasName("PK__Xe__F7052EB793815001");

                entity.ToTable("Xe");

                entity.Property(e => e.BienSo).HasMaxLength(20);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Ten).HasMaxLength(500);

                entity.Property(e => e.TenLoai).HasMaxLength(200);

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.TenLoaiNavigation)
                    .WithMany(p => p.Xes)
                    .HasForeignKey(d => d.TenLoai)
                    .HasConstraintName("FK__Xe__TenLoai__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
