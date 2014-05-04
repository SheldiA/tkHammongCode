using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tk3
{
    class RectangularCode:CodeAlgorithm,ICode
    {
        private const int blockSize = 4;
        private int size;
        public RectangularCode(int size)
        {
            this.size = size;
        }

        public string GetCodeWord(string incomingStr)
        {
            incomingStr = AlignStrByBlock(incomingStr, blockSize);
            FillParityArrays();
            FillGeneratorMatrix(size,size + posParityNumber.Count);
            int[] incomingArr = StrToIntArr(incomingStr);
            int[,] res = MathOperationLibrary.MathOperationLibrary.Multiplicate(incomingArr, generatorMatrix);
            int[] arr = new int[res.GetLength(1)];
            for (int i = 0; i < arr.Length; ++i)
                arr[i] = res[0, i];
            return IntArrToStr(arr);
        }

        private void FillParityArrays()
        {
            List<int> currParity = new List<int>();
            for(int i = 0; i < size; ++i)
            {                
                if (i % blockSize == 0 && i != 0)
                {
                    parityComposition.Add(currParity);
                    currParity = new List<int>();
                    posParityNumber.Add(i + posParityNumber.Count);
                }
                currParity.Add(i);
            }
            parityComposition.Add(currParity);
            posParityNumber.Add(size + posParityNumber.Count);

            for(int i = 0; i < blockSize; ++i)
            {
                currParity = new List<int>();
                int currPos = i;
                while(currPos < size)
                {
                    currParity.Add(currPos);
                    currPos += blockSize;
                }
                posParityNumber.Add(size + posParityNumber.Count);
                parityComposition.Add(currParity);
            }
            currParity = new List<int>();
            for (int i = 0; i < size; ++i)
                currParity.Add(i);
            posParityNumber.Add(size + posParityNumber.Count);
            parityComposition.Add(currParity);
        }

        public string Decode(string codeWord)
        {
            FillPartityCheckMatrix(posParityNumber.Count, size + posParityNumber.Count);
            int[] codeWordArr = StrToIntArr(codeWord);
            int[,] res = MathOperationLibrary.MathOperationLibrary.Multiplicate(codeWordArr, partityCheckMatrix);
            return FixError(res, codeWord);
        }
    }
}
