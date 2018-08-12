using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmProofs.DataStructures
{
    public class BinarySearchTree
    {
        private const char S = ' ';
        private const char L = '/';
        private const char R = '\\';

        public enum Side
        {
            Root,
            Left,
            Right
        }

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

        private void SetMaxDepth(Node node)
        {
            if (node.Level <= MaxLevel) return;

            MaxLevel = node.Level;

            MaxSize = MaxElements(MaxLevel);
        }

        private int MaxNodesForLevel(int level)
        {
            //Max nodes for level = 2^(L - 1)
            var e = Convert.ToInt32(Math.Pow(2, level - 1));

            return e;
        }

        private int MaxElements(int maxLevels)
        {
            //Max size = 2^L - 1
            var e = Convert.ToInt32(Math.Pow(2, maxLevels) - 1);

            return e;
        }

        private void Add(int number)
        {
            Count++;

            if (Root == null)
            {
                Root = new Node(number);
                Root.Side = Side.Root;

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
                    parent.Left.Side = Side.Left;

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
                    parent.Right.Side = Side.Right;

                    SetMaxDepth(parent.Right);
                }
                else
                {
                    Add(parent.Right, number);
                }
            }
        }

        public Node Search(int number)
        {
            var node = Search(Root, number);

            return node;
        }

        private Node Search(Node parent, int number)
        {
            if (IsFound(parent, number)) return parent;

            Node node;

            if (number < parent.Number)
            {
                if (parent.Left == null) return null;

                node = Search(parent.Left, number);
            }
            else
            {
                if (parent.Right == null) return null;

                node = Search(parent.Right, number);
            }

            return node;
        }

        private bool IsFound(Node node, int number)
        {
            if (node == null) return false;

            var isFound = node.Number == number;

            return isFound;
        }

        //This has a very good explanation as to how to delete a node
        //https://www.youtube.com/watch?v=wcIRPqTR3Kc
        public void Remove(int number)
        {
            var t = Search(number);

            if (t == null) return;

            Count--;

            //To remove a node, you have to disassociate all of its parts
            /*       P
             *      / 
             *    T       <-- target
             *   / \
             * L    R  
             *
             * The replacement node has to be a node with no children
             * that still satisfies the current conditions. Look to the
             * right of the node to find something just larger than the
             * target node*/

            //If the right is null then return left
            var n = t.Right == null ? t.Left : PromoteNextNode(t.Right);

            //This is sort of cheating, but works in this situation
            t.Number = n.Number; //Promoting the number, no disassociation needed

            var p = n.Parent;

            //Get the non-null child
            var c = n.Left ?? n.Right;

            //Update the correct side of the parent's reference
            if (n.Side == Side.Left)
            {
                p.Left = c;
            }
            else
            {
                p.Right = c;
            }

            //Update the child's link
            if (c == null) return;

            c.Parent = p;
        }

        //Find the next eligible node for promotion. It must have zero to one children.
        private Node PromoteNextNode(Node node)
        {
            var children = node.CountChildren();

            if (children < 2) return node;

            var n = PromoteNextNode(node.Left);

            return n;
        }

        private List<int> GetValuesAsList(Node startingNode)
        {
            var lst = new List<int>();

            GetValuesAsList(lst, startingNode);

            return lst;
        }

        private void GetValuesAsList(List<int> values, Node parent)
        {
            if (parent == null) return;
            
            values.Add(parent.Number);

            if (parent.Left != null)
            {
                GetValuesAsList(values, parent.Left);
            }

            if (parent.Right == null) return;

            GetValuesAsList(values, parent.Right);
        }

        #region Printing
        public override string ToString()
        {
            var dict = GetAll();

            var sb = new StringBuilder();

            //Minus 2 because fake row at the bottom and the formula requires -1
            var j = dict.Count - 2;

            for (var i = 1; i < dict.Count; i++)
            {
                var spaces = MaxNodesForLevel(j);

                j--;

                var span = new string(S, spaces);

                var lst = dict[i];

                //Branches row
                if (i > 1)
                {
                    sb.Append(span);

                    PrintBranches(sb, lst.Count, span)
                        .AppendLine();
                }

                //Numbers row
                sb.Append(span);

                sb.Append(PrintNumbers(lst, span));

                sb.AppendLine();
            }

            var str = sb.ToString();

            return str;
        }

        private Dictionary<int, List<Node>> GetAll()
        {
            var dict = new Dictionary<int, List<Node>>();

            //This is a base case - Root node is level 1 always
            dict.Add(1, new List<Node>() { Root });

            GetChildrenForPrint(dict, Root);

            return dict;
        }

        private void GetChildrenForPrint(Dictionary<int, List<Node>> dict, Node parent)
        {
            //Storing node regardless of value because it is about position
            //Since the children can be null, predict the maxLevels
            //This will produce a fake row which should be removed later
            var level = parent.Level + 1;

            AddOrUpdate(dict, level, parent.Left);

            if (parent.Left != null)
            {
                GetChildrenForPrint(dict, parent.Left);
            }
            else
            {
                AddNullChildren(dict, level);
            }

            AddOrUpdate(dict, level, parent.Right);

            if (parent.Right == null)
            {
                AddNullChildren(dict, level);

                return;
            }

            GetChildrenForPrint(dict, parent.Right);
        }

        private void AddNullChildren(Dictionary<int, List<Node>> dict, int level)
        {
            var nextLevel = level + 1;

            AddOrUpdate(dict, nextLevel, null, null);
        }

        private void AddOrUpdate(Dictionary<int, List<Node>> dict, int level, params Node[] node)
        {
            if (!dict.TryGetValue(level, out var lst))
            {
                lst = new List<Node>();

                dict.Add(level, lst);
            }

            lst.AddRange(node);
        }

        public string PrintStats()
        {
            var str =
                $"Count     : {Count}\n" +
                $"Max Levels: {MaxLevel}\n" +
                $"Max Size  : {MaxSize}";

            return str;
        }

        private string PrintNumbers(List<Node> nodes, string span)
        {
            var lst = nodes.Select(x => x?.Number.ToString() ?? "n").ToList();

            var str = string.Join(span, lst);

            return str;
        }

        private StringBuilder PrintBranches(StringBuilder sb, int repeatFor, string span)
        {
            for (var i = 0; i < repeatFor; i++)
            {
                //Alternate between left and right
                var c = i % 2 == 0 ? L : R;

                sb.Append(c).Append(span);
            }

            return sb;
        } 
        #endregion

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

            public Side Side { get; set; }

            public Node Parent { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public int CountChildren()
            {
                var count = 0;

                if (Left != null) count++;

                if (Right != null) count++;

                return count;
            }

            public override string ToString()
            {
                return $"Number: {Number}, Level: {Level}";
            }
        }
    }
}
