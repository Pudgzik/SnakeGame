using System;

class Program
{
    static int screenwidth = 32;
    static int screenheight = 16;

    static void Main()
    {
        Console.Clear();
        RysujRamke();
        Console.SetCursorPosition(0, screenheight);
        Console.WriteLine("Projekt SnakeGame - start");
    }

    static void RysujRamke()
    {
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < screenwidth; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("■");
            Console.SetCursorPosition(i, screenheight - 1);
            Console.Write("■");
        }
        for (int i = 0; i < screenheight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("■");
            Console.SetCursorPosition(screenwidth - 1, i);
            Console.Write("■");
        }
    }
}
