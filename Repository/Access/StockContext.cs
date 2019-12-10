using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Conteiner for stock entities
    /// </summary>
    public class StockContext
    {
        ObservableCollection<StockItem> _stockItems;

        OleDbConnection _accessConnection;

        public StockContext()
        {
            string connectionString = @"D:\Projects\Access\EquipmentManager\Склад.accdb";

            _accessConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + connectionString + ";");
            _accessConnection.Open();
        }

        public ObservableCollection<TEntity> Load<TEntity>() where TEntity : class
        {
            using (OleDbDataAdapter adapterDb = new OleDbDataAdapter("SELECT * FROM " + "Номенклатура", _accessConnection))
            {
                DataTable table = new DataTable();
                adapterDb.Fill(table);

                _stockItems = new ObservableCollection<StockItem>();
                foreach (DataRow row in table.Rows)
                {
                    var stockItem = new StockItem
                    {
                        Id = (row["Код"] is DBNull) ? 0 : (int)row["Код"],
                        Title = (row["Наименование"] is DBNull) ? "" : (string)row["Наименование"],
                        Description = (row["Примечание"] is DBNull) ? "" : (string)row["Примечание"],
                        Quantity = (row["Количество"] is DBNull) ? "" : (string)row["Количество"]
                    };

                    _stockItems.Add(stockItem);
                }

                return _stockItems as ObservableCollection<TEntity>;
            }
        }
    }
}
