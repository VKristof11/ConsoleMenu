using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class InputField : Element
    {
        Menu parent;
        public int id;
        public string input;
        public bool defaultt;

        public InputField(string title, int id) : base(title)
        {
            input = "__________";
            this.id = id;
            defaultt = true;
        }



    }
}
