using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csh_Lab_2
{
    class MyMatrix
    {
        #region fields

        public readonly char RowsSeparator = '\n';
        public readonly char ColsSeparator = ',';

        private double[][] m_array;

        // -1 - расчет не проводился; 
        //  0 - не являются: unity, diagonal, symmetric;
        //  1 - являются: unity, diagonal, symmetric;
        private int m_isUnity;
        private int m_isDiagonal;
        private int m_isSymmetric;

        #endregion


        #region constructors

        public MyMatrix(int Rows, int Cols)
        {
            if (Rows < 0 || Cols < 0)
                throw new Exception("Матрица не может иметь отрицательный размер!");

            m_array = new double[Math.Max(Rows, 1)][];
            for (int r = 0; r < m_array.Length; r++)
            {
                m_array[r] = new double[Math.Max(Cols, 1)];
            }

            m_isDiagonal = -1;
            m_isSymmetric = -1;
            m_isUnity = -1;
        }

        public MyMatrix(double[,] array)
        {
            if (array == null)
                throw new ArgumentNullException("Входной массив: null!");

            m_array = new double[array.GetLength(0)][];
            for (int r = 0; r < m_array.Length; r++)
            {
                m_array[r] = new double[array.GetLength(1)];
                for (int c = 0; c < m_array[r].Length; c++)
                {
                    m_array[r][c] = array[r, c];
                }
            }

            m_isDiagonal = -1;
            m_isSymmetric = -1;
            m_isUnity = -1;
        }

        public MyMatrix(double[][] array)
        {
            if (array == null)
                throw new ArgumentNullException("Входной массив: null!");

            int zeroRowCols = array[0].Length;
            m_array = new double[array.Length][];
            for (int r = 0; r < m_array.Length; r++)
            {
                if (zeroRowCols != m_array[r].Length)
                    throw new Exception("Входной массив имеет зубчатую форму!");

                m_array[r] = new double[zeroRowCols];
                Array.Copy(array[r], m_array[r], zeroRowCols);
            }

            m_isDiagonal = -1;
            m_isSymmetric = -1;
            m_isUnity = -1;
        }

        public MyMatrix(MyMatrix other)
        {
            if (other == null)
                throw new ArgumentNullException("Входная матрица: null!");

            m_array = new double[other.Rows][];
            for (int r = 0; r < other.Rows; r++)
            {
                m_array[r] = new double[other.Cols];
                Array.Copy(other.m_array[r], m_array[r], other.Cols);
            }

            m_isDiagonal = -1;
            m_isSymmetric = -1;
            m_isUnity = -1;
        }

        #endregion


        #region properties

        public int Rows => m_array.Length;

        public int Cols => m_array[0].Length;

        public int? Size
        {
            get
            {
                int k = m_array.Length * m_array[0].Length;
                if(k != 0)
                {
                    return k;
                }
                return null;
            }
        }

        public bool IsSquared => Rows == Cols;

        public bool IsEmpty => Rows == 0 && Cols == 0;

        public bool IsUnity
        {
            get
            {
                switch (m_isUnity)
                {
                    case 0:
                        return false;
                    case 1:
                        return true;
                    default:
                        m_isUnity = Round((double[][] m_array, int r, int c) => { return (c == r && m_array[r][c] != 1 || (m_array[r][c] != 0 || m_array[c][r] != 0) && c != r); });
                        return m_isUnity != 1? false : true;
                }
                //if (IsSquared)
                //{
                //    for (int r = 0; r < this.Rows; r++)
                //    {
                //        for (int c = r; c < this.Cols; c++)
                //        {
                //            if (c == r && m_array[r][c] != 1 || (m_array[r][c] != 0 || m_array[c][r] != 0) && c != r)
                //            {
                //                return false;
                //            }
                //        }
                //    }
                //    return true;
                //}
                //return false;
            }
        }

        public bool IsDiagonal
        {
            get
            {
                switch (m_isDiagonal)
                {
                    case 0:
                        return false;
                    case 1:
                        return true;
                    default:
                        m_isDiagonal = Round((double[][] m_array, int r, int c) => { return (c == r && m_array[r][c] == 0 || m_array[r][c] != 0 || m_array[c][r] != 0); });
                        return m_isDiagonal != 1 ? false : true;
                }
                //if (IsSquared)
                //{
                //    for (int r = 0; r < Rows; r++)
                //    {
                //        for (int c = r; c < Cols; c++)
                //        {
                //            if (c == r && m_array[r][c] == 0 || m_array[r][c] != 0 || m_array[c][r] != 0)
                //            {
                //                return false;
                //            }
                //        }
                //    }
                //    return true;
                //}
                //return false;
            }
        }
      
        public bool IsSymmetric
        {
            get
            {
                switch (m_isDiagonal)
                {
                    case 0:
                        return false;
                    case 1:
                        return true;
                    default:
                        m_isDiagonal = Round((double[][] m_array, int r, int c) => { return (m_array[r][c] != m_array[c][r]); });
                        return m_isDiagonal != 1 ? false : true;
                }
                //if (IsSquared)
                //{
                //    for (int r = 0; r < this.Rows; r++)
                //    {
                //        for (int c = r; c < this.Cols; c++)
                //        {
                //            if (m_array[r][c] != m_array[c][r])
                //            {
                //                return false;
                //            }
                //        }
                //    }
                //    return true;
                //}
                //return false;
            }
        }

        public double this[int row, int col]
        {
            get
            {
                CheckMatrixIndex(0, row, m_array.Length);
                CheckMatrixIndex(0, col, m_array[0].Length);
                return m_array[row][col];
            }

            set
            {
                CheckMatrixIndex(0, row, m_array.Length);
                CheckMatrixIndex(0, col, m_array[0].Length);
                m_array[row][col] = value;
            }
        }

        #endregion

        public MyMatrix Transpose()
        {
            MyMatrix temp = new MyMatrix(Cols, Rows);
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    temp[c, r] = this[r, c];
                }
            }
            return temp;
        }

        public double Trace()
        {
            if (!IsSquared)
                throw new Exception("Матрица не квадратная, след посчитать не возможно!");

            double sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                sum += m_array[i][i];
            }
            return sum;
        }

        public MyMatrix Inv()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sBuffer = new StringBuilder(typeof(MyMatrix).Name);
            sBuffer.Append(":").Append(Rows).Append("x").Append(Cols).Append(RowsSeparator);
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    sBuffer.Append(this[r, c]).Append((c != Cols - 1) ? ColsSeparator : RowsSeparator); 
                }
            }
            return sBuffer.ToString();
        }

        public static MyMatrix operator +(MyMatrix M1, MyMatrix M2)
        {
            if (M1 == null || M2 == null)
                throw new ArgumentNullException("M1 или M2 или M1 и M2 null!");

            if (!M1.TryCheckMatrixDemmention(M1, M2))
                throw new Exception("Размеры входных матриц M1 и M2 не совместимы по оператору сложения!");

            MyMatrix temp = new MyMatrix(M1);
            for (int r = 0; r < temp.Rows; r++)
            {
                for (int c = 0; c < temp.Cols; c++)
                {
                    temp[r, c] += M2[r, c];
                }
            }
            return temp;
        }

        public static MyMatrix operator -(MyMatrix M1, MyMatrix M2)
        {
            if (M1 == null || M2 == null)
                throw new ArgumentNullException("M1 или M2 или M1 и M2 null!");

            if (!M1.TryCheckMatrixDemmention(M1, M2))
                throw new Exception("Размеры входных матриц M1 и M2 не совместимы по оператору сложения !");

            MyMatrix temp = new MyMatrix(M1);
            for (int r = 0; r < temp.Rows; r++)
            {
                for (int c = 0; c < temp.Cols; c++)
                {
                    temp[r, c] -= M2[r, c];
                }
            }
            return temp;
        }

        public static MyMatrix operator *(MyMatrix M1, double d)
        {
            if (M1 == null)
                throw new ArgumentNullException("Входная матрица null!");

            MyMatrix temp = new MyMatrix(M1);
            for (int r = 0; r < temp.Rows; r++)
            {
                for (int c = 0; c < temp.Cols; c++)
                {
                    temp[r, c] *= d;
                }
            }
            return temp;
        }

        public static MyMatrix operator *(MyMatrix M1, MyMatrix M2)
        {
            if (M1 == null || M2 == null)
                throw new ArgumentNullException("M1 или M2 или M1 и M2 null!");

            if (M1.Cols != M2.Rows)
                throw new Exception("Размеры входных матриц M1 и M2 не совместимы по оператору умножения !");

            MyMatrix temp = new MyMatrix(M1.Rows, M2.Cols);
            for (int r = 0; r < M1.Rows; r++)
            {
                for (int c = 0; c < M2.Cols; c++)
                {
                    for (int k = 0; k < M1.Cols; k++)
                    {
                        temp[r, c] += M1[r, k] * M2[k, c];

                    }
                }
            }
            return temp;
        }

        public static explicit operator MyMatrix(double[,] array)
        {
            return new MyMatrix(array);
        }

        public static explicit operator MyMatrix(double[][] array)
        {
            return new MyMatrix(array);
        }


        /// <summary>
        /// Получить еденичную матрицу
        /// </summary>
        /// <param name="size">Число строк, столбцов квадратной матрицы</param>
        /// <returns></returns>
        public static MyMatrix GetUnity(int size) 
        {
            MyMatrix temp = new MyMatrix(size, size);
            for (int i = 0; i < size; i++)
            {
                temp[i, i] = 1;
            }
            return temp;
        }

        /// <summary>
        /// Получить нулевую квадратную матрицу
        /// </summary>
        /// <param name="size">Число строк, столбцов квадратной матрицы</param>
        /// <returns></returns>
        public static MyMatrix GetEmpty(int size) => new MyMatrix(size, size);

        /// <summary>
        /// Генерация случайной матрицы
        /// </summary>
        /// <param name="rows">Число строк</param>
        /// <param name="cols">Число столбцов</param>
        /// <param name="maxValue">Максимальное значение случайной величины</param>
        /// <returns></returns>
        public static MyMatrix Randomized(int rows, int cols, int maxValue)
        {
            MyMatrix temp = new MyMatrix(rows, cols);
            Random rand = new Random();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    temp[r, c] = rand.NextDouble() * rand.Next(maxValue);
                }
            }
            return temp;
        }

        public static MyMatrix Parse(string s)
        {
            MyMatrix temp = null;
            Queue<string> sRowLines = new Queue<string>(s.Trim().Split('\n'));
            string[] rowItems = sRowLines.Dequeue().Split(':');

            if (!typeof(MyMatrix).Name.Equals(rowItems[0]))
            {
                throw new FormatException("Ошибка объект не матрица!");
            }

            int rows = int.Parse(rowItems[1].Split('x')[0]);
            int cols = int.Parse(rowItems[1].Split('x')[1]);

            if (rows < 0 && cols < 0)
            {
                throw new FormatException("Ошибка неверно указан размер матрицы!");
            }

            temp = new MyMatrix(Math.Max(rows, 1), Math.Max(cols, 1));

            int rowIdx = 0;
            while (sRowLines.Count != 0)
            {
                rowItems = sRowLines.Dequeue().Split(temp.ColsSeparator);
                if (temp.Cols != rowItems.Length || rowIdx >= rows)
                {
                    throw new FormatException("Ошибка обработки строк, колонок матрицы!");
                }

                for (int c = 0; c < temp.Cols; c++)
                {
                    temp[rowIdx, c] = int.Parse(rowItems[c]);
                }

                rowIdx++;
            }
            return temp;
        }

        public static bool TryParse(string s, out MyMatrix M)
        {
            try
            {
                M = Parse(s);
                return true;
            }
            catch (Exception)
            {
                M = null;
                return false;
            }
        }


        #region subFunction

        private int Round(Func<double[][], int, int, bool> func)
        {
            if (IsSquared)
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = r; c < Cols; c++)
                    {
                        if (func(m_array, r, c))
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
            return 0;
        }

        private bool TryCheckMatrixIndex(int min, int idx, int max) => (min <= idx || idx < max);

        private void CheckMatrixIndex(int min, int idx, int max)
        {
            if (!TryCheckMatrixIndex(min, idx, max))
            {
                throw new IndexOutOfRangeException("Индекс row или col вне диапазона для матрицы!");
            }
        }

        private bool TryCheckMatrixDemmention(MyMatrix m1, MyMatrix m2) => (m1.Rows == m2.Rows && m1.Cols == m2.Cols);

        private void CheckMatrixDemmention(MyMatrix m1, MyMatrix m2)
        {
            if (!TryCheckMatrixDemmention(m1, m2))
            {
                throw new Exception("Размеры входных матриц M1 и M2 не совместимы по выбранному оператору действия!");
            }
        }

        private void CheckNulls(params object[] objs)
        {
            foreach (var item in objs)
            {
                if (item == null)
                {
                    throw new ArgumentNullException((objs.Length > 1) ? "Один из входных параметров принимает значение null" : "Входной параметр принимает значение null");
                }
            }
        }

        #endregion
    }
}
