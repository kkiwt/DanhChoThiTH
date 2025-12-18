using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientStaff
{
        public class MenuItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
        }

        public class OrderItem
        {
            public int TableNumber { get; set; }
            public MenuItem Item { get; set; }
            public int Quantity { get; set; }
            public int Total => Quantity * Item.Price;
        }

}
