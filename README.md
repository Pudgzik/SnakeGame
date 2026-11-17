# SnakeGame

Prosta gra Snake w konsoli dla dwóch graczy, napisana w C# (.NET 8).

## Uruchomienie

Wymagany .NET SDK 8.

```
dotnet run
```

W Visual Studio: otwórz `SnakeGame.csproj` i naciśnij F5.

## Sterowanie

| Gracz | Klawisze |
|---|---|
| Gracz 1 (zielony) | strzałki |
| Gracz 2 (fioletowy) | W, S, A, D |

`Esc` – wyjście z gry.

## Zasady

* `*` to jedzenie: +1 punkt, a wąż rośnie o jeden segment.
* Wąż przegrywa po uderzeniu w ścianę, we własny ogon albo w drugiego węża.
* Zderzenie głowami = remis.
* Wygrywa wąż, który przeżył. Wyniki widać pod planszą.

## Pliki

| Plik | Opis |
|---|---|
| `Snake.cs` | pętla gry, klawisze, jedzenie, kolizje, ekran końca |
| `Waz.cs` | klasa węża: ruch, wzrost, rysowanie |
| `Pixel.cs` | jeden segment węża |
| `Obstakel.cs` | jedzenie |

## Zasady współpracy

* Gałąź `main` zawiera działającą wersję. Nie wysyłamy zmian bezpośrednio do `main`.
* Każde zadanie robimy w osobnej gałęzi (`feature/...`, `fix/...`).
* Po skończeniu zadania otwieramy Pull Request do `main`.
* Druga osoba robi recenzję: uruchamia grę, dodaje komentarze, akceptuje (Approve).
* Po akceptacji autor scala PR i usuwa gałąź.
* Błędy i pomysły zgłaszamy jako Issues.

## Podział zadań

| Zadanie | Osoba | Gałąź |
|---|---|---|
| Szkielet projektu | Osoba 1 | `main` |
| Ruch węża i sterowanie | Osoba 1 | `feature/ruch-i-sterowanie` |
| Jedzenie, punktacja, kolizje | Osoba 2 | `feature/jedzenie-i-kolizje` |
| Drugi gracz (WSAD) | Osoba 1 | `feature/drugi-gracz` |
| Kolizje między wężami, zwycięzca | Osoba 2 | `feature/kolizje-miedzy-wezami` |
| Issue #1: zawracanie o 180° | Osoba 1 | `fix/1-zawracanie` |
| Dokumentacja | Osoba 2 | `docs/readme` |
