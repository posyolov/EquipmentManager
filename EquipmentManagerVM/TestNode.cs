using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    public class TestNode
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public List<TestNode> Nodes { get; set; }
        //public TestNode Parent { get; set; }

        public TestNode(string name)
        {
            Name = name;
            Nodes = new List<TestNode>();
        }

        //public TestNode(int id)
        //{
        //    Id = id;
        //    Name = string.Empty;
        //    TestNodes = new List<TestNode>();
        //}

        //public TestNode(TestNode parent)
        //{
        //    Id = id;
        //    Name = string.Empty;
        //    TestNodes = new List<TestNode>();
        //    Parent = parent;
        //}
    }
}
