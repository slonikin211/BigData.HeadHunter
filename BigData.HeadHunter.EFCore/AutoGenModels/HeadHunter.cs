using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BigData.HeadHunter.EFCore;

public partial class HeadHunter : DbContext
{
    public HeadHunter()
    {
    }

    public HeadHunter(DbContextOptions<HeadHunter> options)
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
        => optionsBuilder.UseSqlite("Filename=../hh.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DictionaryValue>(entity =>
        {
            entity.HasOne(d => d.Key).WithMany(p => p.DictionaryValues).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<EmployerIndustry>(entity =>
        {
            entity.HasOne(d => d.Employer).WithMany().OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Industry).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Vacancy>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Employer).WithMany(p => p.Vacancies).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
