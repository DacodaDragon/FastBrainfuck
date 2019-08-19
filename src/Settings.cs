using Brainfuck_Interpreter.Properties;
using System;
using System.IO;

namespace Brainfuck_Interpreter
{
    class Settings
    {
        private static Lazy<Settings> _Global = new Lazy<Settings>();
        public static Settings Global => _Global.Value;

        public bool FilepathProvided { get; private set; } = false;
        public bool Rainbow { get; private set; } = false;
        public bool Optimized { get; private set; } = true;
        public bool PrintTime { get; private set; } = false;
        public bool Race { get; private set; } = false;
        public string FilePath { get; private set; } = "Not Provided";

        public string FileContents {
            get {
                if (!FilepathProvided)
                {
                    Console.WriteLine("No filepath provided, using mandelbrot instead.");
                    Console.WriteLine("If you intend to run your own file, use:");
                    Console.WriteLine("-fn \"filepath\"");
                    Console.WriteLine();

                    return Resources.Mandelbrot;
                }
                else
                {
                    return File.ReadAllText(FilePath);
                }
            }
        }

        public void SetFromArgs(params string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "?" || args[i] == "-h" || args[i] == "help")
                {
                    Console.WriteLine("-fn [filepath] \tfile containing brainfuck code to run");
                    Console.WriteLine("-? \tdisplays help");
                    Console.WriteLine("-r \tstarts a race between unoptimized and fast brainfuck");
                    Console.WriteLine("-rainbow \tmakes the output pretty");
                    Console.WriteLine("-t \tprints out the time it took to run the program");
                    Environment.Exit(0);
                }


                if (args[i] == "-r" || args[i] == "-race")
                    Race = true;

                if (args[i] == "-unoptimized" || args[i] == "-uo")
                    Optimized = false;

                if (args[i] == "-rainbow")
                    Rainbow = true;

                if (args[i] == "-t")
                    PrintTime = true;

                if (args[i] == "-fn")
                {
                    string file = args[++i];
                    FilepathProvided = true;
                }
            }
        }
    }
}
