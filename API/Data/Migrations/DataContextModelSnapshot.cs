﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interests")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KnownAs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Greenbush",
                            Country = "Martinique",
                            Created = new DateTime(2020, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfBirth = new DateTime(1956, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "Female",
                            Interests = "Sit sit incididunt proident velit.",
                            Introduction = "Sunt esse aliqua ullamco in incididunt consequat commodo...",
                            KnownAs = "Lisa",
                            LastActive = new DateTime(2020, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PasswordHash = new byte[] { 184, 7, 238, 123, 58, 220, 189, 128, 27, 171, 204, 149, 81, 232, 235, 146, 122, 28, 4, 225, 244, 125, 240, 18, 13, 87, 213, 243, 217, 107, 140, 131, 127, 245, 80, 170, 125, 248, 216, 11, 64, 96, 163, 248, 146, 150, 44, 54, 115, 117, 202, 27, 103, 117, 154, 74, 222, 46, 109, 88, 164, 27, 52, 174 },
                            PasswordSalt = new byte[] { 1, 155, 157, 8, 43, 163, 75, 226, 33, 107, 212, 26, 79, 174, 31, 10, 108, 157, 175, 192, 37, 184, 2, 97, 188, 221, 155, 101, 157, 160, 38, 182, 20, 144, 206, 110, 209, 105, 247, 197, 171, 160, 233, 2, 50, 207, 40, 193, 53, 153, 207, 251, 252, 104, 109, 229, 15, 247, 79, 78, 117, 86, 23, 208, 101, 7, 64, 190, 18, 41, 177, 207, 159, 214, 88, 81, 108, 82, 135, 50, 174, 198, 92, 202, 102, 64, 191, 165, 76, 53, 125, 56, 180, 36, 189, 84, 74, 82, 237, 122, 220, 163, 156, 114, 195, 187, 139, 27, 144, 218, 175, 123, 66, 50, 123, 228, 189, 62, 132, 26, 181, 15, 78, 172, 205, 158, 16, 164 },
                            UserName = "Lisa"
                        });
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Photos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AppUserId = 1,
                            IsMain = true,
                            PublicId = "PublicId1",
                            Url = "https://randomuser.me/api/portraits/women/54.jpg"
                        });
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.HasOne("API.Entities.AppUser", "AppUser")
                        .WithMany("Photos")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
