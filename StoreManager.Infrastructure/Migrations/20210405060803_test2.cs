using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreManager.Infrastructure.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseClaimLineItem_ExpenseClaims_ExpenseClaimId",
                table: "ExpenseClaimLineItem");

            migrationBuilder.RenameColumn(
                name: "ExpenseClaimId",
                table: "ExpenseClaimLineItem",
                newName: "ExpenseClaimID");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseClaimLineItem_ExpenseClaimId",
                table: "ExpenseClaimLineItem",
                newName: "IX_ExpenseClaimLineItem_ExpenseClaimID");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseClaimID",
                table: "ExpenseClaimLineItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseClaimLineItem_ExpenseClaims_ExpenseClaimID",
                table: "ExpenseClaimLineItem",
                column: "ExpenseClaimID",
                principalTable: "ExpenseClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseClaimLineItem_ExpenseClaims_ExpenseClaimID",
                table: "ExpenseClaimLineItem");

            migrationBuilder.RenameColumn(
                name: "ExpenseClaimID",
                table: "ExpenseClaimLineItem",
                newName: "ExpenseClaimId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseClaimLineItem_ExpenseClaimID",
                table: "ExpenseClaimLineItem",
                newName: "IX_ExpenseClaimLineItem_ExpenseClaimId");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseClaimId",
                table: "ExpenseClaimLineItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseClaimLineItem_ExpenseClaims_ExpenseClaimId",
                table: "ExpenseClaimLineItem",
                column: "ExpenseClaimId",
                principalTable: "ExpenseClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
