using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            hashCode = unchecked (bytes.Aggregate(0, (current, b) => current * 1023 + b));
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
            var thisEnumerator = GetEnumerator();
            var objEnumerator = enumerableObj.GetEnumerator();
            bool thisHasNext, objHasNext;

            while ((thisHasNext = thisEnumerator.MoveNext()) & (objHasNext = objEnumerator.MoveNext()))
                if (thisEnumerator.Current != objEnumerator.Current)
                    return false;

            thisEnumerator.Dispose();
            objEnumerator.Dispose();

            return !thisHasNext && !objHasNext;
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
            return "[" + string.Join(", ", bytes) + "]";
        }
    }
}