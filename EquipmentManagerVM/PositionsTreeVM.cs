using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Repository;

namespace EquipmentManagerVM
{
    public class PositionsTreeVM
    {
        public ObservableCollection<Position> Positions { get; }

        public PositionsTreeVM(ObservableCollection<Position> positions)
        {
            Positions = positions;
        }
    }
}
