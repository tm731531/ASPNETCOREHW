using Microsoft.EntityFrameworkCore.Migrations;

namespace _20200522.Migrations
{
    public partial class personCourseDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Person",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Course",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Course");
        }
    }
}
