using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            string Mode; // Mode - Single/Multi Player
            string XOrO; // Will the player be the X or tbe O?
            string levelOfDifficulty; // level of difficulty
            gameBordConfig config = new TicTacToe.gameBordConfig();

            Console.WriteLine("Welcome to Tic-Tac-Toe!");

            Mode = "1"; //currently, only the single player mode is available

            XOrO = "";
            if (Mode == "1")
            {
                Console.WriteLine("Please Choose An Option: 1 - I'll be the X; 2- I'll be the O");
                XOrO = Console.ReadLine();
                while (XOrO != "1" && XOrO != "2")
                {
                    Console.WriteLine("Invalid Value.. Please Choose An Option: 1 - I'll be the X; 2- I'll be the O");
                    XOrO = Console.ReadLine();
                };
            }

            levelOfDifficulty = "";
            if (Mode == "1")
            {
                Console.WriteLine("Please Choose The Difficulty Level:" + Environment.NewLine + "1 - Easy; 2 - Hard; 3 - Unbeatable;");
                levelOfDifficulty = Console.ReadLine();
                while (levelOfDifficulty != "1" && levelOfDifficulty != "2" && levelOfDifficulty != "3")
                {
                    Console.WriteLine("Invalid Value.. Please Choose The Difficulty Level:" + Environment.NewLine + "1 - Easy; 2 - Hard; 3 - Unbeatable;");
                    levelOfDifficulty = Console.ReadLine();
                };
            }

            config.gameRun(Mode, XOrO, levelOfDifficulty);

            return;

        }
    }
}
