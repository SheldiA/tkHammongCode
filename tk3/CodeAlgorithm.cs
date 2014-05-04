using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tk3
{
    class CodeAlgorithm
    {
        protected int[,] generatorMatrix;
        protected int[,] partityCheckMatrix;
        protected List<int> posParityNumber;
        protected List<List<int>> parityComposition;

        public CodeAlgorithm()
        {
            posParityNumber = new List<int>();
            parityComposition = new List<List<int>>();
        }

        protected void FillGeneratorMatrix(int numbRow,int numbCol)
        {
            generatorMatrix = new int[numbRow, numbCol];
            for (int i = 0; i < numbRow; ++i)
            {
                int curPosMesSymb = 0;
                for (int j = 0; j < numbCol; ++j)
                {
                    if (posParityNumber.Contains(j))
                    {
                        generatorMatrix[i, j] = (parityComposition[posParityNumber.IndexOf(j)].Contains(i)) ? 1 : 0;
                    }
                    else
                    {
                        generatorMatrix[i, j] = (curPosMesSymb == i) ? 1 : 0;
                        ++curPosMesSymb;
                    }
                }
            }
        }

        protected void FillPartityCheckMatrix(int numbRow, int numbCol)
        {
            partityCheckMatrix = new int[numbRow, numbCol];
            for (int i = 0; i < numbRow; ++i)
            {
                int currMesPos = 0;
                for (int j = 0; j < numbCol; ++j)
                {
                    if (posParityNumber.Contains(j))
                        partityCheckMatrix[i, j] = (posParityNumber.IndexOf(j) == i) ? 1 : 0;
                    else
                    {
                        partityCheckMatrix[i, j] = (parityComposition[i].Contains(currMesPos)) ? 1 : 0;
                        ++currMesPos;
                    }
                }
            }
            partityCheckMatrix = MathOperationLibrary.MathOperationLibrary.Transpose(partityCheckMatrix);
        }

        protected string AlignStrByBlock(string str, int lengthBlock)
        {
            while (str.Length % lengthBlock != 0)
                str = "0" + str;
            return str;
        }

        protected int[] StrToIntArr(string str)
        {
            int[] arr = new int[str.Length];
            for (int i = 0; i < str.Length; ++i)
                arr[i] = (str[i] == '0') ? 0 : 1;
            return arr;
        }

        protected string IntArrToStr(int[] arr)
        {
            string str = "";

            for (int i = 0; i < arr.Length; ++i)
                str += (arr[i] % 2 == 0) ? "0" : "1";
            return str;
        }

        protected string FixError(int[,] syndroms, string codeWord)
        {
            string res = "";
            List<int> numbErrSydd = new List<int>();
            int posErr = -1;
            for (int i = 0; i < syndroms.GetLength(1); ++i)
                if (syndroms[0, i] % 2 != 0)
                    numbErrSydd.Add(i);
            if (numbErrSydd.Count > 1)
            {
                List<int> resComp = parityComposition[numbErrSydd[0]];
                for (int i = 1; i < numbErrSydd.Count; ++i)
                    resComp = parityComposition[numbErrSydd[i]].Where(p => resComp.Contains(p)).ToList();
                if (resComp.Count > 0)
                {
                    int pos = 0;
                    int count = 0;
                    while (pos < resComp[0])
                    {
                        if (!posParityNumber.Contains(count))
                            ++pos;
                        ++count;
                    }
                    posErr = count;
                }
            }
            for (int i = 0; i < codeWord.Length; ++i)
            {
                if (!posParityNumber.Contains(i))
                {
                    if (posErr == i)
                        res += (codeWord[i] == '1') ? '0' : '1';
                    else
                        res += codeWord[i];
                }
            }
            return res;
        }
    }
}
