using System;
using System.Collections.Generic;

namespace ch9_sample_struct_copy
{
    struct Person
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var attendees = new List<Person>();

            Person p = new Person { Name = "old name" };
            attendees.Add(p);

            Person p2 = attendees[0];
            p2.Name = "new name";

            Console.WriteLine(attendees[0].ToString());
        }
    }
}
