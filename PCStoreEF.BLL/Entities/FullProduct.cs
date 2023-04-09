using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStoreEF.BLL.Entities
{
    public class FullProduct
    {
        public int Article { get; set; }

        public string Name { get; set; } = null!;

        public string Picture { get; set; } = null!;

        public int Type { get; set; }

        public float Price { get; set; }

        public string? ProductInfo { get; set; }

        public int BrandId { get; set; }

        public bool Availability { get; set; }
        public string BrandName { get; set; } = null!;
        public string TypeName { get; set; } = null!;
    }
}
