using System;

namespace testsaati
{
    class MatrixInvalidShapeException : Exception
    {
        public MatrixInvalidShapeException(string message)
            : base(message)
        { }
    }
}
