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
        CollectionViewSource _viewJournalEntries;

        public string PositionFilter { get; set; }

        public ICollectionView FilteredJournalEntries
        {
            get
            {
                return _viewJournalEntries.View;
                //return new ObservableCollection<JournalEntry>(_totalJournalEntries.Where(je => je.Position_Name != null && je.Position_Name.StartsWith("T.")));
            }
        }

        public JournalVM(IGenericRepository<JournalEntry> journalRepository)
        {
            _journalRepos = journalRepository;
            _totalJournalEntries = new ObservableCollection<JournalEntry>(_journalRepos?.GetWithInclude(p => p.Position, c => c.JournalEntryCategory));
            _viewJournalEntries = new CollectionViewSource();
            _viewJournalEntries.Source = _totalJournalEntries;
            _viewJournalEntries.Filter += OnViewJournalEntriesFilter;

            //collectionView.Filter += (s, e) => e.Accepted = ((JournalEntry)e.Item).JournalEntryCategory?.Title == "Отключений";
            PositionFilter = "T.";
        }

        private void OnViewJournalEntriesFilter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(PositionFilter))
            {
                e.Accepted = true;
                return;
            }

            JournalEntry je = e.Item as JournalEntry;
            if (je.Position_Name.ToUpper().StartsWith(PositionFilter.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        public void AddJournalEntry(JournalEntry JournalEntry)
        {
            _totalJournalEntries.Add(JournalEntry);
        }
    }
}
