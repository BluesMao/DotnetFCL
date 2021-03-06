﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerableExample
{
    public class Person
    {
        public string firstName;
        public string lastName;
        public Person(string fristName,string lastName)
        {
            this.firstName = fristName;
            this.lastName = lastName;
        }
    }

    public class People : IEnumerable
    {
        private Person[] _people;
        public People(Person[] pArray)
        {
            _people = new Person[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }

    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public PeopleEnum(Person[] list)
        {
            _people = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Person Current
        {
            get
            {
                try
                {
                    return _people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Person[] peopleArray = new Person[3]
        {
            new Person("Jim", "Johnson"),
            new Person("John", "Smith"),
            new Person("Sue", "Rabon"),

        };
            People peopleList = new People(peopleArray);
            foreach (Person p in peopleList)
                Console.WriteLine(p.firstName + " " + p.lastName);

        }
    }
}
