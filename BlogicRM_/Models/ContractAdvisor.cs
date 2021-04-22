using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogicRM_.Models
{
    public class ContractAdvisor
    {
        public int ContractID { get; set; }
        public int AdvisorID { get; set; }

        public Contract Contract { get; set; }
        public Advisor Advisor { get; set; }
    }
}
