using System;

namespace AsciiFormulaAnalyzer
{
    public class Variable : AsciiFormula, IComparable<Variable>
    {
        public char Proposition { get; set; }
        public override int TruthValue { get; set; }

        public Variable(char proposition)
        {
            Proposition = proposition;
        }

        public override string ToString()
        {
            return Proposition.ToString();
        }

        public override string DrawGraph(ref int index, int preIndex = 0)
        {
            string result = Environment.NewLine + $"node{index} [ label = \"{Proposition}\" ]";

            if (preIndex != 0) {
                result += Environment.NewLine + $"node{preIndex} -- node{index}";
            }
            index++;

            return result;
        }

        public void SetTruthValue(int value)
        {
            this.TruthValue = value;
        }

        public override int ComputeTruthValue()
        {
            return TruthValue;
        }

        public override AsciiFormula Nandify()
        {
            return this;
        }

        public int CompareTo(Variable other)
        {
            return this.Proposition.CompareTo(other.Proposition);
        }
    }
}
