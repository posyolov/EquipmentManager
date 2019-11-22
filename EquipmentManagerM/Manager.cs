using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerM
{
    /// <summary>
    /// Model of business logic.
    /// </summary>
    public class Manager
    {
        EquipmentContainer _context;

        GenericRepositoryEF<Position> _positionRepos;
        GenericRepositoryEF<JournalEntry> _journalRepos;
        GenericRepositoryEF<JournalEntryCategory> _journalEntryCategoryRepos;
        GenericRepositoryAccess<StockItem> _stockRepos;

        public RepositoryProxy<Position> PositionReposProxy { get; }
        public RepositoryProxy<JournalEntry> JournalReposProxy { get; }
        public RepositoryProxy<JournalEntryCategory> JournalEntryCategoryReposProxy { get; }
        public RepositoryProxy<StockItem> StockReposProxy { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Manager()
        {
            _context = new EquipmentContainer();
            _positionRepos = new GenericRepositoryEF<Position>(_context);
            _journalRepos = new GenericRepositoryEF<JournalEntry>(_context);
            _journalEntryCategoryRepos = new GenericRepositoryEF<JournalEntryCategory>(_context);

            PositionReposProxy = new RepositoryProxy<Position>(_positionRepos);
            JournalReposProxy = new RepositoryProxy<JournalEntry>(_journalRepos);
            JournalEntryCategoryReposProxy = new RepositoryProxy<JournalEntryCategory>(_journalEntryCategoryRepos);

            _stockRepos = new GenericRepositoryAccess<StockItem>(@"D:\Projects\Access\EquipmentManager\Склад.accdb");
            var dwdw = _stockRepos.Get();
        }
    }
}
