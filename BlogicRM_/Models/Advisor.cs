using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogicRM_.Models
{
    public class Advisor : GenericPerson
    {
        [Key]
        public int AdvisorID { get; set; }

        [Display(Name = "Spravuje")]
        [InverseProperty("Administrator")]
        public List<Contract> Administering { get; set; }

        public ICollection<ContractAdvisor> Contracts { get; set; }
    }
}
