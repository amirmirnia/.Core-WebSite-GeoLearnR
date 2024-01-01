using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.TMU.Migrations
{
    public partial class initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileCourse",
                columns: table => new
                {
                    IdFC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdC = table.Column<int>(type: "int", nullable: false),
                    Lock = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    hour = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    minet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    secend = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCourse", x => x.IdFC);
                    table.ForeignKey(
                        name: "FK_FileCourse_Courses_IdC",
                        column: x => x.IdC,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileCourse_IdC",
                table: "FileCourse",
                column: "IdC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileCourse");
        }
    }
}
