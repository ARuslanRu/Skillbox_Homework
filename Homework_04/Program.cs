using System;

namespace Homework_Theme_04
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задания
            // Задание 1.
            // Заказчик просит вас написать приложение по учёту финансов
            // и продемонстрировать его работу.
            // Суть задачи в следующем: 
            // Руководство фирмы по 12 месяцам ведет учет расходов и поступлений средств. 
            // За год получены два массива – расходов и поступлений.
            // Определить прибыли по месяцам
            // Количество месяцев с положительной прибылью.
            // Добавить возможность вывода трех худших показателей по месяцам, с худшей прибылью, 
            // если есть несколько месяцев, в некоторых худшая прибыль совпала - вывести их все.
            // Организовать дружелюбный интерфейс взаимодействия и вывода данных на экран

            // Пример
            //       
            // Месяц      Доход, тыс. руб.  Расход, тыс. руб.     Прибыль, тыс. руб.
            //     1              100 000             80 000                 20 000
            //     2              120 000             90 000                 30 000
            //     3               80 000             70 000                 10 000
            //     4               70 000             70 000                      0
            //     5              100 000             80 000                 20 000
            //     6              200 000            120 000                 80 000
            //     7              130 000            140 000                -10 000
            //     8              150 000             65 000                 85 000
            //     9              190 000             90 000                100 000
            //    10              110 000             70 000                 40 000
            //    11              150 000            120 000                 30 000
            //    12              100 000             80 000                 20 000
            // 
            // Худшая прибыль в месяцах: 7, 4, 1, 5, 12
            // Месяцев с положительной прибылью: 10




            // Месяц      Доход, тыс. руб.  Расход, тыс. руб.     Прибыль, тыс. руб.
            //     1              100 000             80 000                 20 000
            //     2              120 000             90 000                 30 000
            //     3               80 000             70 000                 20 000
            //     4               70 000             70 000                 20 000
            //     5              100 000             80 000                 20 000
            //     6              200 000            120 000                 20 000
            //     7              130 000            140 000                -10 000
            //     8              150 000             65 000                -10 000
            //     9              190 000             90 000                -10 000
            //    10              110 000             70 000                -10 000
            //    11              150 000            120 000                 30 000
            //    12              100 000             80 000                 20 000


            // * Задание 2
            // Заказчику требуется приложение строящее первых N строк треугольника паскаля. N < 25
            // 
            // При N = 9. Треугольник выглядит следующим образом:
            //                                 1
            //                             1       1
            //                         1       2       1
            //                     1       3       3       1
            //                 1       4       6       4       1
            //             1       5      10      10       5       1
            //         1       6      15      20      15       6       1
            //     1       7      21      35      35       21      7       1
            //                                                              
            //                                                              
            // Простое решение:                                                             
            // 1
            // 1       1
            // 1       2       1
            // 1       3       3       1
            // 1       4       6       4       1
            // 1       5      10      10       5       1
            // 1       6      15      20      15       6       1
            // 1       7      21      35      35       21      7       1
            // 
            // Справка: https://ru.wikipedia.org/wiki/Треугольник_Паскаля


            // 
            // * Задание 3.1
            // Заказчику требуется приложение позволяющщее умножать математическую матрицу на число
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)#Умножение_матрицы_на_число
            // Добавить возможность ввода количество строк и столцов матрицы и число,
            // на которое будет производиться умножение.
            // Матрицы заполняются автоматически. 
            // Если по введённым пользователем данным действие произвести невозможно - сообщить об этом
            //
            // Пример
            //
            //      |  1  3  5  |   |  5  15  25  |
            //  5 х |  4  5  7  | = | 20  25  35  |
            //      |  5  3  1  |   | 25  15   5  |
            //
            //
            // ** Задание 3.2
            // Заказчику требуется приложение позволяющщее складывать и вычитать математические матрицы
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)#Сложение_матриц
            // Добавить возможность ввода количество строк и столцов матрицы.
            // Матрицы заполняются автоматически
            // Если по введённым пользователем данным действие произвести невозможно - сообщить об этом
            //
            // Пример
            //  |  1  3  5  |   |  1  3  4  |   |  2   6   9  |
            //  |  4  5  7  | + |  2  5  6  | = |  6  10  13  |
            //  |  5  3  1  |   |  3  6  7  |   |  8   9   8  |
            //  
            //  
            //  |  1  3  5  |   |  1  3  4  |   |  0   0   1  |
            //  |  4  5  7  | - |  2  5  6  | = |  2   0   1  |
            //  |  5  3  1  |   |  3  6  7  |   |  2  -3  -6  |
            //
            // *** Задание 3.3
            // Заказчику требуется приложение позволяющщее перемножать математические матрицы
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)
            // Справка https://ru.wikipedia.org/wiki/Матрица_(математика)#Умножение_матриц
            // Добавить возможность ввода количество строк и столцов матрицы.
            // Матрицы заполняются автоматически
            // Если по введённым пользователем данным действие произвести нельзя - сообщить об этом
            //  
            //  |  1  3  5  |   |  1  3  4  |   | 22  48  57  |
            //  |  4  5  7  | х |  2  5  6  | = | 35  79  95  |
            //  |  5  3  1  |   |  3  6  7  |   | 14  36  45  |
            //
            //  
            //                  | 4 |   
            //  |  1  2  3  | х | 5 | = | 32 |
            //                  | 6 |  
            //
            #endregion


            //растягиваем окно консоли на ширину экрана
            Console.WindowWidth = Console.LargestWindowWidth;
            var random = new Random();
            int numberOfLines;
            int numberOfColumns;

            #region Задание 1

            int[,] array = new int[12, 4];

            for (int i = 0; i < 12; i++)
            {
                array[i, 0] = i + 1;
                array[i, 1] = random.Next(10000, 200000);
                array[i, 2] = random.Next(10000, 200000);
                array[i, 3] = array[i, 1] - array[i, 2];
            }

            //Для проверки на совпадающие значения худшей прибыли
            array[2, 3] = -199000;
            array[5, 3] = -199000;

            //Выводим на экран содержимое таблички
            Console.WriteLine("{0,10} {1,20} {2,20} {3,20}", "Месяц", "Доход, тыс. руб.", "Расход, тыс.руб.", "Прибыль, тыс. руб.");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.WriteLine("{0,10:### ###} {1,20:### ###} {2,20:### ###} {3,20:### ###}", array[i, 0], array[i, 1], array[i, 2], array[i, 3]);
            }
            Console.WriteLine();

            var profitArray = new int[array.GetLength(0)];
            for (int i = 0; i < profitArray.Length; i++)
            {
                profitArray[i] = array[i, 3];
            }

            var threeMinProfits = Helpers.GetMin(profitArray, 3);

            //Выводим на экран месяцы с худшей прибылью
            Console.Write("Худшая прибыль в месяцах: ");
            for (int i = 0; i < threeMinProfits.Length; i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (array[j, 3] == threeMinProfits[i])
                    {
                        Console.Write($"{array[j, 0]} ");
                    }
                }
            }
            Console.WriteLine();

            //Подсчитываем месяцы с положительной прибылью и выводим на экран
            int count = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, 3] > 0)
                {
                    count++;
                }
            }
            Console.WriteLine($"Месяцев с положительной прибылью: {count}");

            #endregion

            #region Задание 2
            var pascalTriangle = Helpers.PascalTriangle(25);

            //вывод немного смещается если в числе боее 4 цифр
            //можно увеличить число символов для поля вывода, но тогда не помещается в консоль
            foreach (var item in pascalTriangle)
            {
                string str = "";
                foreach (var e in item)
                {
                    str += string.Format("{0,4}", e) + string.Format("{0,4}", " ");
                }
                Helpers.WriteLineCenter(str);
            }
            Console.WriteLine();

            #endregion

            #region Задание 3.1

            Console.Write("Введите количество строк в матрице: ");
            while (!int.TryParse(Console.ReadLine(), out numberOfLines) || numberOfLines < 0)
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод: ");
            }

            Console.Write("Введите количество столбцов в матрице: ");
            while (!int.TryParse(Console.ReadLine(), out numberOfColumns) || numberOfColumns < 0)
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод: ");
            }

            Console.Write("Введите число на которое умножается матрица: ");
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод: ");
            }


            int[,] matrix = new int[numberOfLines, numberOfColumns];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(-9, 10);
                }
            }

            Helpers.MultiplicationMatrixToNumber(matrix, number);

            Console.WriteLine();
            #endregion

            #region Задание 3.2

            Console.Write("Введите количество строк в матрицах: ");
            while (!int.TryParse(Console.ReadLine(), out numberOfLines) || numberOfLines < 0)
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод: ");
            }

            Console.Write("Введите количество столбцов в матрицах: ");
            while (!int.TryParse(Console.ReadLine(), out numberOfColumns) || numberOfColumns < 0)
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод: ");
            }

            int[,] matrixOne = new int[numberOfLines, numberOfColumns];

            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {
                for (int j = 0; j < matrixOne.GetLength(1); j++)
                {
                    matrixOne[i, j] = random.Next(-9, 10);
                }
            }

            int[,] matrixTwo = new int[numberOfLines, numberOfColumns];

            for (int i = 0; i < matrixTwo.GetLength(0); i++)
            {
                for (int j = 0; j < matrixTwo.GetLength(1); j++)
                {
                    matrixTwo[i, j] = random.Next(-9, 10);
                }
            }


            Helpers.AdditionMatrixWithMatrix(matrixOne, matrixTwo);

            Console.WriteLine();

            Helpers.DifferenceMatrixWithMatrix(matrixOne, matrixTwo);

            Console.WriteLine();

            #endregion

            #region Задание 3.3

            Console.Write("Введите количество строк в первой матрице: ");
            while (!int.TryParse(Console.ReadLine(), out numberOfLines) || numberOfLines < 0)
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод: ");
            }

            Console.Write("Введите количество столбцов в первой матрице: ");
            while (!int.TryParse(Console.ReadLine(), out numberOfColumns) || numberOfColumns < 0)
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод: ");
            }

            int[,] matrixThree = new int[numberOfLines, numberOfColumns];
            for (int i = 0; i < matrixThree.GetLength(0); i++)
            {
                for (int j = 0; j < matrixThree.GetLength(1); j++)
                {
                    matrixThree[i, j] = random.Next(-9, 10);
                }
            }

            Console.Write("Введите количество строк во второй матрице: ");
            while (!int.TryParse(Console.ReadLine(), out numberOfLines) || numberOfLines < 0 || numberOfLines != matrixThree.GetLength(1))
            {
                Console.Write("Введено недопустимое значение или" +
                    "\nколичество строк во второй матрице должно совпадать с количеством столбцов в первой" +
                    "\nПовторите ввод: ");
            }

            Console.Write("Введите количество строк во второй матрице: ");
            while (!int.TryParse(Console.ReadLine(), out numberOfColumns) || numberOfColumns < 0 || numberOfColumns != matrixThree.GetLength(0))
            {
                Console.Write("Введено недопустимое значение или" +
                    "\nколичество столбцов во второй матрице должно совпадать с количеством строк в первой" +
                    "\nПовторите ввод: ");
            }

            int[,] matrixFour = new int[numberOfLines, numberOfColumns];
            for (int i = 0; i < matrixFour.GetLength(0); i++)
            {
                for (int j = 0; j < matrixFour.GetLength(1); j++)
                {
                    matrixFour[i, j] = random.Next(-9, 10);
                }
            }

            Helpers.MultiplicationMatrixWithMatrix(matrixThree, matrixFour);

            Console.WriteLine("Тест 1");
            int[,] test1_1 = { { 1, 2, 3 } };
            int[,] test1_2 = { { 4 }, { 5 }, { 6 } };
            Helpers.MultiplicationMatrixWithMatrix(test1_1, test1_2);

            Console.WriteLine("Тест 2");
            int[,] test2_1 = { { 1, 3, 5 }, { 4, 5, 7 }, { 5, 3, 1 } };
            int[,] test2_2 = { { 1, 3, 4 }, { 2, 5, 6 }, { 3, 6, 7 } };
            Helpers.MultiplicationMatrixWithMatrix(test2_1, test2_2);

            #endregion

            Console.ReadKey();
        }
    }
}
