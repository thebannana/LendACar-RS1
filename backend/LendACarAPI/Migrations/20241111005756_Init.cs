using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LendACarAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "PersonSequence");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAvatar = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleCategoryIcon = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [PersonSequence]"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    EmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleCategories_VehicleCategoryID",
                        column: x => x.VehicleCategoryID,
                        principalTable: "VehicleCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyEmployees",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyAdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPositionId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmployees", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_CompanyEmployees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyEmployees_CompanyPositions_CompanyPositionId",
                        column: x => x.CompanyPositionId,
                        principalTable: "CompanyPositions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyEmployees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyReviews",
                columns: table => new
                {
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyReviews", x => new { x.ReviewerId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_CompanyReviews_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyReviews_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DriversLicense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriversLicense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriversLicense_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserReviews",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReviews", x => new { x.ReviewerId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserReviews_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngineDisplacement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnginePower = table.Column<int>(type: "int", nullable: true),
                    TransmissionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirConditioning = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    TowingHitch = table.Column<bool>(type: "bit", nullable: false),
                    MaximumLoad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avaliable = table.Column<bool>(type: "bit", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false),
                    PricePerDay = table.Column<double>(type: "float", nullable: false),
                    VehicleModelID = table.Column<int>(type: "int", nullable: false),
                    VehicleOwnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_VehicleOwnerID",
                        column: x => x.VehicleOwnerID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleModels_VehicleModelID",
                        column: x => x.VehicleModelID,
                        principalTable: "VehicleModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false),
                    CompanyEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHours_CompanyEmployees_CompanyEmployeeId",
                        column: x => x.CompanyEmployeeId,
                        principalTable: "CompanyEmployees",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "VehicleImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleID = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleImages_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleReviews",
                columns: table => new
                {
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReviews", x => new { x.ReviewerId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_VehicleReviews_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleReviews_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_CompanyId",
                table: "CompanyEmployees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_CompanyPositionId",
                table: "CompanyEmployees",
                column: "CompanyPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReviews_CompanyId",
                table: "CompanyReviews",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DriversLicense_UserId",
                table: "DriversLicense",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReviews_UserId",
                table: "UserReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleImages_VehicleID",
                table: "VehicleImages",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleCategoryID",
                table: "VehicleModels",
                column: "VehicleCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReviews_VehicleId",
                table: "VehicleReviews",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleModelID",
                table: "Vehicles",
                column: "VehicleModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleOwnerID",
                table: "Vehicles",
                column: "VehicleOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_CompanyEmployeeId",
                table: "WorkingHours",
                column: "CompanyEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "CompanyReviews");

            migrationBuilder.DropTable(
                name: "DriversLicense");

            migrationBuilder.DropTable(
                name: "UserReviews");

            migrationBuilder.DropTable(
                name: "VehicleImages");

            migrationBuilder.DropTable(
                name: "VehicleReviews");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "CompanyEmployees");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyPositions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VehicleCategories");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropSequence(
                name: "PersonSequence");
        }
    }
}
