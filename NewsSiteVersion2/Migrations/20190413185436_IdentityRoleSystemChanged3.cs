using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsSiteVersion2.Migrations
{
    public partial class IdentityRoleSystemChanged3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserRole");

            migrationBuilder.AddColumn<string>(
                name: "CustomRoleId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomRoleId1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CustomRoleId",
                table: "AspNetUsers",
                column: "CustomRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CustomRoleId1",
                table: "AspNetUsers",
                column: "CustomRoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_CustomRoleId",
                table: "AspNetUsers",
                column: "CustomRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_CustomRoleId1",
                table: "AspNetUsers",
                column: "CustomRoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_CustomRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_CustomRoleId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CustomRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CustomRoleId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomRoleId1",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationUserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_RoleId",
                table: "ApplicationUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_UserId",
                table: "ApplicationUserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_UserId1",
                table: "ApplicationUserRole",
                column: "UserId1");
        }
    }
}
