using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos
{
    public class ProfileDto
    {

        public int PantherProfileId { get; set; }
        [Required]
        public int PantherTypeId { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "Each word must start with a capital letter. No special characters.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Name must be at least 4 characters.")]
        public string PantherName { get; set; }

        [Required]
        [Range(31, double.MaxValue, ErrorMessage = "Weight must be greater than 30.")]
        public double Weight { get; set; }

        [Required]
        public string Characteristics { get; set; }

        [Required]
        public string Warning { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
