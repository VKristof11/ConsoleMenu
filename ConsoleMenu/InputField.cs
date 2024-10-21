using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class InputField : Element
    {
        public string input;

        public InputField(string title) : base(title)
        {
            input = string.Empty;
        }
    }
}
