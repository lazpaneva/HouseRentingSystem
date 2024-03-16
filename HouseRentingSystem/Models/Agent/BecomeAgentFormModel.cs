using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants;


namespace HouseRentingSystem.Models.Agents
{
    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(AgentPhoneMaxLength, MinimumLength = AgentPhoneMinLength)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

    }
}
