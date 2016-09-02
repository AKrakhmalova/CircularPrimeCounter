using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularPrimeCounter
{
    class Program
    {
        //const for max number of the range
        const int n = 1000000;
        static List<int> primeNumbers = new List<int>();
        static List<int> circularPrimeNumbers = new List<int>();

        private static bool IsNumberPrime(int num)
        {
            bool isPrime = true;
            foreach (int primeNumber in primeNumbers)
            {
                //enough to check only if number has the smallest divider (smaller than sqrt of the number)
                if (primeNumber > Math.Sqrt(num))
                    break;

                //check if number is prime
                if (num % primeNumber == 0)
                {
                    isPrime= false;
                    break;
                }
            }
            return isPrime;
        }

        private static bool isNumberCircularPrime(int number)
        {
            bool isCircularPrime = true;
            //Count digits in the number
            double epsilon = 0.1;
            //epsilon is necessary to count properly for whole numbers of lg
            int count = (int)Math.Ceiling(Math.Log10(number+epsilon));

            //shifting number and checking if it is prime
            int factor = (int)Math.Pow(10, count-1);
            for (int k = 0; k < count; k++)
            {
                number = number / 10 + (number % 10) * factor;
                if (number != 2 && number != 5)
                    if (number % 2 == 0 || number % 5 == 0 || !IsNumberPrime(number))
                    {
                        isCircularPrime = false;
                        break;
                    }
            }

            return isCircularPrime;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Here is the list of all circular prime numbers till {0} :",n);
            int num = 2;

            while (num < n)
            {
                //Check numbers that divide to 2 or 5 here not to go in foreach loop (save time)
                if (num % 2 != 0 || num % 5 != 0)
                {
                    if (IsNumberPrime(num))
                    {
                        primeNumbers.Add(num);
                        if (isNumberCircularPrime(num))
                        {
                            circularPrimeNumbers.Add(num);
                            Console.Write(" " + num + " ");
                        }
                    }
                }
                num++;
            }
            Console.WriteLine();
            Console.WriteLine("Count of these numbers is {0}.", circularPrimeNumbers.Count);
            Console.ReadKey();
        }
    }
}
