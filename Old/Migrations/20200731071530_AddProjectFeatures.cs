using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMasters.Migrations
{
    public partial class AddProjectFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feature_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeatureTask",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Progress = table.Column<float>(nullable: false),
                    Estimate = table.Column<float>(nullable: false),
                    FeatureId = table.Column<long>(nullable: true),
                    FeatureTaskId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureTask_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeatureTask_FeatureTask_FeatureTaskId",
                        column: x => x.FeatureTaskId,
                        principalTable: "FeatureTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillScheme",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FeatureTaskId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillScheme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillScheme_FeatureTask_FeatureTaskId",
                        column: x => x.FeatureTaskId,
                        principalTable: "FeatureTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ProjectId",
                table: "Feature",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureTask_FeatureId",
                table: "FeatureTask",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureTask_FeatureTaskId",
                table: "FeatureTask",
                column: "FeatureTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillScheme_FeatureTaskId",
                table: "SkillScheme",
                column: "FeatureTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillScheme");

            migrationBuilder.DropTable(
                name: "FeatureTask");

            migrationBuilder.DropTable(
                name: "Feature");
        }
    }
}
