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
        public IEnumerable<JournalEvent> Data1 { get; }
        public ObservableCollection<string> Data2 { get; }
        public ObservableCollection<string> Data3 { get; }

        public JournalVM(IGenericRepository<JournalEvent> journalRepository)
        {
            Data1 = journalRepository.GetWithInclude(p => p.Position);

            //var kkeke = Data1[0].Position.Name;

            Data2 = new ObservableCollection<string>() { "aaa2", "bbb2", "ccc2" };
            Data3 = new ObservableCollection<string>() { "aaa3", "bbb3", "ccc3" };
        }
    }
}
