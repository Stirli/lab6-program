using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab6;

namespace Lab6_3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Задача 3");
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
                        olderArrList.Add(employee);
                    }
                }
                Console.WriteLine("Вывод коллекции типа ArrayList");
                foreach (Employee employee in olderArrList)
                {
                    Console.WriteLine(employee);
                }
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
