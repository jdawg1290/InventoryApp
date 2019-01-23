using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApp.Models
{
    public class Item
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
