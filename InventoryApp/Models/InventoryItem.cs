using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApp.Models
{
    public class InventoryItem
    {
        public int ID { get; set; }

        public string Serial { get; set; }

        public int ItemID { get; set; }

        public virtual Item Item { get; set; }
    }
}
