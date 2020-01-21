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
    /// ViewModel для журнала событий
    /// </summary>
    public class JournalVM : ViewModelBase
    {
        IGenericRepository<JournalEntry> _journalRepos;

        ObservableCollection<JournalEntry> _totalJournalEntries;
        ICollectionView _viewJournalEntries;

        string _positionFilter = "A.";
        public string PositionFilter
        {
            get => _positionFilter;
            set
            {
                _positionFilter = value;

                _viewJournalEntries?.Refresh();
            }
        }

        public ICollectionView FilteredJournalEntries
        {
            get
            {
                return _viewJournalEntries;
            }
        }

        public JournalVM(IGenericRepository<JournalEntry> journalRepository)
        {
            _journalRepos = journalRepository;
            _totalJournalEntries = new ObservableCollection<JournalEntry>(_journalRepos?.GetWithInclude(p => p.Position, c => c.JournalEntryCategory));

            _viewJournalEntries = CollectionViewSource.GetDefaultView(_totalJournalEntries);
            _viewJournalEntries.Filter = JournalEntriesFilter;
        }

        private bool JournalEntriesFilter(object journalEntry)
        {
            if (journalEntry is JournalEntry je)
            {
                return je.Position_Name.ToUpper().StartsWith(PositionFilter.ToUpper());
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
