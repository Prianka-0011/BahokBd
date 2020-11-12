using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BahokBdDelivery.Migrations
{
    public partial class script : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAreaPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    BaseChargeAmount = table.Column<decimal>(nullable: false),
                    IncreaseChargePerKg = table.Column<decimal>(nullable: false),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAreaPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarchentProfileDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    BusinessName = table.Column<string>(nullable: true),
                    BusinessLink = table.Column<string>(nullable: true),
                    BusinessAddress = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    RoutingName = table.Column<string>(nullable: true),
                    BranchName = table.Column<string>(nullable: true),
                    LastIpAddress = table.Column<string>(nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    PaymentTypeId = table.Column<Guid>(nullable: true),
                    PaymentBankingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarchentProfileDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentBankingType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BankingMethodName = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentBankingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarchentCharge",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MarchentId = table.Column<Guid>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    BaseCharge = table.Column<decimal>(nullable: true),
                    IncreaseCharge = table.Column<decimal>(nullable: true),
                    DeliveryAreaPriceId = table.Column<Guid>(nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarchentCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarchentCharge_DeliveryAreaPrices_DeliveryAreaPriceId",
                        column: x => x.DeliveryAreaPriceId,
                        principalTable: "DeliveryAreaPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarchentCharge_MarchentProfileDetail_MarchentId",
                        column: x => x.MarchentId,
                        principalTable: "MarchentProfileDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarchentStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MarchentId = table.Column<Guid>(nullable: true),
                    StoreName = table.Column<string>(nullable: true),
                    ManagerName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarchentStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarchentStore_MarchentProfileDetail_MarchentId",
                        column: x => x.MarchentId,
                        principalTable: "MarchentProfileDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentBankingOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true),
                    PaymentBankingTypeId = table.Column<Guid>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentBankingOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentBankingOrganization_PaymentBankingType_PaymentBankingTypeId",
                        column: x => x.PaymentBankingTypeId,
                        principalTable: "PaymentBankingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankBranch",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    RoutingName = table.Column<string>(nullable: true),
                    BankId = table.Column<Guid>(nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankBranch_PaymentBankingOrganization_BankId",
                        column: x => x.BankId,
                        principalTable: "PaymentBankingOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarchentPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MarchentId = table.Column<Guid>(nullable: true),
                    PaymentTypeId = table.Column<Guid>(nullable: true),
                    PaymentNameId = table.Column<Guid>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarchentPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarchentPaymentDetails_BankBranch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BankBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarchentPaymentDetails_MarchentProfileDetail_MarchentId",
                        column: x => x.MarchentId,
                        principalTable: "MarchentProfileDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_BankId",
                table: "BankBranch",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_MarchentCharge_DeliveryAreaPriceId",
                table: "MarchentCharge",
                column: "DeliveryAreaPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarchentCharge_MarchentId",
                table: "MarchentCharge",
                column: "MarchentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarchentPaymentDetails_BranchId",
                table: "MarchentPaymentDetails",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_MarchentPaymentDetails_MarchentId",
                table: "MarchentPaymentDetails",
                column: "MarchentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarchentStore_MarchentId",
                table: "MarchentStore",
                column: "MarchentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBankingOrganization_PaymentBankingTypeId",
                table: "PaymentBankingOrganization",
                column: "PaymentBankingTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MarchentCharge");

            migrationBuilder.DropTable(
                name: "MarchentPaymentDetails");

            migrationBuilder.DropTable(
                name: "MarchentStore");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DeliveryAreaPrices");

            migrationBuilder.DropTable(
                name: "BankBranch");

            migrationBuilder.DropTable(
                name: "MarchentProfileDetail");

            migrationBuilder.DropTable(
                name: "PaymentBankingOrganization");

            migrationBuilder.DropTable(
                name: "PaymentBankingType");
        }
    }
}
