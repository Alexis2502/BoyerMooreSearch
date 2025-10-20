using System;
using System.Collections.Generic;

namespace BezpieczenstwoDanych
{
    /// <summary>
    /// Algorytm wyszukiwania pod³añcuchów metod¹ Boyera-Moore'a (Unicode-safe).
    /// </summary>
    public class BoyerMooreSearch
    {
        /// <summary>
        /// Nazwa algorytmu.
        /// </summary>
        public string Name()
        {
            return "Boyer-Moore";
        }

        /// <summary>
        /// Tworzy s³ownik z³ych znaków dla wzorca.
        /// </summary>
        private static Dictionary<char, int> BadCharHeuristic(string pattern)
        {
            var badChar = new Dictionary<char, int>();
            for (int i = 0; i < pattern.Length; i++)
            {
                badChar[pattern[i]] = i; // zapisuje ostatnie wyst¹pienie
            }
            return badChar;
        }

        /// <summary>
        /// Szuka wszystkich wyst¹pieñ wzorca w tekœcie.
        /// </summary>
        public List<int> Search(string pattern, string text)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException("Pattern nie mo¿e byæ null ani pusty", nameof(pattern));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text nie mo¿e byæ null ani pusty", nameof(text));

            var result = new List<int>();
            int m = pattern.Length;
            int n = text.Length;

            var badChar = BadCharHeuristic(pattern);
            int s = 0; // przesuniêcie wzorca wzglêdem tekstu

            while (s <= n - m)
            {
                int j = m - 1;

                // dopasowywanie od koñca wzorca
                while (j >= 0 && pattern[j] == text[s + j])
                    j--;

                if (j < 0)
                {
                    // wzorzec dopasowany
                    result.Add(s);

                    // przesuniêcie wzorca po znalezieniu dopasowania
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
                    // przesuniêcie wzorca w przypadku z³ego znaku
                    char badCharInText = text[s + j];
                    int shift = badChar.ContainsKey(badCharInText) ? j - badChar[badCharInText] : j + 1;
                    s += Math.Max(1, shift);
                }
            }

            return result;
        }
    }
}