using Asan.ResourceParams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Services
{
    public interface IAsanRepository<T>
    {
        List<T> GetAll();        

        bool Create(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        bool Save();
    }
}
