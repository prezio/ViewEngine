using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Using
{
    public class UsingExpression
    {
        public string NamespaceName { get; set; }

        public UsingExpression(string namespaceName)
        {
            NamespaceName = namespaceName;
        }
    }
}
