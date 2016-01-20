using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCrossThrowIf;

namespace CCrossThrowIf.Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var result = WithThrowIt(new TimeSpan(2000));

            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static string WithoutThrowIt(string arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg));
            }

            return arg;
        }

        public static TimeSpan WithThrowIt(TimeSpan arg)
        {
            ThrowIf.Argument.IsNegativeOrZero(() => arg);

            return arg;
        }
    }
}
