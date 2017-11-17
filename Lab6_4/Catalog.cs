using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lab6_4
{
    public class Catalog
    {

        // Ключ - название диска. 
        private readonly Hashtable _disks;

        public Catalog()
        {
            _disks = new Hashtable();
        }
        #region Disk

        // Добавляет диск в каталог и возвращает id диска.
        // Бросает ArgumentNullException
        public void AddDisk(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя диска не может быть пустым");
            if (_disks.Contains(name))
                throw new ArgumentException("Диск уже существует");
            _disks.Add(name, new List<Song>());
        }

        // Удалить диск.
        public void RemoveDisk(string name)
        {
            _disks.Remove(name);
        }

        // Список дисков
        public List<string> EnumerateDisks()
        {
            List<string> list = new List<string>();
            foreach (string s in _disks.Keys)
                list.Add(s);
            return list;
        }

        // Список песен на диске
        public List<Song> EnumerateDisk(string disk)
        {
            // _disks[disk] вернет object, который мы приводим к List<Song>. если это значение будет null, будет брошено исключение
            List<Song> set = _disks[disk] as List<Song>;
            if (set == null) throw new ArgumentException("Диск '" + disk + "' не найден", "disk");
            return set;

        }

        #endregion


        #region Catalog

        // Добавляет песню в каталог.
        public void AddSong(Song song, string disk)
        {
            // Наличие диска в каталоге.
            if (!_disks.Contains(disk))
            {
                throw new ArgumentException("Диск не найден", "disk");
            }

            // Получаем список песен на диске.
            List<Song> diskSongs = _disks[disk] as List<Song>;

            // Если песеня уже есть на диске
            if (diskSongs.Contains(song))
            {
                throw new ArgumentException("Песня" + song + " уже добавлена на этот диск " + disk, "song");
            }
            // Добавляем песню на диск
            diskSongs.Add(song);
        }

        //todo
        // Удаляет песню из каталога.
        public void RemoveSong(Song song, string disk)
        {
            List<Song> diskSongs = _disks[disk] as List<Song>;
            diskSongs.Remove(song);
        }


        // Список песен в каталоге
        public List<Song> EnumerateSongs()
        {
            List<Song> list = new List<Song>();
            foreach (List<Song> songs in _disks.Values)
                foreach (Song song in songs)
                {
                    list.Add(song);
                }

            return list;
        }

        // Поиск песен по исполнителю
        public List<Song> FindSongs(string artistName)
        {
            Regex reg = new Regex(artistName);
            List<Song> list = new List<Song>();
            foreach (List<Song> songs in _disks.Values)
                foreach (Song song in songs)
                {
                    if (reg.IsMatch(song.Artist))
                    {
                        list.Add(song);
                    }
                }
            return list;
        }
        #endregion
    }
}
