using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants;

namespace HouseRentingSystem.Data.Models
{
    [Comment("House Agent")]
    public class Agent
    {
        [Key]
        [Comment("Agent identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(AgentPhoneMaxLength)]
        [Comment("Agent's phone")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Comment("User Identifier")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        //public List<House> Houses { get; set; } = new List<House>();
    }
}
