using System;

namespace Linear_Algebra
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Creating Arrays");
            double[] V1 = new double[] { 1, 2 };
            double[] V2 = new double[] { 3, 4 };

            Console.WriteLine("Creating Array of Arrays");
            double[,] M1 = new double[2,2] { { 1, 2 } , { 3, 4 } };

            Console.WriteLine("Displaying Arrays");
            Array.ForEach(V1, Console.WriteLine);
            Array.ForEach(V2, Console.WriteLine);


            Console.WriteLine("Creating Matix from Array of Arrays");
            Matrix Mat1 = new Matrix(M1);

            Console.WriteLine("Displaying Matrix");
            for (int i = 0; i < Mat1.dim[0]; i++)
            {
                for(int j = 0; j < Mat1.dim[1]; j++)
                {
                    Console.WriteLine(Mat1.entries[i, j]);
                }
     
            }

            Console.WriteLine("Creating Matix Transpose");
            Matrix Mat2 = Mat1.Transpose();

            Console.WriteLine("Displaying Matrix Tranpose");
            for (int i = 0; i < Mat2.dim[0]; i++)
            {
                for (int j = 0; j < Mat2.dim[1]; j++)
                {
                    Console.WriteLine(Mat2.entries[i, j]);
                }

            }
            Console.WriteLine("Check Size Field");
            Array.ForEach(Mat2.dim,Console.WriteLine);

            Console.WriteLine("Adding Matrix and Matrix Transpose");
            Matrix sumMat = Matrix.Add(Mat1, Mat2);

            Console.WriteLine("Displaying Matrix Sum");
            for (int i = 0; i < sumMat.dim[0]; i++)
            {
                for (int j = 0; j < sumMat.dim[1]; j++)
                {
                    Console.WriteLine(sumMat.entries[i, j]);
                }

            }

            Console.WriteLine("Multiplying Matrix and Matrix Transpose");
            Matrix prodMat = Matrix.Multiply(Mat1, Mat2);

            Console.WriteLine("Displaying Matrix Product");
            for (int i = 0; i < prodMat.dim[0]; i++)
            {
                for (int j = 0; j < prodMat.dim[1]; j++)
                {
                    Console.WriteLine(prodMat.entries[i, j]);
                }

            }


            Console.WriteLine("Grabbing Column from Mat1");
            Matrix C1 = Mat1.Column(1);

            Console.WriteLine("Displaying Column");
            for (int i = 0; i < C1.dim[0]; i++)
            {
                for (int j = 0; j < C1.dim[1]; j++)
                {
                    Console.WriteLine(C1.entries[i, j]);
                }

            }


            Console.WriteLine("Comparing dimensions of Matrices");
            Console.WriteLine("Mat1 & Mat2");



        }
    }
}
