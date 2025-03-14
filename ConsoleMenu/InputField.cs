using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class InputField : Element
    {
        public Menu parent;
        public int id;
        public string holder;
        public string input;
        public bool defaultt;
        public int maxSize;

        public InputField(string title, int minHeight, int minWidth, int id, int maxSize, Menu parent) : base(title, minHeight, minWidth)
        {
            this.id = id;
            this.parent = parent;
            this.maxSize = maxSize;
            input = new string('_', maxSize);
            defaultt = true;
        }

        public override void CaclSize()
        {
            width = minWidth + Math.Max(maxSize, title.Length);
            height = minHeight;
        }
    }
}
