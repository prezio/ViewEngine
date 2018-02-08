using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.FuncUsage
{
    public class StringFuncUsageParam : IFuncUsageParam
    {
        public string TextValue { get; set; }

        public StringFuncUsageParam(string textValue)
        {
            TextValue = textValue;
        }
    }
}
