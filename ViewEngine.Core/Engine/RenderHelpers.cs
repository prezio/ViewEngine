using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Engine
{
    public class RenderHelpers
    {
        public static void InvokeVariable(IReadOnlyDictionary<string, Action> environment, string varName)
        {
            if (environment.ContainsKey(varName))
            {
                environment[varName]();
            }
        }
    }
}
