using EFTutorials.CRUD.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFTutorials.CRUD.Repository
{

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T GetById(object id);
        void Insert(T obj);
        void InsertRange(List<T> list);
        void Update(T obj);
        void Delete(T obj);
        void DeleteRange(Expression<Func<T, bool>> predicate);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private LowPriceDBEntities context = null;
        private DbSet<T> entity = null;
        public Repository()
        {
            context = new LowPriceDBEntities();
            entity = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entity.ToList();
        }
        public T GetById(object id)
        {
            return entity.Find(id);
        }
        public void Insert(T obj)
        {
            entity.Add(obj);
            context.SaveChanges();
        }
        public void Update(T obj)
        {
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(T obj)
        {
            entity.Remove(obj);
            context.SaveChanges();
        }
      
        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return entity.Where(predicate).ToList();
        }

        public void InsertRange(List<T> list)
        {
            entity.AddRange(list);
            context.SaveChanges();
        }

        public void DeleteRange(Expression<Func<T, bool>> predicate)
        {
            entity.RemoveRange(entity.Where(predicate));
            context.SaveChanges();
        }
    }
}
