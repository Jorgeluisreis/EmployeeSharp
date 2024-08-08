using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeSharp.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Cargos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaboradores_Cargos_CargoId",
                table: "Colaboradores");

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Colaboradores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaboradores_Cargos_CargoId",
                table: "Colaboradores",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaboradores_Cargos_CargoId",
                table: "Colaboradores");

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Colaboradores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Desenvolvedor" },
                    { 2, "Analista" },
                    { 3, "Gerente" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Colaboradores_Cargos_CargoId",
                table: "Colaboradores",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
