using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Casacade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Upvote_Topic_TopicId",
                table: "Upvote");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "468fef88-c48e-4b11-b840-c50e05147aa0", "AQAAAAIAAYagAAAAEHWt7u9roz1HItKAm5FVdEg1ra/aWad03aUSphxbhATD0wkZgBEGifmvLkTH2cw0OA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2e667a30-42e6-4077-9294-8b18cb23b0d5", "AQAAAAIAAYagAAAAED5Lhq3+fAFYQTbFu26o/6oOw2eKB/gZAHx/2RGpk1jKd2vkxfkqrm5d3sKMxbSD/A==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d16d8939-7692-4c0f-a492-02af4fbc952c", "AQAAAAIAAYagAAAAEPhpwgXyqiQYoHcvlbKBS6s9bHtXrBz+QFFTR6Zus0fevVBo3OWLbhZaJcnvCdyBHg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e1daaad-e339-4f3e-8a65-9699e1073f69", "AQAAAAIAAYagAAAAEMno1r9t9FS0xcdAEARmNfpbQw139Ri8gyxer5lWZ6ZXVhar4xxAyGPZDevZSaQiZA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Upvote_Topic_TopicId",
                table: "Upvote",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Upvote_Topic_TopicId",
                table: "Upvote");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "40bbc8ec-b2b5-4422-bff3-45ffdf21a034", "AQAAAAIAAYagAAAAEARs65fBZle+Ytd9IstMediQIkdbN9kTFqgwBTdw28EEnnMfpfbeoBDTfIMERKHVJQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2e869684-ca9e-4964-8ae2-cf0a35fc6e51", "AQAAAAIAAYagAAAAEPNFKkY5a/GtQPv4GapBQX3/YQ8I0+owK1BeoHVOAS0WldqLSqUNxAZ8GkZJABYcdQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "23a31be1-3d11-4f87-b158-ac3a4b1cfd9f", "AQAAAAIAAYagAAAAEL1nxNnFdPQ9wAftoBl+OLONyzGoUii4Lo1cC3UQyRO1mOZrZuc5p6C8aHFBwvM/2g==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "83aa80c6-aa48-41e3-8376-696e1413c4b4", "AQAAAAIAAYagAAAAEO2edh7awRxIWnMM286XFXIfcrI+LAbJ0WwQVPhusw39h36y5/rF1e8SjDWTRrmyRQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Upvote_Topic_TopicId",
                table: "Upvote",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
