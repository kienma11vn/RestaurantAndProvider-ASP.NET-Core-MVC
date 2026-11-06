using System.ComponentModel.DataAnnotations;

namespace RestaurantAndProvider.Models
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        [Required]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? Location { get; set; }
        [StringLength(10), DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        public List<Restaurant>? Restaurants { get; set; }
    }
}
