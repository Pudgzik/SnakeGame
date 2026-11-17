using System;
using System.Threading;

class Program
{
    static int screenwidth = 32;
    static int screenheight = 16;
    static Random randomnummer = new Random();
    static Obstakel jedzenie = new Obstakel { schermKleur = ConsoleColor.Cyan, karacter = "*" };
    static int score = 0;

    static void Main()
    {
        Console.CursorVisible = false;
        Waz gracz1 = new Waz(screenwidth / 2, screenheight / 2, "RIGHT", ConsoleColor.Green);
        NoweJedzenie(gracz1);

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

            // Jedzenie: +1 punkt i wąż rośnie
            if (gracz1.Glowa.xPos == jedzenie.Xpos && gracz1.Glowa.yPos == jedzenie.Ypos)
            {
                score++;
                gracz1.doWzrostu++;
                NoweJedzenie(gracz1);
            }

            // Kolizja ze ścianą albo z własnym ogonem
            if (UderzylWSciane(gracz1) || gracz1.Zawiera(gracz1.Glowa.xPos, gracz1.Glowa.yPos, 1))
            {
                GameOver();
                return;
            }

            Console.Clear();
            RysujRamke();
            Console.ForegroundColor = jedzenie.schermKleur;
            Console.SetCursorPosition(jedzenie.Xpos, jedzenie.Ypos);
            Console.Write(jedzenie.karacter);
            gracz1.Rysuj();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, screenheight);
            Console.Write("Score: " + score);
            Thread.Sleep(150);
        }
    }

    // Losuje pole wewnątrz ramki, na którym nie ma węża
    static void NoweJedzenie(Waz waz)
    {
        do
        {
            jedzenie.Xpos = randomnummer.Next(1, screenwidth - 1);
            jedzenie.Ypos = randomnummer.Next(1, screenheight - 1);
        } while (waz.Zawiera(jedzenie.Xpos, jedzenie.Ypos, 0));
    }

    static bool UderzylWSciane(Waz waz)
    {
        return waz.Glowa.xPos == 0 || waz.Glowa.xPos == screenwidth - 1
            || waz.Glowa.yPos == 0 || waz.Glowa.yPos == screenheight - 1;
    }

    static void GameOver()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
        Console.WriteLine("Game Over");
        Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
        Console.WriteLine("Twój wynik: " + score);
        Console.ResetColor();
        Console.CursorVisible = true;
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
