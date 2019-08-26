using System;
using System.Linq;


namespace Homework_Theme_03
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задание

            // Просматривая сайты по поиску работы, у вас вызывает интерес следующая вакансия:

            // Требуемый опыт работы: без опыта
            // Частичная занятость, удалённая работа
            //
            // Описание 
            //
            // Стартап «Micarosppoftle» занимающийся разработкой
            // многопользовательских игр ищет разработчиков в свою команду.
            // Компания готова рассмотреть C#-программистов не имеющих опыта в разработке, 
            // но желающих развиваться в сфере разработки игр на платформе .NET.
            //
            // Должность Интерн C#/
            //
            // Основные требования:
            // C#, операторы ввода и вывода данных, управляющие конструкции 
            // 
            // Конкурентным преимуществом будет знание процедурного программирования.
            //
            // Не технические требования: 
            // английский на уровне понимания документации и справочных материалов
            //
            // В качестве тестового задания предлагается решить следующую задачу.
            //
            // Написать игру, в которою могут играть два игрока.
            // При старте, игрокам предлагается ввести свои никнеймы.
            // Никнеймы хранятся до конца игры.
            // Программа загадывает случайное число gameNumber от 12 до 120 сообщая это число игрокам.
            // Игроки ходят по очереди(игра сообщает о ходе текущего игрока)
            // Игрок, ход которого указан вводит число userTry, которое может принимать значения 1, 2, 3 или 4,
            // введенное число вычитается из gameNumber
            // Новое значение gameNumber показывается игрокам на экране.
            // Выигрывает тот игрок, после чьего хода gameNumber обратилась в ноль.
            // Игра поздравляет победителя, предлагая сыграть реванш
            // 
            // * Бонус:
            // Подумать над возможностью реализации разных уровней сложности.
            // В качестве уровней сложности может выступать настраиваемое, в начале игры,
            // значение userTry, изменение диапазона gameNumber, или указание большего количества игроков (3, 4, 5...)

            // *** Сложный бонус
            // Подумать над возможностью реализации однопользовательской игры
            // т е игрок должен играть с компьютером


            // Демонстрация
            // Число: 12,
            // Ход User1: 3
            //
            // Число: 9
            // Ход User2: 4
            //
            // Число: 5
            // Ход User1: 2
            //
            // Число: 3
            // Ход User2: 3
            //
            // User2 победил!

            #endregion

            //bool isGameСontinues = true;

            while (true)
            {

                Console.Write("Выберете режим игры" +
                                "\n1. Однопользовательский" +
                                "\n2. Многопользовательский" +
                                "\nВаш выбор: ");
                int mode;
                while (!int.TryParse(Console.ReadLine(), out mode) || !(mode > 0 && mode < 3))
                {
                    //Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Введено недопустимое значение." +
                        "\nПовторите выбор: ");
                }

                int[] userTryNumbers;
                int gameNumber;
                string[] players;

                switch (mode)
                {
                    //Однопользовательская игра
                    case 1:
                        Console.WriteLine("Укажите настройки сложности.");
                        userTryNumbers = SelectPossibleNumbers();
                        gameNumber = SelectRange();

                        Console.WriteLine();
                        Console.WriteLine($"Ваши настройки следующие:");
                        Console.Write($"Доступные числа: ");
                        foreach (var num in userTryNumbers)
                        {
                            Console.Write($"{num} ");
                        }
                        Console.WriteLine();

                        StartGame(userTryNumbers, gameNumber);

                        break;

                    //Многопользоватльская игра
                    case 2:
                        Console.WriteLine("Укажите настройки сложности.");

                        players = SelectNumbersOfPlayers(5);
                        userTryNumbers = SelectPossibleNumbers();
                        gameNumber = SelectRange();

                        Console.WriteLine();
                        Console.WriteLine($"Ваши настройки следующие:");
                        Console.WriteLine($"Количество игроков: {players.Count()}");
                        Console.Write($"Доступные числа: ");
                        foreach (var num in userTryNumbers)
                        {
                            Console.Write($"{num} ");
                        }
                        Console.WriteLine();

                        StartGame(players, userTryNumbers, gameNumber);

                        break;
                    default:
                        break;
                }

                Console.WriteLine();
                Console.Write(
@"Хотите повторить игру?
1. Да
2. Нет
Ваш выбор: ");
                int resume;
                while (!int.TryParse(Console.ReadLine(), out resume) || !(resume > 0 && resume < 3))
                {
                    Console.Write("Выбран некорректный вариант." +
                        "\nПовторите выбор: ");
                }

                if (resume == 2)
                {
                    break;
                }
            }

            Console.WriteLine("Игра завершена! Нажмите любую клавишу для закрытия окна.");
            Console.ReadKey();
        }

        /// <summary>
        /// Выбор возможных чисел.
        /// </summary>
        /// <returns>Возвращает набор чисел которые можно использовать за ход</returns>
        private static int[] SelectPossibleNumbers()
        {
            Console.Write("Выберете используемые числа." +
                "\n1. для использования чисел [1, 2, 3, 4]" +
                "\n2. для использования чисел [1, 2, 3, 5, 7]" +
                "\n3. для использования чисел [1, 2, 3, 5, 7, 11]" +
                "\nВаш выбор: ");
            int number;
            while (!int.TryParse(Console.ReadLine(), out number) || !(number > 0 && number < 4))
            {
                Console.Write("Выбран некорректный вариант." +
                    "\nПовторите выбор: ");
            }

            int[] numbers;
            switch (number)
            {
                case 1:
                    return numbers = new[] { 1, 2, 3, 4 };
                case 2:
                    return numbers = new[] { 1, 2, 3, 5, 7 };
                case 3:
                    return numbers = new[] { 1, 2, 3, 5, 7, 11 };
                default:
                    return null;
            }
        }

        /// <summary>
        /// Выбор количества игроков, запрос их имен и получения их в массиве.
        /// </summary>
        /// <param name="numberOfPlayers">Задает максимальное количество игроков</param>
        /// <returns>Возвращает массив имен игроков</returns>
        private static string[] SelectNumbersOfPlayers(int numberOfPlayers)
        {
            Console.Write($"Укажите количество игроков (от 2 до {numberOfPlayers} игроков): ");
            int playerCount;
            while (!int.TryParse(Console.ReadLine(), out playerCount) || !(playerCount > 1 && playerCount < numberOfPlayers))
            {
                Console.Write("Введено недопустимое значение." +
                    $"\nУкажите количество игроков (от 2 до {numberOfPlayers} игроков): ");
            }
            //Console.WriteLine($"В игру играет {playerCount} игроков.");
            var players = new string[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                Console.Write($"Игрок {i + 1} введите свой никнейм: ");
                players[i] = Console.ReadLine();
            }
            return players;
        }

        /// <summary>
        /// Запрос диапазона загадываемого числа и его возврат в результате.
        /// </summary>
        /// <returns>Возвращает задуманное число</returns>
        private static int SelectRange()
        {
            Console.WriteLine("Укажите диапазон загадываемого числа.");
            int minNumber, maxNumber;
            Console.Write("Минимально возможное число: ");
            while (!int.TryParse(Console.ReadLine(), out minNumber))
            {
                Console.Write("Введено недопустимое значение." +
                    "\nВведите минимальное значение повторно: ");
            }
            Console.Write("Максимально возможное число: ");
            while (!int.TryParse(Console.ReadLine(), out maxNumber) || minNumber > maxNumber)
            {
                Console.Write("Введено недопустимое значение." +
                    "\nВведите максимальное значение повторно: ");
            }
            return new Random().Next(minNumber, maxNumber);
        }

        /// <summary>
        /// Старт однопользовательской игры с заданными настройками
        /// </summary>
        /// <param name="userTryNumbers">Набор чисел которые можно использовать</param>
        /// <param name="gameNumber">Загаданное число</param>
        private static void StartGame(int[] userTryNumbers, int gameNumber)
        {
            int userTry;
            bool flag = true;
            bool isPlayer = true;
            string winner;

            while (flag)
            {

                if (isPlayer)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Число: {gameNumber}");
                    Console.Write($"Ход Игрока: ");

                    //проверяем что введено число, что оно содержится в наборе чисел которые можно использовать 
                    //и что оно не больше текущего загаданного числа, что бы оно не ушло в минус
                    while (!int.TryParse(Console.ReadLine(), out userTry) || !userTryNumbers.Contains(userTry) || gameNumber < userTry)
                    {
                        Console.Write($"Введено недопустимое число. Введите число повторно: ");
                    }

                    winner = "Игрок";
                    isPlayer = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Число: {gameNumber}");

                    userTry = Computer.Start(gameNumber, userTryNumbers);

                    Console.WriteLine($"Ход Компьютера: {userTry}");

                    winner = "Компьютер";
                    isPlayer = true;
                }


                gameNumber -= userTry;
                if (gameNumber == 0)
                {
                    Console.WriteLine($"{winner} победил!");
                    flag = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Старт многопользовательской игры с заданными настройками
        /// </summary>
        /// <param name="userTryNumbers">Набор чисел которые можно использовать</param>
        /// <param name="players">Список имен игроков</param>
        /// <param name="gameNumber">Загаданное число</param>
        private static void StartGame(string[] players, int[] userTryNumbers, int gameNumber)
        {
            int userTry;
            bool flag = true;
            while (flag)
            {
                for (int i = 0; i < players.Count(); i++)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Число: {gameNumber}");
                    Console.Write($"Ход {players[i]}: ");

                    //проверяем что введено число, что оно содержится в наборе чисел которые можно использовать 
                    //и что оно не больше текущего загаданного числа, что бы оно не ушло в минус
                    while (!int.TryParse(Console.ReadLine(), out userTry) || !userTryNumbers.Contains(userTry) || gameNumber < userTry)
                    {
                        Console.Write($"Введено недопустимое число. {players[i]} введите число повторно: ");
                    }

                    gameNumber -= userTry;
                    if (gameNumber == 0)
                    {
                        Console.WriteLine($"{players[i]} победил!");
                        flag = false;
                        break;
                    }
                }
            }
        }
    }
}
