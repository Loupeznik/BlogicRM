using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogicRM_.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advisor",
                columns: table => new
                {
                    AdvisorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advisor", x => x.AdvisorID);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    InstitutionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.InstitutionID);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvidenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionID = table.Column<int>(type: "int", nullable: true),
                    AdministratorAdvisorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConclusionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contract_Advisor_AdministratorAdvisorID",
                        column: x => x.AdministratorAdvisorID,
                        principalTable: "Advisor",
                        principalColumn: "AdvisorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Institution_InstitutionID",
                        column: x => x.InstitutionID,
                        principalTable: "Institution",
                        principalColumn: "InstitutionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvisorContract",
                columns: table => new
                {
                    AdvisorsAdvisorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractsContractID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvisorContract", x => new { x.AdvisorsAdvisorID, x.ContractsContractID });
                    table.ForeignKey(
                        name: "FK_AdvisorContract_Advisor_AdvisorsAdvisorID",
                        column: x => x.AdvisorsAdvisorID,
                        principalTable: "Advisor",
                        principalColumn: "AdvisorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvisorContract_Contract_ContractsContractID",
                        column: x => x.ContractsContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvisorContract_ContractsContractID",
                table: "AdvisorContract",
                column: "ContractsContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_AdministratorAdvisorID",
                table: "Contract",
                column: "AdministratorAdvisorID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ClientID",
                table: "Contract",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_InstitutionID",
                table: "Contract",
                column: "InstitutionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvisorContract");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Advisor");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Institution");
        }
    }
}
