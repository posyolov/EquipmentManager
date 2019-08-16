using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    /// <summary>
    /// Основная ViewModel
    /// </summary>
    public class MainVM
    {
        public PositionsVM PositionsVM { get; }
        public JournalVM JournalVM { get; }

        public MainVM(PositionsVM positionsVM, JournalVM journalVM)
        {
            PositionsVM = positionsVM;
            JournalVM = journalVM;
        }
    }
}
