using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TerminalArena.Classes;

namespace TerminalArena
{
    class Program
    {
        private static UserInterface _ui;
        static void Main()
        {
            Initialize();
            _ui = new UserInterface("Awesome Terminal Arena");
            int selection;
            do
            {
                selection = _ui.PrintMenu("Haupmenü","Starten", "Info", "Beenden");

                switch (selection)
                {
                    case 0:
                        Arena arena = new Arena();
                        arena.Ui = _ui;
                        arena.StartFight();
                        break;
                    case 1:
                        _ui.PrintInfo();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Auf Wiedersehen!");
                        Environment.Exit(0);
                        break;
                }
            } while (selection != 2);
        }

        static void Initialize()
        {
            if (File.Exists(@"names.json")) return;
            
            List<string> names =  new List<string>
            {
                "Seidir", "Haraikala",
                "Ywain", "Sedir",
                "Mirnhilda", "Traviynla",
                "Machalich", "Áille",
                "Uathach", "Lothur",
                "Igraine", "Setana",
                "Fiall", "Gwalchmai",
                "Firael", "Taltin",
                "Rhonabwy", "Vialigh",
                "Albhe", "Norbwyn",
                "Tuadh", "Boronwyn"
            };

            File.WriteAllText(@"names.json", JsonConvert.SerializeObject(names));
        }
    }
}
