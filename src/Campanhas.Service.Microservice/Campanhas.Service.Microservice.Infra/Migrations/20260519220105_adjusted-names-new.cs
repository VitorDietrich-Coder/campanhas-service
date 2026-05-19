using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campanhas.Service.Microservice.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adjustednamesnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Campaigns_CampaignId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Usuarios_UserId",
                table: "Donations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Donations",
                table: "Donations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns");

            migrationBuilder.RenameTable(
                name: "Donations",
                newName: "Doacoes");

            migrationBuilder.RenameTable(
                name: "Campaigns",
                newName: "Campanhas");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_UserId",
                table: "Doacoes",
                newName: "IX_Doacoes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_CampaignId",
                table: "Doacoes",
                newName: "IX_Doacoes_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doacoes",
                table: "Doacoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campanhas",
                table: "Campanhas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doacoes_Campanhas_CampaignId",
                table: "Doacoes",
                column: "CampaignId",
                principalTable: "Campanhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doacoes_Usuarios_UserId",
                table: "Doacoes",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doacoes_Campanhas_CampaignId",
                table: "Doacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Doacoes_Usuarios_UserId",
                table: "Doacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doacoes",
                table: "Doacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Campanhas",
                table: "Campanhas");

            migrationBuilder.RenameTable(
                name: "Doacoes",
                newName: "Donations");

            migrationBuilder.RenameTable(
                name: "Campanhas",
                newName: "Campaigns");

            migrationBuilder.RenameIndex(
                name: "IX_Doacoes_UserId",
                table: "Donations",
                newName: "IX_Donations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Doacoes_CampaignId",
                table: "Donations",
                newName: "IX_Donations_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Donations",
                table: "Donations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Campaigns_CampaignId",
                table: "Donations",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Usuarios_UserId",
                table: "Donations",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
