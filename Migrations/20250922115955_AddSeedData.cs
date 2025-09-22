using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLEPFinancialSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // In the migration's Up() method, add:
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Email", "PasswordHash", "Role", "FullName", "IsActive", "CreatedAt" },
                values: new object[,]
                {
        { "admin", "admin@blep.gov.pk", "admin123", "WorldBank", "System Administrator", true, DateTime.Now },
        { "projectdirector", "director@blep.gov.pk", "director123", "ProjectDirector", "Project Director", true, DateTime.Now },
        { "finance", "finance@blep.gov.pk", "finance123", "FinanceOfficer", "Finance Officer", true, DateTime.Now }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
