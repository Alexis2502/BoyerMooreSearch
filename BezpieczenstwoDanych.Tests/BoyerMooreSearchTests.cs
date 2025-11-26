using System;
using System.Collections.Generic;
using BezpieczenstwoDanych.src;
using Xunit;

namespace BezpieczenstwoDanych.Tests
{
    public class BoyerMooreSearchTests
    {
        private readonly BoyerMooreSearch _bm;

        public BoyerMooreSearchTests()
        {
            _bm = new BoyerMooreSearch();
        }

        [Fact]
        public void Name_ShouldReturnBoyerMoore()
        {
            string name = _bm.Name();
            Assert.Equal("Boyer-Moore", name);
        }

        [Fact]
        public void Search_ShouldFindSingleOccurrence()
        {
            var result = _bm.Search("ABC", "XYZABCXYZ");
            Assert.Single(result);
            Assert.Equal(3, result[0]);
        }

        [Fact]
        public void Search_ShouldFindMultipleOccurrences()
        {
            var result = _bm.Search("AB", "ABABAB");
            Assert.Equal(new List<int> { 0, 2, 4 }, result);
        }

        [Fact]
        public void Search_ShouldReturnEmptyList_WhenPatternNotFound()
        {
            var result = _bm.Search("XYZ", "ABCDEFG");
            Assert.Empty(result);
        }

        [Fact]
        public void Search_ShouldThrowException_WhenPatternIsNullOrEmpty()
        {
            Assert.Throws<ArgumentException>(() => _bm.Search(null!, "text"));
            Assert.Throws<ArgumentException>(() => _bm.Search("", "text"));
        }

        [Fact]
        public void Search_ShouldThrowException_WhenTextIsNullOrEmpty()
        {
            Assert.Throws<ArgumentException>(() => _bm.Search("pattern", null!));
            Assert.Throws<ArgumentException>(() => _bm.Search("pattern", ""));
        }

    }
}
