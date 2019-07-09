using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Repository;

namespace EquipmentManagerVM
{
    public class PositionsVM
    {
        public ObservableCollection<Position> Positions { get; }
        public ObservableCollection<PositionNode> PositionsTree { get; }
        public PositionNode SelectedPosNode
        {
            get;
            set;
        }

        public PositionsVM(ObservableCollection<Position> positions)
        {
            Positions = positions;

            PositionsTree = new ObservableCollection<PositionNode>();

            //добавляем узлы верхнего уровня
            foreach (Position pos in positions)
            {
                if (!pos.ParentId.HasValue)
                {
                    //для каждого строим ветвь
                    PositionNode posNode = new PositionNode(pos);
                    BuildBranch(posNode, positions);
                    PositionsTree.Add(posNode);
                }
            }

            //TestNode node = new TestNode("A");
            //node.Nodes.Add(new TestNode("aaa"));

            //PositionsTree.Add(node);
            //PositionsTree.Add(new TestNode("B"));
            //PositionsTree.Add(new TestNode("C"));
        }

        void BuildBranch(PositionNode positionNode, ObservableCollection<Position> positions)
        {
            var childrenPositions = positions.Where(c => c.ParentId == positionNode.Id);

            positionNode.Nodes = new ObservableCollection<PositionNode>();

            foreach (var pos in childrenPositions)
            {
                PositionNode posNode = new PositionNode(pos);
                BuildBranch(posNode, positions);
                positionNode.Nodes.Add(posNode);
            }

        }
    }
}
