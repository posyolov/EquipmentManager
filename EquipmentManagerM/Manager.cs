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
        EquipmentContext _equipmentContext;
        StockContext _stockContext;

        IGenericRepository<Position> _positionRepos;
        IGenericRepository<JournalEntry> _journalRepos;
        IGenericRepository<JournalEntryCategory> _journalEntryCategoryRepos;
        IGenericRepository<PositionStatusBitInfo> _positionStatusBitInfoRepos;
        IGenericRepository<StockItem> _stockItemsRepos;

        public RepositoryProxy<Position> PositionReposProxy { get; }
        public RepositoryProxy<JournalEntry> JournalReposProxy { get; }
        public RepositoryProxy<JournalEntryCategory> JournalEntryCategoryReposProxy { get; }
        public RepositoryProxy<PositionStatusBitInfo> PositionStatusBitInfoReposProxy { get; }
        public RepositoryProxy<StockItem> StockItemsReposProxy { get; }

        public IEnumerable<PositionStatusBitInfo> PositionStatusBitsInfo { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Manager()
        {
            _equipmentContext = new EquipmentContext();
            _stockContext = new StockContext();

            _positionRepos = new GenericRepositoryEF<Position>(_equipmentContext);
            _journalRepos = new GenericRepositoryEF<JournalEntry>(_equipmentContext);
            _journalEntryCategoryRepos = new GenericRepositoryEF<JournalEntryCategory>(_equipmentContext);
            _positionStatusBitInfoRepos = new GenericRepositoryEF<PositionStatusBitInfo>(_equipmentContext);
            _stockItemsRepos = new GenericRepositoryAccess<StockItem>(_stockContext);

            PositionReposProxy = new RepositoryProxy<Position>(_positionRepos);
            JournalReposProxy = new RepositoryProxy<JournalEntry>(_journalRepos);
            JournalEntryCategoryReposProxy = new RepositoryProxy<JournalEntryCategory>(_journalEntryCategoryRepos);
            PositionStatusBitInfoReposProxy = new RepositoryProxy<PositionStatusBitInfo>(_positionStatusBitInfoRepos);
            StockItemsReposProxy = new RepositoryProxy<StockItem>(_stockItemsRepos);

            PositionStatusBitsInfo = _positionStatusBitInfoRepos.Get();
        }
    }
}
