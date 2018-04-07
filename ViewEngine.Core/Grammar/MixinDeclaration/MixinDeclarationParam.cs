using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.MixinDeclaration
{
    public class MixinDeclarationParam
    {
        public MixinDeclarationParamType ParamType { get; set; }
        public string ParamContent { get; set; }

        public MixinDeclarationParam(MixinDeclarationParamType type,
            string paramContent)
        {
            ParamType = type;
            ParamContent = paramContent;
        }
    }
}
