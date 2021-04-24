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

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]{6}\/[0-9]{3,4}$", ErrorMessage = "Neplatné rodné číslo. Rodné číslo musí být ve formátu XXXXXX/YYY nebo XXXXXX/YYYY")]
        [Display(Name = "Rodné číslo")]
        [Required]
        public string BirthNumber { get; set; }

        [Display(Name = "Věk")]
        [Required]
        public int Age { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefonní číslo")]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "Celé jméno")]
        public string FullName => string.Format("{0} {1}", Name, Surname);
    }
}
