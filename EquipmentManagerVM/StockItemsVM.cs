using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    /// <summary>
    /// ViewModel for stock items
    /// </summary>
    public class StockItemsVM
    {
        IGenericRepository<StockItem> _stockItemsRepos;

        public ObservableCollection<StockItem> StockItems { get; }

        /// <summary>
        /// Constructor with repository parameter
        /// </summary>
        /// <param name="stockItemsRepos"></param>
        public StockItemsVM(IGenericRepository<StockItem> stockItemsRepos)
        {
            _stockItemsRepos = stockItemsRepos;
            StockItems = new ObservableCollection<StockItem>(_stockItemsRepos.Get());
        }
    }
}
