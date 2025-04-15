using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsMS_Broker.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClientSessionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "client_session_id",
                table: "tb_accounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_session_id",
                table: "tb_accounts");
        }
    }
}
