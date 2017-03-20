using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class token
    {
        public string type { get; set; }
        public string value { get; set; }
        public int line { get; set; }
        public token(string valueString, int lineNumber)
        {
            value=valueString;
            type = "";
            line = lineNumber;
        }
    }
}
