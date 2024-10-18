using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laba__6
{
    internal class Program
    {
        enum ProgrammingLanguage
        {
            Cpp,
            Cs,
            Python,
            Dart,
            Java,
            JavaScript
        }
        class Student
        {

            public string Name { get; set; }
            public int CourseNumber { get; set; }
            public ProgrammingLanguage ProgrammingLanguage { get; set; }
            public double Achievement { get; set; }
            public Student(string name, int courseNumber, ProgrammingLanguage programmingLanguage, double achievement)
            {
                Name = name;
                CourseNumber = courseNumber;
                ProgrammingLanguage = programmingLanguage;
                Achievement = achievement;
            }
            public override string ToString()
            {
                return $"{Name} (Course: {CourseNumber}, Language: {ProgrammingLanguage}, Achievement: {Achievement})";
            }
        }
        class Traineeship
        {
            public List<Student> Candidates { get; set; }
            public List<Department> Departments { get; set; }
            public void Distribute()
            {
                foreach(var department in Departments)
                {
                    department.TraineeDistribution(Candidates);
                }
            }
            public void DisplayResults()
            {
                foreach (var department in Departments)
                {
                    Console.WriteLine($"{department.Title} Trainees:");
                    foreach (var trainee in department.Trainees)
                    {
                        Console.WriteLine(trainee);
                    }
                    Console.WriteLine();
                }

                if (Candidates.Count > 0)
                {
                    Console.WriteLine("Кандидаты, не отобранные ни для одного отдела:");
                    foreach (var candidate in Candidates)
                    {
                        Console.WriteLine(candidate);
                    }
                }
                else
                {
                    Console.WriteLine("Все кандидаты были отобраны.");
                }
            }
        }
        class Department
        {
            public string Title { get; set; }
            public List<Student> Trainees { get; set; }
            public int NumberOfPositions { get; set; }
            public Department(string title, int numberOfPositions)
            {
                Title = title;
                NumberOfPositions = numberOfPositions;
                Trainees = new List<Student>();
            }
            public virtual void TraineeDistribution(List<Student> candidates) 
            {
                foreach (Student student in candidates)
                {
                    if (student.CourseNumber >= 2)
                    {
                        Trainees.Add(student);
                        candidates.Remove(student);
                    }
                }
            }
        }
        class GameDevelopment : Department
        {
            public GameDevelopment(int numberOfPositions) : base("Game Development", numberOfPositions) { }

            public override void TraineeDistribution(List<Student> candidates)
            {
                var studentsToRemove = new List<Student>();
                foreach (Student student in candidates)
                {
                    if ((student.ProgrammingLanguage == ProgrammingLanguage.Cs || student.ProgrammingLanguage == ProgrammingLanguage.Cpp) && student.CourseNumber >= 2 && student.Achievement >= 0.80 && Trainees.Count < NumberOfPositions)
                    {
                        Trainees.Add(student);
                        studentsToRemove.Add(student);
                    }
                }
                foreach (Student student in studentsToRemove)
                {
                    candidates.Remove(student);
                }
            }
        }
        class DataScience : Department
        {
            public DataScience(int numberOfPositions) : base("Data Science", numberOfPositions) { }
            public override void TraineeDistribution(List<Student> candidates)
            {
                var studentsToRemove = new List<Student>();
                foreach (Student student in candidates)
                {
                    if (student.ProgrammingLanguage == ProgrammingLanguage.Python && student.Achievement >= 0.85 && Trainees.Count < NumberOfPositions)
                    {
                        Trainees.Add(student);
                        studentsToRemove.Add(student);
                    }
                }
                foreach (Student student in studentsToRemove)
                {
                    candidates.Remove(student);
                }
            }
        }
        class MobileApplicationDevelopment : Department 
        {
            public MobileApplicationDevelopment(int numberOfPositions) : base("Mobile Application Development", numberOfPositions) { }
            public override void TraineeDistribution(List<Student> candidates)
            {
                var studentsToRemove = new List<Student>();
                foreach (Student student in candidates)
                {
                    if (student.ProgrammingLanguage == ProgrammingLanguage.Dart && student.CourseNumber == 3 && student.Achievement >= 0.85 && Trainees.Count < NumberOfPositions)
                    {
                        Trainees.Add(student);
                        studentsToRemove.Add(student);
                    }
                }
                foreach (Student student in studentsToRemove)
                {
                    candidates.Remove(student);
                }
            }
        }
        static void Main(string[] args)
        {
            Traineeship traineeship = new Traineeship();
            traineeship.Candidates = new List<Student>
            {
                new Student("Alexander", 2, ProgrammingLanguage.Cpp, 0.90),
                new Student("Dmitry", 2, ProgrammingLanguage.Python, 0.60),
                new Student("Ivan", 1, ProgrammingLanguage.Dart, 0.75),
                new Student("Sergey", 3, ProgrammingLanguage.Python, 0.85),
                new Student("Mihail", 2, ProgrammingLanguage.Cpp, 0.95),
                new Student("Natalia", 3, ProgrammingLanguage.Dart, 1.00),
                new Student("Anastasia", 1, ProgrammingLanguage.JavaScript, 0.65),
                new Student("Ekaterina", 3, ProgrammingLanguage.Cs, 0.80),
                new Student("Olga", 2, ProgrammingLanguage.Java, 0.90),
                new Student("Vladimir", 1, ProgrammingLanguage.Cs, 0.70)
            };
            traineeship.Departments = new List<Department>
            {
                new GameDevelopment(3),
                new DataScience(2),
                new MobileApplicationDevelopment(1)
            };
            traineeship.Distribute();
            traineeship.DisplayResults();
        }
    }
}
