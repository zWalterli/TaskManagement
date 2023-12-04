using TaskManagement.Domain.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Interface.Repository
{
    public interface IBaseRepository<T> where T : BaseDbModel
    {
        Task<T> GetById(int id);
        Task<List<T>> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, Object>>[] includes);
        Task<T> Insert(T item);
        Task<List<T>> Insert(List<T> itens);
        Task<T> Update(T item);
        Task<List<T>> Update(List<T> itens);
        Task<bool> Delete(T item);
    }
}
