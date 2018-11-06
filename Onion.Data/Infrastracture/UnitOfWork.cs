using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Data.Infrastracture
{
    public interface IUnitOfWork
    {
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;

        private OnionContext dbContext;
        public OnionContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
