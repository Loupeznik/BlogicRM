using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogicRM_.Models
{
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }

        [Display(Name = "Evidenční číslo")]
        [Required]
        public string EvidenceNumber { get; set; }

        [Display(Name = "Instituce")]
        public int InstitutionID { get; set; }

        [Display(Name = "Klient")]
        public int ClientID { get; set; }

        [Display(Name = "Správce")]
        public int AdministratorID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum uzavření")]
        [Required]
        public DateTime ConclusionDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum platnosti")]
        [Required]
        public DateTime ValidityDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum ukončení")]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "Instituce")]
        public Institution Institution { get; set; }

        [Display(Name = "Klient")]
        public Client Client { get; set; }

        [Display(Name = "Správce")]
        public Advisor Administrator { get; set; }

        [Display(Name = "Poradci")]
        public ICollection<ContractAdvisor> Advisors { get; set; }
    }
}
