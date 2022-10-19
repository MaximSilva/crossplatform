namespace Lab2_60
{
    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";

        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            var inputData = File.ReadLines(InputFilePath).ToList();

            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                if (!inputData.Any())
                {
                    streamWriter.WriteLine("No data in file!");
                }
                else
                {
                    var n_k_info = inputData[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
                    if (n_k_info[0] < 2 || n_k_info[0] > 100 || n_k_info[1] < 1 || n_k_info[1] > 2000)
                    {
                        streamWriter.WriteLine("Out of range exception!");
                    }
                    else
                    {
                        var matrix = inputData.Skip(1).Select(row => row.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(num => Convert.ToInt32(num)).ToArray()).ToArray();
                        streamWriter.WriteLine(GetMaxSumInPath(matrix, n_k_info[1]));
                    }
                }
            }
        }

        /// <summary>
        /// Отримати максимальну суму чисел по шляху довжиною в pathLength
        /// </summary>
        /// <param name="matrix">Матриця чисел</param>
        /// <param name="pathLength">Довжина шляху</param>
        /// <param name="currentStep">Теперішня позиція на шляху</param>
        /// <param name="matrixRow">Рядок матриці, на якому ми зараз знаходимось</param>
        /// <param name="matrixCol">Стовпчик матриці, на якому ми зараз знаходимось</param>
        /// <returns></returns>
        public static int GetMaxSumInPath(int[][] matrix, int pathLength, int currentStep = 1, int matrixRow = 0, int matrixCol = 0)
        {
            if (currentStep == pathLength)
            {
                return matrix[matrixRow][matrixCol];
            }
            int up = 0, down = 0, left = 0, right = 0;
            if (matrixRow != 0)
            {
                up = matrix[matrixRow][matrixCol] + GetMaxSumInPath(matrix, pathLength, currentStep + 1, matrixRow - 1, matrixCol);
            }
            if (matrixCol != 0)
            {
                left = matrix[matrixRow][matrixCol] + GetMaxSumInPath(matrix, pathLength, currentStep + 1, matrixRow, matrixCol - 1);
            }
            if (matrixRow != matrix.Length - 1)
            {
                down = matrix[matrixRow][matrixCol] + GetMaxSumInPath(matrix, pathLength, currentStep + 1, matrixRow + 1, matrixCol);
            }
            if (matrixCol != matrix.Length - 1)
            {
                right = matrix[matrixRow][matrixCol] + GetMaxSumInPath(matrix, pathLength, currentStep + 1, matrixRow, matrixCol + 1);
            }
            return getMaxValue(up, down, left, right);
        }

        private static int getMaxValue(params int[] values)
        {
            int max = values[0];
            for(int i = 1; i < values.Length; i++)
            {
                if (max < values[i])
                {
                    max = values[i];
                }
            }
            return max;
        }


        /// <summary>
        /// Отримати довжину найбільшої зростаючої підмножини у послідовності
        /// </summary>
        /// <param name="numberList">Послідовність чисел</param>
        /// <returns></returns>
        private static int GetMaxLengthOfIncreasingSubsezuence(List<int> numberList)
        {
            int maxLength = 1;
            int thisLength = 1;
            for (int i = 1; i < numberList.Count; i++)
            {
                if (numberList[i] >= numberList[i - 1])
                {
                    thisLength++;
                }
                else
                {
                    maxLength = maxLength < thisLength ? thisLength : maxLength;
                    thisLength = 1;
                }
            }
            return maxLength;
        }
    }
}