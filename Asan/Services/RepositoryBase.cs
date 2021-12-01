using Asan.Contexts;
using Asan.ResourceParams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Services
{
    public class RepositoryBase<T> : IAsanRepository<T>
        where T : class
    {
        private readonly AsanContext _context;
        public DbSet<T> DbSet { get => _context.Set<T>(); }
        public RepositoryBase(AsanContext context)
        {
            _context = context;
        }
        public bool Create(T entity)
        {
            DbSet.Add(entity);
            return Save();            
        }

        public bool Delete(T entity)
        {
            DbSet.Remove(entity);
            return Save();
        }
              
        public bool Update(T entity)
        {
            DbSet.Update(entity);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }       
    }
}
