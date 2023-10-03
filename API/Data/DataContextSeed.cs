using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Data
{
    public partial class DataContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            using var hmac = new HMACSHA512();

            modelBuilder.Entity<Photo>().HasData(
                new Photo
                {
                    Id = 1,
                    Url = "https://randomuser.me/api/portraits/women/54.jpg",
                    IsMain = true,
                    PublicId = "PublicId1",
                    AppUserId = 1 // Specify the foreign key value to link to the AppUser with Id 1
                }
            );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { 
                    Id = 1,
                    UserName = "Lisa",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("testpass")),
                    PasswordSalt = hmac.Key,
                    Gender = "Female",
                    DateOfBirth = new DateTime(1956, 7, 22),
                    KnownAs = "Lisa",
                    Created = new DateTime(2020, 6, 24),
                    LastActive = new DateTime(2020, 6, 27),
                    Introduction = "Sunt esse aliqua ullamco in incididunt consequat commodo...",
                    Interests = "Sit sit incididunt proident velit.",
                    City = "Greenbush",
                    Country = "Martinique",
                    Photos = new List<Photo>()
                });


        }
    }
}
