using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestCaseApi.Migrations
{
    /// <inheritdoc />
    public partial class MetricsAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "completion_tokens",
                table: "test_cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "eval_duration_ms",
                table: "test_cases",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "latency_ms",
                table: "test_cases",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "prompt_eval_duration_ms",
                table: "test_cases",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "prompt_tokens",
                table: "test_cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "total_duration_ms",
                table: "test_cases",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "total_tokens",
                table: "test_cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completion_tokens",
                table: "test_cases");

            migrationBuilder.DropColumn(
                name: "eval_duration_ms",
                table: "test_cases");

            migrationBuilder.DropColumn(
                name: "latency_ms",
                table: "test_cases");

            migrationBuilder.DropColumn(
                name: "prompt_eval_duration_ms",
                table: "test_cases");

            migrationBuilder.DropColumn(
                name: "prompt_tokens",
                table: "test_cases");

            migrationBuilder.DropColumn(
                name: "total_duration_ms",
                table: "test_cases");

            migrationBuilder.DropColumn(
                name: "total_tokens",
                table: "test_cases");
        }
    }
}
