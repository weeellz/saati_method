using System.Collections.Generic;

namespace testsaati
{
    public class Matrix
    {
        private double[,] data;
        public int Height => data.GetLength(0);
        public int Width => data.GetLength(1);

        public Matrix(int height, int width)
        {
            data = new double[height, width];
        }

        public double this[int line, int column]
        {
            get { return data[line, column]; }
            set { data[line, column] = value; }
        }

        // заполнение главной диагонали единицами
        public static void CreateDiagonal(Matrix matrix)
        {
            var len = matrix.Height; // здесь и далее: так как матрицы квадратные, то можно взять размер одного из измерений
            for (int i = 0; i < len; i++)
            {
                matrix[i, i] = 1;
            }
        }

        // нормализация матрицы (считается сумма элементов в столбце и каждый элемент столбца делится на эту сумму)
        public static void Normalize(Matrix matrix)
        {
            var len = matrix.Height;
            for (int j = 0; j < len; j++)
            {
                var columnSum = 0D;

                for (int i = 0; i < len; i++)
                {
                    columnSum += matrix[i, j];
                }

                for (int i = 0; i < len; i++)
                {
                    matrix[i, j] /= columnSum;
                }
            }
        }

        // умножение матриц
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Width != matrix2.Height)
            {
                throw new MatrixInvalidShapeException("Матрицы нельзя перемножить. " +
                                                      "Количество столбцов первой матрицы не совпадает" +
                                                      " с количеством строк второй матрицы.");
            }

            var result = new Matrix(matrix1.Height, matrix2.Width);

            for (int i = 0; i < matrix1.Height; i++)
            {
                for (int j = 0; j < matrix2.Width; j++)
                {
                    for (int z = 0; z < matrix1.Width; z++)
                    {
                        result[i, j] += matrix1[i, z] * matrix2[z, j];
                    }
                }
            }

            return result;
        }

        // формирование матрицы весов по каждому критерию
        public static Matrix FormWeights(List<Matrix> weights)
        {
            var width = weights[0].Width;
            var height = weights[0].Height;
            var weightsMatrix = new Matrix(height, weights.Count);

            for (int z = 0; z < weights.Count; z++)
            {
                for (int j = 0; j < width; j++)
                {
                    for (int i = 0; i < height; i++)
                    {
                        weightsMatrix[i, z] = weights[z][i, j];
                    }
                }
            }
            return weightsMatrix;
        }
    }
}
