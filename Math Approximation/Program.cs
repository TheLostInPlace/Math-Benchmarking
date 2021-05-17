using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Math_Approximation
{
    class Program
    {

        public static float convertRad(float angle)
        {
            return (float)(angle * Math.PI / 180);
        }

        private static long nanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }

        public static Dictionary<string, long> startBenchmark(int maxLoops, int maxTime)
        {

            long startTime = DateTime.Now.Millisecond;

            long taylorTime = 0;
            long taylorModifiedTime = 0;
            long csharpTime = 0;
            long csineTime = 0;

            TaylorMath taylorMath = new TaylorMath();

            ModifiedTaylors modtaylorMath = new ModifiedTaylors();

            CSine csineMath = new CSine();

            Task calculateTaylor = Task.Factory.StartNew(() =>
            {
                taylorMath.sin(0);
                taylorMath.cos(0);

                for (int i = 0; i < maxLoops; i++)
                {
                    float angle = (float)convertRad(i % 360);
                    long time;

                    time = nanoTime();

                    taylorMath.sin(angle);
                    taylorMath.cos(angle);

                    taylorTime += nanoTime() - time;

                    if (DateTime.Now.Millisecond - startTime > maxTime)
                    {
                        break;
                    }

                }

            }, new CancellationToken(false), TaskCreationOptions.LongRunning, TaskScheduler.Default);

            Task calculateModifiedTaylor = Task.Factory.StartNew(() =>
            {

                modtaylorMath.Sin(0);
                modtaylorMath.Cos(0);

                for (int i = 0; i < maxLoops; i++)
                {
                    float angle = (float)convertRad(i % 360);
                    long time;

                    time = nanoTime();

                    modtaylorMath.Sin(angle);
                    modtaylorMath.Cos(angle);

                    taylorModifiedTime += nanoTime() - time;

                    if (DateTime.Now.Millisecond - startTime > maxTime)
                    {
                        break;
                    }

                }

            }, new CancellationToken(false), TaskCreationOptions.LongRunning, TaskScheduler.Default);

            Task calculateCSine = Task.Factory.StartNew(() =>
            {

                csineMath.sin(0);
                csineMath.cos(0);

                for (int i = 0; i < maxLoops; i++)
                {
                    float angle = (float)convertRad(i % 360);
                    long time;

                    time = nanoTime();

                    csineMath.sin(angle);
                    csineMath.cos(angle);

                    csineTime += nanoTime() - time;

                    if (DateTime.Now.Millisecond - startTime > maxTime)
                    {
                        break;
                    }

                }

            }, new CancellationToken(false), TaskCreationOptions.LongRunning, TaskScheduler.Default);

            Task calculateCSharp = Task.Factory.StartNew(() =>
            {
                Math.Sin(0);
                Math.Cos(0);

                for (int i = 0; i < maxLoops; i++)
                {
                    float angle = (float)convertRad(i % 360);
                    long time;

                    time = nanoTime();

                    Math.Sin(angle);
                    Math.Cos(angle);

                    csharpTime += nanoTime() - time;

                    if (DateTime.Now.Millisecond - startTime > maxTime)
                    {
                        break;
                    }

                }

            }, new CancellationToken(false), TaskCreationOptions.LongRunning, TaskScheduler.Default);


            Task.WaitAll(new[] { calculateTaylor, calculateModifiedTaylor, calculateCSine, calculateCSharp});

            Dictionary<string, long> results = new Dictionary<string, long>();

            results.Add("Taylors", taylorTime);
            results.Add("Modified Taylors", taylorModifiedTime);
            results.Add("CSine", csineTime);
            results.Add("CSharp", csharpTime);

            return results;
        }

        public static string showResult(Dictionary<string, long> result)
        {
            StringBuilder results = new StringBuilder();

            string worstTime = result.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            long max = result.Max(x => x.Value);

            long min = result.Min(x => x.Value);

            foreach (KeyValuePair<string, long> dictionary in result)
            {
                results.Append($"Algorithm {dictionary.Key}, Time {dictionary.Value} ns \n ");
            }

            results.AppendLine($"Larger time is {worstTime}, with difference of {max-min} nanoseconds or {(max-min)/1e+9} seconds");

            return results.ToString();
        }

        static void Main(string[] args)
        { 
            Console.WriteLine($"Starting benchmark: \n {showResult(startBenchmark(5_000_000, 1_000_000))}");
        }
    }
}
