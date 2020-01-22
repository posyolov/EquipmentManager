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

        public ICollectionView FilteredJournalEntries { get; }

        public FilterData FilterPosition { get; set; }

        public JournalVM(IGenericRepository<JournalEntry> journalRepository)
        {
            _journalRepos = journalRepository;
            _totalJournalEntries = new ObservableCollection<JournalEntry>(_journalRepos?.GetWithInclude(p => p.Position, c => c.JournalEntryCategory));

            FilteredJournalEntries = CollectionViewSource.GetDefaultView(_totalJournalEntries);

            FilterPosition = new FilterData("Позиция", "", FilteredJournalEntries);
            FilteredJournalEntries.Filter = JournalEntriesFilter;
        }

        private bool JournalEntriesFilter(object journalEntry)
        {
            bool posFiltr = false;

            if (journalEntry is JournalEntry je)
            {
                if (je.Position_Name != null && FilterPosition != null && FilterPosition.FilterString != null)
                    posFiltr = !FilterPosition.Enabled || je.Position_Name.ToUpper().StartsWith(FilterPosition.FilterString.ToUpper());
                else
                    posFiltr = false;
            }
            else
            {
                return false;
            }

            return posFiltr;
        }

        public void AddJournalEntry(JournalEntry JournalEntry)
        {
            _totalJournalEntries.Add(JournalEntry);
        }
    }

    public class FilterData
    {
        private string _filterString;
        private bool _enabled;
        ICollectionView _collectionView;

        public string Title { get; set; }
        public string FilterString
        {
            get => _filterString;
            set
            {
                _filterString = value;
                _collectionView.Refresh();
            }
        }
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                _collectionView.Refresh();
            }
        }

        public FilterData(string title, string filterString, ICollectionView collectionView)
        {
            Title = title;
            _filterString = filterString;
            _collectionView = collectionView;
        }

    }
}
