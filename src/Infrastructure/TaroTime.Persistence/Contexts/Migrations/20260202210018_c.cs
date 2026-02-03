using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaroTime.Persistence.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class c : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompatibilityZodiacs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpertId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserZodiac = table.Column<int>(type: "int", nullable: false),
                    PartnerZodiac = table.Column<int>(type: "int", nullable: false),
                    UserZodiacId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerZodiacId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartnerBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompatibilityPercent = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompatibilityZodiacs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompatibilityZodiacs_AspNetUsers_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompatibilityZodiacs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompatibilityZodiacs_ExpertId",
                table: "CompatibilityZodiacs",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_CompatibilityZodiacs_UserId",
                table: "CompatibilityZodiacs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompatibilityZodiacs");
        }
    }
}
