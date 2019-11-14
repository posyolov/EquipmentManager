using EquipmentManagerM;
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

        public event Action<CreateJournalEntryVM> CreateJournalEntryViewEv;

        readonly Manager _manager;

        public MainVM(Manager manager)
        {
            _manager = manager;

            // Positions.
            PositionsVM = new PositionsVM(manager.PositionRepos);
            // Request to open create journal entry view.
            PositionsVM.CreateJournalEntryReqEv += PositionsVM_CreateJournalEntryReqEv;

            // Journal.
            JournalVM = new JournalVM(manager.JournalRepos);
        }

        // Open window for create journal entry
        private void PositionsVM_CreateJournalEntryReqEv(Repository.Position position)
        {
            // Journal entry view at selected position
            CreateJournalEntryVM _createJournalEntryVM = new CreateJournalEntryVM(position, _manager.EvCategoryRepos, _manager.JournalRepos);
            _createJournalEntryVM.JournalEntryCreatedEv += (je) => JournalVM.AddJournalEntry(je);

            CreateJournalEntryViewEv(_createJournalEntryVM);
        }
    }
}
