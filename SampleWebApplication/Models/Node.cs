using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWebApplication.Models
{
    public class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; set; }
    }
}