using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.TMU.Migrations
{
    public partial class initial20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_walletType_walletTypeTypeId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_walletTypeTypeId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "walletTypeTypeId",
                table: "Wallet");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_TypeId",
                table: "Wallet",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_walletType_TypeId",
                table: "Wallet",
                column: "TypeId",
                principalTable: "walletType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_walletType_TypeId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_TypeId",
                table: "Wallet");

            migrationBuilder.AddColumn<int>(
                name: "walletTypeTypeId",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_walletTypeTypeId",
                table: "Wallet",
                column: "walletTypeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_walletType_walletTypeTypeId",
                table: "Wallet",
                column: "walletTypeTypeId",
                principalTable: "walletType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
