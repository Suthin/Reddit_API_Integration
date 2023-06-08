// Ignore Spelling: Reddit

using Reddit.Controllers;
using Reddit.Integration.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Interfaces
{
    public interface IRedditPostRepository:IRepository<PostEntity> {
    }
}
