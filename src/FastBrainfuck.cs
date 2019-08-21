using System;
using System.Collections.Generic;

namespace Brainfuck_Interpreter
{
    public class FastBrainfuck
    {
        private const int NUMB_PLACEHOLDER = 1337;

        public static int[] Optimize(string code)
        {
            List<int> operations = new List<int>();
            List<int> jumpOperationIndices = new List<int>();

            // Collapse operations down
            for (int i = 0; i < code.Length; i++)
            {
                switch (code[i])
                {
                    case '>':
                    case '<':
                    case '+':
                    case '-':
                        Collapse(operations, code, ref i); break;
                    case '.':
                    case ',':
                        operations.Add(code[i]); break;
                    case '[':
                    case ']':
                        operations.Add(code[i]);
                        jumpOperationIndices.Add(operations.Count - 1);
                        operations.Add(NUMB_PLACEHOLDER);
                        break;
                }
            }

            // perform searches and replace the values afer a
            // [ or ] with a pointer to the corrosponding 
            // opposite end
            for (int i = 0; i < jumpOperationIndices.Count; i++)
            {
                char oper = (char)operations[jumpOperationIndices[i]];

                if (oper == '[')
                {
                    int depth = 1;
                    int j = i + 1;
                    for (; j < jumpOperationIndices.Count; j++)
                    {
                        char nextOper = (char)operations[jumpOperationIndices[j]];
                        if (nextOper == '[') depth++;
                        if (nextOper == ']') depth--;
                        if (depth == 0)
                            break;
                    }

                    operations[jumpOperationIndices[i] + 1] = jumpOperationIndices[j];
                }
                else
                {
                    int depth = 1;
                    int j = i - 1;
                    for (; j < jumpOperationIndices.Count; j--)
                    {
                        char nextOper = (char)operations[jumpOperationIndices[j]];
                        if (nextOper == '[') depth--;
                        if (nextOper == ']') depth++;
                        if (depth == 0)
                            break;
                    }
                    operations[jumpOperationIndices[i] + 1] = jumpOperationIndices[j] + 1;
                }
            }


            return operations.ToArray();
        }

        private static void Collapse(List<int> operations, string code, ref int index)
        {
            char operation = code[index];
            operations.Add(operation);

            int i = 1;
            while (++index < code.Length && code[index] == operation)
                i++;

            --index;
            operations.Add(i);
        }

        public static void Run(int[] code)
        {
            int[] memory = new int[1 << 16];
            int pointer = 0;

            for (int i = 0; i < code.Length; i++)
            {
                switch ((char)code[i])
                {
                    case '>': pointer += code[++i]; break;
                    case '<': pointer -= code[++i]; break;
                    case '+': memory[pointer] += code[++i]; break;
                    case '-': memory[pointer] -= code[++i]; break;
                    case '.': Logger.Print((char)memory[pointer]); break;
                    case ',': memory[pointer] = int.Parse(Console.ReadLine().Trim()); break;
                    case '[': if (memory[pointer] == 0) i = code[++i]; else ++i; break;
                    case ']': if (memory[pointer] != 0) i = code[++i]; else ++i; break;
                }
            }
        }
    }
}
