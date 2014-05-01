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
            return "";
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
        }

        public string Decode(string codeWord)
        {
            return "";
        }
        
    }
}
