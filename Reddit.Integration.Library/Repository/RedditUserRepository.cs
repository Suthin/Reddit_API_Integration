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
    public class RedditUserRepository : IRedditUserRepository {
        public void Delete(UserEntity entity) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserEntity entity) {
            throw new NotImplementedException();
        }

        public IList<UserEntity> GetAll(Expression<Func<UserEntity, bool>>? filter = null, Func<IQueryable<UserEntity>, IOrderedQueryable<UserEntity>>? orderBy = null, int pageIndex = 0, int pageSize = int.MaxValue) {
            throw new NotImplementedException();
        }

        public Task<IList<UserEntity>> GetAllAsync(Expression<Func<UserEntity, bool>>? filter = null, Func<IQueryable<UserEntity>, IOrderedQueryable<UserEntity>>? orderBy = null, int pageIndex = 0, int pageSize = int.MaxValue) {
            throw new NotImplementedException();
        }

        public UserEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetByIdAsync(int id) {
            throw new NotImplementedException();
        }

        public void Insert(UserEntity entity) {
            throw new NotImplementedException();
        }

        public Task InsertAsync(UserEntity entity) {
            throw new NotImplementedException();
        }

        public void Update(UserEntity entity) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserEntity entity) {
            throw new NotImplementedException();
        }
    }
}
