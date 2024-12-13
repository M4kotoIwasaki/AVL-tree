/*public class Tree // Класс дерева
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
    }*/


/*private Node RotateRight(Node node) // Правый поворот
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
    */