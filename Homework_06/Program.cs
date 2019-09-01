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

            Console.WriteLine("Старт");
            var date = DateTime.Now;



            SaveNumbersToFile2(100_000_000);

            //SaveNumbersToFile(100_000_000);
            //SaveNumbersToFile1(100_000_000);


            //var array = ReadFile("test.txt");
            //var text = new StringBuilder();
            //foreach (var item in array)
            //{
            //    text.Append($"{item}, ");
            //}

            //Console.WriteLine(text);

            var timeSpan = DateTime.Now.Subtract(date);

            Console.WriteLine($"Затраченное время: {timeSpan.TotalMilliseconds}");

            Console.ReadKey();
        }


        //для 100_000_000 чисел выполняется 23 сек
        static void SaveNumbersToFile(int nums)
        {
            using (var sw = new StreamWriter("test.txt"))
            {
                for (int i = 0; i < nums; i++)
                {
                    sw.WriteLine(i + 1);
                }
            }
        }

        //для 100_000_000 чисел выполняется 55 сек
        static void SaveNumbersToFile1(int nums)
        {
            var text = new StringBuilder();

            for (int i = 0; i < nums; i++)
            {
                text.AppendLine($"{i}");
            }

            using (var sw = new StreamWriter("test.txt"))
            {
                sw.Write(text);
            }
        }

        static void SaveNumbersToFile2(int nums)
        {
            using (var fs = new FileStream("test.txt", FileMode.OpenOrCreate))
            {
                for (int i = 0; i < nums; i++)
                {
                    byte[] array = Encoding.Default.GetBytes($"{i}");
                    fs.Write(array, 0, array.Length);
                }
            }
        }

        static string[] ReadFile(string name)
        {
            using (var sw = new StreamReader(name))
            {
                var text = sw.ReadToEnd();
                return text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            }
        }
    }
}
