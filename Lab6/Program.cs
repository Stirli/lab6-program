using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
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
