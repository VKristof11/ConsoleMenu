using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class Button : Element
    {
        public Action<Menu> action;

        public Button(string title, Action<Menu> action) : base(title)
        {
            this.action = action;
        }


    }
}
