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
            string grid = "AA00";
            int height = 15;
            int width = 15;

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
            Regex grid4 = new Regex("^(OWN|[A-R]{2}[0-9]{2}([A-X]{2})?)$");

            if (!grid4.IsMatch(center) || height < 0 || height > 20 || width < 0 || width > 20)
            {
                result.Add("Error");
                return result;
            }

            char[] chars = (center == "OWN" ? DALHeaderGrid : center).ToCharArray();

            // Map center maidenhead grid onto a continuous 180 by 180 grid
            // Maidenhead system has origin in south-west whereas a multiplier list
            // starts in upper left corner (thus "- row" below)
            int lonSq = 10 * (chars[0] - 'A') + chars[2] - '0';
            int latSq = 10 * (chars[1] - 'A') + chars[3] - '0';

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    // Calculate the current square's location in the continuous
                    // grid and convert this back to Maidenhead.
                    // Make letters wrap around from R to A and vice versa
                    // Make numbers wrap around from 9 to 0 and vice versa
                    int contVer = (wrap + latSq - row + (height - 1)/ 2) % wrap;
                    int contHor = (wrap + lonSq + col - (width - 1) / 2) % wrap;

                    // Convert location back to grid square format
                    result.Add(string.Format("{0}{1}{2}{3}",
                        (char)(contHor / 10 + 'A'), (char)(contVer / 10 + 'A'),
                        contHor % 10, contVer % 10));
                }
            }

            return result;
        }
    }
}
