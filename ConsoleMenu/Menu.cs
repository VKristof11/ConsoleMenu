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
        public List<Element> subMenus = new List<Element>(10);
        public List<Element> elements = new List<Element>();

        public Menu(Menu parent, string title) : base(title)
        {
            this.parent = parent;
            if (parent == null)
            {
                subMenus.Add(new Back("Exit", true));
            }
            else
            {
                subMenus.Add(new Back("Back", false));
            }
        }

        public int LongestMenu()
        {
            int max = subMenus[0].title.Length;
            for (int i = 1; i < subMenus.Count; i++)
            {
                if (subMenus[i].title.Length > max)
                {
                    max = subMenus[i].title.Length;
                }
            }
            return max;
        }

        public int LongestElement()
        {
            int max = 0;
            if (elements.Count > 0)
            {
                max = elements[0].title.Length;
                for (int i = 1; i < elements.Count; i++)
                {
                    switch (elements[i])
                    {
                        case InputField:
                            if (((InputField)elements[i]).input.Length > max)
                            {
                                max = ((InputField)elements[i]).input.Length;
                            }
                            break;
                        case Button:
                            if (elements[i].title.Length > max)
                            {
                                max = elements[i].title.Length;
                            }
                            break;
                    }

                }
            }
            return max;
        }

        public Menu AddMenu(string title, Action<Menu> next)
        {
            Menu help = new Menu(this, title);
            subMenus.Add(help);
            if (next != null)
            {
                next(help);
            }
            return this;
        }

        public void RemoveMenu(Menu children)
        {
            subMenus.Remove(children);
        }

        public Menu AddInput(string title, int id)
        {
            elements.Add(new InputField(title, id));
            return this;
        }

        public Menu AddButton(string title, Action action)
        {
            elements.Add(new Button(title, action));
            return this;
        }

    }
}
