using System.Collections.Generic;

namespace BezpieczenstwoDanych
{
    public class SearchAlgorithmValidator
    {
        private BoyerMooreSearch algoToValidate;
        private BoyerMooreSearch properlyWorkingAlgorithm;

        public SearchAlgorithmValidator(BoyerMooreSearch algoToValidate)
        {
            this.algoToValidate = algoToValidate;
            this.properlyWorkingAlgorithm = new BoyerMooreSearch();
        }

        public bool Validate(string needle, string haystack)
        {
            if (needle == null || haystack == null)
                return false;

            List<int> algoResult = algoToValidate.Search(needle, haystack);
            List<int> referentialResult = properlyWorkingAlgorithm.Search(needle, haystack);

            algoResult.Sort();
            referentialResult.Sort();

            if (algoResult.Count != referentialResult.Count)
                return false;

            for (int i = 0; i < algoResult.Count; i++)
                if (algoResult[i] != referentialResult[i])
                    return false;

            return true;
        }
    }
}