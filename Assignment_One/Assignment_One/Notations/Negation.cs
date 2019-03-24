namespace AsciiFormulaAnalyzer
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
            string result = $"\nnode{index} [ label = \"\u00ac\" ]";
            int pre = index;

            if (preIndex != 0)
            {
                result += $"\nnode{preIndex} -- node{index}";
            }

            index++;
            result += OnlyFormula.DrawGraph(ref index, pre);

            return result;
        }

        public override int ComputeTruthValue()
        {
            return OnlyFormula.TruthValue == 1 ? 0 : 1;
        }

        public override AsciiFormula Nandify()
        {
            return new Nand(OnlyFormula.Nandify(), OnlyFormula.Nandify());
        }
    }
}
