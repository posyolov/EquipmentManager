using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Обобщённый репозиторий Access
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepositoryAccess<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        string _connectionStr;

        public GenericRepositoryAccess(string connectionString)
        {
            _connectionStr = connectionString;
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get()
        {
            //подключаемся к БД
            //_connectionStr = "";//"EquipmentStatistic.accdb";//@"O:\12_Производственно-технический департамент\12.5_Служба АСУ\01_Поселов\Temp\EquipmentStatistic.accdb";

            OleDbConnection accessConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _connectionStr + ";");
            accessConnection.Open();

            OleDbCommand oleDbCommand = accessConnection.CreateCommand();

            OleDbDataAdapter adapterDb = new OleDbDataAdapter();

            DataTable table = new DataTable();
            oleDbCommand.CommandType = System.Data.CommandType.Text;
            oleDbCommand.CommandText = "SELECT * FROM " + "Номенклатура";// typeof(TEntity).Name;
            adapterDb.SelectCommand = oleDbCommand;
            adapterDb.Fill(table);

            return null;
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Exception RemoveRange(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
