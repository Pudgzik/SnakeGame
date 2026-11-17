using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static int screenwidth = 32;
    static int screenheight = 16;
    static Random randomnummer = new Random();
    static Obstakel jedzenie = new Obstakel { schermKleur = ConsoleColor.Cyan, karacter = "*" };
    static List<Waz> weze = new List<Waz>();

    static void Main()
    {
        Console.CursorVisible = false;
        Waz gracz1 = new Waz("Gracz 1", screenwidth - 8, screenheight / 2 + 3, "LEFT", ConsoleColor.Green);
        Waz gracz2 = new Waz("Gracz 2", 7, screenheight / 2 - 3, "RIGHT", ConsoleColor.Magenta);
        weze.Add(gracz1);
        weze.Add(gracz2);
        NoweJedzenie();

        while (true)
        {
            // Klawisze - KeyAvailable, żeby gra nie czekała na naciśnięcie
            // Gracz 1: strzałki, Gracz 2: W S A D
            while (Console.KeyAvailable)
            {
                ConsoleKey klawisz = Console.ReadKey(true).Key;
                switch (klawisz)
                {
                    case ConsoleKey.UpArrow: gracz1.ZmienKierunek("UP"); break;
                    case ConsoleKey.DownArrow: gracz1.ZmienKierunek("DOWN"); break;
                    case ConsoleKey.LeftArrow: gracz1.ZmienKierunek("LEFT"); break;
                    case ConsoleKey.RightArrow: gracz1.ZmienKierunek("RIGHT"); break;
                    case ConsoleKey.W: gracz2.ZmienKierunek("UP"); break;
                    case ConsoleKey.S: gracz2.ZmienKierunek("DOWN"); break;
                    case ConsoleKey.A: gracz2.ZmienKierunek("LEFT"); break;
                    case ConsoleKey.D: gracz2.ZmienKierunek("RIGHT"); break;
                    case ConsoleKey.Escape: return;
                }
            }

            foreach (Waz waz in weze)
            {
                waz.Ruch();
            }

            // Jedzenie: +1 punkt i wąż rośnie
            foreach (Waz waz in weze)
            {
                if (waz.Glowa.xPos == jedzenie.Xpos && waz.Glowa.yPos == jedzenie.Ypos)
                {
                    waz.score++;
                    waz.doWzrostu++;
                    NoweJedzenie();
                }
            }

            // Kolizja ze ścianą albo z własnym ogonem
            foreach (Waz waz in weze)
            {
                if (UderzylWSciane(waz) || waz.Zawiera(waz.Glowa.xPos, waz.Glowa.yPos, 1))
                    waz.zywy = false;
            }

            if (!gracz1.zywy || !gracz2.zywy)
            {
                GameOver();
                return;
            }

            Console.Clear();
            RysujRamke();
            Console.ForegroundColor = jedzenie.schermKleur;
            Console.SetCursorPosition(jedzenie.Xpos, jedzenie.Ypos);
            Console.Write(jedzenie.karacter);
            foreach (Waz waz in weze)
            {
                waz.Rysuj();
            }
            RysujWyniki();
            Thread.Sleep(150);
        }
    }

    // Losuje pole wewnątrz ramki, na którym nie ma żadnego węża
    static void NoweJedzenie()
    {
        bool zajete;
        do
        {
            jedzenie.Xpos = randomnummer.Next(1, screenwidth - 1);
            jedzenie.Ypos = randomnummer.Next(1, screenheight - 1);
            zajete = false;
            foreach (Waz waz in weze)
            {
                if (waz.Zawiera(jedzenie.Xpos, jedzenie.Ypos, 0)) zajete = true;
            }
        } while (zajete);
    }

    static bool UderzylWSciane(Waz waz)
    {
        return waz.Glowa.xPos == 0 || waz.Glowa.xPos == screenwidth - 1
            || waz.Glowa.yPos == 0 || waz.Glowa.yPos == screenheight - 1;
    }

    static void RysujWyniki()
    {
        Console.SetCursorPosition(0, screenheight);
        foreach (Waz waz in weze)
        {
            Console.ForegroundColor = waz.kolor;
            Console.Write(waz.nazwa + ": " + waz.score + "   ");
        }
    }

    static void GameOver()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
        Console.WriteLine("Game Over");
        int linia = screenheight / 2 + 1;
        foreach (Waz waz in weze)
        {
            Console.ForegroundColor = waz.kolor;
            Console.SetCursorPosition(screenwidth / 5, linia++);
            Console.WriteLine(waz.nazwa + ": " + waz.score + (waz.zywy ? "" : " (przegrał)"));
        }
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
