using System;

namespace StudentManagementSystem
{
    
    public enum Rating
    {
        Fail,
        Good,
        VeryGood,
        Excellent
    }

    
    public class Student
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Governorate { get; set; }
        public double FirstExam { get; set; }
        public double SecondExam { get; set; }
        public double Average { get; private set; }
        public Rating StudentRating { get; set; }

        public Student(int id, string name, string gov, double g1, double g2, Rating rating)
        {
            ID = id;
            FullName = name;
            Governorate = gov;
            FirstExam = g1;
            SecondExam = g2;
            Average = (g1 + g2) / 2;
            StudentRating = rating;
        }

        public override string ToString()
        {
            return $"ID: {ID} | Name: {FullName} | Gov: {Governorate} | Avg: {Average:F2} | Rating: {StudentRating}";
        }
    }

    public class Node
    {
        public Student Data;
        public Node Next;
        public Node Previous;

        public Node(Student data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }

    public class DoublyLinkedList
    {
        public Node Head;
        public Node Tail;

        public void AddFirst(Student student)
        {
            Node newNode = new Node(student);

            if (Head == null)
            {
                Head = Tail = newNode;
            }
            else
            {
                newNode.Next = Head;
                Head.Previous = newNode;
                Head = newNode;
            }
        }

        public void AddLast(Student student)
        {
            Node newNode = new Node(student);

            if (Tail == null)
            {
                Head = Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Previous = Tail;
                Tail = newNode;
            }
        }

        public void DisplayList()
        {
            if (Head == null)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            Node current = Head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

        public void Sort(bool sortByName)
        {
            if (Head == null) return;

            bool swapped;
            do
            {
                swapped = false;
                Node current = Head;

                while (current.Next != null)
                {
                    bool condition = sortByName
                        ? string.Compare(current.Data.FullName, current.Next.Data.FullName) > 0
                        : current.Data.Average > current.Next.Data.Average;

                    if (condition)
                    {
                        Student temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;
                        swapped = true;
                    }

                    current = current.Next;
                }

            } while (swapped);
        }

        public void RecursiveSearch(Node current, double average)
        {
            if (current == null) return;

            if (current.Data.Average == average)
            {
                Console.WriteLine("Found: " + current.Data);
            }

            RecursiveSearch(current.Next, average);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList list = new DoublyLinkedList();

            Console.WriteLine("=== Student Management System ===");

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"\nEnter Student #{i}");
                list.AddLast(InputStudent());
            }

            bool run = true;
            while (run)
            {
                Console.WriteLine("\n1. Sort");
                Console.WriteLine("2. Recursive Search by Average");
                Console.WriteLine("3. Add Student");
                Console.WriteLine("4. Display All");
                Console.WriteLine("5. Exit");
                Console.Write("Choose: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("1.Name | 2.Average : ");
                        list.Sort(Console.ReadLine() == "1");
                        list.DisplayList();
                        break;

                    case "2":
                        Console.Write("Enter average: ");
                        double avg = double.Parse(Console.ReadLine());
                        list.RecursiveSearch(list.Head, avg);
                        break;

                    case "3":
                        Console.Write("1.Beginning | 2.End : ");
                        string choice = Console.ReadLine();
                        Student s = InputStudent();
                        if (choice == "1") list.AddFirst(s);
                        else list.AddLast(s);
                        break;

                    case "4":
                        list.DisplayList();
                        break;

                    case "5":
                        run = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static Student InputStudent()
        {
            try
            {
                Console.Write("ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Governorate: ");
                string gov = Console.ReadLine();

                Console.Write("First Exam: ");
                double g1 = double.Parse(Console.ReadLine());

                Console.Write("Second Exam: ");
                double g2 = double.Parse(Console.ReadLine());

                Console.Write("Rating (0-3): ");
                Rating r = (Rating)int.Parse(Console.ReadLine());

                return new Student(id, name, gov, g1, g2, r);
            }
            catch
            {
                Console.WriteLine("Invalid input, default student created.");
                return new Student(0, "Unknown", "Unknown", 0, 0, Rating.Fail);
            }
        }
    }
}
