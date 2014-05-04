using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tk3
{
    class HammingCode:ICode
    {
        private int[,] generatorMatrix;
        private int[,] partityCheckMatrix;
        private List<string> powerOfTwo;
        private List<int> posParityNumber;
        private int k;
        private int r;
        private int n;

        public HammingCode(int length)
        {
            k = length;
            r = 0;

            while((Math.Pow(2,r) - r - 1) < k)
                ++r;

            n = k + r;
            GenerateAllPowerOfTwo();

            FillPosParityNumber();
            FillGeneratorMatrix();
            FillPartityCheckMatrix();
        }

        private void FillPosParityNumber()
        {
            posParityNumber = new List<int>();
            int i = 0;
            int numb = (int)Math.Pow(2, i);
            while (numb < n)
            {
                posParityNumber.Add(numb - 1);
                ++i;
                numb = (int)Math.Pow(2, i);
            }
        }

        public string GetCodeWord(string incomingStr)
        {
            string result = "";

            if(incomingStr.Length == k)
            {
                int[,] message = new int[1,k];
                for (int i = 0; i < k; ++i)
                    message[0, i] = (incomingStr[i] == '0') ? 0 : 1;
                int[,] res = MathOperationLibrary.MathOperationLibrary.Multiplicate(message, generatorMatrix);
                if(res != null)
                    for (int i = 0; i < n; ++i)
                    {
                        result += (res[0, i] % 2);
                    }
            }

            return result;
        }

        public string Decode(string codeWord)
        {
            string result = "";

            if(codeWord.Length == n)
            {
                int[,] message = new int[n,1];
                for (int i = 0; i < n; ++i)
                    message[i, 0] = (codeWord[i] == '1') ? 1 : 0;
                int[,] syndrom = MathOperationLibrary.MathOperationLibrary.Multiplicate(partityCheckMatrix, message);
                for(int i = r - 1; i >= 0; --i)
                    result += (syndrom[i, 0] % 2);
                result = CorrectErrors(result, codeWord);
            }
            return result;
        }

        private string CorrectErrors(string syndrom,string incomingMess)
        {
            string result = "";

            int errPos = Convert.ToInt32(syndrom, 2) - 1;
            if (errPos > 0)
            {
                string codeWord = ConcatMessageAndPartity(incomingMess.Substring(0, k), incomingMess.Substring(k));
                string temp = "";
                for (int i = codeWord.Length - 1; i >= 0; --i)
                {
                    if (i == errPos)
                        temp += (codeWord[i] == '1') ? '0' : '1';
                    else
                        temp += codeWord[i];
                }
                result = GetMessageFromCodeWord(Reverse(temp));
            }
            else
                result = incomingMess.Substring(0,k);
            return result;
        }

        private void GenerateAllPowerOfTwo()
        {
            powerOfTwo = new List<string>();
            for (int i = n; i > 0; --i)
            {
                string number = Convert.ToString(i, 2);
                
                if (powerOfTwo.Count > 0)
                {
                    while (number.Length < powerOfTwo[0].Length)
                        number = '0' + number;
                }

                string temp = "";
                for (int j = number.Length - 1; j >= 0; --j)
                    temp += number[j];
                powerOfTwo.Add(temp);
            }
            powerOfTwo.Reverse();
        }

        private void FillGeneratorMatrix()
        {
            generatorMatrix = new int[k,n];
            for (int i = 0; i < k; ++i)
                for (int j = 0; j < k; ++j)
                    generatorMatrix[i, j] = (i == j) ? 1 : 0;

            
            for(int i = 0; i < k; ++i)
            {
                string keyWordWithoutPartity = GenerateCodeWord(i, "");
                for(int j = 0; j < r; ++j)
                {
                    int partity = 0;
                    for (int count = 0; count < n; ++count)
                        if (powerOfTwo[count][j] == '1' && keyWordWithoutPartity[count] == '1')
                            ++partity;
                    generatorMatrix[i, j + k] = partity % 2;
                }
            }
        }

        private string GetMessageFromCodeWord(string codeWord)
        {
            string res = "";

            for (int i = 0; i < codeWord.Length; ++i )
            {
                if (!posParityNumber.Contains(i))
                    res += codeWord[i];
            }

            return Reverse(res);
        }

        private string GenerateCodeWord(int row,string parity)
        {
            string message = "";

            for (int i = 0; i < k; ++i)
                message += generatorMatrix[row, i];

            return ConcatMessageAndPartity(message,parity);
        }

        private string ConcatMessageAndPartity(string message,string parity)
        {
            string res = "";
            int countMess = k - 1;
            int countParity = 0;
            for (int i = 0; i < n; ++i)
            {
                if (!posParityNumber.Contains(i))
                {
                    res += (message.Length > countMess) ? message[countMess] : '0';
                    --countMess;
                }
                else
                {
                    res += (parity.Length > countParity) ? parity[countParity] : '0';
                    ++countParity;
                }
            }

            return res;
        }

        private void FillPartityCheckMatrix()
        {
            partityCheckMatrix = new int[r,n];
            for (int i = 0; i < r; ++i)
                for (int j = 0; j < k; ++j)
                    partityCheckMatrix[i, j] = generatorMatrix[j, i + k];

            for (int i = 0; i < r; ++i)
                for (int j = 0; j < r; ++j)
                    if (i == j)
                        partityCheckMatrix[i, j + k] = 1;

        }

        private string Reverse(string str)
        {
            string res = "";
            for (int i = str.Length - 1; i >= 0; --i)
                res += str[i];
            return res;
        }
    }
}
