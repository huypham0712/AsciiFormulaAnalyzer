using System;

namespace AsciiFormulaAnalyzer
{
    public class Disjunction : AsciiFormula
    {
        public AsciiFormula LeftFormula { get; set; }
        public AsciiFormula RightFormula { get; set; }
        public override int TruthValue => ComputeTruthValue();

        public Disjunction(AsciiFormula leftFormula, AsciiFormula rightFormula)
        {
            LeftFormula = leftFormula;
            RightFormula = rightFormula;
        }

        public override string DrawGraph(ref int index, int preIndex = 0)
        {
            string result = $"\nnode{index} [ label = \"\u2228\" ]";
            int pre = index;

            if (preIndex != 0)
            {
                result += $"\nnode{preIndex} -- node{index}";
            }

            index++;
            result += LeftFormula.DrawGraph(ref index, pre);
            result += RightFormula.DrawGraph(ref index, pre);

            return result;
        }

        public override int ComputeTruthValue()
        {
            if (LeftFormula.TruthValue == 1 || RightFormula.TruthValue == 1)
            {
                return 1;
            }

            return 0;
        }

        public override AsciiFormula Nandify()
        {
            return new Nand(new Nand(LeftFormula.Nandify(), LeftFormula.Nandify()),
                new Nand(RightFormula.Nandify(), RightFormula.Nandify()));
        }
    }
}
