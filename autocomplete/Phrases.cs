using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autocomplete
{
    public class Phrases : IReadOnlyList<string>
    {
        private readonly string[] verbs;
        private readonly string[] adjectives;
        private readonly string[] nouns;

        public Phrases(string[] verbs, string[] adjectives, string[] nouns)
        {
            this.verbs = verbs;
            this.adjectives = adjectives;
            this.nouns = nouns;
        }

        // Это называется вычисляемое свойство с геттером.
        public virtual int Length
        {
            get { return verbs.Length*adjectives.Length*nouns.Length; }
        }

        // Это называется индексатор c геттером. Он позволяет писать так var x = phrases[i];
        public virtual string this[int index]
        {
            get
            {
                if (index < 0) throw new IndexOutOfRangeException("index = " + index);
                var ni = index%nouns.Length;
                var ai = index/nouns.Length%adjectives.Length;
                var vi = index/(nouns.Length*adjectives.Length)%verbs.Length;
                return verbs[vi] + " " + adjectives[ai] + " " + nouns[ni];
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
                yield return this[i];
        }

        public override string ToString()
        {
            return string.Format("Size: {3}. Verbs: {0}, Adjectives: {1}, Nouns: {2}", verbs.Length, adjectives.Length,
                nouns.Length, Length);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get { return Length; } }
    }
}