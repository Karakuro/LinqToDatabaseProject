using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinqToDatabaseProject.Migrations
{
    /// <inheritdoc />
    public partial class CharLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterLevel",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterLevel",
                table: "Characters");
        }
    }
}
