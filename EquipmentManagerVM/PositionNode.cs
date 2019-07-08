using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace EquipmentManagerVM
{
    public class PositionNode
    {
        public Position Node { get; set; }
        public ObservableCollection<PositionNode> Nodes { get; set; }

        public PositionNode(Position position)
        {
            Node = position;
        }
    }
}
