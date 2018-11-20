﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CoreWebApp.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "SourceInfos",
				columns: table => new
				{
					SourceInfoId = table.Column<long>(type: "int8", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
					Description = table.Column<string>(type: "text", nullable: true),
					Name = table.Column<string>(type: "text", nullable: true),
					Timestamp = table.Column<DateTime>(type: "timestamp", nullable: false),
					UpdatedTimestamp = table.Column<DateTime>(type: "timestamp", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SourceInfos", x => x.SourceInfoId);
				});

			migrationBuilder.CreateTable(
				name: "DataEventRecords",
				columns: table => new
				{
					DataEventRecordId = table.Column<long>(type: "int8", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
					Description = table.Column<string>(type: "text", nullable: true),
					EventRecordType = table.Column<string>(type: "text", nullable: true),
					Name = table.Column<string>(type: "text", nullable: true),
					SourceInfoId = table.Column<long>(type: "int8", nullable: false),
					Timestamp = table.Column<DateTime>(type: "timestamp", nullable: false),
					UpdatedTimestamp = table.Column<DateTime>(type: "timestamp", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DataEventRecords", x => x.DataEventRecordId);
					table.ForeignKey(
						name: "FK_DataEventRecords_SourceInfos_SourceInfoId",
						column: x => x.SourceInfoId,
						principalTable: "SourceInfos",
						principalColumn: "SourceInfoId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_DataEventRecords_SourceInfoId",
				table: "DataEventRecords",
				column: "SourceInfoId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DataEventRecords");

			migrationBuilder.DropTable(
				name: "SourceInfos");
		}
	}
}
