using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMasters.Migrations
{
    public partial class AddRequiredSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillSchemes_FeatureTask_FeatureTaskId",
                table: "SkillSchemes");

            migrationBuilder.DropIndex(
                name: "IX_SkillSchemes_FeatureTaskId",
                table: "SkillSchemes");

            migrationBuilder.DropColumn(
                name: "FeatureTaskId",
                table: "SkillSchemes");

            migrationBuilder.CreateTable(
                name: "RequiredSkill",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillSchemeId = table.Column<long>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    FeatureTaskId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequiredSkill_FeatureTask_FeatureTaskId",
                        column: x => x.FeatureTaskId,
                        principalTable: "FeatureTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequiredSkill_SkillSchemes_SkillSchemeId",
                        column: x => x.SkillSchemeId,
                        principalTable: "SkillSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequiredSkill_FeatureTaskId",
                table: "RequiredSkill",
                column: "FeatureTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredSkill_SkillSchemeId",
                table: "RequiredSkill",
                column: "SkillSchemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequiredSkill");

            migrationBuilder.AddColumn<long>(
                name: "FeatureTaskId",
                table: "SkillSchemes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillSchemes_FeatureTaskId",
                table: "SkillSchemes",
                column: "FeatureTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillSchemes_FeatureTask_FeatureTaskId",
                table: "SkillSchemes",
                column: "FeatureTaskId",
                principalTable: "FeatureTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
