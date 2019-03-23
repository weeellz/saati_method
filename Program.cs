using System;
using System.Collections.Generic;

namespace testsaati
{
    public class MatrixOperators
    {
        // заполнение главной диагонали единицами
        public void Init(double[,] matrix)
        {
            var len = matrix.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                matrix[i, i] = 1;
            }
        }

        // заполнение элементов матрицы над главной диагональю
        public void ElementsFromKeyboard(double[,] matrix)
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
        public void CalcElements(double[,] matrix)
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
        public void Normalize(double[,] matrix)
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
        public double[] CalcWeights(double[,] matrix)
        {
            var len = matrix.GetLength(0);
            var weights = new double[len];

            for (int i = 0; i < len; i++)
            {
                var lineSum = 0D;

                for (int j = 0; j < len; j++)
                {
                    lineSum += matrix[i, j];
                }

                weights[i] = lineSum / len;
            }

            return weights;
        }

        // формарование матрицы весов по каждому критерию
        public double[,] FormWeights(List<double[]> weights, int alterCount)
        {
            var weightsMatrix = new double[alterCount, weights.Count];

            for (int j = 0; j < weights.Count; j++)
            {
                for (int i = 0; i < alterCount; i++)
                {
                    weightsMatrix[i, j] = weights[j][i];
                }
            }

            return weightsMatrix;
        }

        // умножение матриц (матрица с n столбцами и m строками на матрицу c 1 столбцом и n строк)
        public double[] Multiplication(double[,] matrix1, double[] matrix2)
        {
            var critCount = matrix1.GetLength(1);
            var alterCount = matrix1.GetLength(0);
            var result = new double[critCount];

            for (int i = 0; i < alterCount; i++)
            {
                for (int j = 0; j < critCount; j++)
                {
                    result[i] += matrix1[i, j] * matrix2[j];
                }
            }

            return result;
        }

        // поиск самого подходящего варианта в матрице из одного столбца/строки
        public (double max, int index) FindMax(double[] array)
        {
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

            return (max, index + 1);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Кол-во критериев");
            int critCount = int.Parse(Console.ReadLine());
            Console.WriteLine("Кол-во альтернатив");
            int alterCount = int.Parse(Console.ReadLine());
            var matrix = new MatrixOperators();

            Console.WriteLine("-------------------------------------");

            List<double[,]> matrixes = new List<double[,]>();
            List<double[]> weights = new List<double[]>();

            matrixes.Add(new double[critCount, critCount]);

            for (int i = 0; i < critCount; i++)
            {
                matrixes.Add(new double[alterCount, alterCount]);
            }

            matrix.Init(matrixes[0]);

            for (int i = 1; i < critCount + 1; i++)
            {
                matrix.Init(matrixes[i]);
            }

            matrix.ElementsFromKeyboard(matrixes[0]);

            for (int i = 1; i < critCount + 1; i++)
            {
                matrix.ElementsFromKeyboard(matrixes[i]);
            }

            matrix.CalcElements(matrixes[0]);
            matrix.Normalize(matrixes[0]);
            var critWeightsMatrix = matrix.CalcWeights(matrixes[0]);

            for (int i = 1; i < critCount + 1; i++)
            {
                matrix.CalcElements(matrixes[i]);
                matrix.Normalize(matrixes[i]);
                weights.Add(matrix.CalcWeights(matrixes[i]));
            }

            var weightsMatrix = matrix.FormWeights(weights, alterCount);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов критериев:");

            for (int i = 0; i < critCount; i++)
            {
                Console.Write("{0}\t", critWeightsMatrix[i]);
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов по каждому критерию:");

            for (int i = 0; i < alterCount; i++)
            {
                for (int j = 0; j < critCount; j++)
                {
                    Console.Write("{0}\t", weightsMatrix[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------");

            var result = matrix.Multiplication(weightsMatrix, critWeightsMatrix);
            var mostImportantCrit = matrix.FindMax(critWeightsMatrix);
            var mostSuitableChoice = matrix.FindMax(result);
            Console.WriteLine("Наиболее важный критерий - {0}, c важностью {1}", mostImportantCrit.index, mostImportantCrit.max);
            Console.WriteLine("Наиболее подходящий вариант - {0}, c вероятностью {1}%", mostSuitableChoice.index, mostSuitableChoice.max * 100);

            Console.ReadKey();
        }
    }
}
