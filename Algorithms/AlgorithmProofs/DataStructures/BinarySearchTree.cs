﻿using System;
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

            AddOrUpdate(dict, level, parent.Left);

            if (parent.Left != null)
            {
                GetChildren(dict, parent.Left);
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

            GetChildren(dict, parent.Right);
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

        public override string ToString()
        {
            var dict = GetAll();

            var sb = new StringBuilder();

            //Minus 2 because fake row at the bottom and the formula requires -1
            var j = dict.Count - 2;

            for (var i = 1; i < dict.Count; i++)
            {
                var spaces = Convert.ToInt32(Math.Pow(2, j - 1));

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
