using System;

namespace Homework_23
{
    class Program
    {
        static void Main(string[] args)
        {


            string s = "-212312312312123456789";


            Console.WriteLine(StringToInt(s));
        }

        private static int StringToInt(string str)
        {
            if (str == null) throw new ArgumentException();
            if (str.Length == 0) throw new ArgumentException();

            int result = 0;

            int rank = 1;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[0] == '-')
                {

                }

                switch (str[i])
                {
                    case '0':
                        result += 0 * rank;
                        break;
                    case '1':
                        result += 1 * rank;
                        break;
                    case '2':
                        result += 2 * rank;
                        break;
                    case '3':
                        result += 3 * rank;
                        break;
                    case '4':
                        result += 4 * rank;
                        break;
                    case '5':
                        result += 5 * rank;
                        break;
                    case '6':
                        result += 6 * rank;
                        break;
                    case '7':
                        result += 7 * rank;
                        break;
                    case '8':
                        result += 8 * rank;
                        break;
                    case '9':
                        result += 9 * rank;
                        break;
                    case '-':
                        if (i == 0)
                        {
                            result *= -1;
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                        break;
                    default:
                        throw new ArgumentException();
                        break;
                }
                rank *= 10;
            }

            return checked(result);
        }
    }
}
