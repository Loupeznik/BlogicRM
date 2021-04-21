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


        public int InstitutionID { get; set; }

        public int ClientID { get; set; }

        public int AdministratorID { get; set; }

        [Display(Name = "Datum uzavření")]
        [Required]
        public DateTime ConclusionDate { get; set; }

        [Display(Name = "Datum platnosti")]
        [Required]
        public DateTime ValidityDate { get; set; }

        [Display(Name = "Datum ukončení")]
        [Required]
        public DateTime EndDate { get; set; }

        public Institution Institution { get; set; }
        public Client Client { get; set; }
        public Advisor Administrator { get; set; }

        public ICollection<ContractAdvisor> Advisors { get; set; }

    }
}
