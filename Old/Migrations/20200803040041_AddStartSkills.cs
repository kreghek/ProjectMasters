using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMasters.Migrations
{
    public partial class AddStartSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SkillSchemes",
                columns: new[] { "Id", "FeatureTaskId", "Name" },
                values: new object[] { -1L, null, "Frontend" });

            migrationBuilder.InsertData(
                table: "SkillSchemes",
                columns: new[] { "Id", "FeatureTaskId", "Name" },
                values: new object[] { -2L, null, "Backend" });

            migrationBuilder.InsertData(
                table: "SkillSchemes",
                columns: new[] { "Id", "FeatureTaskId", "Name" },
                values: new object[] { -3L, null, "Mobile" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkillSchemes",
                keyColumn: "Id",
                keyValue: -3L);

            migrationBuilder.DeleteData(
                table: "SkillSchemes",
                keyColumn: "Id",
                keyValue: -2L);

            migrationBuilder.DeleteData(
                table: "SkillSchemes",
                keyColumn: "Id",
                keyValue: -1L);
        }
    }
}
