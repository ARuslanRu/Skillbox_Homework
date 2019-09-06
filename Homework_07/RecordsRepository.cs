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
        private List<Record> records;
        private string[] titles;

        public RecordsRepository(string path)
        {
            //this.path = path;
            this.titles = new string[0];
            this.records = new List<Record>();
            this.LoadRecords(path);
        }

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

        public void AddNewRecord(string caption, string descripition)
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
            var record = new Record(id, caption, descripition, DateTime.Now, DateTime.Now);

            records.Add(record);
            Console.WriteLine("Добавлена запись:");
            Console.WriteLine($"{record.Number,6} {record.Caption,15} {record.Description,25} {record.CreateDateTime,20} {record.LastModifyDateTime,20}");
        }

        public void PrintRecords()
        {
            Console.WriteLine($"{this.titles[0],6} {this.titles[1],15} {this.titles[2],25} {this.titles[3],20} {this.titles[4],20}");

            foreach (var item in records)
            {
                Console.WriteLine($"{item.Number,6} {item.Caption,15} {item.Description,25} {item.CreateDateTime,20} {item.LastModifyDateTime,20}");
            }
        }

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

        public void UpdateRecord(Record record)
        {

        }


        /// <summary>
        /// Удаление записи по номеру
        /// </summary>
        /// <param name="number">Запись</param>
        public void DeleteRecord(Record record)
        {
            records.Remove(record);
        }

        public void SortBy()
        {

        }

        public Record FindRecordByCaption(string caption)
        {
            return new Record();
        }

        public Record FindRecordByDescription(string description)
        {
            return new Record();
        }

        public Record FindRecordByNumber(int number)
        {
            return records.Where(x => x.Number == number).FirstOrDefault();
        }

        public Record FindRecordByDate(DateTime dateFrom, DateTime dateTo)
        {
            return new Record();
        }

        public int Count()
        {
            return records.Count;
        }

    }
}
