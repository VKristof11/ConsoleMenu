using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public abstract class Element
    {
        public string title;

        protected Element(string title)
        {
            this.title = title;
        }
    }
}
