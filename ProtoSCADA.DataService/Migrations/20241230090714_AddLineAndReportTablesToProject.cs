using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtoSCADA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLineAndReportTablesToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportID",
                table: "Metrics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LineID",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportID",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    FactoryID = table.Column<int>(type: "int", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reports_Factorys_FactoryID",
                        column: x => x.FactoryID,
                        principalTable: "Factorys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Users_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactoryID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SupervisorID = table.Column<int>(type: "int", nullable: false),
                    ReportID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lines_Factorys_FactoryID",
                        column: x => x.FactoryID,
                        principalTable: "Factorys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lines_Reports_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Reports",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lines_Users_SupervisorID",
                        column: x => x.SupervisorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReportID",
                table: "Metrics",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_LineID",
                table: "Machines",
                column: "LineID");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_ReportID",
                table: "Machines",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_FactoryID",
                table: "Lines",
                column: "FactoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_ReportID",
                table: "Lines",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_SupervisorID",
                table: "Lines",
                column: "SupervisorID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CreatedByUserID",
                table: "Reports",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_FactoryID",
                table: "Reports",
                column: "FactoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Lines_LineID",
                table: "Machines",
                column: "LineID",
                principalTable: "Lines",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Reports_ReportID",
                table: "Machines",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Reports_ReportID",
                table: "Metrics",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Lines_LineID",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Reports_ReportID",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Reports_ReportID",
                table: "Metrics");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_ReportID",
                table: "Metrics");

            migrationBuilder.DropIndex(
                name: "IX_Machines_LineID",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_ReportID",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "LineID",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "Machines");
        }
    }
}
