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
                        Collapse(operations, code, ref i); continue;
                    case '.':
                    case ',':
                        operations.Add(code[i]); break;
                    case '[':
                        if (CheckForHopping(operations, code, ref i))
                            continue;
                        if (CheckForMove(operations, code, ref i))
                            continue;
                        if (CheckForEmpty(operations, code, ref i))
                            continue;
                        goto case ']';
                    case ']':
                        operations.Add(code[i]);
                        jumpOperationIndices.Add(operations.Count - 1);
                        operations.Add(NUMB_PLACEHOLDER);
                        continue;
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

        private static bool CheckForEmpty(List<int> operations, string code, ref int index)
        {
            if (code[index] != '[')
                return false;

            if (code[index + 1] != '-')

                return false;
            if (code[index + 2] != ']')
                return false;

            index += 2;
            operations.Add('e');
            return true;
        }


        // Finds patterns of [->>+<<] and turns them into a M<value> statement
        private static bool CheckForMove(List<int> operations, string code, ref int index)
        {
            int i = index;
            if (code[i] != '[')
                return false;

            if (code[++i] != '-')
                return false;

            if (code[++i] != '<' && code[i] != '>')
                return false;

            char moveDirection = code[i];
            int moveCount = Count(moveDirection, code, ref i);


            if (moveCount > 20 || code[i] != '+')
                return false;

            if (code[++i] != (moveDirection == '<' ? '>' : '<'))
                return false;

            int moveBackCount = Count(moveDirection == '<' ? '>' : '<', code, ref i);

            if (code[i] != ']' || moveCount != moveBackCount)
                return false;

            operations.Add('m');
            operations.Add(moveDirection == '<' ? -moveCount : moveCount);
            index = i;
            return true;
        }

        private static int Count(char character, string code, ref int index)
        {
            int i = index;
            while (code[++i] == character) { }
            int count = (i - index);
            index = i;
            return count;
        }

        private static bool CheckForHopping(List<int> operations, string code, ref int index)
        {
            int i = index;
            if (code[i] != '[')
                return false;

            if (code[++i] != '<' && code[i] != '>')
                return false;

            char jumpDirection = code[i];
            while (code[++i] == jumpDirection) { };
            if (code[i] == ']')
            {
                operations.Add('j');
                int JumpAmount = (i - index) - 1;
                operations.Add(jumpDirection == '<' ? -JumpAmount : JumpAmount);
                index = i;
                return true;
            }
            return false;
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
                    case '>': pointer += code[++i]; continue;
                    case '<': pointer -= code[++i]; continue;
                    case '+': memory[pointer] += code[++i]; continue;
                    case '-': memory[pointer] -= code[++i]; continue;
                    case '.': Logger.Print((char)memory[pointer]); continue;
                    case ',': memory[pointer] = int.Parse(Console.ReadLine().Trim()); continue;
                    case '[': if (memory[pointer] == 0) i = code[++i]; else ++i; continue;
                    case ']': if (memory[pointer] != 0) i = code[++i]; else ++i; continue;
                    case 'j': while (memory[pointer] != 0) pointer += code[i + 1]; ++i; continue;
                    case 'm': memory[pointer + code[++i]] += memory[pointer]; memory[pointer] = 0; continue;
                    case 'e': memory[pointer] = 0; continue;
                }
            }
        }
    }
}
