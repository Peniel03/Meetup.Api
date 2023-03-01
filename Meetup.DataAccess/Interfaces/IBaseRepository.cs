using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.DataAccess.Interfaces
{
    public interface IBaseRepository<T>
    {
        void AddAsync(T model);
        Task<T?> GetByIdAsync(int Id);
        Task<List<T>> GetAllAsync();
        void UpdateAsync(T model);
        void DeleteAsync(T model);
        Task SavechangesAsync();

    }
}
