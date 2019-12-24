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
    public class PositionNode
    {
        private readonly IEnumerable<PositionStatusBitInfo> _positionStatusBitsInfo;

        public Position PositionData { get; set; }
        public ObservableCollection<PositionStatusBit> StatusBits { get; private set; }
        public ObservableCollection<PositionNode> Nodes { get; set; }

        public event Action<PositionNode, PositionStatusBit> PositionStatusChanged;

        /// <summary>
        /// Constructor without children filling.
        /// </summary>
        public PositionNode(Position position, IEnumerable<PositionStatusBitInfo> positionStatusBitsInfo)
        {
            _positionStatusBitsInfo = positionStatusBitsInfo;

            PositionData = position;
            Nodes = new ObservableCollection<PositionNode>();

            StatusBits = new ObservableCollection<PositionStatusBit>();
            foreach (PositionStatusBitInfo statusBitInfo in _positionStatusBitsInfo)
            {
                if (statusBitInfo.Enable)
                {
                    var statusBit = new PositionStatusBit(position.Status, statusBitInfo);
                    statusBit.BitChanged += OnStatusBitChanged;
                    StatusBits.Add(statusBit);
                }
            }
        }

        /// <summary>
        /// Constructor with children filling.
        /// </summary>
        public PositionNode(Position position, IEnumerable<Position> positions, IEnumerable<PositionStatusBitInfo> positionStatusBitsInfo)
            : this(position, positionStatusBitsInfo)
        {
            BuildBranch(this, positions);
        }

        /// <summary>
        /// Building branch of the node.
        /// </summary>
        /// <param name="positionNode"></param>
        /// <param name="positions"></param>
        void BuildBranch(PositionNode positionNode, IEnumerable<Position> positions)
        {
            var childrenPositions = positions.Where(c => c.ParentName == positionNode.PositionData.Name);

            foreach (var pos in childrenPositions)
            {
                PositionNode posNode = new PositionNode(pos, positions, _positionStatusBitsInfo);
                posNode.PositionStatusChanged += PositionStatusChanged;
                BuildBranch(posNode, positions);
                positionNode.Nodes.Add(posNode);
            }

        }

        private void OnStatusBitChanged(PositionStatusBit statusBit)
        {
            PositionData.Status = (PositionData.Status & ~(1 << statusBit.StatusBitInfo.BitNumber)) | (Convert.ToInt64(statusBit.Value) << statusBit.StatusBitInfo.BitNumber);
            PositionStatusChanged?.Invoke(this, statusBit);
        }
    }
}
