using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLEPFinancialSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddFinancialModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Disbursements_DisbursementId",
                table: "Payments");

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    FiscalYear = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contractor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSafeguards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Findings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MitigationMeasures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSafeguards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialSafeguards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssessmentReport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialSafeguards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReconciliationBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountRequested = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RevisedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualExpenditure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Commitments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetLineItems_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractMilestones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractMilestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractMilestones_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractPayments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcurementPlanId = table.Column<int>(type: "int", nullable: false),
                    BidderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EvaluationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_ProcurementPlans_ProcurementPlanId",
                        column: x => x.ProcurementPlanId,
                        principalTable: "ProcurementPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialAccountTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialAccountId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialAccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialAccountTransactions_SpecialAccounts_SpecialAccountId",
                        column: x => x.SpecialAccountId,
                        principalTable: "SpecialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubProjectComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProjectComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProjectComponents_SubProjects_SubProjectId",
                        column: x => x.SubProjectId,
                        principalTable: "SubProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportingDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WithdrawalApplicationId = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportingDocuments_WithdrawalApplications_WithdrawalApplicationId",
                        column: x => x.WithdrawalApplicationId,
                        principalTable: "WithdrawalApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ProcurementPlanId",
                table: "Bids",
                column: "ProcurementPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineItems_BudgetId",
                table: "BudgetLineItems",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ProjectId",
                table: "Budgets",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMilestones_ContractId",
                table: "ContractMilestones",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPayments_ContractId",
                table: "ContractPayments",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialAccountTransactions_SpecialAccountId",
                table: "SpecialAccountTransactions",
                column: "SpecialAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProjectComponents_SubProjectId",
                table: "SubProjectComponents",
                column: "SubProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportingDocuments_WithdrawalApplicationId",
                table: "SupportingDocuments",
                column: "WithdrawalApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Disbursements_DisbursementId",
                table: "Payments",
                column: "DisbursementId",
                principalTable: "Disbursements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Disbursements_DisbursementId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "BudgetLineItems");

            migrationBuilder.DropTable(
                name: "ContractMilestones");

            migrationBuilder.DropTable(
                name: "ContractPayments");

            migrationBuilder.DropTable(
                name: "EnvironmentalSafeguards");

            migrationBuilder.DropTable(
                name: "SocialSafeguards");

            migrationBuilder.DropTable(
                name: "SpecialAccountTransactions");

            migrationBuilder.DropTable(
                name: "SubProjectComponents");

            migrationBuilder.DropTable(
                name: "SupportingDocuments");

            migrationBuilder.DropTable(
                name: "ProcurementPlans");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "SpecialAccounts");

            migrationBuilder.DropTable(
                name: "SubProjects");

            migrationBuilder.DropTable(
                name: "WithdrawalApplications");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Disbursements_DisbursementId",
                table: "Payments",
                column: "DisbursementId",
                principalTable: "Disbursements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
