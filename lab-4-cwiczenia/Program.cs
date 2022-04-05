using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;

namespace lab_4_cwiczenia
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Zadanie1");
            Console.WriteLine();
            (int, int) point1 = (2, 4);
            (int, int) size = (10, 12);
            Direction4 dir = Direction4.UP;
            var point2 = Exercise1.NextPoint(dir, point1, size);
            Console.WriteLine(point2);
            Console.WriteLine();

            Console.WriteLine("Zadanie2");
            Console.WriteLine();
            Exercise2 exercise2 = new Exercise2();
            Console.WriteLine(exercise2.Show());
            Console.WriteLine();

            Console.WriteLine("Zadanie3");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Zadanie4");
            Console.WriteLine();
            Student[] students = {
          new Student("Kowal","Adam", 'A'),
          new Student("Nowak","Ewa", 'A')
        };
            Exercise4.AssignStudentId(students);
        }
    }

    enum Direction8
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UP_LEFT,
        DOWN_LEFT,
        UP_RIGHT,
        DOWN_RIGHT
    }

    enum Direction4
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    //Cwiczenie 1
    //Zdefiniuj metodę NextPoint, która powinna zwracać krotkę ze współrzędnymi piksela przesuniętego jednostkowo w danym kierunku względem piksela point.
    //Krotka screenSize zawiera rozmiary ekranu (width, height)
    //Przyjmij, że początek układu współrzednych (0,0) jest w lewym górnym rogu ekranu, a prawy dolny ma współrzęne (witdh, height) 
    //Pzzykład
    //dla danych wejściowych 
    //(int, int) point1 = (2, 4);
    //Direction4 dir = Direction4.UP;
    //var point2 = NextPoint(dir, point1);
    //w point2 powinny być wartości (2, 3);
    //Jeśli nowe położenie jest poza ekranem to metoda powinna zwrócić krotkę z point
    class Exercise1
    {
        public static (int, int) NextPoint(Direction4 direction, (int, int) point, (int, int) screenSize = default((int, int)))
        {
            if (point.Item1 > screenSize.Item1 || point.Item2 > screenSize.Item2)
                return point;
            if (point.Item1 <= 0 || point.Item2 <= 0)
                return point;

            switch (direction)
            {
                case Direction4.UP: return (point.Item1, point.Item2 - 1);
                case Direction4.DOWN: return (point.Item1, point.Item2 + 1);
                case Direction4.LEFT: return (point.Item1 - 1, point.Item2 - 1);
                case Direction4.RIGHT: return (point.Item1 + 1, point.Item2 - 1);
            }

            return point;
        }
    }
    //Cwiczenie 2
    //Zdefiniuj metodę DirectionTo, która zwraca kierunek do piksela o wartości value z punktu point. W tablicy screen są wartości
    //pikseli. Początek układu współrzędnych (0,0) to lewy górny róg, więc punkt o współrzęnych (1,1) jest powyżej punktu (1,2) 
    //Przykład
    // Dla danych weejsciowych
    // static int[,] screen =
    // {
    //    {1, 0, 0},
    //    {0, 0, 0},
    //    {0, 0, 0}
    // };
    // (int, int) point = (1, 1);
    // po wywołaniu - Direction8 direction = DirectionTo(screen, point, 1);
    // w direction powinna znaleźć się stała UP_LEFT
    class Exercise2
    {
        static int[,] screen =
        {
        {1, 0, 0},
        {0, 0, 0},
        {0, 0, 0}
    };

        private static (int, int) point = (1, 1);

        private Direction8 direction = DirectionTo(screen, point, 1);

        public Direction8 Show()
        {
            return direction;
        }
        public static Direction8 DirectionTo(int[,] screen, (int, int) point, int value)
        {
            (int, int) new_point = point;
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int j = 0; j < screen.GetLength(0); j++)
                {
                    if (screen[i, j] == value)
                    {
                        new_point = (i, j);
                    }
                }
            }

            (int, int) move_point = (new_point.Item1 - point.Item1, new_point.Item2 - point.Item2);
            Direction8 direction8 = move_point switch
            {
                { Item1: < 0, Item2: 0 } => Direction8.LEFT,
                { Item1: > 0, Item2: 0 } => Direction8.RIGHT,
                { Item1: 0, Item2: < 0 } => Direction8.UP,
                { Item1: 0, Item2: > 0 } => Direction8.DOWN,
                { Item1: < 0, Item2: > 0 } => Direction8.DOWN_LEFT,
                { Item1: < 0, Item2: < 0 } => Direction8.UP_LEFT,
                { Item1: > 0, Item2: > 0 } => Direction8.DOWN_RIGHT,
                { Item1: > 0, Item2: < 0 } => Direction8.UP_RIGHT,
            };
            return direction8;
        }
    }

    //Cwiczenie 3
    //Zdefiniuj metodę obliczającą liczbę najczęściej występujących aut w tablicy cars
    //Przykład
    //dla danych wejściowych
    // Car[] _cars = new Car[]
    // {
    //     new Car(),
    //     new Car(Model: "Fiat", true),
    //     new Car(),
    //     new Car(Power: 100),
    //     new Car(Model: "Fiat", true),
    //     new Car(Power: 125),
    //     new Car()
    // };
    //wywołanie CarCounter(Car[] cars) powinno zwrócić 3
    record Car(string Model = "Audi", bool HasPlateNumber = false, int Power = 100);

    class Exercise3
    {
        public static int CarCounter(Car[] cars)
        {
            var DicOfCars = new Dictionary<object, int>();
            foreach (var item in cars)
            {
                if (DicOfCars.ContainsKey(item))
                {
                    DicOfCars[item]++;
                }
                else
                {
                    DicOfCars[item] = 1;
                }
            }
            return DicOfCars.Values.Max();
        }
    }

    record Student(string LastName, string FirstName, char Group, string StudentId = "");
    //Cwiczenie 4
    //Zdefiniuj metodę AssignStudentId, która każdemu studentowi nadaje pole StudentId wg wzoru znak_grupy-trzycyfrowy-numer.
    //Przykład
    //dla danych wejściowych
    //Student[] students = {
    //  new Student("Kowal","Adam", 'A'),
    //  new Student("Nowak","Ewa", 'A')
    //};
    //po wywołaniu metody AssignStudentId(students);
    //w tablicy students otrzymamy:
    // Kowal Adam 'A' - 'A001'
    // Nowal Ewa 'A'  - 'A002'
    //Należy przyjąc, że są tylko trzy możliwe grupy: A, B i C
    class Exercise4
    {
        public static void AssignStudentId(Student[] students)
        {
            for (int i = 0; i < students.Length; i++)
            {
                students[i] = new Student(students[i].LastName, students[i].FirstName, students[i].Group, students[i].Group + (i + 1).ToString("000"));
                Console.WriteLine($"{students[i].LastName} {students[i].FirstName} '{students[i].Group}' - '{students[i].StudentId}'");
            }
        }
    }
}