using System;
using System.Collections;
using System.Collections.Generic;

namespace ch_19_KM
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] peopleArray = new Person[3]
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon"),
            };

            string[] strArray = new string[3]
            {
                "a",
                "b",
                "c"
            };

            string moji = "あいうえお";  // ReverseStringEnumeratorで処理される

            var collection = new ReverseEnumerable<char>(moji);
            foreach (var item in collection)
            {
                //Console.WriteLine(item.firstName + " " + item.lastName);

                Console.WriteLine(item);
            }
        }
    }

    public class Person
    {
        public Person(string fName, string lName)
        {
            this.firstName = fName;
            this.lastName = lName;
        }

        public string firstName;
        public string lastName;
    }

    public sealed class ReverseEnumerable<T> : IEnumerable<T>
    {
        private class ReverseEnumerator : IEnumerator<T>
        {

            int currentIndex;
            IList<T> collection;

            public ReverseEnumerator(IList<T> srcCollection)
            {
                collection = srcCollection;
                currentIndex = collection.Count;
            }

            public T Current
            {
                get
                {
                    return collection[currentIndex];
                }
            }

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return --currentIndex >= 0;
            }

            public void Reset()
            {
                currentIndex = collection.Count;
            }
        }

        // 改善4
        private sealed class ReverseStringEnumerator : IEnumerator<char>
        {
            private string sourceSequence;
            private int currentIndex;

            public ReverseStringEnumerator(string source)
            {
                sourceSequence = source;
                currentIndex = source.Length;
            }

            public char Current
            {
                get
                {
                    return sourceSequence[currentIndex];
                }
            }

            object IEnumerator.Current => sourceSequence[currentIndex];

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return --currentIndex >= 0;
            }

            public void Reset()
            {
                currentIndex = sourceSequence.Length;
            }
        }

        IEnumerable<T> sourceSequence;
        IList<T> originalSequence;

        public ReverseEnumerable(IEnumerable<T> sequence)
        {
            sourceSequence = sequence;

            // 改善1
            originalSequence = sequence as IList<T>;
        }


        // 改善2
        public ReverseEnumerable(IList<T> sequence)
        {
            sourceSequence = sequence;
            originalSequence = sequence;
        }

        public IEnumerator<T> GetEnumerator()
        {
            // 改善3
            if (sourceSequence is string)
            {
                return new ReverseStringEnumerator(sourceSequence as string) as IEnumerator<T>;
            }

            if (originalSequence == null)
            {
                if (sourceSequence is ICollection<T>)
                {
                    ICollection<T> source = sourceSequence as ICollection<T>;
                }
                else
                {
                    originalSequence = new List<T>();
                    foreach (T item in sourceSequence)
                    {
                        originalSequence.Add(item);
                    }
                }
            }

            //if(originalSequence == null)
            //{
            //    originalSequence = new List<T>();
            //    foreach (T item in sourceSequence)
            //    {
            //        originalSequence.Add(item);
            //    }
            //}

            return new ReverseEnumerator(originalSequence);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
