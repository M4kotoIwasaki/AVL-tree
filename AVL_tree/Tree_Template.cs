namespace AVL_tree;

public class Node //класс узла дерева
{
    public Key Key;
    public int Value; //строка
    public int Count;
    public Node Left;
    public Node Right;
    public int Balance;

    public Node(Key key, int value)
    {
        this.Key = key;
        this.Value = value;
    }
}
public class Key //структура ключа дерева
{
    public string SecondName;
    public string FirstName;
    public string Surname;
    public TimeClass Time;

    public Key(string secondName, string firstName, string surname, TimeClass time)
    {
        SecondName = secondName;
        FirstName = firstName;
        Surname = surname;
        Time = time;
    }

    public bool CompareFIOto(Key another)
    {
        if (another == null)
            return false;

        int compareSecondName = string.Compare(SecondName, another.SecondName, StringComparison.Ordinal);
        if (compareSecondName != 0)
            return compareSecondName > 0;

        int compareFirstName = string.Compare(FirstName, another.FirstName, StringComparison.Ordinal);
        if (compareFirstName != 0)
            return compareFirstName > 0;

        int compareSurname = string.Compare(Surname, another.Surname, StringComparison.Ordinal);
        if (compareSurname != 0)
            return compareSurname > 0;

        return false; //если ФИО совпадают
    }


    public bool CompareKeyTo(Key another) //метод сравнения ключей по ФИО / времени
    {
        
    }
}

public class TimeClass // Класс времени
    (int hours, int minutes)
{
    public int Hours { get; } = hours;
    public int Minutes { get; } = minutes;

    public bool CompareTimeTo(TimeClass another) //check if current time > then another
    {
        if (another == null)
            return false;
        if (Hours > another.Hours)
            return true;
        if (Hours < another.Hours)
            return false;
        if (Minutes > another.Minutes)
            return true;
        if (Minutes < another.Minutes)
            return false;
        return false; // ???????????????
    }
}

public class Tree
{
    private void Search(int x, ref Node p, ref bool h) // Метод поиска и вставки в AVL-дерево
    {
        if (p == null)
        {
            // Вставка нового узла, если текущий узел равен null
            p = new Node(x)
            {
                x = x,
                Count = 1,
                Left = null,
                Right = null,
                Balance = 0
            };
            h = true;
        }
        else if (x < p.Key)
        {
            // Рекурсивный поиск в левом поддереве
            Search(x, ref p.Left, ref h);

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
        else if (x > p.Value)
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
    
    public void Delete(Key x, ref Node p, ref bool h)
    {
        if (p == null)
        {
            // Узел с ключом x не найден
            return;
        }

        if (x < p.Value)
        {
            // Рекурсивно удаляем из левого поддерева
            Delete(x, ref p.Left, ref h);
            if (h) 
                BalanceLeft(ref p, ref h); // Балансировка после удаления
        }
        else if (x > p.Value)
        {
            // Рекурсивно удаляем из правого поддерева
            Delete(x, ref p.Right, ref h);
            if (h) 
                BalanceRight(ref p, ref h); // Балансировка после удаления
        }
        else
        {
            // Удаление найденного узла
            Node q = p;

            if (q.Right == null)
            {
                // Нет правого поддерева — сдвиг левого поддерева
                p = q.Left;
                h = true;
            }
            else if (q.Left == null)
            {
                // Нет левого поддерева — сдвиг правого поддерева
                p = q.Right;
                h = true;
            }
            else
            {
                // У узла есть оба поддерева — ищем замену
                Del(ref q.Left, ref h); // Удаляем предшественника
                if (h) 
                    BalanceLeft(ref p, ref h); // Балансировка после удаления
            }
        }
    }

    private void Del(ref Node r, ref bool h)
    {
        if (r.Right != null)
        {
            // Рекурсивно удаляем самый правый узел
            Del(ref r.Right, ref h);
            if (h) BalanceRight(ref r, ref h); // Балансировка после удаления
        }
        else
        {
            // Замещаем значение ключа и удаляем узел
            r.Value = r.Left.Value;
            r = r.Left;
            h = true;
        }
    }
}