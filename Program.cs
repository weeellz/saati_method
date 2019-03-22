using System;
using System.Collections.Generic;

namespace testsaati
{
    class Program
    {
        // заполнение главной диагонали единицами
        public static void InitMatrix(double[,] matrix, int c)
        {
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
        }

        // заполнение элементов матрицы над главной диагональю
        private static void ElementsFromKeyboard(double[,] matrix, int c)
        {
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
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
        private static void CalcMatrixElements(double[,] matrix, int c)
        {
            for (int i = c - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    matrix[i, j] = 1D / matrix[j, i];
                }
            }
        }

        // нормализация матрицы (считается сумма элементов в столбце и каждый элемент столбца делится на эту сумму)
        private static void NormalizeMatrix(double[,] matrix, int c)
        {
            double columnSum = 0;

            for (int j = 0; j < c; j++)
            {
                for (int i = 0; i < c; i++)
                {
                    columnSum = columnSum + matrix[i, j];
                }

                for (int i = 0; i < c; i++)
                {
                    matrix[i, j] = matrix[i, j] / columnSum;
                }

                columnSum = 0;
            }
        }

        // рассчёт весов строк матрицы (среднее суммы элементов столбцов)
        private static double[] CalcWeights(double[,] matrix, int c)
        {
            double[] weights = new double[c];
            double lineSum = 0;

            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    lineSum = lineSum + matrix[i, j];
                }

                weights[i] = lineSum / c;

                lineSum = 0;
            }

            return weights;
        }

        // формарование матрицы весов по каждому критерию
        private static double[,] FormWeightsMatrix(List<double[]> weights, int c, int a)
        {
            double[,] weightsMatrix = new double[a, c];
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    weightsMatrix[i, j] = 0;
                }
            }

            for (int z = 0; z < weights.Count; z++)
            {
                for (int j = 0; j < c; j++)
                {
                    for (int i = 0; i < a; i++)
                    {
                        weightsMatrix[i, j] = weights[j][i];
                    }
                }
            }

            return weightsMatrix;
        }

        // умножение матриц
        private static double[] MatrixMultiplication(double[,] matrix1, double[] matrix2, int c, int a)
        {
            double[] result = new double[c];

            for (int i = 0; i < c; i++)
            {
                result[i] = 0;
            }

            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    result[i] = result[i] + matrix1[i, j] * matrix2[j];
                }
            }

            return result;
        }

        // поиск самого подходящего варианта
        private static double[] FindMax(double[] array)
        {
            double[] result = new double[2];
            var max = array[0];
            var index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (max < array[i])
                {
                    max = array[i];
                    index = i;
                }
            }

            result[0] = max;
            result[1] = index + 1;

            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Кол-во критериев");
            int c = int.Parse(Console.ReadLine());
            Console.WriteLine("Кол-во альтернатив");
            int a = int.Parse(Console.ReadLine());

            Console.WriteLine("-------------------------------------");

            List<double[,]> matrixes = new List<double[,]>();
            List<double[]> weights = new List<double[]>();

            matrixes.Add(new double[c, c]);

            for (int i = 0; i < c; i++)
            {
                matrixes.Add(new double[a, a]);
            }

            InitMatrix(matrixes[0], c);

            for (int i = 1; i < c + 1; i++)
            {
                InitMatrix(matrixes[i], a);
            }

            ElementsFromKeyboard(matrixes[0], c);

            for (int i = 1; i < c + 1; i++)
            {
                ElementsFromKeyboard(matrixes[i], a);
            }

            CalcMatrixElements(matrixes[0], c);
            NormalizeMatrix(matrixes[0], c);
            var critWeightsMatrix = CalcWeights(matrixes[0], c);

            for (int i = 1; i < c + 1; i++)
            {
                CalcMatrixElements(matrixes[i], a);
                NormalizeMatrix(matrixes[i], a);
                weights.Add(CalcWeights(matrixes[i], a));
            }

            var weightsMatrix = FormWeightsMatrix(weights, c, a);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов критериев:");

            for (int i = 0; i < c; i++)
            {
                Console.Write("{0}\t", critWeightsMatrix[i]);
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов по каждому критерию:");

            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    Console.Write("{0}\t", weightsMatrix[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------");

            var result = MatrixMultiplication(weightsMatrix, critWeightsMatrix, c, a);
            var mostImportantCrit = FindMax(critWeightsMatrix);
            var mostSuitableChoice = FindMax(result);
            Console.WriteLine("Наиболее важный критерий - {0}, c важностью {1}", mostImportantCrit[1], mostImportantCrit[0]);
            Console.WriteLine("Наиболее подходящий вариант - {0}, c вероятностью {1}%", mostSuitableChoice[1], mostSuitableChoice[0] * 100);

            Console.ReadKey();
        }
    }
}
