using ConsoleMenu;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

<<<<<<< HEAD
=======
            /*
            Console.WriteLine(
                "┌─┐\n" +
                "│ │\n" +
                "└─┘");

            Console.WriteLine();

            Console.WriteLine(DrawBox("Input"));

            Console.ReadKey();
            */



>>>>>>> cc0ad8dc019ca68fef9b328dc186ab20c630cd8d
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
<<<<<<< HEAD
                menu.AddMenu("Test1", null)
                .AddMenu("Test2", null);
            })
            .AddInput("Title", 0)
            .AddButton("Elso", (menu) =>
            {
                cmc.WriteOne(cmc.GetInputField(0).input);
            })
            .AddInput("Title", 1)
            .AddButton("Masodik", (menu) =>
            {
                cmc.WriteOne(cmc.GetInputField(1).input);
            })
            .AddButton("Mindeketto", (menu) =>
            {
                cmc.WriteMore(new string[] { cmc.GetInputField(0).input, cmc.GetInputField(1).input });
=======
                menu.AddMenu("Test1", null);
                menu.AddMenu("Test2", null);
>>>>>>> cc0ad8dc019ca68fef9b328dc186ab20c630cd8d
            });

            cmc.UseMenu();
        }
<<<<<<< HEAD
=======


        static string DrawBox(string text)
        {
            return $"┌{new string('─', text.Length)}┐\n" +
                   $"│{text}│\n" +
                   $"└{new string('─', text.Length)}┘";
        }

>>>>>>> cc0ad8dc019ca68fef9b328dc186ab20c630cd8d
    }
}
