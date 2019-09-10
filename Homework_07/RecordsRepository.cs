using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    struct RecordsRepository
    {

        #region Поля

        private List<Record> records;
        private string[] titles;

        #endregion

        #region Конструкторы
        public RecordsRepository(string path)
        {
            this.titles = new string[0];
            this.records = new List<Record>();
            this.LoadRecords(path);
        }

        #endregion

        #region Private методы

        /// <summary>
        /// Загрузка записей из файла
        /// </summary>
        private void LoadRecords(string path)
        {
            using (var sr = new StreamReader(path))
            {
                titles = sr.ReadLine().Split(',');

                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split(',');

                    records.Add(new Record(Convert.ToInt32(args[0]), args[1], args[2], Convert.ToDateTime(args[3]), Convert.ToDateTime(args[4])));
                }
            }
        }

        #endregion

        #region Public методы

        /// <summary>
        ///Создание и добавление новой записи в репозиторий и возврат ее для дальнейшего использования, например вывода в консоль.
        /// </summary>
        /// <param name="caption">Заголовок</param>
        /// <param name="descripition">Описание</param>
        /// <returns>Возвращает записанную запись</returns>
        public Record AddNewRecord(string caption, string descripition)
        {
            int id;
            if (records.Count != 0)
            {
                int[] number = records.Select(x => x.Number).ToArray();
                int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
                id = missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
            }
            else
            {
                id = 1;
            }
            var rec = new Record(id, caption, descripition, DateTime.Now, DateTime.Now);

            records.Add(rec);

            return rec;
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="record">Запись</param>
        public void DeleteRecord(Record record)
        {
            records.Remove(record);
        }

        /// <summary>
        /// Вывод одной записи в консоль
        /// </summary>
        /// <param name="rec">Запись</param>
        public void PrintRecord(Record rec)
        {
            Console.WriteLine($"{rec.Number,6} {rec.Caption,15} {rec.Description,25} {rec.CreateDateTime,20} {rec.LastModifyDateTime,20}");
        }

        /// <summary>
        /// Вывод всех записей в консоль
        /// </summary>
        public void PrintAllRecords()
        {
            Console.WriteLine($"{this.titles[0],6} {this.titles[1],15} {this.titles[2],25} {this.titles[3],20} {this.titles[4],20}");

            foreach (var item in records)
            {
                PrintRecord(item);
            }
        }

        /// <summary>
        /// Сохранение записей в файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void SaveRecords(string path)
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
        /// Редактирование записи по номеру
        /// </summary>
        /// <param name="id">Нормер записи которую нужно отредактирвать</param>
        /// <param name="caption">Новый заголовок</param>
        /// <param name="description">Новое описание</param>
        /// <returns>Возвращает отредактированную запись, для вывода в консоль</returns>
        public Record UpdateRecord(int id, string caption, string description)
        {
            var rec = records.Where(x => x.Number == id).FirstOrDefault();
            records.Remove(rec);
            rec.Caption = caption;
            rec.Description = description;
            rec.LastModifyDateTime = DateTime.Now;
            records.Add(rec);
            return rec;
        }

        /// <summary>
        /// Сортировка списка по номеру
        /// </summary>
        public void SortByNumber() => records.OrderBy(x => x.Number);

        /// <summary>
        /// Сортировка списка по дате создания
        /// </summary>
        public void SortByCreateDate() => records.OrderBy(x => x.CreateDateTime);

        /// <summary>
        /// Поиск записи по номеру
        /// </summary>
        /// <param name="number">Номер записи</param>
        /// <returns>Возвращает запись</returns>
        public Record FindRecordByNumber(int number) => records.Where(x => x.Number == number).FirstOrDefault();

        #endregion




        #region Тестовые методы

        /// <summary>
        /// Поиск по заголовку записи
        /// </summary>
        /// <param name="caption">Искомый заголовок</param>
        /// <returns>Возвращает запись</returns>
        public Record FindRecordByCaption(string caption) => records.Where(x => x.Caption.Contains(caption)).FirstOrDefault();

        /// <summary>
        /// Поиск по тексту записи
        /// </summary>
        /// <param name="description">Искомый текст</param>
        /// <returns>Возвтращает найденную запись</returns>
        public Record FindRecordByDescription(string description) => records.Where(x => x.Description.Contains(description)).FirstOrDefault();

        /// <summary>
        /// Поиск записей в интервале времени по дате создания
        /// </summary>
        /// <param name="dateFrom">С какой даты</param>
        /// <param name="dateTo">По какую дату</param>
        /// <returns></returns>
        public List<Record> FindRecordByDate(DateTime dateFrom, DateTime dateTo)=> records.Where(x => x.CreateDateTime > dateFrom && x.CreateDateTime < dateTo).ToList();

        /// <summary>
        /// Вывод в консоль нескольких записей
        /// </summary>
        /// <param name="recs">Список с записями</param>
        public void PrintRecords(List<Record> recs)
        {
            foreach (var rec in recs)
            {
                Console.WriteLine($"{rec.Number,6} {rec.Caption,15} {rec.Description,25} {rec.CreateDateTime,20} {rec.LastModifyDateTime,20}");
            }
        }

        #endregion




    }
}
