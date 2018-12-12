using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityDatabase.Models
{
    public partial class SqueezeBillContext : DbContext
    {
        public SqueezeBillContext()
        {
        }

        public SqueezeBillContext(DbContextOptions<SqueezeBillContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<EnergyGlossary> EnergyGlossary { get; set; }
        public virtual DbSet<EnergyGlossaryImage> EnergyGlossaryImage { get; set; }
        public virtual DbSet<Faq> Faq { get; set; }
        public virtual DbSet<HowSwitching> HowSwitching { get; set; }
        public virtual DbSet<HowSwitchingImage> HowSwitchingImage { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsImage> NewsImage { get; set; }
        public virtual DbSet<PricePlan> PricePlan { get; set; }
        public virtual DbSet<Retailer> Retailer { get; set; }
        public virtual DbSet<ServiceArea> ServiceArea { get; set; }
        public virtual DbSet<ServiceAreaImage> ServiceAreaImage { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WhySwitching> WhySwitching { get; set; }
        public virtual DbSet<WhySwitchingImage> WhySwitchingImage { get; set; }
        public virtual DbSet<ZipCode> ZipCode { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=180.151.232.92;Database=SqueezeBill; user id=sandbox;password=VnvSan@#18Sky#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CCcf)
                    .HasColumnName("C/Ccf")
                    .HasMaxLength(50);

                entity.Property(e => e.CKwh)
                    .HasColumnName("C/Kwh")
                    .HasMaxLength(50);

                entity.Property(e => e.CcfM)
                    .HasColumnName("Ccf/M")
                    .HasMaxLength(50);

                entity.Property(e => e.CommecialRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CompanyName).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(100);

                entity.Property(e => e.KhwM)
                    .HasColumnName("Khw/M")
                    .HasMaxLength(50);

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ResidentialRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UnitCharget).HasMaxLength(20);
            });

            modelBuilder.Entity<ContactUs>(entity =>
            {
                entity.Property(e => e.EmailId).HasMaxLength(500);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Zipcode).HasMaxLength(20);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Modifieddate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<EnergyGlossaryImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImagePaht).HasMaxLength(500);
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");
            });

            modelBuilder.Entity<HowSwitchingImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImagePaht).HasMaxLength(500);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<NewsImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImagePaht).HasMaxLength(500);
            });

            modelBuilder.Entity<PricePlan>(entity =>
            {
                entity.HasKey(e => e.Planid);

                entity.Property(e => e.CommercialPrice).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ResidentialPrice).HasColumnType("numeric(18, 5)");
            });

            modelBuilder.Entity<Retailer>(entity =>
            {
                entity.Property(e => e.CCcf)
                    .HasColumnName("C/Ccf")
                    .HasMaxLength(20);

                entity.Property(e => e.CKhw)
                    .HasColumnName("C/khw")
                    .HasMaxLength(20);

                entity.Property(e => e.CcfM)
                    .HasColumnName("Ccf/M")
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(100);

                entity.Property(e => e.KhwM)
                    .HasColumnName("Khw/M")
                    .HasMaxLength(20);

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RetailerName).HasMaxLength(200);
            });

            modelBuilder.Entity<ServiceAreaImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImagePaht).HasMaxLength(500);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MobilePhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Otp)
                    .HasColumnName("OTP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PerUnit).HasMaxLength(20);

                entity.Property(e => e.PriceAlert).HasDefaultValueSql("((0))");

                entity.Property(e => e.State)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WhySwitchingImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImagePaht).HasMaxLength(500);
            });

            modelBuilder.Entity<ZipCode>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });
        }
    }
}
