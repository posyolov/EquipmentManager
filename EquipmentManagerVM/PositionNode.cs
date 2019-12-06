using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace EquipmentManagerVM
{
    /// <summary>
    /// Positions tree node
    /// </summary>
    public class PositionNode : INotifyPropertyChanged
    {
        private Position _posData;
        public ObservableCollection<PositionStatusBit> StatusBits { get; private set; }
        public ObservableCollection<PositionNode> Nodes { get; set; }

        public Position PosData
        {
            get => _posData;
            set
            {
                _posData = value;
                NotifyPropertyChanged();
            }
        }

        public event Action<PositionNode, PositionStatusBit> PositionStatusChanged;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PositionNode(Position position, IEnumerable<PositionStatusBitInfo> positionStatusBitsInfo)
        {
            PosData = position;
            Nodes = new ObservableCollection<PositionNode>();

            StatusBits = new ObservableCollection<PositionStatusBit>();
            foreach (PositionStatusBitInfo statusBitInfo in positionStatusBitsInfo)
            {
                if (statusBitInfo.Enable)
                {
                    var statusBit = new PositionStatusBit(position.Status, statusBitInfo);
                    StatusBits.Add(statusBit);
                    statusBit.BitChanged += OnStatusBitChanged;
                }
            }
        }

        /// <summary>
        /// Get position data from node.
        /// </summary>
        /// <returns></returns>
        public Position GetPositionData()
        {
            return new Position() { Id = PosData.Id, Name = PosData.Name, ParentId = PosData.ParentId, Title = PosData.Title , Status = PosData.Status};
        }

        /// <summary>
        /// Set position data to node.
        /// </summary>
        /// <param name="posData"></param>
        public void SetPosData(Position posData)
        {
            PosData = new Position() { Id = posData.Id, Name = posData.Name, ParentId = posData.ParentId, Title = posData.Title, Status = posData.Status }; ;
        }


        private void OnStatusBitChanged(PositionStatusBit statusBit)
        {
            _posData.Status = (_posData.Status & ~(1 << statusBit.StatusBitInfo.BitNumber)) | (Convert.ToInt64(statusBit.Value) << statusBit.StatusBitInfo.BitNumber);
            PositionStatusChanged?.Invoke(this, statusBit);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
