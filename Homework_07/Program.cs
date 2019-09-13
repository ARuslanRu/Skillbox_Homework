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
        /// - Импорт записей по выбранному диапазону дат???
        /// - Упорядочивания записей ежедневника по выбранному полю???


        static void Main(string[] args)
        {
            var recordsRepository = new RecordsRepository("data.csv");

            bool flag = true;


            while (flag)
            {
                Console.Write("Меню:" +
                "\n1. Добавить запись." +
                "\n2. Удалить запись." +
                "\n3. Редактироать запись." +
                "\n4. Найти запись." +
                "\n5. Вывести все записи в консоль." +
                "\n6. Сохранить записи и выйти." +
                "\n7. Догрузить данные из выбранного файла" +
                "\nВаш выбор: ");

                int mode;
                int number;
                Record rec;
                string caption;
                string description;

                while (!int.TryParse(Console.ReadLine(), out mode) || mode < 1 || mode > 7)
                {
                    Console.Write("Введено недопустимое значение." +
                            "\nПовторите ввод: ");
                }

                //Без верификации вводимой из консоли информации
                switch (mode)
                {
                    case 1:
                        //Добавление записи
                        Console.Write("Введите заголовок записи: ");
                        caption = Console.ReadLine();
                        Console.Write("Введите текст записи: ");
                        description = Console.ReadLine();
                        rec = recordsRepository.AddNewRecord(caption, description);
                        Console.WriteLine("Добавлена запись:");
                        recordsRepository.SortByNumber();
                        break;

                    case 2:
                        //Удаление записи по номеру
                        Console.Write("Введите номер записи: ");
                        number = int.Parse(Console.ReadLine());
                        rec = recordsRepository.FindRecordByNumber(number);
                        recordsRepository.DeleteRecord(rec);
                        Console.WriteLine("Удалена запись:");
                        recordsRepository.PrintRecord(rec);
                        break;

                    case 3:
                        //Редактирование записи
                        Console.Write("Введите номер записи: ");
                        number = int.Parse(Console.ReadLine());
                        rec = recordsRepository.FindRecordByNumber(number);
                        Console.WriteLine("Редактирование записи:");
                        recordsRepository.PrintRecord(rec);

                        Console.Write("Введите новый заголовок записи: ");
                        caption = Console.ReadLine();
                        Console.Write("Введите новый текст записи: ");
                        description = Console.ReadLine();
                        rec = recordsRepository.UpdateRecord(number, caption, description);

                        Console.WriteLine("Запись отредактирована:");
                        recordsRepository.PrintRecord(rec);
                        break;

                    case 4:
                        //Поиск записи
                        Console.Write("Введите номер записи: ");
                        number = int.Parse(Console.ReadLine());
                        rec = recordsRepository.FindRecordByNumber(number);
                        Console.WriteLine("Найдена запись:");
                        recordsRepository.PrintRecord(rec);
                        break;

                    case 5:
                        //Выввод в консоль всех записей
                        recordsRepository.PrintAllRecords();

                        break;
                    case 6:
                        //Сохранение в файл
                        recordsRepository.SaveRecords("data.csv");
                        Console.WriteLine("Данные сохранены. Нажмите любую клавишу для выхода.");
                        flag = false;
                        break;
                    case 7:
                        //добавление записей из уже существующего файла
                        Console.Write("Укажите имя файла для импорта данных:");
                        var path = Console.ReadLine();
                        var tempRepository = new RecordsRepository(path);
                        Console.WriteLine("Укажите интервал дат, записи за которые необходимо добавить.");
                        Console.Write("С какой даты? (в формате ДД.ММ.ГГГГ):");
                        var dateFrom = Convert.ToDateTime(Console.ReadLine());
                        Console.Write("По какую дату? (в формате ДД.ММ.ГГГГ):");
                        var dateTo = Convert.ToDateTime(Console.ReadLine());
                     
                        foreach (var item in tempRepository.Records)
                        {
                            if (item.CreateDateTime > dateFrom && item.CreateDateTime < dateTo)
                            {
                                recordsRepository.AddRecord(item);
                            }
                        }
                        Console.WriteLine("Записи добавлены.");

                        break;
                    default:
                        Console.WriteLine("Что-то пошло не так.");
                        break;

                }
            }
        }
    }
}
