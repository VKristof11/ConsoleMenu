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
        public int maxMenuWidth;
        public int maxElementWidth;

        public Menu(Menu parent, string title) : base(title)
        {
            this.parent = parent;
            if (parent == null)
            {
                subMenus.Add(new Back("Exit", 3, 4, true));
            }
            else
            {
                subMenus.Add(new Back("Back", 3, 4, false));
            }
        }

        public Menu(string title) : base(title)
        {
            parent = null;
            subMenus.Add(new Back("Exit", 3, 4, true));
        }

        private void MaxMenuWidth()
        {
            if (subMenus.Count > 0)
            {
                int max = subMenus[0].width;
                for (int i = 1; i < subMenus.Count; i++)
                {
                    if (subMenus[i].width > max)
                    {
                        max = subMenus[i].width;
                    }
                }
                maxMenuWidth = max;
            }
        }

        private void MaxElementWidth()
        {
            if (elements.Count > 0)
            {
                int max = elements[0].width;
                for (int i = 1; i < elements.Count; i++)
                {
                    if (elements[i].width > max)
                    {
                        max = elements[i].width;
                    }
                }
                maxElementWidth = max;
            }
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

        public Menu AddInput(string title, int id, int maxSize)
        {
            elements.Add(new InputField(title, 3, 4, id, maxSize, this));
            return this;
        }

        public Menu AddButton(string title, Action<Menu> action)
        {
            elements.Add(new Button(title, 3, 4, action));
            return this;
        }

        public override void CaclSize()
        {
            width = minWidth + title.Length;
            height = minHeight;
            MaxElementWidth();
            MaxMenuWidth();
        }
    }
}
