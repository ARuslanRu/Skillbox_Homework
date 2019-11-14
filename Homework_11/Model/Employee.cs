using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Homework_11.Model
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dolzhnost { get; set; }
        public abstract decimal Salary { get; }
        public Employee(int id, string name, string dolzhnost)
        {
            Id = id;
            Name = name;
            Dolzhnost = dolzhnost;
        }

        #region Неиспользуемое
        //private static int GetId()
        //{
        //    if (employees.Count != 0)
        //    {
        //        int[] number = employees.Select(x => x.Id).ToArray();
        //        int[] missingNumbers = Enumerable.Range(number[0], number[number.Length - 1]).Except(number).ToArray();
        //        return missingNumbers.Length == 0 ? number.Max() + 1 : missingNumbers.FirstOrDefault();
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}
        #endregion
    }
}
