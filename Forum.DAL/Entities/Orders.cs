using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities
{
    public class Orders
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }=DateTime.Now;
        public string Adress { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }
    }
}
