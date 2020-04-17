using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Homework_12.Model
{
    class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }

        public Node(int id, string name)
        {
            Nodes = new ObservableCollection<Node>();
            Id = id;
            Name = name;
        }
    }
}
