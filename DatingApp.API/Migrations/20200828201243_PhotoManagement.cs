using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class PhotoManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //     table: "AspNetUserRoles");

            // migrationBuilder.DropIndex(
            //     name: "IX_AspNetUserRoles_UserId1",
            //     table: "AspNetUserRoles");

            // migrationBuilder.DropColumn(
            //     name: "UserId1",
            //     table: "AspNetUserRoles");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Photos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Photos");

            // migrationBuilder.AddColumn<int>(
            //     name: "UserId1",
            //     table: "AspNetUserRoles",
            //     type: "INTEGER",
            //     nullable: true);

            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserRoles_UserId1",
            //     table: "AspNetUserRoles",
            //     column: "UserId1");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //     table: "AspNetUserRoles",
            //     column: "UserId1",
            //     principalTable: "AspNetUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);
        }
    }
}
