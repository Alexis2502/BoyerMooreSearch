using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BezpieczenstwoDanych
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string haystack = txtHaystack.Text;
            string needle = txtNeedle.Text;

            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                MessageBox.Show("Wprowadü oba teksty!", "B≥πd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BoyerMooreSearch algorithm = new BoyerMooreSearch();
                List<int> results = algorithm.Search(needle, haystack);

                lstResults.Items.Clear();
                foreach (int index in results)
                {
                    lstResults.Items.Add($"Wystπpienie na indeksie: {index}");
                }

                SearchAlgorithmValidator validator = new SearchAlgorithmValidator(algorithm);
                lblValidation.Text = validator.Validate(needle, haystack) ? "Wynik prawid≥owy" : "Wynik nieprawid≥owy";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"B≥πd: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
