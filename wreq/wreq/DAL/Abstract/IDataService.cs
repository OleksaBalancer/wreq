using System;
using System.Linq;
using System.Linq.Expressions;
using wreq.Models.Abstract;

namespace wreq.DAL.Abstract
{
    public interface IDataService: IDisposable
    {
        IQueryable<T> Get<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        IQueryable<T> GetByAuthor<T>(string authorId) where T : class, IHasAuthorAndName;
        T GetByID<T>(int id) where T : class;
        void Insert<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Delete<T>(int id) where T : class;
        void Update<T>(T entity) where T : class;
        void Save();
    }
}
