using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmProofs.DataStructures
{
    public class BinarySearchTree
    {
        private const string B = "/ \\ ";

        public Node Root { get; private set; }

        public int Count { get; set; }

        public BinarySearchTree()
        {

        }

        public BinarySearchTree(params int[] numbers)
        {
            foreach (var number in numbers)
            {
                Add(number);
            }
        }

        public void Add(int number)
        {
            Count++;

            if (Root == null)
            {
                Root = new Node(number);

                return;
            }

            Add(Root, number);
        }

        private void Add(Node parent, int number)
        {
            if (number < parent.Number)
            {
                if (parent.Left == null)
                {
                    parent.Left = new Node(parent, number);
                }
                else
                {
                    Add(parent.Left, number);
                }
            }
            else
            {
                if (parent.Right == null)
                {
                    parent.Right = new Node(parent, number);
                }
                else
                {
                    Add(parent.Right, number);
                }
            }
        }
        
        public void Remove(int number)
        {
            Count--;
        }

        private Dictionary<int, List<Node>> GetAll()
        {
            var dict = new Dictionary<int, List<Node>>();

            dict.Add(0, new List<Node>() { Root });

            GetChildren(dict, Root);

            return dict;
        }

        private void GetChildren(Dictionary<int, List<Node>> dict, Node parent)
        {
            if (parent.Left != null)
            {
                AddOrUpdate(dict, parent.Left);

                GetChildren(dict, parent.Left);
            }

            if (parent.Right == null) return;

            AddOrUpdate(dict, parent.Right);

            GetChildren(dict, parent.Right);
        }

        private void AddOrUpdate(Dictionary<int, List<Node>> dict, Node node)
        {
            if (!dict.TryGetValue(node.Depth, out var lst))
            {
                lst = new List<Node>();

                dict.Add(node.Depth, lst);
            }

            lst.Add(node);
        }

        public override string ToString()
        {
            var dict = GetAll();

            //Depth is zero based, so adding one
            var maxDepth = dict.Keys.Max() + 1;

            var rows = new List<string>(dict.Count);

            //Create the tree from the bottom up
            for (var i = dict.Count - 1; i >= 0; i--)
            {
                var lst = dict[i];

                var row = i + 1; //Depth is zero based, so advancing it

                //PrintBranches(i, maxDepth);

                rows.Add(PrintNumbers(lst, row));
            }

            rows.Reverse();

            var str = string.Join(Environment.NewLine, rows);

            return str;
        }

        private string PrintNumbers(List<Node> nodes, int row)
        {
            var sb = new StringBuilder();

            //var spaces = row * 2; //14

            sb.Append("  ");

            //  4   5  6   7
            //foreach (var node in nodes)
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];

                sb.Append(node.Number).Append("   ");

                if (i % 2 == 1)
                {
                    sb.Append("  ");
                }
            }

            var str = sb.ToString();

            return str;
        }

        private void PrintBranches(StringBuilder sb, int depth, int maxDepth)
        {
            if (depth == 0) return;

            var repeat = depth + 3;

            var space = new string(' ', repeat);

            for (var i = 0; i < depth; i++)
            {
                sb.Append(space).Append(B);
            }

            sb.AppendLine();
        }

        public class Node
        {
            public Node(int number)
                : this(null, number)
            {

            }

            public Node(Node parent, int number)
            {
                Parent = parent;

                Depth = (parent?.Depth + 1) ?? 0;

                Number = number;
            }

            public int Number { get; set; }

            public int Depth { get; set; }

            public Node Parent { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }  
        }
    }
}
