using System;

namespace Brainfuck_Interpreter
{
    static class BrainfuckInterpreter
    {
        public static void Run(string code)
        {
            int pointer = 0;
            byte[] memory = new byte[12000];
            int loopPointer = 0;
            int[] loops = new int[200];

            for (int i = 0; i < code.Length; i++)
            {
                char a = code[i];
                switch (code[i])
                {
                    case '>': ++pointer; break;
                    case '<': --pointer; break;
                    case '+': ++memory[pointer]; break;
                    case '-': --memory[pointer]; break;
                    case '.': Logger.Print((char)memory[pointer]); break;
                    case ',': memory[pointer] = byte.Parse(Console.ReadLine().Trim()); break;
                    case '[':
                        if (memory[pointer] == 0)
                        {
                            int depthLeft = 1;
                            while (depthLeft >= 1)
                            {
                                switch (code[++i])
                                {
                                    case '[': ++depthLeft; break;
                                    case ']': --depthLeft; break;
                                    default: continue;
                                }
                            }
                        }
                        else loops[loopPointer++] = i;
                        break;

                    case ']':
                        if (memory[pointer] == 0)
                            --loopPointer;
                        else i = loops[--loopPointer] - 1;
                        break;
                }
            }
        }
    }
}
