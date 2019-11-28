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
        EquipmentContainer _equipmentContext;
        StockContainer _stockContext;

        IGenericRepository<Position> _positionRepos;
        IGenericRepository<JournalEntry> _journalRepos;
        IGenericRepository<JournalEntryCategory> _journalEntryCategoryRepos;
        IGenericRepository<PositionStatusBit> _positionStatusBitRepos;
        IGenericRepository<StockItem> _stockItemsRepos;

        public RepositoryProxy<Position> PositionReposProxy { get; }
        public RepositoryProxy<JournalEntry> JournalReposProxy { get; }
        public RepositoryProxy<JournalEntryCategory> JournalEntryCategoryReposProxy { get; }
        public RepositoryProxy<PositionStatusBit> PositionStatusBitReposProxy { get; }
        public RepositoryProxy<StockItem> StockItemsReposProxy { get; }

        public IEnumerable<PositionStatusBit> PositionStatusBits { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Manager()
        {
            _equipmentContext = new EquipmentContainer();
            _stockContext = new StockContainer();

            _positionRepos = new GenericRepositoryEF<Position>(_equipmentContext);
            _journalRepos = new GenericRepositoryEF<JournalEntry>(_equipmentContext);
            _journalEntryCategoryRepos = new GenericRepositoryEF<JournalEntryCategory>(_equipmentContext);
            _positionStatusBitRepos = new GenericRepositoryEF<PositionStatusBit>(_equipmentContext);
            _stockItemsRepos = new GenericRepositoryAccess<StockItem>(_stockContext);

            PositionReposProxy = new RepositoryProxy<Position>(_positionRepos);
            JournalReposProxy = new RepositoryProxy<JournalEntry>(_journalRepos);
            JournalEntryCategoryReposProxy = new RepositoryProxy<JournalEntryCategory>(_journalEntryCategoryRepos);
            PositionStatusBitReposProxy = new RepositoryProxy<PositionStatusBit>(_positionStatusBitRepos);
            StockItemsReposProxy = new RepositoryProxy<StockItem>(_stockItemsRepos);

            PositionStatusBits = _positionStatusBitRepos.Get();
        }
    }
}
