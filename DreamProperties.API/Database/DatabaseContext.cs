﻿using DreamProperties.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DreamProperties.API.Database
{
    public class DatabaseContext: IdentityDbContext<AppUser>
    {
        public DatabaseContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>()
                        .HasData(
                        new Property
                        {
                            Id = 1,
                            Title = "3 Bedroom Independent House",
                            Address = "440 Lisbon Ave, Buffalo, NY 14215, United States",
                            City = "New York",
                            Price = 100000,
                            SquareMeters = 200,
                            NumberOfBedrooms = 3,
                            PropertyType = "House",
                            ImageUrl = "propertyWide.png",
                            NumberOfLikes = 88,
                            ForSale = true
                        },
                        new Property
                        {
                            Id = 2,
                            Title = "2 Bedroom Flat",
                            Address = "132, Main street, Los Angeles, United States",
                            City = "Los Angeles",
                            Price = 1800,
                            SquareMeters = 50,
                            NumberOfBedrooms = 2,
                            PropertyType = "Flat",
                            ImageUrl = "propertyWide.png",
                            NumberOfLikes = 75,
                            ForSale = false
                        },
                        new Property
                        {
                            Id = 3,
                            Title = "3 Bedroom Flat",
                            Address = "132, West street, New York, United States",
                            City = "New York",
                            Price = 2200,
                            SquareMeters = 60,
                            NumberOfBedrooms = 3,
                            PropertyType = "Flat",
                            ImageUrl = "propertyWide.png",
                            NumberOfLikes = 66,
                            ForSale = false
                        },
                        new Property
                        {
                            Id = 4,
                            Title = "Beautiful house",
                            Address = "302 Dearborn St, Buffalo, United States",
                            City = "Buffalo",
                            Price = 60000,
                            SquareMeters = 300,
                            NumberOfBedrooms = 3,
                            PropertyType = "House",
                            ImageUrl = "propertyWide.png",
                            NumberOfLikes = 14,
                            ForSale = true
                        },
                        new Property
                        {
                            Id = 5,
                            Title = "Big house",
                            Address = "33 Walnut St, Montgomery, United States",
                            City = "Montgomery",
                            Price = 400000,
                            SquareMeters = 1000,
                            NumberOfBedrooms = 3,
                            PropertyType = "House",
                            ImageUrl = "propertyWide.png",
                            NumberOfLikes = 2,
                            ForSale = true
                        });
        }
    }
}
