using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lab6_4
{
    class Program
    {

        private static int ReadInt(string message = "Введите число", int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    Console.WriteLine(" Или Ctrl+Z для отмены");
                    string readLine = Console.ReadLine();
                    var readInt = int.Parse(readLine);
                    if (readInt < min || readInt > max)
                    {
                        throw new OverflowException();
                    }

                    return readInt;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введенная строка не является целым числом.\n Попробуйте еще раз");
                }
                catch (ArgumentNullException)
                {
                    // если ввести ctr+z, а потом enter, то Console.ReadLine вернет null
                    throw new ApplicationException("Ввод был отменен");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Число слшком большое или слишком маленькое");
                    Console.WriteLine("Допустимые значения: {0} - {1}", min, max);
                    Console.WriteLine("Попробуйте еще раз");
                }
            }
        }

        private static Song ReadSong()
        {
            return new Song { Name = ReadString("Название песни"), Artist = ReadString("Исполнитель") };
        }

        private static string ReadString(string v)
        {
            Console.Write(v);
            Console.WriteLine(" Или CTRL+Z для отмены");
            string val = Console.ReadLine();
            if (val == null)
                throw new ApplicationException("Ввод был отменен.");
            return val;
        }

        static void Print<T>(IEnumerable<T> enumearble)
        {
            Console.WriteLine("---");
            foreach (T obj in enumearble)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("---");
        }

        // Выводит список на экран, индексируя, и возвращает введенный пользователем индекс
        static T SelectItem<T>(T[] items, string message)
        {
            Console.WriteLine(message);
            // Счетчик
            int i = 0;
            foreach (T item in items)
            {
                // Сначала выводим индекс
                Console.Write("{0,3}: ", i++);
                // Выводим сам элемент
                Console.WriteLine(item);
            }
            return items[ReadInt("Введите индекс:", 0, i)];
        }

        private static string SelDisk(Catalog catalog)
        {
            Console.WriteLine("Выбор диска:");
            string searchDisk = ReadString("Введите регулярное выражение для поиска дисков");
            Regex reg = new Regex(searchDisk, RegexOptions.IgnoreCase);
            // Перечисляем коллекцию дисков в массив
            List<string> diskList = new List<string>();
            foreach (string disk in catalog.EnumerateDisks())
            {
                if (reg.IsMatch(disk))
                {
                    diskList.Add(disk);
                }
            }
            diskList.Sort();

            if (diskList.Count == 0)
            {
                throw new ApplicationException("Элементов больше не найдено.\nВыход\n");
            }

            // SelectItem выводит на консоль коллекцию, и вовзращает выбранный пользователем диск
            string selDisk = SelectItem(diskList.ToArray(), "Выберите диск");
            Console.WriteLine("Выбран диск " + selDisk);
            return selDisk;
        }

        static void Main(string[] args)
        {

            try
            {
                // Создаем пустой каталог
                Catalog catalog = new Catalog();
                int songsPerDisk = 10;
                // Создаем диски
                for (int i = 0; i < 100; i++)
                {
                    catalog.AddDisk("Disk " + i);
                }

                // Создаем песни и добавляем их на диски
                // Создаем массив, т.к. если оставить IEnumerable, то получение элемента по индексу каждый раз будет вызывать перечисление
                {
                    List<string> diskList = catalog.EnumerateDisks();
                    diskList.Sort();
                    string[] disks = diskList.ToArray();
                    for (int i = 0; i < disks.Length * songsPerDisk; i++)
                    {
                        Song song = new Song { Artist = "Artist " + i % 10, Name = "Song " + i };
                        catalog.AddSong(song, disks[i / songsPerDisk]);
                    }
                }
                while (true)
                {
                    // Меню
                    string select = SelectItem(new[]
                {
                        "Добавить диск",
                        "Удалить диск",
                        "Добавить песню",
                        "Удалить песню",
                        "Список дисков",
                        "Содержимое диска",
                        "Содержимое каталога",
                        "Поиск по исполнителю"
                    }, "Главное меню");
                    try
                    {
                        switch (select)
                        {
                            case "Добавить диск":
                                {
                                    Console.WriteLine("Добавление дисков");
                                    // Бесконечно создаем и добавляем в каталог диски, покуда не будет брошен ApplicationException (пользователь введет ctrl+z
                                    while (true)
                                    {
                                        catalog.AddDisk(ReadString("Введите название диска"));
                                        Console.WriteLine();
                                    }
                                }
                            case "Удалить диск":
                                {
                                    while (true)
                                    {
                                        Console.WriteLine("Удаление дисков");
                                        string searchDisk = ReadString("Введите регулярное выражение для поиска");
                                        Regex reg = new Regex(searchDisk, RegexOptions.IgnoreCase);
                                        // Перечисляем коллекцию дисков в массив
                                        try
                                        {
                                            while (true)
                                            {
                                                List<string> diskList = new List<string>();
                                                foreach (string disk in catalog.EnumerateDisks())
                                                {
                                                    if (reg.IsMatch(disk))
                                                        diskList.Add(disk);
                                                }
                                                diskList.Sort();
                                                string[] disks = diskList.ToArray();
                                                if (disks.Length == 0)
                                                {
                                                    Console.WriteLine("Элементов больше не найдено.\nВыход\n");
                                                    break;
                                                }

                                                // SelectItem выводит на консоль коллекцию, и вовзращает выбранный пользователем диск
                                                string selDisk = SelectItem(disks, "Выберите диск");
                                                catalog.RemoveDisk(selDisk);
                                                Console.WriteLine();
                                            }
                                        }
                                        catch (ApplicationException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        Console.WriteLine();
                                    }
                                }
                            case "Добавить песню":
                                {
                                    // Бесконечно создаем и добавляем в каталог песни, покуда не будет брошен ApplicationException (пользователь введет ctrl+z
                                    while (true)
                                    {
                                        Console.WriteLine("Добавление песен");
                                        string selDisk = SelDisk(catalog);
                                        try
                                        {
                                            while (true)
                                            {
                                                // Читаем песню из консоли
                                                Song song1 = ReadSong();
                                                // Добавляем на выбранный диск песню
                                                catalog.AddSong(song1, selDisk);
                                                Console.WriteLine();
                                            }
                                        }
                                        catch (ApplicationException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        Console.WriteLine();
                                    }
                                }
                            case "Удалить песню":
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("Удаление песни. Для выхода в главное меню отмените ввод.");
                                        string selDisk = SelDisk(catalog);
                                        Song[] diskSongs = catalog.EnumerateDisk(selDisk).ToArray();
                                        Song songToRemove = SelectItem(diskSongs, "Выберите песню");

                                        catalog.RemoveSong(songToRemove, selDisk);
                                    }
                                    catch (ArgumentException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    Console.WriteLine();
                                }
                            case "Содержимое каталога":
                                {
                                    List<string> sortedDisks = catalog.EnumerateDisks();
                                    sortedDisks.Sort();
                                    foreach (string disk in sortedDisks)
                                    {
                                        Console.WriteLine(disk);
                                        IEnumerable<Song> songs = catalog.EnumerateDisk(disk);
                                        foreach (Song song in songs)
                                        {
                                            Console.WriteLine("\t" + song);
                                        }
                                        Console.WriteLine();
                                    }
                                }
                                break;
                            case "Содержимое диска":
                                {
                                    string selDisk = SelDisk(catalog);
                                    Console.WriteLine(selDisk);
                                    Print(catalog.EnumerateDisk(selDisk));
                                    Console.WriteLine();
                                }

                                break;
                            case "Список дисков":
                                Print(catalog.EnumerateDisks());
                                Console.WriteLine();
                                break;
                            case "Поиск по исполнителю":
                                Console.WriteLine("Поиск по исполнителю");
                                List<Song> list = catalog.FindSongs(ReadString("Имя исполнителя (регулярное выражение)"));
                                list.Sort();
                                Print(list);
                                break;

                        }

                        Console.WriteLine("Нажмите любую клавишу");
                        Console.ReadKey(true);
                    }
                    catch (ApplicationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
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

            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey(true);
        }
    }
}
