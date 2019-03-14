using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.VisualStyles;

namespace AsciiFormulaAnalyzer
{
    public class TruthTable
    {
        public List<Variable> Variables { get; }
        public AsciiFormula MainFormula { get; }
        public int NumberOfColumn => GetNrOfColumn();
        public int NumberOfRow => GetNrOfRow();

        public TruthTable(AsciiFormula mainFormula, List<Variable> variables)
        {
            Variables = variables;
            MainFormula = mainFormula;
        }

        public List<int> ComputeTruthTable()
        {
            List<int[]> tableValue = TruthTableHelper.GetPreComputeValues(Variables);
            List<int> finalResult = new List<int>();

            for (int j = 0; j < NumberOfRow; j++)
            {
                for (int i = 0; i < Variables.Count; i++)
                {
                    Variables[i].SetTruthValue(tableValue[i][j]);
                }

                int result = MainFormula.ComputeTruthValue();
                finalResult.Add(result);
            }

            return finalResult;
        }



        private List<List<string[]>> FindRowsWithSameNrOfOnes(List<string[]> preSimplifyValues)
        {
            // Find all rows that have the same number of ones.
            List<List<string[]>> sameNrOfOnes = new List<List<string[]>>();
            for (int i = 0; i < Variables.Count + 1; i++)
            {
                List<string[]> termGrouped = new List<string[]>();
                foreach (string[] preSimplifyValue in preSimplifyValues)
                {
                    if (preSimplifyValue.Count(x => x.Equals("1")) == i)
                    {
                        termGrouped.Add(preSimplifyValue);
                    }
                }

                if (termGrouped.Any())
                {
                    sameNrOfOnes.Add(termGrouped);
                }
            }

            return sameNrOfOnes;
        }

        public List<string[]> Simplify(List<string[]> preSimplifyValues, int count = 0)
        {
            count++;
            List<string[]> result = new List<string[]>();

            for (int i = 0; i < preSimplifyValues.Count; i++)
            {
                bool canBeSimplified = false;
                for (int j = 0; j < preSimplifyValues.Count; j++)
                {
                    List<int> diffIndexes = new List<int>();
                    for (int k = 0; k < preSimplifyValues[i].Length; k++)
                    {
                        if (preSimplifyValues[i][k] != preSimplifyValues[j][k])
                        {
                            diffIndexes.Add(k);
                        }
                    }

                    if (diffIndexes.Count == 1)
                    {
                        var smpfdRow = (string[])preSimplifyValues[i].Clone();

                        smpfdRow[diffIndexes[0]] = "*";

                        if (!result.Any(smpfdRow.SequenceEqual))
                        {
                            result.Add(smpfdRow);
                        }

                        canBeSimplified = true;
                    }
                }

                if (!result.Any(preSimplifyValues[i].SequenceEqual) && !canBeSimplified)
                {
                    result.Add(preSimplifyValues[i]);
                }
            }

            if (count < Variables.Count)
            {
                return Simplify(result, count);
            }

            return result;
        }

        private int GetNrOfColumn()
        {
            return Variables.Count;
        }

        private int GetNrOfRow()
        {
            return (int)Math.Pow(2, Variables.Count);
        }
    }
}
