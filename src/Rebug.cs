using System;

namespace Fun
{
    /// <summary>
    /// These are purely for fun and written for fun.
    /// It's not recommended using this in actual production code.
    /// 
    /// However I am not saying you can't. :)
    /// </summary>
    static class Rebug
    {
        private static ConsoleColor[] m_ColorArray = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.DarkBlue, ConsoleColor.Magenta };
        private static ConsoleColor[] m_RainbowArray = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.DarkBlue, ConsoleColor.Magenta };

        private static int m_RainbowIndex = 0;
        public static void Unicorn(object msg) => Unicorn(msg.ToString());
        public static void Unicorn(string msg)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                m_RainbowIndex = (m_RainbowIndex + 1) % m_RainbowArray.Length;
                Console.ForegroundColor = m_RainbowArray[m_RainbowIndex];
                Console.Write(msg[i]);
            }
            Console.Write('\n');
        }

        public static void UnicornWord(object msg) => UnicornWord(msg.ToString());
        public static void UnicornWord(string msg)
        {
            msg += '\n';
            System.Text.StringBuilder b = new System.Text.StringBuilder(32);
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] == '\n' || msg[i] == ' ')
                {
                    m_RainbowIndex = (m_RainbowIndex + 1) % m_RainbowArray.Length;
                    Console.ForegroundColor = m_RainbowArray[m_RainbowIndex];
                    Console.Write(b.ToString());
                    b.Clear();
                }
                b.Append(msg[i]);
            }
        }

        public static void Barf(object msg) => Barf(msg.ToString());
        public static void Barf(string msg)
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < msg.Length; i++)
            {
                Console.ForegroundColor = colors[random.Next() % colors.Length];
                do
                {
                    Console.BackgroundColor = colors[random.Next() % colors.Length];
                } while (Console.BackgroundColor == Console.ForegroundColor);
                Console.Write(msg[i]);
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void Rev(object msg) => Rev(msg.ToString());
        public static void Rev(string msg)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                Console.Write(msg[msg.Length - (i + 1)]);
            }
        }

        public static void Leet(object msg) => Leet(msg.ToString());
        public static void Leet(string msg)
        {
            Console.WriteLine(
             msg.Replace("A", "@")
                .Replace("a", "@")
                .Replace('E', '3')
                .Replace('e', '3')
                .Replace('t', '7')
                .Replace('T', '7')
                .Replace("M", "[T]")
                .Replace('o', '0')
                .Replace('O', '0')
                .Replace('l', '1')
                .Replace('L', '1'));
        }

        public static void CharBow(string message)
        {
            for (int i = 0; i < message.Length; i++)
                CharBow(message[i]);
        }

        public static void CharBow(char character)
        {
            ConsoleColor color = Console.BackgroundColor;
            Console.BackgroundColor = m_RainbowArray[character % m_RainbowArray.Length];

            if (character == ' ')
                Console.BackgroundColor = ConsoleColor.Black;

            Console.Write(character);
            Console.BackgroundColor = color;
        }
    }
}