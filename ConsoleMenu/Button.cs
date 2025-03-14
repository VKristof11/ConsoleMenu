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

        public Button(string title, int minHeight, int minWidth, Action<Menu> action) : base(title, minHeight, minWidth)
        {
            this.action = action;
        }

        public override void CaclSize()
        {
            width = minWidth + title.Length;
            height = minHeight;
        }
    }
}
