using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewEngine.Core.Engine;

namespace ViewEngine.Runtime
{
    class Program
    {
        public static void Main()
        {
            var coreEngine = new CoreEngine();
            coreEngine.Render("Widok",
                "Przestrzen",
                @"Samples\FuncBodyWithUsage.view",
                null,
                "output.cs");
        }
    }
}
