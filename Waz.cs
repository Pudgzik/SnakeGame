using System;
using System.Collections.Generic;

// Wąż: lista pikseli, cialo[0] to głowa
public class Waz
{
    public List<Pixel> cialo = new List<Pixel>();
    public string kierunek;
    public ConsoleColor kolor;
    public int doWzrostu = 0;
    public string nazwa;
    public int score = 0;
    public bool zywy = true;
    string ostatniRuch;

    public Waz(string nazwa, int x, int y, string kierunek, ConsoleColor kolor)
    {
        this.nazwa = nazwa;
        this.kierunek = kierunek;
        this.ostatniRuch = kierunek;
        this.kolor = kolor;
        int krok = (kierunek == "LEFT") ? 1 : -1;   // ogon za głową
        for (int i = 0; i < 3; i++)
        {
            cialo.Add(new Pixel { xPos = x + i * krok, yPos = y, schermKleur = kolor, karacter = "■" });
        }
    }

    public Pixel Glowa
    {
        get { return cialo[0]; }
    }

    // Issue #1: wąż nie może zawrócić o 180° (wjechałby w siebie).
    // Porównujemy z kierunkiem ostatniego RUCHU, a nie ostatniego klawisza.
    public void ZmienKierunek(string nowy)
    {
        if (nowy == "UP" && ostatniRuch == "DOWN") return;
        if (nowy == "DOWN" && ostatniRuch == "UP") return;
        if (nowy == "LEFT" && ostatniRuch == "RIGHT") return;
        if (nowy == "RIGHT" && ostatniRuch == "LEFT") return;
        kierunek = nowy;
    }

    public void Ruch()
    {
        Pixel nowaGlowa = new Pixel { xPos = Glowa.xPos, yPos = Glowa.yPos, schermKleur = kolor, karacter = "■" };
        if (kierunek == "UP") nowaGlowa.yPos--;
        if (kierunek == "DOWN") nowaGlowa.yPos++;
        if (kierunek == "LEFT") nowaGlowa.xPos--;
        if (kierunek == "RIGHT") nowaGlowa.xPos++;

        ostatniRuch = kierunek;
        cialo.Insert(0, nowaGlowa);
        if (doWzrostu > 0)
            doWzrostu--;
        else
            cialo.RemoveAt(cialo.Count - 1);
    }

    // Czy wąż zajmuje pole (x, y)? od = 1 pomija głowę
    public bool Zawiera(int x, int y, int od)
    {
        for (int i = od; i < cialo.Count; i++)
        {
            if (cialo[i].xPos == x && cialo[i].yPos == y) return true;
        }
        return false;
    }

    public void Rysuj()
    {
        Console.ForegroundColor = kolor;
        foreach (Pixel p in cialo)
        {
            if (p.xPos < 0 || p.yPos < 0 || p.xPos >= Console.BufferWidth || p.yPos >= Console.BufferHeight) continue;
            Console.SetCursorPosition(p.xPos, p.yPos);
            Console.Write(p.karacter);
        }
    }
}
