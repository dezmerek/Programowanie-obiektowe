using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8
{
    record Student(int id, string name, int ects);
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IEnumerable<int> enumerable = from n in ints
                                          where n % 2 == 0
                                          select n;

            Console.WriteLine(string.Join(", ", enumerable));

            //Zapisz Linq które zwróci liczby wieksze od 5 z tablicy int

            IEnumerable<int> gt5 = from n in ints where n > 5 select n;
            Console.WriteLine(string.Join(", ", gt5));

            Predicate<int> intPredicate = n =>
            {
                //Console.WriteLine("Wywołanie predykatu dla " + n);
                return n % 2 == 0;
            };
            enumerable = from n in ints
                         where intPredicate.Invoke(n)
                         select n;

            enumerable = from n in enumerable
                         where n > 5
                         select n;
            Console.WriteLine(string.Join(", ", enumerable));

            Console.WriteLine("Suma: " + enumerable.Sum());
            Console.WriteLine("Średnia: " + enumerable.Average());
            Console.WriteLine("Ilość: " + enumerable.Count());
            Console.WriteLine("Najwieksza: " + enumerable.Max());
            Console.WriteLine("Najmniejsza: " + enumerable.Min());
            Student[] students =
            {
                new Student(1,"Ewa",5),
                new Student(2,"Ewa",2),
                new Student(4,"Adam",5),
                new Student(3,"Karol",2),
                new Student(6,"Adam",4),
                new Student(5,"Karol",4),
            };

            IEnumerable<string> enumerable1 = from s in students
                                              orderby s.ects
                                              orderby s.name descending
                                              select s.name + ": " + s.ects;

            Console.WriteLine(string.Join("\n", enumerable1));

            //Wyświetl liczby z ints z kolejno scia malejaca

            Console.WriteLine();

            IEnumerable<int> enumerable2 = from i in ints
                                           orderby i descending
                                           select i;
            Console.WriteLine(string.Join(", ", enumerable2));
            Console.WriteLine(string.Join(", ", ints.OrderByDescending(i => i)));
            //za pomocą fluent api wyślwietlić posortowana wg imion
            Console.WriteLine(string.Join("\n", students.OrderByDescending(s => s.name).ThenBy(s => s.ects)));
            //Grupowanie
            IEnumerable<IGrouping<string, Student>> studentNamesGroup = from s in students
                                                                        group s by s.name;
            foreach (var item in studentNamesGroup)
            {
                Console.WriteLine(item.Key + " " + string.Join(", ", item));
            }

            IEnumerable<(string Key, int)> NameCounters = from s in students
                                                          group s by s.name into groupItem
                                                          select (groupItem.Key, groupItem.Count());

            Console.WriteLine(string.Join(", ", NameCounters));
            string str = "ala ma kota ala lubi koty karol lubi psy";
            //oblicz ile razy wystepuje każde slowo w lancuchy
            string[] strs = str.Split(" ");

            IEnumerable<IGrouping<string, string>> WordGroup = from s in strs
                                                               group s by s;

            foreach (var item in WordGroup)
            {
                Console.WriteLine(item.Key + " " + item.Count());
            }

            Console.WriteLine("--------");

            enumerable = ints.Where(i => i % 2 == 0).Select(i => i + 2);
            (int id, string name) student = students
                .Where(s => s.ects > 0)
                .OrderBy(s => s.id)
                .Select(s => (s.id, s.name))
                .FirstOrDefault(s => s.name.StartsWith("A"));

            Console.WriteLine(student);
            Console.WriteLine("______________________________________________________");
            int[] powerInts = Enumerable
                .Range(0, ints.Length)
                .Select(i => ints[i] * ints[i])
                .ToArray();

            Console.WriteLine(string.Join(", ", powerInts));
            Console.WriteLine("---------------");
            Random random = new Random();
            random.Next(5); // zwraca od 0 do 4;
            //Wygeneruj tablice 100 liczb losowych od 0 do 9
            int[] RandomInts = Enumerable
                .Range(0, 100)
                .Select(i => random.Next(10))
                .ToArray();


            Console.WriteLine(string.Join(", ", RandomInts));


            int page = 0;
            int size = 3;
            List<Student> PageStudents = students.Skip(page * size).Take(size).ToList();
            Console.WriteLine(string.Join(",", PageStudents));
        }
    }
}