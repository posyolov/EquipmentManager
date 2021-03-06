﻿using System;
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
        public ObservableCollection<PositionNode> Nodes { get; } //TODO: сделать недоступным добавление и удаление
        public bool IsSelected { get; set; } //TODO: можно ли убрать?

        public event Action<PositionNode, PositionStatusBit> PositionStatusChanged;

        /// <summary>
        /// Constructor without children filling.
        /// Creating StatusBits from Status.
        /// </summary>
        public PositionNode(Position position, IEnumerable<PositionStatusBitInfo> positionStatusBitsInfo)
        {
            PositionData = position;
            Nodes = new ObservableCollection<PositionNode>();

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
            var childrenPositions = positions.Where(c => c.ParentName == PositionData.Name);

            foreach (var pos in childrenPositions)
            {
                PositionNode posNode = new PositionNode(pos, positions, positionStatusBitsInfo);
                posNode.PositionStatusChanged += OnChildPositionStatusChanged;
                Nodes.Add(posNode);
            }
        }

        /// <summary>
        /// Add node children nodes.
        /// </summary>
        /// <param name="node"></param>
        public void AddChild(PositionNode node)
        {
            if(node != null)
            {
                node.PositionStatusChanged += OnChildPositionStatusChanged;
                Nodes.Add(node);
            }
            else
            {
                throw new ArgumentNullException(nameof(node));
            }
        }

        /// <summary>
        /// Remove child node all over tree.
        /// Return True if removing success, else False.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool RemoveChildWholeTree(PositionNode node)
        {
            return RemoveChildFromNode(node, this);
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
        private void GetPositionsList(PositionNode node, List<Position> positions)
        {
            positions?.Add(node.PositionData);

            if (node.Nodes != null)
                foreach (PositionNode posNode in node.Nodes)
                    GetPositionsList(posNode, positions);
        }

        /// <summary>
        /// Remove child node rerursively.
        /// Return True if removing success, else False.
        /// </summary>
        /// <param name="desiredNode"></param>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        private bool RemoveChildFromNode(PositionNode desiredNode, PositionNode parentNode)
        {
            foreach (PositionNode node in parentNode.Nodes)
            {
                if (node == desiredNode)
                {
                    parentNode.Nodes.Remove(node);
                    return true;
                }

                if (RemoveChildFromNode(desiredNode, node))
                    return true;
            }

            return false;
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
