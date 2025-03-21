﻿using ConsoleMenu;
using System.Text;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu main = new Menu("Main");
            ConsoleMenuControll cmc = new ConsoleMenuControll(main);

            main.AddMenu("Login", (menu) => {
                menu.AddMenu("Test", null)
                .AddInput("Title", 0, 5);
            })
            .AddMenu("Option", null)
            .AddMenu("Test3", (menu) => {
                menu.AddMenu("Test1", null)
                .AddMenu("Test2", null);
            })
            .AddInput("Title", 0, 10)
            .AddButton("Elso", (menu) => {
                cmc.WriteOne(cmc.GetInputField(0).input);
            })
            .AddInput("Title", 1, 6)
            .AddButton("Masodik", (menu) => {
                cmc.WriteOne(cmc.GetInputField(1).input);
            })
            .AddButton("Mindeketto", (menu) => {
                cmc.WriteMore(new string[] { cmc.GetInputField(0).input, cmc.GetInputField(1).input });
            });

            cmc.UseMenu();
        }
    }
}
