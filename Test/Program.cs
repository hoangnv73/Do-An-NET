using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Student>() {
            new Student(){ Id = 1, FirstName="Nguyen", LastName="Hoang", Score = 9},
            new Student(){ Id = 2, FirstName="Hello", LastName="ABC", Score = 5},
            new Student(){ Id = 3, FirstName="Hi", LastName="CD", Score = 1},
            };

            // Add vao mang
            students.Add(new Student() { Id = 4, FirstName = "Van", LastName = "Hoang", Score = 10 });

            // Hoc sinh co diem cao nhat
            var maxScore = students.Max(student => student.Score);
            Console.WriteLine("Hoc sinh co diem cao nhat la: " + maxScore);

            // Hoc sinh co thap cao nhat
            var minScore = students.Min(student => student.Score);
            Console.WriteLine("Hoc sinh co diem thap nhat la: " + minScore);

            // Diem TB cua tat ca hoc sinh
            var averageScore = students.Sum(student => student.Score) / students.Count();
            Console.WriteLine("Diem TB cua hoc sinh la: " + averageScore);

            // In ra man hinh
            var classification = "";
            foreach (var student in students)
            {
                if (student.Score < 5)
                {
                    classification = "Yeu";
                }
                if (student.Score >= 5 && student.Score < 6.5)
                {
                    classification = "TB";
                }
                if (student.Score >= 6.5 && student.Score < 8)
                {
                    classification = "Kha";
                }
                if (student.Score > 6.5 && student.Score <= 10)
                {
                    classification = "Gioi";
                }

                Console.WriteLine("Id: " + student.Id + " FullName: " + student.FirstName + " "
                    + student.LastName + " Score: " + student.Score + " Xep loai: " + classification);
            }
            // Sap xep hoc sinh theo bang chu cai
            var personList = students.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();

            Console.WriteLine("Sap xep ten hoc sinh theo ABC: " + personList);
            Console.ReadKey();
        }


    }
}
