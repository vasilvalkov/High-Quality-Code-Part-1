using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFlow_Loops
{
    class RefactorLoops
    {
        void Refactor()
        {
            bool found = false;

            for (int i = 0; i < array.Length; i++)
            {
                if (i % 10 == 0 && array[i] == expectedValue)
                {
                    found = true;
                    break;
                }

                Console.WriteLine(array[i]);
            }

            // More code here
            if (found)
            {
                Console.WriteLine("Value Found");
            }
        }
    }
}
