using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Stream
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1(File.ReadAllText("input.txt")));
            Console.ReadLine();
        }

        private static int Part1(string rawInput)
        {
            var input = ClearExclamationMarks(rawInput).Skip(1);
            var root = new Group(null, 1);
            Node node = root;
            foreach (var @char in input)
            {
                node = node.Process(@char);
            }

            return root.GetTotalValue();
        }

        private static string ClearExclamationMarks(string input)
        {
            var result = new StringBuilder();
            var ignoring = false;
            foreach (var @char in input)
            {
                if (!ignoring)
                {
                    if (@char == '!')
                    {
                        ignoring = true;
                    }

                    if (!ignoring)
                    {
                        result.Append(@char);
                    }
                }
                else
                {
                    ignoring = false;
                }
            }

            return result.ToString();
        }

        abstract class Node
        {
            protected Node Parent { get; }
            protected IList<Node> Children { get; }

            protected Node(Node parent)
            {
                Parent = parent;
                Parent?.Children.Add(this);
                Children = new List<Node>();
            }

            public abstract Node Process(char @char);

            public abstract int GetTotalValue();
        }

        class Group : Node
        {
            private int Value { get; set; }

            public Group(Node parent, int value) : base(parent)
            {
                Value = value;
            }
            
            public override Node Process(char @char)
            {
                if (@char == '}')
                {
                    return Parent;
                }

                if (@char == '{')
                {
                    return new Group(this, Value+1);
                }

                if (@char == '<')
                {
                    return new Garbage(this);
                }

                return this;
            }

            public override int GetTotalValue()
            {
                return Value + Children.Sum(_ => _.GetTotalValue());
            }
        }

        class Garbage : Node
        {
            public Garbage(Node parent) : base(parent) { }

            public override Node Process(char @char)
            {
                if (@char == '>')
                {
                    return Parent;
                }

                return this;
            }

            public override int GetTotalValue()
            {
                return 0;
            }
        }
    }
}
