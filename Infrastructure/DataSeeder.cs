using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure;

public static class DataSeeder
{


    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        PasswordHasher<User> hasher = new();
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                PhoneNumber = "555334455",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Name = "Admin",
                Surname = "Admininistrator"
            },
            new User
            {
                Id = 2,
                UserName = "ani17",
                NormalizedUserName = "ANI17",
                Email = "ani@gmail.com",
                NormalizedEmail = "ANI@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Asdzxc123"),
                PhoneNumber = "555334456",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Name = "Ani",
                Surname = "Magradze"

            },
            new User
            {
                Id = 3,
                UserName = "rezirezi",
                NormalizedUserName = "REZIREZI",
                Email = "rezi@gmail.com",
                NormalizedEmail = "Rezi@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Qweasd123"),
                PhoneNumber = "555334457",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Name = "Rezi",
                Surname = "Magradze"
            }
        );
    }

    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = -2, Name = "Admin", NormalizedName = "ADMIN" },
            new Role { Id = -1, Name = "User", NormalizedName = "USER" }
        );
    }

    public static void SeedUserRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { RoleId = -2, UserId = 1 },
            new UserRole { RoleId = -1, UserId = 2 },
            new UserRole { RoleId = -1, UserId = 3 }
        );
    }

}