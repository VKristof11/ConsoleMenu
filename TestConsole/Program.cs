using ConsoleMenu;
using System.Text;

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


            Console.WriteLine("Nyomd meg az Enter-t a szerkesztési módba lépéshez...");

            // Input mező hívása
            string inputText = GetCustomInput();
            Console.WriteLine($"A beírt szöveg: {inputText}");
            Console.WriteLine("TEST VÉGE");
            Console.ReadKey();
            */




            Menu main = new Menu(null, "Main");
            ConsoleMenuControll cmc = new ConsoleMenuControll(main);

            main.AddMenu("Login", (menu) =>
            {
                menu.AddMenu("Test", null)
                .AddInput("Title", 0);
            })
            .AddMenu("Option", null)
            .AddMenu("Test3", (menu) =>
            {
                menu.AddMenu("Test1", null)
                .AddMenu("Test2", null);
            })
            .AddInput("Title", 0)
            .AddInput("Title", 1)
            .AddButton("Button", () => {
                // Mit csinál a gomb
            });

            cmc.UseMenu();
        }





        static string DrawBox(string text)
        {
            return $"┌{new string('─', text.Length)}┐\n" +
                   $"│{text}│\n" +
                   $"└{new string('─', text.Length)}┘";
        }

        static string GetCustomInput()
        {
            StringBuilder input = new StringBuilder();
            bool isEditing = false;

            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    if (isEditing)
                    {
                        // Kilépés a szerkesztési módból
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        // Belépés a szerkesztési módba
                        isEditing = true;
                        Console.Write("\nSzerkesztés: ");
                    }
                }
                else if (isEditing)
                {
                    // Szerkesztés mód - karakter hozzáadása vagy törlése
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input.Remove(input.Length - 1, 1);
                        Console.Write("\b \b"); // Backspace megjelenítése a konzolon
                    }
                    else if (key.Key != ConsoleKey.Backspace)
                    {
                        input.Append(key.KeyChar);
                        Console.Write(key.KeyChar); // Karakter megjelenítése a konzolon
                    }
                }
            }

            return input.ToString();
        }
    }
}
