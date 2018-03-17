using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace ViewEngine.GenerateTool
{
    public class Options
    {
        [Option('s', "SolutionDirectory", Required = true, HelpText = "Solution Directory of asp.net project")]
        public string SolutionDirectory { get; set; }

        [Option('n', "NamespaceName", Required = true, HelpText = "Name of namespace where views should be stored")]
        public string NamespaceName { get; set; }
    }
}
