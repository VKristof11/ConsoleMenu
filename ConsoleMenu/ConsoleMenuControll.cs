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
        public int xBefore, yBefore;
        public int longestMenu;
        public int longestElement;

        public ConsoleMenuControll(Menu main)
        {
            this.main = main;
            active = main;
            (x, y) = (0, 0);
            (xBefore, yBefore) = (0, 0);
        }

        public void UseMenu()
        {
            Console.CursorVisible = false;
            route += main.title;
            longestMenu = active.LongestMenu();
            longestElement = active.LongestElement();

            Console.WriteLine(route);
            Console.WriteLine();
            //Console.WriteLine($"X:{x} Y:{y}");
            ShowFull(active.subMenus, active.elements);

            while (true)
            {
                //Console.SetCursorPosition(0, 1);
                //Console.Write($"X:{x} Y:{y}");
                ShowUpdate(active.subMenus, active.elements, xBefore, yBefore);
                (xBefore, yBefore) = (x, y);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x > 0 && y < active.subMenus.Count)
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
                        if (x < 1 && y < active.elements.Count)
                        {
                            x++;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (x == 0)
                        {
                            if (y < active.subMenus.Count - 1)
                            {
                                y++;
                            }
                        }
                        else if (x == 1)
                        {
                            if (y < active.elements.Count - 1)
                            {
                                y++;
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (x == 0 && y < active.subMenus.Count())
                        {
                            switch (active.subMenus[y])
                            {
                                case Menu:
                                    active = (Menu)active.subMenus[y];
                                    route += $"/{active.title}";
                                    (x, y) = (0, 0);
                                    break;
                                case Back:
                                    if (!((Back)active.subMenus[y]).isExit)
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

                            longestMenu = active.LongestMenu();
                            longestElement = active.LongestElement();
                            Console.Clear();
                            Console.WriteLine(route);
                            Console.WriteLine();
                            //Console.WriteLine($"X:{x} Y:{y}");
                            ShowFull(active.subMenus, active.elements);
                        }
                        else if ( x == 1 && y < active.elements.Count )
                        {
                            switch (active.elements[y])
                            {
                                case InputField:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    (int xCursor, int yCursor) = (18, (y + (2 * y)) + 3);
                                    Console.SetCursorPosition(xCursor, yCursor);
                                    StringBuilder input;
                                    if (((InputField)active.elements[y]).defaultt)
                                    {
                                        Console.Write(((InputField)active.elements[y]).input.Replace('_', ' '));
                                        ((InputField)active.elements[y]).input = "";
                                        ((InputField)active.elements[y]).defaultt = false;
                                        input = new StringBuilder();
                                    }
                                    else
                                    {
                                        input = new StringBuilder(((InputField)active.elements[y]).input);
                                    }
                                    Console.SetCursorPosition(xCursor + input.Length, yCursor);
                                    ConsoleKeyInfo key;

                                    do
                                    {
                                        key = Console.ReadKey(intercept: true);
                                        if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                                        {
                                            input.Remove(input.Length - 1, 1);
                                            Console.Write("\b \b");
                                        }
                                        else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                                        {
                                            input.Append(key.KeyChar);
                                            Console.Write(key.KeyChar);
                                        }
                                    } while (key.Key != ConsoleKey.Enter);
                                    Console.ResetColor();
                                    if (input.ToString().Length>0)
                                    {
                                        ((InputField)active.elements[y]).input = input.ToString();
                                    }
                                    else
                                    {
                                        ((InputField)active.elements[y]).input = "__________";
                                        ((InputField)active.elements[y]).defaultt = true;
                                    }
                                    break;
                                case Button:
                                    ((Button)active.elements[y]).action?.Invoke();
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public void ShowUpdate(List<Element> subMenus, List<Element> elements, int xBefore, int yBefore)
        {
            int xCursor, yCursor;

            // ----- Befor reset -----
            if (xBefore == 0 && yBefore < subMenus.Count)
            {
                (xCursor, yCursor) = (0, yBefore + 2 + (yBefore * 2));
                Console.SetCursorPosition(xCursor, yCursor);
                DrawMenu(subMenus[yBefore].title, false);
            }
            
            //int newX = xBefore - 1;
            if (xBefore > 0 && yBefore < elements.Count)
            {
                //(xCursor, yCursor) = ((newX + (10 * newX)) + 15, (yBefore + (2 * yBefore)) + 3);
                (xCursor, yCursor) = (18, (yBefore + (2 * yBefore)) + 3);
                Console.SetCursorPosition(xCursor, yCursor);
                switch (elements[yBefore])
                {
                    case InputField:
                        Console.Write(((InputField)elements[yBefore]).input);
                        break;
                    case Button:
                        DrawButton(elements[yBefore].title, false, xCursor, yCursor);
                        break;
                }
            }
            // --------------------


            // ----- Next update -----
            if (x == 0 && y < subMenus.Count)
            {
                (xCursor, yCursor) = (0, y + 2 + (y * 2));
                Console.SetCursorPosition(xCursor, yCursor);
                DrawMenu(subMenus[y].title, true);
            }
            
            if (x > 0 && y < elements.Count)
            {
                (xCursor, yCursor) = (18, (y + (2 * y)) + 3);
                Console.SetCursorPosition(xCursor, yCursor);
                switch (elements[y])
                {
                    case InputField:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(((InputField)elements[y]).input);
                        Console.ResetColor();
                        break;
                    case Button:
                        DrawButton(elements[y].title, true, xCursor, yCursor);
                        break;
                }
            }
            // --------------------
        }

        public void ShowFull(List<Element> childrens, List<Element> elements)
        {
            for (int i = 0; i < childrens.Count(); i++)
            {
                if (x == 0 && y == i)
                {
                    DrawMenu(childrens[i].title, true);
                }
                else
                {
                    DrawMenu(childrens[i].title, false);
                }
                Console.WriteLine();
            }

            for (int i = 0; i < Console.WindowHeight-1; i++)
            {
                Console.SetCursorPosition(15, i+1);
                Console.Write('|');
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] != null)
                {
                    if (x == 1 && y == i)
                    {
                        //Console.ForegroundColor = ConsoleColor.Blue;
                        //(int xCursor, int yCursor) = ((x + (10 * x)) + 15, (i + (2 * i)) + 3);
                        (int xCursor, int yCursor) = (18, (i + (2 * i)) + 3);
                        Console.SetCursorPosition(xCursor, yCursor);
                        switch (elements[i])
                        {
                            case InputField:
                                Console.Write(((InputField)elements[i]).input);
                                break;
                            case Button:
                                DrawButton(elements[i].title, true, xCursor, yCursor);
                                break;
                        }
                    }
                    else
                    {
                        (int xCursor, int yCursor) = (18, (i + (2 * i)) + 3);
                        Console.SetCursorPosition(xCursor, yCursor);
                        switch (elements[i])
                        {
                            case InputField:
                                Console.Write(((InputField)elements[i]).input);
                                break;
                            case Button:
                                DrawButton(elements[i].title, false, xCursor, yCursor);
                                break;
                        }
                    }
                }
            }
        }

        public void DrawMenu(string text, bool selected)
        {
            if (selected)
            {
                Console.Write($"   ╔{new string('═', longestMenu + 2)}╗\n");
                Console.Write($" =>║ {new string(' ', (longestMenu - text.Length) / 2)}");
                //Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue; 
                Console.Write(text);
                Console.ResetColor();               
                /*
                if (longestWord % 2 == 0)
                {
                    if (text.Length % 2 == 0)
                    {
                        Console.Write($"{new string(' ', (longestWord - text.Length) / 2)} ║\n");
                    }
                    else
                    {
                        Console.Write($"{new string(' ', ((longestWord - text.Length) / 2) + 1)} ║\n");
                    }
                }
                else
                {
                    if (text.Length % 2 == 0)
                    {
                        Console.Write($"{new string(' ', ((longestWord - text.Length) / 2) + 1)} ║\n");
                    }
                    else
                    {
                        Console.Write($"{new string(' ', (longestWord - text.Length) / 2)} ║\n");
                    }
                }
                */
                Console.Write($"{new string(' ', 
                    longestMenu % 2 == 0 ? 
                    text.Length % 2 == 0 ? 
                      (longestMenu - text.Length) / 2 
                    : ((longestMenu - text.Length) / 2) + 1 
                    :text.Length % 2 == 0 ?
                      ((longestMenu - text.Length) / 2) + 1
                    : (longestMenu - text.Length) / 2
                    )} ║\n");
                
                Console.Write($"   ╚{new string('═', longestMenu + 2)}╝");
            }
            else
            {
                Console.Write($"   ╔{new string('═', longestMenu + 2)}╗\n");
                Console.Write($" - ║ {new string(' ', (longestMenu - text.Length) / 2)}{text}{new string(' ', longestMenu % 2 == 0 ? text.Length % 2 == 0 ? (longestMenu - text.Length) / 2 : ((longestMenu - text.Length) / 2) + 1 : text.Length % 2 == 0 ? ((longestMenu - text.Length) / 2) + 1 : (longestMenu - text.Length) / 2)} ║\n");
                Console.Write($"   ╚{new string('═', longestMenu + 2)}╝");
            }
        }

        public void DrawButton(string text, bool selected, int xCursor, int yCursor)
        {
            if (selected)
            {
                Console.SetCursorPosition(xCursor, yCursor);
                Console.Write($"╔{new string('═', longestElement + 2)}╗\n");
                Console.SetCursorPosition(xCursor, yCursor+1);
                Console.Write($"║ {new string(' ', (longestElement - text.Length) / 2)}");
                //Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(text);
                Console.ResetColor();
                /*
                if (longestWord % 2 == 0)
                {
                    if (text.Length % 2 == 0)
                    {
                        Console.Write($"{new string(' ', (longestWord - text.Length) / 2)} ║\n");
                    }
                    else
                    {
                        Console.Write($"{new string(' ', ((longestWord - text.Length) / 2) + 1)} ║\n");
                    }
                }
                else
                {
                    if (text.Length % 2 == 0)
                    {
                        Console.Write($"{new string(' ', ((longestWord - text.Length) / 2) + 1)} ║\n");
                    }
                    else
                    {
                        Console.Write($"{new string(' ', (longestWord - text.Length) / 2)} ║\n");
                    }
                }
                */
                Console.Write($"{new string(' ',
                    longestElement % 2 == 0 ?
                    text.Length % 2 == 0 ?
                      (longestElement - text.Length) / 2
                    : ((longestElement - text.Length) / 2) + 1
                    : text.Length % 2 == 0 ?
                      ((longestElement - text.Length) / 2) + 1
                    : (longestElement - text.Length) / 2
                    )} ║\n");
                Console.SetCursorPosition(xCursor, yCursor+2);
                Console.Write($"╚{new string('═', longestElement + 2)}╝");
            }
            else
            {
                Console.Write($"╔{new string('═', longestElement + 2)}╗\n");
                Console.SetCursorPosition(xCursor, yCursor + 1);
                Console.Write($"║ {new string(' ', (longestElement - text.Length) / 2)}{text}{new string(' ', longestElement % 2 == 0 ? text.Length % 2 == 0 ? (longestElement - text.Length) / 2 : ((longestElement - text.Length) / 2) + 1 : text.Length % 2 == 0 ? ((longestElement - text.Length) / 2) + 1 : (longestElement - text.Length) / 2)} ║\n");
                Console.SetCursorPosition(xCursor, yCursor + 2);
                Console.Write($"╚{new string('═', longestElement + 2)}╝");
            }
        }
    }
}