using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    static class GeneralMethods
    {
        /// <summary>
        ///     This method returns a random integer in a given range
        /// </summary>
        /// <param name="min">
        ///     leftest border of the range
        /// </param>
        /// <param name="max">
        ///     rightest border of the range
        /// </param>
        /// <returns>a random integer within the given range</returns>
        public static int rand(int min, int max)
        {
            Random rdm = new Random();
            return rdm.Next(min, max);
        }

        /// <summary>
        ///     This method gets an array of 9 bits which represent the game board's cells
        ///     where the chosen cell numbers bits are On, and it returns an array of the chosen
        ///     cell numbers (integers)
        /// </summary>
        /// <param name="Ninearr">array of 9 bits</param>
        /// <returns>array of integers - cell numbers</returns>
        public static int[] corpOnBits(bool[] Ninearr)
        {
            int[] corped;
            int j;
            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                if (Ninearr[i])
                {
                    count++;
                }
            }

            corped = new int[count];
            j = 0;
            for (int i = 0; i < 9; i++)
            {
                if (Ninearr[i])
                {
                    corped[j] = i + 1;
                    j++;
                }
            }

            return corped;
        }
    }
}
