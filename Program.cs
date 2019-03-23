using System;
using System.Collections.Generic;

namespace testsaati
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Кол-во критериев");
            int critCount = int.Parse(Console.ReadLine());
            Console.WriteLine("Кол-во альтернатив");
            int alterCount = int.Parse(Console.ReadLine());

            Console.WriteLine("-------------------------------------");

            List<Matrix> matrixes = new List<Matrix>();
            List<Matrix> weights = new List<Matrix>();

            matrixes.Add(new Matrix(critCount, critCount));

            for (int i = 0; i < critCount; i++)
            {
                matrixes.Add(new Matrix(alterCount, alterCount));
            }

            MatrixOperators.Init(matrixes[0]);

            for (int i = 1; i < critCount + 1; i++)
            {
                MatrixOperators.Init(matrixes[i]);
            }

            MatrixOperators.ElementsFromKeyboard(matrixes[0]);

            for (int i = 1; i < critCount + 1; i++)
            {
                MatrixOperators.ElementsFromKeyboard(matrixes[i]);
            }

            MatrixOperators.CalcElements(matrixes[0]);
            MatrixOperators.Normalize(matrixes[0]);
            var critWeightsMatrix = MatrixOperators.CalcWeights(matrixes[0]);

            for (int i = 1; i < critCount + 1; i++)
            {
                MatrixOperators.CalcElements(matrixes[i]);
                MatrixOperators.Normalize(matrixes[i]);
                weights.Add(MatrixOperators.CalcWeights(matrixes[i]));
            }

            var weightsMatrix = MatrixOperators.FormWeights(weights, alterCount);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов критериев:");

            for (int i = 0; i < critCount; i++)
            {
                Console.Write("{0}\t", critWeightsMatrix.data[i, 0]);
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов по каждому критерию:");

            for (int i = 0; i < alterCount; i++)
            {
                for (int j = 0; j < critCount; j++)
                {
                    Console.Write("{0}\t", weightsMatrix.data[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------");

            var result = MatrixOperators.Multiplication(weightsMatrix, critWeightsMatrix);
            var mostImportantCrit = MatrixOperators.FindMax(critWeightsMatrix);
            var mostSuitableChoice = MatrixOperators.FindMax(result);
            Console.WriteLine("Наиболее важный критерий - {0}, c важностью {1}", mostImportantCrit.index, mostImportantCrit.max);
            Console.WriteLine("Наиболее подходящий вариант - {0}, c вероятностью {1}%", mostSuitableChoice.index, mostSuitableChoice.max * 100);

            Console.ReadKey();
        }
    }
}
