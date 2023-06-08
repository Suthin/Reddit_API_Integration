using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Entities
{
    public class PostEntity : BaseEntity
    {
        public string? Title { get; set; }
        public string? Url { get; set; }
        public int Score { get; set; }

        public int UpVotes { get; set; }

    }
}
