using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.DAL.Migrations
{
    public partial class setnullabledateinroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Prescriptions_Prescription_ID",
                table: "Medicine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicine",
                table: "Medicine");

            migrationBuilder.RenameTable(
                name: "Medicine",
                newName: "Medicines");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_Prescription_ID",
                table: "Medicines",
                newName: "IX_Medicines_Prescription_ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Admission_Date",
                table: "Rooms",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicines",
                table: "Medicines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Prescriptions_Prescription_ID",
                table: "Medicines",
                column: "Prescription_ID",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Prescriptions_Prescription_ID",
                table: "Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicines",
                table: "Medicines");

            migrationBuilder.RenameTable(
                name: "Medicines",
                newName: "Medicine");

            migrationBuilder.RenameIndex(
                name: "IX_Medicines_Prescription_ID",
                table: "Medicine",
                newName: "IX_Medicine_Prescription_ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Admission_Date",
                table: "Rooms",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicine",
                table: "Medicine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Prescriptions_Prescription_ID",
                table: "Medicine",
                column: "Prescription_ID",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
