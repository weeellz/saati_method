using System;

namespace testsaati
{
    class MatrixInvalidShapeException : MatrixException
    {
        public void Exception(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Width != matrix2.Height)
            {
                throw new Exception("Размеры матриц не совпадают.");
            }
        }
    }
}
