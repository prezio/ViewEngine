﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Grammar.Common;
using ViewEngine.Core.Grammar.MixinDeclaration;

namespace ViewEngine.Core.Grammar.Scope
{
    public class RegularScope : IVarContent, IMixinBody
    {
        public List<IRegularExpression> Result { get; set; }

        public RegularScope(List<IRegularExpression> expressions)
        {
            Result = expressions;
        }
    }
}
