﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar
{
    public class ModelIntroduceExpression : IExpression
    {
        public string VarType { get; set; }
        public string VarName { get; set; }

        public ModelIntroduceExpression(string varType, string varName)
        {
            VarType = varType;
            VarName = varName;
        }
    }
}
