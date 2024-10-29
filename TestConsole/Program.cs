using ConsoleMenu;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
            Console.WriteLine(
                "┌─┐\n" +
                "│ │\n" +
                "└─┘");

            Console.WriteLine();

            Console.WriteLine(DrawBox("Input"));

            Console.ReadKey();
            */



            Menu main = new Menu(null, "Main");
            ConsoleMenuControll cmc = new ConsoleMenuControll(main);

            main.AddMenu("Login", (menu) =>
            {
                menu.AddMenu("Test", null)
                .AddInput(0, 0);
            })
            .AddInput(0,0)
            .AddInput(1,0)
            .AddInput(2,0)
            .AddInput(0,1)
            .AddInput(1,1)
            .AddInput(2,1)
            .AddMenu("Option", null)
            .AddMenu("Test3", (menu) =>
            {
                menu.AddMenu("Test1", null);
                menu.AddMenu("Test2", null);
            });

            cmc.UseMenu();
        }


        static string DrawBox(string text)
        {
            return $"┌{new string('─', text.Length)}┐\n" +
                   $"│{text}│\n" +
                   $"└{new string('─', text.Length)}┘";
        }

    }
}
