using System;
using System.Collections.Generic;
using System.Linq;

namespace TerminalArena.Classes
{
    /// <summary>
    /// Repräsentiert die Arena für einen Kampf.
    /// </summary>
    public class Arena
    {
        /// <summary>
        /// Das Benutzerinterface
        /// </summary>
        public UserInterface Ui { get; set; }
        private const int MaxFighters = 10;

        private List<Fighter> _enemies;
        private Fighter _player;

        /// <summary>
        /// Erstellt die Arena.
        /// </summary>
        public Arena()
        {
            GenerateEnemies();
            SetupPlayer();
        }

        /// <summary>
        /// Beginnt den Kampf gegen alle Gegner in der Liste
        /// </summary>
        public void StartFight()
        {
            var choice = 0;
            while (_player.IsAlive() && choice == 0)
            {
                Ui.DrawActionBorder();
                Ui.ClearActionArea();

                Fight fight = new Fight(_enemies.First(), _player, Ui);
                fight.DoFight();
                
                Ui.ClearContentArea();
                choice = Ui.PrintMenu("Nächste Runde?", "Nächste Runde", "Beenden");
                if (choice == 1 || !_player.IsAlive())
                {
                    break;
                }

                _enemies.Remove(_enemies.First());
            }
        }

        /// <summary>
        /// Initialisiert eine Liste von Gegnern.
        /// </summary>
        private void GenerateEnemies()
        {
            _enemies = new List<Fighter>();
            for (int i = 0; i < MaxFighters; i++)
            {
                string name = Helper.GetRandomName();

                foreach (Fighter fighter in _enemies)
                {
                    if (fighter.Name == name)
                    {
                        name = Helper.GetRandomName();
                    }
                }

                Fighter enemy = new Fighter(name, 50, 1, 5);
                _enemies.Add(enemy);
            }
        }

        /// <summary>
        /// Initialisiert den Kämpfer des Spielers.
        /// </summary>
        private void SetupPlayer()
        {
            Ui.DrawTitle("Kämpfer erstellen");
            Ui.ClearContentArea();
            Ui.Print("Gib deinem Kämpfer einen Namen: ");
            string name = Console.ReadLine();
            _player = new Fighter(name, 100, 3, 10)
            {
                Actions = new List<Action>
                {
                    new Action("Trank benutzen", "potion"),
                    new Action("Angreifen", "attack"),
                    new Action("Weglaufen", "flee")
                }
            };

        }
    }
}