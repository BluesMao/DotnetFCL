using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 1;
            var b = 2;

            var c = "a";
            var d = "z";
            Console.WriteLine(Comparer.Default.Compare(a,b));
            Console.WriteLine(Comparer.DefaultInvariant.Compare(a,b));

            Console.WriteLine(Comparer.Default.Compare(c, d));
            Console.WriteLine(Comparer.DefaultInvariant.Compare(c, d));

            // Creates the strings to compare.
            String str1 = "llegar";
            String str2 = "lugar";
            Console.WriteLine("Comparing \"{0}\" and \"{1}\" ...", str1, str2);

            // Uses the DefaultInvariant Comparer.
            Console.WriteLine("   Invariant Comparer: {0}", Comparer.DefaultInvariant.Compare(str1, str2));

            // Uses the Comparer based on the culture "es-ES" (Spanish - Spain, international sort).
            Comparer myCompIntl = new Comparer(new CultureInfo("es-ES", false));
            Console.WriteLine("   International Sort: {0}", myCompIntl.Compare(str1, str2));

            // Uses the Comparer based on the culture identifier 0x040A (Spanish - Spain, traditional sort).
            Comparer myCompTrad = new Comparer(new CultureInfo(0x040A, false));
            Console.WriteLine("   Traditional Sort  : {0}", myCompTrad.Compare(str1, str2));


        }
    }
}
