using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LendACarAPI.Migrations
{
    /// <inheritdoc />
    public partial class dbPreparationSprint3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleCategories_VehicleCategoryID",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "VehicleModels");

            migrationBuilder.RenameColumn(
                name: "VehicleCategoryID",
                table: "VehicleModels",
                newName: "VehicleBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_VehicleCategoryID",
                table: "VehicleModels",
                newName: "IX_VehicleModels_VehicleBrandId");

            migrationBuilder.AlterColumn<int>(
                name: "MaximumLoad",
                table: "Vehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "EngineDisplacement",
                table: "Vehicles",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AirConditioning",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "VehicleCategoryID",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VehicleCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "VehicleBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrand", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleCategoryID",
                table: "Vehicles",
                column: "VehicleCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleBrand_VehicleBrandId",
                table: "VehicleModels",
                column: "VehicleBrandId",
                principalTable: "VehicleBrand",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleCategories_VehicleCategoryID",
                table: "Vehicles",
                column: "VehicleCategoryID",
                principalTable: "VehicleCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleBrand_VehicleBrandId",
                table: "VehicleModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleCategories_VehicleCategoryID",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleBrand");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleCategoryID",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleCategoryID",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "VehicleBrandId",
                table: "VehicleModels",
                newName: "VehicleCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_VehicleBrandId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_VehicleCategoryID");

            migrationBuilder.AlterColumn<string>(
                name: "MaximumLoad",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EngineDisplacement",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AirConditioning",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VehicleCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleCategories_VehicleCategoryID",
                table: "VehicleModels",
                column: "VehicleCategoryID",
                principalTable: "VehicleCategories",
                principalColumn: "Id");
        }
    }
}
