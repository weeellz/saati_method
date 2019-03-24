namespace testsaati
{
    public class Matrix
    {
        private double[,] data;

        public Matrix()
        {

        }

        public Matrix(int countLine, int countColumn)
        {
            data = new double[countLine, countColumn];
        }

        public double this[int line, int column]
        {
            get { return data[line, column]; }
            set { data[line, column] = value; }
        }

        public int GetLength(int dimension)
        {
            return data.GetLength(dimension);
        }

        // умножение матриц (матрица с n столбцами и m строками на матрицу c 1 столбцом и n строк)
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            var critCount = matrix1.GetLength(1);
            var alterCount = matrix1.GetLength(0);
            var result = new Matrix(critCount, 1);

            for (int i = 0; i < alterCount; i++)
            {
                for (int j = 0; j < critCount; j++)
                {
                    result[i, 0] += matrix1[i, j] * matrix2[j, 0];
                }
            }

            return result;
        }
    }
}
