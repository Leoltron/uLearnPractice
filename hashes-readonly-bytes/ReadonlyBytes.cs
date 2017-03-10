using System;
using System.Collections.Generic;
using System.Linq;

namespace hashes
{

    public class ReadonlyBytes : IEnumerable<byte>
    {
        private readonly byte[] bytes;
        public int Count => bytes.Length;
        public byte this[int index] => bytes[index];
        private readonly int hashCode;

        public ReadonlyBytes(params byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException();
            this.bytes = bytes;
            hashCode = CalculateHashCode(bytes);
        }

        private static int CalculateHashCode(byte[] bytes)
        {
            return unchecked (bytes.Aggregate(0, (current, b) => current * 1023 + b));
        }

        public IEnumerator<byte> GetEnumerator()
        {
            return ((IEnumerable<byte>) bytes).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(this,obj))
                return true;

            if (obj is ReadonlyBytes)
                return Equals((ReadonlyBytes) obj);

            var enumerableObj = obj as IEnumerable<byte>;
            if (enumerableObj == null) return false;

            var i = 0;
            foreach (var o in enumerableObj)
            {
                if (i >= bytes.Length)
                    return false;
                if (bytes[i] != o)
                    return false;
                i++;
            }
            return i == bytes.Length;
        }

        public bool Equals(ReadonlyBytes other)
        {
            if (bytes.Length != other.bytes.Length || GetHashCode() != other.GetHashCode()) return false;
            for(var i = 0; i < bytes.Length; i++)
                if(this[i] != other[i])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", bytes)}]";
        }
    }
} 