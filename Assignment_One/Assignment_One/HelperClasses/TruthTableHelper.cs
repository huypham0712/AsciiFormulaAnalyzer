using System;
using System.Collections.Generic;

namespace AsciiFormulaAnalyzer
{
    public static class TruthTableHelper
    {
        public static List<int[]> GetPreComputeValues(List<Variable> variables)       
        {
            List<int[]> result = new List<int[]>();
            int nrOfVariable = variables.Count;

            for (int i = 0; i < variables.Count; i++)
            {
                int[] variable = new int[(int)Math.Pow(2, variables.Count)];
                int nrOfZeros = (int)Math.Pow(2, nrOfVariable - 1);
                int nrOfOnes = nrOfZeros;
                int zeroCounter = 0;
                int oneCounter = 0;

                for (int j = 0; j < variable.Length; j++)
                {
                    if (zeroCounter < nrOfZeros)
                    {
                        variable[j] = 0;
                        zeroCounter++;
                    }
                    else if(zeroCounter >= nrOfZeros && oneCounter < nrOfOnes)
                    {
                        variable[j] = 1;
                        oneCounter++;
                    }

                    if (j == variable.Length - 1 && variable[j] != 1)
                    {
                        //reset the loop at the last position of 1;
                        j = FindLastIndexOfOne(variable);
                        zeroCounter = 0;
                        oneCounter = 0;
                    }
                }

                nrOfVariable--;
                result.Add(variable);
            }

            return result;
        }

        private static int FindLastIndexOfOne(int[] variable)
        {
            for (int i = variable.Length - 1; i >= 0; i--)
            {
                if (variable[i] == 1)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
