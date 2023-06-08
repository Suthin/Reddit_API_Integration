using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Models {
    public class Post {

        public string? Title { get; set; }
        public string? Url { get; set; }
        public int Score { get; set; }

        public int UpVotes { get; set; }
    }
}
