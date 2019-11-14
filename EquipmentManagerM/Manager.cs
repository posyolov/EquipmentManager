using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerM
{
    public class Manager
    {
        EquipmentContainer _context;

        public GenericRepositoryEF<Position> PositionRepos { get; }
        public GenericRepositoryEF<JournalEntry> JournalRepos { get; }
        public GenericRepositoryEF<JournalEntryCategory> EvCategoryRepos { get; }

        public Manager()
        {
            //репозитории
            _context = new EquipmentContainer();

            PositionRepos = new GenericRepositoryEF<Position>(_context);
            JournalRepos = new GenericRepositoryEF<JournalEntry>(_context);
            EvCategoryRepos = new GenericRepositoryEF<JournalEntryCategory>(_context);

            //GenericRepositoryEF<PositionNode> tempRepos = new GenericRepositoryEF<PositionNode>(_context);
            GenericRepositoryAccess<StockItem> st = new GenericRepositoryAccess<StockItem>(@"D:\Projects\Access\EquipmentManager\Склад.accdb");
            var dwdw = st.Get();
        }
    }
}
