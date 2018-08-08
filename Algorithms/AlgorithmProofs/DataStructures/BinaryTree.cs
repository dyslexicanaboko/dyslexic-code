namespace AlgorithmProofs.DataStructures
{
    public class BinaryTree
    {
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

        public Node Root { get; private set; }

        public int Count { get; set; }

        //public override string ToString()
        //{
            
        //}

        //private StringBuilder PrettyPrint(Node node)
        //{

        //}

        //private StringBuilder PrettyPrint(Node node, StringBuilder stringBuilder)
        //{

        //}

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
