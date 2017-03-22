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
        public String nodeType { get; set; } //PROGRAM, DECLARATION, etc.

        public treeNode C1 { get; set; } //Pointer to Child 1
        public treeNode C2 { get; set; } //Pointer to Child 2
        public treeNode C3 { get; set; } //Pointer to Child 3
        public treeNode C4 { get; set; } //Pointer to Child 4
        public treeNode C5 { get; set; } //Pointer to Child 5
        public treeNode C6 { get; set; } //Pointer to Child 6
        
        //treeNode 1 chil
        public treeNode(string type, int line, string value)
        {
            nodeType = type;
            lineNumber = line;
            name = value;
            C1 = null;
            C2 = null;
            C3 = null;
            C4 = null;
            C5 = null;
            C6 = null;
        }
        //treeNode 1 chil
        public treeNode(string type, int line, string value, treeNode c1)
        {
            nodeType = type;
            lineNumber = line;
            name = value;
            C1 = c1;
            C2 = null;
            C3 = null;
            C4 = null;
            C5 = null;
            C6 = null;
        }
        //treeNode 2 children
        public treeNode(string type, int line, string value, treeNode c1, treeNode c2)
        {
            nodeType = type;
            lineNumber = line;
            name = value;
            C1 = c1;
            C2 = c2;
            C3 = null;
            C4 = null;
            C5 = null;
            C6 = null;
        }
        //treeNode 3 children
        public treeNode(string type, int line, string value, treeNode c1, treeNode c2, treeNode c3)
        {
            nodeType = type;
            lineNumber = line;
            name = value;
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = null;
            C5 = null;
            C6 = null;
        }
        //treeNode 4 children
        public treeNode(string type, int line, string value, treeNode c1, treeNode c2, treeNode c3, treeNode c4)
        {
            nodeType = type;
            lineNumber = line;
            name = value;
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
            C5 = null;
            C6 = null;
        }
        //treeNode 5 children
        public treeNode(string type, int line, string value, treeNode c1, treeNode c2, treeNode c3, treeNode c4, treeNode c5)
        {
            nodeType = type;
            lineNumber = line;
            name = value;
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
            C5 = c5;
            C6 = null;
        }
        //treeNode 6 children
        public treeNode(string type, int line, string value, treeNode c1, treeNode c2, treeNode c3,treeNode c4, treeNode c5, treeNode c6)
        {
            nodeType = type;
            lineNumber = line;
            name = value;
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
            C5 = c5;
            C6 = c6;
        }
    }
}
