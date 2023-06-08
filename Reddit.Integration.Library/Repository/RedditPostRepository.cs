// Ignore Spelling: Reddit

using Reddit.Integration.Library.Entities;
using Reddit.Integration.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Repository
{
    public class RedditPostRepository : IRedditPostRepository {
        public void Delete(PostEntity entity) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(PostEntity entity) {
            throw new NotImplementedException();
        }

        public IList<PostEntity> GetAll(Expression<Func<PostEntity, bool>>? filter = null, Func<IQueryable<PostEntity>, IOrderedQueryable<PostEntity>>? orderBy = null, int pageIndex = 0, int pageSize = int.MaxValue) {
            throw new NotImplementedException();
        }

        public Task<IList<PostEntity>> GetAllAsync(Expression<Func<PostEntity, bool>>? filter = null, Func<IQueryable<PostEntity>, IOrderedQueryable<PostEntity>>? orderBy = null, int pageIndex = 0, int pageSize = int.MaxValue) {
            throw new NotImplementedException();
        }

        public PostEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public Task<PostEntity> GetByIdAsync(int id) {
            throw new NotImplementedException();
        }

        public void Insert(PostEntity entity) {
            throw new NotImplementedException();
        }

        public Task InsertAsync(PostEntity entity) {
            throw new NotImplementedException();
        }

        public void Update(PostEntity entity) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PostEntity entity) {
            throw new NotImplementedException();
        }
    }
}
