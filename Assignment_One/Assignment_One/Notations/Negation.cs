using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiFormulaAnalyzer.Notations
{
    public class Negation : AsciiFormula
    {
        public AsciiFormula OnlyFormula { get; set; }
        public override int TruthValue => ComputeTruthValue();

        public Negation(AsciiFormula onlyFormula)
        {
            OnlyFormula = onlyFormula;
        }

        public override string DrawGraph(ref int index, int preIndex = 0)
        {
            string result = Environment.NewLine + $"node{index} [ label = \"\u00ac\" ]";
            int pre = index;

            if (preIndex != 0) {
                result += Environment.NewLine + $"node{preIndex} -- node{index}";
            }

            index++;
            result += OnlyFormula.DrawGraph(ref index, pre);

            return result;
        }

        public override int ComputeTruthValue()
        {
            return OnlyFormula.TruthValue == 1 ? 0 : 1;
        }
    }
}
