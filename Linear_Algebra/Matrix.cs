using System;

namespace Linear_Algebra
{
    public class Matrix
    {
        public double[,] entries;
        private int[] size;

        public Matrix(double[,] input)
        {
            // save input array as field entries 
            entries = input;

            // set property size from input array dimensions
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

        //method to that will return specified row
        public Matrix Row(int r)
        {
            // create multidimentional array
            Matrix MatRow = CreateInitializedMat(1, dim[1]);

            // run through the entries of disired row to populate new array
            for (int i = 0; i < dim[1]; i++)
            {
                MatRow.entries[0, i] = entries[r, i];
            }

            return MatRow;
            
        }

        //method to that will return specified column (array index, 0 is first column)
        public Matrix Column(int c)
        {
            // create initialized matrix
            Matrix MatCol = CreateInitializedMat(dim[0], 1);

            // run through the entries of disired column to populate matrix
            for(int i = 0; i < dim[0]; i++)
            {
                MatCol.entries[i, 0] = entries[i, c];
            }

            return MatCol;

        }

        
        public double Norm()
        {
            // initialize value
            double norm = 0;

            // run through each value of matrix
            for(int j = 0; j < dim[0]; j++)
            {
                for(int i = 0; i < dim[1]; i++)
                {
                    // add the squares of each entry
                    norm += Math.Pow(entries[j, i],2);
                }

            }
            // take the square root of the sum of the squares
            norm = Math.Sqrt(norm);

            return norm;
        }

        public Matrix Transpose()
        {
            // Create initialized Matrix
            Matrix transMatrix = CreateInitializedMat(dim[1], dim[0]); 

            for(int i = 0; i < dim[0]; i++)
            {
                for(int j = 0; j < dim[1]; j++)
                {
                    // populate the [j,i] entry of the transpose with the [i,j] entry of original matrix
                    transMatrix.entries[j,i] = entries[i,j];
                }
            }

            return transMatrix;
        }



        // adding matrices 
        public static Matrix Add(Matrix Mat1, Matrix Mat2)
        {
            // verify that the matrices have the same dimensions
            CheckDim(Mat1, Mat2, 0);

            //create initilized matrix
            Matrix sumMat = CreateInitializedMat(Mat1.dim[0], Mat1.dim[1]);

            for (int i = 0; i < Mat1.dim[0]; i++)
            {
                for (int j = 0; j < Mat1.dim[1]; j++)
                {
                    //loop through all entries of matrices and sum
                    sumMat.entries[i,j] = Mat1.entries[i,j] + Mat2.entries[i,j];
                }
            }

            return sumMat;
        }


        public static Matrix Multiply(Matrix Mat1, Matrix Mat2)
        {
            // make sure the dimension of matrices match for multiply
            CheckDim(Mat1, Mat2, 1);

            // create and initialized product matrix 
            Matrix prodMat = CreateInitializedMat(Mat1.dim[0], Mat2.dim[1]);

            for(int k = 0; k < Mat2.dim[1]; k++)
            {
                // for each column in Mat2 create initialized 
                Matrix prodCol = CreateInitializedMat(Mat1.dim[0], 1);

                for (int i = 0; i < Mat1.dim[1]; i++)
                {
                    // the kth column of product Mat is equal to the sum of the ith column of Mat1
                    // scaled by the ith entry of the kth column of Mat2
                    
                    prodCol = Matrix.Add(prodCol,Mat1.Column(i).Scale(Mat2.entries[i, k]));
                }

                // This is using a linear combination of the columns of Mat1 to construct the columns of prodMat
                prodMat.SetColumn(prodCol, k);
            }

            return prodMat;
        }


        public Matrix Scale(double s)
        {
            // create initilized matrix 
            Matrix scaleMat = CreateInitializedMat(dim[0], dim[1]);

            for (int i = 0; i < dim[0]; i++)
            { 
                for (int j = 0; j < dim[1]; j++)
                {
                    // scale each entry by input value
                    scaleMat.entries[i,j] = entries[i,j] * s;
                }
            }

            return scaleMat;
        }

        // populate column of a matrix with a column matrix
        public void SetColumn(Matrix Mat1, int c)
        {
            for(int i = 0; i < dim[0]; i++)
            {
                // set values of desired column equal to Mat1.entries
                entries[i, c] = Mat1.entries[i, 0];
            }
        }

        //populate row of a matrix with row matrix
        public void SetRow(Matrix Mat1, int r)
        {
            for (int i = 0; i < dim[1]; i++)
            {
                entries[r, i] = Mat1.entries[0, i];
            }
        }

        public int Rank()
        {
            Matrix Qmat = CreateInitializedMat(dim[0], dim[1]);

            Qmat = this;

            for (int i = 0; i < dim[1]; i++)
            {
                // create variable for ith column
                Matrix IColumn = Qmat.Column(i);

                //create variable for norm of ith column
                double INorm = IColumn.Norm();

                // Subtract the ith column component from jth columns
                for(int j = i+1; j < dim[1]; j++)
                {
                    // create variable for jth column
                    Matrix JColumn = Qmat.Column(j);

                    // find dot product of ith and jth columns 
                    double Dot = Matrix.Multiply(IColumn.Transpose(), JColumn).entries[0,0];

                    // divide dot product by magnitude of ith column
                    double MagOfIOntoJ = Dot/INorm;

                    // scale normalized ith column by manitude of ith column onto jth column 
                    Matrix IProjOntoJ = IColumn.Scale(MagOfIOntoJ/INorm);

                    //set jth column of Qmat to the value of jth column minus its projection in the ith columns direction
                    Qmat.SetColumn(Matrix.Add(JColumn, IProjOntoJ.Scale(-1)),j);

                }
                
            }

            return Qmat.NumNonZero(1);

        }

        public Matrix Normalize()
        {
            return this.Scale(Math.Pow(this.Norm(), -1));
        }

        public void Print()
        {

            for(int i = 0; i < dim[0]; i++)
            {
                for(int j = 0; j < dim[1]; j++)
                {
                    Console.Write("{0,-5}",entries[i,j]);
                }

                Console.Write("\n");
            }

        }

        public int NumNonZero(int d = 1)
        {
            int nonZeroRC = 0;

            if(d == 0)
            {
                for (int i = 0; i < dim[0]; i++)
                {
                    double norm = this.Row(i).Norm();

                    if (norm < 0.001)
                    {

                    }
                    else if (norm > 0.001)
                    {
                        nonZeroRC += 1;
                    }

                }
                    return nonZeroRC;
            }
            else if(d == 1)
            {
                for(int i = 0; i < dim[0]; i++)
                {
                    double norm = this.Column(i).Norm();

                    if (norm < 0.001)
                    {

                    }
                    else if (norm > 0.001)
                    {
                        nonZeroRC += 1;
                    }
                }
                return nonZeroRC;
            }
            else
            {
                throw new Exception("NumNonZero integer must be 0 or 1, for number of nonzero rows or columns respectively");
            }
        }



        private static Matrix CreateInitializedMat(int r, int c)
        {
            double[,] VectArray = new double[r,c];

            return new Matrix(VectArray);
        }

        private static void CheckDim(Matrix Mat1, Matrix Mat2, int d)
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
