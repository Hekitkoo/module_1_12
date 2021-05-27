using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ORM.Core.Interfaces;
using ORM.Core.Models;

namespace ORM.EF.Repositories
{
    public class EFRouteRepository : IRepository<Route, int>
    {
        private EFContext context;

        public EFRouteRepository(EFContext context)
        {
            this.context = context;
        }

        public void Create(Route entity)
        {
            context.Routes.Add(entity);
        }

        public Route GetById(int entityId)
        {
            return context.Routes.FirstOrDefault(x => x.Id == entityId);
        }

        public IEnumerable<Route> GetAll()
        {
            return context.Routes.ToList();
        }

        public void Update(Route entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int entityId)
        {
            var item = context.Routes.Find(entityId);
            if (item == null)
            {
                return;
            }
            context.Routes.Remove(item);
        }
    }
}