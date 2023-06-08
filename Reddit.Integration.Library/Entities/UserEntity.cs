using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Entities
{
    public class UserEntity : BaseEntity
    {

        public string? userName { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }

        public string? Url { get; set; }
    }
}
