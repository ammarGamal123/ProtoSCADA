using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtoSCADA.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipsAndSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Machines_MachineID",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Machines_MachineID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Factorys_FactoryID",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Reports_ReportID",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Users_SupervisorID",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Factorys_FactoryID",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Lines_LineID",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Reports_ReportID",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Machines_MachineID",
                table: "Metrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Reports_ReportID",
                table: "Metrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Factorys_FactoryID",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_CreatedByUserID",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Factorys_FactoryID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_ReportID",
                table: "Metrics");

            migrationBuilder.DropIndex(
                name: "IX_Machines_ReportID",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_ReportID",
                table: "Lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factorys",
                table: "Factorys");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Factorys",
                newName: "Factories");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Reports",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_FactoryID",
                table: "Reports",
                newName: "IX_Report_FactoryID");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Metrics",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastMaintance",
                table: "Machines",
                newName: "LastMaintenance");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_FactoryID",
                table: "Machines",
                newName: "IX_Machine_FactoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Lines_FactoryID",
                table: "Lines",
                newName: "IX_Line_FactoryID");

            migrationBuilder.RenameColumn(
                name: "ThersholdValue",
                table: "Alerts",
                newName: "ThresholdValue");

            migrationBuilder.AlterColumn<int>(
                name: "FactoryID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LineID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Remove the duplicate RoleID column addition
            // migrationBuilder.AddColumn<int>(
            //     name: "RoleID",
            //     table: "Users",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "LineID",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LineID",
                table: "Machines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Machines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Lines",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Lines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LineNumber",
                table: "Lines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<int>(
                name: "FactoryID",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LineID",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Alerts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "FactoryID",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LineID",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factories",
                table: "Factories",
                column: "ID");

                        migrationBuilder.CreateIndex(
                name: "IX_Users_LineID",
                table: "Users",
                column: "LineID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LineID",
                table: "Reports",
                column: "LineID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_FactoryID",
                table: "Events",
                column: "FactoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LineID",
                table: "Events",
                column: "LineID");

            migrationBuilder.CreateIndex(
                name: "IX_Alert_FactoryID",
                table: "Alerts",
                column: "FactoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_LineID",
                table: "Alerts",
                column: "LineID");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Factories_FactoryID",
                table: "Alerts",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Lines_LineID",
                table: "Alerts",
                column: "LineID",
                principalTable: "Lines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Machines_MachineID",
                table: "Alerts",
                column: "MachineID",
                principalTable: "Machines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Factories_FactoryID",
                table: "Events",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Lines_LineID",
                table: "Events",
                column: "LineID",
                principalTable: "Lines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Machines_MachineID",
                table: "Events",
                column: "MachineID",
                principalTable: "Machines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Factories_FactoryID",
                table: "Lines",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Users_SupervisorID",
                table: "Lines",
                column: "SupervisorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Factories_FactoryID",
                table: "Machines",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Lines_LineID",
                table: "Machines",
                column: "LineID",
                principalTable: "Lines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Machines_MachineID",
                table: "Metrics",
                column: "MachineID",
                principalTable: "Machines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Factories_FactoryID",
                table: "Reports",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Lines_LineID",
                table: "Reports",
                column: "LineID",
                principalTable: "Lines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_CreatedByUserID",
                table: "Reports",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Factories_FactoryID",
                table: "Users",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Lines_LineID",
                table: "Users",
                column: "LineID",
                principalTable: "Lines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleID",
                table: "Users",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Factories_FactoryID",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Lines_LineID",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Machines_MachineID",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Factories_FactoryID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Lines_LineID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Machines_MachineID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Factories_FactoryID",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Users_SupervisorID",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Factories_FactoryID",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Lines_LineID",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Machines_MachineID",
                table: "Metrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Factories_FactoryID",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Lines_LineID",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_CreatedByUserID",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Factories_FactoryID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Lines_LineID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_LineID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reports_LineID",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Event_FactoryID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_LineID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Alert_FactoryID",
                table: "Alerts");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_LineID",
                table: "Alerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factories",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "LineID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LineID",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "LineNumber",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FactoryID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LineID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FactoryID",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "LineID",
                table: "Alerts");

            migrationBuilder.RenameTable(
                name: "Factories",
                newName: "Factorys");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Reports",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_Report_FactoryID",
                table: "Reports",
                newName: "IX_Reports_FactoryID");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Metrics",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "LastMaintenance",
                table: "Machines",
                newName: "LastMaintance");

            migrationBuilder.RenameIndex(
                name: "IX_Machine_FactoryID",
                table: "Machines",
                newName: "IX_Machines_FactoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Line_FactoryID",
                table: "Lines",
                newName: "IX_Lines_FactoryID");

            migrationBuilder.RenameColumn(
                name: "ThresholdValue",
                table: "Alerts",
                newName: "ThersholdValue");

            migrationBuilder.AlterColumn<int>(
                name: "FactoryID",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Reports",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ReportID",
                table: "Metrics",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LineID",
                table: "Machines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ReportID",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Lines",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<int>(
                name: "ReportID",
                table: "Lines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Alerts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factorys",
                table: "Factorys",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReportID",
                table: "Metrics",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_ReportID",
                table: "Machines",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_ReportID",
                table: "Lines",
                column: "ReportID");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Machines_MachineID",
                table: "Alerts",
                column: "MachineID",
                principalTable: "Machines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Machines_MachineID",
                table: "Events",
                column: "MachineID",
                principalTable: "Machines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Factorys_FactoryID",
                table: "Lines",
                column: "FactoryID",
                principalTable: "Factorys",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Reports_ReportID",
                table: "Lines",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Users_SupervisorID",
                table: "Lines",
                column: "SupervisorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Factorys_FactoryID",
                table: "Machines",
                column: "FactoryID",
                principalTable: "Factorys",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Metrics_Machines_MachineID",
                table: "Metrics",
                column: "MachineID",
                principalTable: "Machines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Reports_ReportID",
                table: "Metrics",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Factorys_FactoryID",
                table: "Reports",
                column: "FactoryID",
                principalTable: "Factorys",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_CreatedByUserID",
                table: "Reports",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Factorys_FactoryID",
                table: "Users",
                column: "FactoryID",
                principalTable: "Factorys",
                principalColumn: "ID");
        }
    }
}
