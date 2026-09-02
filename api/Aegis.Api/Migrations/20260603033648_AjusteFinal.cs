using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aegis.Api.Migrations
{
    /// <inheritdoc />
    public partial class AjusteFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_MISSAO_INTERCEPTACAO_TB_CHASER_ChaserId1",
                table: "TB_MISSAO_INTERCEPTACAO");

            migrationBuilder.DropIndex(
                name: "IX_TB_MISSAO_INTERCEPTACAO_ChaserId1",
                table: "TB_MISSAO_INTERCEPTACAO");

            migrationBuilder.DropColumn(
                name: "ChaserId1",
                table: "TB_MISSAO_INTERCEPTACAO");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUMERO_ALTITUDE_KM",
                table: "TB_SATELITE",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_Z",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_Y",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_X",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TAMANHO_METROS",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MASSA_KG",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_Z",
                table: "TB_CHASER",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_Y",
                table: "TB_CHASER",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_BATERIA",
                table: "TB_CHASER",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NUMERO_ALTITUDE_KM",
                table: "TB_SATELITE",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AddColumn<int>(
                name: "ChaserId1",
                table: "TB_MISSAO_INTERCEPTACAO",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_Z",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_Y",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_X",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TAMANHO_METROS",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MASSA_KG",
                table: "TB_DETRITO_ESPACIAL",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_Z",
                table: "TB_CHASER",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TXT_COORDENADA_Y",
                table: "TB_CHASER",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_BATERIA",
                table: "TB_CHASER",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MISSAO_INTERCEPTACAO_ChaserId1",
                table: "TB_MISSAO_INTERCEPTACAO",
                column: "ChaserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_MISSAO_INTERCEPTACAO_TB_CHASER_ChaserId1",
                table: "TB_MISSAO_INTERCEPTACAO",
                column: "ChaserId1",
                principalTable: "TB_CHASER",
                principalColumn: "ID_CHASER");
        }
    }
}
