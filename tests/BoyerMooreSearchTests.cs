using System;
using System.Collections.Generic;
using Xunit;
using BezpieczenstwoDanych.src;

namespace BezpieczenstwoDanych.Tests
{
    public class BoyerMooreSearchTests
    {
        private readonly BoyerMooreSearch _searcher = new BoyerMooreSearch();

        // --- PODSTAWY ---

        [Fact]
        public void Search_FindsSingleOccurrence()
        {
            string text = "abcdefgh";
            string pattern = "cde";

            var result = _searcher.Search(pattern, text);

            Assert.Single(result);
            Assert.Equal(2, result[0]);
        }

        [Fact]
        public void Search_FindsMultipleOccurrences()
        {
            string text = "test test test";
            string pattern = "test";

            var result = _searcher.Search(pattern, text);

            Assert.Equal(new List<int> { 0, 5, 10 }, result);
        }

        [Fact]
        public void Search_NoMatch_ReturnsEmpty()
        {
            string text = "abcdefgh";
            string pattern = "xyz";

            var result = _searcher.Search(pattern, text);

            Assert.Empty(result);
        }

        // --- BŁĘDNE PARAMETRY ---

        [Fact]
        public void Search_EmptyPattern_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                _searcher.Search("", "abcdef"));
        }

        [Fact]
        public void Search_NullPattern_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                _searcher.Search(null, "abcdef"));
        }

        [Fact]
        public void Search_EmptyText_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                _searcher.Search("abc", ""));
        }

        [Fact]
        public void Search_NullText_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                _searcher.Search("abc", null));
        }

        // --- PRZYPADKI BRZEGOWE ---

        [Fact]
        public void Search_PatternLongerThanText_ReturnsEmpty()
        {
            var result = _searcher.Search("abcdef", "abc");
            Assert.Empty(result);
        }

        [Fact]
        public void Search_OverlappingMatches()
        {
            string text = "aaaaa";
            string pattern = "aaa";

            var result = _searcher.Search(pattern, text);

            Assert.Equal(new List<int> { 0, 3 }, result);
        }

        [Fact]
        public void Search_MatchAtEndOfText()
        {
            string text = "abcabcabcXYZ";
            string pattern = "XYZ";

            var result = _searcher.Search(pattern, text);

            Assert.Single(result);
            Assert.Equal(9, result[0]);
        }

        // --- DUŻY TEST ---

        [Fact]
        public void Search_LargeText_FindsCorrectPosition()
        {
            string pattern = "xyz";
            string text = new string('a', 50000) + "xyz" + new string('b', 50000);

            var result = _searcher.Search(pattern, text);

            Assert.Single(result);
            Assert.Equal(50000, result[0]);
        }

        // --- CASE SENSITIVITY ---

        [Fact]
        public void Search_IsCaseSensitive()
        {
            string text = "Algorithm and ALGORITHM";
            string pattern = "algorithm";

            Assert.Empty(_searcher.Search(pattern, text));
        }
    }
}
