namespace AVL_tree;

public class Node //класс узла дерева
{
    public Key Key;
    private int Count;
    public Node Left;
    public Node Right;
    public int Balance;

    public Node(Key key)
    {
        this.Key = key;
    }
}
public class Key //класс ключа дерева
{
    public string FullName;
    public TimeClass Time;

    public Key(string fullName, int hours, int minutes)
    {
        FullName = fullName;
        Time = new TimeClass(hours, minutes);
    }

    public int CompareTo(Key other) //метод сравнения ключей по ФИО / времени
    {
        if (other == null)
            return 1;
        
        int nameComparison = String.Compare(FullName, other.FullName, StringComparison.Ordinal);
        if (nameComparison != 0)
            return nameComparison;
        
        if (Time.Hours != other.Time.Hours)
            return Time.Hours.CompareTo(other.Time.Hours);
        
        return Time.Minutes.CompareTo(other.Time.Minutes);
    }
}

public class TimeClass // Класс времни
{
    public int Hours { get; }
    public int Minutes { get; }

    public TimeClass(int hours, int minutes)
    {
        Hours = hours;
        Minutes = minutes;
    }
}

public class Tree // Класс дерева
{
    public Node Root;

    private bool HeightIncreased;

    public Node Search(Node node, Key key)
    {
        if (node == null)
        {
            HeightIncreased = true;
            return new Node(key);
        }

        if (key.CompareTo(node.Key) < 0)
        {
            node.Left = Search(node.Left, key);

            if (HeightIncreased)
            {
                switch (node.Balance)
                {
                    case 1:
                        node.Balance = 0;
                        HeightIncreased = false;
                        break;
                    case 0:
                        node.Balance = -1;
                        break;
                    case -1:
                        node = BalanceLeft(node);
                        HeightIncreased = false;
                        break;
                }
            }
        }
        else if (key.CompareTo(node.Key) > 0)
        {
            node.Right = Search(node.Right, key);

            if (HeightIncreased)
            {
                switch (node.Balance)
                {
                    case 1:
                        node = BalanceRight(node);
                        HeightIncreased = false;
                        break;
                    case 0:
                        node.Balance = 1;
                        break;
                    case -1:
                        node.Balance = 0;
                        HeightIncreased = false;
                        break;
                }
            }
        }
        else
        {
            HeightIncreased = false;
        }
        return node;
    }

    public void Insert(Key key)
    {
        Root = Search(Root, key);
    }

    private Node BalanceLeft(Node node)
    {
        Node left = node.Left;

        if (left.Balance == -1)
        {
            // LL
            node = RotateRight(node);
            node.Balance = 0;
            node.Right.Balance = 0;
        }
        else
        {
            // LR
            Node leftRight = left.Right;
            node.Left = RotateLeft(left);
            node = RotateRight(node);
            
            if (leftRight.Balance == -1)
            {
                node.Right.Balance = 1;
                node.Left.Balance = 0;
            }
            else if (leftRight.Balance == 1)
            {
                node.Right.Balance = 0;
                node.Left.Balance = -1;
            }
            else
            {
                node.Right.Balance = 0;
                node.Left.Balance = 0;
            }

            node.Balance = 0;
        }

        return node;
    }
    
    private Node BalanceRight(Node node)
    {
        Node right = node.Right;

        if (right.Balance == 1)
        {
            // RR
            node = RotateLeft(node);
            node.Balance = 0;
            node.Left.Balance = 0;
        }
        else
        {
            // RL
            Node rightLeft = right.Left;
            node.Right = RotateRight(right);
            node = RotateLeft(node);

            if (rightLeft.Balance == 1)
            {
                node.Left.Balance = -1;
                node.Right.Balance = 0;
            }
            else if (rightLeft.Balance == -1)
            {
                node.Left.Balance = 0;
                node.Right.Balance = 1;
            }
            else
            {
                node.Left.Balance = 0;
                node.Right.Balance = 0;
            }

            node.Balance = 0;
        }

        return node;
    }
    
    private Node RotateRight(Node node) // Правый поворот
    {
        Node newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;
        return newRoot;
    }
    
    private Node RotateLeft(Node node) // Левый поворот
    {
        Node newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;
        return newRoot;
    }
    
}