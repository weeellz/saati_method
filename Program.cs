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

            for (int i = 0; i < critCount + 1; i++)
            {
                MatrixExtensions.Init(matrixes[i]);
            }

            for (int i = 0; i < critCount + 1; i++)
            {
                matrixes[i].ElementsFromKeyboard();
            }

            MatrixExtensions.CalcElements(matrixes[0]);
            MatrixExtensions.Normalize(matrixes[0]);
            var critWeightsMatrix = MatrixExtensions.CalcWeights(matrixes[0]);

            for (int i = 1; i < critCount + 1; i++)
            {
                MatrixExtensions.CalcElements(matrixes[i]);
                MatrixExtensions.Normalize(matrixes[i]);
                weights.Add(MatrixExtensions.CalcWeights(matrixes[i]));
            }

            var weightsMatrix = Matrix.FormWeights(weights);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов критериев:");

            critWeightsMatrix.Show(critWeightsMatrix.Height, critWeightsMatrix.Width);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Матрица коэффициентов по каждому критерию:");

            weightsMatrix.Show(weightsMatrix.Height, weightsMatrix.Width);

            Console.WriteLine("-------------------------------------");

            var result = new Matrix();

            try
            {
                result = weightsMatrix * critWeightsMatrix;
            }
            catch (MatrixInvalidShapeException ex)
            {
                Console.WriteLine("Ошибка:" + ex.Message);
            }

            Console.WriteLine("Результирующая матрица:");

            result.Show(result.Height, result.Width);

            Console.WriteLine("-------------------------------------");

            var mostImportantCrit = MatrixExtensions.FindMax(critWeightsMatrix);
            var mostSuitableChoice = MatrixExtensions.FindMax(result);

            Console.WriteLine("Наиболее важный критерий - {0}, c важностью {1}", mostImportantCrit.lineIndex, mostImportantCrit.max);

            if (result.Width < 2)
            {
                Console.WriteLine("Наиболее подходящий вариант - {0}, c вероятностью {1}%", mostSuitableChoice.lineIndex, mostSuitableChoice.max * 100);
            }
            else
            {
                Console.WriteLine("Наиболее подходящий вариант - [{0},{1}], c вероятностью {2}%", mostSuitableChoice.lineIndex,
                    mostSuitableChoice.columnIndex, mostSuitableChoice.max * 100);
            }

            Console.ReadKey();
        }
    }
}
