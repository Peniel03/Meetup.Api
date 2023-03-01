using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.Interfaces
{
    public interface IBaseService<T>
    {
        Task<T> AddAsync(T model);
        Task<T?> GetByIdAsync(int Id);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateAsync(T model);
        Task<T> DeleteAsync(T model);
        Task SavechangesAsync();
    }
}
