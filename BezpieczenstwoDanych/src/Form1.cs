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
                Stopwatch sw = Stopwatch.StartNew();

                var results = searcher.Search(pattern, text);

                sw.Stop();
                long memAfter = GC.GetTotalMemory(false);

                int n = text.Length;
                int m = pattern.Length;
                int k = results.Count;

                lstResults.Items.Add($"Znaleziono dopasowañ: {k}");
                lstResults.Items.Add($"Czas wykonania: {sw.ElapsedMilliseconds} ms");
                lstResults.Items.Add($"Zu¿ycie pamiêci: {memAfter - memBefore} bajtów");
                lstResults.Items.Add($"D³ugoœæ tekstu: {n}, d³ugoœæ wzorca: {m}");
                lstResults.Items.Add($"Przybli¿ona z³o¿onoœæ: O(n/m * {Math.Max(1, k)})"); // prosta estymacja

                if (k > 0)
                {
                    lstResults.Items.Add("Pozycje dopasowañ:");
                    foreach (var pos in results)
                        lstResults.Items.Add(pos);
                }

                // przewiniêcie do koñca
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
