using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewEngine.Core.Engine
{
    public class RenderException : Exception
    {
        public RenderException(string message) 
            : base(message)
        {
        }
    }
}
