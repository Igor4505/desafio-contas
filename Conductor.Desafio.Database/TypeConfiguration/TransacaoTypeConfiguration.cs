using System;
using System.Collections.Generic;
using System.Text;
using Conductor.Desafio.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conductor.Desafio.Database.TypeConfiguration
{
    class TransacaoTypeConfiguration : IEntityTypeConfiguration<TransacaoModel>
    {
        public void Configure(EntityTypeBuilder<TransacaoModel> builder)
        {
            //PRIMARY KEY
            builder.HasKey(t => t.Id);

            //CAMPOS
            builder
                .Property(t => t.Valor)
                .HasColumnType("money");
            builder
                .Property(t => t.DataTransacao)
                .HasDefaultValueSql("GETDATE()");

            //RELAÇÕES
            builder
                .HasOne(t => t.Conta)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(t => t.ContaId);

            //INDEXES

            //SHADOW PROPERTIES

            //ROW VERSION
            builder.Property<byte[]>("RowVersion").IsConcurrencyToken();
        }
    }
}
