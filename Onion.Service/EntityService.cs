using Onion.Data.Infrastracture;
using Onion.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Onion.Service
{
    public interface IEntityService<TEnt> where TEnt : class
    {
        IEnumerable<TEnt> GetEntities();
        TEnt GetEntity(int id);
        void CreateEntity(TEnt entity);
        void UpdateEntity(TEnt entity);
        void DeleteEntity(TEnt entity);
        void Save();
    }

    public class EntityService<TEnt> : IEntityService<TEnt> where TEnt : class
    {
        private readonly IEntityRepository<TEnt> entityRepository;
        private readonly IUnitOfWork unitOfWork;

        public EntityService(IEntityRepository<TEnt> entityRepository, IUnitOfWork unitOfWork)
        {
            this.entityRepository = entityRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IEntityService Members
        public IEnumerable<TEnt> GetEntities()
        {
            return entityRepository.GetAll();
        }

        public TEnt GetEntity(int id)
        {
            return entityRepository.GetById(id);
        }

        public void CreateEntity(TEnt entity)
        {
            entityRepository.Add(entity);
        }

        public void UpdateEntity(TEnt entity)
        {
            entityRepository.Update(entity);
        }

        public void DeleteEntity(TEnt entity)
        {
            entityRepository.Delete(entity);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
        #endregion

    }
}
