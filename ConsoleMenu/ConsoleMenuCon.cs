using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class ConsoleMenuCon
    {
        public Menu main;
        public Menu active;
        public string route = "";
        public int x, y;

        public ConsoleMenuCon(Menu main)
        {
            this.main = main;
            active = main;
            (x, y) = (0, 0);
        }

        public void UseMenu()
        {
            route += main.title;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(route);
                Console.WriteLine($"X:{x} Y:{y}");
                Show(active.childrens);


                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x > 0)
                        {
                            x--;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y > 0)
                        {
                            y--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (x < (active.elements.GetLength(1) - 1))
                        {
                            x++;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (y < (active.elements.GetLength(0) - 1))
                        {
                            y++;
                        }
                        break;
                    case ConsoleKey.Enter:

                        if (x == 0)
                        {
                            switch (active.childrens[y])
                            {
                                case Menu:
                                    active = (Menu)active.childrens[y];
                                    route += $"/{active.title}";
                                    (x, y) = (0, 0);
                                    break;
                                case Back:
                                    if (active.parent != null)
                                    {
                                        active = active.parent;
                                        route = route.Substring(0, route.LastIndexOf('/'));
                                        (x, y) = (0, 0);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (active.elements[x, y])
                            {
                                case InputField:
                                    break;

                            }
                        }
                        break;

                }


            }
        }


        public void Show(List<Element> childrens)
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
