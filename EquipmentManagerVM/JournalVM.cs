using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Repository;
using System.Windows.Data;
using System.ComponentModel;

namespace EquipmentManagerVM
{
    /// <summary>
    /// Journal entries ViewModel
    /// </summary>
    public class JournalVM : ViewModelBase
    {
        IGenericRepository<JournalEntry> _journalRepos;

        ObservableCollection<JournalEntry> _totalJournalEntries;

        public ICollectionView FilteredJournalEntries { get; }

        public FilterCriteriaString FilterCriteriaPosition { get; }
        public FilterCriteriaString FilterCriteriaDescription { get; }

        public JournalVM(IGenericRepository<JournalEntry> journalRepository)
        {
            _journalRepos = journalRepository;
            _totalJournalEntries = new ObservableCollection<JournalEntry>(_journalRepos?.GetWithInclude(p => p.Position, c => c.JournalEntryCategory));

            FilterCriteriaPosition = new FilterCriteriaString("Позиция");
            FilterCriteriaPosition.CriteriaChanged += () => FilteredJournalEntries.Refresh();
            FilterCriteriaDescription = new FilterCriteriaString("Описание");
            FilterCriteriaDescription.CriteriaChanged += () => FilteredJournalEntries.Refresh();

            FilteredJournalEntries = CollectionViewSource.GetDefaultView(_totalJournalEntries);
            FilteredJournalEntries.Filter = JournalEntriesFilter;
        }

        private bool JournalEntriesFilter(object journalEntry)
        {
            if (journalEntry is JournalEntry je)
            {
                return FilterCriteriaPosition.ContainsIn(je.Position_Name) && 
                       FilterCriteriaDescription.ContainsIn(je.Description);
            }
            else
            {
                return false;
            }
        }

        public void AddJournalEntry(JournalEntry JournalEntry)
        {
            _totalJournalEntries.Add(JournalEntry);
        }
    }
}
