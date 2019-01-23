using Microsoft.EntityFrameworkCore.Migrations;

namespace Prueba_Crud.Migrations
{
    public partial class newModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tipoRiesgo",
                table: "Polisas",
                newName: "typeRisk");

            migrationBuilder.RenameColumn(
                name: "tipoCubrimiento",
                table: "Polisas",
                newName: "typeCoverings");

            migrationBuilder.RenameColumn(
                name: "precio",
                table: "Polisas",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "porcentaje",
                table: "Polisas",
                newName: "porcentage");

            migrationBuilder.RenameColumn(
                name: "fechaInicio",
                table: "Polisas",
                newName: "dateInitial");

            migrationBuilder.RenameColumn(
                name: "fechaFin",
                table: "Polisas",
                newName: "dateFinal");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Polisas",
                newName: "name");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Polisas",
                type: "VARCHAR(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Polisas");

            migrationBuilder.RenameColumn(
                name: "typeRisk",
                table: "Polisas",
                newName: "tipoRiesgo");

            migrationBuilder.RenameColumn(
                name: "typeCoverings",
                table: "Polisas",
                newName: "tipoCubrimiento");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Polisas",
                newName: "precio");

            migrationBuilder.RenameColumn(
                name: "porcentage",
                table: "Polisas",
                newName: "porcentaje");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Polisas",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "dateInitial",
                table: "Polisas",
                newName: "fechaInicio");

            migrationBuilder.RenameColumn(
                name: "dateFinal",
                table: "Polisas",
                newName: "fechaFin");
        }
    }
}
