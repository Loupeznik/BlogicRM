using System.ComponentModel.DataAnnotations;

namespace BlogicRM_.Models
{
    public class GenericPerson
    {
        [Display(Name = "Jméno")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Příjmení")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Rodné číslo")]
        [Required]
        public string BirthNumber { get; set; }

        [Display(Name = "Věk")]
        [Required]
        public int Age { get; set; }

        [Display(Name = "Telefonní číslo")]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "Celé jméno")]
        public string FullName => string.Format("{0} {1}", Name, Surname);
    }
}
