using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

public partial class HhContext : DbContext
{
    public HhContext()
    {
    }

    public HhContext(DbContextOptions<HhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<DictionaryKey> DictionaryKeys { get; set; }

    public virtual DbSet<DictionaryValue> DictionaryValues { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<EmployerIndustry> EmployerIndustries { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<Vacancy> Vacancies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Filename=D:\\Projects\\BigData.HeadHunter\\hh.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.ToTable("Area");

            entity.HasIndex(e => e.Id, "IX_Area_Id").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DictionaryKey>(entity =>
        {
            entity.ToTable("Dictionary.Keys");
        });

        modelBuilder.Entity<DictionaryValue>(entity =>
        {
            entity.ToTable("Dictionary.Values");

            entity.HasOne(d => d.Key).WithMany(p => p.DictionaryValues)
                .HasForeignKey(d => d.KeyId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.ToTable("Employer");

            entity.HasIndex(e => e.Id, "IX_Employer_Id").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Area).WithMany(p => p.Employers).HasForeignKey(d => d.AreaId);
        });

        modelBuilder.Entity<EmployerIndustry>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Employer.Industries");

            entity.HasOne(d => d.Employer).WithMany()
                .HasForeignKey(d => d.EmployerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Industry).WithMany()
                .HasForeignKey(d => d.IndustryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.ToTable("Industry");
        });

        modelBuilder.Entity<Vacancy>(entity =>
        {
            entity.ToTable("Vacancy");

            entity.HasIndex(e => e.Id, "IX_Vacancy_Id").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SalaryCurrency).HasColumnName("Salary.Currency");
            entity.Property(e => e.SalaryFrom).HasColumnName("Salary.From");
            entity.Property(e => e.SalaryGross).HasColumnName("Salary.Gross");
            entity.Property(e => e.SalaryTo).HasColumnName("Salary.To");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Area).WithMany(p => p.Vacancies).HasForeignKey(d => d.AreaId);

            entity.HasOne(d => d.Employer).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.EmployerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
