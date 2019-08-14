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
    public class JournalVM : ViewModelBase
    {
        IGenericRepository<JournalEvent> _journalRepos;

        ObservableCollection<JournalEvent> _totalJournalEvents;

        public ObservableCollection<TabItem> Tabs { get; }

        public JournalVM(IGenericRepository<JournalEvent> journalRepository)
        {
            _journalRepos = journalRepository;
            _totalJournalEvents = new ObservableCollection<JournalEvent>(_journalRepos?.GetWithInclude(p => p.Position, c => c.EventCategory));

            Tabs = new ObservableCollection<TabItem>();
            CollectionViewSource collectionView;

            collectionView = new CollectionViewSource();
            collectionView.Source = _totalJournalEvents;
            Tabs.Add(new TabItem() { Header = "Общий", Content = collectionView });

            collectionView = new CollectionViewSource();
            collectionView.Source = _totalJournalEvents;
            collectionView.Filter += (s, e) => e.Accepted = ((JournalEvent)e.Item).EventCategory.Title == "Дежурный";
            Tabs.Add(new TabItem() { Header = "Дежурный", Content = collectionView });

            collectionView = new CollectionViewSource();
            collectionView.Source = _totalJournalEvents;
            collectionView.Filter += (s, e) => e.Accepted = ((JournalEvent)e.Item).EventCategory.Title == "Отключений";
            Tabs.Add(new TabItem() { Header = "Отключений", Content = collectionView });
        }

        public void AddJournalEvent(JournalEvent journalEvent)
        {
            _totalJournalEvents.Add(journalEvent);
        }
    }

    public sealed class TabItem
    {
        public string Header { get; set; }
        public CollectionViewSource Content { get; set; }
    }
}
