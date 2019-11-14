using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    /// <summary>
    /// ViewModel окна создания нового события
    /// </summary>
    public class CreateJournalEntryVM : ViewModelBase
    {
        public event Action<JournalEntry> JournalEntryCreatedEv;

        public JournalEntry JEntry { get; set; }

        public IEnumerable<JournalEntryCategory> EvCategories { get; }
        IGenericRepository<JournalEntry> _journalRepos;
        IGenericRepository<JournalEntryCategory> _evCategoryRepository;

        private bool closeTrigger;
        public bool CloseTrigger
        {
            get => closeTrigger;
            set
            {
                closeTrigger = value;
                NotifyPropertyChanged();
            }
        }

        public DelegateCommand<object> CreateJournalEntryCommand { get; }

        public CreateJournalEntryVM(Position position, IGenericRepository<JournalEntryCategory> evCategoryRepository, IGenericRepository<JournalEntry> journalRepository)
        {
            //запрос списка категорий
            _evCategoryRepository = evCategoryRepository;
            EvCategories = _evCategoryRepository.Get();

            JEntry = new JournalEntry()
            {
                DateTime = DateTime.Now,
                Position = position,
                //EntryCategory = new EntryCategory()
            };

            CreateJournalEntryCommand = new DelegateCommand<object>(
                execute: CreateJournalEntryExecute,
                canExecute: CreateJournalEntryCanExecute
                );

            _journalRepos = journalRepository;
        }

        private bool CreateJournalEntryCanExecute(object obj)
        {
            return true; // JEntry.Position != null && JEntry.EntryCategory != null;
        }

        private void CreateJournalEntryExecute(object obj)
        {
            if (JEntry != null && JEntry.Position != null && JEntry.JournalEntryCategory != null)
            {
                CloseTrigger = true;

                _journalRepos.Add(JEntry);

                JournalEntryCreatedEv?.Invoke(JEntry);
            }
        }

    }
}
