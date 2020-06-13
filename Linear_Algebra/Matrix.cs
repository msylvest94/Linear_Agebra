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
                size = new int[2] { dim[0], dim[1]};
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
                Row[0, i] = this.entries[r-1, i];
            }

            Matrix MatRow = new Matrix(Row);

            return MatRow;
            
        }

        public Matrix Column(int c)
        {
            double[,] Column = new double[this.dim[0],1];

            for (int i = 0; i < this.dim[0]; i++)
            {
                Column[i, 0] = this.entries[i, c-1];
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


        public static Matrix Scale(double s, Matrix Mat1)
        {
            Matrix scaleMat = CreateInitializedMat(Mat1.dim[0], Mat1.dim[1]);

            for (int i = 0; i < Mat1.dim[0]; i++)
            {


                for (int j = 0; j < Mat1.dim[1]; j++)
                {

                    scaleMat.entries[i,j] = Mat1.entries[i,j] * s;

                }
            }

            return scaleMat;
        }


        public void SetColumn(Matrix Mat1,int c)
        {
            for(int i = 0; i < dim[0]; i++)
            {
                entries[i, c] = Mat1.entries[i, 1];
            }
        }

        public void SetRow(Matrix Mat1, int r)
        {
            for (int i = 0; i < dim[1]; i++)
            {
                entries[r, i] = Mat1.entries[1, i];
            }
        }

        private static Matrix CreateInitializedMat(int r, int c)
        {
            double[,] VectArray = new double[r,c];

            return new Matrix(VectArray);
        }

        private static bool CheckDim(Matrix Mat1, Matrix Mat2)
        {
            bool output = new bool();

            if(Mat1.dim == Mat2.dim)
            {
                output = true;
            }
            else
            {
                output = false;
            }

            return output;
        }



    }
}
