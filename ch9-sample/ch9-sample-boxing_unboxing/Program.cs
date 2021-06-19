using System;
using System.Diagnostics;
using System.Linq;

namespace ch9sample_boxing_unboxing
{
    class Program
    {
        const int tryMaxCount = 5;
        const int procLoopMaxCount = 1000000;

        static void Main(string[] args)
        {
            var resultsBoxingUnBoxing = new long[tryMaxCount];
            var resultNonBoxing = new long[tryMaxCount];

            // 処理結果が前後する可能性があるので何回か試行する
            for (int i = 0; i < tryMaxCount; i++)
            {
                // 1.boxing/unboxingが発生するパターン
                resultsBoxingUnBoxing[i] = measureProcTimeMillisecond(boxingUnboxingMethod);

                // 2.発生しないパターン
                resultNonBoxing[i] = measureProcTimeMillisecond(nonBoxingMethod);
            }

            outputResult("boxing/unboxingが発生するパターン", resultsBoxingUnBoxing);
            outputResult("発生しないパターン", resultNonBoxing);
        }

        static long measureProcTimeMillisecond(Action targetFunction)
        {
            Stopwatch sw = new();
            sw.Restart();
            targetFunction();
            sw.Stop();

            return sw.ElapsedMilliseconds;
        }

        private static void outputResult(string message, long[] results)
        {
            Console.WriteLine(message);
            foreach (var (result, index) in results.Select((v, i) => (v, i)))
            {
                Console.WriteLine($"{index}回目:{result}msec");
            }
            Console.WriteLine($"平均:{results.Average()}msec");
        }

        private static void nonBoxingMethod()
        {
            for (int i = 0; i < procLoopMaxCount; i++)
            {
                // 処理時間を計測したいだけなので戻り値は使わない
                // 引数の型:intにintを渡すのでボックス化発生しない
                _ = procWithNonBoxing(i);
            }
        }

        private static void boxingUnboxingMethod()
        {
            for (int i = 0; i < procLoopMaxCount; i++)
            {
                // 処理時間を計測したいだけなので戻り値は使わない
                // 引数の型:objectにintを渡すのでボックス化発生する
                _ = procWithBoxingUnBoxing(i);
            }
        }

        static int procWithBoxingUnBoxing(System.Object valueObject)
        {
            // ボックス化解除
            int value = (int)valueObject;
            return value * value;
        }

        static int procWithNonBoxing(int valueObject)
        {
            return valueObject * valueObject;
        }
    }
}
