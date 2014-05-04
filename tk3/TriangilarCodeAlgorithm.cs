using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAlgorithms;

namespace tk3
{
    class TriangilarCodeAlgorithm:ICode
    {
        private int sizeOfHigherLine;
        private int size;
        private int[,] generatorMatrix;
        private int[,] partityCheckMatrix;
        private List<int> posParityNumber;
        private List<List<int>> parityComposition;

        public TriangilarCodeAlgorithm(int size)
        {
            sizeOfHigherLine = GetMaxLine(size);
            parityComposition = new List<List<int>>();
        }

        private int GetMaxLine(int n)
        {
            size = 0;
            int step = 0;
            while (size < n)
            {
                ++step;
                size += step;
            }
            return step;
        }

        private string AlignStrByBlock(string str, int lengthBlock)
        {
            while (str.Length % lengthBlock != 0)
                str = "0" + str;
            return str;
        }

        public string GetCodeWord(string incomingStr)
        {
            incomingStr = AlignStrByBlock(incomingStr, size);
            char[][] triangle = GetTriangleFromStr(incomingStr, sizeOfHigherLine);
            FillParityPos(triangle);
            FillParityComposition(triangle);
            FillGeneratorMatrix();            
            int[] incomingArr = StrToIntArr(incomingStr);
            int[,] res = MathOperationLibrary.MathOperationLibrary.Multiplicate(incomingArr, generatorMatrix);
            int[] arr = new int[res.GetLength(1)];
            for (int i = 0; i < arr.Length; ++i )
                arr[i] = res[0, i];
            return IntArrToStr(arr);
        }

        private int[] StrToIntArr(string str)
        {
            int[] arr = new int[str.Length];
            for (int i = 0; i < str.Length; ++i )
                arr[i] = (str[i] == '0') ? 0 : 1;
                return arr;
        }

        private string IntArrToStr(int[] arr)
        {
            string str ="";

            for (int i = 0; i < arr.Length; ++i)
                str += (arr[i] % 2 == 0) ? "0" : "1";
            return str;
        }

        private void FillParityComposition(char[][] triangle)
        {
            for(int i = 0; i <= sizeOfHigherLine; ++i)
            {
                List<int> currParity = new List<int>();
                if(i < triangle.GetLength(0))
                {
                    int posFirst = GetPosElementInTriangle(triangle, i, 0);
                    for(int trCol = 0; trCol < triangle[i].Length; ++trCol)
                    {
                        currParity.Add(posFirst);
                        ++posFirst;
                    }
                }

                int j = (i < triangle.GetLength(0)) ? triangle[i].Length : 0;
                for (int trRow = i - 1; trRow >= 0; --trRow )
                {
                    currParity.Add(GetPosElementInTriangle(triangle,trRow,j));
                }
                currParity.Sort();
                parityComposition.Add(currParity);
            }
        }

        private int GetPosElementInTriangle(char[][] triangle,int row,int col)
        {
            int res = 0;

            if(row < triangle.GetLength(0) && col < triangle[row].Length)
            {
                for (int i = 0; i < row; ++i)
                    res += triangle[i].Length;
                res += col;
            }

            return res;
        }

        private void FillParityPos(char[][] triangle)
        {
            posParityNumber = new List<int>();
            int strPos = 0;
            for(int i = 0; i < triangle.GetLength(0);++i)
            {
                strPos += triangle[i].Length;
                posParityNumber.Add(strPos);
                ++strPos;
            }
            posParityNumber.Add(strPos);
        }

        private char[][] GetTriangleFromStr(string str,int higherLine)
        {
            char[][] result = new char[higherLine][];
            int strPos = 0;
            int curStrSize = higherLine;
            for(int i = 0; i < higherLine;++i)
            {
                result[i] = new char[curStrSize];
                for (int j = 0; j < curStrSize; ++j, ++strPos)
                    if (strPos < str.Length)
                        result[i][j] = str[strPos];
                --curStrSize;
            }
            return result;
        }

        private void FillGeneratorMatrix()
        {
            generatorMatrix = new int[size, size + sizeOfHigherLine + 1];
            for(int i = 0; i < size; ++i)
            {
                int curPosMesSymb = 0;
                for (int j = 0; j < generatorMatrix.GetLength(1); ++j)
                {
                    if(posParityNumber.Contains(j))
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

        private void FillPartityCheckMatrix()
        {
            partityCheckMatrix = new int[posParityNumber.Count, size + posParityNumber.Count];
            for(int i = 0; i < partityCheckMatrix.GetLength(0); ++i)
            {
                int currMesPos = 0;
                for(int j = 0; j < partityCheckMatrix.GetLength(1); ++j)
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

        public string Decode(string codeWord)
        {
            FillPartityCheckMatrix();
            int[] codeWordArr = StrToIntArr(codeWord);
            int[,] res = MathOperationLibrary.MathOperationLibrary.Multiplicate(codeWordArr, partityCheckMatrix);
            return FixError(res,codeWord);
        }

        private string FixError(int[,] syndroms,string codeWord)
        {
            string res = "";
            List<int> numbErrSydd = new List<int>();
            int posErr = -1;
            for (int i = 0; i < syndroms.GetLength(1); ++i)
                if (syndroms[0, i] % 2 != 0)
                    numbErrSydd.Add(i);
            if(numbErrSydd.Count > 1)
            {
                List<int> resComp = parityComposition[numbErrSydd[0]];
                for (int i = 1; i < numbErrSydd.Count; ++i)
                    resComp = parityComposition[numbErrSydd[i]].Where(p => resComp.Contains(p)).ToList();
                if(resComp.Count > 0)
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
