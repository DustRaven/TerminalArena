using System.Collections.Generic;
using System.Threading;
using TerminalArena.Helpers;
using TerminalArena.Interfaces;

namespace TerminalArena.Classes
{
    /// <summary>
    /// Diese Klasse repräsentiert einen Kampf.
    /// </summary>
    public class Fight
    {
        private readonly Fighter _enemy;
        private readonly Fighter _player;
        private readonly UserInterface _ui;
        private readonly Dice _dice;

        /// <summary>
        /// Initialisiert einen Kampf zwischen zwei Kämpfern.
        /// </summary>
        /// <param name="enemy">Der Gegner</param>
        /// <param name="player">Der Spieler</param>
        /// <param name="ui">Eine Instanz des Interface</param>
        public Fight(Fighter enemy, Fighter player, UserInterface ui)
        {
            _enemy = enemy;
            _player = player;
            _dice = new Dice(2);
            _ui = ui;
        }

        private void PrintFighter(Fighter fighter)
        {
            _ui.PrintLine(fighter.ToString()); 
            _ui.PrintLine($"LP: {fighter.HealthBar()}");
            _ui.PrintLine();
        }

        private void PrintMessage(string message)
        {
            _ui.PrintLine(message);
            Thread.Sleep(500);
        }

        /// <summary>
        /// Führt den Kampf aus, bis einer der Kämpfer stirbt.
        /// </summary>
        public void DoFight()
        {
            Fighter first = _player;
            Fighter second = _enemy;

            _ui.DrawTitle($"{_player} vs {_enemy}");

            bool secondGoesFirst = _dice.Roll() <= _dice.GetSides() / 2;
            if (secondGoesFirst)
            {
                first = _enemy;
                second = _player;
            }

            while (first.IsAlive() && second.IsAlive())
            {
                string action;
                if (first.Actions.Count > 0)
                {
                    action = _ui.PrintActions(first.Actions);
                    first.DoAction(action);
                }
                _ui.ClearContentArea();
                first.Attack(second);
                PrintFighter(first);
                PrintFighter(second);
                PrintMessage(first.Message);
                PrintMessage(second.Message);

                if (second.IsAlive())
                {
                    if (second.Actions.Count > 0)
                    {
                        action = _ui.PrintActions(second.Actions);
                        second.DoAction(action);
                    }
                    _ui.ClearContentArea();
                    second.Attack(first);
                    PrintFighter(first);
                    PrintFighter(second);
                    PrintMessage(second.Message);
                    PrintMessage(first.Message);
                }
            }
        }
    }
}
