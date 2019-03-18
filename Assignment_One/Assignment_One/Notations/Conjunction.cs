using System;

namespace AsciiFormulaAnalyzer.Notations
{
    public class Conjunction : AsciiFormula
    {
        public AsciiFormula LeftFormula { get; set; }
        public AsciiFormula RightFormula { get; set; }
        public override int TruthValue => ComputeTruthValue();

        public override int ComputeTruthValue()
        {
            if (LeftFormula.TruthValue == 1 && RightFormula.TruthValue == 1)
            {
                return 1;
            }

            return 0;
        }

        public Conjunction(AsciiFormula leftFormula, AsciiFormula rightFormula)
        {
            LeftFormula = leftFormula;
            RightFormula = rightFormula;
        }

        public override string DrawGraph(ref int index, int preIndex = 0)
        {
            string result = Environment.NewLine + $"node{index} [ label = \"\u2227\" ]";
            int pre = index;

            if (preIndex != 0) {
                result += Environment.NewLine + $"node{preIndex} -- node{index}";
            }

            index++;
            result += LeftFormula.DrawGraph(ref index, pre);
            result += RightFormula.DrawGraph(ref index, pre);

            return result;
        }
    }
}
