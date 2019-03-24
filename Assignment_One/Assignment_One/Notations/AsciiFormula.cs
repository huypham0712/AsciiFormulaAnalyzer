using System.Collections.Generic;

namespace AsciiFormulaAnalyzer
{
    public abstract class AsciiFormula
    {
        public List<Variable> Variables { get; set; }
        public virtual int TruthValue { get; set; }
        public abstract string DrawGraph(ref int index, int preIndex = 0);
        public abstract int ComputeTruthValue();
        public abstract AsciiFormula Nandify();
    }
}
