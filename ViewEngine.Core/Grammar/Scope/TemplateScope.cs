using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.FuncDeclaration;

namespace ViewEngine.Core.Grammar.Scope
{
    public class TemplateScope : IVarContent, IRegularExpression, IMixinBody
    {
        public List<ITemplateExpression> Result { get; set; }

        public TemplateScope(List<ITemplateExpression> expressions)
        {
            Result = expressions;
        }
    }
}
