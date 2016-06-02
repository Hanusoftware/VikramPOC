using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VikramWebRole.Models
{
    public class RedisCacheModel
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public bool Equals(RedisCacheModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Age == other.Age;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RedisCacheModel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Age;
            }
        }

        public static bool operator ==(RedisCacheModel left, RedisCacheModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RedisCacheModel left, RedisCacheModel right)
        {
            return !Equals(left, right);
        }
    }
}