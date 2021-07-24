﻿// <auto-generated />
using DreamProperties.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DreamProperties.API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DreamProperties.API.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ForSale")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfBedrooms")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLikes")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("PropertyType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SquareMeters")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Properties");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "132, West street, New York, United States",
                            City = "New York",
                            ForSale = true,
                            ImageUrl = "propertyWide.png",
                            NumberOfBedrooms = 3,
                            NumberOfLikes = 88,
                            Price = 40500,
                            PropertyType = "House",
                            SquareMeters = 1800f,
                            Title = "3 Bedroom Independent House"
                        },
                        new
                        {
                            Id = 2,
                            Address = "132, Main street, Los Angeles, United States",
                            City = "Los Angeles",
                            ForSale = false,
                            ImageUrl = "propertyWide.png",
                            NumberOfBedrooms = 2,
                            NumberOfLikes = 75,
                            Price = 1800,
                            PropertyType = "Flat",
                            SquareMeters = 50f,
                            Title = "2 Bedroom Flat"
                        },
                        new
                        {
                            Id = 3,
                            Address = "132, West street, New York, United States",
                            City = "New York",
                            ForSale = false,
                            ImageUrl = "propertyWide.png",
                            NumberOfBedrooms = 3,
                            NumberOfLikes = 66,
                            Price = 2200,
                            PropertyType = "Flat",
                            SquareMeters = 60f,
                            Title = "3 Bedroom Flat"
                        },
                        new
                        {
                            Id = 4,
                            Address = "132, West street, New York, United States",
                            City = "New York",
                            ForSale = true,
                            ImageUrl = "propertyWide.png",
                            NumberOfBedrooms = 3,
                            NumberOfLikes = 14,
                            Price = 40500,
                            PropertyType = "House",
                            SquareMeters = 1800f,
                            Title = "3 Bedroom Independent House"
                        },
                        new
                        {
                            Id = 5,
                            Address = "132, West street, New York, United States",
                            City = "New York",
                            ForSale = true,
                            ImageUrl = "propertyWide.png",
                            NumberOfBedrooms = 3,
                            NumberOfLikes = 2,
                            Price = 40500,
                            PropertyType = "House",
                            SquareMeters = 1000f,
                            Title = "3 Bedroom Independent House"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
