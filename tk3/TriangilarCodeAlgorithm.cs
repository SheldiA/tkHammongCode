using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAlgorithms;

namespace tk3
{
    class TriangilarCodeAlgorithm : CodeAlgorithm, ICode
    {
        private int sizeOfHigherLine;
        private int size;        

        public TriangilarCodeAlgorithm(int size)
        {
            sizeOfHigherLine = GetMaxLine(size);
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

         

        public string GetCodeWord(string incomingStr)
        {
            incomingStr = AlignStrByBlock(incomingStr, size);
            char[][] triangle = GetTriangleFromStr(incomingStr, sizeOfHigherLine);
            FillParityPos(triangle);
            FillParityComposition(triangle);
            FillGeneratorMatrix(size, size + sizeOfHigherLine + 1);            
            int[] incomingArr = StrToIntArr(incomingStr);
            int[,] res = MathOperationLibrary.MathOperationLibrary.Multiplicate(incomingArr, generatorMatrix);
            int[] arr = new int[res.GetLength(1)];
            for (int i = 0; i < arr.Length; ++i )
                arr[i] = res[0, i];
            return IntArrToStr(arr);
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

        public string Decode(string codeWord)
        {
            FillPartityCheckMatrix(posParityNumber.Count, size + posParityNumber.Count);
            int[] codeWordArr = StrToIntArr(codeWord);
            int[,] res = MathOperationLibrary.MathOperationLibrary.Multiplicate(codeWordArr, partityCheckMatrix);
            return FixError(res,codeWord);
        }        
        
    }
}
