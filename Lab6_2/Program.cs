using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab6_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Задача 2");
                Queue<Employee> older = new Queue<Employee>();

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
                    }
                }

                Console.WriteLine("Вывод коллекции типа Queue<T>");
                foreach (Employee employee in older)
                    Console.WriteLine(employee);
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