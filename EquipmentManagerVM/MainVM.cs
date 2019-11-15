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
        // Main Model.
        readonly Manager _manager;

        // ViewModels.
        public PositionsVM PositionsVM { get; }
        public JournalVM JournalVM { get; }

        // Event to high level for create journal entry.
        public event Action<JournalEntryCreateVM> JournalEntryCreateViewEv;

        public MainVM()
        {
            // Main Model.
            _manager = new Manager();


            // Positions.
            PositionsVM = new PositionsVM(_manager.PositionRepos);

            // Request to create journal entry.
            // Create VM and notify high level.
            PositionsVM.JournalEntryCreateReqEv += (pos) =>
            {
                // Make JournalEntryCreateVM at selected position.
                JournalEntryCreateVM _journalEntryCreateVM = new JournalEntryCreateVM(pos, _manager.EvCategoryRepos, _manager.JournalRepos);
                // !!!high coupling? JournalVM must update herself?!!! Event to notify JournalVM that new entry was created.
                _journalEntryCreateVM.JournalEntryCreatedEv += (je) => JournalVM.AddJournalEntry(je);

                // Gen event for high level.
                JournalEntryCreateViewEv(_journalEntryCreateVM);
            };


            // Journal.
            JournalVM = new JournalVM(_manager.JournalRepos);
        }
    }
}
