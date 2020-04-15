using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_11.Task_2
{
    interface I1
    {
        void M();
    }

    interface I2
    {
        void M();
    }

    class A : I1, I2
    {
        public void M() { Console.WriteLine("A.M()"); }

        void I1.M() { Console.WriteLine("I1A.M()"); }
        void I2.M() { Console.WriteLine("I2A.M()"); }
    }

    class B : A
    {
        //Не разобрался как это можно сделать
    }

    
}
