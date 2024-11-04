using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class Back : Element
    {
        public bool isExit;
        public Back(string title, bool isExit) : base(title)
        {
            this.isExit = isExit;
        }
    }
}
