using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Store.BLL.Entities
{
    public class FullProduct
    {
        public int Article { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Type { get; set; }
        public double Price { get; set; }
        public string ProductInfo { get; set; }
        public int BrandID { get; set; }
        public bool Availlability { get; set; }
        public string BrandName { get; set; }

        public string TypeName { get; set; }
    }
}
