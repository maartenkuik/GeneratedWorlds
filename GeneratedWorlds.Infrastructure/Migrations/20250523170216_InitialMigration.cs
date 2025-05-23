using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneratedWorlds.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Reference);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Reference);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSkills",
                columns: table => new
                {
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillType = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkills", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_CharacterSkills_Characters_CharacterReference",
                        column: x => x.CharacterReference,
                        principalTable: "Characters",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Characters_CharacterReference",
                        column: x => x.CharacterReference,
                        principalTable: "Characters",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Potions",
                columns: table => new
                {
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedSkill = table.Column<int>(type: "int", nullable: false),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potions", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_Potions_Items_Reference",
                        column: x => x.Reference,
                        principalTable: "Items",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterReference",
                table: "CharacterSkills",
                column: "CharacterReference");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_CharacterReference",
                table: "InventoryItems",
                column: "CharacterReference");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkills");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "Potions");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
