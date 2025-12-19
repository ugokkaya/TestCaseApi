using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestCaseApi.Migrations
{
    /// <inheritdoc />
    public partial class ModelUsedAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "model_used",
                table: "test_cases",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "model_used",
                table: "test_cases");
        }
    }
}
