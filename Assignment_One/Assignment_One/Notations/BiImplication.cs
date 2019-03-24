using System;

namespace AsciiFormulaAnalyzer
{
    public class BiImplication : AsciiFormula
    {
        public AsciiFormula LeftFormula { get; set; }
        public AsciiFormula RightFormula { get; set; }
        public override int TruthValue => ComputeTruthValue();

        public override int ComputeTruthValue()
        {
            if (LeftFormula.TruthValue == RightFormula.TruthValue)
            {
                return 1;
            }
            return 0;
        }

        public override AsciiFormula Nandify()
        {
            return new Nand(
                new Nand(new Nand(LeftFormula.Nandify(), LeftFormula.Nandify()),
                    new Nand(RightFormula.Nandify(), RightFormula.Nandify())),
                new Nand(LeftFormula.Nandify(), RightFormula.Nandify()));
        }

        public BiImplication(AsciiFormula leftFormula, AsciiFormula rightFormula)
        {
            LeftFormula = leftFormula;
            RightFormula = rightFormula;
        }

        public override string DrawGraph(ref int index, int preIndex = 0)
        {
            string result = $"\nnode{index} [ label = \"\u21d4\" ]";
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
    }
}
