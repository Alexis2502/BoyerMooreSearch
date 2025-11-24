using BezpieczenstwoDanych.src.BezpieczenstwoDanych;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BezpieczenstwoDanych
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

                lstResults.Items.Add($"Znaleziono {results.Count} dopasowañ.");

                if (results.Count > 0)
                {
                    lstResults.Items.Add("Pozycje dopasowañ:");
                    foreach (var pos in results)
                        lstResults.Items.Add(pos);
                }
            }
            catch (Exception ex)
            {
                lblValidation.Text = $"B³¹d: {ex.Message}";
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
                lblValidation.Text = "Podaj tekst, w którym szukasz.";
                return;
            }

            if (string.IsNullOrEmpty(pattern))
            {
                lblValidation.Text = "Podaj wzorzec do wyszukania.";
                return;
            }

            try
            {
                long memBefore = GC.GetTotalMemory(true);

                var sw = Stopwatch.StartNew();
                var results = searcher.Search(pattern, text);
                sw.Stop();

                long memAfter = GC.GetTotalMemory(false);

                // konwersja do nanosekund:
                double ns = sw.ElapsedTicks * (1_000_000_000.0 / Stopwatch.Frequency);

                int n = text.Length;
                int m = pattern.Length;
                int k = results.Count;

                lstResults.Items.Add($"Znaleziono dopasowañ: {k}");
                lstResults.Items.Add($"Czas (ns): {ns:F0} ns");
                lstResults.Items.Add($"Czas (µs): {(ns / 1000):F3} µs");
                lstResults.Items.Add($"Zu¿ycie pamiêci: {memAfter - memBefore} B");
                lstResults.Items.Add($"D³ugoœæ tekstu: {n}, d³ugoœæ wzorca: {m}");

                if (k > 0)
                {
                    lstResults.Items.Add("Pozycje dopasowañ:");
                    foreach (var pos in results)
                        lstResults.Items.Add(pos);
                }

                if (lstResults.Items.Count > 0)
                    lstResults.TopIndex = lstResults.Items.Count - 1;

            }
            catch (Exception ex)
            {
                lblValidation.Text = $"B³¹d: {ex.Message}";
            }
        }
    }
}