using System;

namespace gridmatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            string grid = "RR85MR";
            int height = 15;
            int width = 15;

            string[,] array = GridMatrix(grid, height, width);

            for (int vert = 0; vert < height; vert++)
            {
                for (int hor = 0; hor < width; hor++)
                {
                    Console.Write(array[vert, hor] + " ");
                }
                Console.WriteLine();
            }
        }

        static private string[,] GridMatrix(string square, int height, int width)
        {
            string[,] result = new string[height, width];

            // Map maidenhead grid onto continuous grid
            int lonsq = 10 * (square.Substring(0, 1)[0] - 'A') + int.Parse(square.Substring(2, 1));
            int latsq = 10 * (square.Substring(1, 1)[0] - 'A') + int.Parse(square.Substring(3, 1));

            //Console.WriteLine(string.Format("Longitude square #{0}", lonsq));
            //Console.WriteLine(string.Format("Latitude square #{0}", latsq));

            for (int vert = 0; vert < height; vert++)
            {
                for (int hor = 0; hor < width; hor++)
                {
                    // Real square in continuous grid corresponding to vert and hor
                    int maxp1 = 10 * ('R' - 'A' + 1);
                    int thisvert = (maxp1 + latsq - (vert - (height - 1) / 2)) % maxp1;
                    int thishor = (maxp1 + lonsq + (hor - (width - 1) / 2)) % maxp1;

                    // Form name of grid
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

            // Map maidenhead grid onto continuous grid
            int lonsq = 10 * (square.Substring(0, 1)[0] - 'A') + int.Parse(square.Substring(2, 1));
            int latsq = 10 * (square.Substring(1, 1)[0] - 'A') + int.Parse(square.Substring(3, 1));

            //Console.WriteLine(string.Format("Longitude square #{0}", lonsq));
            //Console.WriteLine(string.Format("Latitude square #{0}", latsq));

            for (int vert = 0; vert < height; vert++)
            {
                for (int hor = 0; hor < width; hor++)
                {
                    // Real square in continuous grid corresponding to vert and hor
                    int thisvert = latsq - (vert - (height - 1) / 2);
                    int thishor = lonsq + (hor - (width - 1) / 2);

                    // Form name of grid
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
