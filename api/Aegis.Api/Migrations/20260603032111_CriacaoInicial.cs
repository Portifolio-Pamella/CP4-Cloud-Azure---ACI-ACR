using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aegis.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CHASER",
                columns: table => new
                {
                    ID_CHASER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_CHASER = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    NUM_BATERIA = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    TXT_COORDENADA_X = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    TXT_COORDENADA_Y = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    TXT_COORDENADA_Z = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CHASER", x => x.ID_CHASER);
                });

            migrationBuilder.CreateTable(
                name: "TB_DETRITO_ESPACIAL",
                columns: table => new
                {
                    ID_DETRITO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_DETRITO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    MASSA_KG = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    TAMANHO_METROS = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    TXT_COORDENADA_X = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    TXT_COORDENADA_Y = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    TXT_COORDENADA_Z = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DETRITO_ESPACIAL", x => x.ID_DETRITO);
                });

            migrationBuilder.CreateTable(
                name: "TB_EMPRESA_AEROESPACIAL",
                columns: table => new
                {
                    ID_EMPRESA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_EMPRESA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CNPJ_ID_EMPRESA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PAIS_ORIGEM = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    STATUS_OPERACAO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EMPRESA_AEROESPACIAL", x => x.ID_EMPRESA);
                });

            migrationBuilder.CreateTable(
                name: "TB_SATELITE",
                columns: table => new
                {
                    ID_SATELITE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NUMERO_SATELITE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NUMERO_ALTITUDE_KM = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    ID_EMPRESA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SATELITE", x => x.ID_SATELITE);
                    table.ForeignKey(
                        name: "FK_TB_SATELITE_TB_EMPRESA_AEROESPACIAL_ID_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "TB_EMPRESA_AEROESPACIAL",
                        principalColumn: "ID_EMPRESA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ALERTA_COLISAO",
                columns: table => new
                {
                    ID_ALERTA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_SATELITE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_DETRITO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STATUS_GRAVIDADE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DetritoEspacialId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALERTA_COLISAO", x => x.ID_ALERTA);
                    table.ForeignKey(
                        name: "FK_TB_ALERTA_COLISAO_TB_DETRITO_ESPACIAL_DetritoEspacialId",
                        column: x => x.DetritoEspacialId,
                        principalTable: "TB_DETRITO_ESPACIAL",
                        principalColumn: "ID_DETRITO");
                    table.ForeignKey(
                        name: "FK_TB_ALERTA_COLISAO_TB_DETRITO_ESPACIAL_ID_DETRITO",
                        column: x => x.ID_DETRITO,
                        principalTable: "TB_DETRITO_ESPACIAL",
                        principalColumn: "ID_DETRITO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ALERTA_COLISAO_TB_SATELITE_ID_SATELITE",
                        column: x => x.ID_SATELITE,
                        principalTable: "TB_SATELITE",
                        principalColumn: "ID_SATELITE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_MISSAO_INTERCEPTACAO",
                columns: table => new
                {
                    ID_MISSAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_ALERTA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_CHASER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATA_EXECUCAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS_MISSAO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ChaserId1 = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MISSAO_INTERCEPTACAO", x => x.ID_MISSAO);
                    table.ForeignKey(
                        name: "FK_TB_MISSAO_INTERCEPTACAO_TB_ALERTA_COLISAO_ID_ALERTA",
                        column: x => x.ID_ALERTA,
                        principalTable: "TB_ALERTA_COLISAO",
                        principalColumn: "ID_ALERTA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_MISSAO_INTERCEPTACAO_TB_CHASER_ChaserId1",
                        column: x => x.ChaserId1,
                        principalTable: "TB_CHASER",
                        principalColumn: "ID_CHASER");
                    table.ForeignKey(
                        name: "FK_TB_MISSAO_INTERCEPTACAO_TB_CHASER_ID_CHASER",
                        column: x => x.ID_CHASER,
                        principalTable: "TB_CHASER",
                        principalColumn: "ID_CHASER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALERTA_COLISAO_DetritoEspacialId",
                table: "TB_ALERTA_COLISAO",
                column: "DetritoEspacialId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALERTA_COLISAO_ID_DETRITO",
                table: "TB_ALERTA_COLISAO",
                column: "ID_DETRITO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALERTA_COLISAO_ID_SATELITE",
                table: "TB_ALERTA_COLISAO",
                column: "ID_SATELITE");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MISSAO_INTERCEPTACAO_ChaserId1",
                table: "TB_MISSAO_INTERCEPTACAO",
                column: "ChaserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MISSAO_INTERCEPTACAO_ID_ALERTA",
                table: "TB_MISSAO_INTERCEPTACAO",
                column: "ID_ALERTA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MISSAO_INTERCEPTACAO_ID_CHASER",
                table: "TB_MISSAO_INTERCEPTACAO",
                column: "ID_CHASER");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SATELITE_ID_EMPRESA",
                table: "TB_SATELITE",
                column: "ID_EMPRESA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_MISSAO_INTERCEPTACAO");

            migrationBuilder.DropTable(
                name: "TB_ALERTA_COLISAO");

            migrationBuilder.DropTable(
                name: "TB_CHASER");

            migrationBuilder.DropTable(
                name: "TB_DETRITO_ESPACIAL");

            migrationBuilder.DropTable(
                name: "TB_SATELITE");

            migrationBuilder.DropTable(
                name: "TB_EMPRESA_AEROESPACIAL");
        }
    }
}
