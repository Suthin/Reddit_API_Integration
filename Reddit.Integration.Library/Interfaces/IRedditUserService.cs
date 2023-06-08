using Reddit.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reddit.Integration.Library.Models;
using User = Reddit.Integration.Library.Models.User;

namespace Reddit.Integration.Library.Interfaces {
    public interface IRedditUserService {

        Task<IEnumerable<User>> GetTopUsersAsync(string token, TokenType tokenType, string subRedditName = "", int noOfUsers = 0);
    }
}
