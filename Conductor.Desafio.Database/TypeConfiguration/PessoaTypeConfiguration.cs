using System;
using System.Collections.Generic;
using System.Text;
using Conductor.Desafio.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conductor.Desafio.Database.TypeConfiguration
{
    public class PessoaTypeConfiguration : IEntityTypeConfiguration<PessoaModel>
    {
        public void Configure(EntityTypeBuilder<PessoaModel> builder)
        {
            //PRIMARY KEY
            builder.HasKey(p => p.Id);

            //CAMPOS
            builder
                .Property(p => p.Nascimento)
                .HasColumnType("date");
            builder
                .Property(p => p.Ativo)
                .HasDefaultValue(true);
            //RELAÇÕES

            //INDEXES
            builder
                .HasIndex(p => p.Cpf)
                .HasName("CpfPessoa")
                .IsUnique();
            builder
                .HasIndex(p => p.Email)
                .HasName("EmailPessoa")
                .IsUnique();

            //SHADOW PROPERTIES
            builder.Property<DateTime>("DataCriacao").HasDefaultValueSql("GETDATE()");

            //ROW VERSION
            builder.Property<byte[]>("RowVersion").IsConcurrencyToken();
        }
    }
}
