using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class treeNode
    {
        public String name { get; set; }//namespace
        public int lineNumber { get; set; } //Line in program where this construct is found
        public int nValue { get; set; } //Numerical value of a number
        public String nodeType { get; set; } //PROGRAM, DECLARATION, etc.

        public treeNode C1 { get; set; } //Pointer to Child 1
        public treeNode C2 { get; set; } //Pointer to Child 2
        public treeNode C3 { get; set; } //Pointer to Child 3

        //treeNode with string value
        public treeNode(string type, int line, string value, treeNode c1, treeNode c2, treeNode c3)
        {
            nodeType = type;
            lineNumber = line;
            name = value;
            C1 = c1;
            C2 = c2;
            C3 = c3;
        }
    }
}
