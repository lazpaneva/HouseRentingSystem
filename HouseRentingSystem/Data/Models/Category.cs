using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants;

namespace HouseRentingSystem.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(NameLength)]
        public string Name { get; set; } = null!;
        public IEnumerable<House> Houses { get; set; } = new List<House>();
        //        • Id – a unique integer, Primary Key
        //• Name – a string with max length 50 (required)
        //• Houses – a collection of House
    }
}
