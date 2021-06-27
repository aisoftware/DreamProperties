namespace DreamProperties.Common.Models
{
    public enum PropertyType
    {
        House,
        Flat,
        Commercial
    }

    public class Property
    {
        public Property(string title,
                        string address,
                        string city,
                        int price,
                        float squateFoots,
                        int numberOfBedrooms,
                        PropertyType propertyType,
                        string imageUrl,
                        int numberOfLikes, bool forSale = true)
        {
            City = city;
            NumberOfBedrooms = numberOfBedrooms;
            Price = price;
            PropertyType = propertyType;
            ForSale = forSale;
            NumberOfLikes = numberOfLikes;
            Title = title;
            SquareMeters = squateFoots;
            Address = address;
            ImageUrl = imageUrl;
        }

        public string City { get; set; }
        public int NumberOfBedrooms  { get; set; }
        public int Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public bool ForSale { get; set; }
        public int NumberOfLikes { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public float SquareMeters { get; set; }
        public string Address { get; set; }
        public string ListingDescription
        {
            get => $"{NumberOfBedrooms} BHK for ${Price}" + (ForSale ? "" : " / Month");
        }
    }
}
