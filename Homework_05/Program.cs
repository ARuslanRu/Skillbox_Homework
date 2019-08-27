using System;

namespace HomeWork_5
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задания

            // Домашнее задание
            // Требуется написать несколько методов
            //
            // Задание 1.
            // Воспользовавшись решением задания 3 четвертого модуля
            // 1.1. Создать метод, принимающий число и матрицу, возвращающий матрицу умноженную на число
            // 1.2. Создать метод, принимающий две матрицу, возвращающий их сумму
            // 1.3. Создать метод, принимающий две матрицу, возвращающий их произведение
            //
            // Задание 2.
            // 1. Создать метод, принимающий  текст и возвращающий слово, содержащее минимальное количество букв
            // 2.* Создать метод, принимающий  текст и возвращающий слово(слова) с максимальным количеством букв 
            // Примечание: слова в тексте могут быть разделены символами (пробелом, точкой, запятой) 
            // Пример: Текст: "A ББ ВВВ ГГГГ ДДДД  ДД ЕЕ ЖЖ ЗЗЗ"
            // 1. Ответ: А
            // 2. ГГГГ, ДДДД
            //
            // Задание 3. Создать метод, принимающий текст. 
            // Результатом работы метода должен быть новый текст, в котором
            // удалены все кратные рядом стоящие символы, оставив по одному 
            // Пример: ПППОООГГГООООДДДААА >>> ПОГОДА
            // Пример: Ххххоооорррооошшшиий деееннннь >>> хороший день
            // 
            // Задание 4. Написать метод принимающий некоторое количесво чисел, выяснить
            // является заданная последовательность элементами арифметической или геометрической прогрессии
            // 
            // Примечание
            //             http://ru.wikipedia.org/wiki/Арифметическая_прогрессия
            //             http://ru.wikipedia.org/wiki/Геометрическая_прогрессия
            //
            // *Задание 5
            // Вычислить, используя рекурсию, функцию Аккермана:
            // A(2, 5), A(1, 2)
            // A(n, m) = m + 1, если n = 0,
            //         = A(n - 1, 1), если n <> 0, m = 0,
            //         = A(n - 1, A(n, m - 1)), если n> 0, m > 0.
            // 
            // Весь код должен быть откоммментирован

            #endregion

            #region Задание 1
            Console.WriteLine("Задание 1:");
            int[,] matrix1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            int[,] matrix2 = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            int[,] matrixTestError = { { 1, 1, 1 }, { 1, 1, 1 } };

            int[,] matrix3 = { { 1, 2, 3 } };
            int[,] matrix4 = { { 1 }, { 2 }, { 3 } };
            int num = 5;

            var m1 = Methods.MultiplicationMatrixToNumber(num, matrix1);
            Methods.PrintMatrix(m1);
            Console.WriteLine();

            var m2 = Methods.AdditionMatrixWithMatrix(matrix1, matrix2);
            Methods.PrintMatrix(m2);
            Console.WriteLine();

            var mError1 = Methods.AdditionMatrixWithMatrix(matrix1, matrixTestError);
            Methods.PrintMatrix(mError1);
            Console.WriteLine();

            var m3 = Methods.MultiplicationMatrixWithMatrix(matrix1, matrix2);
            Methods.PrintMatrix(m3);
            Console.WriteLine();

            var m4 = Methods.MultiplicationMatrixWithMatrix(matrix3, matrix4);
            Methods.PrintMatrix(m4);
            Console.WriteLine();

            var m5 = Methods.MultiplicationMatrixWithMatrix(matrix4, matrix3);
            Methods.PrintMatrix(m5);
            Console.WriteLine();

            var mError2 = Methods.MultiplicationMatrixWithMatrix(matrix1, matrixTestError);
            Methods.PrintMatrix(mError2);
            Console.WriteLine();

            #endregion

            #region Задание 2
            Console.WriteLine("Задание 2:");
            var text = "A ББ ВВВ ГГГГ ДДДД  ДД ЕЕ ЖЖ ЗЗЗ";

            var str1 = Methods.MinСharacters(text);
            Console.WriteLine(str1);

            var str2 = Methods.MaxСharacters(text);
            foreach (var item in str2)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            Console.WriteLine();

            #endregion

            #region Задание 3
            Console.WriteLine("Задание 3:");
            var text1 = "ПППОООГГГООООДДДААА";
            var text2 = "Ххххоооорррооошшшиий деееннннь";

            var t1 = Methods.RemoveDuplicates(text1);
            var t2 = Methods.RemoveDuplicates(text2);

            Console.WriteLine(t1);
            Console.WriteLine(t2);

            Console.WriteLine();

            #endregion

            #region Задание 4
            Console.WriteLine("Задание 4:");

            double[] testArray01 = { 1, 3, 5, 7 };
            double[] testArray02 = { 1, 2, 3, 4, 5, 6, 7 };
            double[] testArray03 = { 1, 2, 3, 5, 6, 7 };
            double[] testArray04 = { 1, 2, 4, 8, 16, 32 };
            double[] testArray05 = { 1, 2, 4, 8, 16, 31 };
            double[] testArray06 = { 1, 0, 5, 7 };
            double[] testArray07 = { 0, 2, 4, 8 };
            double[] testArray08 = { 1, 2, 4, 0, 8, 16, 32 };
            double[] testArray09 = { -5, -3, -1, 1, 3, 5, 7 };
            double[] testArray10 = { -1, -2, -4, -8, -16, -32 };
            double[] testArray11 = { -1, 2, -4, 8, -16, 32 };
            double[] testArray12 = { 1, -1, 1, -1, 1 };
            double[] testArray13 = { 50, 25, 12.5, 6.25, 3.125 };

            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray01));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray02));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray03));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray04));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray05));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray06));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray07));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray08));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray09));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray10));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray11));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray12));
            Console.WriteLine(Methods.IsArithmeticOrGeometricProgression(testArray13));

            Console.WriteLine();

            #endregion

            #region Задание 5

            Console.WriteLine("Задание 5:");

            Console.WriteLine($"А(2,5): {Methods.AckermanFunction(2, 5)}");
            Console.WriteLine($"А(3,3): {Methods.AckermanFunction(3, 3)}");
            Console.WriteLine($"А(1,2): {Methods.AckermanFunction(1, 2)}");
            Console.WriteLine($"А(3,5): {Methods.AckermanFunction(3, 5)}");

            #endregion

            Console.ReadKey();
        }
    }
}


