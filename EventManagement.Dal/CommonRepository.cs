using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Dal
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly EventManagementDbContext _dbContext;
        private DbSet<T> table;

        public CommonRepository(EventManagementDbContext context)
        {
            _dbContext = context;
            table = _dbContext.Set<T>();
        }

        public void Delete(T item)
        {
            table.Remove(item);
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public T GetDetails(int id)
        {
            return table.Find(id);
        }

        public void Insert(T item)
        {
            table.Add(item);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Update(T item)
        {
            table.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
