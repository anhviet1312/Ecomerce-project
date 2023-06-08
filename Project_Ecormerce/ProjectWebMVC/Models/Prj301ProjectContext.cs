using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectWebMVC.Models;

public partial class Prj301ProjectContext : DbContext
{
    public Prj301ProjectContext()
    {
    }

    public Prj301ProjectContext(DbContextOptions<Prj301ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BalanceChange> BalanceChanges { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PhoneDetail> PhoneDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        IConfiguration configuration = null;
        configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        options.UseSqlServer(configuration.GetConnectionString("MyDB"));

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BalanceChange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__balance___3213E83F0A847184");

            entity.ToTable("balance_change");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Userid)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("userid");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.User).WithMany(p => p.BalanceChanges)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_userid_balance");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__Category__D837D05F19E15FE0");

            entity.ToTable("Category");

            entity.Property(e => e.Cid)
                .ValueGeneratedNever()
                .HasColumnName("cid");
            entity.Property(e => e.Cname)
                .HasMaxLength(20)
                .HasColumnName("cname");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order__3213E83FE924060F");

            entity.ToTable("order");

            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Userid)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_userid");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.Oid, e.Pid }).HasName("PK__order_de__7F2CB282F614CC6C");

            entity.ToTable("order_detail");

            entity.Property(e => e.Oid).HasColumnName("oid");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.OidNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.Oid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_detai__oid__76969D2E");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.Pid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_detai__pid__778AC167");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__phone__DD37D91A49AC1BAA");

            entity.ToTable("phone");

            entity.Property(e => e.Pid)
                .ValueGeneratedNever()
                .HasColumnName("pid").ValueGeneratedOnAdd(); ;
            entity.Property(e => e.Cid).HasColumnName("cid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Img)
                .IsUnicode(false)
                .HasColumnName("img");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Phones)
                .HasForeignKey(d => d.Cid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_cid");
        });

        modelBuilder.Entity<PhoneDetail>(entity =>
        {
            entity.HasKey(e => e.Pdid).HasName("PK__phone_de__822264962B8933A1");

            entity.ToTable("phone_detail");

            entity.Property(e => e.Pdid)
                .ValueGeneratedNever()
                .HasColumnName("pdid");
            entity.Property(e => e.Camera).HasColumnName("camera");
            entity.Property(e => e.Pin).HasColumnName("pin");
            entity.Property(e => e.Ram).HasColumnName("ram");
            entity.Property(e => e.Storage).HasColumnName("storage");

            entity.HasOne(d => d.Pd).WithOne(p => p.PhoneDetail)
                .HasForeignKey<PhoneDetail>(d => d.Pdid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pdid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__user__F3DBC573E032F77C");

            entity.ToTable("user");

            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Telephone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telephone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
