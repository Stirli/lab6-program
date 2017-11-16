using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab6
{
    class Program
    {


        private static void Task1()
        {
            Console.WriteLine("Задача 1");
            Stack<char> charStack = new Stack<char>();
            // Разбираем текстовый файл на символы.
            foreach (string ch in File.ReadLines("textfile.txt"))
                foreach (char c in ch)
                    charStack.Push(c);
            // Создаем стек
            Console.WriteLine("Содержимое файла в обратном порядке:");
            // Создаем и выводим на экран строку на основе стека символов. (читая стек, метод ToArray() вернет символы в обратном порядке)
            Console.WriteLine(new string(charStack.ToArray()));
            Console.WriteLine("Содерживоме файла в обратном порядке (только гласные):");
            // Тоже самое, но предварительно фильтруем негласные символы
            List<char> list = new List<char>();
            foreach (char ch in charStack)
            {
                if ("ОИАЫЮЯЭЁУЕоиаыюяэёуеAaEeIiOoUuYy".Contains(ch.ToString()))
                    list.Add(ch);
            }
            Console.WriteLine(new string(list.ToArray()));
            Console.ReadKey(true);
        }

        private static void Task23()
        {
            Console.WriteLine("Задачи 2 и 3");
            Queue<Employee> older = new Queue<Employee>();
            ArrayList olderArrList = new ArrayList();
            
            // Перебираем строки
            foreach (string line in File.ReadLines("employes.txt"))
            {
                // Создаем Employee из строки
                Employee employee = Employee.Parse(line);
                if (employee.Age < 30)
                {
                    // Сначала 
                    Console.WriteLine(employee);
                }
                else
                {
                    older.Enqueue(employee);
                    olderArrList.Add(employee);
                }
            }

            Console.WriteLine("Вывод коллекции типа Queue<T>");
            foreach (Employee employee in older)
            {
                Console.WriteLine(employee);
            }

            Console.WriteLine("Вывод коллекции типа ArrayList");
            foreach (Employee employee in olderArrList)
            {
                Console.WriteLine(employee);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Task1();
                Console.ReadKey(true);
                Task23();
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey(true);
        }
    }
}
