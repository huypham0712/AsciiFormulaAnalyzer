﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiFormulaAnalyzer
{
    public class Implication : AsciiFormula
    {
        public AsciiFormula LeftFormula { get; set; }
        public AsciiFormula RightFormula { get; set; }
        public override int TruthValue => ComputeTruthValue();

        public Implication(AsciiFormula leftFormula, AsciiFormula rightFormula)
        {
            LeftFormula = leftFormula;
            RightFormula = rightFormula;
        }

        public override string DrawGraph(ref int index, int preIndex)
        {
            string result = $"\nnode{index} [ label = \"\u21d2\" ]";
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
            if (LeftFormula.TruthValue == 1 && RightFormula.TruthValue == 0)
            {
                return 0;
            }

            return 1;
        }

        public override AsciiFormula Nandify()
        {
            return new Nand(LeftFormula.Nandify(), new Nand(RightFormula.Nandify(), RightFormula.Nandify()));
        }
    }
}
