using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Onion.Data.Infrastracture
{
    public interface IRepository<TEnt> where TEnt : class
    {
        void Add(TEnt entity);
        void Update(TEnt entity);
        void Delete(TEnt entity);
        void Delete(Expression<Func<TEnt, bool>> where);
        TEnt GetById(int id);
        TEnt GetEntity(Expression<Func<TEnt, bool>> where);
        IEnumerable<TEnt> GetAll();
        IEnumerable<TEnt> GetMany(Expression<Func<TEnt, bool>> where);
    }

    public abstract class Repository<TEnt>  : IRepository<TEnt>
        where TEnt : class
    {
        private readonly DbSet<TEnt> dbSet;
        protected IDbFactory dbFactory { get; private set; }
        private OnionContext dbContext;
        protected OnionContext DbContext
        {
           get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        protected Repository(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
            dbSet = DbContext.Set<TEnt>();
        }

        #region Implementation

        public void Add(TEnt entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEnt entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEnt entity)
        {
            dbSet.Remove(entity);
        }

        public void Delete(Expression<Func<TEnt, bool>> where)
        {
            IEnumerable<TEnt> entities = dbSet.Where<TEnt>(where).AsEnumerable();
            foreach (TEnt entity in entities)   //TODO: maybe change to dbSet.RemoveRange(entities);
                dbSet.Remove(entity); 
        }

        public TEnt GetById(int id)
        {
            return dbSet.Find(id);
        }

        public TEnt GetEntity(Expression<Func<TEnt, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault(); 
        }

        public IEnumerable<TEnt> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<TEnt> GetMany(Expression<Func<TEnt, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }
        #endregion


    }
}
