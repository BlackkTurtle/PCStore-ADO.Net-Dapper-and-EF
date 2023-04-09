using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities
{
    public class PartOrders
    {
        public int POrderID { get; set; }
        public int Article { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
