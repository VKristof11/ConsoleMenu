using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleMenu
{
    public class ConsoleMenuControll
    {
        private Menu main;
        private Menu active;
        private string route = "";
        private int x, y;
        private int xBefore, yBefore;
        private int longestMenu;
        private int longestElement;

        private int extraWidth = 100;
        private int extraHeight = 15;
        private string eraser = " ";

        private int elementsXOffset = 17;

        public ConsoleMenuControll(Menu main)
        {
            this.main = main;
            active = main;
            (x, y) = (0, 0);
            (xBefore, yBefore) = (0, 0);
        }

        public void UseMenu()
        {
            Setup();
            ShowFull(active.subMenus, active.elements);

            while (true)
            {
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
                            elementsXOffset = 10 + longestMenu;
                            ShowFull(active.subMenus, active.elements);
                        }
                        else if (x == 1 && y < active.elements.Count)
                        {
                            switch (active.elements[y])
                            {
                                case InputField:
                                    Console.CursorVisible = true;
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    (int xCursor, int yCursor) = (elementsXOffset, (y + (2 * y)) + 3);
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
                                    if (input.ToString().Length > 0)
                                    {
                                        ((InputField)active.elements[y]).input = input.ToString();
                                    }
                                    else
                                    {
                                        ((InputField)active.elements[y]).input = "__________";
                                        ((InputField)active.elements[y]).defaultt = true;
                                    }
                                    Console.CursorVisible = false;
                                    break;
                                case Button:
                                    ((Button)active.elements[y]).action(active);
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void Setup()
        {
            Console.CursorVisible = false;
            route = main.title;
            longestMenu = active.LongestMenu();
            longestElement = active.LongestElement();
            elementsXOffset = 10 + longestMenu;
            SetWindowSize();
            eraser = new string(' ', Console.WindowWidth);
        }

        private void SetWindowSize()
        {
            Console.WindowWidth = longestMenu + longestElement + extraWidth;
            Console.BufferWidth = longestMenu + longestElement + extraWidth;
            Console.WindowHeight = Math.Max(active.subMenus.Count, active.elements.Count) * 3 + 2 + extraHeight;
            Console.BufferHeight = Math.Max(active.subMenus.Count, active.elements.Count) * 3 + 2 + extraHeight;
        }

        public void WriteOne(string text)
        {
            Console.SetCursorPosition(0, Math.Max(active.subMenus.Count, active.elements.Count) * 3 + 3);
            for (int i = 0; i < extraHeight - 2; i++)
            {
                Console.WriteLine(eraser);
            }
            Console.SetCursorPosition(0, Math.Max(active.subMenus.Count, active.elements.Count) * 3 + 3);
            Console.Write(text);
        }

        public void WriteMore(string[] texts)
        {
            Console.SetCursorPosition(0, Math.Max(active.subMenus.Count, active.elements.Count) * 3 + 3);
            for (int i = 0; i < extraHeight - 2; i++)
            {
                Console.WriteLine(eraser);
            }

            for (int i = 0; i < texts.Length; i++)
            {
                Console.SetCursorPosition(0, i + Math.Max(active.subMenus.Count, active.elements.Count) * 3 + 3);
                Console.Write(texts[i]);
            }
        }

        public InputField GetInputField(int id)
        {
            for (int i = 0; i < active.elements.Count; i++)
            {
                if (active.elements[i] is InputField)
                {
                    if (((InputField)active.elements[i]).id == id)
                    {
                        return (InputField)active.elements[i];
                    }
                }
            }
            return null;
        }

        private void ShowFull(List<Element> subMenus, List<Element> elements)
        {
            Console.Clear();
            SetWindowSize();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(route);
            Console.ResetColor();

            Console.SetCursorPosition(0, 1);
            Console.Write($"{new string('─', longestMenu + 8)}┬{new string('─', longestElement + 6)}┬{new string('─', Console.WindowWidth - (longestMenu + 8 + longestElement + 8) - 1)}");
            for (int i = 2; i < Console.WindowHeight; i++) //Math.Max(subMenus.Count, elements.Count)* 3 + 2
            {
                Console.SetCursorPosition(longestMenu + 8, i);
                Console.Write('│');
                Console.SetCursorPosition(longestMenu + longestElement + 15, i);
                Console.Write('│');
            }
            //Console.SetCursorPosition(0, Math.Max(subMenus.Count, elements.Count) * 3 + 2);
            //Console.Write($"{new string('─', longestMenu + 8)}┴{new string('─', Console.WindowWidth - (longestMenu + 8)-1)}");

            Console.SetCursorPosition(0, 2);
            for (int i = 0; i < subMenus.Count(); i++)
            {
                if (x == 0 && y == i)
                {
                    DrawMenu(subMenus[i].title, true);
                }
                else
                {
                    DrawMenu(subMenus[i].title, false);
                }
                Console.WriteLine();
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] != null)
                {
                    if (x == 1 && y == i)
                    {
                        (int xCursor, int yCursor) = (elementsXOffset, (i + (2 * i)) + 2);
                        Console.SetCursorPosition(xCursor, yCursor);
                        switch (elements[i])
                        {
                            case InputField:
                                DrawInput((InputField)elements[i], true, xCursor, yCursor);
                                break;
                            case Button:
                                DrawButton(elements[i].title, true, xCursor, yCursor);
                                break;
                        }
                    }
                    else
                    {
                        (int xCursor, int yCursor) = (elementsXOffset, (i + (2 * i)) + 2);
                        Console.SetCursorPosition(xCursor, yCursor);
                        switch (elements[i])
                        {
                            case InputField:
                                DrawInput((InputField)elements[i], false, xCursor, yCursor);
                                break;
                            case Button:
                                DrawButton(elements[i].title, false, xCursor, yCursor);
                                break;
                        }
                    }
                }
            }
        }

        private void ShowUpdate(List<Element> subMenus, List<Element> elements, int xBefore, int yBefore)
        {
            int xCursor, yCursor;

            // ----- Befor reset -----
            if (xBefore == 0 && yBefore < subMenus.Count)
            {
                (xCursor, yCursor) = (0, yBefore + 2 + (yBefore * 2));
                Console.SetCursorPosition(xCursor, yCursor);
                DrawMenu(subMenus[yBefore].title, false);
            }

            if (xBefore > 0 && yBefore < elements.Count)
            {
                (xCursor, yCursor) = (elementsXOffset, (yBefore + (2 * yBefore)) + 2);
                Console.SetCursorPosition(xCursor, yCursor);
                switch (elements[yBefore])
                {
                    case InputField:
                        DrawInput((InputField)elements[yBefore], false, xCursor, yCursor);
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
                (xCursor, yCursor) = (elementsXOffset, (y + (2 * y)) + 2);
                Console.SetCursorPosition(xCursor, yCursor);
                switch (elements[y])
                {
                    case InputField:
                        DrawInput((InputField)elements[y], true, xCursor, yCursor);
                        break;
                    case Button:
                        DrawButton(elements[y].title, true, xCursor, yCursor);
                        break;
                }
            }
            // --------------------
        }

        private void DrawMenu(string text, bool selected)
        {
            if (selected)
            {
                Console.Write($"   ╔{new string('═', longestMenu + 2)}╗\n");
                Console.Write($" =>║ {new string(' ', (longestMenu - text.Length) / 2)}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(text);
                Console.ResetColor();
                Console.Write($"{new string(' ', longestMenu % 2 == 0 ? text.Length % 2 == 0 ? (longestMenu - text.Length) / 2 : ((longestMenu - text.Length) / 2) + 1 : text.Length % 2 == 0 ? ((longestMenu - text.Length) / 2) + 1 : (longestMenu - text.Length) / 2)} ║\n");
                Console.Write($"   ╚{new string('═', longestMenu + 2)}╝");
            }
            else
            {
                Console.Write($"   ╔{new string('═', longestMenu + 2)}╗\n");
                Console.Write($" - ║ {new string(' ', (longestMenu - text.Length) / 2)}{text}{new string(' ', longestMenu % 2 == 0 ? text.Length % 2 == 0 ? (longestMenu - text.Length) / 2 : ((longestMenu - text.Length) / 2) + 1 : text.Length % 2 == 0 ? ((longestMenu - text.Length) / 2) + 1 : (longestMenu - text.Length) / 2)} ║\n");
                Console.Write($"   ╚{new string('═', longestMenu + 2)}╝");
            }
        }

        private void DrawButton(string text, bool selected, int xCursor, int yCursor)
        {
            if (selected)
            {
                Console.SetCursorPosition(xCursor, yCursor);
                Console.Write($"╔{new string('═', longestElement + 2)}╗\n");
                Console.SetCursorPosition(xCursor, yCursor + 1);
                Console.Write($"║ {new string(' ', (longestElement - text.Length) / 2)}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(text);
                Console.ResetColor();
                Console.Write($"{new string(' ',
                    longestElement % 2 == 0 ?
                    text.Length % 2 == 0 ?
                      (longestElement - text.Length) / 2
                    : ((longestElement - text.Length) / 2) + 1
                    : text.Length % 2 == 0 ?
                      ((longestElement - text.Length) / 2) + 1
                    : (longestElement - text.Length) / 2
                    )} ║\n");
                Console.SetCursorPosition(xCursor, yCursor + 2);
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

        private void DrawInput(InputField input, bool selected, int xCursor, int yCursor)
        {
            if (selected)
            {
                Console.SetCursorPosition(xCursor, yCursor);
                Console.Write($"{input.title}:");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(xCursor, yCursor + 1);
                Console.Write($"{input.input}");
                Console.ResetColor();
            }
            else
            {
                Console.SetCursorPosition(xCursor, yCursor);
                Console.Write($"{input.title}:");
                Console.SetCursorPosition(xCursor, yCursor + 1);
                Console.Write($"{input.input}");
            }
        }
    }