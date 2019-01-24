using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApp.Models
{
    public class CheckoutCreateViewModel
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        public string Serial { get; set; }
    }
}
