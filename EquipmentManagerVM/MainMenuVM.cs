using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    /// <summary>
    /// ViewModel for MainMenu.
    /// </summary>
    public class MainMenuVM : ViewModelBase
    {
        public event Action OpenStockItemsViewRequest;

        public DelegateCommand<object> OpenStockItemsViewCommand { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainMenuVM()
        {
            OpenStockItemsViewCommand = new DelegateCommand<object>(
                execute: OpenStockItemsViewExecute,
                canExecute: OpenStockItemsViewCanExecute
                );
        }

        /// <summary>
        /// OpenStockItemsView execute criteria.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool OpenStockItemsViewCanExecute(object obj)
        {
            return true;
        }

        /// <summary>
        /// OpenStockItemsView execute method. Invoke event for main VM
        /// </summary>
        /// <param name="obj"></param>
        private void OpenStockItemsViewExecute(object obj)
        {
            OpenStockItemsViewRequest?.Invoke();
        }
    }
}
