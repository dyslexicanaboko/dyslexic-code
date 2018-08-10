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

        public int Count { get; private set; }

        public int MaxLevel { get; private set; }

        public int MaxSize { get; private set; }

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

                    SetMaxDepth(parent.Left);
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

                    SetMaxDepth(parent.Right);
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

            //This is a base case
            dict.Add(1, new List<Node>() { Root });

            GetChildren(dict, Root);

            return dict;
        }

        private void GetChildren(Dictionary<int, List<Node>> dict, Node parent)
        {
            //Storing node regardless of value because it is about position

            //Since the children can be null, predict the level
            var level = parent.Level + 1;

            AddOrUpdate(dict, parent.Left, level);

            if (parent.Left != null)
            {
                GetChildren(dict, parent.Left);
            }

            AddOrUpdate(dict, parent.Right, level);

            if (parent.Right == null) return;

            GetChildren(dict, parent.Right);
        }

        private void AddOrUpdate(Dictionary<int, List<Node>> dict, Node node, int depth)
        {
            if (!dict.TryGetValue(depth, out var lst))
            {
                lst = new List<Node>();

                dict.Add(depth, lst);
            }

            lst.Add(node);
        }

        public override string ToString()
        {
            var dict = GetAll();

            var maxWidth = Count * 2;

            var rows = new List<string>(dict.Count);

            //Create the tree from the bottom up
            for (var i = dict.Count - 1; i > 0; i--)
            {
                var lst = dict[i];

                var row = i + 1; //Level is zero based, so advancing it

                //PrintBranches(i, maxDepth);

                rows.Add(PrintNumbers(lst));
            }

            rows.Reverse();

            var str = string.Join(Environment.NewLine, rows);

            return str;
        }

        private string PrintNumbers(List<Node> nodes)
        {
            var lst = nodes.Select(x => x?.Number.ToString() ?? "[]").ToList();

            var str = string.Join(" ", lst);

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

        private void SetMaxDepth(Node node)
        {
            if (node.Level <= MaxLevel) return;

            MaxLevel = node.Level;

            //MaxSize = 2^L - 1
            MaxSize = MaxNodesPerLevel(MaxLevel);
        }

        private int MaxNodesPerLevel(int level)
        {
            var e = Convert.ToInt32(Math.Pow(2, level) - 1);

            return e;
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

                Level = (parent?.Level + 1) ?? 1;

                Number = number;
            }

            public int Number { get; set; }

            public int Level { get; set; }

            public Node Parent { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }  
        }
    }
}
