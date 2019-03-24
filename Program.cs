using System;
using System.Collections.Generic;

namespace testsaati
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Кол-во критериев");
            var critCount = int.Parse(Console.ReadLine());
            Console.WriteLine("Кол-во альтернатив");
            var alterCount = int.Parse(Console.ReadLine());

            Console.WriteLine("-------------------------------------");

            var matrixes = new List<Matrix>();
            var weights = new List<Matrix>();

            matrixes.Add(new Matrix(critCount, critCount));

            for (int i = 0; i < critCount; i++)
            {
                matrixes.Add(new Matrix(alterCount, alterCount));
            }

            for (int i = 0; i < critCount + 1; i++)
            {
                Matrix.CreateDiagonal(matrixes[i]);
            }

            for (int i = 0; i < critCount + 1; i++)
            {
                matrixes[i].ElementsFromKeyboard();
            }

            matrixes[0].CalcElements();
            Matrix.Normalize(matrixes[0]);
            var critWeightsMatrix = matrixes[0].CalcWeights();

            for (int i = 1; i < critCount + 1; i++)
            {
                matrixes[i].CalcElements();
                Matrix.Normalize(matrixes[i]);
                weights.Add(matrixes[i].CalcWeights());
            }

            var weightsMatrix = Matrix.FormWeights(weights);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов критериев:");

            critWeightsMatrix.Show();

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов по каждому критерию:");

            weightsMatrix.Show();

            Console.WriteLine("-------------------------------------");

            var result = weightsMatrix * critWeightsMatrix;

            Console.WriteLine("Результирующая матрица:");

            result.Show();

            Console.WriteLine("-------------------------------------");

            var mostImportantCrit = critWeightsMatrix.FindMax();
            var mostSuitableChoice = result.FindMax();

            Console.WriteLine("Наиболее важный критерий - {0}, c важностью {1}",
                mostImportantCrit.lineIndex, mostImportantCrit.max);

            if (result.Width < 2)
            {
                Console.WriteLine("Наиболее подходящий вариант - {0}, c вероятностью {1}%",
                    mostSuitableChoice.lineIndex, mostSuitableChoice.max * 100);
            }
            else
            {
                Console.WriteLine("Наиболее подходящий вариант - [{0},{1}], c вероятностью {2}%",
                    mostSuitableChoice.lineIndex, mostSuitableChoice.columnIndex, mostSuitableChoice.max * 100);
            }

            Console.ReadKey();
        }
    }
}
