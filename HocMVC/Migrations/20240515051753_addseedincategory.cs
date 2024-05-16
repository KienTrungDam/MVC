using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HocMVC.Migrations
{
    /// <inheritdoc />
    public partial class addseedincategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DesplayOrder", "Name" },
                values: new object[] { 4, 4, "VietNam" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
