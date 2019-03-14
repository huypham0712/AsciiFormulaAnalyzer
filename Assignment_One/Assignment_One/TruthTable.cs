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

        public List<List<string[]>> Simplify(List<List<string[]>> data)
        {
            List<List<string[]>> result = new List<List<string[]>>();
            bool canBeSimplified = false;

            //For each pair
            for (int i = 0; i < data.Count - 1; i++)
            {
                List<string[]> simplifiedData = new List<string[]>();
                foreach (string[] previousGroup in data[i])
                {
                    foreach (string[] groupBehind in data[i + 1])
                    {
                        int tmp = GetIndexDiff(previousGroup, groupBehind);
                        if (tmp != -1)
                        {
                            string[] simplifiedRow = (string[])previousGroup.Clone();
                            simplifiedRow[tmp] = "*";
                            canBeSimplified = true;
                            if (!CheckDuplicatedSimplifiedRow(simplifiedData, simplifiedRow))
                            {
                                simplifiedData.Add(simplifiedRow);
                            }                        
                        }
                    }
                }

                if (simplifiedData.Any())
                {
                    result.Add(simplifiedData);
                }
            }

            if (!canBeSimplified || !data.Any())
            {
                return data;
            }

            return Simplify(result);
        }

        private bool CheckDuplicatedSimplifiedRow(List<string[]> simplifiedData, string[] simplifiedRow)
        {
            foreach (var array in simplifiedData)
            {
                if (array.SequenceEqual(simplifiedRow))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetIndexDiff(string[] s1, string[] s2)
        {
            if (!s1.Any() || !s2.Any())
            {
                return -1;
            }

            List<int> diffIndex = new List<int>();
            for (int i = 0; i < s1.Length; i++)
            {
                if (!s1[i].Equals(s2[i]))
                {
                    diffIndex.Add(i);
                }
            }

            if (diffIndex.Count == 1)
            {
                return diffIndex[0];
            }

            return -1;
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
