using Microsoft.EntityFrameworkCore.Migrations;

namespace Database_Test.Migrations
{
    public partial class databasetests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParentDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    GrandChildrenId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentDatabase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrandChildDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrandChildrenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrandChildDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrandChildDatabase_ParentDatabase_GrandChildrenId",
                        column: x => x.GrandChildrenId,
                        principalTable: "ParentDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChildDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrandChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildDatabase_GrandChildDatabase_GrandChildId",
                        column: x => x.GrandChildId,
                        principalTable: "GrandChildDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildDatabase_GrandChildId",
                table: "ChildDatabase",
                column: "GrandChildId");

            migrationBuilder.CreateIndex(
                name: "IX_GrandChildDatabase_GrandChildrenId",
                table: "GrandChildDatabase",
                column: "GrandChildrenId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentDatabase_ChildId",
                table: "ParentDatabase",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentDatabase_ChildDatabase_ChildId",
                table: "ParentDatabase",
                column: "ChildId",
                principalTable: "ChildDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildDatabase_GrandChildDatabase_GrandChildId",
                table: "ChildDatabase");

            migrationBuilder.DropTable(
                name: "GrandChildDatabase");

            migrationBuilder.DropTable(
                name: "ParentDatabase");

            migrationBuilder.DropTable(
                name: "ChildDatabase");
        }
    }
}
