using System;
using System.Linq;

namespace AsciiFormulaAnalyzer
{
    internal static class FormulaValidationHelper
    {
        private static readonly char[] ValidNotation = { '>', '&', '~', '|', '=', '%' };
        public static bool CheckValidFormula(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return false;

            string formula = IgnoreSpaces(userInput);
            bool hasInitialNotation = CheckInitNotation(formula);
            bool hasStartBracket = CheckStartBracket(formula);
            bool hasEndBracket = CheckEndBracket(formula);

            if(hasEndBracket && hasInitialNotation && hasStartBracket)
            {
                if (IsSingleFormula(formula))
                {
                    return true;
                }
                else
                {
                    //Perform validation on complex formula that has at least 2 propositions
                    return CheckComplexFormula(formula);
                }
            }
            return false;
        }


        #region Validation methods
        private static bool CheckComplexFormula(string formula)
        {   
            //The formula already has =(something) format;
            string[] invalidStuffs = new[] {",,", "()", "(,)", ")=", ")&", ")~", ")>", ")|", "(,", ",)"};
            bool notContainInvalidStuff = true;

            foreach (string s in invalidStuffs)
            {
                if (formula.Contains(s))
                {
                    notContainInvalidStuff = false;
                }
            }

            return notContainInvalidStuff;
        }

        private static bool IsSingleFormula(string formula)
        {
            return formula.Length == 4 && char.IsLetter(formula[2]);
        }

        private static bool CheckEndBracket(string formula)
        {
            return formula[formula.Length - 1].Equals(')');
        }

        private static bool CheckStartBracket(string formula)
        {
            return formula[1].Equals('(');
        }

        private static bool CheckInitNotation(string formula)
        {
            return ValidNotation.Contains(formula[0]);
        }

        #endregion

        private static string IgnoreSpaces(string original)
        {
            if (!original.Contains(" ") || string.IsNullOrWhiteSpace(original))
            {
                return original;
            }

            int indexOfSpace = original.IndexOf(' ');

            string front = original.Substring(0, indexOfSpace);
            string rear = original.Substring(indexOfSpace + 1, original.Length - indexOfSpace - 1);

            return IgnoreSpaces(front + rear);
        }
    }
}
