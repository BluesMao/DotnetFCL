using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AttributeExample
{
    public enum Animal
    {
        Dog = 1,
        Cat,
        Bird,
    }

    public class AnimalTypeAttribute : Attribute
    {
        protected Animal thePet;
        public AnimalTypeAttribute(Animal pet)
        {
            thePet = pet;
        }

        public Animal Pet {

            get { return thePet; }
            set { thePet = value; }
        }      
    }

    class AnimalTypeTestClass
    {
        [AnimalType(Animal.Dog)]
        public void DogMethod() { }

        [AnimalType(Animal.Cat)]
        public void CatMethod() { }

        [AnimalType(Animal.Bird)]
        public void BirdMethod() { }


    }

    class Program
    {
        static void Main(string[] args)
        {
            //var e = Enum.Parse(typeof(Animal),"1");

            AnimalTypeTestClass testClass = new AnimalTypeTestClass();
            Type type = testClass.GetType();

            foreach (MethodInfo mInfo in type.GetMethods())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() == typeof (AnimalTypeAttribute))
                        Console.WriteLine("Method {0} has a pet {1} attribute.",mInfo.Name,((AnimalTypeAttribute)attr).Pet);
                }
            }

            Console.ReadKey();
        }
    }
}
