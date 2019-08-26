using System;

namespace Homework_Theme_04
{
    class Helpers
    {
        /// <summary>
        /// Получаем заданое количество минимальных значений из исходного массива
        /// </summary>
        /// <param name="profit">Исходный массив</param>
        /// <param name="countMin">Количество минимальных значений</param>
        /// <returns></returns>
        public static int[] GetMin(int[] profit, int countMin)
        {
            Array.Sort(profit);

            int[] arrayMin = new int[countMin];
            //Первый элемент является минимальным
            arrayMin[0] = profit[0];

            //Начинаем со второго элемента, просматривать массив.
            int counter = 1;
            for (int i = 1; i < profit.Length; i++)
            {
                //Если количество минимальных значений которые нужно найти меньше счетчика, то выходим из цикла
                if (counter >= countMin)
                {
                    break;
                }

                //Если текущий элемент исходного массива равен предудущему элементу, то переходим к следующему элементу
                if (profit[i] == profit[i - 1])
                {
                    continue;
                }
                else
                {
                    arrayMin[counter] = profit[i];
                    counter++;
                }
            }
            return arrayMin;
        }

        /// <summary>
        /// Вывод строки по центру консоли
        /// </summary>
        /// <param name="str">Строка</param>
        public static void WriteLineCenter(string str)
        {
            //вычисляем ширину поля с учетом длины строки
            var padding = Console.WindowWidth / 2 + str.Length / 2;
            //печатаем строку в поле с выравниванием по правому краю
            Console.WriteLine("{0," + padding + "}", str);
        }

        /// <summary>
        /// Отрисовка треугольника паскаля
        /// </summary>
        /// <param name="lineCount">Размер</param>
        /// <returns></returns>
        public static int[][] PascalTriangle(int lineCount)
        {
            //создаем массив массивов размерностью lineCount
            int[][] array = new int[lineCount][];

            for (int i = 0; i < lineCount; i++)
            {
                //размерность каждого внутреннего массива увеличивается с каждой итерацией на 1
                array[i] = new int[i + 1];

                //рассчитываем элементы массива
                for (int j = 0; j < i + 1; j++)
                {
                    //если элементы первый или последний то равен 1
                    if (j == 0 || j == i)
                    {
                        array[i][j] = 1;
                    }
                    //иначе элемент равен сумме элементов с таким же индексом и индексом минус 1 из пердыдущего массива 
                    else
                    {
                        array[i][j] = array[i - 1][j] + array[i - 1][j - 1];
                    }
                }
            }
            return array;
        }

        /// <summary>
        /// Умножение матрицы на число и вывод результата в консоль
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="num"></param>
        public static void MultiplicationMatrixToNumber(int[,] matrix, int num)
        {
            Console.WriteLine("Умножение матрицы на число");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix.GetLength(0) / 2 == i)
                {
                    Console.Write($" {num} x ");
                }
                else
                {
                    Console.Write("     ");
                }

                Console.Write("| ");

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0,2}", matrix[i, j]);
                }
                Console.Write(" |");

                if (matrix.GetLength(0) / 2 == i)
                {
                    Console.Write(" = ");
                }
                else
                {
                    Console.Write("   ");
                }
                Console.Write("| ");

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0,4}", matrix[i, j] * num);
                }
                Console.Write(" |");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Сложение двух матриц и вывод результата в консоль
        /// </summary>
        /// <param name="matrixOne">Первая матрица</param>
        /// <param name="matrixTwo">Вторая матрица</param>
        public static void AdditionMatrixWithMatrix(int[,] matrixOne, int[,] matrixTwo)
        {
            Console.WriteLine("Сложение матриц");
            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {
                Console.Write("| ");

                for (int j = 0; j < matrixOne.GetLength(1); j++)
                {
                    Console.Write("{0,3}", matrixOne[i, j]);
                }
                Console.Write(" |");


                if (matrixOne.GetLength(0) / 2 == i)
                {
                    Console.Write(" + ");
                }
                else
                {
                    Console.Write("   ");
                }


                Console.Write("| ");
                for (int j = 0; j < matrixTwo.GetLength(1); j++)
                {
                    Console.Write("{0,3}", matrixTwo[i, j]);
                }
                Console.Write(" |");

                if (matrixOne.GetLength(0) / 2 == i)
                {
                    Console.Write(" = ");
                }
                else
                {
                    Console.Write("   ");
                }

                Console.Write("| ");
                for (int j = 0; j < matrixTwo.GetLength(1); j++)
                {
                    Console.Write("{0,3}", matrixOne[i, j] + matrixTwo[i, j]);
                }
                Console.Write(" |");

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Вычитание двух матриц и вывод результата в консоль
        /// </summary>
        /// <param name="matrixOne">Первая матрица</param>
        /// <param name="matrixTwo">Вторая матрица</param>
        public static void DifferenceMatrixWithMatrix(int[,] matrixOne, int[,] matrixTwo)
        {
            Console.WriteLine("Вычитание матриц");
            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {
                Console.Write("| ");

                for (int j = 0; j < matrixOne.GetLength(1); j++)
                {
                    Console.Write("{0,3}", matrixOne[i, j]);
                }
                Console.Write(" |");


                if (matrixOne.GetLength(0) / 2 == i)
                {
                    Console.Write(" - ");
                }
                else
                {
                    Console.Write("   ");
                }


                Console.Write("| ");
                for (int j = 0; j < matrixTwo.GetLength(1); j++)
                {
                    Console.Write("{0,3}", matrixTwo[i, j]);
                }
                Console.Write(" |");

                if (matrixOne.GetLength(0) / 2 == i)
                {
                    Console.Write(" = ");
                }
                else
                {
                    Console.Write("   ");
                }

                Console.Write("| ");
                for (int j = 0; j < matrixTwo.GetLength(1); j++)
                {
                    Console.Write("{0,3}", matrixOne[i, j] - matrixTwo[i, j]);
                }
                Console.Write(" |");

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Перемножение матриц и вывод результата в консоль
        /// </summary>
        /// <param name="matrixOne">Первая матрица</param>
        /// <param name="matrixTwo">Вторая матрица</param>
        public static void MultiplicationMatrixWithMatrix(int[,] matrixOne, int[,] matrixTwo)
        {
            //Вычисляем результат перемножения матриц
            int[,] result = new int[matrixOne.GetLength(0), matrixTwo.GetLength(1)];

            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {
                for (int j = 0; j < matrixTwo.GetLength(1); j++)
                {
                    for (int x = 0; x < matrixOne.GetLength(1); x++)
                    {
                        result[i, j] += matrixOne[i, x] * matrixTwo[x, j];
                    }
                }
            }

            //Выбираем максимальное количество строк при выводе результата в консоль
            int lines;
            if (matrixOne.GetLength(0) > matrixTwo.GetLength(0))
            {
                lines = matrixOne.GetLength(0);
            }
            else
            {
                lines = matrixTwo.GetLength(0);
            }

            //Вспомогательные переменные счетчиков текущей строки для каждой матрицы
            int lineNowOne = 0;
            int lineNowTwo = 0;
            int lineNowRes = 0;

            //Вспомогательные переменные для вычисления пустых строк
            int emptyLines, emptyLinesBefore, emptyLinesAfter;


            //Начало построчного вывода в консоль
            for (int i = 0; i < lines; i++)
            {
                //Вычисляем количество пустых строк в первой матрице, их количестово в начале и конце вывода
                emptyLines = lines - matrixOne.GetLength(0);
                emptyLinesBefore = emptyLines / 2;
                emptyLinesAfter = emptyLines % 2 == 0 ? emptyLines / 2 : emptyLines / 2 + 1;

                //Стравниваем индекс текущей строки с количеством пустых строк вначале и количеством пустых строк в конце
                //Если текущий индекс попадает в допустимый интервал то выводим строку в консоль и индексируем вспомогательный счетчик для первой матрицы, 
                //для последующего отображения следующей строки этой матрицы
                if (i + 1 > emptyLinesBefore && i < lines - emptyLinesAfter)
                {
                    Console.Write("| ");
                    for (int j = 0; j < matrixOne.GetLength(1); j++)
                    {
                        Console.Write("{0,2}", matrixOne[lineNowOne, j]);
                    }
                    Console.Write(" |");
                    lineNowOne++;
                }
                //Иначе выводим пустую строку
                else
                {
                    Console.Write("  ");
                    for (int j = 0; j < matrixOne.GetLength(1); j++)
                    {
                        Console.Write("{0,2}", " ");
                    }
                    Console.Write("  ");
                }

                //Вывод знака умножения по центру
                if (lines / 2 == i)
                {
                    Console.Write(" x ");
                }
                else
                {
                    Console.Write("   ");
                }

                //Вычисляем количество пустых строк во второй матрице, их количестово в начале и конце вывода
                emptyLines = lines - matrixTwo.GetLength(0);
                emptyLinesBefore = emptyLines / 2;
                emptyLinesAfter = emptyLines % 2 == 0 ? emptyLines / 2 : emptyLines / 2 + 1;

                //Стравниваем индекс текущей строки с количеством пустых строк вначале и количеством пустых строк в конце
                //Если текущий индекс попадает в допустимый интервал то выводим строку в консоль и индексируем вспомогательный счетчик для второй матрицы, 
                //для последующего отображения следующей строки этой матрицы
                if (i + 1 > emptyLinesBefore && i  < lines - emptyLinesAfter)
                {
                    Console.Write("| ");
                    for (int j = 0; j < matrixTwo.GetLength(1); j++)
                    {
                        Console.Write("{0,2}", matrixTwo[lineNowTwo, j]);
                    }
                    Console.Write(" |");
                    lineNowTwo++;
                }
                //Иначе выводим пустую строку
                else
                {
                    Console.Write("  ");
                    for (int j = 0; j < matrixTwo.GetLength(1); j++)
                    {
                        Console.Write("{0,2}", " ");
                    }
                    Console.Write("  ");
                }

                //Вывод знака равно по центру
                if (lines / 2 == i)
                {
                    Console.Write(" = ");
                }
                else
                {
                    Console.Write("   ");
                }

                //Вычисляем количество пустых строк в результирующей матрице, их количестово в начале и конце вывода
                emptyLines = lines - result.GetLength(0);
                emptyLinesBefore = emptyLines / 2;
                emptyLinesAfter = emptyLines % 2 == 0 ? emptyLines / 2 : emptyLines / 2 + 1;

                //Стравниваем индекс текущей строки с количеством пустых строк вначале и количеством пустых строк в конце
                //Если текущий индекс попадает в допустимый интервал то выводим строку в консоль и индексируем вспомогательный счетчик для результирующей матрицы, 
                //для последующего отображения следующей строки этой матрицы
                if (i + 1 > emptyLinesBefore && i  < lines - emptyLinesAfter)
                {
                    Console.Write("| ");
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        Console.Write("{0,4}", result[lineNowRes, j]);
                    }
                    Console.Write(" |");
                    lineNowRes++;
                }
                //Иначе выводим пустую строку
                else
                {
                    Console.Write("  ");
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        Console.Write("{0,2}", " ");
                    }
                    Console.Write("  ");
                }

                Console.WriteLine();
            }
        }
    }
}
