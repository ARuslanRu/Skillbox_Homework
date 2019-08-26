using System;

namespace Homework_Theme_01
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задание

            // Заказчик просит написать программу «Записная книжка». Оплата фиксированная - $ 120.

            // В данной программе, должна быть возможность изменения значений нескольких переменных для того,
            // чтобы персонифецировать вывод данных, под конкретного пользователя.

            // Для этого нужно: 
            // 1. Создать несколько переменных разных типов, в которых могут храниться данные
            //    - имя;
            //    - возраст;
            //    - рост;
            //    - баллы по трем предметам: история, математика, русский язык;

            // 2. Реализовать в системе автоматический подсчёт среднего балла по трем предметам, 
            //    указанным в пункте 1.

            // 3. Реализовать возможность печатки информации на консоли при помощи 
            //    - обычного вывода;
            //    - форматированного вывода;
            //    - использования интерполяции строк;

            // 4. Весь код должен быть откомментирован с использованием обычных и хml-комментариев

            // **
            // 5. В качестве бонусной части, за дополнительную оплату $50, заказчик просит реализовать 
            //    возможность вывода данных в центре консоли.

            #endregion

            var record = new NotebookRecord();

            record.Name = "Иван";
            record.Age = 15;
            record.Height = 170;
            record.HistoryScore = 5;
            record.MathematicsScore = 4;
            record.RussianScore = 5;

            record.PrintInfoNormal();

            record.PrintInfoFormatted();

            record.PrintInfoInterpolation();

            record.PrintInfoCenter();

            Console.ReadKey();
        }
    }

    class NotebookRecord
    {
        #region Public properties
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Рост
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Баллы по истории
        /// </summary>
        public int HistoryScore { get; set; }
        /// <summary>
        /// Баллы по математике
        /// </summary>
        public int MathematicsScore { get; set; }
        /// <summary>
        /// Баллы по русскому языку
        /// </summary>
        public int RussianScore { get; set; }
        #endregion

        #region Public methods

        /// <summary>
        /// Печать в консоль с использованием обычного вывода
        /// </summary>
        public void PrintInfoNormal()
        {
            Console.WriteLine("Печать в консоль с использованием обычного вывода");
            string str = "Имя: " + Name + "\nВозраст: " + Age + "\nРост: " + Height +
                "\nБаллы по истории: " + HistoryScore + "\nБаллы по математике: " + MathematicsScore +
                "\nБаллы по русскому языку: " + RussianScore + "\nСредний балл по трем предметам: " + GetAverageScore();

            Console.WriteLine(str);
            Console.WriteLine();
        }

        /// <summary>
        /// Печать в консоль с использованием форматированного вывода
        /// </summary>
        public void PrintInfoFormatted()
        {
            Console.WriteLine("Печать в консоль с использованием форматированного вывода");

            string pattern = "Имя: {0}\nВозраст: {1}\nРост: {2}\nБаллы по истории: {3}\nБаллы по математике: {4}" +
                "\nБаллы по русскому языку: {5}\nСредний балл по трем предметам: {6}";

            Console.WriteLine(pattern,
                Name,
                Age,
                Height,
                HistoryScore,
                MathematicsScore,
                RussianScore,
                GetAverageScore()
                );
            Console.WriteLine();
        }

        /// <summary>
        /// Печать в консоль с использованием интерполяции строк
        /// </summary>
        public void PrintInfoInterpolation()
        {
            Console.WriteLine("Печать в консоль с использованием интерполяции строк");
            string str = $"Имя: {Name}\nВозраст: {Age}\nРост: {Height}\nБаллы по истории: {HistoryScore}\nБаллы по математике: {MathematicsScore}" +
                $"\nБаллы по русскому языку: {RussianScore}\nСредний балл по трем предметам: {GetAverageScore()}";

            Console.WriteLine(str);
            Console.WriteLine();
        }

        /// <summary>
        /// Вывод данных в центре консоли
        /// </summary>
        public void PrintInfoCenter()
        {
            PrintStringCenter("Вывод данных в центре консоли");
            PrintStringCenter($"Имя: {Name}");
            PrintStringCenter($"Возраст: {Age}");
            PrintStringCenter($"Рост: {Height}");
            PrintStringCenter($"Баллы по истории: {HistoryScore}");
            PrintStringCenter($"Баллы по математике: {MathematicsScore}");
            PrintStringCenter($"Баллы по русскому языку: {RussianScore}");
            PrintStringCenter($"Средний балл по трем предметам: {GetAverageScore()}");

            Console.WriteLine();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Подсчет среднего балла по трем предметам
        /// </summary>
        /// <returns></returns>
        private double GetAverageScore()
        {
            var averageScore = (double)(HistoryScore + MathematicsScore + RussianScore) / 3;
            return Math.Round(averageScore, 2);
        }

        /// <summary>
        /// Печать строки по центру
        /// </summary>
        /// <param name="str">Строка которую нужно расположить по центру консоли</param>
        private void PrintStringCenter(string str)
        {
            //вычисляем ширину поля с учетом длины строки
            var padding = Console.WindowWidth / 2 + str.Length / 2;
            //печатаем строку в поле с выравниванием по правому краю
            Console.WriteLine("{0," + padding + "}", str);
        }

        #endregion
    }
}
