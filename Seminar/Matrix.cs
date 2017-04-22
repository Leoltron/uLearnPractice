using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar
{
    struct Matrix
    {
        private readonly int[,] values;
        public readonly int Width;
        public readonly int Height;
        public bool IsSquare => Width == Height;

        public Matrix(int[,] values)
        {
            this.values = values;
            Width = values.GetLength(1);
            Height = values.GetLength(0);
        }

        public int this[int column, int row] => values[column, row];

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (!m1.SizeEquals(m2))
                throw new ArgumentException();
            var resultMatrix = new int[m1.Height, m1.Width];
            for (var i = 0; i < m1.Width; i++)
            for (var j = 0; j < m1.Height; j++)
                resultMatrix[i, j] = m1[i, j] + m2[i, j];
            return new Matrix(resultMatrix);
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return m1 + m2 * -1;
        }

        public static Matrix operator *(Matrix m, int t)
        {
            var resultMatrix = new int[m.Height,m.Width];
            for (var i = 0; i < m.Width; i++)
            for (var j = 0; j < m.Height; j++)
                resultMatrix[i, j] = m[i, j] * t;
            return new Matrix(resultMatrix);
        }

        public static Matrix operator *(int t, Matrix m)
        {
            return m * t;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Width != m2.Height)
                throw new ArgumentException(
                    $"First matrix's width ({m1.Width}) must be equal to second matrix's height ({m2.Height})");
            var resultMatrix = new int[m1.Height, m2.Width];
            for (var i = 0; i < m1.Height; i++)
            for (var j = 0; j < m2.Width; j++)
            {
                var aij = 0;
                for (var n = 0; n < m2.Width; n++)
                    aij += m1[i, n] * m2[n, j];
                resultMatrix[i, j] = aij;
            }
             return new Matrix(resultMatrix);
        }

        public static Matrix operator ^(Matrix matrix, int power)
        {
            if(power < 1)
                throw  new ArgumentException($"Power must be positive! Got: {power}");
            for (var i = 1; i < power; i++)
                matrix *= matrix;
            return matrix;
        }

        public bool SizeEquals(Matrix other)
        {
            return Width == other.Height && Height == other.Height;
        }
    }
}