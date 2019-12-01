using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PositionWithStatusBits : Position
    {
        public ObservableCollection<PositionStatusBit> StatusBits
        {
            get
            {
                var bits = new ObservableCollection<PositionStatusBit>();
                foreach(var bit in )
            }
            set;
        }
    }
}
