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

            recordsRepository.PrintAllRecords();

            Console.ReadKey();
            Console.WriteLine();

            recordsRepository.AddRecord(new Record(recordsRepository.Count() + 1, "Пример 2", "Тест", DateTime.Now, DateTime.Now));
            recordsRepository.AddRecord(new Record(recordsRepository.Count() + 1, "Пример 3", "Тест 3 3", DateTime.Now, DateTime.Now));

            recordsRepository.PrintAllRecords();
            Console.ReadKey();

            recordsRepository.SaveRecords();
            Console.ReadKey();


        }
    }
}
