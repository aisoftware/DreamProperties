﻿namespace DreamProperties.Common.Models
{
    public enum PropertyType
    {
        House,
        Flat,
        Commercial
    }

    public class Property
    {
        public Property(string city, int numberOfBedrooms, int price, PropertyType propertyType, int numberOfLikes, bool forSale = true)
        {
            City = city;
            NumberOfBedrooms = numberOfBedrooms;
            Price = price;
            PropertyType = propertyType;
            ForSale = forSale;
            NumberOfLikes = numberOfLikes;
        }

        public string City { get; set; }
        public int NumberOfBedrooms  { get; set; }
        public int Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public bool ForSale { get; set; }

        public int NumberOfLikes { get; set; }

        public string ListingDescription
        {
            get => $"{NumberOfBedrooms} BHK for ${Price}" + (ForSale ? "" : " / Month");
        }
    }
}