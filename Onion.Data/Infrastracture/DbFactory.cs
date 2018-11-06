using System;

namespace Onion.Data.Infrastracture
{
    public interface IDbFactory : IDisposable
    {
        OnionContext Init();
    }

    public class DbFactory : Disposable, IDbFactory
    {
        OnionContext dbContext;

        public OnionContext Init()
        {
            return dbContext ?? (dbContext = new OnionContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
