using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace EquipmentManagerVM
{
    public class PositionStatusBit
    {
        bool _value;

        public PositionStatusBitInfo StatusBitInfo { get; set; }
        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                BitChanged?.Invoke(this);
            }
        }

        public event Action<PositionStatusBit> BitChanged;

        public PositionStatusBit(long status, PositionStatusBitInfo statusBitInfo)
        {
            StatusBitInfo = statusBitInfo;
            _value = (status & (1 << statusBitInfo.BitNumber)) != 0;
        }
    }
}
