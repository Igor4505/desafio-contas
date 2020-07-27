using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Conductor.Desafio.Database.Migrations
{
    public partial class migrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(nullable: true, defaultValue: true),
                    Cpf = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Email = table.Column<string>(nullable: true),
                    Genero = table.Column<int>(nullable: false),
                    Nascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCriacao = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Descricao = table.Column<string>(nullable: true),
                    FlagAtivo = table.Column<bool>(nullable: false),
                    LimiteSaqueDiario = table.Column<decimal>(type: "Money", nullable: false),
                    PessoaId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    Saldo = table.Column<decimal>(type: "Money", nullable: false),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contas_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContaId = table.Column<int>(nullable: false),
                    DataTransacao = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Descricao = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true),
                    TipoTransacao = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contas_PessoaId",
                table: "Contas",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "CpfPessoa",
                table: "Pessoas",
                column: "Cpf",
                unique: true,
                filter: "[Cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailPessoa",
                table: "Pessoas",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_ContaId",
                table: "Transacoes",
                column: "ContaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
