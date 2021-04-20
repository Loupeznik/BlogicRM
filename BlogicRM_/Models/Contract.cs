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
        public Guid ContractID { get; set; }

        [Required]
        public string EvidenceNumber { get; set; }

        [Required]
        public Institution Institution { get; set; }

        [InverseProperty("Administering")]
        public Advisor Administrator { get; set; }

        [Required]
        public DateTime ConclusionDate { get; set; }
        [Required]
        public DateTime ValidityDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Client Client { get; set; }

        [InverseProperty("Contracts")]
        public ICollection<Advisor> Advisors { get; set; }

    }
}
