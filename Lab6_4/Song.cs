using System;

namespace Lab6_4
{
    public class Song : IEquatable<Song>, IComparable<Song>
    {
        public string Name { get; set; }
        public string Artist { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, Artist);
        }


        // Хеш-код. используется в коллекциях для быстрого сравнения объектов
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Artist != null ? Artist.GetHashCode() : 0);
            }
        }

        // То же самое
        public bool Equals(Song other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Artist, other.Artist);
        }

        // То же самое
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Song)obj);
        }

        // Для метода Sort
        public int CompareTo(Song other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var artistComparison = string.Compare(Artist, other.Artist, StringComparison.Ordinal);
            if (artistComparison != 0) return artistComparison;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}