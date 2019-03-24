using System;

namespace testsaati
{
    public static class MatrixExtensions
    {
        // заполнение главной диагонали единицами
        public static void Init(Matrix matrix)
        {
            var len = matrix.Height; // здесь и далее: так как матрицы квадратные, то можно взять размер одного из измерений
            for (int i = 0; i < len; i++)
            {
                matrix[i, i] = 1;
            }
        }

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
        public static void Show(this Matrix matrix, int height, int width)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write("{0}\t", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
