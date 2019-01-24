using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApp.Models
{
    public class Checkout
    {
        public int ID { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public int InventoryItemID { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }

        [DisplayName("Start Time")]
        public DateTime Start { get; set; }

        [DisplayName("End Time")]
        public DateTime? End { get; set; }
    }
}
