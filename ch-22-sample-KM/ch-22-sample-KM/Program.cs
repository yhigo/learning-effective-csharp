using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ch_22_sample_KM
{
    class Program
    {
        static void Main(string[] args)
        {
            Planet[] pArray = new Planet[]
            {
                new Planet
                {
                    Name = "earth",
                    Mass= 5
                },
                new Planet
                {
                    Name = "venus",
                    Mass = 4
                },
                new Planet
                {
                    Name = "mars",
                    Mass = 6
                }
            };

            CoVariantArray(pArray);

            //UnsafeVariantArray(pArray);

            CovariantGeneric(pArray);

            var cList = new List<CelestialBody>
            {
                new Moon
                {
                    Name = "luna",
                    Mass = 4
                },
                new Asteroid
                {
                    Name = "Hygiea",
                    Mass = 8
                },
                new Planet
                {
                    Name = "earth",
                    Mass= 5
                },
            };

            InvariantGeneric(cList);
        }

        static void CoVariantArray(CelestialBody[] baseItems)
        {
            foreach (var item in baseItems)
            {
                Console.WriteLine($"The {item.Name} mass is {item.Mass} kg");
            }
        }

        static void UnsafeVariantArray(CelestialBody[] baseItems)
        {
            // 実行時エラー
            baseItems[0] = new Asteroid
            {
                Name = "Hygiea",
                Mass = 8
            };
        }

        static void CovariantGeneric(IEnumerable<CelestialBody> baseItems)
        {

            /*
             * public interface IEnumerable<out T> : IEnumerable
             * {
             *      IEnumerator<T> GetEnumerator();
             *  }
             */

            /*
             * public interface IEnumerator<out T> : IEnumerator, IDisposable
             * {
             *      T Current { get; }
             *  }
             */

            /*
             * out修飾子を指定することで、T型は戻り値としてか使いませんよ～～～
             * とコンパイラに伝えている
             * →　共変性
             */

            foreach (var item in baseItems)
            {
                Console.WriteLine($"The {item.Name} mass is {item.Mass} kg");
            }

            // ↓これはIEnumerable<T>がインデクサを持っていないためコンパイルエラーになる
            //baseItems[0] = new Asteroid
            //{
            //    Name = "Hygiea",
            //    Mass = 8
            //};
        }

        static void InvariantGeneric(IList<CelestialBody> baseItems)
        {
            Action<IList<CelestialBody>> consoleWrite = (list) =>
            {
                Console.WriteLine("\n----------\n");

                foreach (var item in list)
                {
                    Console.WriteLine(item.Name);
                }
                
                Console.WriteLine("\n----------\n");
            };

            consoleWrite(baseItems);

            var newList = new List<CelestialBody>(baseItems);
            newList.Sort(CelestialBody.CompareByName);

            consoleWrite(newList);
        }
    }

    public abstract class CelestialBody : IComparable<CelestialBody>
    {
        public double Mass { get; set; }

        public string Name { get; set; }

        public int CompareTo([AllowNull] CelestialBody other)
        {
            return Name.CompareTo(other.Name);
        }

        public static Comparison<CelestialBody> CompareByName => (left, right) =>
        {
            return left.CompareTo(right);
        };
    }

    public class Planet : CelestialBody
    {

    }

    public class Moon : CelestialBody
    {

    }

    public class Asteroid : CelestialBody
    {

    }
}
