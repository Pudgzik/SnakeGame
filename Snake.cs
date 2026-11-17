using System;
using System.Threading;

class Program
{
    static int screenwidth = 32;
    static int screenheight = 16;

    static void Main()
    {
        Console.CursorVisible = false;
        Waz gracz1 = new Waz(screenwidth / 2, screenheight / 2, "RIGHT", ConsoleColor.Green);

        while (true)
        {
            // Klawisze - KeyAvailable, żeby gra nie czekała na naciśnięcie
            while (Console.KeyAvailable)
            {
                ConsoleKey klawisz = Console.ReadKey(true).Key;
                switch (klawisz)
                {
                    case ConsoleKey.UpArrow: gracz1.ZmienKierunek("UP"); break;
                    case ConsoleKey.DownArrow: gracz1.ZmienKierunek("DOWN"); break;
                    case ConsoleKey.LeftArrow: gracz1.ZmienKierunek("LEFT"); break;
                    case ConsoleKey.RightArrow: gracz1.ZmienKierunek("RIGHT"); break;
                    case ConsoleKey.Escape: return;
                }
            }

            gracz1.Ruch();

            Console.Clear();
            RysujRamke();
            gracz1.Rysuj();
            Thread.Sleep(150);
        }
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
