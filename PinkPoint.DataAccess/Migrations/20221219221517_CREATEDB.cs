﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CREATEDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logged = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Callsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userid = table.Column<Guid>(name: "user_id", type: "uniqueidentifier", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "nvarchar(max)", nullable: true),
                    lastname = table.Column<string>(name: "last_name", type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthdate = table.Column<DateTime>(name: "birth_date", type: "datetime2", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: true),
                    creationtsz = table.Column<DateTime>(name: "creation_tsz", type: "datetime2", nullable: true),
                    lastupdatedtsz = table.Column<DateTime>(name: "last_updated_tsz", type: "datetime2", nullable: true),
                    deletetsz = table.Column<DateTime>(name: "delete_tsz", type: "datetime2", nullable: true),
                    statusactive = table.Column<bool>(name: "status_active", type: "bit", nullable: true),
                    statusvisibility = table.Column<bool>(name: "status_visibility", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NLogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
