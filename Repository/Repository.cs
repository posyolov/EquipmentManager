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
        public static ObservableCollection<Area> GetAreas()
        {
            EquipmentContainer context = new EquipmentContainer();
            context.AreaSet.Load();
            var data = context.AreaSet.Local;
            return data;
        }
    }
}
