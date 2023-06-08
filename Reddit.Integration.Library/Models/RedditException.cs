// Ignore Spelling: Reddit

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Models {
    public class RedditException : Exception {
       
        public RedditException(string message)
            : base(message) {
        }
        public RedditException(string message, Exception inner)
            : base(message, inner) {
        }

       
    }
}
