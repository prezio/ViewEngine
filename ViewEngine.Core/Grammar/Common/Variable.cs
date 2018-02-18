using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.Common
{
    public class Variable : IVarContent
    {
        public string Name { get; set; }

        public Variable(string name)
        {
            Name = name;
        }
    }
}
