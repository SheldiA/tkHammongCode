using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tk3
{
    interface ICode
    {
        string GetCodeWord(string incomingStr);
        string Decode(string codeWord);
    }
}
