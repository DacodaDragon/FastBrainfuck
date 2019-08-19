using System;
using System.Globalization;
using System.Threading;
using System.Diagnostics;

namespace Brainfuck_Interpreter
{
    /// <summary>
    /// Entry point
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Settings.Global.SetFromArgs(args);

            string fileContents = Settings.Global.FileContents;
            
            if (Settings.Global.Race)
            {
                Race(fileContents);
                Environment.Exit(0);
            }

            if (Settings.Global.Optimized)
            {
                TimeSpan span = RunTimed(() => RunOptimized(fileContents));
                if (Settings.Global.PrintTime)
                    Console.WriteLine(span);
            }
            else
            {
                TimeSpan span = RunTimed(() => RunUnoptimized(fileContents));
                if (Settings.Global.PrintTime)
                    Console.WriteLine(span);
            }
        }

        /// <summary>
        /// Runs a normal brainfuck interpreter
        /// </summary>
        private static void RunUnoptimized(string fileContents)
            => BrainfuckInterpreter.Run(fileContents);

        /// <summary>
        /// Optimizes the code (source to source compiler) and interprets it
        /// </summary>
        private static void RunOptimized(string fileContents)
            => FastBrainfuck.Run(FastBrainfuck.Optimize(fileContents));

        /// <summary>
        /// Races between 
        /// </summary>
        /// <param name="fileContents"></param>
        private static void Race(string fileContents)
        {
            Console.WriteLine("Unoptimized:");
            TimeSpan unoptimizedTime = RunTimed(() => RunUnoptimized(fileContents));

            Console.WriteLine();
            Console.WriteLine("Optimized:");
            TimeSpan optimizedTime = RunTimed(() => RunOptimized(fileContents));

            Console.WriteLine();
            if (Settings.Global.PrintTime)
            {
                Console.WriteLine($"Unoptimized Time: {unoptimizedTime}");
                Console.WriteLine($"Optimized Time:   {optimizedTime}");
                Console.WriteLine($"Time difference:  {optimizedTime - unoptimizedTime}");
            }
        }

        private static TimeSpan RunTimed(Action timed)
        {
            Stopwatch Stopwatch = Stopwatch.StartNew();
            timed?.Invoke();
            Stopwatch.Stop();
            return Stopwatch.Elapsed;
        }
    }
}
