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

        public MainMenuVM MainMenuVM { get; }
        public PositionsVM PositionsVM { get; }
        public JournalVM JournalVM { get; }

        public event Action<JournalEntryCreateVM> JournalEntryCreateViewRequestEv;
        public event Action<StockItemsVM> StockItemsViewRequestEv;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainVM()
        {
            _manager = new Manager();

            MainMenuVM = new MainMenuVM();
            MainMenuVM.OpenStockItemsViewRequest += OnOpenStockItemsViewRequest;

            PositionsVM = new PositionsVM(_manager.PositionReposProxy, _manager.PositionStatusBitsInfo);
            PositionsVM.JournalEntryCreateReqEv += OnJournalEntryCreateRequestEv;
            PositionsVM.JournalEntryCreatedEv += OnJournalEntryCreatedEv;

            JournalVM = new JournalVM(_manager.JournalReposProxy, _manager.JournalEntryCategoryReposProxy, _manager.PositionStatusBitInfoReposProxy);
        }

        /// <summary>
        /// Create StockItemsVM and invoke event for creating StockItemsView.
        /// </summary>
        private void OnOpenStockItemsViewRequest()
        {
            StockItemsVM _stockItemsVM = new StockItemsVM(_manager.StockItemsReposProxy);

            StockItemsViewRequestEv?.Invoke(_stockItemsVM);
        }

        /// <summary>
        /// Create JournalEntryCreateVM and invoke event for creating JournalEntryCreateView.
        /// </summary>
        /// <param name="selectedPosition"></param>
        private void OnJournalEntryCreateRequestEv(Position selectedPosition)
        {
            JournalEntryCreateVM _journalEntryCreateVM = new JournalEntryCreateVM(selectedPosition, _manager.JournalEntryCategoryReposProxy);
            _journalEntryCreateVM.JournalEntryCreatedEv += OnJournalEntryCreatedEv; 

            JournalEntryCreateViewRequestEv?.Invoke(_journalEntryCreateVM);
        }

        /// <summary>
        /// Notify JournalVM that journal entry was created. ???may be throw model?
        /// </summary>
        /// <param name="je"></param>
        private void OnJournalEntryCreatedEv(JournalEntry je)
        {
            _manager.JournalReposProxy.Add(je);
            JournalVM.AddJournalEntry(je);
        }
    }
}
