using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        public string Address { get; set; }
        public ICollection<Cash> Cashes { get; set; }
       
    }
}
