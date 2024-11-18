using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LACrimes.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Lat = table.Column<double>(type: "double precision", precision: 7, scale: 4, nullable: false),
                    Lon = table.Column<double>(type: "double precision", precision: 7, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Crime",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Desc = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Premis",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Desc = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premis", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Desc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Street",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Victim",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Sex = table.Column<char>(type: "character(1)", nullable: false),
                    Descent = table.Column<char>(type: "character(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Victim", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Weapon",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Desc = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubArea",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    RpdDistNo = table.Column<int>(type: "integer", nullable: false),
                    AreaID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubArea", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubArea_Area_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Area",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrimeRecord",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    DrNo = table.Column<int>(type: "integer", nullable: false),
                    DateRptd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOcc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubAreaID = table.Column<Guid>(type: "uuid", nullable: false),
                    VictimID = table.Column<Guid>(type: "uuid", nullable: false),
                    PremisID = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusID = table.Column<Guid>(type: "uuid", nullable: false),
                    WeaponID = table.Column<Guid>(type: "uuid", nullable: false),
                    StreetID = table.Column<Guid>(type: "uuid", nullable: false),
                    CrossStreetID = table.Column<Guid>(type: "uuid", nullable: false),
                    CoordinatesID = table.Column<Guid>(type: "uuid", nullable: false),
                    Crime1ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Crime2ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Crime3ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrimeRecord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Coordinates_CoordinatesID",
                        column: x => x.CoordinatesID,
                        principalTable: "Coordinates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Crime_Crime1ID",
                        column: x => x.Crime1ID,
                        principalTable: "Crime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Crime_Crime2ID",
                        column: x => x.Crime2ID,
                        principalTable: "Crime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Crime_Crime3ID",
                        column: x => x.Crime3ID,
                        principalTable: "Crime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Premis_PremisID",
                        column: x => x.PremisID,
                        principalTable: "Premis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Street_CrossStreetID",
                        column: x => x.CrossStreetID,
                        principalTable: "Street",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Street_StreetID",
                        column: x => x.StreetID,
                        principalTable: "Street",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_SubArea_SubAreaID",
                        column: x => x.SubAreaID,
                        principalTable: "SubArea",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Victim_VictimID",
                        column: x => x.VictimID,
                        principalTable: "Victim",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeRecord_Weapon_WeaponID",
                        column: x => x.WeaponID,
                        principalTable: "Weapon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_CoordinatesID",
                table: "CrimeRecord",
                column: "CoordinatesID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_Crime1ID",
                table: "CrimeRecord",
                column: "Crime1ID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_Crime2ID",
                table: "CrimeRecord",
                column: "Crime2ID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_Crime3ID",
                table: "CrimeRecord",
                column: "Crime3ID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_CrossStreetID",
                table: "CrimeRecord",
                column: "CrossStreetID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_DrNo",
                table: "CrimeRecord",
                column: "DrNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_ID",
                table: "CrimeRecord",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_PremisID",
                table: "CrimeRecord",
                column: "PremisID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_StatusID",
                table: "CrimeRecord",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_StreetID",
                table: "CrimeRecord",
                column: "StreetID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_SubAreaID",
                table: "CrimeRecord",
                column: "SubAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_VictimID",
                table: "CrimeRecord",
                column: "VictimID");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeRecord_WeaponID",
                table: "CrimeRecord",
                column: "WeaponID");

            migrationBuilder.CreateIndex(
                name: "IX_SubArea_AreaID",
                table: "SubArea",
                column: "AreaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrimeRecord");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "Crime");

            migrationBuilder.DropTable(
                name: "Premis");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Street");

            migrationBuilder.DropTable(
                name: "SubArea");

            migrationBuilder.DropTable(
                name: "Victim");

            migrationBuilder.DropTable(
                name: "Weapon");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
