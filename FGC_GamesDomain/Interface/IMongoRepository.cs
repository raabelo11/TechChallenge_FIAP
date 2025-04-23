using FGC_Games.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGC_Games.Domain.Interface
{
    public interface IMongoRepository <T> where T : class
    {
        Task<List<T>> Get();
        Task<T> GetById(int id);
        Task<T> CreateAsync(T Entity);
        Task<T> UpdateAsync(int id, T Entity);
        Task<T> DeleteAsync(int id);

    }
}
