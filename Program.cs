using System;

namespace gridmatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            string grid = "RR85MR";
            int height = 9;
            int width = 9;

            string[,] matrix = GridMatrix(grid, height, width);
            Console.WriteLine("Matrix:");
            for (int vert = 0; vert < height; vert++)
            {
                for (int hor = 0; hor < width; hor++)
                    Console.Write(matrix[vert, hor] + " ");
                Console.WriteLine();
            }

            string[] array = GridArray(grid, height, width);
            Console.WriteLine("Array:");
            for (int index = 1; index <= array.Length; index++)
            {
                if (index % width != 0)
                    Console.Write(array[index - 1] + " ");
                else
                    Console.WriteLine(array[index - 1]);
            }
        }

        static private string[,] GridMatrix(string square, int height, int width)
        {
            string[,] result = new string[height, width];

            // Map maidenhead grid onto a continuous grid
            int lonsq = 10 * (square.Substring(0, 1)[0] - 'A') + int.Parse(square.Substring(2, 1));
            int latsq = 10 * (square.Substring(1, 1)[0] - 'A') + int.Parse(square.Substring(3, 1));

            for (int vert = 0; vert < height; vert++)
            {
                for (int hor = 0; hor < width; hor++)
                {
                    // Real square in continuous grid corresponding to vert and hor
                    int maxp1 = 10 * ('R' - 'A' + 1);
                    int thisvert = (maxp1 + latsq - (vert - (height - 1) / 2)) % maxp1;
                    int thishor = (maxp1 + lonsq + (hor - (width - 1) / 2)) % maxp1;

                    // Form name of grid in matrix
                    string firstc = ((char)(thishor / 10 + 'A')).ToString();
                    string secc = ((char)(thisvert / 10 + 'A')).ToString();
                    string firstd = (thishor % 10).ToString();
                    string secd = (thisvert % 10).ToString();

                    result[vert, hor] = firstc + secc + firstd + secd;
                }
            }

            return result;
        }

        static private string[] GridArray(string square, int height, int width)
        {
            string[] result = new string[height * width];

            // Map maidenhead grid onto a continuous grid
            int lonsq = 10 * (square.Substring(0, 1)[0] - 'A') + int.Parse(square.Substring(2, 1));
            int latsq = 10 * (square.Substring(1, 1)[0] - 'A') + int.Parse(square.Substring(3, 1));

            for (int vert = 0; vert < height; vert++)
            {
                for (int hor = 0; hor < width; hor++)
                {
                    int maxp1 = 10 * ('R' - 'A' + 1);
                    int thisvert = (maxp1 + latsq - (vert - (height - 1) / 2)) % maxp1;
                    int thishor = (maxp1 + lonsq + (hor - (width - 1) / 2)) % maxp1;

                    // Form name of grid in array
                    string firstc = ((char)(thishor / 10 + 'A')).ToString();
                    string secc = ((char)(thisvert / 10 + 'A')).ToString();
                    string firstd = (thishor % 10).ToString();
                    string secd = (thisvert % 10).ToString();

                    result[vert * width + hor] = firstc + secc + firstd + secd;
                }
            }

            return result;
        }
    }
}
