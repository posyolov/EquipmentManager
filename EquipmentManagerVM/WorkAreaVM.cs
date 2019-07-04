using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace EquipmentManagerVM
{
    public class WorkAreaVM
    {
        public ObservableCollection<string> Data { get; }

        public WorkAreaVM(ObservableCollection<string> data)
        {
            Data = data;
        }
    }
}
