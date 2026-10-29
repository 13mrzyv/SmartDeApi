using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartDeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCampaignStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "campaigns",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Active");

            migrationBuilder.Sql("UPDATE campaigns SET status = CASE WHEN is_active THEN 'Active' ELSE 'Inactive' END");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "campaigns");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "campaigns",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldDefaultValue: "Active");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "campaigns",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.Sql("UPDATE campaigns SET is_active = (status = 'Active')");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "campaigns",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.DropColumn(
                name: "status",
                table: "campaigns");
        }
    }
}
