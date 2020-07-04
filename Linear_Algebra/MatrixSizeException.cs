using System;
namespace Linear_Algebra
{
    public class MatrixSizeException : Exception
    {
        public MatrixSizeException(string message) : base(message)
        {
        }
    }
}
