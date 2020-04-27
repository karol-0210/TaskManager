using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    public static class ConsoleEx
    {
        
        public static void Write(string text, ConsoleColor text_color)
        {
            Console.ForegroundColor = text_color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Write(string text, ConsoleColor text_color, ConsoleColor background_color)
        {
            Console.ForegroundColor = text_color;
            Console.BackgroundColor = background_color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteLine(string text, ConsoleColor text_color)
        {
            Console.ForegroundColor = text_color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteLine(string text, ConsoleColor text_color, ConsoleColor background_color)
        {
            Console.ForegroundColor = text_color;
            Console.BackgroundColor = background_color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
