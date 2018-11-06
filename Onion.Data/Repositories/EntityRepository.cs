using Onion.Data.Infrastracture;

namespace Onion.Data.Repositories
{

    public interface IEntityRepository<TEnt> : IRepository<TEnt> where TEnt : class
    { }

    public class EntityRepository<TEnt> : Repository<TEnt>, IEntityRepository<TEnt> where TEnt : class
    {
        public EntityRepository(IDbFactory dbFactory)
            :base(dbFactory) { }
    }
}
