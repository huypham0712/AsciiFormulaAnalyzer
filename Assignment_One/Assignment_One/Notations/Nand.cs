using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiFormulaAnalyzer
{
    public class Nand : AsciiFormula
    {
        public AsciiFormula LeftFormula { get; set; }
        public AsciiFormula RightFormula { get; set; }
        public override int TruthValue => ComputeTruthValue();

        public Nand(AsciiFormula leftFormula, AsciiFormula rightFormula)
        {
            LeftFormula = leftFormula;
            RightFormula = rightFormula;
        }

        public override string DrawGraph(ref int index, int preIndex = 0)
        {
              return new Negation(
                  new Conjunction(LeftFormula, RightFormula)).DrawGraph(ref index, preIndex);
        }

        public override int ComputeTruthValue()
        {
            if (LeftFormula.TruthValue == 1 && RightFormula.TruthValue == 1)
            {
                return 0;
            }

            return 1;
        }

        public override AsciiFormula Nandify()
        {
            return this;
        }

        public override string ToString()
        {
            return $"%({LeftFormula},{RightFormula})";
        }
    }
}
