using System;
using System.ComponentModel.DataAnnotations;

namespace TreinoAPI.DTO.Auth
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(6)]
        public string Password { get; set; }
    }
}
