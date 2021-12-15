using Microsoft.EntityFrameworkCore.Migrations;

namespace Database_Test.Migrations
{
    public partial class databasetest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CloneParentDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloneParentDatabase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParentDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: true),
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
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrandChildDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrandChildDatabase_ParentDatabase_ParentId",
                        column: x => x.ParentId,
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
                    GrandChildId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildDatabase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildDatabase_GrandChildDatabase_GrandChildId",
                        column: x => x.GrandChildId,
                        principalTable: "GrandChildDatabase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildDatabase_GrandChildId",
                table: "ChildDatabase",
                column: "GrandChildId");

            migrationBuilder.CreateIndex(
                name: "IX_CloneParentDatabase_ChildId",
                table: "CloneParentDatabase",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_GrandChildDatabase_ParentId",
                table: "GrandChildDatabase",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentDatabase_ChildId",
                table: "ParentDatabase",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_CloneParentDatabase_ChildDatabase_ChildId",
                table: "CloneParentDatabase",
                column: "ChildId",
                principalTable: "ChildDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentDatabase_ChildDatabase_ChildId",
                table: "ParentDatabase",
                column: "ChildId",
                principalTable: "ChildDatabase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildDatabase_GrandChildDatabase_GrandChildId",
                table: "ChildDatabase");

            migrationBuilder.DropTable(
                name: "CloneParentDatabase");

            migrationBuilder.DropTable(
                name: "GrandChildDatabase");

            migrationBuilder.DropTable(
                name: "ParentDatabase");

            migrationBuilder.DropTable(
                name: "ChildDatabase");
        }
    }
}
