using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Repository;
using System.Windows.Data;

namespace EquipmentManagerVM
{
    /// <summary>
    /// ViewModel для журнала событий
    /// </summary>
    public class JournalVM : ViewModelBase
    {
        IGenericRepository<JournalEntry> _journalRepos;

        ObservableCollection<JournalEntry> _totalJournalEntries;

        public ObservableCollection<TabItem> Tabs { get; }

        public JournalVM(IGenericRepository<JournalEntry> journalRepository)
        {
            _journalRepos = journalRepository;
            _totalJournalEntries = new ObservableCollection<JournalEntry>(_journalRepos?.GetWithInclude(p => p.Position, c => c.JournalEntryCategory));

            Tabs = new ObservableCollection<TabItem>();
            CollectionViewSource collectionView;

            collectionView = new CollectionViewSource();
            collectionView.Source = _totalJournalEntries;
            Tabs.Add(new TabItem() { Header = "Общий", Content = collectionView });

            collectionView = new CollectionViewSource();
            collectionView.Source = _totalJournalEntries;
            collectionView.Filter += (s, e) => e.Accepted = ((JournalEntry)e.Item).JournalEntryCategory.Title == "Дежурный";
            Tabs.Add(new TabItem() { Header = "Дежурный", Content = collectionView });

            collectionView = new CollectionViewSource();
            collectionView.Source = _totalJournalEntries;
            collectionView.Filter += (s, e) => e.Accepted = ((JournalEntry)e.Item).JournalEntryCategory.Title == "Отключений";
            Tabs.Add(new TabItem() { Header = "Отключений", Content = collectionView });
        }

        public void AddJournalEntry(JournalEntry JournalEntry)
        {
            _totalJournalEntries.Add(JournalEntry);
        }
    }

    public sealed class TabItem
    {
        public string Header { get; set; }
        public CollectionViewSource Content { get; set; }
    }
}
