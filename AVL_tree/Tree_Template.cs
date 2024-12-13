namespace AVL_tree;

public class Node //класс узла дерева
{
    public Key Key;
    public int Count;
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

    public int getValue(Key key) //метод сравнения ключей по ФИО / времени
    {
        if (key == null)
            return 0;
        
        int fullnameValue = String.Compare(FullName, key.FullName, StringComparison.Ordinal);
        if (fullnameValue != 0)
            return fullnameValue;
        
        if (Time.Hours != key.Time.Hours)
            return Time.Hours.CompareTo(key.Time.Hours);
        
        return Time.Minutes.CompareTo(key.Time.Minutes);
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

public class Tree
{
    private void Search(Key key, ref Node p, ref bool h) // Метод поиска и вставки в AVL-дерево
    {
        if (p == null)
        {
            // Вставка нового узла, если текущий узел равен null
            p = new Node(key)
            {
                Key = key,
                Count = 1,
                Left = null,
                Right = null,
                Balance = 0
            };
            h = true; // Высота дерева увеличилась
        }
        else if (key < p.Key)
        {
            // Рекурсивный поиск в левом поддереве
            Search(key, ref p.Left, ref h);

            if (h)
            {
                // Левая ветвь выросла
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
                    // Баланс = -1: требуется восстановление баланса
                    Node p1 = p.Left;

                    if (p1.Balance == -1)
                    {
                        // Одиночная LL-ротация
                        p.Left = p1.Right;
                        p1.Right = p;
                        p.Balance = 0;
                        p = p1;
                    }
                    else
                    {
                        // Двойная LR-ротация
                        Node p2 = p1.Right;
                        p1.Right = p2.Left;
                        p2.Left = p1;
                        p.Left = p2.Right;
                        p2.Right = p;

                        // Обновление балансов
                        p.Balance = (p2.Balance == -1) ? 1 : 0;
                        p1.Balance = (p2.Balance == 1) ? -1 : 0;
                        p = p2;
                    }

                    p.Balance = 0;
                    h = false;
                }
            }
        }
        else if (key > p.Key)
        {
            // Рекурсивный поиск в правом поддереве
            Search(key, ref p.Right, ref h);

            if (h)
            {
                // Правая ветвь выросла
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
                    // Баланс = +1: требуется восстановление баланса
                    Node p1 = p.Right;

                    if (p1.Balance == 1)
                    {
                        // Одиночная RR-ротация
                        p.Right = p1.Left;
                        p1.Left = p;
                        p.Balance = 0;
                        p = p1;
                    }
                    else
                    {
                        // Двойная RL-ротация
                        Node p2 = p1.Left;
                        p1.Left = p2.Right;
                        p2.Right = p1;
                        p.Right = p2.Left;
                        p2.Left = p;

                        // Обновление балансов
                        p.Balance = (p2.Balance == 1) ? -1 : 0;
                        p1.Balance = (p2.Balance == -1) ? 1 : 0;
                        p = p2;
                    }

                    p.Balance = 0;
                    h = false;
                }
            }
        }
        else
        {
            // Ключ уже существует, увеличиваем счетчик
            p.Count++;
        }
    }


    private void BalanceLeft(ref Node p, ref bool h)
    {
        if (p.Balance == -1)
        {
            // уменьшилась левая ветвь
            p.Balance = 0;
        }
        else if (p.Balance == 0)
        {
            p.Balance = 1;
            h = false;
        }
        else
        {
            // восстановить баланс
            Node p1 = p.Right;

            if (p1.Balance >= 0)
            {
                // одиночная RR-ротация
                p.Right = p1.Left;
                p1.Left = p;

                if (p1.Balance == 0)
                {
                    p.Balance = 1;
                    p1.Balance = -1;
                    h = false;
                }
                else
                {
                    p.Balance = 0;
                    p1.Balance = 0;
                }

                p = p1;
            }
            else
            {
                // Двойная RL ротация
                Node p2 = p1.Left;
                p1.Left = p2.Right;
                p2.Right = p1;
                p.Right = p2.Left;
                p2.Left = p;
                
                if (p2.Balance == +1)
                    p.Balance = -1;
                else
                    p.Balance = 0;

                if (p2.Balance == -1)
                    p1.Balance = 1;
                else
                    p1.Balance = 0;

                p = p2;
                p2.Balance = 0;
            }
        }
    }

    private void BalanceRight(ref Node p, ref bool h)
    {
        if (p.Balance == 1)
        {
            // уменьшилась правая ветвь
            p.Balance = 0;
        }
        else if (p.Balance == 0)
        {

            p.Balance = -1;
            h = false;
        }
        else
        {
            // rebalance
            Node p1 = p.Left;

            if (p1.Balance <= 0)
            {
                // Одиночная LL ротация
                p.Left = p1.Right;
                p1.Right = p;

                if (p1.Balance == 0)
                {
                    p.Balance = -1;
                    p1.Balance = 1;
                    h = false;
                }
                else
                {
                    p.Balance = 0;
                    p1.Balance = 0;
                }

                p = p1;
            }
            else
            {
                // Двойная LR ротация
                Node p2 = p1.Right;
                p1.Right = p2.Left;
                p2.Left = p1;
                p.Left = p2.Right;
                p2.Right = p;
                
                if (p2.Balance == -1)
                    p.Balance = 1;
                else
                    p.Balance = 0;

                if (p2.Balance == +1)
                    p1.Balance = -1;
                else
                    p1.Balance = 0;

                p = p2;
                p2.Balance = 0;
            }
        }
    }
}