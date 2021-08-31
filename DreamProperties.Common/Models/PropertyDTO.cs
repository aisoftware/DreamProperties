namespace DreamProperties.Common.Models
{
    public enum PropertyType
    {
        House,
        Flat,
        Commercial
    }

    public class PropertyDTO
    {
        public int Id { get; set; }
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
        //Add later
        public string OwnersEmail { get; set; }
        public string Amenities { get; set; }
        public string ListingDescription
        {
            get => $"{NumberOfBedrooms} BHK for ${Price}" + (ForSale ? "" : "/Month");
        }
    }
}
