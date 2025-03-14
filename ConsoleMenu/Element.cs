using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public abstract class Element
    {
        public string title;
        public int minHeight;
        public int minWidth;
        public int height;
        public int width;

        protected Element(string title, int minHeight, int minWidth)
        {
            this.title = title;
            this.minHeight = minHeight;
            this.minWidth = minWidth;
            CaclSize();
        }
        protected Element(string title)
        {
            this.title = title;
            minHeight = 3;
            minWidth = 4;
            CaclSize();
        }

        public abstract void CaclSize();

    }
}
