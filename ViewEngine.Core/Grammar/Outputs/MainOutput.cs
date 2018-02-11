using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Model;
using ViewEngine.Core.Grammar.Scope;

namespace ViewEngine.Core.Grammar.Outputs
{
    public class MainOutput
    {
        public List<IRegularExpression> Expressions { get; set; }
        public List<ModelIntroduceExpression> Models { get; set; }

        public MainOutput(List<IRegularExpression> expressions,
            List<ModelIntroduceExpression> models)
        {
            Expressions = expressions;
            Models = models;
        }
    }
}
