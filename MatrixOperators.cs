using System;
using System.Collections.Generic;

namespace testsaati
{
    public static class MatrixOperators
    {
        // заполнение главной диагонали единицами
        public static void Init(Matrix matrix)
        {
            var len = matrix.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                matrix[i, i] = 1;
            }
        }

        // заполнение элементов матрицы над главной диагональю
        public static void ElementsFromKeyboard(this Matrix matrix)
        {
            var len = matrix.GetLength(0);
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
            var len = matrix.GetLength(0);
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
            var len = matrix.GetLength(0);
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
            var len = matrix.GetLength(0);
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

        // формарование матрицы весов по каждому критерию
        public static Matrix FormWeights(List<Matrix> weights, int alterCount)
        {
            var weightsMatrix = new Matrix(alterCount, weights.Count);
            var len = weights[0].GetLength(1);

            for (int z = 0; z < weights.Count; z++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int i = 0; i < alterCount; i++)
                    {
                        weightsMatrix[i, z] = weights[z][i, j];
                    }
                }
            }
            return weightsMatrix;
        }

        // поиск самого подходящего варианта в матрице из одного столбца/строки
        public static (double max, int index) FindMax(Matrix matrix)
        {
            var max = matrix[0, 0];
            var index = 0;
            var len = matrix.GetLength(0);

            for (int i = 0; i < len; i++)
            {
                if (max < matrix[i, 0])
                {
                    max = matrix[i, 0];
                    index = i;
                }
            }

            return (max, index + 1);
        }
    }
}
