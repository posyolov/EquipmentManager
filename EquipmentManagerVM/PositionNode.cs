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
        public Position PositionData { get; }
        public ObservableCollection<PositionStatusBit> StatusBits { get; }
        public ObservableCollection<PositionNode> Nodes { get; set; }
        public bool IsSelected { get; set; }

        public event Action<PositionNode, PositionStatusBit> PositionStatusChanged;

        /// <summary>
        /// Constructor without children filling.
        /// Creating StatusBits from Status.
        /// </summary>
        public PositionNode(Position position, IEnumerable<PositionStatusBitInfo> positionStatusBitsInfo)
        {
            PositionData = position;

            StatusBits = new ObservableCollection<PositionStatusBit>();
            foreach (PositionStatusBitInfo statusBitInfo in positionStatusBitsInfo)
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
        /// Call constructor without children filling and then creating all children nodes recursively.
        /// </summary>
        public PositionNode(Position position, IEnumerable<Position> positions, IEnumerable<PositionStatusBitInfo> positionStatusBitsInfo)
            : this(position, positionStatusBitsInfo)
        {
            Nodes = new ObservableCollection<PositionNode>();
            var childrenPositions = positions.Where(c => c.ParentName == PositionData.Name);

            foreach (var pos in childrenPositions)
            {
                PositionNode posNode = new PositionNode(pos, positions, positionStatusBitsInfo);
                posNode.PositionStatusChanged += OnChildPositionStatusChanged;
                Nodes.Add(posNode);
            }
        }

        /// <summary>
        /// Extract the node`s children position data to list recursively.
        /// </summary>
        /// <param name="childrenPosData"></param>
        /// <param name="childNode"></param>
        public List<Position> GetAllPositionsList()
        {
            List<Position> positions = new List<Position>();

            GetPositionsList(this, positions);

            return positions;
        }

        /// <summary>
        /// Extract Positions from node and his children.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="positions"></param>
        void GetPositionsList(PositionNode node, List<Position> positions)
        {
            positions?.Add(node.PositionData);

            if (node.Nodes != null)
                foreach (PositionNode posNode in node.Nodes)
                    GetPositionsList(posNode, positions);
        }

        /// <summary>
        /// Invoke PositionStatusChanged event on child StatusBit changed.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="statusBit"></param>
        private void OnChildPositionStatusChanged(PositionNode node, PositionStatusBit statusBit)
        {
            PositionStatusChanged?.Invoke(node, statusBit);
        }

        /// <summary>
        /// Invoke PositionStatusChanged event on StatusBit changed.
        /// </summary>
        /// <param name="statusBit"></param>
        private void OnStatusBitChanged(PositionStatusBit statusBit)
        {
            PositionData.Status = (PositionData.Status & ~(1 << statusBit.StatusBitInfo.BitNumber)) | (Convert.ToInt64(statusBit.Value) << statusBit.StatusBitInfo.BitNumber);
            PositionStatusChanged?.Invoke(this, statusBit);
        }
    }
}
