using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    public class MainVM
    {
        public PositionsTreeVM PositionsTree { get; }
        public WorkAreaVM WorkArea { get; }

        public MainVM(PositionsTreeVM positionsTree, WorkAreaVM workArea)
        {
            PositionsTree = positionsTree;
            WorkArea = workArea;
        }
    }
}
