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

        public PositionsVM(ObservableCollection<Position> positions)
        {
            Positions = positions;

            PositionsTree = new ObservableCollection<PositionNode>();

            //добавляем узлы верхнего уровня
            foreach (var pos in positions)
            {
                if (!pos.ParentId.HasValue)
                    PositionsTree.Add(new PositionNode(pos));
            }

            //добавляем дочерние элементы



            //TestNode node = new TestNode("A");
            //node.Nodes.Add(new TestNode("aaa"));

            //PositionsTree.Add(node);
            //PositionsTree.Add(new TestNode("B"));
            //PositionsTree.Add(new TestNode("C"));
        }
    }
}
