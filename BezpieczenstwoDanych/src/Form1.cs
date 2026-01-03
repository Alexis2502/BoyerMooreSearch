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
                lblValidation.Text = "Podaj tekst, w którym szukasz.";
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

                lstResults.Items.Add($"Znaleziono {results.Count} dopasowań");

                if (results.Count > 0)
                {
                    lstResults.Items.Add("Pozycje dopasowań:");
                    foreach (var pos in results)
                        lstResults.Items.Add(pos);
                }
            }
            catch (Exception ex)
            {
                lblValidation.Text = $"Błąd: {ex.Message}";
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
                lblValidation.Text = "Podaj tekst, w którym szukasz";
                return;
            }

            if (string.IsNullOrEmpty(pattern))
            {
                lblValidation.Text = "Podaj wzorzec do wyszukania";
                return;
            }

            try
            {
                Stopwatch swFactorial = Stopwatch.StartNew();
                MathUtils.Factorial(50000);
                swFactorial.Stop();

                double factorialMs =
                    swFactorial.ElapsedTicks * 1000.0 / Stopwatch.Frequency;

                long memBefore = GC.GetAllocatedBytesForCurrentThread();

                Stopwatch swSearch = Stopwatch.StartNew();
                var results = searcher.Search(pattern, text);
                swSearch.Stop();

                long memAfter = GC.GetAllocatedBytesForCurrentThread();

                double searchMs =
                    swSearch.ElapsedTicks * 1000.0 / Stopwatch.Frequency;

                double ratio = searchMs / factorialMs;

                int n = text.Length;
                int m = pattern.Length;
                int k = results.Count;


                lstResults.Items.Add($"Ilość dopasowań: {k}");
                lstResults.Items.Add($"Czas Boyer-Moore: {searchMs:F4} ms");
                lstResults.Items.Add($"Czas obliczania silni: {factorialMs:F4} ms");
                lstResults.Items.Add($"Stosunek czasów (Boyer-Moore / Silnia): {ratio:F4}");
                lstResults.Items.Add($"Ilość przydzielonej pamięci: {memAfter - memBefore} bajtów");
                lstResults.Items.Add($"Długość tekstu: {n}, długość wzorca: {m}");
                lstResults.Items.Add($"Przybliżona złożonosc: O(n/m * {Math.Max(1, k)})");

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

