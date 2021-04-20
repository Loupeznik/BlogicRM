using System.ComponentModel.DataAnnotations;

namespace BlogicRM_.Models
{
    public class GenericPerson
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string BirthNumber { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
