using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Common
{
    public class VariableReference : IVarContent
    {
        public string ReferenceString { get; set; }

        public VariableReference(string referenceString)
        {
            ReferenceString = referenceString;
        }
    }
}
