using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Repository;

namespace EquipmentManagerVM
{
    public class JournalVM : ViewModelBase
    {
        IGenericRepository<JournalEvent> _journalRepos;

        public ObservableCollection<JournalEvent> JournalEvents { get; set; }
        public ObservableCollection<string> Data2 { get; }
        public ObservableCollection<string> Data3 { get; }

        public JournalVM(IGenericRepository<JournalEvent> journalRepository)
        {
            _journalRepos = journalRepository;
            JournalEvents = new ObservableCollection<JournalEvent>(_journalRepos?.GetWithInclude(p => p.Position, c => c.EventCategory));

            Data2 = new ObservableCollection<string>() { "aaa2", "bbb2", "ccc2" };
            Data3 = new ObservableCollection<string>() { "aaa3", "bbb3", "ccc3" };
        }

        public void AddJournalEvent(JournalEvent journalEvent)
        {
            JournalEvents.Add(journalEvent);
        }
    }
}
