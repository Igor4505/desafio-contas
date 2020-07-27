using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Conductor.Desafio.Core.Models;
using Conductor.Desafio.Database.TypeConfiguration;

namespace Conductor.Desafio.Database.DataContext
{
    public class DesafioDbContext : DbContext
    {
        public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options)
        {

        }

        //DB SETS
        public DbSet<PessoaModel> Pessoas { get; set; }
        public DbSet<ContaModel> Contas { get; set; }
        public DbSet<TransacaoModel> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransacaoTypeConfiguration());
        }
    }
}
