using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace gridmatrix
{
    class Program
    {
        const string DALHeaderGrid = "JO65MR";

        static void Main(string[] args)
        {
            string grid = "AA00AA";
            int height = 11;
            int width = 11;

            // First element is upper left corner, last element is lower right corner
            List<string> gridlist = GetList_GridArray(grid, height, width);
            int count = 0;
            foreach (string square in gridlist)
            {
                Console.Write(square + (++count % width != 0 ? " " : "\n"));
            }
        }

        static List<string> GetList_GridArray(string center, int height, int width)
        {
            const int wrap = 10 * ('R' + 1 - 'A');
            List<string> result = new List<string>();
            Regex validation = new Regex("^(OWNGRID|[A-R]{2}[0-9]{2}([A-X]{2})?)$");

            if (!validation.IsMatch(center) || height < 0 || height > 20 || width < 0 || width > 20)
            {
                result.Add("Error");
                return result;
            }

            char[] gridSquare = (center == "OWNGRID" ? DALHeaderGrid : center).ToCharArray();

            // Map center maidenhead grid onto a continuous 180 by 180 grid
            // Maidenhead system has origin in south-west whereas a multiplier list
            // starts in upper left corner (thus "- row" below)
            int centCol = 10 * (gridSquare[0] - 'A') + gridSquare[2] - '0';
            int centRow = 10 * (gridSquare[1] - 'A') + gridSquare[3] - '0';

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    // Calculate the [row,col] square's coordinates in the continuous
                    // grid and then convert them back to the corresponding Maidenhead grid.
                    // Make letters wrap around from R to A and vice versa
                    // Make numbers wrap around from 9 to 0 and vice versa
                    int thisRow = (centRow - (row - (height - 1)/ 2) + wrap) % wrap;
                    int thisCol = (centCol + (col - (width - 1) / 2) + wrap) % wrap;

                    // Convert location back to grid square format
                    result.Add(string.Format("{0}{1}{2}{3}",
                        (char)(thisCol / 10 + 'A'), (char)(thisRow / 10 + 'A'),
                        thisCol % 10, thisRow % 10));
                }
            }

            return result;
        }
    }
}
