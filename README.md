# BoyerMoreeSearch

## Opis

Projekt **BoyerMooreSearch** zawiera implementację algorytmu **Boyer–Moore** do wyszukiwania podłańcuchów w tekście. Aplikacja umożliwia wizualizację rozkładu liter w tekście w postaci wykresu słupkowego, pomiar czasu wykonania algorytmu w sposób bezpośredni i względny względem obliczenia dużej silni oraz monitorowanie przydzielonej pamięci podczas wyszukiwania. Całość została napisana w języku **C#**, wykorzystując **.NET 8** oraz **Windows Forms** jako interfejs użytkownika.

Repozytorium zawiera również oddzielny projekt testowy oparty na **xUnit**, umożliwiający jednostkowe testowanie algorytmu. Testy można uruchamiać bezpośrednio z wiersza poleceń przy użyciu polecenia (`dotnet test`).

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

| Komenda                                             | Opis                                       |
| ----------------------------------------------------| -------------------------------------------|
| `dotnet build`                                      | Buduje wszystkie projekty w solution       |
| `dotnet run --project BezpieczenstwoDanych`         | Uruchamia aplikację WinForms               |
| `dotnet test`                                       | Uruchamia testy xUnit                      |
| `dotnet clean`                                      | Czyści foldery `bin` i `obj`               |
| `dotnet restore`                                    | Pobiera pakiety NuGet                      |
| `dotnet nuget locals all --clear`                   | Czyści cache pakietów NuGet                |
| `dotnet test --logger "console;verbosity=detailed`  | Uruchamia testy z wyświetleniem infromacji |

---
