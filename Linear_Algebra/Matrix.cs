using System;

namespace Linear_Algebra
{
    public class Matrix
    {
        public double[,] entries;
        private int[] size;

        public Matrix(double[,] input)
        {

            entries = input;

            if (entries.GetUpperBound(0) == -1 || entries.GetUpperBound(1) == -1)
            {
                size = new int[] { 0, 0};
            }
            else
            {
                size = new int[2] {entries.GetUpperBound(0)+1, entries.GetUpperBound(1)+1};
            }
            
        }

        public int[] dim
        {
            get
            {
                return size;
            }
        }

        public Matrix Row(int r)
        {
            double[,] Row = new double[1,this.dim[1]];

            for(int i = 0; i < this.dim[1]; i++)
            {
                Row[0, i] = this.entries[r, i];
            }

            Matrix MatRow = new Matrix(Row);

            return MatRow;
            
        }

        public Matrix Column(int c)
        {
            double[,] Column = new double[this.dim[0],1];

            for (int i = 0; i < this.dim[0]; i++)
            {
                Column[i, 0] = this.entries[i, c];
            }

            Matrix MatCol = new Matrix(Column);

            return MatCol;

        }


        public Matrix Transpose()
        {
            Matrix transMatrix = CreateInitializedMat(dim[0], dim[1]); 

            for(int i = 0; i < dim[0]; i++)
            {
                for(int j = 0; j < dim[1]; j++)
                {
                    transMatrix.entries[i,j] = this.entries[j,i];
                }
            }

            return transMatrix;
        }


        public static Matrix Add(Matrix Mat1, Matrix Mat2)
        {
            CheckDim(Mat1, Mat2, 0);

            Matrix sumMat = CreateInitializedMat(Mat1.dim[0], Mat1.dim[1]);

            for (int i = 0; i < Mat1.dim[0]; i++)
            {
                for (int j = 0; j < Mat1.dim[1]; j++)
                {
                    sumMat.entries[i,j] = Mat1.entries[i,j] + Mat2.entries[i,j];
                }
            }

            return sumMat;
        }

        public static Matrix Multiply(Matrix Mat1, Matrix Mat2)
        {
            CheckDim(Mat1, Mat2, 1);

            Matrix prodMat = CreateInitializedMat(Mat1.dim[0], Mat2.dim[1]);

            for(int k = 0; k < Mat2.dim[1]; k++)
            {
                Matrix prodCol = CreateInitializedMat(Mat1.dim[1], 1);

                for (int i = 0; i < Mat1.dim[1]; i++)
                {
                    prodCol = Matrix.Add(prodCol,Mat1.Column(i).Scale(Mat2.entries[i, k]));
                }

                prodMat.SetColumn(prodCol, k);
            }

            return prodMat;
        }


        public Matrix Scale(double s)
        {
            Matrix scaleMat = CreateInitializedMat(dim[0], dim[1]);

            for (int i = 0; i < dim[0]; i++)
            { 
                for (int j = 0; j < dim[1]; j++)
                {
                    scaleMat.entries[i,j] = entries[i,j] * s;
                }
            }

            return scaleMat;
        }


        public void SetColumn(Matrix Mat1, int c)
        {
            for(int i = 0; i < dim[0]; i++)
            {
                entries[i, c] = Mat1.entries[i, 0];
            }
        }

        public void SetRow(Matrix Mat1, int r)
        {
            for (int i = 0; i < dim[1]; i++)
            {
                entries[r, i] = Mat1.entries[0, i];
            }
        }

        public static Matrix CreateInitializedMat(int r, int c)
        {
            double[,] VectArray = new double[r,c];

            return new Matrix(VectArray);
        }

        public static void CheckDim(Matrix Mat1, Matrix Mat2, int d)
        {

            if (d == 0)
            {
                if (Mat1.dim[0] == Mat2.dim[0] && Mat1.dim[1] == Mat2.dim[1])
                {

                }
                else
                {
                    throw new MatrixSizeException("Matrix dimensions do not match");
                }

            }
            else if (d == 1)
            {
                if (Mat1.dim[1] == Mat2.dim[0])
                {

                }
                else
                {
                    throw new MatrixSizeException("Matrix 1 does not have as many columns as Matrix 2 has rows");
                }

            }
            else
            {
                Console.WriteLine("Error third entry must be integer be 0 or 1 \n 0: do all dimensions match. \n 1: do matrix dimension allow multiplcation");

            }
        }



    }
}
