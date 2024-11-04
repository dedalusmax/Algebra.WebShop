using Algebra.WebShop.App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Algebra.WebShop.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class adminuserrole : Migration
    {
        private const string ADMIN_ROLE_ID = "A3EFCE80-5BBC-478F-8554-F5FA5C315635";
        private const string ADMIN_ROLE_NAME = "Admin";
        private const string ADMIN_USER_ID = "D4C12CE8-A3E7-4707-AB0C-601D361FF0F5";
        private const string ADMIN_USER_NAME = "admin@webshop.hr";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var pwd = hasher.HashPassword(null, "Password123!");

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_ID}', '{ADMIN_ROLE_NAME}', '{ADMIN_ROLE_NAME.ToUpper()}')");
            migrationBuilder.Sql($"INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) VALUES ('{ADMIN_USER_ID}', '{ADMIN_USER_NAME}', '{ADMIN_USER_NAME.ToUpper()}', '{ADMIN_USER_NAME}', '{ADMIN_USER_NAME.ToUpper()}', 1, '{pwd}', NEWID(), NEWID(), 1, 0, 0, 0)");
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles VALUES ('{ADMIN_USER_ID}', '{ADMIN_ROLE_ID}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // update-database 20241021184416_identity-domain

            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{ADMIN_USER_ID}' AND RoleId = '{ADMIN_ROLE_ID}'");
            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{ADMIN_USER_ID}'");
            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ADMIN_ROLE_ID}'");
        }
    }
}
