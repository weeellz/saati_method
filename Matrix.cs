namespace testsaati
{
    public class Matrix
    {
        public double[,] data;

        public Matrix()
        {

        }

        public Matrix(int countLine, int countColumn)
        {
            data = new double[countLine, countColumn];
        }
    }
}
