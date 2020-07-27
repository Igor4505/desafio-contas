using System;
using System.Collections.Generic;
using System.Text;
using Conductor.Desafio.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conductor.Desafio.Database.TypeConfiguration
{
    public class ContaTypeConfiguration : IEntityTypeConfiguration<ContaModel>
    {
        public void Configure(EntityTypeBuilder<ContaModel> builder)
        {
            //PRIMARY KEY
            builder.HasKey(c => c.Id);

            //CAMPOS
            builder.Property(c => c.Saldo).HasColumnType("Money");
            builder.Property(c => c.LimiteSaqueDiario).HasColumnType("Money");

            //RELAÇÕES
            builder
                .HasOne(c => c.Pessoa)
                .WithMany(p => p.Contas)
                .HasForeignKey(b => b.PessoaId);

            //INDEXES

            //SHADOW PROPERTIES
            builder.Property<DateTime>("DataCriacao").HasDefaultValueSql("GETDATE()");

            //ROW VERSION
            builder.Property<byte[]>("RowVersion").IsConcurrencyToken();
        }
    }
}
