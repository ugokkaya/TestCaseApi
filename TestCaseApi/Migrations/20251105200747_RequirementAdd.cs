using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestCaseApi.Migrations
{
    /// <inheritdoc />
    public partial class RequirementAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "requirement",
                table: "test_cases",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requirement",
                table: "test_cases");
        }
    }
}
