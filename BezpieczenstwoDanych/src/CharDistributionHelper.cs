using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScottPlot;

namespace BezpieczenstwoDanych.src
{
    public static class CharDistributionHelper
    {
        // Liczy rozkład liter w tekście (tylko a-z, małe litery)
        public static Dictionary<char, int> GetCharDistribution(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            return text
                .Where(char.IsLetter)               // tylko litery
                .Select(char.ToLowerInvariant)      // normalizacja do małych liter
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        // Zapisuje wykres rozkładu liter do katalogu "plots" w folderze projektu
        public static string SaveCharDistributionPlot(Dictionary<char, int> dist)
        {
            if (dist == null || dist.Count == 0)
                throw new ArgumentException("Rozkład znaków jest pusty.", nameof(dist));

            var ordered = dist.OrderBy(k => k.Key).ToList();
            double[] values = ordered.Select(x => (double)x.Value).ToArray();
            string[] labels = ordered.Select(x => x.Key.ToString()).ToArray();

            // Tworzymy wykres
            var plt = new Plot(800, 450);
            plt.AddBar(values);
            plt.XTicks(labels);
            plt.Title("Rozkład znaków w tekście");
            plt.YLabel("Liczba wystąpień");
            plt.XLabel("Znak");

            // Lokalizacja katalogu projektu (poza bin/)
            string binDir = AppContext.BaseDirectory;
            string projectDir = Directory.GetParent(binDir)!.Parent!.Parent!.Parent!.FullName;

            string reportsDir = Path.Combine(projectDir, "plots");
            Directory.CreateDirectory(reportsDir);

            string filePath = Path.Combine(
                reportsDir,
                $"CharDistribution_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png");

            plt.SaveFig(filePath);

            // Zwracamy ścieżkę do pliku, żeby aplikacja mogła pokazać użytkownikowi
            return filePath;
        }

        // Kombinowana metoda do wygenerowania i zapisania wykresu
        public static string PlotCharDistribution(string text)
        {
            var dist = GetCharDistribution(text);
            return SaveCharDistributionPlot(dist);
        }
    }
}
