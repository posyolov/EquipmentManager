using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    public class MainVM
    {
        public PositionsVM PositionsVM { get; }
        public WorkAreaVM WorkAreaVM { get; }

        public MainVM(PositionsVM positionsVM, WorkAreaVM workAreaVM)
        {
            PositionsVM = positionsVM;
            WorkAreaVM = workAreaVM;
        }
    }
}
