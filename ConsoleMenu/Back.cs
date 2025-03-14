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

        public Back(string title, int minHeight, int minWidth, bool isExit) : base(title, minHeight, minWidth)
        {
            this.isExit = isExit;
        }

        public override void CaclSize()
        {
            width = minWidth + title.Length;
            height = minHeight;
        }
    }
}
