﻿/*Реализовать пакет подпрограмм для работы с бинарным деревом поиска (АВЛ), ключ в котором представлен в виде структуры (ФИО + время) (типы полей ключа должны соответствовать типу данных в предметной области - все что является числом, должно храниться в виде числа, а не строки, например, время - это не строка, а структура из двух полей целочисленного типа).
   Основные операции:
   [*] 1. Инициализация (пустого дерева)
   [*] 2. Добавление нового элемента
   [] 3. Удаление заданного элемента (минимальный справа) (при полном совпадении ключа и номера строки)
   [] 4. Поиск заданного элемента
   [*] 5. Печать
   [*] 6. Обход: слева направо
   [*] 7. Освобождение памяти (удаление всего дерева)
   [] 8. (доп. операция - вариант выдается при сдаче задания)
   Входные данные: текстовый файл, каждая из строк которого содержит данные заданной предметной области (ФИО + время).
   Выходные данные: текстовый файл, строки которого содержат данные входного файла в порядке заданного обхода дерева поиска.
   [] 1. Загрузить данные из текстового файла в дерево поиска. При загрузке вызывать процедуру добавления с проверкой ключа на корректность. Список номеров строк входного файла, содержащих заданный ключ, хранить в цепочке (списке) задания 1.1.
   [] 2. Вызвать подпрограммы работы с деревом.
   [] 3. Сохранить итоговое дерево в текстовый файл в порядке заданного обхода.
 */

using AVL_tree;
using System;
using System.Reflection.Metadata.Ecma335;

public class Program
{
    private static Tree tree;

    private static void SuccessMessage()
    {
        Console.Clear();
        Console.WriteLine("\n\t\t--------------");
        Console.WriteLine("\t\t| ВЫПОЛНЕНО! |");
        Console.WriteLine("\t\t--------------\n");
    }

    private static void ShowError(string errorMessage)
    {
        Console.Clear();
        Console.WriteLine("\n\t\t-----------");
        Console.WriteLine("\t\t| ОШИБКА! |");
        Console.WriteLine("\t\t-----------\n");
        Console.WriteLine($"\"{errorMessage}\"");
    }

    public static void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Создать дерево");
            Console.WriteLine("2. Добавить элемент в дерево");
            Console.WriteLine("3. Удалить заданный элемент (минимальный справа)");
            Console.WriteLine("4. Поиск заданного элемента");
            Console.WriteLine("5. Печать дерева");
            Console.WriteLine("6. Обход слева направо");
            Console.WriteLine("7. Освобождение памяти");
            Console.WriteLine("\n9. Выход из программы");

            int choice;
            var tempChoice = Console.ReadLine();
            if (!int.TryParse(tempChoice, out choice))
            {
                ShowError("Введите корректное число.");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
                return;
            }

            switch (choice)
            {
                case 1:
                    MakeTreeFromFile("C:\\Users\\Nikita\\Desktop\\My programms\\AVL-tree\\AVL_tree\\Input.txt");
                    SuccessMessage();
                    break;
                case 2:
                    AddElement("C:\\Users\\Nikita\\Desktop\\My programms\\AVL-tree\\AVL_tree\\Input.txt");
                    SuccessMessage();
                    break;
                case 3:
                    Console.WriteLine("Введите фамилию: ");
                    string secondNameForFio = Console.ReadLine();
                    Console.WriteLine("Введите имя: ");
                    string nameForFIO = Console.ReadLine();
                    Console.WriteLine("Введите отчество: ");
                    string surnameForFio = Console.ReadLine();
                    FIO name = new FIO(secondNameForFio, nameForFIO, surnameForFio);
                    bool isValidTime = false;
                    int hours = 0;
                    int minutes = 0;
                    while (!isValidTime)
                    {
                        Console.WriteLine("Введите часы: ");
                        string hoursInput = Console.ReadLine();
                        Console.WriteLine("Введите минуты: ");
                        string minutesInput = Console.ReadLine();
                        if (int.TryParse(hoursInput, out hours) && int.TryParse(minutesInput, out minutes))
                        {
                            if (hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                            {
                                isValidTime = true;
                            }
                            else
                            {
                                Console.WriteLine("Ошибка формата.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка данных.");
                        }
                    }
                    Console.WriteLine("Введите номер строки: ");
                    string userValue = Console.ReadLine();
                    if (int.TryParse(userValue, out int valueNumber))
                    {
                        if (valueNumber < 1)
                        {
                            Console.WriteLine("Ошибка, некорректный номер.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка, введено некорректное значение. Введите число.");
                    }
                    TimeClass time = new TimeClass(hours, minutes);
                    Key userKey = new Key(name, time);
                    bool h = false;
                    tree.DeleteMinimalRight(ref tree.Root, userKey, valueNumber, ref h);
                    SuccessMessage();
                    name = null;
                    time = null;
                    userKey = null;
                    break;
                case 4:
                    Console.WriteLine("Введите фамилию: ");
                    secondNameForFio = Console.ReadLine();
                    Console.WriteLine("Введите имя: ");
                    nameForFIO = Console.ReadLine();
                    Console.WriteLine("Введите отчество: ");
                    surnameForFio = Console.ReadLine();
                    name = new FIO(secondNameForFio, nameForFIO, surnameForFio);
                    isValidTime = false;
                    hours = 0;
                    minutes = 0;
                    while (!isValidTime)
                    {
                        Console.WriteLine("Введите часы: ");
                        string hoursInput = Console.ReadLine();
                        Console.WriteLine("Введите минуты: ");
                        string minutesInput = Console.ReadLine();
                        if (int.TryParse(hoursInput, out hours) && int.TryParse(minutesInput, out minutes))
                        {
                            if (hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                            {
                                isValidTime = true;
                            }
                            else
                            {
                                Console.WriteLine("Ошибка формата.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка данных.");
                        }
                    }
                    Console.WriteLine("Введите номер строки: ");
                    userValue = Console.ReadLine();
                    if (int.TryParse(userValue, out valueNumber))
                    {
                        if (valueNumber < 1)
                        {
                            Console.WriteLine("Ошибка, некорректный номер.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка, введено некорректное значение. Введите число.");
                    }
                    time = new TimeClass(hours, minutes);
                    userKey = new Key(name, time);
                    tree.FindElement(tree.Root, name, time, valueNumber);
                        //SuccessMessage();
                    name = null;
                    time = null;
                    userKey = null;
                    break;
                case 5:
                    try
                    {
                        if (tree != null && tree.Root != null)
                            tree.PrintTree();
                        else
                        {
                            if (tree == null)
                                throw new Exception("Дерево не инициализированно.");
                            else
                                throw new Exception("Пустое дерево. Заполните дерево.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    break;
                case 6:
                    LeftRightWrite();
                    break;
                case 7:
                    MemoryFree(tree);
                    break;
                case 9:
                    return;
                default:
                    ShowError("Выбранный пункт меню отсутствует.");
                    break;
            }
            
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }
    }
    
    public static void InitializeTree()
    {
        MemoryFree(tree);
        tree = new Tree();
    }

    // Очистка памяти
    private static void FreeNode(Node node)
    {
        if (node == null)
            return;

        
        FreeNode(node.Left);
        FreeNode(node.Right);

        
        node.Left = null;
        node.Right = null;
        node.Key = null;
    }

    public static void MemoryFree(Tree tree)
    {
        if (tree != null && tree.Root != null)
        {
            FreeNode(tree.Root);
            tree.Root = null;
            SuccessMessage();
        }
    }

    public static Tree MakeTreeFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден.", filePath);
        }
        
        InitializeTree();
        
        string[] lines = File.ReadAllLines(filePath);
        int lineNumber = 1;
        
        foreach (string line in lines)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    lineNumber++;
                    continue;
                }
                
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4) //
                {
                    throw new FormatException($"Неверный формат строки {lineNumber}.");
                }
                
                string secondName = parts[0];
                string firstName = parts[1];
                string surname = parts[2];
                
                string[] timeParts = parts[3].Split(':');
                if (timeParts.Length != 2 ||
                    !int.TryParse(timeParts[0], out int hours) ||
                    !int.TryParse(timeParts[1], out int minutes) ||
                    hours < 0 || hours > 23 ||
                    minutes < 0 || minutes > 59)
                {
                    throw new FormatException($"Неверный формат времени в строке {lineNumber}: {parts[3]}");
                }
                
                FIO name = new FIO(secondName, firstName, surname);
                TimeClass time = new TimeClass(hours, minutes);
                Key key = new Key(name, time);
                
                bool heightChanged = false;
                tree.Search(key, ref tree.Root, ref heightChanged, lineNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в исходном файле: {ex.Message} {lineNumber}");
            }

            lineNumber++;
        }

        return tree;
    }

    public static void AddElement(string filePath)
    {
        bool brk = false;
        while (brk != true)
        {
            try
            {
                Console.WriteLine("Введите элемент для добавления: ");
                string line = Console.ReadLine();
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4) //
                {
                    throw new FormatException($"Неверный формат строки.");
                }
                
                string secondName = parts[0];
                string firstName = parts[1];
                string surname = parts[2];
                
                string[] timeParts = parts[3].Split(':');
                if (timeParts.Length != 2 ||
                    !int.TryParse(timeParts[0], out int hours) ||
                    !int.TryParse(timeParts[1], out int minutes) ||
                    hours < 0 || hours > 23 ||
                    minutes < 0 || minutes > 59)
                {
                    throw new FormatException($"Неверный формат времени в строке.");
                }

                File.AppendAllText(filePath, $"\n{line}");
                MakeTreeFromFile(filePath);
                brk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Введите элемент заново.");
            }
        }
    }
    
    private static void LeftRight(Node node, List<string> result)
    {
        if (node == null) return;
        
        LeftRight(node.Left, result);
        
        string values = node.Value != null && node.Value.Count > 0
            ? $" [{string.Join(", ", node.Value)}]"
            : " []";
        result.Add($"{node.Key.Name} {node.Key.Time}{values}");
        
        LeftRight(node.Right, result);
    }
        
    public static void LeftRightWrite()
    {
        List<string> result = new List<string>();
        LeftRight(tree.Root, result);
        
        string output = "[ " + string.Join(", ", result) + " ]";
        
        Console.WriteLine(output);
        string filePath = "C:\\Users\\Nikita\\Desktop\\My programms\\AVL-tree\\AVL_tree\\Output.txt";
        
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.Write("\n");
                writer.Write(output + ", ");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка записи: {ex.Message}");
        }
    }
    
    public static void Main(string[] args)
    {
        string filePath = "C:\\Users\\Nikita\\Desktop\\My programms\\AVL-tree\\AVL_tree\\Output.txt";
        File.Create(filePath).Dispose();
        Menu();
        return;
    }
}
