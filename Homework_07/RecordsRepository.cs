using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework_07
{
    struct RecordsRepository
    {
        private List<Record> records; // Список записей
        private string path; // Путь к файлу с записями
        private string[] titles; // Массив, храняий заголовки полей. используется в PrintDbToConsole

        public RecordsRepository(string path)
        {
            this.path = path;
            this.titles = new string[0];
            this.records = new List<Record>();
            this.LoadRecords();
        }

        /// <summary>
        /// Загрузка записей из файла
        /// </summary>
        private void LoadRecords()
        {
            using (var sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split(',');

                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split(',');

                    AddRecord(new Record(Convert.ToInt32(args[0]), args[1], args[2], Convert.ToDateTime(args[3]), Convert.ToDateTime(args[4])));
                }
            }
        }

        /// <summary>
        /// Метод добавления записи в хранилище
        /// </summary>
        /// <param name="ConcreteWorker">Запись</param>
        public void AddRecord(Record concreteRecord)
        {
            records.Add(concreteRecord);
        }


        public void CreateRecord()
        {

        }

        public void ReadRecord()
        {

        }

        public void UpdateRecord()
        {

        }

        public void DeleteRecord()
        {

        }

        /// <summary>
        /// Сохранение текущих записей хранилища в файл
        /// </summary>
        public void SaveRecords()
        {
            using (var sw = new StreamWriter(path, false))
            {
                string temp = String.Format("{0},{1},{2},{3},{4}",
                                                this.titles[0],
                                                this.titles[1],
                                                this.titles[2],
                                                this.titles[3],
                                                this.titles[4]);

                sw.WriteLine(temp);

                foreach (var item in records)
                {
                    temp = String.Format("{0},{1},{2},{3},{4}",
                                            item.Number,
                                            item.Caption,
                                            item.Description,
                                            item.CreateDateTime,
                                            item.LastModifyDateTime);
                    sw.WriteLine(temp);
                }
            }
        }

        /// <summary>
        /// Вывод записей из хранилища в консоль
        /// </summary>
        public void PrintAllRecords()
        {
            Console.WriteLine($"{this.titles[0],6} {this.titles[1],15} {this.titles[2],25} {this.titles[3],20} {this.titles[4],20}");

            foreach (var item in records)
            {
                Console.WriteLine($"{item.Number,6} {item.Caption,15} {item.Description,25} {item.CreateDateTime,20} {item.LastModifyDateTime,20}");
            }
        }

        /// <summary>
        /// Количество записей в хранилище
        /// </summary>
        /// <returns>Возвращает количество записей в хранилище</returns>
        public int Count()
        {
            return records.Count;
        }
    }
}
