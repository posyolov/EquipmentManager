using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace Repository
{
    public static class RepositoryData
    {
        public static ObservableCollection<Position> GetPositions()
        {
            EquipmentContainer context = new EquipmentContainer();
            context.Positions.Load();
            var data = context.Positions.Local;
            return data;
        }
    }
}
