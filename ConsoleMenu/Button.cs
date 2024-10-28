using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class Button : Element
    {
        public Action action;

        public Button(string title, Action action) : base(title)
        {
            this.action = action;
        }


    }
}
