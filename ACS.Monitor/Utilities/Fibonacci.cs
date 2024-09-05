using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.Monitor.Utilities
{
    class Fibonacci
    {
        IEnumerable<int> Fib(int a = 1, int b = 1)
        {
            while (true)
            {
                yield return a;
                a = a + b;
                yield return b;
                b = b + a;
            }
        }

        private void TestFib()
        {
            foreach (int item in Fib().Take(10))
            {
                Console.WriteLine(item);
            }
        }

    }
}
