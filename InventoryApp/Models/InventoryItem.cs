using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InventoryApp.Models
{
    public class InventoryItem
    {
        public int ID { get; set; }

        [DisplayName("Serial Number")]
        public string Serial { get; set; }

        public int ItemID { get; set; }

        [DisplayName("Item Type")]
        public virtual Item Item { get; set; }

        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}
