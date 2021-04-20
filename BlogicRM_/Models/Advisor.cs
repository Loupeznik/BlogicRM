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
        public Guid AdvisorID { get; set; }

        [InverseProperty("Administrator")]
        public List<Contract> Administering { get; set; }

        [InverseProperty("Advisors")]
        public ICollection<Contract> Contracts { get; set; }
    }
}
