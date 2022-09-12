using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Entities
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Range(10, 90, ErrorMessage = "The {0} must be between {1} and {2}.")]
        public int Age { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
