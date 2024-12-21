using System;

namespace AVL_tree
{
    public class FIO
    {
        public string SecondName { get; }
        public string FirstName { get; }
        public string Surname { get; }

        public FIO(string secondName, string firstName, string surname)
        {
            SecondName = secondName;
            FirstName = firstName;
            Surname = surname;
        }

        public int CompareTo(FIO another)
        {
            if (another == null)
                return 1;

            int compareSecondName = string.Compare(SecondName, another.SecondName, StringComparison.Ordinal);
            if (compareSecondName != 0)
                return compareSecondName;

            int compareFirstName = string.Compare(FirstName, another.FirstName, StringComparison.Ordinal);
            if (compareFirstName != 0)
                return compareFirstName;

            return string.Compare(Surname, another.Surname, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"{SecondName} {FirstName} {Surname}";
        }
    }

    public class TimeClass
    {
        public int Hours { get; }
        public int Minutes { get; }

        public TimeClass(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public int CompareTimeTo(TimeClass another)
        {
            if (another == null)
                return 1;

            if (Hours > another.Hours)
                return 1;
            if (Hours < another.Hours)
                return -1;

            if (Minutes > another.Minutes)
                return 1;
            if (Minutes < another.Minutes)
                return -1;

            return 0;
        }

        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}";
        }
    }

    public class Key
    {
        public FIO Name { get; }
        public TimeClass Time { get; }

        public Key(FIO name, TimeClass time)
        {
            Name = name;
            Time = time;
        }

        public int CompareKeyTo(Key another)
        {
            if (another == null)
                return 1;

            int nameComparison = Name.CompareTo(another.Name);
            if (nameComparison != 0)
                return nameComparison;

            return Time.CompareTimeTo(another.Time);
        }

        public override string ToString()
        {
            return $"{Name}, Time: {Time}";
        }
    }

    public class Node
    {
        public Key Key;
        public List<int> Value;
        //public int Count;
        public Node Left;
        public Node Right;
        public int Balance;

        public Node(Key key, int value)
        {
            Key = key;
            Value = new List<int> { value }; 
            //Count = 1;
            Left = null;
            Right = null;
            Balance = 0;
        }
    }

    public class Tree
    {
        public Node Root;

        public Tree()
        {
            Root = null;
        }

        public void Search(Key x, ref Node p, ref bool h, int value)
        {
            if (p == null)
            {
                p = new Node(x, value);
                h = true;
            }
            else if (x.CompareKeyTo(p.Key) < 0)
            {
                Search(x, ref p.Left, ref h, value);
                if (h)
                {
                    if (p.Balance == -1)
                    {
                        p.Balance = 0;
                        h = false;
                    }
                    else if (p.Balance == 0)
                    {
                        p.Balance = 1;
                    }
                    else
                    {
                        BalanceLeft(ref p);
                        h = false;
                    }
                }
            }
            else if (x.CompareKeyTo(p.Key) > 0)
            {
                Search(x, ref p.Right, ref h, value);
                if (h)
                {
                    if (p.Balance == 1)
                    {
                        p.Balance = 0;
                        h = false;
                    }
                    else if (p.Balance == 0)
                    {
                        p.Balance = -1;
                    }
                    else
                    {
                        BalanceRight(ref p);
                        h = false;
                    }
                }
            }
            else
            {
                p.Value.Add(value);
                h = false;
            }
        }

        private void BalanceLeft(ref Node p)
        {
            Node p1 = p.Left;
            if (p1.Balance == -1)
            {
                p.Left = p1.Right;
                p1.Right = p;
                p.Balance = 0;
                p = p1;
            }
            else
            {
                Node p2 = p1.Right;
                p1.Right = p2.Left;
                p2.Left = p1;
                p.Left = p2.Right;
                p2.Right = p;

                if (p2.Balance == -1)
                    p.Balance = 1;
                else
                    p.Balance = 0;

                if (p2.Balance == 1)
                    p1.Balance = -1;
                else
                    p1.Balance = 0;

                p = p2;
            }
            p.Balance = 0;
        }

        private void BalanceRight(ref Node p)
        {
            Node p1 = p.Right;
            if (p1.Balance == 1)
            {
                p.Right = p1.Left;
                p1.Left = p;
                p.Balance = 0;
                p = p1;
            }
            else
            {
                Node p2 = p1.Left;
                p1.Left = p2.Right;
                p2.Right = p1;
                p.Right = p2.Left;
                p2.Left = p;

                if (p2.Balance == 1)
                    p.Balance = -1;
                else
                    p.Balance = 0;

                if (p2.Balance == -1)
                    p1.Balance = 1;
                else
                    p1.Balance = 0;

                p = p2;
            }
            p.Balance = 0;
        }

        public void Delete(Key x, ref Node p, ref bool h)
        {
            if (p == null)
            {
                return;
            }

            if (x.CompareKeyTo(p.Key) < 0)
            {
                Delete(x, ref p.Left, ref h);
                if (h)
                    BalanceRight(ref p);
            }
            else if (x.CompareKeyTo(p.Key) > 0)
            {
                Delete(x, ref p.Right, ref h);
                if (h)
                    BalanceLeft(ref p);
            }
            else
            {
                Node q = p;
                if (q.Right == null)
                {
                    p = q.Left;
                    h = true;
                }
                else if (q.Left == null)
                {
                    p = q.Right;
                    h = true;
                }
                else
                {
                    Del(ref q.Left, ref h);
                    if (h)
                        BalanceRight(ref p);
                }
            }
        }

        private void Del(ref Node r, ref bool h)
        {
            if (r.Right != null)
            {
                Del(ref r.Right, ref h);
                if (h)
                    BalanceLeft(ref r);
            }
            else
            {
                r = r.Left;
                h = true;
            }
        }
        
        // Печать дерева
     
        public void PrintTree()
        {
            string filePath = "C:\\Users\\Nikita\\Desktop\\My programms\\AVL-tree\\AVL_tree\\Output.txt";
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    string values = Root.Value != null && Root.Value.Count > 0
                        ? $" [{string.Join(", ", Root.Value)}]"
                        : " []";

                    Console.WriteLine($"{Root.Key.Name.ToString() + " " + Root.Key.Time.ToString()}{values}");
                    writer.WriteLine($"{Root.Key.Name.ToString() + " " + Root.Key.Time.ToString()}{values}");

                    PrintSubTree(writer, Root.Left, "", Root.Right == null);
                    PrintSubTree(writer, Root.Right, "", true);
                }
            }
        }

        private void PrintSubTree(StreamWriter writer, Node node, string prefix, bool isLast)
        {
            string filePath = "C:\\Users\\Nikita\\Desktop\\My programms\\AVL-tree\\AVL_tree\\Output.txt";
            
                if (node == null)
                    return;

                Console.Write(prefix);
                writer.Write(prefix);
                
                if (isLast)
                {
                    Console.Write("└─");
                    writer.Write("└─");
                    prefix += "  ";
                }
                else
                {
                    Console.Write("├─");
                    writer.Write("├─");
                    prefix += "│ ";
                }

                string values = node.Value != null && node.Value.Count > 0
                    ? $" [{string.Join(", ", node.Value)}]"
                    : " []";

                Console.WriteLine($"{node.Key.Name.ToString() + " " + node.Key.Time.ToString()}{values}");
                writer.WriteLine($"{node.Key.Name.ToString() + " " + node.Key.Time.ToString()}{values}");

                if (node.Left != null || node.Right != null)
                {
                    PrintSubTree(writer, node.Left, prefix, node.Right == null);
                    PrintSubTree(writer, node.Right, prefix, true);
                
            }
        }
        
        public void DeleteIfMinRight(Key key, int value)
        {
            bool heightChanged = false;
            Root = DeleteIfMinRight(Root, key, value, ref heightChanged);
        }

        private Node DeleteIfMinRight(Node node, Key key, int value, ref bool heightChanged)
        {
            if (node == null)
                return null;

            int comparison = key.CompareKeyTo(node.Key);
            if (comparison < 0) // Ищем в левом поддереве
            {
                node.Left = DeleteIfMinRight(node.Left, key, value, ref heightChanged);
                if (heightChanged)
                    BalanceRight(ref node);
            }
            else if (comparison > 0) // Ищем в правом поддереве
            {
                node.Right = DeleteIfMinRight(node.Right, key, value, ref heightChanged);
                if (heightChanged)
                    BalanceLeft(ref node);
            }
            else // Если ключ найден
            {
                if (node.Value.Contains(value))
                {
                    Node minRightNode = FindMin(node.Right);

                    if (minRightNode != null && node.Key.CompareKeyTo(minRightNode.Key) == 0)
                    {
                        node.Right = DeleteMin(node.Right, ref heightChanged);
                        if (heightChanged)
                            BalanceLeft(ref node);
                    }
                }
            }

            return node;
        }

        private Node FindMin(Node node)
        {
            if (node == null)
                return null;

            while (node.Left != null)
                node = node.Left;

            return node;
        }

        private Node DeleteMin(Node node, ref bool heightChanged)
        {
            if (node.Left == null)
            {
                heightChanged = true;
                return node.Right;
            }

            node.Left = DeleteMin(node.Left, ref heightChanged);

            if (heightChanged)
                BalanceRight(ref node);

            return node;
        }
    }
}