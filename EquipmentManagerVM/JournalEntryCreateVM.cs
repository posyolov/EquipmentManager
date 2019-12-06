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
    public class JournalEntryCreateVM : ViewModelBase
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

        public DelegateCommand<object> JournalEntryCreateCommand { get; }

        public JournalEntryCreateVM(Position position, IGenericRepository<JournalEntryCategory> evCategoryRepository, IGenericRepository<JournalEntry> journalRepository)
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

            JournalEntryCreateCommand = new DelegateCommand<object>(
                execute: JournalEntryCreateExecute,
                canExecute: JournalEntryCreateCanExecute
                );

            _journalRepos = journalRepository;
        }

        private bool JournalEntryCreateCanExecute(object obj)
        {
            return true; // JEntry.Position != null && JEntry.EntryCategory != null;
        }

        private void JournalEntryCreateExecute(object obj)
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
