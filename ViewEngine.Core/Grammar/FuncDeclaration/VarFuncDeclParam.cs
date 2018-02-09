﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Grammar.FuncDeclaration
{
    public class VarFuncDeclParam : IFuncDeclParam
    {
        public string VarType { get; set; }
        public string VarName { get; set; }

        public VarFuncDeclParam(string varType,
            string varName)
        {
            VarType = varType;
            VarName = varName;
        }
    }
}