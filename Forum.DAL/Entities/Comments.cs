using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities
{
    public class Comments
    {
        public int CommentID { get; set; }
        public int Article { get; set; }
        public int Stars { get; set; }
        public DateTime CommentDate { get; set; }= DateTime.Now;
        public int UserID { get; set; }
        public string Comment { get; set; }
    }
}
