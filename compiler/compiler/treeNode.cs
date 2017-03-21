using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class treeNode
    {
        public int lineNumber; //Line in program where this construct is found
        public int nValue; //Numerical value of a number
        public String sValue; //Lexeme or string value of an identifier
        public String nodeType; //PROGRAM, DECLARATION, etc.
        public int typeSpecifier; //VOID or INT
        public String rename; //Used by the Semantic Analyzer
        public bool visited; //Initialized to false, used for traversals

        public treeNode C1; //Pointer to Child 1
        public treeNode C2; //Pointer to Child 2
        public treeNode C3; //Pointer to Child 3
        public treeNode sibling; //Pointer to Sibling 

        //treeNode with string value
        public treeNode(string type, int line, string value)
        {
            nodeType = type;
            lineNumber = line;
            sValue = value;
        }
    }
}
