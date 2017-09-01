using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStructuralComparableExample
{
    public class PopulationComparer<T1,T2,T3,T4,T5,T6> : IComparer
    {
        private int itemPosition;
        private int multiplier = -1;

        //public PopulationComparer(int component) : this(component,true)
        //{

        //}

        public PopulationComparer(int component,bool desceding = true)
        {
            if (!desceding) multiplier = 1;
            if (component <= 0 || component > 6)
            {
                throw new ArgumentException("The component argument is out of range");
            }

            itemPosition = component;
        }

        public int Compare(object x,object y)
        {
            var tX = x as Tuple<T1, T2, T3, T4, T5, T6>;
            if (tX == null)
            {
                return 0;
            }
            else
            {
                var tY = y as Tuple<T1, T2, T3, T4, T5, T6>;
                switch (itemPosition)
                {
                    case 1:
                        return Comparer<T1>.Default.Compare(tX.Item1, tY.Item1) * multiplier;
                    case 2:
                        return Comparer<T2>.Default.Compare(tX.Item2, tY.Item2) * multiplier;
                    case 3:
                        return Comparer<T3>.Default.Compare(tX.Item3, tY.Item3) * multiplier;
                    case 4:
                        return Comparer<T4>.Default.Compare(tX.Item4, tY.Item4) * multiplier;
                    case 5:
                        return Comparer<T5>.Default.Compare(tX.Item5, tY.Item5) * multiplier;
                    case 6:
                        return Comparer<T6>.Default.Compare(tX.Item6, tY.Item6) * multiplier;
                    default:
                        return Comparer<T1>.Default.Compare(tX.Item1, tY.Item1) * multiplier;

                }
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // cities, 1960-2000.
            Tuple<string, int, int, int, int, int>[] cities =
           { Tuple.Create("Los Angeles", 2479015, 2816061, 2966850, 3485398, 3694820),
             Tuple.Create("New York", 7781984, 7894862, 7071639, 7322564, 8008278),
             Tuple.Create("Chicago", 3550904, 3366957, 3005072, 2783726, 2896016) };

            // Display array in unsorted order.
            Console.WriteLine("In unsorted order:");
            foreach (var city in cities)
                Console.WriteLine(city.ToString());
            Console.WriteLine();

            Array.Sort(cities, new PopulationComparer<string, int, int, int, int, int>(3));

            // Display array in sorted order.
            Console.WriteLine("Sorted by population in 1970:");
            foreach (var city in cities)
                Console.WriteLine(city.ToString());
            Console.WriteLine();

            Array.Sort(cities, new PopulationComparer<string, int, int, int, int, int>(6));

            // Display array in sorted order.
            Console.WriteLine("Sorted by population in 2000:");
            foreach (var city in cities)
                Console.WriteLine(city.ToString());
        }
    }
}
