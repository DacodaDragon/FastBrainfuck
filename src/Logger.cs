using System;

namespace Brainfuck_Interpreter
{
    public static class Logger
    {
        public static void Print(object message) => Print(message.ToString());
        public static void Print(string message)
        {
            if (Settings.Global.Rainbow)
                Fun.Rebug.CharBow(message);
            else Console.Write(message);
        }
    }
}
