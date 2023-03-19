using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimplyMVC.Migrations
{
    public partial class Commentsv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublished",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePublished",
                table: "Comments");
        }
    }
}
