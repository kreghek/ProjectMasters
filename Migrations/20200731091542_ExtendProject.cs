using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMasters.Migrations
{
    public partial class ExtendProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillScheme_FeatureTask_FeatureTaskId",
                table: "SkillScheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillScheme",
                table: "SkillScheme");

            migrationBuilder.RenameTable(
                name: "SkillScheme",
                newName: "SkillSchemes");

            migrationBuilder.RenameIndex(
                name: "IX_SkillScheme_FeatureTaskId",
                table: "SkillSchemes",
                newName: "IX_SkillSchemes_FeatureTaskId");

            migrationBuilder.AddColumn<float>(
                name: "Logged",
                table: "FeatureTask",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "RealValue",
                table: "FeatureTask",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillSchemes",
                table: "SkillSchemes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FeatureTaskId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_FeatureTask_FeatureTaskId",
                        column: x => x.FeatureTaskId,
                        principalTable: "FeatureTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchemeId = table.Column<long>(nullable: true),
                    Value = table.Column<float>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Skill_SkillSchemes_SchemeId",
                        column: x => x.SchemeId,
                        principalTable: "SkillSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_FeatureTaskId",
                table: "Employee",
                column: "FeatureTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_EmployeeId",
                table: "Skill",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_SchemeId",
                table: "Skill",
                column: "SchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillSchemes_FeatureTask_FeatureTaskId",
                table: "SkillSchemes",
                column: "FeatureTaskId",
                principalTable: "FeatureTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillSchemes_FeatureTask_FeatureTaskId",
                table: "SkillSchemes");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillSchemes",
                table: "SkillSchemes");

            migrationBuilder.DropColumn(
                name: "Logged",
                table: "FeatureTask");

            migrationBuilder.DropColumn(
                name: "RealValue",
                table: "FeatureTask");

            migrationBuilder.RenameTable(
                name: "SkillSchemes",
                newName: "SkillScheme");

            migrationBuilder.RenameIndex(
                name: "IX_SkillSchemes_FeatureTaskId",
                table: "SkillScheme",
                newName: "IX_SkillScheme_FeatureTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillScheme",
                table: "SkillScheme",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillScheme_FeatureTask_FeatureTaskId",
                table: "SkillScheme",
                column: "FeatureTaskId",
                principalTable: "FeatureTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
