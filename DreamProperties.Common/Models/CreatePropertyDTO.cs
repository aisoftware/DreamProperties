using System;
using System.ComponentModel.DataAnnotations;

namespace DreamProperties.Common.Models
{
    public class CreatePropertyDTO
    {
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "City must contain between 3 and 200 characters.")]
        public string City { get; set; }
        [Required]
        [Range(1, 20)]
        public int NumberOfBedrooms { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }
        [Required]
        public PropertyType PropertyType { get; set; }
        [Required]
        public bool ForSale { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must contain between 3 and 200 characters.")]
        public string Title { get; set; }
        [Required]
        public float SquareMeters { get; set; }
        [Required]
        [StringLength(400, MinimumLength = 3, ErrorMessage = "Address must contain between 3 and 400 characters.")]
        public string Address { get; set; }
    }
}
