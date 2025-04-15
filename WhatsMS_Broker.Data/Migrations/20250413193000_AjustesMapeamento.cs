using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsMS_Broker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesMapeamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageInbounds_Accounts_AccountId",
                table: "MessageInbounds");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageOutbounds_Accounts_AccountId",
                table: "MessageOutbounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageOutbounds",
                table: "MessageOutbounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageInbounds",
                table: "MessageInbounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "MessageOutbounds",
                newName: "tb_message_outbound");

            migrationBuilder.RenameTable(
                name: "MessageInbounds",
                newName: "tb_message_inbound");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "tb_accounts");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "tb_message_outbound",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_message_outbound",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ToNumber",
                table: "tb_message_outbound",
                newName: "to_number");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "tb_message_outbound",
                newName: "sent_at");

            migrationBuilder.RenameColumn(
                name: "MessageType",
                table: "tb_message_outbound",
                newName: "message_type");

            migrationBuilder.RenameColumn(
                name: "FromNumber",
                table: "tb_message_outbound",
                newName: "from_number");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_message_outbound",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_MessageOutbounds_AccountId",
                table: "tb_message_outbound",
                newName: "IX_tb_message_outbound_AccountId");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "tb_message_inbound",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_message_inbound",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ToNumber",
                table: "tb_message_inbound",
                newName: "to_number");

            migrationBuilder.RenameColumn(
                name: "MessageType",
                table: "tb_message_inbound",
                newName: "message_type");

            migrationBuilder.RenameColumn(
                name: "IsGroup",
                table: "tb_message_inbound",
                newName: "is_group");

            migrationBuilder.RenameColumn(
                name: "FromNumber",
                table: "tb_message_inbound",
                newName: "from_number");

            migrationBuilder.RenameColumn(
                name: "DateReceived",
                table: "tb_message_inbound",
                newName: "date_received");

            migrationBuilder.RenameIndex(
                name: "IX_MessageInbounds_AccountId",
                table: "tb_message_inbound",
                newName: "IX_tb_message_inbound_AccountId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_accounts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "WebhookUrl",
                table: "tb_accounts",
                newName: "webhook_url");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_accounts",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SessionName",
                table: "tb_accounts",
                newName: "session_name");

            migrationBuilder.RenameColumn(
                name: "QrCodeBase64",
                table: "tb_accounts",
                newName: "qrcode_base64");

            migrationBuilder.RenameColumn(
                name: "PortRun",
                table: "tb_accounts",
                newName: "port_run");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "tb_accounts",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "tb_accounts",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_accounts",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "tb_accounts",
                newName: "client_name");

            migrationBuilder.RenameColumn(
                name: "AuthToken",
                table: "tb_accounts",
                newName: "auth_token");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "tb_message_outbound",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "to_number",
                table: "tb_message_outbound",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "message_type",
                table: "tb_message_outbound",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "from_number",
                table: "tb_message_outbound",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "tb_message_outbound",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "to_number",
                table: "tb_message_inbound",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "message_type",
                table: "tb_message_inbound",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "from_number",
                table: "tb_message_inbound",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_received",
                table: "tb_message_inbound",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "webhook_url",
                table: "tb_accounts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "tb_accounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "session_name",
                table: "tb_accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "port_run",
                table: "tb_accounts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "tb_accounts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "tb_accounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "client_name",
                table: "tb_accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "auth_token",
                table: "tb_accounts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_message_outbound",
                table: "tb_message_outbound",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_message_inbound",
                table: "tb_message_inbound",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_accounts",
                table: "tb_accounts",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_outbound_Status",
                table: "tb_message_outbound",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_inbound_date_received",
                table: "tb_message_inbound",
                column: "date_received");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_message_inbound_tb_accounts_AccountId",
                table: "tb_message_inbound",
                column: "AccountId",
                principalTable: "tb_accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_message_outbound_tb_accounts_AccountId",
                table: "tb_message_outbound",
                column: "AccountId",
                principalTable: "tb_accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_message_inbound_tb_accounts_AccountId",
                table: "tb_message_inbound");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_message_outbound_tb_accounts_AccountId",
                table: "tb_message_outbound");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_message_outbound",
                table: "tb_message_outbound");

            migrationBuilder.DropIndex(
                name: "IX_tb_message_outbound_Status",
                table: "tb_message_outbound");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_message_inbound",
                table: "tb_message_inbound");

            migrationBuilder.DropIndex(
                name: "IX_tb_message_inbound_date_received",
                table: "tb_message_inbound");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_accounts",
                table: "tb_accounts");

            migrationBuilder.RenameTable(
                name: "tb_message_outbound",
                newName: "MessageOutbounds");

            migrationBuilder.RenameTable(
                name: "tb_message_inbound",
                newName: "MessageInbounds");

            migrationBuilder.RenameTable(
                name: "tb_accounts",
                newName: "Accounts");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "MessageOutbounds",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MessageOutbounds",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "to_number",
                table: "MessageOutbounds",
                newName: "ToNumber");

            migrationBuilder.RenameColumn(
                name: "sent_at",
                table: "MessageOutbounds",
                newName: "SentAt");

            migrationBuilder.RenameColumn(
                name: "message_type",
                table: "MessageOutbounds",
                newName: "MessageType");

            migrationBuilder.RenameColumn(
                name: "from_number",
                table: "MessageOutbounds",
                newName: "FromNumber");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "MessageOutbounds",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_tb_message_outbound_AccountId",
                table: "MessageOutbounds",
                newName: "IX_MessageOutbounds_AccountId");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "MessageInbounds",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MessageInbounds",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "to_number",
                table: "MessageInbounds",
                newName: "ToNumber");

            migrationBuilder.RenameColumn(
                name: "message_type",
                table: "MessageInbounds",
                newName: "MessageType");

            migrationBuilder.RenameColumn(
                name: "is_group",
                table: "MessageInbounds",
                newName: "IsGroup");

            migrationBuilder.RenameColumn(
                name: "from_number",
                table: "MessageInbounds",
                newName: "FromNumber");

            migrationBuilder.RenameColumn(
                name: "date_received",
                table: "MessageInbounds",
                newName: "DateReceived");

            migrationBuilder.RenameIndex(
                name: "IX_tb_message_inbound_AccountId",
                table: "MessageInbounds",
                newName: "IX_MessageInbounds_AccountId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Accounts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "webhook_url",
                table: "Accounts",
                newName: "WebhookUrl");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Accounts",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "session_name",
                table: "Accounts",
                newName: "SessionName");

            migrationBuilder.RenameColumn(
                name: "qrcode_base64",
                table: "Accounts",
                newName: "QrCodeBase64");

            migrationBuilder.RenameColumn(
                name: "port_run",
                table: "Accounts",
                newName: "PortRun");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Accounts",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Accounts",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Accounts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "client_name",
                table: "Accounts",
                newName: "ClientName");

            migrationBuilder.RenameColumn(
                name: "auth_token",
                table: "Accounts",
                newName: "AuthToken");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "MessageOutbounds",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ToNumber",
                table: "MessageOutbounds",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "MessageType",
                table: "MessageOutbounds",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FromNumber",
                table: "MessageOutbounds",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "MessageOutbounds",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "ToNumber",
                table: "MessageInbounds",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "MessageType",
                table: "MessageInbounds",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FromNumber",
                table: "MessageInbounds",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReceived",
                table: "MessageInbounds",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "WebhookUrl",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "SessionName",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PortRun",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AuthToken",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageOutbounds",
                table: "MessageOutbounds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageInbounds",
                table: "MessageInbounds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageInbounds_Accounts_AccountId",
                table: "MessageInbounds",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageOutbounds_Accounts_AccountId",
                table: "MessageOutbounds",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
