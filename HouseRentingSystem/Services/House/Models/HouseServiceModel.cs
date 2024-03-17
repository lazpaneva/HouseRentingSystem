using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Services.House.Models
{
    public class HouseServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Address { get; set; } = null!;

        [DisplayName("Image URL")]
        public string ImageUrl { get; set; } = null!;

        [DisplayName("Price per Month")]
        public decimal PricePerMonth { get; set; }

        [DisplayName("Is Rented")]
        public bool IsRented { get; set; }
    }
}
