using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieBox.Migrations
{
    public partial class PopulateMemberShipType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MembershipType",
                columns: new[] { "Id", "Name", "SignUpFee", "DurationInMonths", "DiscountRate" },
                values: new object[] { 1, "Pay as you go", 0, 0, 0 }
            );

            migrationBuilder.InsertData(
               table: "MembershipType",
               columns: new[] { "Id", "Name", "SignUpFee", "DurationInMonths", "DiscountRate" },
               values: new object[] { 2, "Monthly", 30, 1, 10 }
           );

            migrationBuilder.InsertData(
               table: "MembershipType",
               columns: new[] { "Id", "Name", "SignUpFee", "DurationInMonths", "DiscountRate" },
               values: new object[] { 3, "Quarterly", 90, 3, 15 }
           );

            migrationBuilder.InsertData(
               table: "MembershipType",
               columns: new[] { "Id", "Name", "SignUpFee", "DurationInMonths", "DiscountRate" },
               values: new object[] { 4, "Yearly", 300, 12, 20 }
           );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
