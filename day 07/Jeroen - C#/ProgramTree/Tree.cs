using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProgramTree
{
    class Tree
    {
        public static Tree Parse(string input)
        {
            using (var reader = new StringReader(input))
            {
                var items = (
                    from line in reader.ReadLines()
                    select line.Split("->")
                    into split
                    let parentStr = split[0].Split(' ')
                    let parentLabel = parentStr[0]
                    let weight = int.Parse(parentStr[1].Trim(' ', '(', ')'))
                    let children = split.Length > 1 ? split[1].Trim().Split(", ") : new string[0]
                    select (node: parentLabel, weight: weight, children: children)
                ).ToList();


                var edges = (
                    from item in items
                    let parent = item.node
                    from child in item.children
                    select (parent: parent, child: child)
                );

                return new Tree(items.Select(item => (item.node, item.weight)), edges);
            }
        }

        public Tree(IEnumerable<(string label, int weight)> nodes, IEnumerable<(string parent, string child)> edges)
        {
            Nodes = nodes
                .Select(i => new Node(i.label, i.weight))
                .ToDictionary(n => n.Label);


            foreach (var edge in edges)
            {
                var parent = Nodes[edge.parent];
                var child = Nodes[edge.child];
                parent.AddChild(child);
                child.SetParent(parent);
            }
        }

        private Dictionary<string, Node> Nodes { get; }
        public Node Root => Nodes.Single(n => n.Value.Parent == null).Value;

        public Node Find(Func<Node, bool> predicate)
        {
            return Root.Traverse().FirstOrDefault(predicate);
        }

    };
    class Node
    {
        public readonly string Label;
        private readonly int _weight;
        public Node Parent => _parent;
        private readonly List<Node> _children = new List<Node>();

        public int Weight
        {
            get
            {
                return Traverse().Select(n => n._weight).Sum();
            }
        }

        public override string ToString()
        {
            return $"{Label} ({Weight})";
        }

        public bool HasValidWeight => !Children.Any() || Children.Select(c => c.Weight).Distinct().Count() == 1;

        private Node _parent;
        public Node(string label, int weight)
        {
            Label = label;
            _weight = weight;
        }

        public void AddChild(Node child)
        {
            _children.Add(child);
        }

        public IReadOnlyCollection<Node> Children => _children;
        public int PrivateWeight => _weight;
        public IEnumerable<Node> Siblings => Parent.Children.Where(n => n.Label != Label);

        public void SetParent(Node parent)
        {
            _parent = parent;
        }

        public IEnumerable<Node> Traverse()
        {
            yield return this;
            foreach (var child in Children)
            {
                foreach (var node in child.Traverse())
                    yield return node;
            }
        }
    }
    static class Ex
    {
        public static IEnumerable<string> ReadLines(this TextReader reader)
        {
            while (reader.Peek() >= 0)
            {
                yield return reader.ReadLine();
            }
        }
    }

}