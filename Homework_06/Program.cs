﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            var num = ReadNumFromFile("num.txt");
            Console.WriteLine($"Число из файла: {num}");
            var numOfGroups = GetNumOfGroups(num);
            Console.WriteLine($"Количество групп: {numOfGroups}");




            Console.WriteLine("Старт");
            var date = DateTime.Now;



            var L = AssignNumbersToGroups(num, numOfGroups);

            foreach (var list in L)
            {
                Console.WriteLine($"Группа: ");
                foreach (var item in list)
                {
                    Console.Write($"{item}, ");
                }
                Console.WriteLine();
            }



            var timeSpan = DateTime.Now.Subtract(date);

            Console.WriteLine($"Затраченное время: {timeSpan.TotalMilliseconds}");

            Console.ReadKey();
        }


        #region Задача 1

        static int ReadNumFromFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                return int.Parse(sr.ReadToEnd());
            }
        }

        static int GetNumOfGroups(int num)
        {
            return (int)Math.Log(num, 2) + 1;
        }

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

        #region Тест
        //для 100_000_000 чисел выполняется 33 сек
        static void SaveNumbersToFile_1(int nums)
        {
            using (var sw = new StreamWriter("test.txt"))
            {
                for (int i = 0; i < nums; i++)
                {
                    sw.Write($"{i + 1}\n");
                }
            }
        }

        //для 100_000_000 чисел выполняется 30 сек
        static void SaveNumbersToFile_2(int nums)
        {
            using (var sw = new StreamWriter("test.txt", true, Encoding.UTF8, 65536))
            {
                for (int i = 0; i < nums; i++)
                {
                    sw.Write($"{i + 1}\n");
                }
            }
        }


        //для 100_000_000 чисел выполняется 44 сек
        static void SaveNumbersToFile1(int nums)
        {
            var text = new StringBuilder();

            for (int i = 0; i < nums; i++)
            {
                text.Append($"{i + 1}\n");
            }

            using (var sw = new StreamWriter("test.txt"))
            {
                sw.Write(text);
            }
        }

        //для 100_000_000 чисел выполняется 40 сек
        static void SaveNumbersToFile_3(int nums)
        {
            using (var fs = new FileStream("test.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 65536, true))
            {
                for (int i = 0; i < nums; i++)
                {
                    byte[] array = Encoding.Default.GetBytes($"{i + 1}\n");
                    fs.Write(array, 0, array.Length);
                }
            }
        }

        //
        static int[] ReadFile_1(string name)
        {
            var text = new StringBuilder();
            using (var sw = new StreamReader(name))
            {
                text.Append(sw.ReadToEnd());
            }
            return text.ToString().Trim().Split('\n').Select(x => int.Parse(x)).ToArray();
        }

        //
        static int[] ReadFile_2(string fileName)
        {
            byte[] bytes;
            using (var fsSource = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fsSource.Length];
                int numBytesToRead = (int)fsSource.Length;
                int numBytesRead = 0;

                while (numBytesToRead > 0)
                {
                    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                    if (n == 0)
                        break;
                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                numBytesToRead = bytes.Length;
            }
            return Encoding.ASCII.GetString(bytes).Trim().Split('\n').Select(x => int.Parse(x)).ToArray();
        }

        static int[] ReadFile_3(string name)
        {
            IEnumerable<string> nums = File.ReadAllLines(name);

            return null;

            //return text.ToString().Trim().Split('\n').Select(x => int.Parse(x)).ToArray();
        }

        static byte[] ReadFile_4(string fileName)
        {
            byte[] bytes;
            using (var fsSource = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fsSource.Length];
                int numBytesToRead = (int)fsSource.Length;
                int numBytesRead = 0;

                while (numBytesToRead > 0)
                {
                    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                    if (n == 0)
                        break;
                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                numBytesToRead = bytes.Length;
            }
            return bytes;
        }

        static void Store()
        {
            double sum = 0;
            //Подсчитываем количество символов в цифрах от 1 до 100 000 000
            for (long i = 1, j = 1; i < 100_000_000; i *= 10, j++)
            {
                Console.WriteLine($"i = {i}");
                Console.WriteLine($"j = {j}");
                Console.WriteLine(9 * i);
                sum += 9 * i * j;
                Console.WriteLine(sum);
            }

            //Добавляем количество символов перехода на следующую строку
            sum += 99_999_997;

            //Умножаем на количество байт в символе
            sum *= 2;
            Console.WriteLine(sum / 1024 / 1024);

        }

        //Ругается если пробую создать массив из 1_000_000_000 элементов
        static int[] CreateNumsArray(int num)
        {
            int[] nums = new int[num];
            for (int i = 0; i < num; i++)
            {
                nums[i] = i;
            }
            return nums;
        }

        #endregion
    }
}