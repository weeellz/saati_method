using System;

namespace testsaati
{
    public static class MatrixExtensions
    {
        // заполнение элементов матрицы над главной диагональю
        public static void ElementsFromKeyboard(this Matrix matrix)
        {
            var len = matrix.Height;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (j > i)
                    {
                        var number = double.Parse(Console.ReadLine());
                        matrix[i, j] = number;
                    }
                }
            }
        }

        // расчёт элементов под главной диагональю
        public static void CalcElements(Matrix matrix)
        {
            var len = matrix.Height;
            for (int i = len - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    matrix[i, j] = 1D / matrix[j, i];
                }
            }
        }

        // расчёт весов строк матрицы (среднее суммы элементов столбцов)
        public static Matrix CalcWeights(Matrix matrix)
        {
            var len = matrix.Height;
            var weights = new Matrix(len, 1);

            for (int i = 0; i < len; i++)
            {
                var lineSum = 0D;

                for (int j = 0; j < len; j++)
                {
                    lineSum += matrix[i, j];
                }

                weights[i, 0] = lineSum / len;
            }

            return weights;
        }

        // поиск самого подходящего варианта в матрице
        public static (double max, int lineIndex, int columnIndex) FindMax(Matrix matrix)
        {
            var max = matrix[0, 0];
            var lineIndex = 0;
            var columnIndex = 0;
            var height = matrix.Height;
            var width = matrix.Width;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (max < matrix[i, 0])
                    {
                        max = matrix[i, 0];
                        lineIndex = i;
                        columnIndex = j;
                    }
                }
            }

            return (max, lineIndex + 1, columnIndex + 1);
        }

        // печать матрицы
        public static void Show(this Matrix matrix)
        {
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    Console.Write("{0}\t", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
