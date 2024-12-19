using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Savills.SIA.Entities.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ContactType> ContactTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryDepartment> CountryDepartments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Siaclient> Siaclients { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_City");

            entity.ToTable("City");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasColumnType("decimal(38, 10)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(38, 10)");
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_City_StateId");
        });

        modelBuilder.Entity<ContactType>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__ContactT__A25C5AA6B5A9C7B4");

            entity.ToTable("ContactType");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("pk_Country");

            entity.ToTable("Country");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.DialCode).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasColumnType("decimal(38, 10)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(38, 10)");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<CountryDepartment>(entity =>
        {
            entity.HasKey(e => new { e.CountryCode, e.DepartmentCode }).HasName("PK__CountryD__8B7185BBAD585802");

            entity.ToTable("CountryDepartment");

            entity.Property(e => e.CountryCode).HasMaxLength(10);
            entity.Property(e => e.DepartmentCode).HasMaxLength(10);
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.DepartmentCodeNavigation).WithMany(p => p.CountryDepartments)
                .HasForeignKey(d => d.DepartmentCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CountryDepartment_DepartmentCode");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("pk_Department");

            entity.ToTable("Department");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<Siaclient>(entity =>
        {
            entity.ToTable("SIAClient");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BusinessRegistrationNumber).HasMaxLength(150);
            entity.Property(e => e.ContactTypeCode).HasMaxLength(50);
            entity.Property(e => e.CountryCode).HasMaxLength(10);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(150);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.MailingAddress).HasMaxLength(100);
            entity.Property(e => e.MobileDialCode).HasMaxLength(5);
            entity.Property(e => e.MobileNumber).HasMaxLength(50);
            entity.Property(e => e.OfficeDialCode).HasMaxLength(5);
            entity.Property(e => e.OfficeNumber).HasMaxLength(50);
            entity.Property(e => e.OrganizationName).HasMaxLength(500);
            entity.Property(e => e.SicdivisionCode)
                .HasMaxLength(20)
                .HasColumnName("SICDivisionCode");
            entity.Property(e => e.SicmajorCode)
                .HasMaxLength(20)
                .HasColumnName("SICMajorCode");
            entity.Property(e => e.SourceCountryCode).HasMaxLength(10);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.Website).HasMaxLength(250);

            entity.HasOne(d => d.ContactTypeCodeNavigation).WithMany(p => p.Siaclients)
                .HasForeignKey(d => d.ContactTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactTypeCode");

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.SiaclientCountryCodeNavigations)
                .HasForeignKey(d => d.CountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIAClient_CountryCode");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SiaclientCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIAClient_CreateUser");

            entity.HasOne(d => d.LastUpdatedByNavigation).WithMany(p => p.SiaclientLastUpdatedByNavigations)
                .HasForeignKey(d => d.LastUpdatedBy)
                .HasConstraintName("FK_SIAClient_LastUpdateUser");

            entity.HasOne(d => d.SourceCountryCodeNavigation).WithMany(p => p.SiaclientSourceCountryCodeNavigations)
                .HasForeignKey(d => d.SourceCountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIAClient_SourceCountryCode");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_State");

            entity.ToTable("State");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CountryCode).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasColumnType("decimal(38, 10)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(38, 10)");
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_State_CountryCode");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address1).HasMaxLength(150);
            entity.Property(e => e.Address2).HasMaxLength(150);
            entity.Property(e => e.BusinessCountry).HasMaxLength(50);
            entity.Property(e => e.BusinessCountryCode).HasMaxLength(10);
            entity.Property(e => e.Company).HasMaxLength(255);
            entity.Property(e => e.CountryCode).HasMaxLength(10);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentCode).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsApplicationUser).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(60);
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.OfficeNumber).HasMaxLength(50);
            entity.Property(e => e.Teams).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Zip).HasMaxLength(50);

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_City_City");

            entity.HasOne(d => d.State).WithMany(p => p.Users)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_State_State");

            entity.HasOne(d => d.CountryDepartment).WithMany(p => p.Users)
                .HasForeignKey(d => new { d.BusinessCountryCode, d.DepartmentCode })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__168F36CB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
