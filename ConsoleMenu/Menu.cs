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
        public Element[,] elements = new Element[10, 5];

        public Menu(string title) : base(title)
        {
            parent = null;
        }

        public Menu(Menu parent, string title) : base(title)
        {
            this.parent = parent;
            childrens.Add(new Back("Back"));
        }

        public Menu AddMenu(string title, Action<Menu> test)
        {
            Menu help = new Menu(this, title);
            childrens.Add(help);
            if (test != null)
            {
                test(help);
            }
            return this;
        }

        public void RemoveMenu(Menu childe)
        {
            childrens.Remove(childe);
        }

        public void Show(int y)
        {
            for (int i = 0; i < childrens.Count(); i++)
            {
                if (y == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(childrens[i].title);
                Console.ResetColor();
            }
        }
    }
}
