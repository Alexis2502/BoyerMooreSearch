using System;
using System.Collections.Generic;

namespace BezpieczenstwoDanych.src
{
    /// <summary>
    /// Algorytm wyszukiwania podłańcuchów metodą Boyera-Moore'a.
    /// </summary>
    public class BoyerMooreSearch
    {
        /// <summary>
        /// Nazwa algorytmu
        /// </summary>
        public string Name()
        {
            return "Boyer-Moore";
        }

        /// <summary>
        /// Tworzy słownik złych znaków dla wzorca
        /// </summary>
        private static Dictionary<char, int> BadCharHeuristic(string pattern)
        {
            var badChar = new Dictionary<char, int>();
            for (int i = 0; i < pattern.Length; i++)
            {
                badChar[pattern[i]] = i; // zapisuje ostatnie wystąpienie znaku
            }
            return badChar;
        }

        /// <summary>
        /// Szuka wszystkich wystąpień wzorca w tekście
        /// </summary>
        public List<int> Search(string pattern, string text)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException("Wzorzec nie może byc null ani pusty", nameof(pattern));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text nie może byc null ani pusty", nameof(text));

            var result = new List<int>();
            int m = pattern.Length;
            int n = text.Length;

            var badChar = BadCharHeuristic(pattern);
            int s = 0; // przesunięcie wzorca względem tekstu

            while (s <= n - m)
            {
                int j = m - 1;

                // dopasowywanie od końca wzorca
                while (j >= 0 && pattern[j] == text[s + j])
                    j--;

                if (j < 0)
                {
                    // wzorzec dopasowany
                    result.Add(s);

                    // przesunięcie wzorca po znalezieniu dopasowania
                    if (s + m < n)
                    {
                        char nextChar = text[s + m];
                        int shift = badChar.ContainsKey(nextChar) ? m - badChar[nextChar] : m + 1;
                        s += shift;
                    }
                    else
                    {
                        s += 1;
                    }
                }
                else
                {
                    // przesunięcie wzorca w przypadku złego znaku
                    char badCharInText = text[s + j];
                    int shift = badChar.ContainsKey(badCharInText) ? j - badChar[badCharInText] : j + 1;
                    s += Math.Max(1, shift);
                }
            }

            return result;
        }
    }
}