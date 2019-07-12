using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IEntities<T>
    {
        ObservableCollection<T> Entities { get; }

        void Update(T entity);
    }
}
