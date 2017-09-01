using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionBaseExample
{
    public class Int16Collectoin : CollectionBase
    {
        public Int16 this[int index]
        {
            get
            {
                return (Int16)List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(Int16 value)
        {
            return List.Add(value);
        }

        public int IndexOf(Int16 value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index,Int16 value)
        {
            List.Insert(index, value);
        }

        public void Remove(Int16 value)
        {
            List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        public bool Contains(Int16 value)
        {
            return List.Contains(value);
        }


        protected override void OnInsert(int index, object value)
        {
            base.OnInsert(index, value);
        }

        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Int16Collectoin coll = new Int16Collectoin();
            coll.Add(1);
            coll.Add(2);
            coll.Add(3);

            coll.Contains(1);
        }
    }
}
