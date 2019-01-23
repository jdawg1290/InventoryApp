using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApp.Models
{
    public class Checkout
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public virtual ICollection<InventoryItem> InvItems { get; set; }
    }
}
