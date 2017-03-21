using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class treeNode
    {
        public int lineNumber { get; set; } //Line in program where this construct is found
        public int nValue { get; set; } //Numerical value of a number
        public String sValue { get; set; } //Lexeme or string value of an identifier
        public String nodeType { get; set; } //PROGRAM, DECLARATION, etc.
        public int typeSpecifier { get; set; } //VOID or INT
        public String rename { get; set; } //Used by the Semantic Analyzer
        public bool visited { get; set; } //Initialized to false, used for traversals

        public treeNode C1 { get; set; } //Pointer to Child 1
        public treeNode C2 { get; set; } //Pointer to Child 2
        public treeNode C3 { get; set; } //Pointer to Child 3
        public treeNode sibling { get; set; } //Pointer to Sibling 

        //treeNode with string value
        public treeNode(string type, int line, string value, treeNode c1, treeNode c2, treeNode c3, treeNode sibling)
        {
            nodeType = type;
            lineNumber = line;
            sValue = value;
        }
    }
}
