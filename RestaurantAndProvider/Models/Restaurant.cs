using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAndProvider.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? Location { get; set; }
        [Range(1, 5)]
        public int Ratings { get; set; }
        [ForeignKey("Provider")]
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
    }
}
