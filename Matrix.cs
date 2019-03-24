using System.Collections.Generic;

namespace testsaati
{
    public class Matrix
    {
        private double[,] data;
        public int Width { get; set; }
        public int Height { get; set; }

        public Matrix()
        {

        }

        public Matrix(int height, int width)
        {
            data = new double[height, width];
            Height = height;
            Width = width;
        }

        public double this[int line, int column]
        {
            get { return data[line, column]; }
            set { data[line, column] = value; }
        }

        // умножение матриц
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
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
