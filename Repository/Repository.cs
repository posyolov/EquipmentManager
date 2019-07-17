using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;

namespace Repository
{
    public class RepositoryData : IEntities<Position>
    {
        public ObservableCollection<Position> Entities
        {
            get
            {
                using (var context = new EquipmentContainer())
                {
                    context.Positions.Load();
                    return context.Positions.Local;
                }
            }
        }

        public void Update(Position entity)
        {
            using (var context = new EquipmentContainer())
            {
                context.Positions.AddOrUpdate(entity);
                context.SaveChanges();
            }
        }

        public void Add(Position entity)
        {
            Update(entity);
        }

        public void Remove(Position entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<Position> branch)
        {
            using (var context = new EquipmentContainer())
            {
                foreach (var pos in branch)
                {
                    context.Entry(pos).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
    }
}
