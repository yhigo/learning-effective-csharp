using System;
using System.Collections.Generic;
using System.IO;

namespace ch_23_sample_KM
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            int b = 10;
            int sum = Example.Add(a, b, (x, y) => x + y);
            Console.WriteLine($"AddFunc : {sum}");

            Console.WriteLine("\n---\n");

            double[] xValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double[] yValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            List<Point> values = new List<Point>(System.Linq.Enumerable.Zip(xValues, yValues, (x, y) => new Point(x, y)));
            foreach (var item in values)
            {
                // Linq標準のZip関数の結果
                Console.WriteLine($"X: {item.X} Y: {item.Y}");
            }

            Console.WriteLine("\n---\n");

            List<Point> myValues = new List<Point>(Example.Zip(xValues, yValues, (x, y) => new Point(x, y)));
            foreach (var item in myValues)
            {
                // 自前Zip関数の結果
                Console.WriteLine($"X: {item.X} Y: {item.Y}");
            }

            Console.WriteLine("\n---\n");

            var readValue = new InputCollection<Point>((inputStream) => new Point(inputStream));
            using (var reader = new StreamReader("TextFile1.txt"))
            {
                while (!reader.EndOfStream)
                {
                    readValue.ReadFromStream(reader);
                }
            }
            foreach (var item in readValue.Values)
            {
                Console.WriteLine($"X: {item.X} Y: {item.Y}");
            }

        }
    }

    public delegate TOutput Func<T1, T2, TOutput>(T1 arg1, T2 arg2);

    public static class Example
    {
        public static T Add<T>(T left, T right, Func<T, T, T> AddFunc)
        {
            return AddFunc(left, right);
        }

        public static IEnumerable<TOutput> Zip<T1, T2, TOutput>(IEnumerable<T1> left, IEnumerable<T2> right, Func<T1, T2, TOutput> generator)
        {
            IEnumerator<T1> leftSeq = left.GetEnumerator();
            IEnumerator<T2> rightSeq = right.GetEnumerator();
            while (leftSeq.MoveNext() && rightSeq.MoveNext())
            {
                yield return generator(leftSeq.Current, rightSeq.Current);
            }
            leftSeq.Dispose();
            rightSeq.Dispose();
        }
    }

    public class Point
    {
        public double X { get; }
        public double Y { get; }
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point(TextReader reader)
        {
            string line = reader.ReadLine();
            string[] fields = line.Split(',');
            if (fields.Length != 2)
            {
                throw new InvalidOperationException("入力形式が不正です");
            }
            double value;
            if (!double.TryParse(fields[0], out value))
            {
                throw new InvalidOperationException("Xの値を解析できません");
            }
            else
            {
                X = value;
            }

            if (!double.TryParse(fields[1], out value))
            {
                throw new InvalidOperationException("Yの値を解析できません");
            }
            else
            {
                Y = value;
            }
        }
    }

    public delegate T CreateFromStream<T>(TextReader reader);

    public class InputCollection<T>
    {
        private List<T> thingsRead = new List<T>();
        private readonly CreateFromStream<T> readFunc;

        public InputCollection(CreateFromStream<T> readFunc)
        {
            this.readFunc = readFunc;
        }

        public void ReadFromStream(TextReader reader)
        {
            thingsRead.Add(readFunc(reader));
        }

        public IEnumerable<T> Values
        {
            get { return thingsRead; }
        }
    }
}
