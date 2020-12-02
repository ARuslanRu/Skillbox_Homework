using System;
using System.Threading.Tasks;

namespace Homework_16
{
    class Program
    {
        static void Main(string[] args)
        {

            var t = DateTime.Now;

            var tasks = InitTasksPool(10, 1_000_000_000, 2_000_000_000);
            foreach (var item in tasks)
            {
                item.Start();
            }

            Console.WriteLine("Ждем окончания выполнения задач");
            Task.WaitAll(tasks);

            int count = 0;
            foreach (var item in tasks)
            {
                count += item.Result;
            }

            Console.WriteLine($"Вермя выполнения {(DateTime.Now - t).TotalMilliseconds}. Количество кратных чисел {count}");
        }

        static Task<int>[] InitTasksPool(int numOfTasks, int numFrom, int numTo)
        {
            Task<int>[] tasks = new Task<int>[numOfTasks];
            int interval = (numTo - numFrom) / numOfTasks;
            for (int i = 0; i < numOfTasks; i++)
            {

                numTo = numFrom + interval;

                int form = numFrom;
                int to = numTo;
                Console.WriteLine($"Интеравал от {form} до {to}");
                tasks[i] = new Task<int>(() => IntervalCalc(form, to));
                numFrom = numTo;
            }

            return tasks;
        }
        static int Sum(int value)
        {
            int sum = 0;
            while(value > 0)
            {
                sum += value % 10;
                value = value / 10;
            }
            return sum;
        }
        public static bool IsMultiple(int num, int div)
        {
            if (div == 0 || div == 1) return true;
            if (num % div == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static int IntervalCalc(int from, int to)
        {
            int innerCount = 0;
            for (int i = from; i < to; i++)
            {
                int sum = Sum(i);
                bool isYes = IsMultiple(sum, i % 10);
                //Console.WriteLine($"Число {i}. Сумма {sum}. Кратно {isYes}");
                if (isYes) { innerCount++; }
            }
            Console.WriteLine($"В интревале от{from} до {to}. Количество кратных {innerCount}");
            return innerCount;
        }
    }

}
