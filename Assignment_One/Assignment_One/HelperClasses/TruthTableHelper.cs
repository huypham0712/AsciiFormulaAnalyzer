﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        public static List<List<string[]>> GetPreSimplifyValues(List<int[]> tableValue, List<int> truthTableResult, List<Variable> variables)
        {
            List<int> indices = GetIndicesOfTrue(truthTableResult);
            List<string[]> result = new List<string[]>();

            foreach (int index in indices)
            {
                string[] data = new string[variables.Count];
                int counter = 0;
                for (int i = 0; i < tableValue.Count; i++)
                {
                    for (int j = 0; j < tableValue[i].Length; j++)
                    {
                        if (j == index)
                        {
                            data[counter] = tableValue[i][j].ToString();
                            counter++;
                        }
                    }
                }
                result.Add(data);
            }

            List<List<string[]>> sameNrOfOnes = new List<List<string[]>>();
            for (int i = 0; i < variables.Count + 1; i++)
            {
                List<string[]> termGrouped = new List<string[]>();
                foreach (string[] preSimplifyValue in result)
                {
                    if (preSimplifyValue.Count(x => x.Equals("1")) == i)
                    {
                        termGrouped.Add(preSimplifyValue);
                    }
                }

                if (termGrouped.Any())
                {
                    sameNrOfOnes.Add(termGrouped);
                }
            }

            return sameNrOfOnes;
        }

        public static List<string[]> GetFalseRows(List<int[]> tableValue, List<int> truthTableResult, List<Variable> variables)
        {
            List<int> indices = GetIndicesOfFalse(truthTableResult);
            List<string[]> result = new List<string[]>();

            foreach (int index in indices)
            {
                string[] data = new string[variables.Count];
                int counter = 0;
                for (int i = 0; i < tableValue.Count; i++)
                {
                    for (int j = 0; j < tableValue[i].Length; j++)
                    {
                        if (j == index)
                        {
                            data[counter] = tableValue[i][j].ToString();
                            counter++;
                        }
                    }
                }
                result.Add(data);
            }
            return result;
        }

        private static List<int> GetIndicesOfTrue(List<int> truthTableResult)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < truthTableResult.Count; i++)
            {
                if (truthTableResult[i] == 1)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        private static List<int> GetIndicesOfFalse(List<int> truthTableResult)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < truthTableResult.Count; i++)
            {
                if (truthTableResult[i] == 0)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public static int GetNrOfSimplifiedRows(List<List<string[]>> simplifiedData)
        {
            int result = 0;
            foreach (var listOfData in simplifiedData)
            {
                result += listOfData.Count;
            }

            return result;
        }
    }
}
