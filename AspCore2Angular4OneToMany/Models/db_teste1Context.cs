using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AspCore2Angular4OneToMany.Models
{
    public partial class db_teste1Context : DbContext
    {
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Empregado> Empregado { get; set; }

        public db_teste1Context(DbContextOptions<db_teste1Context> options)
: base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.Nome).HasMaxLength(50);
            });

            modelBuilder.Entity<Empregado>(entity =>
            {
                entity.Property(e => e.EmpregadoId).HasColumnName("EmpregadoID");

                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Nome).HasMaxLength(50);

                entity.Property(e => e.Sobrenome).HasMaxLength(50);

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.Empregado)
                    .HasForeignKey(d => d.DepartamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empregado_Departamento");
            });
        }
    }
}
