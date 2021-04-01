using System;

namespace gridmatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            string grid = "JO65MR";
            int height = 5;
            int width = 5;

            string[,] array = GridArray(grid, height, width);

            for (int vert = 0; vert < height; vert++)
            {
                for (int hor = 0; hor < width; hor++)
                {
                    Console.Write(array[vert, hor] + " ");
                }
                Console.WriteLine();
            }
        }

        static private string [,] GridArray(string square, int height, int width)
        {
            string[,] result = new string[height,width];

            for (int vert = 0; vert < height; vert++)
            {
                for (int hor = 0; hor < width; hor++)
                {
                    result[vert, hor] = vert.ToString("00") + "-" + hor.ToString("00");
                }
            }

            return result;
        }
    }
}
