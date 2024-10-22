using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class Menu : Element
    {
        public Menu parent;
        public List<Element> childrens = new List<Element>();
        public Element[,] elements = new Element[10, 4];

        public Menu(Menu parent, string title) : base(title)
        {
            this.parent = parent;
            if (parent == null)
            {
                childrens.Add(new Back("Exit"));
            }
            else
            {
                childrens.Add(new Back("Back"));
            }
        }

        public Menu AddMenu(string title, Action<Menu> next)
        {
            Menu help = new Menu(this, title);
            childrens.Add(help);
            if (next != null)
            {
                next(help);
            }
            return this;
        }

        public void RemoveMenu(Menu children)
        {
            childrens.Remove(children);
        }

        public Menu AddInput(int row, int col)
        {
            elements[row, col] = new InputField(null);
            return this;
        }

    }
}
