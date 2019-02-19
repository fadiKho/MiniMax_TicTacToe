using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// gameBoardConfig - represents the class of game statuses (configs)
    /// </summary>
    class gameBordConfig
    {
        /// <summary>
        /// XTurn - is it X-Player's turn?
        /// </summary>
        public bool XTurn { get; set; }

        /// <summary>
        /// Board - the current board config
        /// </summary>
        public string[,] Board = new string[3, 3];

        /// <summary>
        /// gameBoardConfig Constructor
        /// </summary>
        /// <param name="game">
        ///     a game to copy
        /// </param>
        public gameBordConfig(gameBordConfig game)
        {
            this.XTurn = game.XTurn;
            string[,] b = new string[3, 3];
            b = copyNineBoard(game.Board);
            this.Board = b;
        }

        /// <summary>
        /// gameBoardConfig Constructor
        /// </summary>
        /// <param name="XT">
        ///     a value for the XTurn attribute
        /// </param>
        /// <param name="brd">
        ///     a board to copy for the Board attribute
        /// </param>
        public gameBordConfig(bool XT, string[,] brd)
        {
            this.XTurn = XT;
            string[,] b = new string[3, 3];
            b = copyNineBoard(brd);
            this.Board = b;
        }

        /// <summary>
        ///     gameBoardConfig Constructor
        /// </summary>
        public gameBordConfig() { }

        /// <summary>
        ///     runs a TicTacToe game
        /// </summary>
        /// <param name="Mode">
        ///     single or multiplayer mode
        /// </param>
        /// <param name="XOrO">
        ///     if single player, will the player be X or O
        /// </param>
        public void gameRun(string Mode, string XOrO, string levelOfDiff)
        {
            bool gameOver;
            string tempSelection;
            bool[] best = new bool[9];
            bool[] empty = new bool[9];

            this.XTurn = true;
            gameOver = false;
            while (!gameOver)
            {
                if (Mode == "1")
                {
                    if ((this.XTurn && XOrO == "1") || (!this.XTurn && !(XOrO == "1")))
                    {
                        Console.WriteLine("____________________________________________________");
                        Console.WriteLine("Please select a cell in the grid for your next step");
                        Console.WriteLine(this.getBoardTxt());
                        Console.WriteLine("____________________________________________________");
                        tempSelection = Console.ReadLine();
                        while (!(this.addStep(this.XTurn, Convert.ToInt16((tempSelection)))))
                        {
                            Console.WriteLine("The Cell Chosen is not empty!");
                            tempSelection = Console.ReadLine();
                        }
                    }
                    else
                    {
                        best = this.MiniMax();
                        empty = this.getEmptyCells();
                        this.addStep(this.XTurn, selectCell(best, empty, levelOfDiff));
                    }
                }
                else
                {
                    Console.WriteLine("The multiplayer option is unavailable!");
                    return;
                }

                if (this.getResult() != 0)
                {
                    if (this.getResult() == 1)
                    {
                        Console.WriteLine(this.getBoardTxt());
                        Console.WriteLine("X Wins!");
                    }
                    else if (this.getResult() == 2)
                    {
                        Console.WriteLine(this.getBoardTxt());
                        Console.WriteLine("O Wins!");
                    }
                    else
                    {
                        Console.WriteLine(this.getBoardTxt());
                        Console.WriteLine("It's A Draw!");
                    }

                    gameOver = true;

                }

                this.XTurn = !(this.XTurn);
            }

            Console.WriteLine("To Exit, Please Press Enter");
            Console.ReadLine();
        }

        /// <summary>
        ///     plays one step for the current player, in the chosen cell
        /// </summary>
        /// <param name="XTurn">
        ///     is it X turn?
        /// </param>
        /// <param name="cellNum">
        ///     the chosen cell in the board
        /// </param>
        /// <returns>True if the step is legal, False otherwise</returns>
        private bool addStep(bool XTurn, int cellNum)
        {
            int col; int row;
            string player;

            col = (((cellNum % 3) + 2) % 3);
            row = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(cellNum) / 3) - 1);

            if (!(string.IsNullOrEmpty(this.Board[row, col])))
            {
                return false;
            }

            if (XTurn)
            {
                player = "X";
            }
            else
            {
                player = "O";
            }

            this.Board[row, col] = player;

            return true;
        }

        /// <summary>
        ///     get the current board as a string for printing
        /// </summary>
        /// <returns>the current board as a string for printing</returns>
        private string getBoardTxt()
        {
            string tempBoard = "";
            string tempCell;
            tempBoard = tempBoard + "---------------------" + Environment.NewLine;

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(this.Board[i, j]))
                    {
                        tempCell = " ";
                    }
                    else
                    {
                        tempCell = this.Board[i, j];
                    }
                    tempBoard = tempBoard + "|  " + tempCell + "  |";
                }


                tempBoard = tempBoard + Environment.NewLine + "---------------------" + Environment.NewLine;

            }

            return tempBoard;
        }

        /// <summary>
        ///     check if the current board is final: a player winned, or a draw
        /// </summary>
        /// <returns>
        ///     0 - if the board if not final
        ///     1 - if X winned
        ///     2 - if O winned
        ///     3 - if there's a draw
        /// </returns>
        private int getResult()
        {
            for (int i = 0; i < 3; i++)
            {
                if (this.Board[i, 0] == "X" && this.Board[i, 1] == "X" && this.Board[i, 2] == "X")
                {
                    return 1;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (this.Board[i, 0] == "O" && this.Board[i, 1] == "O" && this.Board[i, 2] == "O")
                {
                    return 2;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (this.Board[0, i] == "X" && this.Board[1, i] == "X" && this.Board[2, i] == "X")
                {
                    return 1;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (this.Board[0, i] == "O" && this.Board[1, i] == "O" && this.Board[2, i] == "O")
                {
                    return 2;
                }
            }

            if ((this.Board[0, 0] == "X" && this.Board[1, 1] == "X" && this.Board[2, 2] == "X") || (this.Board[0, 2] == "X" && this.Board[1, 1] == "X" && this.Board[2, 0] == "X"))
            {
                return 1;
            }

            if ((this.Board[0, 0] == "O" && this.Board[1, 1] == "O" && this.Board[2, 2] == "O") || (this.Board[0, 2] == "O" && this.Board[1, 1] == "O" && this.Board[2, 0] == "O"))
            {
                return 2;
            }

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(this.Board[i, j]))
                    {
                        return 0;
                    }
                }

            return 3;

        }

        /// <summary>
        ///     get the empty cell numbers as array of bits
        /// </summary>
        /// <returns>empty cell numbers as array of bits</returns>
        private bool[] getEmptyCells()
        {
            bool[] arr = new bool[9];         
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arr[((i * 3) + (j) + 1) - 1] = string.IsNullOrEmpty(this.Board[i, j]);
                }
            }

            return arr;
        }

        /// <summary>
        ///     runs Minimax algorithm to get the optimal cell to choose
        /// </summary>
        /// <returns>the optimal cell numbers as array of bits</returns>
        private bool[] MiniMax()
        {
            int res;
            gameBordConfig temp;
            bool[] best = new bool[9];

            if (this.XTurn)
            {
                res = MMax(this);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrEmpty(this.Board[i, j]))
                        {
                            temp = new gameBordConfig(XTurn, this.Board);
                            temp.addStep(XTurn, ((i * 3) + (j) + 1));
                            temp.XTurn = !(temp.XTurn);
                            if (MMin(temp) == res)
                            {
                                best[((i * 3) + (j) + 1) - 1] = true;
                            }
                            else
                            {
                                best[((i * 3) + (j) + 1) - 1] = false;
                            }
                        }
                    }

                return best;

            }
            else
            {
                res = MMin(this);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrEmpty(this.Board[i, j]))
                        {
                            temp = new gameBordConfig(XTurn, this.Board);
                            temp.addStep(XTurn, ((i * 3) + (j) + 1));
                            temp.XTurn = !(temp.XTurn);
                            if (MMax(temp) == res)
                            {
                                best[((i * 3) + (j) + 1) - 1] = true;
                            }
                            else
                            {
                                best[((i * 3) + (j) + 1) - 1] = false;
                            }
                        }
                    }

                return best;
            }
        }

        /// <summary>
        ///     a private method of the MiniMax method. computes the best step for a Max player (X player in our case)
        /// </summary>
        /// <param name="b">
        ///     current game config
        /// </param>
        /// <returns>the optimal cell number</returns>
        private int MMax(gameBordConfig b)
        {
            int tempRes;

            if (b.getResult() > 0)
            {
                if (b.getResult() == 1)
                {
                    return 10;
                }
                else if (b.getResult() == 2)
                {
                    return -10;
                }
                else
                {
                    return 0;
                }
            }

            tempRes = -100;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(b.Board[i, j]))
                    {
                        gameBordConfig b1 = new gameBordConfig(b);
                        b1.addStep(b1.XTurn, ((i * 3) + (j) + 1));
                        b1.XTurn = !(b1.XTurn);
                        tempRes = Math.Max(tempRes, MMin(b1));
                    }
                }

            return tempRes;
        }

        /// <summary>
        ///     a private method of the MiniMax method. computes the best step for a Min player (O player in our case)
        /// </summary>
        /// <param name="b">
        ///     current game config
        /// </param>
        /// <returns>the optimal cell number</returns>
        private int MMin(gameBordConfig b)
        {
            int tempRes;

            if (b.getResult() > 0)
            {
                if (b.getResult() == 1)
                {
                    return 10;
                }
                else if (b.getResult() == 2)
                {
                    return -10;
                }
                else
                {
                    return 0;
                }
            }

            tempRes = 100;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(b.Board[i, j]))
                    {
                        gameBordConfig b1 = new gameBordConfig(b);
                        b1.addStep(b1.XTurn, ((i * 3) + (j) + 1));
                        b1.XTurn = !(b1.XTurn);
                        tempRes = Math.Min(tempRes, MMax(b1));
                    }
                }

            return tempRes;
        }

        /// <summary>
        /// copy a board - create a copy
        /// </summary>
        /// <param name="brd">
        ///     the board to be copied
        /// </param>
        /// <returns>the new copy of the given board</returns>
        private string[,] copyNineBoard(string[,] brd)
        {
            string[,] newBoard = new string[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    newBoard[i, j] = brd[i, j];
                }

            return newBoard;
        }

        /// <summary>
        ///     this method selects an empty cell for
        /// </summary>
        /// <param name="best">
        ///     array of bits, in which the bits who represent the best places are On
        /// </param>
        /// <param name="empty">
        ///     array of bits, in which the bits who represent the empty places are On
        /// </param>
        /// <param name="levelOfDiff">
        ///         level of difficulty as string
        /// </param>
        /// <returns>number of the cell as int</returns>
        private int selectCell (bool[] best, bool[] empty, string levelOfDiff)
        {
            int[] corpBest;
            int bestCell; int emptyCell;

            switch (levelOfDiff)
            {
                // easy
                case "1":
                    {
                        corpBest = GeneralMethods.corpOnBits(empty);
                        return corpBest[GeneralMethods.rand(0, corpBest.Length - 1)];
                    }

                // hard
                case "2":
                    {
                        bestCell = -1;
                        emptyCell = -1;
                        for (int i = 0; i < 9; i++)
                        {
                            if (best[i])
                                bestCell = i + 1;
                        }

                        emptyCell = bestCell;
                        for (int i = 0; i < 9; i++)
                        {
                            if (empty[i] && !best[i])
                                emptyCell = i + 1;
                        }

                        if (GeneralMethods.rand(0, 1) == 0)
                            return emptyCell;
                        else
                            return bestCell;
                    }

                // unbeatable
                case "3":
                    {
                        corpBest = GeneralMethods.corpOnBits(best);
                        return corpBest[GeneralMethods.rand(0, corpBest.Length - 1)];
                    }

                default:
                    {
                        return -1;
                    }
            }
        }
    }
}
