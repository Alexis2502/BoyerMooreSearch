using System;
using System.Text;
using System.Collections.Generic;
using BezpieczenstwoDanych.src;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using ScottPlot;


namespace BezpieczenstwoDanych.Tests
{
    public class BoyerMooreSearchTests
    {
        private readonly BoyerMooreSearch _bm;
        private readonly ITestOutputHelper _output;


        // Konstruktor testu
        public BoyerMooreSearchTests(ITestOutputHelper output)
        {
            _bm = new BoyerMooreSearch();
            _output = output;
        }

        // Wyszukiwanie pojedyńczego wystapienia
        [Fact]
        public void Search_ShouldFindSingleOccurrence()
        {
            var result = _bm.Search("kota", "Ala ma kota");
            Assert.Single(result);
            Assert.Equal(7, result[0]);
        }

        // Wyszukiwanie wielu wystapień
        [Fact]
        public void Search_ShouldFindMultipleOccurrences()
        {
            var result = _bm.Search("ab", "abababab");
            Assert.Equal(new List<int> { 0, 2, 4, 6 }, result);
        }

        // Brak wystapień wzorca
        [Fact]
        public void Search_ShouldReturnEmpty_WhenPatternNotFound()
        {
            var result = _bm.Search("xyz", "abcdefgh");
            Assert.Empty(result);
        }

        // Wzorzec na początku
        [Fact]
        public void Search_ShouldFindPatternAtStart()
        {
            var result = _bm.Search("test", "testowanie algorytmu");
            Assert.Single(result);
            Assert.Equal(0, result[0]);
        }

        // Wzorzec na końcu
        [Fact]
        public void Search_ShouldFindPatternAtEnd()
        {
            var result = _bm.Search("przyklad", "to jest przyklad");
            Assert.Single(result);
            Assert.Equal(8, result[0]);
        }

        // Wzorzec w środku
        [Fact]
        public void Search_ShouldFindPatternAtMiddle()
        {
            var result = _bm.Search("abc", "xxxxxabcxxxxx");
            Assert.Single(result);
            Assert.Equal(5, result[0]);
        }

        // Pusty tekst
        [Fact]
        public void Search_ShouldThrowWhenTextIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => _bm.Search("abc", ""));
        }

        // Pusty wzorzec
        [Fact]
        public void Search_ShouldHandleEmptyPattern()
        {
            Assert.Throws<ArgumentException>(() => _bm.Search("", "abcdefgh"));
        }

        // Wzorzec dłuższy niz tekst
        [Fact]
        public void Search_ShouldHandlePatternLongerThanText()
        {
            var result = _bm.Search("abcd", "abc");
            Assert.Empty(result);
        }

        // Wzorzec z powtarzającymi sie znakami
        [Fact]
        public void Search_ShouldHandleRepeatedCharacters()
        {
            var result = _bm.Search("aaa", "aaaaaaaaaaaaa");
            Assert.Equal(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, result);
        }

        // Rozklad naturalny
        [Fact]
        public void Search_ShouldHandleNaturalDecomposition()
        {
            string text =
                "Normalny tekst zawiera slowa o roznej dlugosci i czestotliwosci wystepowania. " +
                "Dzieki temu rozklad liter jest zblizony do naturalnego rozkladu jezykowego.";

            string pattern = "rozklad";
            
            var result = _bm.Search(pattern, text);
            Assert.Equal(new List<int> {  90,  133 }, result);
        }

        // Rozklad losowy
        [Fact]
        public void Search_ShouldHandleRandomDistribution_AndReportCharCounts()
        {
            string text = GenerateRandomText(400);
            string pattern = "ab";

            var result = _bm.Search(pattern, text);
            var dist = GetCharDistribution(text);

            _output.WriteLine("Tekst: ");
            _output.WriteLine(text);
            _output.WriteLine("Rozklad znakow: ");

            SaveCharDistributionPlot(dist, _output);

            Assert.NotNull(result);

            // Wyświetlenie rozkładu znaków w konsoli testów
            // foreach (var kv in dist.OrderBy(k => k.Key))
            //     _output.WriteLine($"{kv.Key} = {kv.Value}");
        }

        // Rozklad powtarzalny
        [Fact]
        public void Search_ShouldHandleRepetitiveDistribution()
        {
            string text = new string('a', 200);
            string pattern = "a";
            var result = _bm.Search(pattern, text);
            Assert.Equal(200, result.Count);
        }

        // Pomocnicza metoda do generowania losowego tekstu
        private static string GenerateRandomText(int length)
        {
            var rand = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz";
            var buffer = new char[length];

            for (int i = 0; i < length; i++)
                buffer[i] = chars[rand.Next(chars.Length)];

            return new string(buffer);
        }

        // Pomocniczna metoda do uzyskiwania rozkladu znakow
        private static Dictionary<char, int> GetCharDistribution(string text)
        {
            return text
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        // Pomocnicza metoda do zapisywania wykresu rozkladu znakow
        private static void SaveCharDistributionPlot(
            Dictionary<char, int> dist,
            ITestOutputHelper output)
        {
            var ordered = dist.OrderBy(k => k.Key).ToList();
            double[] values = ordered.Select(x => (double)x.Value).ToArray();
            string[] labels = ordered.Select(x => x.Key.ToString()).ToArray();

            var plt = new ScottPlot.Plot(800, 450);
            plt.AddBar(values);
            plt.XTicks(labels);
            plt.Title("Rozkład znaków w tekście");
            plt.YLabel("Liczba wystąpień");
            plt.XLabel("Znak");

            string binDir = AppContext.BaseDirectory;
            string projectDir = Directory.GetParent(binDir)!.Parent!.Parent!.Parent!.FullName;
            string reportsDir = Path.Combine(projectDir, "plots");
            Directory.CreateDirectory(reportsDir);
            string filePath = Path.Combine(
                reportsDir,
                $"CharDistribution_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            plt.SaveFig(filePath);
            output.WriteLine($"Wykres zapisany do: {filePath}");
        }
    }
}