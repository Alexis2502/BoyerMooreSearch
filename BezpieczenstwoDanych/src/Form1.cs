using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BezpieczenstwoDanych.src
{
    public partial class Form1 : Form
    {
        private BoyerMooreSearch searcher = new BoyerMooreSearch();

        public Form1()
        {
            InitializeComponent();
        }

        // Prostym przyciskiem tylko wyszukiwanie bez benchmarku
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string text = txtHaystack.Text;
            string pattern = txtNeedle.Text;

            lstResults.Items.Clear();
            lblValidation.Text = "";

            if (string.IsNullOrEmpty(text))
            {
                lblValidation.Text = "Podaj tekst, w kt�rym szukasz.";
                return;
            }

            if (string.IsNullOrEmpty(pattern))
            {
                lblValidation.Text = "Podaj tekst do wyszukania.";
                return;
            }

            try
            {
                var results = searcher.Search(pattern, text);

                lstResults.Items.Add($"Znaleziono {results.Count} dopasowa�.");

                if (results.Count > 0)
                {
                    lstResults.Items.Add("Pozycje dopasowa�:");
                    foreach (var pos in results)
                        lstResults.Items.Add(pos);
                }
            }
            catch (Exception ex)
            {
                lblValidation.Text = $"B��d: {ex.Message}";
            }
        }

        private void BtnUserBenchmark_Click(object sender, EventArgs e)
        {
            string text = txtHaystack.Text;
            string pattern = txtNeedle.Text;

            lstResults.Items.Clear();
            lblValidation.Text = "";

            if (string.IsNullOrEmpty(text))
            {
                lblValidation.Text = "Podaj tekst, w kt�rym szukasz.";
                return;
            }

            if (string.IsNullOrEmpty(pattern))
            {
                lblValidation.Text = "Podaj wzorzec do wyszukania.";
                return;
            }

        try
        {
            long memBefore = GC.GetAllocatedBytesForCurrentThread();
            Stopwatch sw = Stopwatch.StartNew();

            var results = searcher.Search(pattern, text);

            sw.Stop();
            long memAfter = GC.GetAllocatedBytesForCurrentThread();

            double elapsedMs = sw.ElapsedTicks * 1000.0 / Stopwatch.Frequency;

            int n = text.Length;
            int m = pattern.Length;
            int k = results.Count;

            lstResults.Items.Add($"Znaleziono dopasowań: {k}");
            lstResults.Items.Add($"Czas wykonania: {elapsedMs:F4} ms");
            lstResults.Items.Add($"Przydzielono pamięci: {memAfter - memBefore} bajtów");
            lstResults.Items.Add($"Długość tekstu: {n}, długość wzorca: {m}");
            lstResults.Items.Add($"Przybliżona złożoność: O(n/m * {Math.Max(1, k)})");

            if (k > 0)
            {
                lstResults.Items.Add("Pozycje dopasowań:");
                foreach (var pos in results)
                    lstResults.Items.Add(pos);
            }

            if (lstResults.Items.Count > 0)
                lstResults.TopIndex = lstResults.Items.Count - 1;
        }
        catch (Exception ex)
        {
            lblValidation.Text = $"Błąd: {ex.Message}";
        }

        }
    }
}
