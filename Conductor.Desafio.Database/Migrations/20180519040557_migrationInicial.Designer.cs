﻿// <auto-generated />
using Conductor.Desafio.Core.Enums;
using Conductor.Desafio.Database.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Conductor.Desafio.Database.Migrations
{
    [DbContext(typeof(DesafioDbContext))]
    [Migration("20180519040557_migrationInicial")]
    partial class migrationInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Conductor.Desafio.Core.Models.ContaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Descricao");

                    b.Property<bool>("FlagAtivo");

                    b.Property<decimal>("LimiteSaqueDiario")
                        .HasColumnType("Money");

                    b.Property<int>("PessoaId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken();

                    b.Property<decimal>("Saldo")
                        .HasColumnType("Money");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("Conductor.Desafio.Core.Models.PessoaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Cpf");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email");

                    b.Property<int>("Genero");

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("date");

                    b.Property<string>("Nome");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken();

                    b.Property<string>("Senha");

                    b.Property<string>("Sobrenome");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasName("CpfPessoa")
                        .HasFilter("[Cpf] IS NOT NULL");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("EmailPessoa")
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Conductor.Desafio.Core.Models.TransacaoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContaId");

                    b.Property<DateTime>("DataTransacao")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Descricao");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken();

                    b.Property<int>("TipoTransacao");

                    b.Property<decimal>("Valor")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("Conductor.Desafio.Core.Models.ContaModel", b =>
                {
                    b.HasOne("Conductor.Desafio.Core.Models.PessoaModel", "Pessoa")
                        .WithMany("Contas")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Conductor.Desafio.Core.Models.TransacaoModel", b =>
                {
                    b.HasOne("Conductor.Desafio.Core.Models.ContaModel", "Conta")
                        .WithMany("Transacoes")
                        .HasForeignKey("ContaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
