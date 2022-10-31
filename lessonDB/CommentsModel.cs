using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lessonDB
{
    internal class CommentsModel
    {
        public int? id { get; set; }
        public int? postId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }

    }
}
