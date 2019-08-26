using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HomeWork_5
{
    class Methods
    {
        /// <summary>
        /// Умножение числа на матрицу
        /// </summary>
        /// <param name="num">Число</param>
        /// <param name="matrix">Матрица</param>
        /// <returns>Возвращает матрицу умноженную на число</returns>
        public static int[,] MultiplicationMatrixToNumber(int num, int[,] matrix)
        {
            int[,] resultMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    resultMatrix[i, j] = matrix[i, j] * num;
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Сложение двух матриц
        /// </summary>
        /// <param name="matrixOne">Матрица 1</param>
        /// <param name="matrixTwo">Матрица 2</param>
        /// <returns>Возвращает сумму двух матриц</returns>
        public static int[,] AdditionMatrixWithMatrix(int[,] matrixOne, int[,] matrixTwo)
        {

            if (matrixOne.GetLength(0) != matrixTwo.GetLength(0) || matrixOne.GetLength(1) != matrixTwo.GetLength(1))
            {
                Console.WriteLine($"Ошибка метода {nameof(AdditionMatrixWithMatrix)}: Размерность двух матриц должна быть одинаковой.");
                return null;
            }

            int[,] resultMatrix = new int[matrixOne.GetLength(0), matrixOne.GetLength(1)];

            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {

                for (int j = 0; j < matrixOne.GetLength(1); j++)
                {
                    resultMatrix[i, j] = matrixOne[i, j] + matrixTwo[i, j];
                }
            }
            return resultMatrix;
        }

        /// <summary>
        /// Умножение двух матриц
        /// </summary>
        /// <param name="matrixOne">Матрица 1</param>
        /// <param name="matrixTwo">Матрица 2</param>
        /// <returns>Возвращает произведение двух матриц</returns>
        public static int[,] MultiplicationMatrixWithMatrix(int[,] matrixOne, int[,] matrixTwo)
        {
            if (matrixOne.GetLength(0) != matrixTwo.GetLength(1) || matrixOne.GetLength(1) != matrixTwo.GetLength(0))
            {
                Console.WriteLine($"Ошибка {nameof(MultiplicationMatrixWithMatrix)}: Количество строк первой матрицы должно соответсвовать количеству столбцов второй и наоборот");
                return null;
            }

            int[,] resultMatrix = new int[matrixOne.GetLength(0), matrixTwo.GetLength(1)];

            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {
                for (int j = 0; j < matrixTwo.GetLength(1); j++)
                {
                    for (int x = 0; x < matrixOne.GetLength(1); x++)
                    {
                        resultMatrix[i, j] += matrixOne[i, x] * matrixTwo[x, j];
                    }
                }
            }
            return resultMatrix;
        }

        /// <summary>
        /// Возвращает слово содержащее минимальное количество букв
        /// </summary>
        /// <param name="text">Текст</param>
        /// <returns>Возвращает слово содержащее минимальное количество букв</returns>
        public static string MinСharacters(string text)
        {
            string pattern = @"[^,.\s]+";
            Regex rgx = new Regex(pattern);
            var matchCollection = rgx.Matches(text);

            int minLength = int.MaxValue;
            string minWord = "";

            foreach (Match item in matchCollection)
            {
                if (item.Length < minLength)
                {
                    minLength = item.Length;
                    minWord = item.Value;
                }
            }
            return minWord;
        }

        /// <summary>
        /// Метод возвращающий слово(слова) с максимальным количеством букв
        /// </summary>
        /// <param name="text">текст</param>
        /// <returns>Возвращает слова с максимальным количеством букв</returns>
        public static string[] MaxСharacters(string text)
        {
            string pattern = @"[^,.\s]+";
            Regex rgx = new Regex(pattern);
            var matchCollection = rgx.Matches(text);

            int maxLength = int.MinValue;
            foreach (Match item in matchCollection)
            {
                if (item.Length > maxLength)
                {
                    maxLength = item.Length;
                }
            }

            var list = new List<string>();
            foreach (Match item in matchCollection)
            {
                if (item.Length == maxLength)
                {
                    list.Add(item.Value);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// Удаляет все кратные рядом стоящие символы, оставив по одному
        /// </summary>
        /// <param name="text">Текст</param>
        /// <returns>Возвращает новый текст без повторяющихся подряд символов</returns>
        public static string RemoveDuplicates(string text)
        {
            string resultString = text[0].ToString();

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i].ToString().ToUpper() != text[i - 1].ToString().ToUpper())
                {
                    resultString += text[i];
                }
            }
            return resultString;
        }

        /// <summary>
        /// Определяет арифметическая или геомертическая последовательность 
        /// </summary>
        /// <param name="numbers">массив чисел</param>
        /// <returns>Возвращает ответ в виде фраз: "Не является прогрессией", "Арифметическая прогрессия", "Геометрическая прогрессия"</returns>
        public static string IsArithmeticOrGeometricProgression(double[] numbers)
        {
            if (numbers.Length < 3)
            {
                return "Не является прогрессией";
            }

            bool isArithmeticProgression = false;
            for (int i = 2; i < numbers.Length; i++)
            {
                if (numbers[i] - numbers[i - 1] == numbers[i - 1] - numbers[i - 2])
                {
                    isArithmeticProgression = true;
                }
                else
                {
                    isArithmeticProgression = false;
                    break;
                }
            }

            bool isGeometricProgression = false;
            if (numbers[0] != 0 && numbers[1] != 0 && numbers[2] != 0)
            {
                for (int i = 2; i < numbers.Length; i++)
                {
                    if (numbers[i] / numbers[i - 1] == numbers[i - 1] / numbers[i - 2])
                    {
                        isGeometricProgression = true;
                    }
                    else
                    {
                        isGeometricProgression = false;
                        break;
                    }
                }
            }

            if (isArithmeticProgression)
            {
                return "Арифметическая прогрессия";
            }

            if (isGeometricProgression)
            {
                return "Геометрическая прогрессия";
            }


            return "Не является прогрессией";
        }

        /// <summary>
        /// Вывод матрицы в консоль
        /// </summary>
        /// <param name="matirix">Матрица</param>
        public static void PrintMatrix(int[,] matrix)
        {
            if (matrix is null)
            {
                Console.WriteLine($"Ошибка метода {nameof(PrintMatrix)}: Ссылка на объект не указывает на экземпляр объекта.");
                return;
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j],3}");
                }
                Console.WriteLine();
            }
        }
    }
}
