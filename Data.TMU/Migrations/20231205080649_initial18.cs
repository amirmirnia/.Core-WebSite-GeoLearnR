using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.TMU.Migrations
{
    public partial class initial18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courseComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idC = table.Column<int>(type: "int", nullable: false),
                    idU = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAllow = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courseComments_Courses_idC",
                        column: x => x.idC,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_courseComments_Users_idU",
                        column: x => x.idU,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courseComments_idC",
                table: "courseComments",
                column: "idC");

            migrationBuilder.CreateIndex(
                name: "IX_courseComments_idU",
                table: "courseComments",
                column: "idU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseComments");
        }
    }
}
