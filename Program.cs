using System;
using System.Collections.Generic;

namespace gridmatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            string grid = "AA00MR";
            int height = 11;
            int width = 11;

            // 0,0 is upper left corner, height-1,width-1 is lower right corner
            Console.WriteLine("Matrix:");
            string[,] matrix = GridMatrix(grid, height, width);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width - 1; col++)
                    Console.Write(matrix[row, col] + " ");
                Console.WriteLine(matrix[row, width - 1]);
            }

            // 0 is upper left corner, height*width-1 is lower right corner
            Console.WriteLine("Array:");
            string[] array = GridArray(grid, height, width);
            for (int index = 0; index < height * width; index++)
            {
                if ((index + 1) % width != 0)
                    Console.Write(array[index] + " ");
                else
                    Console.WriteLine(array[index]);
            }

            // First element is upper left corner, last element is lower right corner
            Console.WriteLine("List:");
            List<string> gridlist = GetList_GridArray(grid, height, width);
            int count = 1;
            foreach (string square in gridlist)
            {
                if (count++ % width != 0)
                    Console.Write(square + " ");
                else
                    Console.WriteLine(square);
            }
        }

        static private string[,] GridMatrix(string square, int height, int width)
        {
            string[,] result = new string[height, width];

            // Map center maidenhead grid onto a continuous 150 by 150 grid
            // Maidenhead system has origin in south-west
            // The created matrix has 0,0 in upper left corner
            int lonsq = 10 * (square.Substring(0, 1)[0] - 'A') + int.Parse(square.Substring(2, 1));
            int latsq = 10 * (square.Substring(1, 1)[0] - 'A') + int.Parse(square.Substring(3, 1));

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    // Calculate the current square's location in the continuous grid 
                    // Make letter wrap around from R to A and vice versa
                    // Make number wrap around from 9 to 0 and vice versa
                    int wrap = 10 * ('S' - 'A');
                    int contVer = (wrap + latsq - (row - (height - 1) / 2)) % wrap;
                    int contHor = (wrap + lonsq + (col - (width - 1) / 2)) % wrap;

                    // Convert location back to grid square format
                    result[row, col] = string.Format("{0}{1}{2}{3}",
                        (char)(contHor / 10 + 'A'),
                        (char)(contVer / 10 + 'A'),
                        contHor % 10,
                        contVer % 10);
                }
            }

            return result;
        }

        static private string[] GridArray(string square, int height, int width)
        {
            string[] result = new string[height * width];

            // Map center maidenhead grid onto a continuous 150 by 150 grid
            // Maidenhead system has origin in south-west
            // The created matrix has 0,0 in upper left corner
            int lonsq = 10 * (square.Substring(0, 1)[0] - 'A') + int.Parse(square.Substring(2, 1));
            int latsq = 10 * (square.Substring(1, 1)[0] - 'A') + int.Parse(square.Substring(3, 1));

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    // Calculate the current square's location in the continuous grid 
                    // Make letter wrap around from R to A and vice versa
                    // Make number wrap around from 9 to 0 and vice versa
                    int wrap = 10 * ('S' - 'A');
                    int contVer = (wrap + latsq - (row - (height - 1) / 2)) % wrap;
                    int contHor = (wrap + lonsq + (col - (width - 1) / 2)) % wrap;

                    // Convert location back to grid square format
                    result[row + col * width] = string.Format("{0}{1}{2}{3}",
                        (char)(contHor / 10 + 'A'),
                        (char)(contVer / 10 + 'A'),
                        contHor % 10,
                        contVer % 10);
                }
            }

            return result;
        }

        static List<string> GetList_GridArray(string square, int height, int width)
        {
            List<string> result = new List<string>();

            // Map center maidenhead grid onto a continuous 150 by 150 grid
            // Maidenhead system has origin in south-west
            // The created matrix has 0,0 in upper left corner
            int lonsq = 10 * (square.Substring(0, 1)[0] - 'A') + int.Parse(square.Substring(2, 1));
            int latsq = 10 * (square.Substring(1, 1)[0] - 'A') + int.Parse(square.Substring(3, 1));

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    // Calculate the current square's location in the continuous grid 
                    // Make letter wrap around from R to A and vice versa
                    // Make number wrap around from 9 to 0 and vice versa
                    int wrap = 10 * ('S' - 'A');
                    int contVer = (wrap + latsq - (row - (height - 1) / 2)) % wrap;
                    int contHor = (wrap + lonsq + (col - (width - 1) / 2)) % wrap;

                    // Convert location back to grid square format

                    result.Add(string.Format("{0}{1}{2}{3}",
                        (char)(contHor / 10 + 'A'),
                        (char)(contVer / 10 + 'A'),
                        contHor % 10,
                        contVer % 10));
                }
            }

            return result;

        }
    }
}
