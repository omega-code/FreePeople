using FreePeople.Persistence.DTO;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FreePeople.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
					table.UniqueConstraint("UK_City_Name", x => x.Name);
				});

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CityId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrator_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
					table.UniqueConstraint("UK_Administrator_Email", x => x.Email);
					table.UniqueConstraint("UK_Administrator_Name", x => x.Name);
				});

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(maxLength: 2000, nullable: false),
                    CityId = table.Column<Guid>(nullable: false),
                    ContactName = table.Column<string>(maxLength: 400, nullable: false),
                    ContactPhone = table.Column<string>(maxLength: 50, nullable: false),
                    HowToGet = table.Column<string>(maxLength: 2000, nullable: false),
                    MapUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
					table.UniqueConstraint("UK_Place_CityName", x => new { x.CityId, x.Name });
					table.ForeignKey(
                        name: "FK_Place_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Speaker",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    About = table.Column<string>(maxLength: 2000, nullable: false),
                    CityId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 400, nullable: false),
                    Facebook = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Photo = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaker", x => x.Id);
					table.UniqueConstraint("UK_Speaker_Email", x => x.Email);
					table.UniqueConstraint("UK_Speaker_Name", x => x.Name);
					table.ForeignKey(
                        name: "FK_Speaker_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Talk",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApprovedAt = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<Guid>(nullable: true),
                    CityId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(maxLength: 400, nullable: false),
                    FullInfo = table.Column<string>(maxLength: 2000, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true),
                    PlaceVerifiedAt = table.Column<DateTime>(nullable: true),
                    PlaceVerifiedById = table.Column<Guid>(nullable: true),
                    PublishedAt = table.Column<DateTime>(nullable: true),
                    PublishedById = table.Column<Guid>(nullable: true),
                    Report = table.Column<string>(maxLength: 2000, nullable: true),
                    ReportedAt = table.Column<DateTime>(nullable: true),
                    ReportedById = table.Column<Guid>(nullable: true),
                    ShortInfo = table.Column<string>(maxLength: 1000, nullable: false),
                    SpeakerId = table.Column<Guid>(nullable: false),
                    StartsAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talk_Administrator_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Administrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Talk_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Talk_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Talk_Administrator_PlaceVerifiedById",
                        column: x => x.PlaceVerifiedById,
                        principalTable: "Administrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Talk_Administrator_PublishedById",
                        column: x => x.PublishedById,
                        principalTable: "Administrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Talk_Administrator_ReportedById",
                        column: x => x.ReportedById,
                        principalTable: "Administrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Talk_Speaker_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });


			migrationBuilder.CreateIndex(
				name: "IX_Administrator_Email",
				table: "Administrator",
				column: "Email");

			migrationBuilder.CreateIndex(
                name: "IX_Administrator_CityId",
                table: "Administrator",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_CityId",
                table: "Place",
                column: "CityId");

			migrationBuilder.CreateIndex(
				name: "IX_Speaker_Email",
				table: "Speaker",
				column: "Email");

			migrationBuilder.CreateIndex(
                name: "IX_Speaker_CityId",
                table: "Speaker",
                column: "CityId");

			migrationBuilder.CreateIndex(
				name: "IX_Talk_StartsAt",
				table: "Talk",
				column: "StartsAt");

			migrationBuilder.CreateIndex(
				name: "IX_Talk_ApprovedById",
				table: "Talk",
				column: "ApprovedById");

			migrationBuilder.CreateIndex(
                name: "IX_Talk_CityId",
                table: "Talk",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Talk_PlaceId",
                table: "Talk",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Talk_PlaceVerifiedById",
                table: "Talk",
                column: "PlaceVerifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Talk_PublishedById",
                table: "Talk",
                column: "PublishedById");

            migrationBuilder.CreateIndex(
                name: "IX_Talk_ReportedById",
                table: "Talk",
                column: "ReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_Talk_SpeakerId",
                table: "Talk",
                column: "SpeakerId");

			var cityId = Guid.NewGuid().ToString();

			migrationBuilder.Sql($@"insert into City (Id, Name) values ('{cityId}', N'Москва')");
			migrationBuilder.Sql($@"insert into Administrator (Id, Name, Email, CityId) values (newid(), N'Великий магистр', 'master@freepeople.world', '{cityId}')");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Talk");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "Speaker");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
