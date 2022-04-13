using System;
using System.Collections.Generic;

namespace lab_6
{
    class Student:IComparable<Student>
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        public int CompareTo(Student other)
        {
            if (Name.CompareTo(other.Name) == 0)
            {
                return Ects.CompareTo(other.Ects);
            }
            else
            return Name.CompareTo(other.Name);
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Student Equals");
            return obj is Student student &&
                   Name == student.Name &&
                   Ects == student.Ects;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Student HashCode");
            return HashCode.Combine(Name, Ects);
        }

        public override string ToString()
        {
            return $"Name = {Name}, Ects = {Ects}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ICollection<string> names = new List<string>();
            names.Add("Ewa");
            names.Add("Karol");
            names.Add("Adam");
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine(names.Contains("Adam"));

            //
            ICollection<Student> students = new List<Student>();
            students.Add(new Student() { Name = "Ewa", Ects = 12 });
            students.Add(new Student() { Name = "Karol", Ects = 11 });
            students.Add(new Student() { Name = "Adam", Ects = 17 });
            students.Remove(new Student() { Name = "Adam", Ects = 17 });

            foreach (Student name in students)
            {
                Console.WriteLine(name.Name + " " + name.Ects);
            }
            Console.WriteLine(students.Contains(new Student() { Name = "Adam", Ects = 17 }));
            List<Student> list = (List<Student>)students;
            Console.WriteLine(list[0]);
            list.Insert(1, new Student() { Name = "Robert", Ects = 45 });
            foreach (Student name in students)
            {
                Console.WriteLine(name.Name + " " + name.Ects);
            }
            int index = list.IndexOf(new Student() { Name = "Karol", Ects = 11 });
            Console.WriteLine("Karol ma pozycję " + index);
            list.RemoveAt(0);

            Console.WriteLine("---------------SET---------------");

            ISet<string> set = new HashSet<string>();
            set.Add("Ewa");
            set.Add("Karol");
            set.Add("Adam");
            set.Add("Adam");
            set.Add("Zofia");
            foreach (string name in set)
            {
                Console.WriteLine(name);
            }

            ISet<Student> studentGroup = new HashSet<Student>();
            studentGroup.Add(new Student() { Name = "Ewa", Ects = 12 });
            studentGroup.Add(new Student() { Name = "Karol", Ects = 11 });
            studentGroup.Add(new Student() { Name = "Adam", Ects = 17 });
            studentGroup.Add(new Student() { Name = "Adam", Ects = 17 });
            foreach (Student name in studentGroup)
            {
                Console.WriteLine(name.Name + " " + name.Ects);
            }
            studentGroup.Contains(new Student() { Name = "Adam", Ects = 17 });

            //studentGroup.Remove(new Student() { Name = "Karol", Ects = 11 });
            studentGroup = new SortedSet<Student>(studentGroup);
            studentGroup.Add(new Student() { Name = "Ewa", Ects = 3 });
            foreach (Student name in studentGroup)
            {
                Console.WriteLine(name.Name + " " + name.Ects);
            }
            Console.WriteLine(studentGroup.Contains(new Student() { Name = "Adam", Ects = 17 }));
            Dictionary<Student, string> phoneBook = new Dictionary<Student, string>();
            phoneBook[list[0]] = "555444666";
            phoneBook[list[1]] = "111222333";
            phoneBook[new Student() { Name = "Robert", Ects = 45 }] = "777888999";
            Console.WriteLine(phoneBook.Keys);
            if (phoneBook.ContainsKey(new Student() { Name = "Ewa", Ects = 12 }))
            {
                Console.WriteLine("Jest telefon");
            }
            else
            {
                Console.WriteLine("Brak telefonu");
            }

            foreach(var item in phoneBook)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            string[] arr = { "adam", "ewa", "karol", "ewa", "ania", "karol", "adam", "adam", "ewa" };
            Dictionary<string, int> counters = new Dictionary<string, int>();
            foreach(string name in arr)
            {
                if(counters.ContainsKey(name))
                {
                    counters[name] += 1;
                }
                else
                {
                    counters[name] = 1;
                }
                foreach(var item in counters)
                {
                    Console.WriteLine(item.Key + " wystepuje " + item.Value);
                }
            }
        }
    }
}
