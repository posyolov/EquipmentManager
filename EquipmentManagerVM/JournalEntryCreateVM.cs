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

        public IEnumerable<JournalEntryCategory> EntryCategories { get; }
        IGenericRepository<JournalEntryCategory> _entryCategoryRepository;

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

        public JournalEntryCreateVM(Position position, IGenericRepository<JournalEntryCategory> entryCategoryRepository)
        {
            //запрос списка категорий
            _entryCategoryRepository = entryCategoryRepository;
            EntryCategories = _entryCategoryRepository.Get();

            JEntry = new JournalEntry()
            {
                DateTime = DateTime.Now,
                Position = position,
            };

            JournalEntryCreateCommand = new DelegateCommand<object>(
                execute: JournalEntryCreateExecute,
                canExecute: JournalEntryCreateCanExecute
                );
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
                JournalEntryCreatedEv?.Invoke(JEntry);
            }
        }

    }
}
