using EquipmentManagerM;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    /// <summary>
    /// Main ViewModel
    /// </summary>
    public class MainVM
    {
        readonly Manager _manager;

        public PositionsVM PositionsVM { get; }
        public JournalVM JournalVM { get; }

        public event Action<JournalEntryCreateVM> JournalEntryCreateViewEv;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainVM()
        {
            _manager = new Manager();
            
            PositionsVM = new PositionsVM(_manager.PositionReposProxy);
            PositionsVM.JournalEntryCreateReqEv += OnJournalEntryCreateRequestEv;
            
            JournalVM = new JournalVM(_manager.JournalReposProxy);
        }

        /// <summary>
        /// Create JournalEntryCreateVM and invoke event for creating JournalEntryCreateView.
        /// </summary>
        /// <param name="selectedPosition"></param>
        private void OnJournalEntryCreateRequestEv(Position selectedPosition)
        {
            JournalEntryCreateVM _journalEntryCreateVM = new JournalEntryCreateVM(selectedPosition, _manager.JournalEntryCategoryReposProxy, _manager.JournalReposProxy);
            _journalEntryCreateVM.JournalEntryCreatedEv += OnJournalEntryCreatedEv; 

            JournalEntryCreateViewEv?.Invoke(_journalEntryCreateVM);
        }

        /// <summary>
        /// Notify JournalVM that journal entry was created. ???may be throw model?
        /// </summary>
        /// <param name="je"></param>
        private void OnJournalEntryCreatedEv(JournalEntry je)
        {
            JournalVM.AddJournalEntry(je);
        }
    }
}
