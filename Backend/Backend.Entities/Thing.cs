using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class Thing : Base
    {
        public string Description { get; set; }
        public virtual Category Category { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
