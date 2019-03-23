using System;
using System.Collections.Generic;

namespace testsaati
{
    public static class MatrixOperators
    {
        // заполнение главной диагонали единицами
        public static void Init(Matrix matrix)
        {
            var len = matrix.data.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                matrix.data[i, i] = 1;
            }
        }

        // заполнение элементов матрицы над главной диагональю
        public static void ElementsFromKeyboard(Matrix matrix)
        {
            var len = matrix.data.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (j > i)
                    {
                        var number = double.Parse(Console.ReadLine());
                        matrix.data[i, j] = number;
                    }
                }
            }
        }

        // расчёт элементов под главной диагональю
        public static void CalcElements(Matrix matrix)
        {
            var len = matrix.data.GetLength(0);
            for (int i = len - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    matrix.data[i, j] = 1D / matrix.data[j, i];
                }
            }
        }

        // нормализация матрицы (считается сумма элементов в столбце и каждый элемент столбца делится на эту сумму)
        public static void Normalize(Matrix matrix)
        {
            var len = matrix.data.GetLength(0);
            for (int j = 0; j < len; j++)
            {
                var columnSum = 0D;

                for (int i = 0; i < len; i++)
                {
                    columnSum += matrix.data[i, j];
                }

                for (int i = 0; i < len; i++)
                {
                    matrix.data[i, j] /= columnSum;
                }
            }
        }

        // расчёт весов строк матрицы (среднее суммы элементов столбцов)
        public static Matrix CalcWeights(Matrix matrix)
        {
            var len = matrix.data.GetLength(0);
            var weights = new Matrix(len, 1);

            for (int i = 0; i < len; i++)
            {
                var lineSum = 0D;

                for (int j = 0; j < len; j++)
                {
                    lineSum += matrix.data[i, j];
                }

                weights.data[i, 0] = lineSum / len;
            }

            return weights;
        }

        // формарование матрицы весов по каждому критерию
        public static Matrix FormWeights(List<Matrix> weights, int alterCount)
        {
            var weightsMatrix = new Matrix(alterCount, weights.Count);
            var len = weights[0].data.GetLength(1);

            for (int z = 0; z < weights.Count; z++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int i = 0; i < alterCount; i++)
                    {
                        weightsMatrix.data[i, z] = weights[z].data[i, j];

                    }
                }
            }
            return weightsMatrix;
        }

        // умножение матриц (матрица с n столбцами и m строками на матрицу c 1 столбцом и n строк)
        public static Matrix Multiplication(Matrix matrix1, Matrix matrix2)
        {
            var critCount = matrix1.data.GetLength(1);
            var alterCount = matrix1.data.GetLength(0);
            var result = new Matrix(critCount, 1);

            for (int i = 0; i < alterCount; i++)
            {
                for (int j = 0; j < critCount; j++)
                {
                    result.data[i, 0] += matrix1.data[i, j] * matrix2.data[j, 0];
                }
            }

            return result;
        }

        // поиск самого подходящего варианта в матрице из одного столбца/строки
        public static (double max, int index) FindMax(Matrix matrix)
        {
            var max = matrix.data[0, 0];
            var index = 0;
            var len = matrix.data.GetLength(0);

            for (int i = 0; i < len; i++)
            {
                if (max < matrix.data[i, 0])
                {
                    max = matrix.data[i, 0];
                    index = i;
                }
            }

            return (max, index + 1);
        }
    }
}
