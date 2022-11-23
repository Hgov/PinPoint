using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinkPoint.DataAccess.Migrations
{
    public partial class dataInsert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "bio", "birth_date", "creation_tsz", "delete_tsz", "email", "first_name", "gender", "last_name", "last_updated_tsz", "phone", "status_active", "status_visibility" },
                values: new object[] { new Guid("a94b6b4b-b72a-4ad9-8ee1-357128f3bd95"), "test aşamsında kullanıcı biyografisi", new DateTime(1990, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "victoria@gmail.com", "Victoria", 2, "Mercedes", new DateTime(2022, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "05070053723", true, true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "bio", "birth_date", "creation_tsz", "delete_tsz", "email", "first_name", "gender", "last_name", "last_updated_tsz", "phone", "status_active", "status_visibility" },
                values: new object[] { new Guid("b94b6b4b-b72a-4ad9-8ee1-357128f3bd95"), "test aşamsında kullanıcı biyografisi", new DateTime(1992, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "angela@gmail.com", "Angela", 2, "Bear", new DateTime(2022, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "05070053755", true, true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "bio", "birth_date", "creation_tsz", "delete_tsz", "email", "first_name", "gender", "last_name", "last_updated_tsz", "phone", "status_active", "status_visibility" },
                values: new object[] { new Guid("c94b6b4b-b72a-4ad9-8ee1-357128f3bd95"), "test aşamsında kullanıcı biyografisi", new DateTime(1993, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "hayrettin.gov@gmail.com", "Hayrettin", 1, "Göv", new DateTime(2022, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "05070053711", true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: new Guid("a94b6b4b-b72a-4ad9-8ee1-357128f3bd95"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: new Guid("b94b6b4b-b72a-4ad9-8ee1-357128f3bd95"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: new Guid("c94b6b4b-b72a-4ad9-8ee1-357128f3bd95"));
        }
    }
}
