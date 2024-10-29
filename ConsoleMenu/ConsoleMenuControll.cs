using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class ConsoleMenuControll
    {
        public Menu main;
        public Menu active;
        public string route = "";
        public int x, y;

        public ConsoleMenuControll(Menu main)
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
                Show(active.childrens, active.elements);


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

                        if (x == 0 && y<active.childrens.Count())
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
                                    else
                                    {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                        }
                        else if (active.elements[x, y] != null)
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


        public void Show(List<Element> childrens, Element[,] elements)
        {
            for (int i = 0; i < childrens.Count(); i++)
            {
                if (x == 0 && y == i)
                {
                    DrawBox(childrens[i].title, true);
                }
                else
                {
                    DrawBox(childrens[i].title, false);
                }
                Console.WriteLine();
            }
            for (int y = 0; y < elements.GetLength(1); y++)
            {
                for (int x = 0; x < elements.GetLength(0); x++)
                {
                    
                    if (elements[x, y] != null)
                    {
                        (int xCursor, int yCursor) = ((x + (10 * x)) + 15, (y + (2 * y)) + 3);
                        Console.SetCursorPosition(xCursor, yCursor);
                        Console.Write(elements[x, y].title);
                    }
                }
            }
            
        }

        public void DrawBox(string text, bool selected)
        {
            if (selected)
            {
                Console.Write($"   ╔{new string('═', text.Length)}╗\n");
                Console.Write($" =>║"); 
                //Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue; 
                Console.Write(text);
                Console.ResetColor();
                Console.Write("║\n");
                Console.Write($"   ╚{new string('═', text.Length)}╝");
            }
            else
            {
                Console.Write($"   ╔{new string('═', text.Length)}╗\n");
                Console.Write($" - ║{text}║\n");
                Console.Write($"   ╚{new string('═', text.Length)}╝");
            }
        }
    }
}
