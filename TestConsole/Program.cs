using ConsoleMenu;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu main = new Menu("Main");
            ConsoleMenuCon cm = new ConsoleMenuCon(main);

            main.AddMenu("Login", (menu) =>
            {
                menu.AddMenu("Test", null);
            })
            .AddMenu("Option", null)
            .AddMenu("Test3", (menu) =>
            {
                menu.AddMenu("Test1", null);
                menu.AddMenu("Test2", null);
            });

            cm.UseMenu();

        }
    }
}
