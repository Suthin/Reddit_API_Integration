using Reddit.Integration.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Interfaces {

    public interface IRedditPostService {

        Task<IEnumerable<Post>> GetTopPostsAsync(string token, TokenType tokenType,  string subRedditName = "", int noOfPosts = 0);
    }
}
