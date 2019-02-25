using System;
using System.Collections.Generic;
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
