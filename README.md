# BoyerMoreeSearch

## Opis

Projekt **BoyerMoreeSearch** zawiera implementację algorytmu **Boyer–Moore** do wyszukiwania podłańcuchów w tekście w języku C#.
Został stworzony w **.NET 8** i wykorzystuje **Windows Forms** jako interfejs użytkownika.

Dodatkowo repozytorium zawiera oddzielny projekt testowy zbudowany w oparciu o **xUnit**, umożliwiający jednostkowe testowanie algorytmu.
Testy można uruchamiać bezpośrednio z wiersza poleceń (`dotnet test`).

---

## Spis treści

* [Uruchamianie aplikacji](#uruchamianie-aplikacji)
* [Uruchamianie testów](#uruchamianie-testów)
* [Przydatne komendy](#przydatne-komendy)

---

## Uruchamianie aplikacji

W folderze głównym solution:

```bash
dotnet build
dotnet run --project BezpieczenstwoDanych
```

---

## Uruchamianie testów

```bash
dotnet build
dotnet test
```

---

## Przydatne komendy

| Komenda                                     | Opis                                 |
| ------------------------------------------- | ------------------------------------ |
| `dotnet build`                              | Buduje wszystkie projekty w solution |
| `dotnet run --project BezpieczenstwoDanych` | Uruchamia aplikację WinForms         |
| `dotnet test`                               | Uruchamia testy xUnit                |
| `dotnet clean`                              | Czyści foldery `bin` i `obj`         |
| `dotnet restore`                            | Pobiera pakiety NuGet                |
| `dotnet nuget locals all --clear`           | Czyści cache pakietów NuGet          |

---
