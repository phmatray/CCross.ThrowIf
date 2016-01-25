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
            double d = default(double);
            decimal dec = default(decimal);
            WithThrowIt(11);

            Console.ReadLine();
        }

        public static string WithoutThrowIt(string arg)
        {
            throw new ArgumentOutOfRangeException();
            throw new ArgumentNullException();
            throw new ArgumentException();

            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg));
            }

            return arg;
        }

        public static void WithThrowIt(int value, string toto = "bibi")
        {
            //ThrowIf.Argument.IsGreaterThan(() => value, 10);
            //ThrowIf.Argument.IsLowerThan(() => value, 10);
            //ThrowIf.Argument.IsEqualTo(() => value, 10);

            ThrowIf.Argument.IsEqualTo(() => toto, "bibi", "message perso");

            ThrowIf<ArgumentOutOfRangeException>.IsNull(() => toto, "message perso");

            if (value > 5)
                throw new ArgumentOutOfRangeException(nameof(value));
        }
    }

    public static class Experiment
    {
        public static String ThenSample(string arg)
        {
            arg.IsNullOrWhiteSpace().Then(() => { arg = "Fallback"; });
            return arg;
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return String.IsNullOrWhiteSpace(s);
        }

        public static void Then(this bool b, Action action)
        {
            if (b) action.Invoke();
        }
    }
}
