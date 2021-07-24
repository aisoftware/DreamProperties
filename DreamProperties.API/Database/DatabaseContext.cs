using DreamProperties.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamProperties.API.Database
{
    public class DatabaseContext: DbContext
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
                                Address = "132, West street, New York, United States",
                                City = "New York",
                                Price = 40500,
                                SquareMeters = 1800,
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
                                Title = "3 Bedroom Independent House",
                                Address = "132, West street, New York, United States",
                                City = "New York",
                                Price = 40500,
                                SquareMeters = 1800,
                                NumberOfBedrooms = 3,
                                PropertyType = "House",
                                ImageUrl = "propertyWide.png",
                                NumberOfLikes = 14,
                                ForSale = true
                            },
                            new Property
                            {
                                Id = 5,
                                Title = "3 Bedroom Independent House",
                                Address = "132, West street, New York, United States",
                                City = "New York",
                                Price = 40500,
                                SquareMeters = 1000,
                                NumberOfBedrooms = 3,
                                PropertyType = "House",
                                ImageUrl = "propertyWide.png",
                                NumberOfLikes = 2,
                                ForSale = true
                            }
                );
        }
    }
}
