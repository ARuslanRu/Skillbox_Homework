using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    class Program
    {

        /// Разработать ежедневник.
        /// В ежедневнике реализовать возможность 
        /// - создания
        /// - удаления
        /// - реактирования 
        /// записей
        /// 
        /// В отдельной записи должно быть не менее пяти полей
        /// 
        /// Реализовать возможность 
        /// - Загрузки даннах из файла
        /// - Выгрузки даннах в файл
        /// - Добавления данных в текущий ежедневник из выбранного файла
        /// - Импорт записей по выбранному диапазону дат
        /// - Упорядочивания записей ежедневника по выбранному полю


        static void Main(string[] args)
        {
            var recordsRepository = new RecordsRepository("data.csv");



            bool flag = true;


            while (flag)
            {
                Console.Write("Меню:" +
                "\n1. Добавить запись." +
                "\n2. Найти запись." +
                "\n3. Редактироать запись." +
                "\n4. Удалить запись." +
                "\n5. Вывести все записи в консоль." +
                "\n6. Сохранить записи и выйти." +
                "\nВаш выбор: ");

                int mode;

                while (!int.TryParse(Console.ReadLine(), out mode) || mode < 1 || mode > 6)
                {
                    Console.Write("Введено недопустимое значение." +
                            "\nПовторите ввод: ");
                }

                int number;

                switch (mode)
                {
                    case 1:
                        Console.Write("Введите заголовок записи: ");
                        var caption = Console.ReadLine();

                        Console.Write("Введите текст записи: ");
                        var description = Console.ReadLine();

                        recordsRepository.AddNewRecord(caption, description);
                        break;
                    case 2:
                        Console.Write("Введите номер записи: ");
                        number = int.Parse(Console.ReadLine());




                        break;
                    case 3:
                        break;
                    case 4:
                        Console.Write("Введите номер записи: ");
                        number = int.Parse(Console.ReadLine());

                        var r =  recordsRepository.FindRecordByNumber(number);

                        recordsRepository.DeleteRecord(r);

                        break;
                    case 5:
                        recordsRepository.PrintRecords();

                        break;
                    case 6:
                        recordsRepository.SaveRecords("data.csv");
                        Console.WriteLine("Данные сохранены. Нажмите любую клавишу для выхода.");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Что-то пошло не так.");
                        break;
                }






            }
        }
    }
}
