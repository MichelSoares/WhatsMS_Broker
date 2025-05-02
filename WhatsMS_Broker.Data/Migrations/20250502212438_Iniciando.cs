using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WhatsMS_Broker.Data.Migrations
{
    /// <inheritdoc />
    public partial class Iniciando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    session_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    webhook_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    auth_token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    client_session_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    port_run = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    qrcode_base64 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_message_inbound",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMessageWhatsApp = table.Column<string>(type: "text", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    date_received = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    from_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    to_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    message_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    midia_content_type = table.Column<string>(type: "text", nullable: true),
                    midia_url = table.Column<string>(type: "text", nullable: true),
                    profile_name = table.Column<string>(type: "text", nullable: true),
                    notify_name = table.Column<string>(type: "text", nullable: true),
                    author = table.Column<string>(type: "text", nullable: true),
                    latitude = table.Column<double>(type: "numeric(10,6)", nullable: true),
                    longitude = table.Column<double>(type: "numeric(10,6)", nullable: true),
                    is_forwarded = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    forwarding_score = table.Column<int>(type: "integer", nullable: true),
                    is_group = table.Column<bool>(type: "boolean", nullable: false),
                    is_coletado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_message_inbound", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_message_inbound_tb_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "tb_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_message_outbound",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    from_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    to_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    message_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_message_outbound", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_message_outbound_tb_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "tb_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_inbound_AccountId",
                table: "tb_message_inbound",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_inbound_date_received",
                table: "tb_message_inbound",
                column: "date_received");

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_outbound_AccountId",
                table: "tb_message_outbound",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_outbound_Status",
                table: "tb_message_outbound",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_message_inbound");

            migrationBuilder.DropTable(
                name: "tb_message_outbound");

            migrationBuilder.DropTable(
                name: "tb_accounts");
        }
    }
}
