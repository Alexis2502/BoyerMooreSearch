using System;
using System.Collections.Generic;

namespace BezpieczenstwoDanych.src
{
    /// <summary>
    /// Algorytm wyszukiwania pod�a�cuch�w metod� Boyera-Moore'a.
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
        /// Tworzy s�ownik z�ych znak�w dla wzorca.
        /// </summary>
        private static Dictionary<char, int> BadCharHeuristic(string pattern)
        {
            var badChar = new Dictionary<char, int>();
            for (int i = 0; i < pattern.Length; i++)
            {
                badChar[pattern[i]] = i; // zapisuje ostatnie wyst�pienie
            }
            return badChar;
        }

        /// <summary>
        /// Szuka wszystkich wyst�pie� wzorca w tek�cie.
        /// </summary>
        public List<int> Search(string pattern, string text)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException("Pattern nie mo�e by� null ani pusty", nameof(pattern));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text nie mo�e by� null ani pusty", nameof(text));

            var result = new List<int>();
            int m = pattern.Length;
            int n = text.Length;

            var badChar = BadCharHeuristic(pattern);
            int s = 0; // przesuni�cie wzorca wzgl�dem tekstu

            while (s <= n - m)
            {
                int j = m - 1;

                // dopasowywanie od ko�ca wzorca
                while (j >= 0 && pattern[j] == text[s + j])
                    j--;

                if (j < 0)
                {
                    // wzorzec dopasowany
                    result.Add(s);

                    // przesuni�cie wzorca po znalezieniu dopasowania
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
                    // przesuni�cie wzorca w przypadku z�ego znaku
                    char badCharInText = text[s + j];
                    int shift = badChar.ContainsKey(badCharInText) ? j - badChar[badCharInText] : j + 1;
                    s += Math.Max(1, shift);
                }
            }

            return result;
        }
    }
}