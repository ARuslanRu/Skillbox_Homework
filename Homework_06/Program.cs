using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Homework_06
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задание
            /// Домашнее задание
            ///
            /// Группа начинающих программистов решила поучаствовать в хакатоне с целью демонстрации
            /// своих навыков. 
            /// 
            /// Немного подумав они вспомнили, что не так давно на занятиях по математике
            /// они проходили тему "свойства делимости целых чисел". На этом занятии преподаватель показывал
            /// пример с использованием фактов делимости. 
            /// Пример заключался в следующем: 
            /// Написав на доске все числа от 1 до N, N = 50, преподаватель разделил числа на несколько групп
            /// так, что если одно число делится на другое, то эти числа попадают в разные руппы. 
            /// В результате этого разбиения получилось M групп, для N = 50, M = 6
            /// 
            /// N = 50
            /// Группы получились такими: 
            /// 
            /// Группа 1: 1
            /// Группа 2: 2 3 5 7 11 13 17 19 23 29 31 37 41 43 47
            /// Группа 3: 4 6 9 10 14 15 21 22 25 26 33 34 35 38 39 46 49
            /// Группа 4: 8 12 18 20 27 28 30 42 44 45 50
            /// Группа 5: 16 24 36 40
            /// Группа 6: 32 48
            /// 
            /// M = 6
            /// 
            /// ===========
            /// 
            /// N = 10
            /// Группы получились такими: 
            /// 
            /// Группа 1: 1
            /// Группа 2: 2 7 9
            /// Группа 3: 3 4 10
            /// Группа 4: 5 6 8
            /// 
            /// M = 4
            /// 
            /// Участники хакатона решили эту задачу, добавив в неё следующие возможности:
            /// 1. Программа считыват из файла (путь к которому можно указать) некоторое N, 
            ///    для которого нужно подсчитать количество групп
            ///    Программа работает с числами N не превосходящими 1 000 000 000
            ///   
            /// 2. В ней есть два режима работы:
            ///   2.1. Первый - в консоли показывается только количество групп, т е значение M
            ///   2.2. Второй - программа получает заполненные группы и записывает их в файл используя один из
            ///                 вариантов работы с файлами
            ///            
            /// 3. После выполения пунктов 2.1 или 2.2 в консоли отображается время, за которое был выдан результат 
            ///    в секундах и миллисекундах
            /// 
            /// 4. После выполнения пунта 2.2 программа предлагает заархивировать данные и если пользователь соглашается -
            /// делает это.
            /// 
            /// Попробуйте составить конкуренцию начинающим программистам и решить предложенную задачу
            /// (добавление новых возможностей не возбраняется)
            ///
            /// * При выполнении текущего задания, необходимо документировать код 
            ///   Как пометками, так и xml документацией
            ///   В обязательном порядке создать несколько собственных методов
            #endregion

            string path; //путь к файлу
            int number; //число из файла
            int numOfGroups; //количество групп
            DateTime date; //начальное время
            TimeSpan timeSpan; //интервал за который выполнилась задача
            List<int>[] numsInGroups; //массив чисел распределенных по группам


            Console.Write("Укажите путь к файлу с числом N \n(если остависть пустым, то по умолчанию будет указан файл number.txt в текущем каталоге): ");
            path = Console.ReadLine();

            if (string.IsNullOrEmpty(path))
            {
                path = "number.txt";
                Console.WriteLine($"Взято значение по умолчанию: {path}");
            }

            while (!File.Exists(path))
            {
                Console.Write($"Файл {path} не существует или неверно указан путь. Укажите путь повторно: ");
                path = Console.ReadLine();
            }

            if (!TryReadNumFromFile(path, out number))
            {
                Console.WriteLine("Не удалось прочитать число из файла! Проверьте содержимое файла! Приложение будет закрыто!");
                Console.ReadKey();
                Environment.Exit(0);
            }

            Console.WriteLine($"Число из файла: {number}");

            Console.WriteLine("---------------");

            Console.WriteLine("Выберите вариант работы (Введите число)" +
                "\n1. Показать только количество групп." +
                "\n2. Получить заполненные группы и записать их в файл.");

            int mode;
            while (!int.TryParse(Console.ReadLine(), out mode) && (mode != 1 || mode != 2))
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод (выберите (1) или (2)): ");
            }

            switch (mode)
            {
                case 1:
                    date = DateTime.Now;

                    numOfGroups = GetNumOfGroups(number);
                    Console.WriteLine($"Количество групп: {numOfGroups}");

                    timeSpan = DateTime.Now.Subtract(date);
                    Console.WriteLine($"Затраченное время: {timeSpan.TotalMilliseconds}");

                    break;
                case 2:
                    date = DateTime.Now;

                    numOfGroups = GetNumOfGroups(number);
                    numsInGroups = AssignNumbersToGroups(number, numOfGroups);

                    int groupNum = 1;

                    using (var sw = new StreamWriter("test.txt"))
                    {
                        foreach (var list in numsInGroups)
                        {
                            sw.Write($"Группа {groupNum}:\n");
                            foreach (var item in list)
                            {
                                sw.Write($"{item} ");
                            }
                            sw.Write("\n");
                            groupNum++;
                        }
                    }

                    timeSpan = DateTime.Now.Subtract(date);
                    Console.WriteLine($"Затраченное время: {timeSpan.TotalMilliseconds}");

                    break;
                default:
                    Console.WriteLine("Что-то пошло не так.");
                    break;
            }

            Console.WriteLine("Заархивировать результат" +
                "\n1. Да" +
                "\n2. Нет");
            while (!int.TryParse(Console.ReadLine(), out mode) && (mode != 1 || mode != 2))
            {
                Console.Write("Введено недопустимое значение." +
                        "\nПовторите ввод (выберите (1) или (2)): ");
            }

            if (mode == 1)
            {
                string source = "test.txt";
                string compressed = "test.zip";
                using (FileStream ss = new FileStream(source, FileMode.OpenOrCreate))
                {
                    using (FileStream ts = File.Create(compressed))   // поток для записи сжатого файла
                    {
                        // поток архивации
                        using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
                        {
                            ss.CopyTo(cs); // копируем байты из одного потока в другой
                            Console.WriteLine("Сжатие файла {0} завершено. Было: {1}  стало: {2}.",
                                              source,
                                              ss.Length,
                                              ts.Length);
                        }
                    }
                }
            }

            Console.ReadKey();
        }

        #region Методы

        /// <summary>
        /// Пытаемся получить номер из файла
        /// </summary>
        /// <param name="path">Путь к файлу с указанием его имени</param>
        /// <param name="number">Считанное число</param>
        /// <returns>Получаем результат удалось ли считать число или нет</returns>
        static bool TryReadNumFromFile(string path, out int number)
        {
            using (var sr = new StreamReader(path))
            {
                return int.TryParse(sr.ReadToEnd(), out number);
            }
        }

        /// <summary>
        /// Вычисляем количество групп
        /// </summary>
        /// <param name="num">максимальное число</param>
        /// <returns>возвращаем количество групп</returns>
        static int GetNumOfGroups(int num)
        {
            return (int)Math.Log(num, 2) + 1;
        }
       
        /// <summary>
        /// распеределяем числа по группам (с большим максимальным числом работает очень долго)
        /// </summary>
        /// <param name="num">максимальное число</param>
        /// <param name="numOfGroups">количество групп для максимального числа</param>
        /// <returns>возвращаем массив чисел распределенных по спискам</returns>
        static List<int>[] AssignNumbersToGroups(int num, int numOfGroups)
        {
            List<int>[] nums = new List<int>[numOfGroups];

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = new List<int>();
            }

            nums[0].Add(1);

            bool isThisGroup = false;

            for (int i = 2; i <= num; i++)
            {
                for (int j = 0; j < numOfGroups; j++)
                {
                    isThisGroup = true;

                    foreach (var item in nums[j])
                    {
                        //Если число делится нацело, прерываем проверку по j-ому массиву и переходим к следующему
                        if (i % item == 0)
                        {
                            isThisGroup = false;
                            break;
                        }
                    }

                    //если число не делится на цело ни на одно число в j-ом массиве, то добавляем число в него и прерываем для перехода к проверки следующего числа.
                    if (isThisGroup)
                    {
                        nums[j].Add(i);
                        break;
                    }
                }
            }
            return nums;
        }

        #endregion   
    }
}
